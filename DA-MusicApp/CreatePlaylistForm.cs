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

namespace DA_MusicApp
{
    public partial class CreatePlaylistForm : Form
    {
        public CreatePlaylistForm()
        {
            InitializeComponent();
        }
        private string username;
        private bool isok = false;
        public CreatePlaylistForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string playlistName = txtPlaylistName.Text;
            if (!string.IsNullOrWhiteSpace(playlistName))
            {
                try
                {
                    using (TcpClient client = new TcpClient("10.0.102.123", 12345))
                    {
                        NetworkStream stream = client.GetStream();
                        string message = $"CREATE_PLAYLIST:{username}:{playlistName}";
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        byte[] responseData = new byte[256];
                        int bytes = stream.Read(responseData, 0, responseData.Length);
                        string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                        if (response == "SUCCESS")
                        {
                            MessageBox.Show("Playlist created successfully!");
                            this.DialogResult = DialogResult.OK;
                            isok = true;
                            this.Close();
                        }
                        else if (response == "EXISTS") {
                            MessageBox.Show("Playlist already exists. Please choose a different name.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to create playlist.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
            }
        }

        private void CreatePlaylistForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isok)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            
        }
    }
}
