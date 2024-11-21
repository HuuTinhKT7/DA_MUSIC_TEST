using MusicServer;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using NAudio.Wave;
using System.Reflection.PortableExecutable;
using static System.Collections.Specialized.BitVector32;
using System.Data;

class Server
{
    private static PasswordHasher passwordHasher = new PasswordHasher();
    private static string connectionString = "Server=DESKTOP-F755DK5\\SQLEXPRESS;Database=MusicDB;User Id=myuser;Password=710710710;";
    public static string email;
    private static bool CheckUsernameExists(string connectionString, string username)
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
                return count > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
    private static bool CheckUsernameOrEmailExists(string connectionString, string username, string email)
    {
        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
    static void Main(string[] args)
    {

        TcpListener server = null;
        try
        {
            int port = 12345;
            server = new TcpListener(IPAddress.Any, port); server.Start();
            Console.WriteLine("Server started...");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            if (server != null) server.Stop();
        }
    }
    private static void HandleClient(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytes = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytes);

            if (message == "GET_SONG_LIST")
            {
                string songList = GetSongList();
                byte[] songData = Encoding.UTF8.GetBytes(songList);
                stream.Write(songData, 0, songData.Length);
            }

            else if (message == "ADDSONG")
            {
                byte[] buffer1 = new byte[10 * 1024 * 1024];
                int bytes1 = stream.Read(buffer1, 0, buffer1.Length);
                string message1 = Encoding.UTF8.GetString(buffer1, 0, bytes1);
                string[] parts1 = message1.Split(':');
                string songName = parts1[1];
                string artist = parts1[2];
                byte[] songFile = Convert.FromBase64String(parts1[3]);
                AddSong(songName, artist, songFile);
                byte[] response = Encoding.UTF8.GetBytes("SUCCESS");
                stream.Write(response, 0, response.Length);
                Console.WriteLine("Song added: " + songName);

            }

            else
            {
                string[] parts = message.Split(':');
                string username = parts[0];
                string password = parts[1];
                if (parts.Length == 2 && parts[0] == "GET_SONG")
                {
                    string songName = parts[1];
                    byte[] songFile = GetSongFile(songName);
                    if (songFile != null)
                    {
                        stream.Write(songFile, 0, songFile.Length);
                        Console.WriteLine("Sent song: " + songName);
                    }
                    else
                    {
                        byte[] response = Encoding.UTF8.GetBytes("SONG_NOT_FOUND");
                        stream.Write(response, 0, response.Length); Console.WriteLine("Song not found: " + songName);
                    }
                }
                if (parts.Length == 2)
                {
                    if (parts[0] == "LOGOUT")
                    {
                        bool success = LogoutUser(parts[1]);
                        string response = success ? "SUCCESS" : "FAILED";
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length);
                    }
                    if (parts[0] == "VALIDATE_TOKEN")
                    {
                        string token = parts[1];
                        if (ValidateToken(token, out username, out string email))
                        {
                            byte[] response = Encoding.UTF8.GetBytes("VALID");
                            stream.Write(response, 0, response.Length);
                            string userInfo = $"{username}:{email}";
                            byte[] userData = Encoding.UTF8.GetBytes(userInfo);
                            stream.Write(userData, 0, userData.Length);
                        }
                        else
                        {
                            byte[] response = Encoding.UTF8.GetBytes("INVALID");
                            stream.Write(response, 0, response.Length);
                        }
                    }
                    if (parts[0] == "GET_PLAYLISTS")
                    {
                        string playlists = GetUserPlaylists(parts[1]);
                        byte[] playlistData = Encoding.UTF8.GetBytes(playlists);
                        stream.Write(playlistData, 0, playlistData.Length);
                    }
                }
                else if (parts.Length == 3 )
                {
                    if (parts[0] == "GET_SONGS_BY_PLAYLIST") {
                        string playlistName = parts[2];
                        string songs = GetSongsByPlaylist(parts[1], playlistName);
                        byte[] songData = Encoding.UTF8.GetBytes(songs);
                        byte[] songSize = BitConverter.GetBytes(songData.Length);
                        stream.Write(songSize, 0, songSize.Length);
                        stream.Write(songData, 0, songData.Length);
                    }
                    if (parts[0] == "CREATE_PLAYLIST")
                    {
                        Console.WriteLine("aa");
                        bool success = CreatePlaylist(parts[1], parts[2]); 
                        string response = success ? "SUCCESS" : "FAIL";
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length);
                    }
                    if (parts[0] == "DELETE_PLAYLIST") { 
                        bool success = DeletePlaylist(parts[1], parts[2]); 
                        string response = success ? "SUCCESS" : "FAIL"; 
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length); 
                    }
                    if (parts[0] == "DELETE_SONG")
                    {
                        bool success = DeleteSong(parts[1], parts[2]);
                        string response = success ? "SUCCESS" : "FAILED";
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length);
                    }
                }
                else
                {
                    string hashedPassword = parts[2];
                    string saltString = parts[3];
                    if (parts[0] == "SIGNUP")
                    {
                        if (CheckUsernameOrEmailExists(connectionString, parts[1], parts[2]))
                        {
                            byte[] response = Encoding.UTF8.GetBytes("FAIL");
                            stream.Write(response, 0, response.Length);
                            Console.WriteLine("Signup failed: " + parts[1]);
                        }
                        else
                        {
                            var (passwordHash1, salt1) = passwordHasher.HashPassword(parts[3]);
                            string hashedPassword1 = Convert.ToBase64String(passwordHash1);
                            string saltString1 = Convert.ToBase64String(salt1);
                            AddUser(parts[1], parts[2], hashedPassword1, saltString1);
                            string token = Guid.NewGuid().ToString();
                            bool tokenUpdated = UpdateUserToken(parts[1], token);
                            if (tokenUpdated)
                            {
                                byte[] response = Encoding.UTF8.GetBytes("SUCCESS");
                                stream.Write(response, 0, response.Length);
                                byte[] tokenData = Encoding.UTF8.GetBytes(token);
                                stream.Write(tokenData, 0, tokenData.Length);
                                Console.WriteLine("User registered: " + username);
                            }
                        }
                    }
                    else if (parts[0] == "ADD_SONG_TO_PLAYLIST") {
                        bool success = AddSongToPlaylist(parts[1], parts[2], parts[3]);
                        string response = success ? "SUCCESS" : "FAIL"; 
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length); 
                    }
                    else
                    {
                        if (AuthenticateUser(username, password, hashedPassword, saltString))
                        {
                            string token = Guid.NewGuid().ToString();
                            bool tokenUpdated = UpdateUserToken(username, token);
                            if (tokenUpdated)
                            {
                                Console.WriteLine("User authenticated: " + username);
                                byte[] response = Encoding.UTF8.GetBytes("SUCCESS");
                                stream.Write(response, 0, response.Length);
                                byte[] emailData = Encoding.UTF8.GetBytes(email);
                                byte[] emailSize = BitConverter.GetBytes(emailData.Length);
                                stream.Write(emailSize, 0, emailSize.Length);
                                stream.Write(emailData, 0, emailData.Length);
                                byte[] tokenData = Encoding.UTF8.GetBytes(token);
                                byte[] tokenSize = BitConverter.GetBytes(tokenData.Length);
                                stream.Write(tokenSize, 0, tokenSize.Length);
                                stream.Write(tokenData, 0, tokenData.Length);
                            }
                            else
                            {
                                byte[] response = Encoding.UTF8.GetBytes("FAIL");
                                stream.Write(response, 0, response.Length);
                                Console.WriteLine("Failed to update token: " + username);
                            }
                        }
                        else
                        {
                            byte[] response = Encoding.UTF8.GetBytes("FAIL");
                            stream.Write(response, 0, response.Length);
                            Console.WriteLine("Authentication failed: " + username);
                        }
                    }
                }
            }
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            client.Close();
        }
    }

    private static bool AuthenticateUser(string username, string password, string hashedPassword, string saltString)
    {

        if (CheckUsernameExists(connectionString, username))
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT email, password_hash, password_salt FROM Users WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHashString = reader["password_hash"].ToString();
                            string storedSaltString = reader["password_salt"].ToString();
                            email = reader["email"].ToString();
                            Console.WriteLine(email);


                            byte[] storedHash = Convert.FromBase64String(storedHashString);
                            byte[] storedSalt = Convert.FromBase64String(storedSaltString);

                            if (passwordHasher.VerifyPassword(password, storedHash, storedSalt))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            return false;
        }
    }

    private static string GetSongList()
    {
        string query = "SELECT songname, artistname FROM Songs";
        List<string> songs = new List<string>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read() )
                {
                    string songName = reader["songname"].ToString();
                    string artist = reader["artistname"].ToString();
                    songs.Add($"{songName}:{artist}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return string.Join(",", songs);
    }

    private static void AddUser(string username, string email, string passwordHash, string salt)
    {
        string query = "INSERT INTO Users (Username, Email, Password_Hash, Password_Salt) VALUES (@Username, @Email, @PasswordHash, @Salt)";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);
            command.Parameters.AddWithValue("@Salt", salt);
            try
            {
                connection.Open(); command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    private static byte[] GetSongFile(string songName)
    {
        string query = "SELECT song_file FROM Songs WHERE songname = @songName";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@songName", songName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return (byte[])reader["song_file"];
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return null;
    }
    private static void AddSong(string songName, string artist, byte[] songFile)
    {
        string query = "INSERT INTO Songs (songname, artistname, song_file) VALUES (@songName, @artist, @songFile)";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@songName", songName);
            command.Parameters.AddWithValue("@artist", artist);
            command.Parameters.AddWithValue("@songFile", songFile);
            try
            {
                connection.Open(); command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private static bool DeleteSong(string songName,string artist)
    {
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand("DeleteSong", connection) { 
                CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@songName", songName);
            command.Parameters.AddWithValue("@artistName", artist);
            try {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex) { 
                Console.WriteLine(ex.Message); 
                return false; 
            }
        }
    }

    private static bool UpdateUserToken(string username, string token)
    {
        string query = "UPDATE Users SET token = @token WHERE username = @username";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@token", token);
            command.Parameters.AddWithValue("@username", username);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message); return false;
            }
        }
    }
    private static bool LogoutUser(string username)
    {
        string query = "UPDATE Users SET token = NULL WHERE username = @username";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message); return false;
            }
        }
    }
    private static bool ValidateToken(string token, out string username, out string email)
    {
        username = "";
        email = ""; string query = "SELECT username, email FROM Users WHERE token = @token";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@token", token);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    username = reader["username"].ToString();
                    email = reader["email"].ToString();
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

    private static string GetUserPlaylists(string username) {
        string query = "SELECT Name FROM Playlists WHERE UserId = (SELECT UserId FROM Users WHERE Username = @username)";
        List<string> playlists = new List<string>();
        using (SqlConnection connection = new SqlConnection(connectionString)) { 
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    string playlistName = reader["Name"].ToString();
                    if (!string.IsNullOrWhiteSpace(playlistName)) {
                        playlists.Add(playlistName); 
                    }
                }
            }
            catch (SqlException ex) { 
                Console.WriteLine(ex.Message); 
            }
        }
        return string.Join(",", playlists); 
    }

    private static string GetSongsByPlaylist(string username, string playlistName) { 
        string query = "SELECT SongName, Artist FROM PlaylistSongs WHERE PlaylistId = (SELECT PlaylistId FROM Playlists WHERE Name = @playlistName AND UserId = (SELECT UserId FROM Users WHERE Username = @username))";
        List<string> songs = new List<string>();
        using (SqlConnection connection = new SqlConnection(connectionString)) { 
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@playlistName", playlistName);
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(); 
                while (reader.Read()) {
                    string songName = reader["SongName"].ToString();
                    string artist = reader["Artist"].ToString();
                    if (!string.IsNullOrWhiteSpace(songName) && !string.IsNullOrWhiteSpace(artist)) {
                        songs.Add($"{songName}:{artist}"); 
                    }
                }
            }
            catch (SqlException ex) {
                Console.WriteLine(ex.Message); 
            }
        }
        return string.Join(",", songs); 
    }

    private static bool CreatePlaylist(string username, string playlistName) { 
        string query = "INSERT INTO Playlists (UserId, Name) VALUES ((SELECT UserId FROM Users WHERE Username = @username), @playlistName)";
        using (SqlConnection connection = new SqlConnection(connectionString)) { 
            SqlCommand command = new SqlCommand(query, connection); 
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@playlistName", playlistName);
            try {
                connection.Open(); int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
            catch (SqlException ex) {
                Console.WriteLine(ex.Message); return false; 
            }
        }
    }

    private static bool DeletePlaylist(string username, string playlistName) { 
        string query = "DELETE FROM Playlists WHERE Name = @playlistName AND UserId = (SELECT UserId FROM Users WHERE Username = @username)";
        using (SqlConnection connection = new SqlConnection(connectionString)) { 
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username); 
            command.Parameters.AddWithValue("@playlistName", playlistName);
            try {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
            catch (SqlException ex) {
                Console.WriteLine(ex.Message); return false; 
            }
        }
    }

    private static bool AddSongToPlaylist(string username, string playlistName, string songName) { 
        string query = "INSERT INTO PlaylistSongs (PlaylistId, SongName, Artist) VALUES ((SELECT PlaylistId FROM Playlists WHERE Name = @playlistName AND UserId = (SELECT UserId FROM Users WHERE Username = @username)), @songName, (SELECT artistname FROM Songs WHERE songname = @songName))";
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@playlistName", playlistName);
            command.Parameters.AddWithValue("@songName", songName); 
            try {
                connection.Open(); 
                int rowsAffected = command.ExecuteNonQuery(); 
                return rowsAffected > 0; 
            }
            catch (SqlException ex) {
                Console.WriteLine(ex.Message); return false; 
            }
        }
    }   
}
