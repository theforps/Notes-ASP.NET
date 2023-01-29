using Microsoft.AspNetCore.Mvc;
using WebNotes.Data;
using WebNotes.Models;

namespace WebNotes.Controllers
{
    public class CollectionOfNotesController : Controller
    {
        public readonly NotesDbContext _db;
        public CollectionOfNotesController(NotesDbContext db)  
        {  
            _db = db;  
        }

        public IActionResult Grid()
        {
            var notes = _db.Notes;

            return View(notes);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var obj = new Note();

            if (id == 0)
                return NotFound();
            else if (id == null)
                return View(obj);

            obj = _db.Notes.Find(id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Upsert(Note note)
        {
            if (ModelState.IsValid)
            {
                if (note.Id != 0) 
                {
                    _db.Notes.Update(note);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Notes.Add(note);
                    _db.SaveChanges();
                }
                return RedirectToAction("Grid");
            }

            return View(note);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var obj = _db.Notes.Find(id);

            if (obj == null)
                return NotFound();
            else
            {
                _db.Notes.Remove(obj);
                _db.SaveChanges();
            }

            return RedirectToAction("Grid");
        }
    }
}
