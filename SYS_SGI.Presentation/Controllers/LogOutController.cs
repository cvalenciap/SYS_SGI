using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYS_SGI.Presentation.Controllers
{
    public class LogOutController : Controller
    {
        //
        // GET: /LogOut/

        public string LogOutClient()
        {
            Session.Clear();
            Session.Abandon();
            return "";
        }

    }
}
