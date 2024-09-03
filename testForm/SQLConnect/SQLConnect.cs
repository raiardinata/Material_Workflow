using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace testForm.SQLConnect
{
    public class SQLConnect
    {
        private static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString; }
        }

        //private static String ConnectionString_AddOn
        //{
        //    get { return ConfigurationManager.ConnectionStrings["SCM_AddOn"].ConnectionString; }
        //}

        public DataTable ExecuteDataTable(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(storedProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                cn.Open();

            try
            {
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                        cmd.Parameters.Add(param);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public DataSet ExecuteDataSet(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataSet dt = new DataSet();
            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(storedProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                cn.Open();

            try
            {
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                        cmd.Parameters.Add(param);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public int ExecuteNonQuery(string storedProcedureName, params SqlParameter[] arrParam)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(storedProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                cn.Open();

            try
            {
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                        cmd.Parameters.Add(param);
                }
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public int ExecuteNonQuery3(params SqlParameter[] arrParam)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "msdb.dbo.sp_start_job";
            cmd.CommandTimeout = 0;
            cmd.Connection = cn;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                cn.Open();

            try
            {
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                        cmd.Parameters.Add(param);
                }
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public DataTable ExecuteDataTableCC(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(storedProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                cn.Open();

            try
            {
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                        cmd.Parameters.Add(param);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }

        public int ExecuteNonQueryCC(string storedProcedureName, params SqlParameter[] arrParam)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(storedProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                cn.Open();

            try
            {
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                        cmd.Parameters.Add(param);
                }
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
        }
    }
}