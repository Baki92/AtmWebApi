using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using AtmWebApi.Interfaces;

namespace AtmWebApi.DB
{
    public class Dal : IDal
    {
        private static readonly string connectionString = "Data Source="+ Environment.CurrentDirectory + "\\SQLiteDB\\AtmDB.db";

        public T get<T>(string sql, object param) where T : class
        {
            T result;
            using (var connection = new SqliteConnection(Dal.connectionString))
            {
                result = connection.Query<T>(sql, param).FirstOrDefault<T>();
            }
            return result;
        }
        public bool insert<T>(string sql, object param) where T : class
        {
            using (var connection = new SqliteConnection(Dal.connectionString))
            {
                connection.Execute(sql, param);
            }
            return true;
        }
        public bool update<T>(string sql, object param) where T : class
        {
            using (var connection = new SqliteConnection(Dal.connectionString))
            {
                connection.Execute(sql, param);
            }
            return true;
        }
        public bool delete(string sql, object param)
        {
            using (var connection = new SqliteConnection(Dal.connectionString))
            {
                connection.Execute(sql, param);
            }
            return true;
        }
    }
}
