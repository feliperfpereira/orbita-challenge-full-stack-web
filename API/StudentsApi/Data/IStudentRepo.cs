using StudentsApi.Models;

namespace StudentsApi.Data
{
    public interface IStudentRepo
    {
        Task SaveChanges();
        Task<Student?> GetStudentByRA(string ra);
        Task<Student?> GetStudentById(int id);
        Task<IEnumerable<Student>> GetStudentByValue(string value);
        Task<IEnumerable<Student>> GetAllStudents();
        Task CreateStudent(Student student);
        void DeleteStudent(Student student);
    }
}