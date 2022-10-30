using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;

namespace StudentsApi.Data
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AppDbContext _context;

        public StudentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateStudent(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            await _context.AddAsync(student);
        }

        public void DeleteStudent(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            _context.Students.Remove(student);
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentByRA(string? ra)
        {
            return await _context.Students.FirstOrDefaultAsync(c => c.RA == ra);
        }
        
        public async Task<IEnumerable<Student>> GetStudentByValue(string value)
        {
            return await _context.Students.Where(p => 
                p.CPF!.ToLower().Contains(value) || 
                p.Email!.ToLower().Contains(value) ||
                p.Nome!.ToLower().Contains(value) ||
                p.RA!.ToLower().Contains(value)
                ).ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}