using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebNotes.Data;
using WebNotes.Models;

namespace WebNotes.Controllers
{
    public class LoginScreenController : Controller
    {
        public readonly NotesDbContext _db;
        public LoginScreenController(NotesDbContext db)  
        {  
            _db = db;  
        }

        public IActionResult Main()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var obj = new User();

            return View(obj);
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var obj = _db.Users;

            foreach (var x in obj)
            {
                if (x.Login == user.Login)
                {
                    if (x.Password == user.Password)
                    {
                        WC.Id = x.Id;
                        return RedirectToAction("Grid", "CollectionOfNotes");
                    }
                    return View(user);
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var obj = new User();

            return View(obj);
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            var obj = _db.Users;

            foreach (var x in obj)
            {
                if (x.Login == user.Login)
                {
                    return View(user);
                }
                
            }
            if (user.Password == user.ConfirmPassword)
            {
                user.DateOfCreate = DateTime.Now;
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            return View("Main");
        }


    }
}
