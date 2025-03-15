using Microsoft.EntityFrameworkCore;
using WebNotes.Models;
using WebNotes.Models.User;

namespace WebNotes.Data;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options): base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }
}