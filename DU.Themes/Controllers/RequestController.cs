using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DU.Themes.Controllers
{
    [Authorize]
    public class RequestController : BaseController
    {
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewRequest()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}