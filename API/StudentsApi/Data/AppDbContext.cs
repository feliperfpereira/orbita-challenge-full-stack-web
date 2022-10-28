using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;
namespace StudentsApi.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();

}
