/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2019-9-6
 * Time: 13:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace GameAward
{
    public class SQLDbHelper
    {
    	string DBName;

        public SQLDbHelper(string dbname)
        {
            DBName = dbname;
        }

        public void CloseSqlConnection(SqlConnection con, ref string msg)
        {
            try
            {
                if (con.State > ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            catch (SqlException exception)
            {
                msg = msg + "SQL错误：" + exception.Message + "\n";
            }
            catch (Exception exception2)
            {
                msg = msg + "异常：" + exception2.Message + "\n";
            }
        }

        public DataSet ExecuteDataSet(string sqlStr, SqlParameter[] sqlparams, ref string msg, int CommandTimeout = 0)
        {
            DataSet dataSet = null;
            using (SqlConnection connection = this.GetSqlConnection(ref msg))
            {
                connection.Open();
                try
                {
                    try
                    {
                        dataSet = new DataSet();
                        SqlCommand selectCommand = new SqlCommand(sqlStr, connection) {
                            CommandTimeout = CommandTimeout
                        };
                        if (sqlparams != null)
                        {
                            selectCommand.Parameters.AddRange(sqlparams);
                        }
                        new SqlDataAdapter(selectCommand).Fill(dataSet);
                    }
                    catch (SqlException exception)
                    {
                        msg = msg + "SQL执行错误：" + exception.Message + "\n";
                    }
                    catch (Exception exception2)
                    {
                        msg = msg + "异常：" + exception2.Message + "\n";
                    }
                    return dataSet;
                }
                finally
                {
                    connection.Close();
                }
            }
            return dataSet;
        }

        public DataTable ExecuteDataTable(string sqlStr, SqlParameter[] sqlparams, ref string msg, int CommandTimeout = 0)
        {
            DataTable table = null;
            using (SqlConnection connection = this.GetSqlConnection(ref msg))
            {
                connection.Open();
                try
                {
                    try
                    {
                        SqlCommand selectCommand = new SqlCommand(sqlStr, connection) {
                            CommandTimeout = CommandTimeout
                        };
                        if (sqlparams != null)
                        {
                            selectCommand.Parameters.AddRange(sqlparams);
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        if (sqlparams != null)
                        {
                            table = dataSet.Tables[0];
                        }
                    }
                    catch (SqlException exception)
                    {
                        msg = msg + "SQL执行错误：" + exception.Message + "\n";
                    }
                    catch (Exception exception2)
                    {
                        msg = msg + "异常：" + exception2.Message + "\n";
                    }
                    return table;
                }
                finally
                {
                    connection.Close();
                }
            }
            return table;
        }

        public int ExecuteNonQuery(string sqlStr, SqlParameter[] sqlparams, ref string msg, int CommandTimeout = 0)
        {
            int num = 0;
            using (SqlConnection connection = this.GetSqlConnection(ref msg))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(sqlStr, connection) {
                            Transaction = transaction,
                            CommandTimeout = CommandTimeout
                        };
                        command.Prepare();
                        if (sqlparams != null)
                        {
                            command.Parameters.AddRange(sqlparams);
                        }
                        num = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                        num = -1;
                        msg = msg + "SQL执行错误：" + exception.Message + "\n";
                    }
                    catch (Exception exception2)
                    {
                        transaction.Rollback();
                        num = -1;
                        msg = msg + "异常：" + exception2.Message + "\n";
                    }
                    return num;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public SqlDataReader ExecuteReader(string sqlStr, SqlParameter[] sqlparams, ref string msg, int CommandTimeout = 0)
        {
            SqlDataReader reader = null;
            using (SqlConnection connection = this.GetSqlConnection(ref msg))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(sqlStr, connection) {
                        CommandTimeout = CommandTimeout
                    };
                    if (sqlparams != null)
                    {
                        command.Parameters.AddRange(sqlparams);
                    }
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (SqlException exception)
                {
                    msg = msg + "SQL执行错误：" + exception.Message + "\n";
                }
                catch (Exception exception2)
                {
                    msg = msg + "异常：" + exception2.Message + "\n";
                }
            }
            return reader;
        }

        public object ExecuteScalar(string sqlStr, SqlParameter[] sqlparams, ref string msg, int CommandTimeout = 0)
        {
            object obj2 = null;
            using (SqlConnection connection = this.GetSqlConnection(ref msg))
            {
                connection.Open();
                try
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(sqlStr, connection) {
                            CommandTimeout = CommandTimeout
                        };
                        if (sqlparams != null)
                        {
                            command.Parameters.AddRange(sqlparams);
                        }
                        obj2 = command.ExecuteScalar();
                    }
                    catch (SqlException exception)
                    {
                        msg = msg + "SQL执行错误：" + exception.Message + "\n";
                    }
                    catch (Exception exception2)
                    {
                        msg = msg + "异常：" + exception2.Message + "\n";
                    }
                    return obj2;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return obj2;
        }

        public SqlConnection GetSqlConnection(ref string msg)
        {
            SqlConnection connection = null;
            try
            {
                string conStr = ConfigurationManager.AppSettings[DBName].ToString();
                connection = new SqlConnection(conStr);
            }
            catch (SqlException exception)
            {
                msg = msg + "SQL连接错误：" + exception.Message + "\n";
            }
            catch (Exception exception2)
            {
                msg = msg + "异常：" + exception2.Message + "\n";
            }
            return connection;
        }

        
    }
}
