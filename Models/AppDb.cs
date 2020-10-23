using System;
using MySqlConnector;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace BlogPost.Models
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}