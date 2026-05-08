using DataFut.Models.Entities;

namespace DataFut.Models.Entities
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int JogadorId { get; set; }
        public int? ClubeOrigemId { get; set; }
        public int ClubeDestinoId { get; set; }
        public DateTime DataTransferencia { get; set; }
        public decimal Valor { get; set; }
        // Relacionamentos
        public virtual Jogador Jogador { get; set; } = null!;
        public virtual Clube? ClubeOrigem { get; set; }
        public virtual Clube ClubeDestino { get; set; } = null!;

    }
}
