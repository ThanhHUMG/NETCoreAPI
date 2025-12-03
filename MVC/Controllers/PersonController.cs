using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.Models.Process;
using X.PagedList;
using X.PagedList.Extensions;

namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            var list = _context.Employees.ToList().ToPagedList(page ?? 1, 5);
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee person)
        {
            if (ModelState.IsValid)
            {
                await _context.Employees.AddAsync(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var person = await _context.Employees.FindAsync(id);
            if (person == null) return NotFound();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee person)
        {
            if (id != person.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null) return NotFound();
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Employees.FindAsync(id);
            if (person != null)
            {
                _context.Employees.Remove(person);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Upload() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
            {
                TempData["msg"] = "Choose a file!";
                return View();
            }

            var extension = Path.GetExtension(file.FileName);
            if (extension != ".xls" && extension != ".xlsx")
            {
                TempData["msg"] = "Only .xlsx or .xls allowed!";
                return View();
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Excels");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string filePath = Path.Combine(uploadPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var dt = _excelProcess.ExcelToDataTable(filePath);

            foreach (System.Data.DataRow row in dt.Rows)
            {
                var emp = new Employee()
                {
                    FullName = row[0].ToString(),
                    NamSinh = int.Parse(row[1].ToString()),
                    DiaChi = row[2].ToString(),
                    ChuyenNganh = row[3].ToString(),
                    Truong = row[4].ToString()
                };
                await _context.Employees.AddAsync(emp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download()
        {
            var employees = await _context.Employees.ToListAsync();
            using var package = new OfficeOpenXml.ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Employees");

            string[] headers = { "Full Name", "Năm Sinh", "Địa Chỉ", "Chuyên Ngành", "Trường" };
            for (int i = 0; i < headers.Length; i++)
                ws.Cells[1, i + 1].Value = headers[i];

            for (int i = 0; i < employees.Count; i++)
            {
                ws.Cells[i + 2, 1].Value = employees[i].FullName;
                ws.Cells[i + 2, 2].Value = employees[i].NamSinh;
                ws.Cells[i + 2, 3].Value = employees[i].DiaChi;
                ws.Cells[i + 2, 4].Value = employees[i].ChuyenNganh;
                ws.Cells[i + 2, 5].Value = employees[i].Truong;
            }

            byte[] file = package.GetAsByteArray();
            return File(file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Employees_{DateTime.Now:yyyyMMddHHmm}.xlsx");
        }
    }
}
