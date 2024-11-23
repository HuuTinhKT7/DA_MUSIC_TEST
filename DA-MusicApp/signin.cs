    using System.Data.SqlClient;
    using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

    namespace DA_MusicApp
    {
    public partial class signin : Form
    {
        public string server = "localhost"; // Server IP
        public int port = 12345; // Server port
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
                        File.WriteAllText("token.txt", token);
                        MainForm mainForm = new MainForm(username, email, this,0);
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
            if (File.Exists("token.txt"))
            {
                string token = File.ReadAllText("token.txt"); // Gửi token đến server để xác thực
                if (ValidateToken(token, out string username, out string email))
                {
                    MainForm mainForm = new MainForm(username,email,this,1);
                    mainForm.Show();
                }
            }
        }
        bool ValidateToken(string token, out string username, out string email)
        {
            username = "";
            email = "";
            try
            {
                using (TcpClient client = new TcpClient(server, 12345))
                {
                    NetworkStream stream = client.GetStream();
                    string message = $"VALIDATE_TOKEN:{token}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);
                    if (response == "VALID")
                    {
                        byte[] userData = new byte[256];
                        int userBytes = stream.Read(userData, 0, userData.Length);
                        string userInfo = Encoding.UTF8.GetString(userData, 0, userBytes);
                        string[] parts = userInfo.Split(':');
                        username = parts[0]; email = parts[1];
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return false;
        }


    }
}
