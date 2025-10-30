using System.ComponentModel.DataAnnotations;

namespace MVC.Models.ThongTin;
public class Test{
    [Key]
    public int id { get; set; } = default;
    public int x{ get; set; } = default;
    public int y { get; set; } = default;
}