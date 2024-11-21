using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace DA_MusicApp
{
    public partial class Plays : Form
    {
        public Plays()
        {
            InitializeComponent();
        }
        private IWavePlayer waveOut;
        private WaveStream mp3Reader;
        private byte[] songData;
        private Thread playThread;
        private System.Windows.Forms.Timer timer;
        string server = "10.0.102.123"; // Server IP
        int port = 12345; // Server port
        private TimeSpan currentTime;
        private bool isPlaying = true;
        private MemoryStream ms;
        private int currentRowIndex = -1;
        public string currentMode = "All Songs";
        private string[] artists = new string[100];
        private int slArtist = 0;
        private string[] songslc;
        private string? username;
        private string selectedSongName;
        private string selectedPlaylist;
        private string selectedArtist;

        public Plays(byte[] songData, int currentRowIndex, string? username)
        {
            InitializeComponent();
            LoadSongList();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Cập nhật mỗi giây
            timer.Tick += Timer_Tick;
            this.songData = songData;
            currentTime = TimeSpan.Zero;
            playThread = new Thread(() => PlaySong(songData));
            playThread.Start();
            this.currentRowIndex = currentRowIndex;
            UpdateButtons();
            this.username = username;

        }

        public void LoadSongList()
        {
            btnAddtoPlaylist.Enabled = false;
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes("GET_SONG_LIST");
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[1024 * 1024];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    string[] songs = response.Split(',');
                    songslc = response.Split(',');
                    var dataTable = new System.Data.DataTable();
                    dataTable.Columns.Add("Song Name");
                    dataTable.Columns.Add("Artist");
                    foreach (var song in songs)
                    {
                        var songDetails = song.Split(':');
                        if (!string.IsNullOrEmpty(songDetails[0]))
                        {
                            if (songDetails.Length == 2)
                            {


                                dataTable.Rows.Add(songDetails[0], songDetails[1]);
                                if (!isArtistExisted(songDetails[1]) && !string.IsNullOrEmpty(songDetails[1]))
                                {
                                    artists[slArtist] = songDetails[1];
                                    slArtist++;
                                }

                            }
                            else dataTable.Rows.Add(songDetails[0]);
                        }
                    }

                    songList.DataSource = dataTable;
                    songList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mp3Reader != null && mp3Reader.CurrentTime.TotalSeconds < mp3Reader.TotalTime.TotalSeconds)
            {
                trackBar1.Value = (int)mp3Reader.CurrentTime.TotalSeconds;
            }
            else if (mp3Reader != null && mp3Reader.CurrentTime >= mp3Reader.TotalTime)
            {
                // Tự động chuyển sang bài tiếp theo khi bài hiện tại phát xong
                PlayNextSong();
                btnStop.Text = "Stop";
                unSelectIndex(currentRowIndex - 1);
                selectIndex(currentRowIndex);
            }
        }

        private void PlaySong(byte[] songData)
        {
            try
            {
                ms = new MemoryStream(songData);
                mp3Reader = new Mp3FileReader(ms);
                waveOut = new WaveOutEvent();
                waveOut.Init(mp3Reader);
                mp3Reader.CurrentTime = currentTime; // Thiết lập thời gian hiện tại
                waveOut.Play();
                this.Invoke((MethodInvoker)delegate
                {
                    trackBar1.Maximum = (int)mp3Reader.TotalTime.TotalSeconds;
                    trackBar1.Value = (int)currentTime.TotalSeconds;
                    timer.Start();
                });
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing song: {ex.Message}");
            }
        }



        private void Plays_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
            if (mp3Reader != null)
            {
                mp3Reader.Dispose();
                mp3Reader = null;
            }
            if (ms != null)
            {
                ms.Dispose();
                ms = null;
            }
            if (playThread != null && playThread.IsAlive)
            {
                playThread.Abort();
            }
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (mp3Reader != null)
            {
                mp3Reader.CurrentTime = TimeSpan.FromSeconds(trackBar1.Value);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    currentTime = mp3Reader.CurrentTime; // Lưu lại thời gian hiện tại
                    waveOut.Pause();
                    btnStop.Text = "Play";
                }
                else if (waveOut.PlaybackState == PlaybackState.Paused)
                {
                    waveOut.Play();
                    mp3Reader.CurrentTime = currentTime; // Thiết lập lại thời gian trước khi tiếp tục
                    btnStop.Text = "Stop";
                }
            }
        }

        private void songList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != this.currentRowIndex)
            {
                unSelectIndex(currentRowIndex);
                selectedSongName = songList.Rows[e.RowIndex].Cells[0].Value.ToString();
                GetAndPlaySong(selectedSongName);
                btnStop.Text = "Stop";
                this.currentRowIndex = e.RowIndex;
                selectIndex(currentRowIndex);
            }
        }

        private void GetAndPlaySong(string songName)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"GET_SONG:{songName}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] songData = new byte[10 * 1024 * 1024]; // Assuming max song file size is 10MB
                    int bytes = stream.Read(songData, 0, songData.Length);
                    if (bytes > 0)
                    {
                        // Dừng bài hát hiện tại
                        if (waveOut != null)
                        {
                            waveOut.Stop();
                            waveOut.Dispose();
                            waveOut = null;
                        }
                        if (mp3Reader != null)
                        {
                            mp3Reader.Dispose();
                            mp3Reader = null;
                        }
                        if (ms != null)
                        {
                            ms.Dispose();
                            ms = null;
                        }
                        // Phát bài hát mới
                        this.songData = songData;
                        currentTime = TimeSpan.Zero;
                        playThread = new Thread(() => PlaySong(songData));
                        playThread.Start();
                    }
                    else
                    { MessageBox.Show("Song not found or failed to load."); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.currentRowIndex > 0)
            {
                this.currentRowIndex--;
                PlaySongAtIndex(this.currentRowIndex);
                UpdateButtons();
                btnStop.Text = "Stop";
                unSelectIndex(currentRowIndex + 1);
                selectIndex(currentRowIndex);
            }
        }

        private void PlaySongAtIndex(int index)
        {
            string songName = songList.Rows[index].Cells[0].Value.ToString();
            GetAndPlaySong(songName);
        }

        private void PlayNextSong()
        {
            if (this.currentRowIndex < songList.Rows.Count - 1)
            {
                this.currentRowIndex++;
                PlaySongAtIndex(this.currentRowIndex);
                UpdateButtons();
            }
        }

        private void UpdateButtons()
        {
            btnBack.Enabled = this.currentRowIndex > 0;
            btnNext.Enabled = this.currentRowIndex < songList.Rows.Count - 2;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.currentRowIndex < songList.Rows.Count - 1)
            {
                this.currentRowIndex++;
                PlaySongAtIndex(this.currentRowIndex);
                UpdateButtons();
                btnStop.Text = "Stop";
                unSelectIndex(this.currentRowIndex - 1);
                selectIndex(this.currentRowIndex);
            }
        }

        private void btnAllsongs_Click(object sender, EventArgs e)
        {
            currentMode = "All Songs";
            LoadSongList();
            lbAllsongs.Text = "All songs (x songs)";
            if(btnAddtoPlaylist.Visible == false)
            {
                btnAddtoPlaylist.Visible = true;
            }
        }

        private void Plays_Shown(object sender, EventArgs e)
        {
            selectIndex(currentRowIndex);
        }
        private void unSelectIndex(int index)
        {
            if (index >= 0 && index < songList.Rows.Count)
            {
                // Trả dòng thứ 3 về trạng thái bình thường
                songList.Rows[index].DefaultCellStyle.BackColor = Color.White; // Màu nền mặc định
                songList.Rows[index].DefaultCellStyle.ForeColor = Color.Black; // Màu chữ mặc định
                songList.Rows[index].Selected = false; // Bỏ chọn dòng
            }
        }

        private void selectIndex(int index)
        {
            songList.Rows[index].DefaultCellStyle.BackColor = Color.LightBlue; // Màu nền
            songList.Rows[index].DefaultCellStyle.ForeColor = Color.Black; // Màu chữ
            songList.Rows[index].Selected = true; // Đánh dấu dòng là đã chọn
        }
        private bool isArtistExisted(string art)
        {
            foreach (string a in artists)
            {
                if (art == a)
                    return true;
            }
            return false;
        }

        private void btnArtists_Click(object sender, EventArgs e)
        {
            
            ArtistSelectionForm artistSelectionForm = new ArtistSelectionForm(artists);
            if (artistSelectionForm.ShowDialog() == DialogResult.OK)
            {
                currentMode = "Artist";
                selectedArtist = artistSelectionForm.SelectedArtist;
                LoadSongsByArtist(selectedArtist);
                lbAllsongs.Text = "Artist: " + selectedArtist;
                if(btnAddtoPlaylist.Visible==false)
                    btnAddtoPlaylist.Visible = true;
            }
        }

        private void LoadSongsByArtist(string artist)
        {
            btnAddtoPlaylist.Enabled = false;
            try
            {
                int slbh = 0;
                string[] songs = new string[100];
                foreach (string x in songslc)
                {
                    string[] parts = x.Split(':');
                    if (parts.Length == 2 && parts[1] == artist)
                    {
                        songs[slbh] = x;
                        slbh++;
                    }
                }

                var dataTable = new DataTable();
                dataTable.Columns.Add("Song Name");
                dataTable.Columns.Add("Artist");
                for (int i = 0; i < slbh; i++)
                {
                    var songDetails = songs[i].Split(':');
                    if (songDetails.Length == 2)
                    {
                        dataTable.Rows.Add(songDetails[0], songDetails[1]);
                    }
                }
                songList.DataSource = dataTable;
                songList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnPlaylist_Click(object sender, EventArgs e)
        {
            
            PlaylistSelectionForm playlistSelectionForm = new PlaylistSelectionForm(username);
            if (playlistSelectionForm.ShowDialog() == DialogResult.OK)
            {
                currentMode = "Playlist";
                this.selectedPlaylist = playlistSelectionForm.SelectedPlaylist;
                LoadSongsByPlaylist(selectedPlaylist);
                lbAllsongs.Text = "Playlist: " + selectedPlaylist;
                if(btnAddtoPlaylist.Visible == true)
                    btnAddtoPlaylist.Visible = false;
            }
        }

        private void LoadSongsByPlaylist(string playlistName)
        {
            btnAddtoPlaylist.Enabled = false;
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"GET_SONGS_BY_PLAYLIST:{username}:{playlistName}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] sizeData = new byte[4];
                    stream.Read(sizeData, 0, sizeData.Length);
                    int playlistSize = BitConverter.ToInt32(sizeData, 0);
                    byte[] responseData = new byte[playlistSize];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    string[] songs = response.Split(','); var dataTable = new DataTable();
                    dataTable.Columns.Add("Song Name");
                    dataTable.Columns.Add("Artist");
                    foreach (var song in songs)
                    {
                        var songDetails = song.Split(':');
                        if (songDetails.Length == 2)
                        {
                            dataTable.Rows.Add(songDetails[0], songDetails[1]);
                        }
                        else dataTable.Rows.Add(songDetails[0]);
                    }
                    songList.DataSource = dataTable;
                    songList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void songList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAddtoPlaylist.Enabled = true;
            selectedSongName = songList.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnAddtoPlaylist_Click(object sender, EventArgs e)
        {
            
            PlaylistSelectionForm playlistSelectionForm = new PlaylistSelectionForm(username, "AddSong"); 
            if (playlistSelectionForm.ShowDialog() == DialogResult.OK) {
                string selectedPlaylist = playlistSelectionForm.SelectedPlaylist; 
                AddSongToPlaylist(selectedPlaylist, selectedSongName); 
            }
        }
        private void AddSongToPlaylist(string playlistName, string songName) { 
            try {
                using (TcpClient client = new TcpClient(server, port)) { 
                    NetworkStream stream = client.GetStream();
                    string message = $"ADD_SONG_TO_PLAYLIST:{username}:{playlistName}:{songName}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS") { 
                        MessageBox.Show("Song added to playlist successfully!"); 
                    }
                    else {
                        MessageBox.Show("Failed to add song to playlist."); 
                    }
                }
            }
            
            catch (Exception ex) {
                MessageBox.Show("Exception: " + ex.Message); 
            }
        }

        public void reloadsonglist()
        {
            if(currentMode == "All Songs")
            {
                LoadSongList();
            }
            else if(currentMode == "Playlist")
            {
                LoadSongsByPlaylist(selectedPlaylist);
            }
            else
            {
                LoadSongsByArtist(selectedArtist);
            }
        }
    }
}
