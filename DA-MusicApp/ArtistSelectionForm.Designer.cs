namespace DA_MusicApp
{
    partial class ArtistSelectionForm
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
            listBoxArtists = new ListBox();
            btnOK = new Button();
            SuspendLayout();
            // 
            // listBoxArtists
            // 
            listBoxArtists.FormattingEnabled = true;
            listBoxArtists.ItemHeight = 25;
            listBoxArtists.Location = new Point(80, 37);
            listBoxArtists.Name = "listBoxArtists";
            listBoxArtists.Size = new Size(351, 379);
            listBoxArtists.TabIndex = 0;
            listBoxArtists.SelectedIndexChanged += listBoxArtists_SelectedIndexChanged;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(453, 382);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 34);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // ArtistSelectionForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 450);
            Controls.Add(btnOK);
            Controls.Add(listBoxArtists);
            Name = "ArtistSelectionForm";
            Text = "ArtistSelectionForm";
            FormClosing += ArtistSelectionForm_FormClosing;
            Shown += ArtistSelectionForm_Shown;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxArtists;
        private Button btnOK;
    }
}