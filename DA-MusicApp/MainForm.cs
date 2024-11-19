using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        string server = "127.0.0.1"; // Server IP
        int port = 12345; // Server port
        private string selectedSongName;
        private bool isDeleteMode = false;
        private int currentRowIndex = -1;
        private DataTable songDataTable;
        public MainForm(string username, string email)
        {
            InitializeComponent();
            this.username = username;
            this.email = email;
            LoadData();
            LoadSongList();
        }

        private void LoadData()
        {
            txtUsername.Text = username;
            txtEmail.Text = email;
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



        private void btnLogout_Click(object sender, EventArgs e)
        {

            signin signin = new signin();
            signin.Show();
            this.Close();
        }

        private void songList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.currentRowIndex = e.RowIndex;
                selectedSongName = songList.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (isDeleteMode)
                {
                    var confirmResult = MessageBox.Show($"Are you sure to delete '{selectedSongName}'?", "Confirm Delete", MessageBoxButtons.OKCancel);
                    if (confirmResult == DialogResult.OK)
                    {
                        DeleteSong(selectedSongName);
                    }
                    // Tắt chế độ xóa sau khi xóa hoặc hủy
                    isDeleteMode = false;
                    btnDeleteSong.Text = "Delete Song";
                    Cursor = Cursors.Default;
                }
                else
                {
                    PlaySong(selectedSongName);
                }
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
                        Plays playForm = new Plays(songData,this.currentRowIndex);
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
            AddSongForm addSongForm = new AddSongForm(this);
            addSongForm.Show();
        }

        private void btnDeleteSong_Click(object sender, EventArgs e)
        {
            isDeleteMode = !isDeleteMode;
            btnDeleteSong.Text = isDeleteMode ? "Cancel Delete" : "Delete Song"; 
            Cursor = isDeleteMode ? Cursors.Hand : Cursors.Default; // Thay đổi con trỏ thành hình thùng rác
        }
        private void DeleteSong(string songName)
        {
            try { 
                using (TcpClient client = new TcpClient(server, port)) {
                    NetworkStream stream = client.GetStream();
                    string message = $"DELETE_SONG:{songName}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length); 
                    byte[] responseData = new byte[1024];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS") { MessageBox.Show("Song deleted successfully!"); 
                        LoadSongList(); } else { MessageBox.Show("Failed to delete song."); 
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Exception: " + ex.Message); 
            }
        }
    }
}
