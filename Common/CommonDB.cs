using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PayuTest.Common
{
    public class CommonDB
    {
        public DataTable ExecuteQuery(string ConnectionString, string spName, SqlParameter[] param)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandTimeout = 10000;
                cmd.CommandText = spName;
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        cmd.Parameters.Add(param[i]);
                    }
                }
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                conn.Close();
            }

            return dt;
        }

        public DataSet ExecuteMultipleQuery(string ConnectionString, string spName, SqlParameter[] param)
        {
            DataSet dt = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(spName, conn);
                    cmd.CommandTimeout = 10000;
                    cmd.CommandText = spName;
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            cmd.Parameters.Add(param[i]);
                        }
                    }
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return dt;
        }

    }
}