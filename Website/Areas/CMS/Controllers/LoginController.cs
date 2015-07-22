﻿using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Resources;
using Website.Areas.CMS.Models;
using Website.Common;
using Website.Models;
using AutoMapper;
using System.Web.Security;

namespace Website.Areas.CMS.Controllers
{
    public class LoginController : Controller
    {
        public WebAPPEntities db = new WebAPPEntities();
        //
        // GET: /CMS/Login/

        public ActionResult Index(UserViewModel userVm)
        {
            userVm.UserName = "1";
            userVm.PassWord = "1";
            return View("Login", userVm);
        }

        [HttpPost]
        public ActionResult Verify(UserViewModel userVm)
        {
            ModelState.Clear();
            if (!Validate(userVm))
            {
                return View("Login", userVm);
            }

            var objDbUser = db.Users.FirstOrDefault(o => o.UserName == userVm.UserName);

            if (objDbUser == null)
            {
                ModelState.AddModelError("Message", TextMessage.LoginController_Validate_NotValid);
                return View("Login", userVm);
            }

            //Mapper.CreateMap<User, UserViewModel>();
            //var userViewModel = Mapper.Map<User>(user);

            if (!Hashing.VerifyHashedPassword(objDbUser.PassWord, userVm.PassWord) || string.IsNullOrEmpty(objDbUser.Roles))
            {
                ModelState.AddModelError("Message", TextMessage.LoginController_Validate_NotValid);
                return View("Login", userVm);
            }
            else
            {
                Session.Add("User", objDbUser);
                FormsAuthentication.SetAuthCookie(objDbUser.UserName, userVm.RememberMe);

               

                return RedirectToAction("index", "Home");
            }
        }
        
       

        private bool Validate(UserViewModel userVm)
        {

            if (string.IsNullOrEmpty(userVm.UserName) || string.IsNullOrWhiteSpace(userVm.UserName))
            {
                ModelState.AddModelError("Message", TextMessage.LoginController_Validate_UserName);
            }

            if (string.IsNullOrEmpty(userVm.PassWord) || string.IsNullOrWhiteSpace(userVm.PassWord))
            {
                ModelState.AddModelError("Message", TextMessage.LoginController_Validate_PassWord);
            }
            return ModelState.IsValid;
        }
    }
}
