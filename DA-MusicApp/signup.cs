using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net.Sockets;

namespace DA_MusicApp
{
    public partial class signup : Form
    {
        string server = "127.0.0.1"; // Server IP
        int port = 12345; // Server port
        private PasswordHasher passwordHasher = new PasswordHasher();
        signin signin;
        public signup()
        {
            InitializeComponent();
        }
        public signup(signin signin)
        {
            InitializeComponent();
            this.signin = signin;
        }
                
        private bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
            return Regex.IsMatch(email, pattern);
        }
        private bool IsValid(string s)
        {
            if (s.Length >= 6 && s.Length <= 20)
            {
                return true; // hợp lệ
            }
            return false; //không hợp lệ
        }
        private bool check()
        {
            if (!IsValid(txtUsername.Text))
            {
                MessageBox.Show("Tài khoản phải từ 6 - 20 ký tự!");
                return false;
            }
            

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email Không hợp lệ!");
                return false;
            }
            
            if (!IsValid(txtPassword.Text))
            {
                MessageBox.Show("Mật khâu phải từ 6 - 20 ký tự!");
                return false;
            }
            if (txtPassword.Text != txtRepassword.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng!");
                return false;
            }

            return true;
        }


        private void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            if (check())
            {
                try
                {
                    using (TcpClient client = new TcpClient(server, port))
                    {
                        NetworkStream stream = client.GetStream();
                        string message = $"SIGNUP:{username}:{email}:{password}";
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        byte[] responseData = new byte[1024];
                        int bytes = stream.Read(responseData, 0, responseData.Length);
                        string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                        if (response == "SUCCESS")
                        {
                            byte[] tokenData = new byte[256];
                            stream.Read(tokenData, 0, tokenData.Length); 
                            string token = Encoding.UTF8.GetString(tokenData, 0, tokenData.Length); // Lưu token vào tệp
                            File.WriteAllText("token.txt", token);
                            MessageBox.Show("Signup successful! Opening main form...");
                            MainForm mainForm = new MainForm(username,email,signin,0);
                            mainForm.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Signup failed. Username or email already exists.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
            }
        }

            private void btnSignin_Click(object sender, EventArgs e)
        {
            signin signin = new signin();
            signin.Show();
            this.Close();
        }
    }
}
