using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 物品类 的摘要说明
/// </summary>
public class 物品类
{
    private byte[] _物品_byte;
    public byte[] 物品_byte
    {
        get
        {
            return _物品_byte;
        }
        set
        {
            _物品_byte = value;
        }
    }

    public 物品类(byte[] 物品_byte_)
    {
        物品_byte = 物品_byte_;
    }
    public string 得到属性类型(string text2)
    {
        string sxname2 = "";
        switch (text2)
        {
            case "1":
                sxname2 = "火";
                break;

            case "2":
                sxname2 = "水";
                break;

            case "3":
                sxname2 = "风";
                break;

            case "4":
                sxname2 = "内";
                break;

            case "5":
                sxname2 = "外";
                break;

            case "6":
                sxname2 = "毒";
                break;
            default:
                sxname2 = "";
                break;
        }
        return sxname2;
    }
    public string 得到属性类型2(string text2)
    {
        string sxname2 = "";
        switch (text2)
        {
            case "1":
                sxname2 = "攻击";
                break;

            case "2":
                sxname2 = "防御";
                break;

            case "3":
                sxname2 = "生命";
                break;

            case "4":
                sxname2 = "内功";
                break;

            case "5":
                sxname2 = "命中";
                break;

            case "6":
                sxname2 = "回避";
                break;
            case "7":
                sxname2 = "武功";
                break;
            case "8":
                sxname2 = "气功";
                break;
            case "9":
                sxname2 = "升级成功率";
                break;
            case "10":
                sxname2 = "追加伤害";
                break;
            case "11":
                sxname2 = "武防";
                break;
            case "12":
                sxname2 = "获得金钱";
                break;
            case "13":
                sxname2 = "死亡损失经验";
                break;
            case "15":
                sxname2 = "经验获得";
                break;
            default:
                sxname2 = "";
                break;
        }
        return sxname2;
    }

    public int 物品类型;

    public string 物品名称
    {
        get
        {
            try
            {
                int id = System.BitConverter.ToInt32(物品ID, 0);
                if (id != 0)
                {
                    string sql = string.Format("select fld_pid,fld_name,FLD_RESIDE2 from [tbl_xwwl_item] WHERE fld_pid ={0}", System.BitConverter.ToInt32(物品ID, 0));
                    DataTable table1 = DBA.GetDBToDataTable(sql, "PublicDb", "PublicDb");
                    if (table1.Rows.Count == 0)
                    {
                        return "未知物品";
                    }
                    else
                    {
                        物品类型 = (int)table1.Rows[0]["FLD_RESIDE2"];
                        return table1.Rows[0]["fld_name"].ToString();
                    }
                }
                else
                {
                    return "空";
                }
            }
            catch
            {
                //throw new ArgumentException("提示：数据错误1！" + ex.Message);
                return "无法读取";
            }
        }
    }
    public byte[] 物品全局ID
    {
        get
        {
            byte[] rwname__zbbaoid = new byte[8];
            Buffer.BlockCopy(物品_byte, 0, rwname__zbbaoid, 0, 8);
            return rwname__zbbaoid;
        }
        set
        {
            Buffer.BlockCopy(value, 0, 物品_byte, 0, 8);
        }
    }
    public byte[] 物品ID
    {
        get
        {
            byte[] rwname__zbbaoid = new byte[4];
            Buffer.BlockCopy(物品_byte, 8, rwname__zbbaoid, 0, 4);
            return rwname__zbbaoid;
        }
        set
        {
            Buffer.BlockCopy(value, 0, 物品_byte, 8, 4);
        }
    }
    public byte[] 物品数量
    {
        get
        {
            byte[] rwname__zbbaoid = new byte[4];
            Buffer.BlockCopy(物品_byte, 12, rwname__zbbaoid, 0, 4);
            return rwname__zbbaoid;
        }
        set
        {
            Buffer.BlockCopy(value, 0, 物品_byte, 12, 4);
        }
    }

    public int 强化类型
    {
        get
        {
            if (FLD_MAGIC0.ToString().Length < 8)
                return 0;
            string qh = FLD_MAGIC0.ToString();
            return int.Parse(qh.Substring(qh.Length - 8, 1));
        }
    }
    public int 强化数量
    {
        get
        {
            if (FLD_MAGIC0.ToString().Length < 8)
                return 0;
            string qh = FLD_MAGIC0.ToString();
            return int.Parse(qh.Substring(qh.Length - 2, 2));
        }
    }

    public string 属性类型
    {
        get
        {
            if (FLD_MAGIC0.ToString().Length < 10)
                return "";
            string qh = FLD_MAGIC0.ToString();
            return 得到属性类型(qh.Substring(qh.Length - 4, 1)) + qh.Substring(qh.Length - 4, 1);
        }
    }
    public int 属性数量
    {
        get
        {
            if (FLD_MAGIC0.ToString().Length < 10)
                return 0;
            string qh = FLD_MAGIC0.ToString();
            return int.Parse(qh.Substring(qh.Length - 3, 1));
        }
    }

    public int FLD_MAGIC0
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 16, rwname__zbbaoid2, 0, 4);
            return System.BitConverter.ToInt32(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 16, 4);
        }
    }

    public int FLD_MAGIC1
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 20, rwname__zbbaoid2, 0, 4);
            return System.BitConverter.ToInt32(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 20, 4);
        }
    }

    public int FLD_MAGIC2
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 24, rwname__zbbaoid2, 0, 4);
            return System.BitConverter.ToInt32(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 24, 4);
        }
    }
    public int FLD_MAGIC3
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 28, rwname__zbbaoid2, 0, 4);
            return System.BitConverter.ToInt32(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 28, 4);
        }
    }
    public int FLD_MAGIC4
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 32, rwname__zbbaoid2, 0, 4);
            return System.BitConverter.ToInt32(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 32, 4);
        }
    }
    public string FLD_MAGIC11
    {
        get
        {
            if (FLD_MAGIC1.ToString().Length < 8)
                return "";
            string qh = FLD_MAGIC1.ToString();
            return 得到属性类型2(qh.Substring(qh.Length - 8, 1)) + qh.Substring(qh.Length - 2, 2);
        }
    }
    public string FLD_MAGIC22
    {
        get
        {
            if (FLD_MAGIC2.ToString().Length < 8)
                return "";
            string qh = FLD_MAGIC2.ToString();
            return 得到属性类型2(qh.Substring(qh.Length - 8, 1)) + qh.Substring(qh.Length - 2, 2);
        }
    }
    public string FLD_MAGIC33
    {
        get
        {
            if (FLD_MAGIC3.ToString().Length < 8)
                return "";
            string qh = FLD_MAGIC3.ToString();
            return 得到属性类型2(qh.Substring(qh.Length - 8, 1)) + qh.Substring(qh.Length - 2, 2);
        }
    }
    public string FLD_MAGIC44
    {
        get
        {
            if (FLD_MAGIC4.ToString().Length < 8)
                return "";
            string qh = FLD_MAGIC4.ToString();
            return 得到属性类型2(qh.Substring(qh.Length - 8, 1)) + qh.Substring(qh.Length - 2, 2);
        }
    }
    public bool 物品绑定
    {
        get
        {
            try
            {
                byte[] rwname__zbbaoid = new byte[2];
                Buffer.BlockCopy(物品_byte, 36, rwname__zbbaoid, 0, 1);
                int id = System.BitConverter.ToInt16(rwname__zbbaoid, 0);
                if (id != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        set
        {
            if (value)
            {
                Buffer.BlockCopy(System.BitConverter.GetBytes(1), 0, 物品_byte, 36, 1);
            }
            else
            {
                Buffer.BlockCopy(System.BitConverter.GetBytes(0), 0, 物品_byte, 36, 1);
            }
        }
    }

    public int FLD_FJ_MAGIC0
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 36, rwname__zbbaoid2, 0, 2);
            return System.BitConverter.ToInt32(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 36, 2);
        }
    }

    public int FLD_FJ_MAGIC2
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[2];
            Buffer.BlockCopy(物品_byte, 42, rwname__zbbaoid2, 0, 2);
            return System.BitConverter.ToInt16(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 42, 2);
        }
    }
    public int FLD_FJ_MAGIC3
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[2];
            Buffer.BlockCopy(物品_byte, 44, rwname__zbbaoid2, 0, 2);
            return System.BitConverter.ToInt16(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 44, 2);
        }
    }
    public int FLD_FJ_MAGIC4
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[2];
            Buffer.BlockCopy(物品_byte, 46, rwname__zbbaoid2, 0, 2);
            return System.BitConverter.ToInt16(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 46, 2);
        }
    }
    public int FLD_FJ_MAGIC5
    {
        get
        {
            byte[] rwname__zbbaoid2 = new byte[2];
            Buffer.BlockCopy(物品_byte, 48, rwname__zbbaoid2, 0, 2);
            return System.BitConverter.ToInt16(rwname__zbbaoid2, 0);
        }
        set
        {
            Buffer.BlockCopy(System.BitConverter.GetBytes(value), 0, 物品_byte, 48, 2);
        }
    }
    public DateTime FLD_FJ_KSSJ
    {
        get
        {

            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 52, rwname__zbbaoid2, 0, 4);
            DateTime aa = new DateTime(1970, 1, 1, 8, 0, 0);
            aa = aa.AddSeconds(System.BitConverter.ToInt32(rwname__zbbaoid2, 0));
            return aa;
        }
        set
        {
            DateTime aa = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan aaa = value.Subtract(aa);
            Buffer.BlockCopy(System.BitConverter.GetBytes(aaa.TotalSeconds), 0, 物品_byte, 52, 4);
        }
    }
    public DateTime FLD_FJ_JSSJ
    {
        get
        {

            byte[] rwname__zbbaoid2 = new byte[4];
            Buffer.BlockCopy(物品_byte, 56, rwname__zbbaoid2, 0, 4);
            DateTime aa = new DateTime(1970, 1, 1, 8, 0, 0);
            aa = aa.AddSeconds(System.BitConverter.ToInt32(rwname__zbbaoid2, 0));
            return aa;
        }
        set
        {
            DateTime aa = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan aaa = value.Subtract(aa);
            Buffer.BlockCopy(System.BitConverter.GetBytes(aaa.TotalSeconds), 0, 物品_byte, 56, 4);
        }
    }
}
