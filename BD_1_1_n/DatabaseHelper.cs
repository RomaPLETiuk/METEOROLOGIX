using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_1_1_n
{
    public class DatabaseHelper
    {
        private string connectionString;
        private NpgsqlConnection connection;

        public DatabaseHelper(string host, int port, string username, string password, string database)
        {
            connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database};";
            connection = new NpgsqlConnection(connectionString); // Ініціалізація з'єднання
        }

        public NpgsqlConnection GetConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}

