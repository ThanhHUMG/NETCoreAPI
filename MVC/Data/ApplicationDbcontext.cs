using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Models.ThongTin;
namespace MVC.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options) { }
        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sv> SinhViens { get; set; }
    }
}