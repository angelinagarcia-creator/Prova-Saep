using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROVASAEP.Data;
using PROVASAEP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PROVASAEP.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int EstoqueMinimo = 5;

        public MovimentacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = _context.Movimentacoes
                .Include(m => m.Material)
                .OrderByDescending(m => m.DataMovimentacao);

            return View(await lista.ToListAsync());
        }

        // GET: Movimentacao/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(
                _context.Materials.OrderBy(m => m.Nome),
                "Id", "Nome"
            );

            return View();
        }

        // POST: Movimentacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movimentacao movimentacao)
        {
            ModelState.Remove("Material");

            if (!ModelState.IsValid)
            {
                ViewData["MaterialId"] = new SelectList(
                    _context.Materials, "Id", "Nome", movimentacao.MaterialId);
                return View(movimentacao);
            }

            var material = await _context.Materials.FindAsync(movimentacao.MaterialId);
            if (material == null)
                return NotFound();

            if (movimentacao.Tipo == "Saída")
            {
                if (material.QuantidadeEstoque < movimentacao.Quantidade)
                {
                    ModelState.AddModelError("", "Estoque insuficiente.");
                    return View(movimentacao);
                }

                material.QuantidadeEstoque -= movimentacao.Quantidade;
            }
            else if (movimentacao.Tipo == "Entrada")
            {
                material.QuantidadeEstoque += movimentacao.Quantidade;
            }

            _context.Add(movimentacao);
            _context.Update(material);
            await _context.SaveChangesAsync();

            if (material.QuantidadeEstoque <= EstoqueMinimo)
                TempData["Alerta"] = $"ALERTA: Estoque baixo de {material.Nome}.";

            return RedirectToAction(nameof(Index));
        }
    }
}
