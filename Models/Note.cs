using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotes.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [MaxLength(5000)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
