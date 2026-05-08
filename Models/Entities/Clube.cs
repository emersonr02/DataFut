namespace DataFut.Models.Entities
{
    public class Clube
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string EscudoUrl { get; set; } = string.Empty;
        public bool IsAmador { get; set; } 
        public bool IsAtivo { get; set; }
        // Relacionamento com Jogador
        public virtual ICollection<Jogador> Jogadores { get; set; } = new List<Jogador>();
        public virtual ICollection<Transferencia> TransferenciasOrigem { get; set; } = new List<Transferencia>();
        public virtual ICollection<Transferencia> TransferenciasDestino { get; set; } = new List<Transferencia>();
    }
}
