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
            textBox1 = new TextBox();
            btnSearch = new Button();
            label2 = new Label();
            btnAdd = new Button();
            btnDeleteSong = new Button();
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
            songList.Location = new Point(506, 133);
            songList.Name = "songList";
            songList.RowHeadersWidth = 62;
            songList.Size = new Size(424, 543);
            songList.TabIndex = 19;
            songList.CellContentDoubleClick += songList_CellContentDoubleClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(506, 50);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(394, 31);
            textBox1.TabIndex = 20;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(909, 50);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(88, 34);
            btnSearch.TabIndex = 21;
            btnSearch.Text = "search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(506, 22);
            label2.Name = "label2";
            label2.Size = new Size(135, 25);
            label2.TabIndex = 22;
            label2.Text = "search for song";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(367, 642);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(112, 34);
            btnAdd.TabIndex = 23;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDeleteSong
            // 
            btnDeleteSong.Location = new Point(367, 587);
            btnDeleteSong.Name = "btnDeleteSong";
            btnDeleteSong.Size = new Size(112, 34);
            btnDeleteSong.TabIndex = 25;
            btnDeleteSong.Text = "Delete";
            btnDeleteSong.UseVisualStyleBackColor = true;
            btnDeleteSong.Click += btnDeleteSong_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1009, 751);
            Controls.Add(btnDeleteSong);
            Controls.Add(btnAdd);
            Controls.Add(label2);
            Controls.Add(btnSearch);
            Controls.Add(textBox1);
            Controls.Add(songList);
            Controls.Add(btnLogout);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(txtUsername);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "MainForm";
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
        private TextBox textBox1;
        private Button btnSearch;
        private Label label2;
        private Button btnAdd;
        private Button btnDeleteSong;
    }
}