namespace MVC.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MVC.Data;
    using MVC.Models.ThongTin;

    public class ThongTinController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public ThongTinController(ApplicationDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var SinhVienS = await _context.SinhViens.ToListAsync();
            return View(SinhVienS);
        }
        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MSV,HoTen,Khoa")] Sv sv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sv);
        }
    }
}