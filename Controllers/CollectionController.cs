using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNotes.Data;
using WebNotes.Models;
using WebNotes.Models.Notes;

namespace WebNotes.Controllers;

[Authorize]
public class CollectionController : Controller
{
    private readonly NotesDbContext _db;
    public CollectionController(NotesDbContext db)  
    {  
        _db = db;  
    }

    [HttpGet]
    public async Task<IActionResult> Grid(CancellationToken cancellationToken = default)
    {
        var notes = await _db.Notes
            .OrderByDescending(x => x.CreatedDate)
            .Where(x => x.User.Login == HttpContext.User.Identity.Name)
            .ToArrayAsync(cancellationToken);

        var result = new VMNotes {
            notes = notes,
            message = null
        };

        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Grid(VMNotes note, CancellationToken cancellationToken = default)
    {
        var notes = await _db.Notes
            .OrderByDescending(x => x.CreatedDate)
            .Where(x => x.User.Login == HttpContext.User.Identity.Name)
            .ToArrayAsync(cancellationToken);

        var result = new VMNotes {
            notes = notes,
            message = "Уверены, что хотите выйти?"
        };

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id, CancellationToken cancellationToken = default)
    {
       var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Login == HttpContext.User.Identity.Name, cancellationToken);
            
        var obj = new Note { 
            User =  user
        };

        if (id == null)
            return View(obj);
        else
        {
            if (id == 0)
                return NotFound();

            obj = await _db.Notes
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (obj == null)
                return NotFound();

            return View(obj);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(Note note, CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Login.Equals(HttpContext.User.Identity.Name), cancellationToken);

        note.User = user;

        //if (!ModelState.IsValid) return View(note);
        
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
        
        await _db.SaveChangesAsync(cancellationToken);
        
        return RedirectToAction("Grid");

    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var obj = await _db.Notes.FindAsync(id, cancellationToken);

        if (obj == null)
            return NotFound();
        else
        {
            _db.Notes.Remove(obj);
            await _db.SaveChangesAsync(cancellationToken);
        }

        return RedirectToAction("Grid");
    }

    public async Task<IActionResult> Exit()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Main", "Authorization");
    }
}