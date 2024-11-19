namespace DA_MusicApp
{
    partial class AddSongForm
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
            txtSongName = new TextBox();
            txtArtistName = new TextBox();
            txtFilePath = new TextBox();
            btnAddSong = new Button();
            btnBrowse = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtSongName
            // 
            txtSongName.Location = new Point(145, 48);
            txtSongName.Name = "txtSongName";
            txtSongName.Size = new Size(198, 31);
            txtSongName.TabIndex = 0;
            // 
            // txtArtistName
            // 
            txtArtistName.Location = new Point(145, 100);
            txtArtistName.Name = "txtArtistName";
            txtArtistName.Size = new Size(198, 31);
            txtArtistName.TabIndex = 1;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(145, 154);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(198, 31);
            txtFilePath.TabIndex = 2;
            // 
            // btnAddSong
            // 
            btnAddSong.Location = new Point(190, 191);
            btnAddSong.Name = "btnAddSong";
            btnAddSong.Size = new Size(112, 34);
            btnAddSong.TabIndex = 3;
            btnAddSong.Text = "Add song";
            btnAddSong.UseVisualStyleBackColor = true;
            btnAddSong.Click += btnAddSong_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(366, 151);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(93, 34);
            btnBrowse.TabIndex = 4;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 51);
            label1.Name = "label1";
            label1.Size = new Size(103, 25);
            label1.TabIndex = 5;
            label1.Text = "Song name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 103);
            label2.Name = "label2";
            label2.Size = new Size(103, 25);
            label2.TabIndex = 6;
            label2.Text = "Artist name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 160);
            label3.Name = "label3";
            label3.Size = new Size(46, 25);
            label3.TabIndex = 7;
            label3.Text = "Path";
            // 
            // AddSongForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 234);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBrowse);
            Controls.Add(btnAddSong);
            Controls.Add(txtFilePath);
            Controls.Add(txtArtistName);
            Controls.Add(txtSongName);
            Name = "AddSongForm";
            Text = "AddSongForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSongName;
        private TextBox txtArtistName;
        private TextBox txtFilePath;
        private Button btnAddSong;
        private Button btnBrowse;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}