﻿using DataFut.Data;
using DataFut.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataFut.Controllers
{
    public class ClubesController : Controller
    {
        private readonly DataFutDbContext _context;

        public ClubesController(DataFutDbContext context)
        {
            _context = context;
        }

        // GET: /Clubes
        public async Task<IActionResult> Index()
        {
            var clubes = await _context.Clubes
                .Where(c => c.IsAtivo)
                .OrderBy(c => c.Nome)
                .ToListAsync();

            return View(clubes);
        }

        // GET: /Clubes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var clube = await _context.Clubes
                .Include(c => c.Jogadores)
                    .ThenInclude(j => j.Posicao)
                .Include(c => c.TransferenciasOrigem)
                    .ThenInclude(t => t.Jogador)
                .Include(c => c.TransferenciasDestino)
                    .ThenInclude(t => t.Jogador)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clube == null) return NotFound();
            return View(clube);
        }

        // GET: /Clubes/Create
        public IActionResult Create() => View();

        // POST: /Clubes/Create
        [Authorize(Roles = "Gestor de Clube")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nome,Sigla,Cidade,EscudoUrl,IsAmador,IsAtivo")] Clube clube)
        {
            if (!ModelState.IsValid) return View(clube);

            _context.Add(clube);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Clubes/Edit/5
        [Authorize(Roles = "Gestor de Clube")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var clube = await _context.Clubes.FindAsync(id);
            if (clube == null) return NotFound();
            return View(clube);
        }

        // POST: /Clubes/Edit/5
        [Authorize(Roles = "Gestor de Clube")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, [Bind("Id,Nome,Sigla,Cidade,EscudoUrl,IsAmador,IsAtivo")] Clube clube)
        {
            if (id != clube.Id) return NotFound();
            if (!ModelState.IsValid) return View(clube);

            try
            {
                _context.Update(clube);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clubes.Any(c => c.Id == id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Clubes/Delete/5
        [Authorize(Roles = "Gestor de Clube")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var clube = await _context.Clubes
                .Include(c => c.Jogadores)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clube == null) return NotFound();
            return View(clube);
        }

        // POST: /Clubes/Delete/5
        [Authorize(Roles = "Gestor de Clube")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clube = await _context.Clubes
                .Include(c => c.Jogadores)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clube == null) return NotFound();

            // Jogadores ficam sem clube (SetNull definido no DbContext)
            foreach (var jogador in clube.Jogadores)
                jogador.ClubeAtualId = null;

            _context.Clubes.Remove(clube);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}