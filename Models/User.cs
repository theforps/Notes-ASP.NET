using System.ComponentModel.DataAnnotations;

namespace WebNotes.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfCreate { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
