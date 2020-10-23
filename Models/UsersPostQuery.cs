using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace BlogPost.Models
{
    public class UsersPostQuery
    {
        public AppDb Db { get; }

        public UsersPostQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<UsersModel> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `name`, `address`, `contact` FROM `laravel.accounts` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<UsersModel>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `name`, `address`,`contact` FROM `laravel.accounts` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `accounts`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<UsersModel>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<UsersModel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new UsersModel(Db)
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Contact = reader.GetInt32(3)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}