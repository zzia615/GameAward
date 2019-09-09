using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

public class SqlDBA
{
    private static SqlCommand CreateCommand(SqlConnection conn, string procName, SqlParameter[] prams)
    {
        SqlCommand command = new SqlCommand(procName, conn) {
            CommandType = CommandType.StoredProcedure
        };
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
            {
                command.Parameters.Add(parameter);
            }
        }
        command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
        return command;
    }

    public static SqlCommand CreateCommandSql(SqlConnection conn, string procName, SqlParameter[] prams)
    {
        SqlCommand command = new SqlCommand(procName, conn) {
            CommandType = CommandType.Text,
            CommandTimeout = 180
        };
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
            {
                command.Parameters.Add(parameter);
            }
        }
        command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
        return command;
    }

    public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value) {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
    }

    public static SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size) {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
    }

    public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
    {
        SqlParameter parameter;
        if (Size > 0)
        {
            parameter = new SqlParameter(ParamName, DbType, Size);
        }
        else
        {
            parameter = new SqlParameter(ParamName, DbType);
        }
        parameter.Direction = Direction;
        if ((Direction != ParameterDirection.Output) || (Value != null))
        {
            parameter.Value = Value;
        }
        return parameter;
    }

    public static int RunProc(SqlConnection conn, string procName, SqlParameter[] prams)
    {
        SqlCommand command1 = CreateCommand(conn, procName, prams);
        command1.ExecuteNonQuery();
        return (int) command1.Parameters["ReturnValue"].Value;
    }

    public static void RunProc(SqlConnection conn, string procName, out SqlDataReader dataReader)
    {
        dataReader = CreateCommand(conn, procName, null).ExecuteReader(CommandBehavior.CloseConnection);
    }

    public static void RunProc(SqlConnection conn, string procName, SqlParameter[] prams, out DataSet dataReader)
    {
        SqlCommand selectCommand = CreateCommand(conn, procName, prams);
        using (SqlDataAdapter adapter = new SqlDataAdapter(selectCommand))
        {
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            selectCommand.Parameters.Clear();
            conn.Close();
            conn.Dispose();
            dataReader = dataSet;
            adapter.Dispose();
        }
    }

    public static void RunProc(SqlConnection conn, string procName, SqlParameter[] prams, out SqlDataReader dataReader)
    {
        dataReader = CreateCommand(conn, procName, prams).ExecuteReader(CommandBehavior.CloseConnection);
    }
}
