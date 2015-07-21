﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Common;

namespace Website.Areas.CMS.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /CMS/Home/
        [SessionExpire]
        public ActionResult Index()
        {
            return View();
        }
	}
}