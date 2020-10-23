using System;
using MySqlConnector;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace BlogPost.Models
{
    public class UsersDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public UsersDb(string conn)
        {
            Connection = new MySqlConnection(conn);
        }

        public void Dispose() => Connection.Dispose();
    }

    
    
}