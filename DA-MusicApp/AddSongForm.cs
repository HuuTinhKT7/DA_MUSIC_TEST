﻿using System;
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
    public partial class AddSongForm : Form
    {
        string server ; // Server IP
        int port = 12345; // Server port
        private MainForm mainForm;
        private Plays plays;
        public AddSongForm(MainForm mainForm, Plays plays,string server)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.plays = plays;
            this.server = server;
        }
        

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnAddSong_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtArtistName.Text)||string.IsNullOrEmpty(txtSongName.Text)))
            {
                try
                {
                    using (TcpClient client = new TcpClient(server, port))
                    {
                        NetworkStream stream = client.GetStream();
                        string songName = txtSongName.Text;
                        string artist = txtArtistName.Text;
                        string filePath = txtFilePath.Text;
                        byte[] songFile = File.ReadAllBytes(filePath);
                        string songFileBase64 = Convert.ToBase64String(songFile);
                        string message = $"ADD_SONG:{songName}:{artist}:{songFileBase64}";
                        string message0 = "ADDSONG";
                        byte[] data0 = Encoding.UTF8.GetBytes(message0);
                        stream.Write(data0, 0, data0.Length);
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        byte[] responseData = new byte[1024];
                        int bytes = stream.Read(responseData, 0, responseData.Length);
                        string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                        if (response == "SUCCESS")
                        {
                            MessageBox.Show("Song added successfully!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add song.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
        }
    }
}
