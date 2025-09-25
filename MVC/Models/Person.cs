using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVC.Models
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public int Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public int NamSinh { get; set; } = default!;
        public string DiaChi { get; set; } = default!;
    }
    public class Employee : Person
    {
        public string ChuyenNganh { get; set; } = default!;
        public string Truong { get; set; } = default!;
    }
}