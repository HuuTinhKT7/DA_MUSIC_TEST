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

namespace DA_MusicApp
{
    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
        }
        string connectionString = "Server=DESKTOP-F755DK5\\SQLEXPRESS;Database=MusicDB;User Id=myuser;Password=710710710;";
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
        private bool check(string connectionString)
        {
            if (!IsValid(txtUsername.Text))
            {
                MessageBox.Show("Tài khoản phải từ 6 - 20 ký tự!");
                return false;
            }
            if (CheckUsernameExists(connectionString, txtUsername.Text))
            {
                MessageBox.Show("tài khoản đã tồn tại!");
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email Không hợp lệ!");
                return false;
            }
            if (CheckEmailExists(connectionString, txtEmail.Text))
            {
                MessageBox.Show("Email đã tồn tại!");
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

        private bool CheckUsernameExists(string connectionString, string username)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Nếu count > 0, username đã tồn tại
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("error " + ex);
                    return false; // Xử lý lỗi
                }
            }
        }
        private bool CheckEmailExists(string connectionString, string email)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Nếu count > 0, username đã tồn tại
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("error " + ex);
                    return false; // Xử lý lỗi
                }
            }
        }

        private void AddUserToDatabase(string connectionString, string username, string email, string password)
        {
            string query = "INSERT INTO Users (Username, Email, Password_Hash,Password_salt) VALUES (@Username, @Email, @Password_hash,@Password_salt)";
            var passwordHasher = new PasswordHasher();
            (byte[] hash, byte[] salt) = passwordHasher.HashPassword(password);
            string hashString = Convert.ToBase64String(hash);
            string saltString = Convert.ToBase64String(salt);

            using (var connection = new SqlConnection(connectionString)) // Sử dụng SqlConnection
            {
                password = HashPassword(password);
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@password_Hash", hashString);
                command.Parameters.AddWithValue("@password_Salt", saltString);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        public int? GetUserIdByUsername(string username)
        {
            int? userId = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT userID FROM users WHERE username = @username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        userId = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (log hoặc ném lại ngoại lệ)
                    MessageBox.Show("Error: " + ex);
                }
            }

            return userId;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            if (check(connectionString))
            {
                AddUserToDatabase(connectionString, username, email, password);
                //MainForm main = new MainForm(GetUserIdByUsername(username));

                //main.Show();
                this.Close();
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
