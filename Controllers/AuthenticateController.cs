using AuthenticationDemo.DAL;
using AuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            try
            {
                Random r = new Random();

                int otp = r.Next(1000 , 9999);
                var userEmail = authenticateDAL.GetUserEmail(username);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ksp9823707965@gmail.com");
                mailMessage.To.Add(userEmail);
                mailMessage.Subject = "Password Reset DemoApp";
                mailMessage.Body = $"Hello {userEmail}, your OTP for password reset is: {otp}. Please be careful while resetting your password.";

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("ksp9823707965@gmail.com", "jmyg abhl jxsm pixr")
                };

                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    // Log the error or display a message
                    Console.WriteLine("An error occurred while sending the email: " + ex.Message);
                }
                return Json(otp, JsonRequestBehavior.AllowGet);
              

            }
            catch (Exception ex) {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            
           
        }


        public ActionResult ResetPass(string newpass , string username)
        {
            bool res = authenticateDAL.ResetPass(newpass , username);
            if (res)
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error in Reseting Password Please Try Again After Some Time " , JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}