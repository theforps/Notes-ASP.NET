using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (WC.Id == null || WC.Id == 0)
                return RedirectToAction("Main","LoginScreen");

            var notes = _db.Notes.OrderByDescending(x => x.CreatedDate).Where(x => x.User.Id == WC.Id);

            return View(notes);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (WC.Id == null || WC.Id == 0)
                return RedirectToAction("Main", "LoginScreen");

            var obj = new Note { 
                User =  _db.Users.FirstOrDefault(x => x.Id == WC.Id)
            };

            if (id == null)
            {
                return View(obj);
            }
            else
            {
                if (id == 0)
                    return NotFound();

                obj = _db.Notes.Find(id);

                if (obj == null)
                    return NotFound();

                return View(obj);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Note note)
        {
            note.User = await _db.Users.FirstOrDefaultAsync(x => x.Id == WC.Id);

            //if (ModelState.IsValid)
            {
                if (note.Id == 0) 
                {
                    note.CreatedDate= DateTime.Now;
                    note.CountOfChanges += 1;
                    _db.Notes.Add(note);
                }
                else
                {
                    note.CreatedDate = DateTime.Now;
                    note.CountOfChanges += 1;
                    _db.Notes.Update(note);
                }
                _db.SaveChanges();
                return RedirectToAction("Grid");
            }

            //return View(note);
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

        public IActionResult Exit()
        {
            WC.Id = 0;

            return RedirectToAction("Main", "LoginScreen");
        }
    }
}
