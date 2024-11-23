namespace DA_MusicApp
{
    partial class ShareRequestsForm
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
            listBoxRequests = new ListBox();
            SuspendLayout();
            // 
            // listBoxRequests
            // 
            listBoxRequests.FormattingEnabled = true;
            listBoxRequests.ItemHeight = 25;
            listBoxRequests.Location = new Point(36, 48);
            listBoxRequests.Name = "listBoxRequests";
            listBoxRequests.Size = new Size(314, 354);
            listBoxRequests.TabIndex = 0;
            listBoxRequests.SelectedIndexChanged += listBoxRequests_SelectedIndexChanged;
            // 
            // ShareRequestsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(442, 450);
            Controls.Add(listBoxRequests);
            Name = "ShareRequestsForm";
            Text = "ShareRequestsForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxRequests;
    }
}