    using System.Data.SqlClient;
    using System.Net.Sockets;
    using System.Text;

    namespace DA_MusicApp
    {
        public partial class signin : Form
        {
            string server = "127.0.0.1"; // Server IP
            int port = 12345; // Server port
            private PasswordHasher passwordHasher = new PasswordHasher();
            public signin()
            {
                InitializeComponent();
            }
        

            private void btnSignin_Click(object sender, EventArgs e)
            {
                try
                {
                    using (TcpClient client = new TcpClient(server, port))
                    {
                        NetworkStream stream = client.GetStream();

                        string username = txtUsername.Text;
                        string password = txtPassword.Text;

                        // Hash the password
                        var (hash, salt) = passwordHasher.HashPassword(password);
                        string hashedPassword = Convert.ToBase64String(hash);
                        string saltString = Convert.ToBase64String(salt);

                        string message = $"{username}:{password}:{hashedPassword}:{saltString}";
                        byte[] data = Encoding.UTF8.GetBytes(message);

                        stream.Write(data, 0, data.Length);
                        byte[] responseData = new byte[256];
                        int bytes = stream.Read(responseData, 0, responseData.Length);
                        string response = Encoding.UTF8.GetString(responseData, 0, bytes);

                        if (response == "SUCCESS")
                        {
                            MessageBox.Show("Login successful! Opening main form...");
                            byte[] responseData1 = new byte[256];
                            int bytes1 = stream.Read(responseData1, 0, responseData1.Length);
                            string email = Encoding.UTF8.GetString(responseData1, 0, bytes1);

                        // Open main form logic here
                        MainForm mainForm = new MainForm(username,email);
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Login failed. Please check your credentials.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
            }

            private void btnSignup_Click(object sender, EventArgs e)
            {
                signup signup = new signup();
                signup.Show();
                this.Hide();
            }
        }
    }
