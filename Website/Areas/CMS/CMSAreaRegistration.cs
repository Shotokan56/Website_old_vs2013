﻿using System.Web.Mvc;
using System.Web.Optimization;

namespace Website.Areas.CMS
{
    public class CMSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CMS_default",
                "CMS/{controller}/{action}",
                new { controller = "Login", action = "Index" }
            );
        }
    }
}