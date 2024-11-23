using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_MusicApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        string? username;
        string? email;
        string server; // Server IP
        int port = 12345; // Server port
        private string selectedSongName;
        private string selectedArtist;
        private bool isDeleteMode = false;
        private int currentRowIndex = -1;
        private DataTable songDataTable;
        signin signin;
        Plays plays;
        int signinbytoken = 0;
        private TcpClient client;
        private NetworkStream stream;
        private Thread listenThread;
        private string currentMode = "All Songs";
        string selectedPlaylist;
        private string[] artists = new string[100];
        private int slArtist = 0;
        private string[] songslc;
       

        public MainForm(string username, string email, signin signin, int signinbytoken)
        {
            InitializeComponent();
            this.signin = signin;
            server = signin.server;
            this.username = username;
            this.email = email;
            LoadData();
            LoadSongList();
            ConnectToServer();
            this.signinbytoken = signinbytoken;
            btnDeleteSong.Enabled = false;
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient(server, 12345);
                stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes("CONNECT");
                stream.Write(data, 0, data.Length);
                listenThread = new Thread(ListenForNotifications);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
        private void ListenForNotifications()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytes);
                    if (message == "UPDATE_SONGLIST")
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(LoadSongList));
                        }
                        else
                        {
                            LoadSongList();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        private void LoadData()
        {
            txtUsername.Text = username;
            txtEmail.Text = email;
        }

        public void LoadSongList()
        {
            btnAddtoPlaylist.Enabled = false;
            try
            {
                slArtist = 0;
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
                    songDataTable = new DataTable();
                    songDataTable.Columns.Add("Song Name");
                    songDataTable.Columns.Add("Artist");
                    foreach (var song in songs)
                    {
                        var songDetails = song.Split(':');
                        if (!string.IsNullOrEmpty(songDetails[0]))
                        {
                            if (songDetails.Length == 2)
                            {


                                songDataTable.Rows.Add(songDetails[0], songDetails[1]);
                                if (!isArtistExisted(songDetails[1]) && !string.IsNullOrEmpty(songDetails[1]))
                                {
                                    artists[slArtist] = songDetails[1];
                                    slArtist++;
                                }

                            }
                            else songDataTable.Rows.Add(songDetails[0]);
                        }
                    }

                    songList.DataSource = songDataTable;
                    songList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }



        private void btnLogout_Click(object sender, EventArgs e)
        {

            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"LOGOUT:{this.username}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS")
                    {
                        MessageBox.Show("Logout successful!");
                        if (File.Exists("token.txt"))
                        {
                            File.Delete("token.txt");
                        }
                        signin.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Logout failed. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void songList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                this.currentRowIndex = e.RowIndex;
                selectedSongName = songList.Rows[e.RowIndex].Cells[0].Value.ToString();
                string slArtist = songList.Rows[e.RowIndex].Cells[1].Value.ToString();
                PlaySong(selectedSongName);

            }
        }

        private void PlaySong(string songName)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"GET_SONG:{songName}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] songData = new byte[10 * 1024 * 1024]; // Assuming max song file size is 1MB
                    int bytes = stream.Read(songData, 0, songData.Length);
                    if (bytes > 0)
                    {
                        Plays playForm = new Plays(songData, this.currentRowIndex, username, server);
                        plays = playForm;
                        playForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Song not found or failed to load.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSongForm addSongForm = new AddSongForm(this, plays, server);
            addSongForm.Show();
        }

        private void btnDeleteSong_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show($"Are you sure to delete '{selectedSongName}'?", "Confirm Delete", MessageBoxButtons.OKCancel);
            if (confirmResult == DialogResult.OK)
            {
                DeleteSong(selectedSongName, selectedArtist);
            }
        }
        private void DeleteSong(string songName, string artist)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"DELETE_SONG:{songName}:{artist}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[1024];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS")
                    {
                        MessageBox.Show("Song deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete song.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtSearch.Text.ToLower();
            DataView dv = songDataTable.DefaultView;
            dv.RowFilter = $"[Song Name] LIKE '%{filterText}%' OR [Artist] LIKE '%{filterText}%'";
            songList.DataSource = dv;
            btnDeleteSong.Enabled = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.signin.Hide();
        }

        private void songList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDeleteSong.Enabled = true;
            selectedSongName = songList.Rows[e.RowIndex].Cells[0].Value.ToString();
            selectedArtist = songList.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnAllsongs_Click(object sender, EventArgs e)
        {
            currentMode = "All Songs";
            LoadSongList();
            lbAllsongs.Text = "All songs (x songs)";
        }

        private void btnPlaylist_Click(object sender, EventArgs e)
        {

            PlaylistSelectionForm playlistSelectionForm = new PlaylistSelectionForm(username, server);
            if (playlistSelectionForm.ShowDialog() == DialogResult.OK)
            {
                currentMode = "Playlist";
                this.selectedPlaylist = playlistSelectionForm.SelectedPlaylist;
                LoadSongsByPlaylist(selectedPlaylist);
                lbAllsongs.Text = "Playlist: " + selectedPlaylist;
                if (btnAddtoPlaylist.Visible == true)
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

        private void btnArtists_Click(object sender, EventArgs e)
        {

            ArtistSelectionForm artistSelectionForm = new ArtistSelectionForm(artists);
            if (artistSelectionForm.ShowDialog() == DialogResult.OK)
            {
                currentMode = "Artist";
                selectedArtist = artistSelectionForm.SelectedArtist;
                LoadSongsByArtist(selectedArtist);
                lbAllsongs.Text = "Artist: " + selectedArtist;
                if (btnAddtoPlaylist.Visible == false)
                    btnAddtoPlaylist.Visible = true;
            }
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

        private void btnAddtoPlaylist_Click(object sender, EventArgs e)
        {

            PlaylistSelectionForm playlistSelectionForm = new PlaylistSelectionForm(username, "AddSong", server);
            if (playlistSelectionForm.ShowDialog() == DialogResult.OK)
            {
                string selectedPlaylist = playlistSelectionForm.SelectedPlaylist;
                AddSongToPlaylist(selectedPlaylist, selectedSongName);
            }
        }

        private void AddSongToPlaylist(string playlistName, string songName)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"ADD_SONG_TO_PLAYLIST:{username}:{playlistName}:{songName}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS")
                    {
                        MessageBox.Show("Song added to playlist successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add song to playlist.");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
    }
}
