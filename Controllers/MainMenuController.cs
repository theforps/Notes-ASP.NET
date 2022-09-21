using Microsoft.AspNetCore.Mvc;
using System;
using WebNotes.Data;
using WebNotes.Models;

namespace WebNotes.Controllers
{
    public class MainMenuController : Controller
    {
        public readonly NotesDbContext _db;
        public MainMenuController(NotesDbContext db)  {  _db = db;  }


        public IActionResult Main()
        {
            IEnumerable<Note> notesList = _db.Notes;
            return View(notesList);
        }

        //get - create
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                note.CreatedDate = DateTime.Now;
                //временно, пока не добавлю валидацию данных
                _db.Notes.Add(note);
                _db.SaveChanges();
                return RedirectToAction("Main");
            }
            return View(note);
        }

        //get - info
        public IActionResult Info(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var note = _db.Notes.Find(id);

            return View(note);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Info(Note note)
        {
            if (ModelState.IsValid)
            {
                note.CreatedDate = DateTime.Now;
                if (note.Description == null) note.Description = "";
                _db.Notes.Update(note);
                _db.SaveChanges();
                return RedirectToAction("Main");
            }
            return View(note);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int? id)
        {
            var note = _db.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            _db.Notes.Remove(note);
            _db.SaveChanges();

            return RedirectToAction("Main");

        }


    }
}
