using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameAward.Controllers
{
    public class UnRegisterController : Controller
    {
        //
        // GET: /UnRegister/

        public ActionResult Index()
        {
            string host = HttpContext.Request.ServerVariables["HTTP_HOST"];
            string data = host + "-" + SymmetricMethod.GetIP();
            return Content(data);
        }

    }
}
