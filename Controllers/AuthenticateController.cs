using AuthenticationDemo.DAL;
using AuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace AuthenticationDemo.Controllers
{
    public class AuthenticateController : Controller
    {
        AuthenticateDAL authenticateDAL = new AuthenticateDAL();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login( User user )
        {
            bool IsvalidUser = false;
            try
            {
                 IsvalidUser = authenticateDAL.IsValidUser( user );
            }
            catch(Exception ex)
            {

            }
            if (IsvalidUser)
            {
                ViewBag.Msg = "Login SuccesFully ! WelCome " + user.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Msg = "Invalid Login Credentials , Please Enter Correct UserId And Password !";
                return View("Login");
            }
        }

        [HttpGet]
        public ActionResult ForgePassword() 
        {
            return View();
        }

       
        public JsonResult GeneratedOTP(string username)
        {
            var userEmail = authenticateDAL.GetUserEmail( username );

             


            int otp = 0;
            return Json(otp, JsonRequestBehavior.AllowGet);
        }
    }
}