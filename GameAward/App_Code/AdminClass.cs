using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class AdminClass
{
    public static void aacc()
    {
    }

    public static string cutString(string strInput, int intLen)
    {
        strInput = strInput.Trim();
        if (Encoding.Default.GetBytes(strInput).Length <= intLen)
        {
            return strInput;
        }
        string s = "";
        string str2 = "";
        for (int i = 0; i < strInput.Length; i++)
        {
            if (Encoding.Default.GetBytes(s).Length >= (intLen - 4))
            {
                break;
            }
            s = s + strInput.Substring(i, 1);
        }
        str2 = "<img src=\"images/sys_bg85.gif\" href=\"#\" onMouseOver=\"fnDef();\" title=\"" + strInput + "\">";
        return (s + str2);
    }

    public static string GetItemslist(ItmeSClass ItmeSC, byte[] Itmes, int 物品数)
    {
        Dictionary<int, 物品类> dictionary = new Dictionary<int, 物品类>();
        for (int i = 0; i < 物品数; i++)
        {
            if (Itmes.Length >= ((i * 0x49) + 0x49))
            {
                byte[] dst = new byte[0x49];
                try
                {
                    Buffer.BlockCopy(Itmes, i * 0x49, dst, 0, 0x49);
                    物品类 物品类 = new 物品类(dst);
                    dictionary.Add(i, 物品类);
                    ItmeClass item = new ItmeClass {
                        ID = i,
                        FLD_ID = BitConverter.ToInt64(物品类.物品全局ID, 0),
                        FLD_ItmeID = BitConverter.ToInt32(物品类.物品ID, 0),
                        FLD_NAME = 物品类.物品名称,
                        FLD_Type = 物品类.物品类型,
                        FLD_Suliang = BitConverter.ToInt32(物品类.物品数量, 0),
                        FLD_MAGIC0 = 物品类.FLD_MAGIC0,
                        FLD_MAGIC1 = 物品类.FLD_MAGIC1,
                        FLD_MAGIC2 = 物品类.FLD_MAGIC2,
                        FLD_MAGIC3 = 物品类.FLD_MAGIC3,
                        FLD_MAGIC4 = 物品类.FLD_MAGIC4,
                        //FLD_CjFh = 物品类.FLD_FJ_初级附魂,
                        //FLD_ZjFh = 物品类.FLD_FJ_中级附魂,
                        //FLD_JingHua = 物品类.FLD_FJ_进化,
                        FLD_Bind = 物品类.物品绑定
                    };
                    ItmeSC.ItmeList.Add(item);
                    if (BitConverter.ToInt32(物品类.物品ID, 0) == 0)
                    {
                        ItmeSC.Kwei++;
                    }
                }
                catch (Exception exception)
                {
                    throw new ArgumentException("提示：数据错误1！" + exception.Message);
                }
            }
        }
        return JsonConvert.SerializeObject(ItmeSC);
    }

    public static string Getsh(string sh, string id)
    {
        if (sh == "0")
        {
            return ("<a class='black' href='admin_Post.aspx?id=" + id + "&type=sh'>通过</a>");
        }
        return ("<a class='black' href='admin_Post.aspx?id=" + id + "&type=bsh'>取消</a>");
    }

    public static bool GetUser(string id, string pwd)
    {
        string sqlCommand = string.Format("select id from Admin where name=@UserId and pwd=@UserPwd", new object[0]);
        SqlParameter[] prams = new SqlParameter[] { SqlDBA.MakeInParam("@UserId", SqlDbType.VarChar, 10, id), SqlDBA.MakeInParam("@UserPwd", SqlDbType.VarChar, 10, pwd) };
        DataTable dBToDataTable = DBAi.GetDBToDataTable(sqlCommand, prams);
        if (dBToDataTable == null)
        {
            return false;
        }
        if (dBToDataTable.Rows.Count == 0)
        {
            return false;
        }
        return true;
    }

    public static bool GetUserONLINE(string id, string server)
    {
        DataTable table = DBA.GetDBToDataTable(string.Format("select FLD_ID,FLD_ONLINE from [TBL_ACCOUNT] where FLD_ID='{0}'",id), "rxjhaccount", server);
        if (table != null)
        {
            if (int.Parse(table.Rows[0]["FLD_ONLINE"].ToString()) == 1)
            {
                table.Dispose();
                return true;
            }
            table.Dispose();
            return false;
        }
        table.Dispose();
        return true;
    }

    public static void SetItmeLog(long id, string ItmeName, string name, string sx, string server, string sm)
    {
        DBAi.ExeSqlCommand(string.Format("INSERT INTO Itme_Log (ItmeId,ItmeName,[name],shuxing,server,sm)values({0},'{1}','{2}','{3}','{4}','{5}')",
            id,ItmeName,name,sx,server,sm));
    }

    public static void SetLog(string 用户, string IP, string 操作类型, string 操作内容)
    {
        DBAi.ExeSqlCommand(string.Format("INSERT INTO 操作日志(用户,IP,操作类型,操作内容) VALUES('{0}','{1}','{2}','{3}')",
            用户,IP,操作类型,操作内容));
    }

    public static string ToString(byte[] bytes)
    {
        StringBuilder builder = new StringBuilder();
        foreach (byte num2 in bytes)
        {
            builder.Append(Convert.ToString((short) num2, 0x10).PadLeft(2, '0').ToUpper());
        }
        return builder.ToString();
    }

    public static void UpItemsDate(string ServerId, string UserName, byte[] Itmes, string SelectDb)
    {
        string str = "TBL_XWWL_Char";
        string str2 = "FLD_ITEM";
        string str3 = "FLD_NAME";
        if (SelectDb == "0")
        {
            str = "TBL_XWWL_Char";
            str2 = "FLD_ITEM";
            str3 = "FLD_NAME";
        }
        else if (SelectDb == "1")
        {
            str = "TBL_XWWL_Char";
            str2 = "FLD_WEARITEM";
            str3 = "FLD_NAME";
        }
        else if (SelectDb == "2")
        {
            str = "TBL_XWWL_Warehouse";
            str2 = "FLD_ITEM";
            str3 = "FLD_NAME";
        }
        else if (SelectDb == "3")
        {
            str = "TBL_XWWL_PublicWarehouse";
            str2 = "FLD_ITEM";
            str3 = "FLD_ID";
        }
        DBA.ExeSqlCommand(string.Format("update {0} set {1}=0x{2}  WHERE {3} ='{4}'",
            str, str2, ToString(Itmes), str3, UserName), "rxjhgame", ServerId);
    }
}
