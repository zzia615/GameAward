using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace GameAward.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            base.OnActionExecuting(filterContext);
            try
            {
                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                int i = 0;
                foreach (var ip in ipe.AddressList)
                {
                    if (ip.ToString() != "27.148.190.123")
                    {
                        i++;
                    }
                }

                if (i == ipe.AddressList.Length) 
                {
                    filterContext.HttpContext.Response.Redirect("/UnRegister");
                    return;
                }

                /*
                SymmetricMethod md5 = new SymmetricMethod();
                string kke = md5.Decrypto(AppConfig.LicenseKey);
                string host = filterContext.HttpContext.Request.ServerVariables["HTTP_HOST"];
                if (kke == "")
                {
                    filterContext.HttpContext.Response.Redirect("/UnRegister");
                    return;
                }

                string[] ddd = kke.Split('|');
                string[] ccc = ddd[0].Split(',');
                bool eee = false;
                for (int i = 0; i < ccc.Length; i++)
                {
                    if (host.IndexOf(ccc[i]) != -1)
                    {
                        eee = true;
                    }
                }
                if (!eee)
                {
                    filterContext.HttpContext.Response.Redirect("/UnRegister");
                    return;
                }
                  */

            }
            catch
            {
                filterContext.HttpContext.Response.Redirect("/UnRegister");
                return;
            }


        }
    }
}
