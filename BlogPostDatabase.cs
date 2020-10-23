using System;
using MySql.Data.MySqlClient;

namespace BlogPost
{
  public class BlogPostDatabase : IDisposable
  {
    public MySqlConnection Connection;

    public BlogPostDatabase(string connectionString)
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