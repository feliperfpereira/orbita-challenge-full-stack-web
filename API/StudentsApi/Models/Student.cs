using System.ComponentModel.DataAnnotations;

namespace StudentsApi.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string? RA { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? CPF { get; set; }
    }
}