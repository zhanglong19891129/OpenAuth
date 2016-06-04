﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Infrastructure.Helper;
using OpenAuth.App;
using OpenAuth.Mvc.Models;

namespace OpenAuth.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private LoginApp _app;

        public LoginController()
        {
            _app = AutofacExt.GetFromFac<LoginApp>();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            try
            {
                _app.Login(username, password);
                return RedirectToAction("Index", "Home");
                
            }
            catch (Exception e)
            {
                var response = new BjuiResponse
                {
                    statusCode = "300",
                    message = e.Message
                };
                return View(response);
            }
        }

        /// <summary>
        /// 开发者登陆
        /// </summary>
        public ActionResult LoginByDev()
        {
            try
            {
                _app.LoginByDev();
                return RedirectToAction("Index", "Home");

            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}