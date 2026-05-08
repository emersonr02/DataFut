using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DataFut.ViewModels
{
    public class JogadorContratacaoViewModel
    {
        
        // Dados básicos do jogador
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(15, 45, ErrorMessage = "A idade deve estar entre 15 e 45 anos.")]
        public int Idade { get; set; }

        // IDs selecionados pelo utilizador
        [Required(ErrorMessage ="Selecione um clube.")]
        public int ClubeId { get; set; }

        [Required(ErrorMessage = "Selecione uma posição.")]
        public int PosicaoId { get; set; }

        // Listas para os <select> na view
        public SelectList? Clubes { get; set; }
        public SelectList? Posicoes { get; set; }
    }
}
