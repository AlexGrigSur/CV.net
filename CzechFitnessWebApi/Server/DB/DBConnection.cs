using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;

namespace IPISserver.DataBase.Connection
{
    public class DB
    {
        private MySqlConnection connection;
        private MySqlCommand command = new MySqlCommand();
        static protected DB instance = null;
        static private string connectString = "server=db;port=3306;username=root;password=146923785;Allow User Variables=True";
        private DB()
        {
            connection = new MySqlConnection(connectString);
            command = new MySqlCommand();
        }
        private void OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
            }
            catch
            {
                Console.WriteLine("Oops, something went wrong");
            }
        }
        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        /// <summary>
        /// Singelton method that provide database connection
        /// </summary>
        /// <returns>DB instance</returns>
        public static DB GetInstance()
        {
            if (instance == null)
            {
                instance = new DB();
                return instance;
            }
            else
                return instance;
        }
        /// <summary>
        /// Execute SQL command
        /// </summary>
        /// <param name="SQLcommand">sql command</param>
        /// <param name="paramsColl">parameters list</param>
        public string ExecuteCommand(string SQLcommand, List<MySqlParameter> paramsColl = null)
        {
            command.CommandText = SQLcommand;
            command.Connection = connection;
            command.Parameters.Clear();
            if (paramsColl != null)
                foreach (MySqlParameter i in paramsColl)
                    command.Parameters.Add(i);

            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();
            return command.LastInsertedId.ToString();
        }
        /// <summary>
        /// Execute SQL DataReader
        /// </summary>
        /// <param name="SQLcommand">sql command</param>
        /// <param name="paramsColl">parameters list</param>
        /// <returns></returns>
        public List<string[]> ExecuteReader(string SQLcommand, List<MySqlParameter> paramsColl = null)
        {
            command.CommandText = SQLcommand;
            command.Connection = connection;
            command.Parameters.Clear();

            if (paramsColl != null)
                foreach (MySqlParameter i in paramsColl)
                    command.Parameters.Add(i);


            OpenConnection();

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> result = new List<string[]>();

            while (reader.Read())
            {
                result.Add(new string[reader.FieldCount]);
                for (int j = 0; j < reader.FieldCount; ++j)
                    result.Last()[j] = reader[j].ToString();
            }
            reader.Close();
            CloseConnection();

            return result;
        }
    }
}
