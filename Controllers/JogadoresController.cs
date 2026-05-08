using System.Linq;
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
        public async Task<IActionResult> Create(Jogador jogador, int[] posicoesSelecionadas)
        {
            if (ModelState.IsValid)
            {
                // Validação: como Jogador.Posicao é uma única entidade (não coleção),
                // verificamos igualdade pelo Id em vez de usar Any(...)
                foreach (var posicaoId in posicoesSelecionadas)
                {
                    var contagem = await _context.Jogadores
                        .Where(j => j.ClubeId == jogador.ClubeId)
                        .Where(j => j.Posicao != null && j.Posicao.Id == posicaoId)
                        .CountAsync();

                    if (contagem >= 5)
                    {
                        var posicaoNome = _context.Posicoes
                            .FirstOrDefault(p => p.Id == posicaoId)?.Nome ?? "selecionada";

                        ModelState.AddModelError("Posicoes", $"Limite atingido: O clube já tem 5 jogadores na posição {posicaoNome}.");
                        return View(jogador);
                    }
                }

                // Associa a primeira posição selecionada ao jogador (modelo atual tem uma única Posicao)
                if (posicoesSelecionadas != null && posicoesSelecionadas.Length > 0)
                {
                    var primeiraId = posicoesSelecionadas[0];
                    var p = await _context.Posicoes.FindAsync(primeiraId);
                    if (p != null) jogador.Posicao = p;
                }

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
                    await _context.Clubes.OrderBy(c => c.Nome).ToListAsync(),
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