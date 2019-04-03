using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Web;

namespace SeguridadApp.Helpers
{
    public class SQLDataAccessHelper
    {
        string connectionString = string.Empty;
        public SQLDataAccessHelper()
        {
            try
            {
                connectionString = "Data Source = .; Initial Catalog = SeguridadDpto; Integrated Security = true";

            }
            catch (Exception)
            {

                throw;
            }
        }


        //Para el select
        public DataTable executeQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(commandText, conn))
            {
                DataTable dt = new DataTable();
                cmd.CommandType = commandType;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
                return dt;
            }

        }

        public bool executeNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            bool b = false;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(commandText, conn))
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