using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerSmallItem
{
    public class SqlHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        //用于数据库的insert，update
        public static int ExecuteNonQuery(string  sql,params  SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            { 
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        //用于数据库的select
        public static DataTable ExecuteDataTable(string sql, params  SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }
    }
}
