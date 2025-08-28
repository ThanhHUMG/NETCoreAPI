namespace MVC.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using MVC.Models;
    public class Bai3Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            ViewData["Message"] = "Your welcome message";

            return View();
        }
        [HttpPost]
        public IActionResult Index(Bai3 b3)
        {
            b3.BMI = b3.CanNang / b3.ChieuCao * b3.ChieuCao;
            
        }
    }
}