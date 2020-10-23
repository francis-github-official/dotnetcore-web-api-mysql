using System;
using MySql;
using MySqlConnector;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BlogPost.Models
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int Contact { get; set; }

        internal AppDb Db { get; set; }

        public UsersModel()

        {
        }

        internal UsersModel(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Users` (`name`, `address`,'contanct') VALUES (@name, @address, @contact);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Users` SET `name` = @name, `address` = @address, 'contact'= @contact WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Users` WHERE `id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@address",
                DbType = DbType.String,
                Value = Address,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@contact",
                DbType = DbType.Int32,
                Value = Contact,
            });

        }
    }
}