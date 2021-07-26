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
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(int id)
        {
            var objComplex = HttpContext.Session.GetString("FoxLog");
            if(objComplex == "true"){
                var enumerableCar = _db.Task.Where(x => x.UserId == id).ToList();
                ViewData["uid"]= id;
                return View(enumerableCar);
            }else{
                return RedirectToAction("Index", "Welcome");
            }

            
        }

        // GET-Create
        public IActionResult Create(int uid)
        {
            // var tlist = _db.Task.Where(x => x.UserId.Equals(uid));
            ViewData["uid"]= uid;
            // System.Diagnostics.Debug.WriteLine(uid);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tasks obj)
        {
            System.Diagnostics.Debug.WriteLine(obj.UserId);

            _db.Task.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", new {id = obj.UserId });
        }

        public IActionResult Details(int? id){
            Tasks task = _db.Task.Find(id);
            return View(task);
        }

        public IActionResult Deletes(int uid, int? id){
            Tasks task = _db.Task.Find(id);
            _db.Task.Remove(task);
            _db.SaveChanges();
            return RedirectToAction("Index", new{id= uid});
        }

        public IActionResult Updates(int uid, int? id)
        {

            Tasks task = _db.Task.Find(id);
            // _db.User.Update(user);
            // _db.SaveChanges();
            ViewData["uid"] = uid; 
            return View(task);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Updates(Tasks obj){
            _db.Task.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", new{id=obj.UserId});
        }

    }
}