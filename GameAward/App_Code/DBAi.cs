using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

public class DBAi
{
    public static string strConnection = ConfigurationManager.AppSettings["ConnString"];

    public static int ExeSqlCommand(string sqlCommand)
    {
        SqlConnection connection = new SqlConnection(getstrConnection());
        SqlCommand command = new SqlCommand(sqlCommand, connection);
        int num = -1;
        try
        {
            connection.Open();
        }
        catch
        {
            return -1;
        }
        try
        {
            num = command.ExecuteNonQuery();
        }
        catch (Exception)
        {
        }
        connection.Close();
        return num;
    }

    public static int ExeSqlCommand(string sqlCommand, SqlParameter[] prams)
    {
        int num2;
        using (SqlConnection connection = new SqlConnection(getstrConnection()))
        {
            using (SqlCommand command = SqlDBA.CreateCommandSql(connection, sqlCommand, prams))
            {
                int num = -1;
                try
                {
                    connection.Open();
                }
                catch
                {
                    return -1;
                }
                try
                {
                    num = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
                command.Dispose();
                connection.Close();
                connection.Dispose();
                num2 = num;
            }
        }
        return num2;
    }

    public static int ExeSqlCommand(string sqlCommand, ref Exception exception)
    {
        SqlConnection connection = new SqlConnection(getstrConnection());
        SqlCommand command = new SqlCommand(sqlCommand, connection);
        try
        {
            connection.Open();
        }
        catch (Exception exception2)
        {
            exception = exception2;
            return -1;
        }
        connection.Close();
        return command.ExecuteNonQuery();
    }

    public static bool ExeSqlCommands(string[] sqlCommands)
    {
        if (sqlCommands.Length < 1)
        {
            return false;
        }
        SqlConnection connection = new SqlConnection(getstrConnection());
        try
        {
            connection.Open();
        }
        catch
        {
            return false;
        }
        int index = 0;
        using (SqlTransaction transaction = connection.BeginTransaction())
        {
            while (index < sqlCommands.Length)
            {
                SqlCommand command = new SqlCommand(sqlCommands[index], connection) {
                    Transaction = transaction
                };
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
                index++;
            }
            if (index >= sqlCommands.Length)
            {
                transaction.Commit();
            }
        }
        if (connection.State != ConnectionState.Closed)
        {
            connection.Close();
        }
        return true;
    }

    public static int ExeSqlCommands(string[] sqlCommands, bool mustExist)
    {
        int num = 0;
        if (sqlCommands.Length >= 1)
        {
            SqlConnection connection = new SqlConnection(getstrConnection());
            try
            {
                connection.Open();
            }
            catch
            {
                return -1;
            }
            int index = 0;
            int num3 = 0;
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                while (index < sqlCommands.Length)
                {
                    SqlCommand command = new SqlCommand(sqlCommands[index], connection) {
                        Transaction = transaction
                    };
                    try
                    {
                        num3 = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        num3 = -1;
                    }
                    if ((num3 < 0) || (mustExist && (num3 == 0)))
                    {
                        num = -2;
                        transaction.Rollback();
                        break;
                    }
                    num += num3;
                    index++;
                }
                if (index >= sqlCommands.Length)
                {
                    transaction.Commit();
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        return num;
    }

    public static int ExeSqlCommands(ArrayList sqlCommands, bool mustExist)
    {
        int num = 0;
        if (sqlCommands.Count >= 1)
        {
            SqlConnection connection = new SqlConnection(getstrConnection());
            try
            {
                connection.Open();
            }
            catch
            {
                return -1;
            }
            int num2 = 0;
            int num3 = 0;
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                while (num2 < sqlCommands.Count)
                {
                    SqlCommand command = new SqlCommand(sqlCommands[num2].ToString(), connection) {
                        Transaction = transaction
                    };
                    try
                    {
                        num3 = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        num3 = -1;
                    }
                    if ((num3 < 0) || (mustExist && (num3 == 0)))
                    {
                        num = -2;
                        transaction.Rollback();
                        break;
                    }
                    num += num3;
                    num2++;
                }
                if (num2 >= sqlCommands.Count)
                {
                    transaction.Commit();
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        return num;
    }

    public static DataTable GetDBToDataTable(string sqlCommand)
    {
        SqlConnection connection = new SqlConnection(getstrConnection());
        SqlDataAdapter adapter = new SqlDataAdapter {
            SelectCommand = new SqlCommand(sqlCommand, connection)
        };
        try
        {
            connection.Open();
        }
        catch
        {
            return null;
        }
        DataTable dataTable = new DataTable();
        try
        {
            adapter.Fill(dataTable);
        }
        catch (Exception)
        {
        }
        adapter.Dispose();
        connection.Close();
        connection.Dispose();
        return dataTable;
    }

    public static DataTable GetDBToDataTable(string sqlCommand, SqlParameter[] prams)
    {
        DataTable table2;
        using (SqlConnection connection = new SqlConnection(getstrConnection()))
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                using (adapter.SelectCommand = SqlDBA.CreateCommandSql(connection, sqlCommand, prams))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    DataTable dataTable = new DataTable();
                    try
                    {
                        adapter.Fill(dataTable);
                    }
                    catch (Exception)
                    {
                    }
                    adapter.Dispose();
                    connection.Close();
                    connection.Dispose();
                    table2 = dataTable;
                }
            }
        }
        return table2;
    }

    public static DataRowCollection GetDBValue(string sqlCommand) {
        return 
        GetDBToDataTable(sqlCommand).Rows;
    }

    public static ArrayList GetDBValue_1(string sqlCommand)
    {
        SqlConnection connection = new SqlConnection(getstrConnection());
        SqlCommand command = new SqlCommand(sqlCommand, connection);
        try
        {
            connection.Open();
        }
        catch
        {
            return null;
        }
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();
            return null;
        }
        ArrayList list = new ArrayList();
        if (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                list.Add(reader[i]);
            }
        }
        reader.Close();
        connection.Close();
        return list;
    }

    public static ArrayList GetDBValue_2(string sqlCommand)
    {
        SqlConnection connection = new SqlConnection(getstrConnection());
        SqlCommand command = new SqlCommand(sqlCommand, connection);
        try
        {
            connection.Open();
        }
        catch
        {
            return null;
        }
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            reader.Close();
            connection.Close();
            return null;
        }
        ArrayList list = new ArrayList();
        while (reader.Read())
        {
            list.Add(reader[0]);
        }
        reader.Close();
        connection.Close();
        return list;
    }

    public static object GetDBValue_3(string sqlCommand)
    {
        object obj2;
        using (SqlConnection connection = new SqlConnection(getstrConnection()))
        {
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    return null;
                }
                connection.Close();
                obj2 = command.ExecuteScalar();
            }
        }
        return obj2;
    }

    public static object GetDBValue_3(string sqlCommand, SqlParameter[] prams)
    {
        object obj2;
        using (SqlConnection connection = new SqlConnection(getstrConnection()))
        {
            using (SqlCommand command = SqlDBA.CreateCommandSql(connection, sqlCommand, prams))
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    return null;
                }
                connection.Close();
                obj2 = command.ExecuteScalar();
            }
        }
        return obj2;
    }

    public static DataSet GetList(string TBName, int PageSize, int CurPage, string KeyField, string Condition, string Order)
    {
        DataSet dataReader = null;
        SqlConnection conn = new SqlConnection(getstrConnection());
        try
        {
            SqlParameter[] prams = new SqlParameter[] { SqlDBA.MakeInParam("@TBName", SqlDbType.NVarChar, 100, TBName), SqlDBA.MakeInParam("@PageSize", SqlDbType.Int, 10, PageSize), SqlDBA.MakeInParam("@CurPage", SqlDbType.Int, 10, CurPage), SqlDBA.MakeInParam("@KeyField", SqlDbType.NVarChar, 100, KeyField), SqlDBA.MakeInParam("@Condition", SqlDbType.NVarChar, 200, Condition), SqlDBA.MakeInParam("@Order", SqlDbType.NVarChar, 200, Order) };
            SqlDBA.RunProc(conn, "XW_PublicTurnPageWebSite", prams, out dataReader);
        }
        catch (Exception)
        {
        }
        return dataReader;
    }

    public static DataSet GetNewList(string 表名, int 每页显示数, int 当前页, string 查询条件, string 表的主键, string 排序字段, int 排序方法, out int 总页数, out int 记录数)
    {
        DataSet dataReader = null;
        SqlConnection conn = new SqlConnection(getstrConnection());
        总页数 = 0;
        记录数 = 0;
        try
        {
            SqlParameter[] prams = new SqlParameter[] { SqlDBA.MakeInParam("@tblName", SqlDbType.NVarChar, 200, 表名), SqlDBA.MakeInParam("@pageSize", SqlDbType.Int, 10, 每页显示数), SqlDBA.MakeInParam("@page", SqlDbType.Int, 10, 当前页), SqlDBA.MakeInParam("@strCondition", SqlDbType.NVarChar, 0x3e8, 查询条件), SqlDBA.MakeInParam("@ID", SqlDbType.NVarChar, 150, 表的主键), SqlDBA.MakeInParam("@fldSort", SqlDbType.NVarChar, 200, 排序字段), SqlDBA.MakeInParam("@Sort", SqlDbType.Bit, 10, 排序方法), SqlDBA.MakeOutParam("@pageCount", SqlDbType.Int, 4), SqlDBA.MakeOutParam("@Counts", SqlDbType.Int, 4) };
            SqlDBA.RunProc(conn, "proc_ListPageInt", prams, out dataReader);
            总页数 = Convert.ToInt32(prams[7].Value);
            记录数 = Convert.ToInt32(prams[8].Value);
        }
        catch (Exception exception)
        {
            throw new ArgumentException("SqlDBA发生错误，信息为：" + exception.Message);
        }
        return dataReader;
    }

    public static string getstrConnection()
    {
        return ConfigurationManager.AppSettings["ConnString"];
    }
}
