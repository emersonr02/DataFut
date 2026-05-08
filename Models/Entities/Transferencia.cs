using DataFut.Models.Entities;

namespace DataFut.Models.Entities
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int JogadorId { get; set; }
        public int ClubeOrigemId { get; set; } = 0;
        public int ClubeDestinoId { get; set; }
        public DateTime DataTransferencia { get; set; }
        public decimal Valor { get; set; }
        // Relacionamentos
        public Jogador Jogador { get; set; }
        public Clube ClubeOrigem { get; set; }
        public Clube ClubeDestino { get; set; }

    }
}
