using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DataFut.ViewModels
{
    public class JogadorContratacaoViewModel
    {
        
        // Dados básicos do jogador
        public int Id { get; set; }

        [Required] public string Nome { get; set; }
        [Required] public string Apelido { get; set; }
        [Required] public DateTime DataNascimento { get; set; }
        [Required] public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "Selecione uma posição.")]
        public int PosicaoId { get; set; }

        // Listas para os <select> na view
        public SelectList? Clubes { get; set; }
        public SelectList? Posicoes { get; set; }
    }
}
