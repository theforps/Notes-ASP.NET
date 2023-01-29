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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }



    }
}
