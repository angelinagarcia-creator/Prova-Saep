using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROVASAEP.Data;
using PROVASAEP.Models;
using System.Threading.Tasks;

public class UsuarioController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Usuario
    public async Task<IActionResult> Index()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return View(usuarios);
    }

    // GET: Usuario/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
            return NotFound();

        return View(usuario);
    }

    // GET: Usuario/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Usuario/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Usuario usuario)
    {
        if (!ModelState.IsValid)
            return View(usuario);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Usuario/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound();

        return View(usuario);
    }

    // POST: Usuario/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Usuario usuario)
    {
        if (id != usuario.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(usuario);

        _context.Update(usuario);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Usuario/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
            return NotFound();

        return View(usuario);
    }

    // POST: Usuario/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
