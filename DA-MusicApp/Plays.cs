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
        string server = "127.0.0.1"; // Server IP
        int port = 12345; // Server port
        private TimeSpan currentTime;
        private bool isPlaying = true;
        private MemoryStream ms;
        private int currentRowIndex = -1;

        public Plays(byte[] songData, int currentRowIndex)
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
        }

        public void LoadSongList()
        {
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
                    var dataTable = new System.Data.DataTable();
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
            if (waveOut != null) {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null; 
            }
            if (mp3Reader != null) {
                mp3Reader.Dispose();
                mp3Reader = null; 
            }
            if (ms != null) {
                ms.Dispose();
                ms = null; 
            }
            if (playThread != null && playThread.IsAlive) {
                playThread.Abort(); 
            }
            if (timer != null) { 
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
                string selectedSongName = songList.Rows[e.RowIndex].Cells[0].Value.ToString();
                GetAndPlaySong(selectedSongName);
                btnStop.Text = "Stop";
                this.currentRowIndex = e.RowIndex;
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
            if (this.currentRowIndex < songList.Rows.Count - 1) {
                this.currentRowIndex++;
                PlaySongAtIndex(this.currentRowIndex);
                UpdateButtons();
                btnStop.Text = "Stop";
            }
        }
    }
}
