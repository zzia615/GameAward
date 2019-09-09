using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
using GameAward.Models;

    public class RxjhHelper
    {
        public ItmeSClass GetBag(string username, string serverdb = "0", string serverid = "d1")
        {
            Dictionary<int, 物品类> dictionary = null;
            string server = serverid;
            string str2 = username;
            string str3 = serverdb;
            string str4 = "TBL_XWWL_Char";
            string str5 = "FLD_ITEM,FLD_ID,FLD_JOB,FLD_ZX";
            string str6 = "FLD_ITEM";
            string str7 = "FLD_NAME";
            int num = 0x24;
            if (str3 == "0")
            {
                str4 = "TBL_XWWL_Char";
                str5 = "FLD_ITEM,FLD_ID,FLD_JOB,FLD_ZX";
                str6 = "FLD_ITEM";
                str7 = "FLD_NAME";
                num = 0x24;
            }
            else if (str3 == "1")
            {
                str4 = "TBL_XWWL_Char";
                str5 = "FLD_WEARITEM,FLD_ID,FLD_JOB,FLD_ZX";
                str6 = "FLD_WEARITEM";
                str7 = "FLD_NAME";
                num = 15;
            }
            else if (str3 == "2")
            {
                str4 = "TBL_XWWL_Warehouse";
                str5 = "FLD_ITEM,FLD_ID";
                str6 = "FLD_ITEM";
                str7 = "FLD_NAME";
                num = 60;
            }
            else if (str3 == "3")
            {
                str4 = "TBL_XWWL_PublicWarehouse";
                str5 = "FLD_ITEM,FLD_ID";
                str6 = "FLD_ITEM";
                str7 = "FLD_ID";
                num = 60;
            }
            DataTable table = DBA.GetDBToDataTable(string.Format("select {3} from {2} WHERE {1} ='{0}'", new object[] { str2, str7, str4, str5 }), "rxjhgame", server);
            if (table == null)
            {
                throw new ArgumentException("提示：错误没有这个人物！数据库可能有问题");
            }
            if (table.Rows.Count == 0)
            {
                throw new ArgumentException("提示：错误没有这个人物！");
            }
            dictionary = new Dictionary<int, 物品类>();
            ItmeSClass class2 = new ItmeSClass();
            byte[] src = (byte[]) table.Rows[0][str6];
            for (int i = 0; i < num; i++)
            {
                if (src.Length >= (i *  37 + 37))
                {
                    byte[] dst = new byte[37];
                    try
                    {
                        Buffer.BlockCopy(src, i * 37, dst, 0, 37);
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
                        };
                        class2.ItmeList.Add(item);
                        if (BitConverter.ToInt32(物品类.物品ID, 0) == 0)
                        {
                            class2.Kwei++;
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new ArgumentException("提示：数据错误1！" + exception.Message);
                    }
                    class2.FLD_ID = table.Rows[0]["FLD_ID"].ToString();
                    class2.FLD_Name = str2;
                    if ((str3 != "2") && (str3 != "3"))
                    {
                        class2.FLD_JOB = table.Rows[0]["FLD_JOB"].ToString();
                        class2.FLD_ZX = table.Rows[0]["FLD_ZX"].ToString();
                    }
                    DataTable table2 = DBA.GetDBToDataTable(string.Format("select FLD_ID,FLD_ONLINE,FLD_SEX from [TBL_ACCOUNT] where FLD_ID='{0}'",class2.FLD_ID), "rxjhaccount", server);
                    class2.FLD_ONLINE = table2.Rows[0]["FLD_ONLINE"].ToString();
                    table2.Dispose();
                }
            }
            table.Dispose();
            return class2;
        }




        public bool InsertItem(string username, int weizi, int itemid)
        {
            try
            {
                string server = "d1";
                string name = username;
                int num = weizi;
                long num2 = 0L;
                int num3 = itemid;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                int num7 = 0;
                int num8 = 0;
                int num9 = 0;
                int num10 = 0;
                int num11 = 0;
                int num12 = 1;
                bool flag = false;
                string sm = "550088081";
                string selectDb = "0";
                string str5 = "TBL_XWWL_Char";
                string str6 = "FLD_ITEM,FLD_ID,FLD_JOB,FLD_ZX";
                string str7 = "FLD_ITEM";
                string str8 = "FLD_NAME";
                if (selectDb == "0")
                {
                    str5 = "TBL_XWWL_Char";
                    str6 = "FLD_ITEM,FLD_ID,FLD_JOB,FLD_ZX";
                    str7 = "FLD_ITEM";
                    str8 = "FLD_NAME";
                }
                else if (selectDb == "1")
                {
                    str5 = "TBL_XWWL_Char";
                    str6 = "FLD_WEARITEM,FLD_ID,FLD_JOB,FLD_ZX";
                    str7 = "FLD_WEARITEM";
                    str8 = "FLD_NAME";
                }
                else if (selectDb == "2")
                {
                    str5 = "TBL_XWWL_Warehouse";
                    str6 = "FLD_ITEM,FLD_ID";
                    str7 = "FLD_ITEM";
                    str8 = "FLD_NAME";
                }
                else if (selectDb == "3")
                {
                    str5 = "TBL_XWWL_PublicWarehouse";
                    str6 = "FLD_ITEM,FLD_ID";
                    str7 = "FLD_ITEM";
                    str8 = "FLD_ID";
                }
                DataTable table = DBA.GetDBToDataTable(string.Format("select {3} from {2} WHERE {1} ='{0}'", new object[] { name, str8, str5, str6 }), "rxjhgame", server);
                if (table == null)
                {
                    return false;
                }
                if (table.Rows.Count == 0)
                {
                    table.Dispose();
                    throw new ArgumentException("提示：错误没有这个人物！");
                }
                物品类 物品类 = new 物品类(new byte[37]);
                if (num2 != 0)
                {
                    sm = "更新物品 " + sm;
                }
                else
                {
                    num2 = long.Parse(DBA.GetDBValue_3("EXEC XWWL_GetItemSerial 1", "rxjhgame", server).ToString());
                }
                物品类.物品全局ID = BitConverter.GetBytes(num2);
                物品类.物品ID = BitConverter.GetBytes(num3);
                物品类.物品数量 = BitConverter.GetBytes(num12);
                物品类.FLD_MAGIC0 = num4;
                物品类.FLD_MAGIC1 = num5;
                物品类.FLD_MAGIC2 = num6;
                物品类.FLD_MAGIC3 = num7;
                物品类.FLD_MAGIC4 = num8;
                //物品类.FLD_FJ_初级附魂 = num9;
                //物品类.FLD_FJ_中级附魂 = num10;
                //物品类.FLD_FJ_进化 = num11;
                物品类.物品绑定 = flag;
                byte[] dst = (byte[]) table.Rows[0][str7];
                AdminClass.SetItmeLog(BitConverter.ToInt64(物品类.物品全局ID, 0), 物品类.物品名称, name, string.Concat(new object[] { num4, "_", num5, "_", num6, "_", num7, "_", num8, " 初", num9, " 中", num10, " 进", num11 }), server, sm);
                Buffer.BlockCopy(物品类.物品_byte, 0, dst, num * 37, 37);
                AdminClass.UpItemsDate(server, name, dst, selectDb);
                ItmeSClass itmeSC = new ItmeSClass {
                    FLD_ID = table.Rows[0]["FLD_ID"].ToString(),
                    FLD_Name = name
                };
                if ((selectDb != "2") && (selectDb != "3"))
                {
                    itmeSC.FLD_JOB = table.Rows[0]["FLD_JOB"].ToString();
                    itmeSC.FLD_ZX = table.Rows[0]["FLD_ZX"].ToString();
                }
                DataTable table2 = DBA.GetDBToDataTable(string.Format("select FLD_ID,FLD_ONLINE,FLD_SEX from [TBL_ACCOUNT] where FLD_ID='{0}'",itmeSC.FLD_ID)
                    , "rxjhaccount", server);
                itmeSC.FLD_ONLINE = table2.Rows[0]["FLD_ONLINE"].ToString();
                itmeSC.FLD_SEX = table2.Rows[0]["FLD_SEX"].ToString();
                table2.Dispose();
                AdminClass.GetItemslist(itmeSC, dst, 0x24);
                table.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }