using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace GameAward
{
    public class AppConfig
    {
        /// <summary>
        /// 转生
        /// </summary>
        public static int FLD_ZS
        {
            get
            {
                try
                {
                    return int.Parse(GetConfig("ZhuanShengCiShu"));
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 金元宝
        /// </summary>
        public static int Gold_YB_Price
        {
            get
            {
                try
                {
                    return int.Parse(GetConfig("DanJia"));
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 元宝
        /// </summary>
        public static int YB_Price
        {
            get
            {
                try
                {
                    return int.Parse(GetConfig("YuanBaoDanJia"));
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 抽奖次数
        /// </summary>
        public static int CJ_CS
        {
            get
            {
                try
                {
                    return int.Parse(GetConfig("chushicishu"));
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static string LicenseKey
        {
            get
            {
                try
                {
                    return GetConfig("LicenseKey");
                }
                catch
                {
                    return "";
                }
            }
        }

        
        static string GetConfig(string key) 
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}