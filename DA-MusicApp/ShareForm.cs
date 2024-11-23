using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_MusicApp
{
    public partial class ShareForm : Form
    {
        private string username;
        private string playlistName;
        private string server;
        public ShareForm()
        {
            InitializeComponent();
        }
        public ShareForm(string username, string playlistname, string server)
        {
            InitializeComponent();
            this.username = username;
            this.playlistName = playlistname;
            this.server = server;
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (TcpClient client = new TcpClient(server, 12345))
                {
                    NetworkStream stream = client.GetStream();
                    string message = "GET_USERS";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[1024];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    string[] users = response.Split(',');
                    foreach (string user in users)
                    {
                        if(user!=this.username)
                            listBoxUsers.Items.Add(user);
                    }
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItem != null) {
                string selectedUser = listBoxUsers.SelectedItem.ToString();
                SharePlaylist(selectedUser); 
            }
            else {
                MessageBox.Show("Please select a user to share with."); 
            }
        }
        private void SharePlaylist(string selectedUser) { 
            try { using (TcpClient client = new TcpClient(server, 12345)) {
                    NetworkStream stream = client.GetStream(); 
                    string message = $"SHARE_PLAYLIST:{username}:{playlistName}:{selectedUser}";
                    byte[] data = Encoding.UTF8.GetBytes(message); 
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes); 
                    if (response == "SUCCESS") {
                        MessageBox.Show("Playlist shared successfully!");
                        this.Close();
                    }
                    else {
                        MessageBox.Show("Failed to share playlist."); 
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Exception: " + ex.Message); 
            }
        }
    }
}
