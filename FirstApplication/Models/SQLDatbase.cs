    using MySql.Data.MySqlClient;
    using System;
    using System.Data;


    namespace FirstApplication.Models
    {
        public class SQLDatabase
        {
            public static string ConnectionString = "Server=localhost;Database=test1;Uid=root;Pwd=;";
            public static int ExecNonQuery(string Command)
            {
                MySqlConnection con = new MySqlConnection(ConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = Command;
                cmd.CommandType = CommandType.Text;
                try
                {
                    con.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        return 1;
                    }
                    else
                    {
                        con.Close();
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    con.Close();
                    throw ex;
                }

            }
            public static DataTable GetDataTable(string Query)
            {
                MySqlConnection con = new MySqlConnection(ConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = Query;
                cmd.CommandType = CommandType.Text;

                MySqlDataAdapter da = new MySqlDataAdapter();

                DataTable tab = new DataTable();

                da.SelectCommand = cmd;
                try
                {
                    da.Fill(tab);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return tab;
            }

        }
    }