namespace DA_MusicServerApp
{
    partial class MusicServerApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAdd = new Button();
            musiclist = new ListBox();
            label1 = new Label();
            btnDelete = new Button();
            btnEdit = new Button();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(23, 102);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(124, 83);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add song";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // musiclist
            // 
            musiclist.FormattingEnabled = true;
            musiclist.ItemHeight = 25;
            musiclist.Location = new Point(274, 102);
            musiclist.Name = "musiclist";
            musiclist.Size = new Size(426, 604);
            musiclist.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(274, 49);
            label1.Name = "label1";
            label1.Size = new Size(89, 25);
            label1.TabIndex = 2;
            label1.Text = "Music List";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(23, 283);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(124, 83);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete Song";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(23, 484);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(124, 83);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "update Song";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // MusicServerApp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 756);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(label1);
            Controls.Add(musiclist);
            Controls.Add(btnAdd);
            Name = "MusicServerApp";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAdd;
        private ListBox musiclist;
        private Label label1;
        private Button btnDelete;
        private Button btnEdit;
    }
}
