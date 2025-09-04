using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbcontext _context;
        public PersonController(ApplicationDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() {
            var model = await _context.Person.ToListAsync();
            return View(model);
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,NamSinh")] Person person)
        public async Task<IActionResult> Edit(string id)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FullName,NamSinh")] Person person)
        public async Task<IActionResult> Delete(string id)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken
        ]
        public async Task<IActionResult> DeleteConfirmed(string id)
        private bool PersonExists(string id)
    }
}