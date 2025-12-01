using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROVASAEP.Data;
using PROVASAEP.Models;

namespace PROVASAEP.Controllers
{
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Material
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materials.ToListAsync());
        }

        // GET: Material/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);
            if (material == null) return NotFound();

            return View(material);
        }

        // GET: Material/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Material/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material material)
        {
            if (!ModelState.IsValid)
                return View(material);

            _context.Add(material);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Material/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Materials.FindAsync(id);
            if (material == null) return NotFound();

            return View(material);
        }

        // POST: Material/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material material)
        {
            if (id != material.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(material);

            _context.Update(material);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Material/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);
            if (material == null) return NotFound();

            return View(material);
        }

        // POST: Material/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
