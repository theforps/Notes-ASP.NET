using System.ComponentModel.DataAnnotations;

namespace WebNotes.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string DateOfCreate { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
