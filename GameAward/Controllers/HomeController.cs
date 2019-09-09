/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2019-9-6
 * Time: 13:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GameAward.Models;
using System.Text;
namespace GameAward.Controllers
{
    public class HomeController : BaseController
    {
        RxjhAccountContext accountcontext = new RxjhAccountContext();
        RxjhGameContext gamecontext = new RxjhGameContext();
        GameCjContext cjcontext = new GameCjContext();
        RxjhHelper rxjh = new RxjhHelper();
        const string MIME_JSON = "application/json";

        string Session_FLDID 
        {
            get 
            {
                if(Session["FLD_ID"]!=null)
                {
                    return Session["FLD_ID"].ToString();
                }
                return "";
            }
        
        }
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
		{
            if (!string.IsNullOrEmpty(Session_FLDID))
            {
                var user = cjcontext.GamingUser.Where(a => a.UserId == Session_FLDID).SingleOrDefault();
                ViewData["GamingUser"] = user;
                var account = accountcontext.TBL_ACCOUNT.Where(a=>a.FLD_ID == Session_FLDID).FirstOrDefault();
                if(account!=null)
                {
                    ViewData["yuanbao"] = account.FLD_RXPIONT;
                    ViewData["jinyuanbao"] = account.FLD_RXPIONTX;
                }
            }
            ViewData["GamingGoods"] = cjcontext.GamingGoods.ToList();
            return View();
		}

        public ActionResult Bind()
        {
            if (string.IsNullOrEmpty(Session_FLDID))
            {
                return base.RedirectToAction("Index");
            }

            var xwwl_char = gamecontext.TBL_XWWL_Char.Where(a => a.FLD_ID == Session_FLDID).ToList();
            List<string> dataList = (from a in xwwl_char
                                     select a.FLD_NAME).ToList();

            return View(dataList);
        }


        public ActionResult Binding()
        {
            string s_success = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "成功"
            });
            string str = base.Request.Params["UserName"];
            if (!string.IsNullOrEmpty(str) && (!string.IsNullOrEmpty(Session_FLDID)))
            {
                try
                {
                    var xwwl_char = gamecontext.TBL_XWWL_Char.Where(a => a.FLD_ID == Session_FLDID && a.FLD_NAME == str).SingleOrDefault();
                    if (xwwl_char == null)
                    {
                        s_success = JsonConvert.SerializeObject(new
                        {
                            success = false,
                            message = "数据异常"
                        });
                        return Content(s_success, MIME_JSON);
                    }
                    var gameUser = cjcontext.GamingUser.Where(a => a.UserId == Session_FLDID).SingleOrDefault();
                    if (gameUser != null)
                    {
                        gameUser.UserName = str;
                        cjcontext.Entry(gameUser).State = System.Data.EntityState.Modified;
                    }
                    else
                    {
                        gameUser = new GamingUser
                        {
                            UserId = Session_FLDID,
                            UserName = str,
                            Count = AppConfig.CJ_CS
                        };
                        cjcontext.Entry(gameUser).State = System.Data.EntityState.Added;
                    }
                    cjcontext.SaveChanges();
                    return Content(s_success, MIME_JSON);
                }
                catch (Exception)
                {
                    s_success = JsonConvert.SerializeObject(new
                    {
                        success = false,
                        message = "数据异常"
                    });
                    return Content(s_success, MIME_JSON);
                }
            }
            else 
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "数据异常"
                });
                return Content(s_success, MIME_JSON);
            }
        }




        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
		[HttpPost]
		public ActionResult Login()
        {
            string s_success = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "登录成功"
            });
            string s_user = Request.Params["UserName"];
            string s_pwd = Request.Params["PassWord"];

            if (string.IsNullOrEmpty(s_user)) {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "请输入用户名"
                });

                return Content(s_success,MIME_JSON);
            }
            if (string.IsNullOrEmpty(s_pwd))
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "请输入密码"
                });

                return Content(s_success, MIME_JSON);
            }

            var account = accountcontext.TBL_ACCOUNT.Where(a => a.FLD_ID == s_user && a.FLD_PASSWORD == s_pwd).SingleOrDefault();
            if (account == null) 
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "用户名密码有误"
                });

                return Content(s_success, MIME_JSON);
            }

            var gameuser = cjcontext.GamingUser.Where(a => a.UserId == account.FLD_ID).FirstOrDefault();
            if (gameuser == null) 
            {
                GamingUser user = new GamingUser
                {
                    UserId = account.FLD_ID,
                    UserName = account.FLD_NAME,
                    Count = AppConfig.CJ_CS
                };
                cjcontext.Entry(user).State = System.Data.EntityState.Added;
                cjcontext.SaveChanges();
            }

            Session["FLD_ID"] = account.FLD_ID;
            return Content(s_success, MIME_JSON);
		}
        /// <summary>
        /// 获取抽奖记录
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneRecord()
        {
            var dataList = cjcontext.GamingRecord.ToList();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(dataList.Count);
            var record = dataList[num];
            string[] textArray1 = new string[] { "恭喜玩家【", record.UserName, "】抽取到了【", record.ItemName, "】！" };

            string s_success = JsonConvert.SerializeObject(new
            {
                success = true,
                message = string.Concat(textArray1)
            });
            return Content(s_success, MIME_JSON);
        }
        /// <summary>
        /// 抽奖一次
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult ChoujiangOneTimes()
        {
            return Choujiang(1);
        }
        /// <summary>
        /// 抽奖十次
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult ChoujiangTenTimes()
        {
            return Choujiang(10);
        }
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        public string Exit()
        {
            Session.Abandon();
            return "true";
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginView()
        {
            return base.View();
        }
        /// <summary>
        /// 购买
        /// </summary>
        /// <returns></returns>
        public ActionResult GouMaiView()
        {
            return base.View();
        }
        /// <summary>
        /// 购买元宝
        /// </summary>
        /// <returns></returns>
        public ActionResult GouMaiViewYuanBao()
        {
            return base.View();
        }

        [HttpPost]
        public ActionResult GouMai()
        {

            string s_success = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "购买成功"
            }); 
            if (string.IsNullOrEmpty(Session_FLDID))
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "请重新登录"
                });
                return Content(s_success, MIME_JSON);
            }

            int num = 0;
            if (!int.TryParse(Request.Params["Count"], out num))
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "参数异常！"
                });
                return Content(s_success, MIME_JSON);
            }

            var gameUser = cjcontext.GamingUser.Where(a => a.UserId == Session_FLDID).SingleOrDefault();
            if (gameUser == null) 
            {

                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "数据异常请重新登陆!"
                });
                return Content(s_success, MIME_JSON);
            }


            var account = accountcontext.TBL_ACCOUNT.Where(a => a.FLD_ID == Session_FLDID).SingleOrDefault();
            if (account == null) 
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "用户角色不存在"
                });
                return Content(s_success, MIME_JSON);
            }
            if (account.FLD_RXPIONTX < num * AppConfig.Gold_YB_Price)
            {

                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "金元宝余额不足！"
                });
                return Content(s_success, MIME_JSON);
            }
            try
            {
                account.FLD_RXPIONTX = account.FLD_RXPIONTX - num * AppConfig.Gold_YB_Price;
                accountcontext.Entry(account).State = System.Data.EntityState.Modified;
                accountcontext.SaveChanges();

                gameUser.Count += num;
                cjcontext.Entry(gameUser).State = System.Data.EntityState.Modified;
                cjcontext.SaveChanges();
            }
            catch 
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "系统出错，购买失败！"
                });
                return Content(s_success, MIME_JSON);
            }

            return Content(s_success, MIME_JSON);
        }


        [HttpPost]
        public ActionResult GouMaiByYuanBao()
        {
            string s_success = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "购买成功"
            });
            if (string.IsNullOrEmpty(Session_FLDID))
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "请重新登录"
                });
                return Content(s_success, MIME_JSON);
            }

            int num = 0;
            if (!int.TryParse(Request.Params["Count"], out num))
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "参数异常！"
                });
                return Content(s_success, MIME_JSON);
            }

            var gameUser = cjcontext.GamingUser.Where(a => a.UserId == Session_FLDID).SingleOrDefault();
            if (gameUser == null)
            {

                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "数据异常请重新登陆!"
                });
                return Content(s_success, MIME_JSON);
            }


            var account = accountcontext.TBL_ACCOUNT.Where(a => a.FLD_ID == Session_FLDID).SingleOrDefault();
            if (account == null)
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "用户角色不存在"
                });
                return Content(s_success, MIME_JSON);
            }
            if (account.FLD_RXPIONT < num * AppConfig.YB_Price)
            {

                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "元宝余额不足！"
                });
                return Content(s_success, MIME_JSON);
            }
            try
            {
                account.FLD_RXPIONT = account.FLD_RXPIONT - num * AppConfig.YB_Price;
                accountcontext.Entry(account).State = System.Data.EntityState.Modified;
                accountcontext.SaveChanges();

                gameUser.Count += num;
                cjcontext.Entry(gameUser).State = System.Data.EntityState.Modified;
                cjcontext.SaveChanges();
            }
            catch
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "系统出错，购买失败！"
                });
                return Content(s_success, MIME_JSON);
            }

            return Content(s_success, MIME_JSON);
        }



        #region 查看物品和插入物品
        ItmeSClass GetBag(string username)
        {
            //return rxjh.GetBag(username);
            var xwwl_char = gamecontext.TBL_XWWL_Char.Where(a => a.FLD_NAME == username).SingleOrDefault();
            if (xwwl_char == null)
            {
                throw new ArgumentException("提示：错误没有这个人物！数据库可能有问题");
            }
            int num = 36;
            ItmeSClass class2 = new ItmeSClass();
            byte[] src = xwwl_char.FLD_ITEM;
            for (int i = 0; i < num; i++)
            {
                if (src.Length >= ((i * 37) + 37))
                {
                    byte[] dst = new byte[37];
                    try
                    {
                        Buffer.BlockCopy(src, i * 37, dst, 0, 37);
                        物品类 物品类 = new 物品类(dst);
                        ItmeClass item = new ItmeClass
                        {
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
                            //FLD_Bind = 物品类.物品绑定
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
                    class2.FLD_ID = xwwl_char.FLD_ID;
                    class2.FLD_Name = username;
                    class2.FLD_JOB = xwwl_char.FLD_JOB.ToString();
                    class2.FLD_ZX = xwwl_char.FLD_ZX.ToString();
                }
            }
            return class2;
        }


        bool InsertItem(string username, int weizi, int itemid)
        {

            //return rxjh.InsertItem(username, weizi,itemid);
            try
            {
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
                var xwwl_char = gamecontext.TBL_XWWL_Char.Where(a => a.FLD_NAME == username).SingleOrDefault();
                if (xwwl_char == null)
                {
                    throw new ArgumentException("提示：错误没有这个人物！");
                }
                物品类 物品类 = new 物品类(new byte[37]);
                if (num2 != 0)
                {
                    sm = "更新物品 " + sm;
                }
                else
                {
                    num2 = gamecontext.Database.SqlQuery<int>("EXEC XWWL_GetItemSerial 1").SingleOrDefault();
                }
                物品类.物品全局ID = BitConverter.GetBytes(num2);
                物品类.物品ID = BitConverter.GetBytes(num3);
                物品类.物品数量 = BitConverter.GetBytes(num12);
                物品类.FLD_MAGIC0 = num4;
                物品类.FLD_MAGIC1 = num5;
                物品类.FLD_MAGIC2 = num6;
                物品类.FLD_MAGIC3 = num7;
                物品类.FLD_MAGIC4 = num8;
                物品类.物品绑定 = flag;
                byte[] dst = (byte[])xwwl_char.FLD_ITEM;
                Buffer.BlockCopy(物品类.物品_byte, 0, dst, num * 37, 37);
                //gamecontext.Database.ExecuteSqlCommand(string.Format("update TBL_XWWL_Char set FLD_ITEM=0x{0}  WHERE FLD_NAME ='{1}'",
                //     ToString(dst), username));

                xwwl_char.FLD_ITEM = dst;
                gamecontext.Entry(xwwl_char).State = System.Data.EntityState.Modified;
                gamecontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        string ToString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in bytes)
            {
                builder.Append(Convert.ToString((short)num2, 0x10).PadLeft(2, '0').ToUpper());
            }
            return builder.ToString();
        }

        #endregion

        #region 随机生成奖品

        GamingGoods GetOneGoods()
        {
            return GetManyGoods(1).SingleOrDefault();
        }


        List<GamingGoods> GetManyGoods(int num)
        {
            var dataList = this.cjcontext.GamingGoods.ToList();
            List<GamingGoods> goodsList = new List<GamingGoods>();
            foreach (var goods in dataList)
            {
                for (int i = 0; i < goods.LuckNum; i++)
                {
                    goodsList.Add(goods);
                }
            }
            List<GamingGoods> resultList = new List<GamingGoods>();
            for (int i = 0; i < num; i++)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int r = random.Next(goodsList.Count);
                resultList.Add(goodsList[r]);
            }
            return resultList;
        } 
        #endregion

        #region 抽奖

        ActionResult Choujiang(int count)
        {
            string s_success = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "成功"
            });
            if (string.IsNullOrEmpty(Session_FLDID))
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "请重新登录"
                });
                return Content(s_success, MIME_JSON);
            }

            var gameUser = cjcontext.GamingUser.Where(a => a.UserId == Session_FLDID).SingleOrDefault();
            var account = accountcontext.TBL_ACCOUNT.Where(a => a.FLD_ID == Session_FLDID).SingleOrDefault();
            string msg = "";

            if (gameUser == null || account == null)
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "该角色不存在！"
                });
                return Content(s_success, MIME_JSON);
            }
            else
            {
                if ((gameUser != null && string.IsNullOrEmpty(gameUser.UserName))
                    || (account != null && string.IsNullOrEmpty(account.FLD_NAME)))
                {
                    s_success = JsonConvert.SerializeObject(new
                    {
                        success = false,
                        message = "该角色名不存在！"
                    });
                    return Content(s_success, MIME_JSON);
                }
            }
            if (account.FLD_ONLINE == 1)
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "帐号在线请先下线！"
                });
                return Content(s_success, MIME_JSON);
            }
            if (gameUser.Count < count)
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "抽奖次数不足"
                });
                return Content(s_success, MIME_JSON);
            }

            if (!CheckFLDZS(gameUser.UserName, out msg))
            {
                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = msg
                });
                return Content(s_success, MIME_JSON);
            }
            try
            {
                ItmeSClass class2 = GetBag(gameUser.UserName);
                class2.FLD_ONLINE = account.FLD_ONLINE.ToString();
                List<int> kwList = new List<int>();
                List<GamingGoods> resultGoods = new List<GamingGoods>();
                for (int num2 = 0; num2 < 0x24; num2++)
                {
                    if (class2.ItmeList[num2].FLD_ID == 0 && kwList.Count < count)
                    {
                        kwList.Add(num2);
                    }
                }
                if (class2.Kwei >= count)
                {
                    foreach (int weizi in kwList)
                    {
                        var gameGoods = GetOneGoods();
                        resultGoods.Add(gameGoods);
                        int itemId = int.Parse(gameGoods.GoodId);
                        bool flag = InsertItem(gameUser.UserName, weizi, itemId);
                        GamingRecord record = new GamingRecord
                        {
                            ItemId = gameGoods.GoodId,
                            ItemName = gameGoods.Name,
                            Struts = flag,
                            UserId = gameUser.UserId,
                            UserName = gameUser.UserName,
                            UserSpace = Request.UserHostAddress,
                            CreateTime = DateTime.Now
                        };
                        cjcontext.Entry(record).State = System.Data.EntityState.Added;
                        cjcontext.SaveChanges();
                        if (!flag)
                        {
                            s_success = JsonConvert.SerializeObject(new
                            {
                                success = false,
                                message = "系统异常"
                            });
                            return Content(s_success, MIME_JSON);
                        }

                        gameUser.Count--;
                        cjcontext.Entry(gameUser).State = System.Data.EntityState.Modified;
                        cjcontext.SaveChanges();
                    }

                    //s_success = JsonConvert.SerializeObject(new
                    //{
                    //    success = true,
                    //    message = "成功"
                    //});

                    if (resultGoods.Count == 1)
                    {
                        s_success = JsonConvert.SerializeObject(new
                        {
                            success = true,
                            message = "成功",
                            itemid = resultGoods[0].GoodId,
                            itemname = resultGoods[0].Name,
                        });
                    }
                    else 
                    {
                        s_success = JsonConvert.SerializeObject(new
                        {
                            success = true,
                            message = "成功",
                            list = (from a in resultGoods
                                   select new {Name=a.Name}).ToList()
                        });
                    }

                    return Content(s_success, MIME_JSON);
                }
                else
                {
                    s_success = JsonConvert.SerializeObject(new
                    {
                        success = false,
                        message = "背包空间不足"
                    });
                    return Content(s_success, MIME_JSON);
                }
            }
            catch (Exception ex)
            {

                s_success = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = ex.Message
                });
                return Content(s_success, MIME_JSON);
            }
        }
        #endregion

        #region 校验转生次数
        bool CheckFLDZS(string fld_name, out string msg)
        {
            msg = "";
            int fld_zs = 0;
            var xwwl_char = gamecontext.TBL_XWWL_Char.Where(a => a.FLD_NAME == fld_name).SingleOrDefault();
            if (xwwl_char != null)
            {
                fld_zs = xwwl_char.FLD_ZS;
            }
            else
            {
                msg = "用户名查不到任何数据！";
                return false;
            }

            if (fld_zs <= AppConfig.FLD_ZS)
            {
                msg = string.Format("转生次数大于{0}才可以参加活动哟！", AppConfig.FLD_ZS);
                return false;
            }

            return true;
        }
        #endregion
    }
}
