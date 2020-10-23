using System;
using MySql.Data.MySqlClient;

namespace BlogPost
{
  public class AccountsDatabase : IDisposable
  {
    public MySqlConnection Connection;

    public AccountsDatabase(string connectionString)
    {
      Connection = new MySqlConnection(connectionString);
      this.Connection.Open();
    }

    public void Dispose()
    {
      Connection.Close();
    }
  }
}