using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DataFut.Data;
using DataFut.Models.Entities;
using DataFut.ViewModels;

namespace DataFut.Controllers
{
    public class JogadoresController : Controller
    {
        private readonly DataFutDbContext _context;

        public JogadoresController(DataFutDbContext context)
        {
            _context = context;
        }

        // GET: Jogadores
        //aqui so leitura, qualquer utilizador pode aceder 
        public async Task<IActionResult> Index()
        {
            var jogadores = await _context.Jogadores
                .Include(j => j.ClubeAtual)
                .Include(j => j.Posicao)
                .ToListAsync();
            return View(jogadores);
        }

        // GET: Jogadores/Contratar
        // Apenas utilizadores com o perfil "Gestor de Clube" podem aceder 
        [Authorize(Roles = "Gestor de Clube")]
        public async Task<IActionResult> Contratar()
        {
            var model = new JogadorContratacaoViewModel();
            await PrepararListasViewModel(model);
            return View(model);
        }

        // POST: Jogadores/Contratar
        [HttpPost]
        [Authorize(Roles = "Gestor de Clube")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contratar(JogadorContratacaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Validação da Lógica de Negócio: Limite de 5 jogadores por posição no clube 
                var contagem = await _context.Jogadores
                    .CountAsync(j => j.ClubeAtualId == model.ClubeId && j.PosicaoId == model.PosicaoId);

                if (contagem >= 5)
                {
                    var posicaoNome = (await _context.Posicoes.FindAsync(model.PosicaoId))?.Nome ?? "esta posição";

                    // Apresenta mensagem de erro apropriada conforme o requisito 
                    ModelState.AddModelError(string.Empty, $"Limite atingido: O clube já possui 5 jogadores na posição {posicaoNome}.");

                    await PrepararListasViewModel(model);
                    return View(model);
                }

                // 2. Mapeamento para a Entidade de Domínio 
                var novoJogador = new Jogador
                {
                    Nome = model.Nome,
                    Apelido = model.Apelido,
                    DataNascimento = model.DataNascimento,
                    Nacionalidade = model.Nacionalidade,
                    PosicaoId = model.PosicaoId,
                    ClubeAtualId = model.ClubeId
                };

                // 3. Persistência e Registo de Histórico 
                _context.Jogadores.Add(novoJogador);
                await _context.SaveChangesAsync(); // Gera o ID do jogador

                var transferencia = new Transferencia
                {
                    JogadorId = novoJogador.Id,
                    ClubeDestinoId = model.ClubeId,
                    DataTransferencia = DateTime.Now
                };

                _context.Transferencias.Add(transferencia);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Se o modelo for inválido, recarrega as listas de seleção 
            await PrepararListasViewModel(model);
            return View(model);
        }

        // SelectLists exigidas
        private async Task PrepararListasViewModel(JogadorContratacaoViewModel model)
        {
            model.Clubes = new SelectList(
                await _context.Clubes.OrderBy(c => c.Nome).ToListAsync(),
                "Id",
                "Nome"
            );

            model.Posicoes = new SelectList(
                await _context.Posicoes.OrderBy(p => p.Nome).ToListAsync(),
                "Id",
                "Nome"
            );
        }
    }
}