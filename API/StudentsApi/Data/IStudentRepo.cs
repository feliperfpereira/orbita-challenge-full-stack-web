using StudentsApi.Models;

namespace StudentsApi.Data
{
    public interface IStudentRepo
    {
        Task SaveChanges();
        Task<Student?> GetStudentByRA(int id);
        Task<IEnumerable<Student>> GetAllStudents();
        Task CreateStudent(Student student);
        void DeleteStudent(Student student);
    }
}