using DOTNET.Data;  
using DOTNET.Models;  
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Mvc;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
  
namespace DOTNET.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WelcomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        // public IActionResult Index()
        // {
        //     var objComplex = HttpContext.Session.GetString("FoxLog");
        //     if(objComplex == "true"){
        //         IEnumerable<Users> objList = _db.User;
        //         return View(objList);
        //     }else{
        //         return RedirectToAction("Login", "Login");
        //     }

            
        // }

        public IActionResult Index()
        {
            var objComplex = HttpContext.Session.GetString("FoxLog");
            

            if(objComplex == "true"){
                var eid = HttpContext.Session.GetString("Username");
                var tlist = _db.User.Where(x => x.EmailId.Equals(eid)).ToList();
                ViewData["id"] = eid;

                // IEnumerable<Users> objList = _db.User;
                return View(tlist);
            }else{
                return RedirectToAction("Login", "Login");
            }

            
        }

        // GET-Create
        public IActionResult Create()
        {   
            var eid = HttpContext.Session.GetString("Username");
            ViewData["id"] = eid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Users obj)
        {
            _db.User.Add(obj);
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        // public IActionResult Details(int? id){
        //     Users user = _db.User.Find(id);
        //     return View(user);
        // }

        public IActionResult Deletes(int? id){
            Users user = _db.User.Find(id);
            _db.User.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Updates(int? id)
        {

            Users user = _db.User.Find(id);

            // _db.User.Update(user);
            // _db.SaveChanges();
            var eid = HttpContext.Session.GetString("Username");
            ViewData["id"] = eid;

            return View(user);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public  IActionResult Updates(Users obj){
            _db.User.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}