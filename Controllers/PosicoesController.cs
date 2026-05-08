using DataFut.Models.Entities;
using DataFut.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataFut.Controllers
{
    public class PosicoesController : Controller
    {
        private readonly DataFutDbContext _context;

        public PosicoesController(DataFutDbContext context)
        {
            _context = context;
        }

        // GET: /Posicoes
        public async Task<IActionResult> Index()
        {
            var posicoes = await _context.Posicoes
                .OrderBy(p => p.Nome)
                .ToListAsync();

            return View(posicoes);
        }

        // GET: /Posicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var posicao = await _context.Posicoes
                .Include(p => p.Jogadores)
                    .ThenInclude(j => j.ClubeAtual)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (posicao == null) return NotFound();
            return View(posicao);
        }

        // GET: /Posicoes/Create
        public IActionResult Create() => View();

        // POST: /Posicoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Posicao posicao)
        {
            if (!ModelState.IsValid) return View(posicao);

            _context.Add(posicao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Posicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var posicao = await _context.Posicoes.FindAsync(id);
            if (posicao == null) return NotFound();
            return View(posicao);
        }

        // POST: /Posicoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Posicao posicao)
        {
            if (id != posicao.Id) return NotFound();
            if (!ModelState.IsValid) return View(posicao);

            try
            {
                _context.Update(posicao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Posicoes.Any(p => p.Id == id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Posicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var posicao = await _context.Posicoes
                .Include(p => p.Jogadores)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (posicao == null) return NotFound();
            return View(posicao);
        }

        // POST: /Posicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posicao = await _context.Posicoes
                .Include(p => p.Jogadores)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (posicao == null) return NotFound();

            // Impede apagar se tiver jogadores associados
            if (posicao.Jogadores.Any())
            {
                ModelState.AddModelError(string.Empty,
                    "Não é possível apagar esta posição pois tem jogadores associados.");
                return View(posicao);
            }

            _context.Posicoes.Remove(posicao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
