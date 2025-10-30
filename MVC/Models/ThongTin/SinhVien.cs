using System.ComponentModel.DataAnnotations;

namespace MVC.Models.ThongTin
{
    public class SinhVien
    {
        [Key]
        public double MSV { get; set; } = default!;
        public string HoTen { get; set; } = default!;
        public string Khoa { get; set; } = default!;
    }
}