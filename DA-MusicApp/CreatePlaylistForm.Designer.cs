namespace DA_MusicApp
{
    partial class CreatePlaylistForm
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
            txtPlaylistName = new TextBox();
            btnOK = new Button();
            SuspendLayout();
            // 
            // txtPlaylistName
            // 
            txtPlaylistName.Location = new Point(50, 36);
            txtPlaylistName.Name = "txtPlaylistName";
            txtPlaylistName.Size = new Size(281, 31);
            txtPlaylistName.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(127, 83);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 34);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // CreatePlaylistForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 129);
            Controls.Add(btnOK);
            Controls.Add(txtPlaylistName);
            Name = "CreatePlaylistForm";
            Text = "CreatePlaylistForm";
            FormClosing += CreatePlaylistForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPlaylistName;
        private Button btnOK;
    }
}