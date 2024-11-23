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
    public partial class ShareRequestsForm : Form
    {
        private string username;
        private string server;
        PlaylistSelectionForm PlaylistSelectionForm;
        public ShareRequestsForm()
        {
            InitializeComponent();
        }
        public ShareRequestsForm(string username, string server, PlaylistSelectionForm playlistSelectionForm)
        {
            InitializeComponent();
            this.username = username;
            this.server = server;
            LoadShareRequests();
            PlaylistSelectionForm = playlistSelectionForm;
        }

        private void LoadShareRequests()
        {
            try
            {
                using (TcpClient client = new TcpClient(server, 12345))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"GET_SHARE_REQUESTS:{username}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[2000];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "none")
                        listBoxRequests.Items.Add("Không có request nào");
                    else
                    {
                        string[] requests = response.Split(',');
                        listBoxRequests.Items.AddRange(requests);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void listBoxRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxRequests.SelectedItem != null)
            {
                var selectedRequest = listBoxRequests.SelectedItem.ToString();
                var parts = selectedRequest.Split(':');
                int requestId = int.Parse(parts[0]);
                string requestInfo = parts[1];
                var result = MessageBox.Show($"{requestInfo}\nWould you like to accept or delete this request?", "Share Request", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    AcceptShareRequest(requestId);
                }
                else if (result == DialogResult.No)
                {
                    DeleteShareRequest(requestId);
                }
            }
        }

        private void AcceptShareRequest(int requestId)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, 12345))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"ACCEPT_SHARE_REQUEST:{requestId}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS")
                    {
                        MessageBox.Show("Playlist accepted successfully!");
                        this.PlaylistSelectionForm.LoadPlaylists();
                        LoadShareRequests(); // Reload share requests after accepting
                    }
                    else
                    {
                        MessageBox.Show("Failed to accept playlist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void DeleteShareRequest(int requestId)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, 12345))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"DELETE_SHARE_REQUEST:{requestId}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "SUCCESS")
                    {
                        MessageBox.Show("Share request deleted successfully!");
                        LoadShareRequests(); // Reload share requests after deleting
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete share request.");
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
