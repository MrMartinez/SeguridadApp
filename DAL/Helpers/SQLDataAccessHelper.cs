using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL.Helpers
{
    public class SQLDataAccessHelper
    {
        
        string connectionString = string.Empty;
        public SQLDataAccessHelper()
        {
            try
            {
                connectionString = Singleton.Instance._connection;
                //connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\MrMartinez\Documents\Developer\SeguridadApp\SeguridadAppBD.accdb";

            }
            catch (Exception)
            {

                throw;
            }
        }


        //Para el select
        public DataTable executeQuery(string commandText, CommandType commandType, params OleDbParameter[] parameters)
        {
            using (var conn = new OleDbConnection(connectionString))
            using (var cmd = new OleDbCommand(commandText, conn))
            {
                DataTable dt = new DataTable();
                cmd.CommandType = commandType;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
                return dt;
            }

        }

        public bool executeNonQuery(string commandText, CommandType commandType, params OleDbParameter[] parameters)
        {
            bool b = false;
            using (var conn = new OleDbConnection(connectionString))
            using (var cmd = new OleDbCommand(commandText, conn))
            {
                conn.Open();
                cmd.CommandText = commandText;
                cmd.Parameters.AddRange(parameters);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
            }
            return b;

        }
           

    }
}