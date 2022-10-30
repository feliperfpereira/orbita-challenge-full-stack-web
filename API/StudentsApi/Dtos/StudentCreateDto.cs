using System.ComponentModel.DataAnnotations;
using StudentsApi.CustomValidation;

namespace StudentsApi.Dtos
{
    public class StudentCreateDto
    {
        [Required]
        public string? RA { get; set; }
        [Required]
        public string? Nome { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [CustomValidation.CPF]
        public string? CPF { get; set; }
    }
}