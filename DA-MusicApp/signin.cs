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
                        byte[] sizeData = new byte[4];
                        stream.Read(sizeData, 0, sizeData.Length);
                        int emailSize = BitConverter.ToInt32(sizeData, 0);
                        byte[] emailData = new byte[emailSize];
                        stream.Read(emailData, 0, emailData.Length);
                        string email = Encoding.UTF8.GetString(emailData, 0, emailSize);
                        stream.Read(sizeData, 0, sizeData.Length);
                        int tokenSize = BitConverter.ToInt32(sizeData, 0);
                        byte[] tokenData = new byte[tokenSize];
                        stream.Read(tokenData, 0, tokenData.Length);
                        string token = Encoding.UTF8.GetString(tokenData, 0, tokenSize);
                        MessageBox.Show(token);
                        File.WriteAllText("token.txt", token);
                        // Open main form logic here
                        MainForm mainForm = new MainForm(username, email, this);
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
            signup signup = new signup(this);
            signup.Show();
            this.Hide();
        }

        private void signin_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
