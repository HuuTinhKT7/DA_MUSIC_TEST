namespace DA_MusicApp
{
    partial class MainForm
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
            txtEmail = new TextBox();
            label4 = new Label();
            txtUsername = new TextBox();
            label1 = new Label();
            btnLogout = new Button();
            songList = new DataGridView();
            txtSearch = new TextBox();
            label2 = new Label();
            btnAdd = new Button();
            btnDeleteSong = new Button();
            btnArtists = new Button();
            btnPlaylist = new Button();
            btnAllsongs = new Button();
            lbAllsongs = new Label();
            btnAddtoPlaylist = new Button();
            ((System.ComponentModel.ISupportInitialize)songList).BeginInit();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(148, 289);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(171, 31);
            txtEmail.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 292);
            label4.Name = "label4";
            label4.Size = new Size(54, 25);
            label4.TabIndex = 16;
            label4.Text = "Email";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(148, 238);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(171, 31);
            txtUsername.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 241);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 14;
            label1.Text = "Username";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(182, 326);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(82, 34);
            btnLogout.TabIndex = 18;
            btnLogout.Text = "Log out";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // songList
            // 
            songList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            songList.Location = new Point(494, 154);
            songList.Name = "songList";
            songList.RowHeadersWidth = 62;
            songList.Size = new Size(486, 543);
            songList.TabIndex = 19;
            songList.CellClick += songList_CellClick;
            songList.CellContentDoubleClick += songList_CellContentDoubleClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(494, 50);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(486, 31);
            txtSearch.TabIndex = 20;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(494, 22);
            label2.Name = "label2";
            label2.Size = new Size(345, 25);
            label2.TabIndex = 22;
            label2.Text = "search for song (type song name or artist)";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(356, 642);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(112, 34);
            btnAdd.TabIndex = 23;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDeleteSong
            // 
            btnDeleteSong.Location = new Point(356, 587);
            btnDeleteSong.Name = "btnDeleteSong";
            btnDeleteSong.Size = new Size(112, 34);
            btnDeleteSong.TabIndex = 25;
            btnDeleteSong.Text = "Delete";
            btnDeleteSong.UseVisualStyleBackColor = true;
            btnDeleteSong.Click += btnDeleteSong_Click;
            // 
            // btnArtists
            // 
            btnArtists.Location = new Point(868, 93);
            btnArtists.Name = "btnArtists";
            btnArtists.Size = new Size(112, 34);
            btnArtists.TabIndex = 28;
            btnArtists.Text = "Artists";
            btnArtists.UseVisualStyleBackColor = true;
            btnArtists.Click += btnArtists_Click;
            // 
            // btnPlaylist
            // 
            btnPlaylist.Location = new Point(680, 93);
            btnPlaylist.Name = "btnPlaylist";
            btnPlaylist.Size = new Size(112, 34);
            btnPlaylist.TabIndex = 27;
            btnPlaylist.Text = "Playlist";
            btnPlaylist.UseVisualStyleBackColor = true;
            btnPlaylist.Click += btnPlaylist_Click;
            // 
            // btnAllsongs
            // 
            btnAllsongs.Location = new Point(494, 93);
            btnAllsongs.Name = "btnAllsongs";
            btnAllsongs.Size = new Size(112, 34);
            btnAllsongs.TabIndex = 26;
            btnAllsongs.Text = "All Songs";
            btnAllsongs.UseVisualStyleBackColor = true;
            btnAllsongs.Click += btnAllsongs_Click;
            // 
            // lbAllsongs
            // 
            lbAllsongs.AutoSize = true;
            lbAllsongs.Location = new Point(494, 126);
            lbAllsongs.Name = "lbAllsongs";
            lbAllsongs.Size = new Size(161, 25);
            lbAllsongs.TabIndex = 29;
            lbAllsongs.Text = "All songs (x songs)";
            // 
            // btnAddtoPlaylist
            // 
            btnAddtoPlaylist.Location = new Point(308, 537);
            btnAddtoPlaylist.Name = "btnAddtoPlaylist";
            btnAddtoPlaylist.Size = new Size(160, 34);
            btnAddtoPlaylist.TabIndex = 30;
            btnAddtoPlaylist.Text = "Add to playlist";
            btnAddtoPlaylist.UseVisualStyleBackColor = true;
            btnAddtoPlaylist.Click += btnAddtoPlaylist_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1009, 751);
            Controls.Add(btnAddtoPlaylist);
            Controls.Add(lbAllsongs);
            Controls.Add(btnArtists);
            Controls.Add(btnPlaylist);
            Controls.Add(btnAllsongs);
            Controls.Add(btnDeleteSong);
            Controls.Add(btnAdd);
            Controls.Add(label2);
            Controls.Add(txtSearch);
            Controls.Add(songList);
            Controls.Add(btnLogout);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(txtUsername);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "MainForm";
            Shown += MainForm_Shown;
            ((System.ComponentModel.ISupportInitialize)songList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmail;
        private Label label4;
        private TextBox txtUsername;
        private Label label1;
        private Button btnLogout;
        private DataGridView songList;
        private TextBox txtSearch;
        private Label label2;
        private Button btnAdd;
        private Button btnDeleteSong;
        private Button btnArtists;
        private Button btnPlaylist;
        private Button btnAllsongs;
        private Label lbAllsongs;
        private Button btnAddtoPlaylist;
    }
}