namespace DataFut.Models.Entities
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; } = string.Empty;

        public int PosicaoId { get; set; }
        public virtual Posicao Posicao { get; set; } = null!;

        public int? ClubeId { get; set; }
        public virtual Clube? ClubeAtual { get; set; }

        public virtual ICollection<Transferencia> Transferencias { get; set; } = new List<Transferencia>();
    }
}

