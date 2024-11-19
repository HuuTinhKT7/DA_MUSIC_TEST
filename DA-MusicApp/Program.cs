using System.Net.Sockets;
using System.Text;

namespace DA_MusicApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            
            signin signin = new signin();
            if (File.Exists("token.txt"))
            {
                string token = File.ReadAllText("token.txt"); // Gửi token đến server để xác thực
                if (ValidateToken(token, out string username, out string email))
                {
                    Application.Run(new MainForm(username, email, signin));
                    return;
                }
            }
            Application.Run(new signin());

            static bool ValidateToken(string token, out string username, out string email)
            {
                username = "";
                email = "";
                try
                {
                    using (TcpClient client = new TcpClient("127.0.0.1", 12345))
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
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new signin());
        }
    }
}