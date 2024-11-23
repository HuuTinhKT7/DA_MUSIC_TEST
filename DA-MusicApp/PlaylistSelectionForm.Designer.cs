namespace DA_MusicApp
{
    partial class PlaylistSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBoxPlaylists = new ListBox();
            btnOK = new Button();
            btnCreatePlaylist = new Button();
            btnDeletePlaylist = new Button();
            btnShare = new Button();
            btnRequest = new Button();
            SuspendLayout();
            // 
            // listBoxPlaylists
            // 
            listBoxPlaylists.FormattingEnabled = true;
            listBoxPlaylists.ItemHeight = 25;
            listBoxPlaylists.Location = new Point(59, 41);
            listBoxPlaylists.Name = "listBoxPlaylists";
            listBoxPlaylists.Size = new Size(315, 354);
            listBoxPlaylists.TabIndex = 0;
            listBoxPlaylists.SelectedIndexChanged += listBoxPlaylists_SelectedIndexChanged;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(389, 361);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 34);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCreatePlaylist
            // 
            btnCreatePlaylist.Location = new Point(389, 256);
            btnCreatePlaylist.Name = "btnCreatePlaylist";
            btnCreatePlaylist.Size = new Size(112, 34);
            btnCreatePlaylist.TabIndex = 3;
            btnCreatePlaylist.Text = "create";
            btnCreatePlaylist.UseVisualStyleBackColor = true;
            btnCreatePlaylist.Click += btnCreatePlaylist_Click;
            // 
            // btnDeletePlaylist
            // 
            btnDeletePlaylist.Location = new Point(389, 310);
            btnDeletePlaylist.Name = "btnDeletePlaylist";
            btnDeletePlaylist.Size = new Size(112, 34);
            btnDeletePlaylist.TabIndex = 4;
            btnDeletePlaylist.Text = "delete";
            btnDeletePlaylist.UseVisualStyleBackColor = true;
            btnDeletePlaylist.Click += btnDeletePlaylist_Click;
            // 
            // btnShare
            // 
            btnShare.Location = new Point(380, 41);
            btnShare.Name = "btnShare";
            btnShare.Size = new Size(112, 34);
            btnShare.TabIndex = 5;
            btnShare.Text = "Share";
            btnShare.UseVisualStyleBackColor = true;
            btnShare.Click += btnShare_Click;
            // 
            // btnRequest
            // 
            btnRequest.Location = new Point(380, 81);
            btnRequest.Name = "btnRequest";
            btnRequest.Size = new Size(112, 34);
            btnRequest.TabIndex = 6;
            btnRequest.Text = "request";
            btnRequest.UseVisualStyleBackColor = true;
            btnRequest.Click += btnRequest_Click;
            // 
            // PlaylistSelectionForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 407);
            Controls.Add(btnRequest);
            Controls.Add(btnShare);
            Controls.Add(btnDeletePlaylist);
            Controls.Add(btnCreatePlaylist);
            Controls.Add(btnOK);
            Controls.Add(listBoxPlaylists);
            Name = "PlaylistSelectionForm";
            Text = "PlaylistSelectionForm";
            FormClosing += PlaylistSelectionForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxPlaylists;
        private Button btnOK;
        private Button btnCreatePlaylist;
        private Button btnDeletePlaylist;
        private Button btnShare;
        private Button btnRequest;
    }
}