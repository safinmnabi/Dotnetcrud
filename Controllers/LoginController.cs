using DOTNET.Data;  
using DOTNET.Models;  
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Mvc;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace dotnet.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(SignIn _login)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Logs.FirstOrDefault(s => s.email == _login.email);
                if (check == null)
                {
                    _login.password = GetMD5(_login.password);
                    // _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Logs.Add(_login);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "User already exists";
                    return View();
                }


            }
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username,string Password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(Password);
                var data =_db.Logs.Where(s => s.email.Equals(Username) && s.password.Equals(f_password));



                if (data.Count() > 0 )
                {

                    HttpContext.Session.SetString("FoxLog", "true");
                    HttpContext.Session.SetString("Username", Username);

                    // var obj = HttpContext.Session.GetString("FoxLog");


                    // System.Diagnostics.Debug.WriteLine(obj);

                    return RedirectToAction("Index", "Welcome");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                    // System.Diagnostics.Debug.WriteLine("Failed");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", new { controller = "Login" });
        }


        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }


    }
}
