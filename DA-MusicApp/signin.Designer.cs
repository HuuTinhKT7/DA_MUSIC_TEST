namespace DA_MusicApp
{
    partial class signin
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
            label1 = new Label();
            label2 = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnSignin = new Button();
            btnSignup = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(63, 94);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 198);
            label2.Name = "label2";
            label2.Size = new Size(89, 25);
            label2.TabIndex = 1;
            label2.Text = "password";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(198, 91);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(154, 31);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(198, 192);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(154, 31);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnSignin
            // 
            btnSignin.Location = new Point(224, 278);
            btnSignin.Name = "btnSignin";
            btnSignin.Size = new Size(112, 34);
            btnSignin.TabIndex = 3;
            btnSignin.Text = "sign in";
            btnSignin.UseVisualStyleBackColor = true;
            btnSignin.Click += btnSignin_Click;
            // 
            // btnSignup
            // 
            btnSignup.Location = new Point(224, 341);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(112, 34);
            btnSignup.TabIndex = 4;
            btnSignup.Text = "sign up";
            btnSignup.UseVisualStyleBackColor = true;
            btnSignup.Click += btnSignup_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 346);
            label3.Name = "label3";
            label3.Size = new Size(195, 25);
            label3.TabIndex = 6;
            label3.Text = "don't have an account?";
            // 
            // signin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(447, 423);
            Controls.Add(label3);
            Controls.Add(btnSignup);
            Controls.Add(btnSignin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "signin";
            Text = "sign in";
            Load += signin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnSignin;
        private Button btnSignup;
        private Label label3;
    }
}
