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
            var notes = _db.Notes.OrderByDescending(x => x.CreatedDate).Where(x => x.UserId == WC.Id);

            return View(notes);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var obj = new Note();

            if (id == null)
                return View(obj);
            else
            {
                if (id == 0)
                    return NotFound();

                obj = _db.Notes.Find(id);

                if (obj == null ) 
                    return NotFound();

                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Note note)
        {
            if (ModelState.IsValid)
            {
                if (note.Id == 0) 
                {
                    note.CreatedDate= DateTime.Now;
                    note.UserId= WC.Id;
                    _db.Notes.Add(note);
                }
                else
                {
                    note.CreatedDate = DateTime.Now;
                    _db.Notes.Update(note);
                }
                _db.SaveChanges();
                return RedirectToAction("Grid");
            }

            return View(note);
        }

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
