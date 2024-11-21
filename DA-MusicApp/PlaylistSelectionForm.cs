using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DA_MusicApp
{
    public partial class PlaylistSelectionForm : Form
    {
        public PlaylistSelectionForm()
        {
            InitializeComponent();
        }
        private string username;
        private bool isok = false;
        private string server = "127.0.0.1";
        private int port = 12345;
        bool reload = false;
        string? mode;
        
        public string SelectedPlaylist { get; private set; }
        public PlaylistSelectionForm(string username)
        {
            InitializeComponent();
            this.username = username;
            LoadPlaylists();
            btnDeletePlaylist.Enabled = false;
            btnOK.Enabled = false;
            UpdateButtonMode();
        }
        public PlaylistSelectionForm(string username, string? mode)
        {
            InitializeComponent();
            this.username = username;
            LoadPlaylists();
            this.mode = mode;
            UpdateButtonMode();
            btnDeletePlaylist.Enabled = false;
            btnOK.Enabled = false;
        }

        private void LoadPlaylists()
        {
            listBoxPlaylists.Items.Clear();
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 12345))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"GET_PLAYLISTS:{username}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[1024];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    string[] playlists = response.Split(',');
                    listBoxPlaylists.Items.AddRange(playlists);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedPlaylist = listBoxPlaylists.SelectedItem?.ToString();
            this.DialogResult = DialogResult.OK;
            isok = true;
            this.Close();
        }

        private void PlaylistSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isok)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnCreatePlaylist_Click(object sender, EventArgs e)
        {
            CreatePlaylistForm createPlaylistForm = new CreatePlaylistForm(username);
            if (createPlaylistForm.ShowDialog() == DialogResult.OK)
            {
                LoadPlaylists();
            }


        }

        private void btnDeletePlaylist_Click(object sender, EventArgs e)
        {
            SelectedPlaylist = listBoxPlaylists.SelectedItem?.ToString();
            var confirmResult = MessageBox.Show($"Are you sure to delete '{SelectedPlaylist}'?", "Confirm Delete", MessageBoxButtons.OKCancel);
            if (confirmResult == DialogResult.OK)
            {
                DeletePlaylist(SelectedPlaylist);
            }

        }

        private void DeletePlaylist(string playlistName)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"DELETE_PLAYLIST:{username}:{playlistName}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS")
                    {
                        MessageBox.Show("Playlist deleted successfully!");
                        reload = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete playlist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            if (reload)
            {
                LoadPlaylists();
                reload = false;
            }
        }

        private void listBoxPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
            btnDeletePlaylist.Enabled = true;
        }
        private void UpdateButtonMode() {
            if (this.mode == "AddSong") {
                btnOK.Text = "Add"; 
            }
            else {
                btnOK.Text = "OK"; 
            }
        }
    }
}
