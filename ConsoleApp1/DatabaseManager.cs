using System;
using MySql.Data.MySqlClient;

namespace ConsoleApp1.DatabaseManager;
public class DatabaseManager
{
    string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";

    public bool ValidateLogin(string username, string password)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM users WHERE username = @username AND password = @password";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                return reader.Read();
            }
        }
    }
}