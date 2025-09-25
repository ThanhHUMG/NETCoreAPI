using System.ComponentModel.DataAnnotations;

namespace MVC.Models.ThongTin
{
    public class Sv
    {
        [Key]
        public double MSV { get; set; } = default!;
        public string HoTen { get; set; } = default!;
        public string Khoa { get; set; } = default!;
    }
}