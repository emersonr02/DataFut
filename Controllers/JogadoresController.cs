using Microsoft.AspNetCore.Mvc;
using DataFut.Data;
using DataFut.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataFut.Controllers
{
    public class JogadoresController : Controller
    {
        private readonly DataFutDbContext _context;

        public JogadoresController(DataFutDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Jogador jogador, int[] posicoesSelecionadas)
        {
            if (ModelState.IsValid)
            {
                foreach (var posicaoId in posicoesSelecionadas)
                {
                    // Contamos quantos jogadores já existem no mesmo clube que tenham esta posição
                    var contagem = await _context.Jogadores
                        .Where(j => j.ClubeId == jogador.ClubeId)
                        .Where(j => j.Posicoes.Any(p => p.Id == posicaoId))
                        .CountAsync();

                    if (contagem >= 5)
                    {
                        // Se atingir 5, adicionamos um erro e paramos o processo
                        var posicaoNome = _context.Posicoes
                            .FirstOrDefault(p => p.Id == posicaoId)?.Nome ?? "selecionada";

                        ModelState.AddModelError("Posicoes", $"Limite atingido: O clube já tem 5 jogadores na posição {posicaoNome}.");
                        return View(jogador);
                    }
                }

                // Se passou na validação acima, associamos as posições ao jogador
                foreach (var id in posicoesSelecionadas)
                {
                    var p = await _context.Posicoes.FindAsync(id);
                    if (p != null) jogador.Posicoes.Add(p);
                }

                _context.Add(jogador);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(jogador);
        }
    }
}