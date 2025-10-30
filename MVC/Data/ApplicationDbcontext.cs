using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Models.ThongTin;
namespace MVC.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SinhVien> SinhVien { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}