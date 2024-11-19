namespace DA_MusicApp
{
    partial class Plays
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plays));
            btnAllsongs = new Button();
            btnPlaylist = new Button();
            btnArtists = new Button();
            pictureBox1 = new PictureBox();
            lbAllsongs = new Label();
            lbSongname = new Label();
            label2 = new Label();
            btnBack = new Button();
            btnNext = new Button();
            btnStop = new Button();
            btnComment = new Button();
            panel1 = new Panel();
            trackBar1 = new TrackBar();
            picShuffle = new PictureBox();
            picLoop = new PictureBox();
            songList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picShuffle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLoop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)songList).BeginInit();
            SuspendLayout();
            // 
            // btnAllsongs
            // 
            btnAllsongs.Location = new Point(72, 52);
            btnAllsongs.Name = "btnAllsongs";
            btnAllsongs.Size = new Size(112, 34);
            btnAllsongs.TabIndex = 3;
            btnAllsongs.Text = "All Songs";
            btnAllsongs.UseVisualStyleBackColor = true;
            // 
            // btnPlaylist
            // 
            btnPlaylist.Location = new Point(258, 52);
            btnPlaylist.Name = "btnPlaylist";
            btnPlaylist.Size = new Size(112, 34);
            btnPlaylist.TabIndex = 4;
            btnPlaylist.Text = "Playlist";
            btnPlaylist.UseVisualStyleBackColor = true;
            // 
            // btnArtists
            // 
            btnArtists.Location = new Point(446, 52);
            btnArtists.Name = "btnArtists";
            btnArtists.Size = new Size(112, 34);
            btnArtists.TabIndex = 5;
            btnArtists.Text = "Artists";
            btnArtists.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(17, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // lbAllsongs
            // 
            lbAllsongs.AutoSize = true;
            lbAllsongs.Location = new Point(39, 100);
            lbAllsongs.Name = "lbAllsongs";
            lbAllsongs.Size = new Size(161, 25);
            lbAllsongs.TabIndex = 8;
            lbAllsongs.Text = "All songs (x songs)";
            // 
            // lbSongname
            // 
            lbSongname.AutoSize = true;
            lbSongname.Location = new Point(17, 89);
            lbSongname.Name = "lbSongname";
            lbSongname.Size = new Size(103, 25);
            lbSongname.TabIndex = 9;
            lbSongname.Text = "Song name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 114);
            label2.Name = "label2";
            label2.Size = new Size(62, 25);
            label2.TabIndex = 10;
            label2.Text = "Artists";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(205, 105);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(74, 34);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(395, 105);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(71, 34);
            btnNext.TabIndex = 12;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(306, 105);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(69, 34);
            btnStop.TabIndex = 13;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnComment
            // 
            btnComment.Location = new Point(549, 512);
            btnComment.Name = "btnComment";
            btnComment.Size = new Size(112, 34);
            btnComment.TabIndex = 14;
            btnComment.Text = "comment";
            btnComment.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(trackBar1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(lbSongname);
            panel1.Controls.Add(btnStop);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnNext);
            panel1.Controls.Add(btnBack);
            panel1.Location = new Point(39, 570);
            panel1.Name = "panel1";
            panel1.Size = new Size(519, 150);
            panel1.TabIndex = 15;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(205, 17);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(299, 69);
            trackBar1.TabIndex = 16;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // picShuffle
            // 
            picShuffle.Image = Properties.Resources.shuffle;
            picShuffle.Location = new Point(564, 583);
            picShuffle.Name = "picShuffle";
            picShuffle.Size = new Size(76, 60);
            picShuffle.SizeMode = PictureBoxSizeMode.Zoom;
            picShuffle.TabIndex = 16;
            picShuffle.TabStop = false;
            // 
            // picLoop
            // 
            picLoop.Image = (Image)resources.GetObject("picLoop.Image");
            picLoop.Location = new Point(564, 649);
            picLoop.Name = "picLoop";
            picLoop.Size = new Size(76, 60);
            picLoop.SizeMode = PictureBoxSizeMode.Zoom;
            picLoop.TabIndex = 17;
            picLoop.TabStop = false;
            // 
            // songList
            // 
            songList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            songList.Location = new Point(39, 128);
            songList.Name = "songList";
            songList.RowHeadersWidth = 62;
            songList.Size = new Size(504, 418);
            songList.TabIndex = 20;
            songList.CellContentDoubleClick += songList_CellContentDoubleClick;
            // 
            // Plays
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(721, 738);
            Controls.Add(songList);
            Controls.Add(picLoop);
            Controls.Add(picShuffle);
            Controls.Add(panel1);
            Controls.Add(btnComment);
            Controls.Add(lbAllsongs);
            Controls.Add(btnArtists);
            Controls.Add(btnPlaylist);
            Controls.Add(btnAllsongs);
            Name = "Plays";
            Text = "Plays";
            FormClosing += Plays_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picShuffle).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLoop).EndInit();
            ((System.ComponentModel.ISupportInitialize)songList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAllsongs;
        private Button btnPlaylist;
        private Button btnArtists;
        private PictureBox pictureBox1;
        private Label lbAllsongs;
        private Label lbSongname;
        private Label label2;
        private Button btnBack;
        private Button btnNext;
        private Button btnStop;
        private Button btnComment;
        private Panel panel1;
        private TrackBar trackBar1;
        private PictureBox picShuffle;
        private PictureBox picLoop;
        private DataGridView songList;
    }
}