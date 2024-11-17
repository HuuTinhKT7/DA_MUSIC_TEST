namespace DA_MusicApp
{
    partial class signup
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
            btnSignin = new Button();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            btnSignup = new Button();
            txtEmail = new TextBox();
            label4 = new Label();
            txtRepassword = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // btnSignin
            // 
            btnSignin.Location = new Point(291, 435);
            btnSignin.Name = "btnSignin";
            btnSignin.Size = new Size(112, 34);
            btnSignin.TabIndex = 6;
            btnSignin.Text = "sign in";
            btnSignin.UseVisualStyleBackColor = true;
            btnSignin.Click += btnSignin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(249, 234);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(171, 31);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(249, 89);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(171, 31);
            txtUsername.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 240);
            label2.Name = "label2";
            label2.Size = new Size(89, 25);
            label2.TabIndex = 6;
            label2.Text = "password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 95);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 5;
            label1.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(47, 444);
            label3.Name = "label3";
            label3.Size = new Size(200, 25);
            label3.TabIndex = 11;
            label3.Text = "already have an accont?";
            // 
            // btnSignup
            // 
            btnSignup.Location = new Point(291, 377);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(112, 34);
            btnSignup.TabIndex = 5;
            btnSignup.Text = "sign up";
            btnSignup.UseVisualStyleBackColor = true;
            btnSignup.Click += btnSignup_Click;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(249, 163);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(171, 31);
            txtEmail.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 169);
            label4.Name = "label4";
            label4.Size = new Size(54, 25);
            label4.TabIndex = 12;
            label4.Text = "Email";
            // 
            // txtRepassword
            // 
            txtRepassword.Location = new Point(249, 314);
            txtRepassword.Name = "txtRepassword";
            txtRepassword.Size = new Size(171, 31);
            txtRepassword.TabIndex = 4;
            txtRepassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(36, 320);
            label5.Name = "label5";
            label5.Size = new Size(160, 25);
            label5.TabIndex = 14;
            label5.Text = "Re-enter password";
            // 
            // signup
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 531);
            Controls.Add(txtRepassword);
            Controls.Add(label5);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnSignup);
            Controls.Add(btnSignin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "signup";
            Text = "signup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSignin;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Label label2;
        private Label label1;
        private Label label3;
        private Button btnSignup;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtRepassword;
        private Label label5;
    }
}