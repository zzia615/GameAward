using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

public class DBA
{
    public static int ExeSqlCommand(string sqlCommand, string db, string server)
    {
        int num2;
        using (SqlConnection connection = new SqlConnection(getstrConnection(db, server)))
        {
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
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
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
                num2 = num;
            }
        }
        return num2;
    }

    public static DataSet GetDataSet(string sql, string db, string server)
    {
        SqlConnection selectConnection = new SqlConnection(getstrConnection(db, server));
        DataSet dataSet = new DataSet();
        new SqlDataAdapter(sql, selectConnection).Fill(dataSet);
        return dataSet;
    }

    public static DataTable GetDBToDataTable(string sqlCommand, string db, string server)
    {
        DataTable table2;
        using (SqlConnection connection = new SqlConnection(getstrConnection(db, server)))
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                using (adapter.SelectCommand = new SqlCommand(sqlCommand, connection))
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

    public static object GetDBValue_3(string sqlCommand, string db, string server)
    {
        object obj3;
        using (SqlConnection connection = new SqlConnection(getstrConnection(db, server)))
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
                object obj2 = null;
                try
                {
                    obj2 = command.ExecuteScalar();
                }
                catch (Exception)
                {
                }
                command.Dispose();
                connection.Close();
                connection.Dispose();
                obj3 = obj2;
            }
        }
        return obj3;
    }

    public static DataSet GetList(string TBName, int PageSize, int CurPage, string KeyField, string Condition, string Order, string db, string server)
    {
        DataSet dataReader = null;
        SqlConnection conn = new SqlConnection(getstrConnection(db, server));
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

    public static DataSet GetList(string TBName, int PageSize, int CurPage, string KeyField, string Condition, string Order, string KeyAscDesc, string db, string server)
    {
        DataSet dataReader = null;
        SqlConnection conn = new SqlConnection(getstrConnection(db, server));
        try
        {
            SqlParameter[] prams = new SqlParameter[] { SqlDBA.MakeInParam("@TBName", SqlDbType.NVarChar, 100, TBName), SqlDBA.MakeInParam("@PageSize", SqlDbType.Int, 10, PageSize), SqlDBA.MakeInParam("@CurPage", SqlDbType.Int, 10, CurPage), SqlDBA.MakeInParam("@KeyField", SqlDbType.NVarChar, 100, KeyField), SqlDBA.MakeInParam("@KeyAscDesc", SqlDbType.NVarChar, 4, KeyAscDesc), SqlDBA.MakeInParam("@Condition", SqlDbType.NVarChar, 200, Condition), SqlDBA.MakeInParam("@Order", SqlDbType.NVarChar, 200, Order) };
            SqlDBA.RunProc(conn, "XW_PublicTurnPageWebSite", prams, out dataReader);
        }
        catch (Exception)
        {
        }
        return dataReader;
    }

    public static DataSet GetNewList(string 表名, string 显示字段, int 每页显示数, int 当前页, string 查询条件, string 表的主键, string 排序字段, int 排序方法, out int 总页数, out int 记录数, string db, string server)
    {
        DataSet dataReader = null;
        SqlConnection conn = new SqlConnection(getstrConnection(db, server));
        总页数 = 0;
        记录数 = 0;
        try
        {
            SqlParameter[] prams = new SqlParameter[] { SqlDBA.MakeInParam("@tblName", SqlDbType.NVarChar, 200, 表名), SqlDBA.MakeInParam("@fldName", SqlDbType.NVarChar, 500, 显示字段), SqlDBA.MakeInParam("@pageSize", SqlDbType.Int, 10, 每页显示数), SqlDBA.MakeInParam("@page", SqlDbType.Int, 10, 当前页), SqlDBA.MakeInParam("@strCondition", SqlDbType.NVarChar, 0x3e8, 查询条件), SqlDBA.MakeInParam("@ID", SqlDbType.NVarChar, 150, 表的主键), SqlDBA.MakeInParam("@fldSort", SqlDbType.NVarChar, 200, 排序字段), SqlDBA.MakeInParam("@Sort", SqlDbType.Bit, 10, 排序方法), SqlDBA.MakeOutParam("@pageCount", SqlDbType.Int, 4), SqlDBA.MakeOutParam("@Counts", SqlDbType.Int, 4) };
            SqlDBA.RunProc(conn, "proc_ListPageInt", prams, out dataReader);
            总页数 = Convert.ToInt32(prams[8].Value);
            记录数 = Convert.ToInt32(prams[9].Value);
        }
        catch (Exception exception)
        {
            throw new ArgumentException("SqlDBA发生错误，信息为：" + exception.Message);
        }
        return dataReader;
    }

    public static string getstrConnection(string db, string server)
    {
        try
        {
            if (db == null)
            {
                db = "rxjhgame";
            }
            if (db == "PublicDb")
            {
                return ConfigurationManager.AppSettings["PublicDb"].ToString();
            }
            string sqlCommand = string.Format("select * from serverlist WHERE serverid = @serverid", new object[0]);
            SqlParameter[] prams = new SqlParameter[] { SqlDBA.MakeInParam("@serverid", SqlDbType.VarChar, 30, server) };
            DataTable dBToDataTable = DBAi.GetDBToDataTable(sqlCommand, prams);
            if (dBToDataTable != null)
            {
                if (dBToDataTable.Rows.Count > 0)
                {
                    if (db == "rxjhaccount")
                    {
                        return string.Format("Data Source={0};uid={1};pwd={2};database={3};Packet Size=4096;Pooling=true;Max Pool Size=512;Min Pool Size=1"
                            ,dBToDataTable.Rows[0]["DbIp"].ToString(),dBToDataTable.Rows[0]["DbUser"].ToString(),dBToDataTable.Rows[0]["DbPass"].ToString(),dBToDataTable.Rows[0]["DbAccount"].ToString());
                    }
                    return string.Format("Data Source={0};uid={1};pwd={2};database={3};Packet Size=4096;Pooling=true;Max Pool Size=512;Min Pool Size=1",
                    dBToDataTable.Rows[0]["DbIp"].ToString(), dBToDataTable.Rows[0]["DbUser"].ToString(), dBToDataTable.Rows[0]["DbPass"].ToString(), dBToDataTable.Rows[0]["DbGame"].ToString());
                }
                return null;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
}
