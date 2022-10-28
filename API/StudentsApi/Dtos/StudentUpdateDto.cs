using System.ComponentModel.DataAnnotations;

namespace StudentsApi.Dtos
{
    public class StudentUpdateDto
    {
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
