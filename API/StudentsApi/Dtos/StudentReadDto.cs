namespace StudentsApi.Dtos
{
    public class StudentReadDto
    {     
        public int id { get; set; }
        public string? RA { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }
   
        public string? CPF { get; set; }
    }
}