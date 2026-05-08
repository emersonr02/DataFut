using Microsoft.AspNetCore.Mvc;
using DataFut.Data;
using DataFut.Models.Entities;
using Microsoft.EntityFrameworkCore;
using DataFut.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Create(Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                // Contamos quantos jogadores já existem no clube selecionado (ClubeAtualId) que possuem a mesma posição selecionada (PosicaoId)
                var contagem = await _context.Jogadores
                    .Where(j => j.ClubeAtualId == jogador.ClubeAtualId)
                    .Where(j => j.PosicaoId == jogador.PosicaoId)
                    .CountAsync();

                if (contagem >= 5)
                {
                    var posicaoNome = await _context.Posicoes
                        .Where(p => p.Id == jogador.PosicaoId)
                        .Select(p => p.Nome)
                        .FirstOrDefaultAsync() ?? "esta posição";

                    ModelState.AddModelError("", $"Limite atingido: O clube já possui 5 jogadores para a posição {posicaoNome}.");

                    // Retorna a View com os dados preenchidos e a mensagem de erro
                    return View(jogador);
                }

                // Se passou na validação, adiciona o jogador
                _context.Add(jogador);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(jogador);
        }

        [HttpGet]
        public async Task<IActionResult> Contratar()
        {
            var viewModel = new JogadorContratacaoViewModel
            {
                // Popular as SelectLists diretamente a partir do DB
                Clubes = new SelectList(
                    await _context.Clubes.OrderBy(c =>c.Nome).ToListAsync(),
                    "Id",
                    "Nome"
                 ),
                Posicoes = new SelectList(
                    await _context.Posicoes.OrderBy(p => p.Nome).ToListAsync(),
                    "Id",
                    "Nome"
        )
            };

            return View(viewModel);
        }
    }
}