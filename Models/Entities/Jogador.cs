namespace DataFut.Models.Entities
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; } = string.Empty;
        public int ClubeId { get; set; }

        // Relacionamento com Clube
        public ICollection<Clube> Clubes { get; set; } = new List<Clube>();

        // Relacionamento com Posicao
        public List<Posicao> Posicoes { get; set; } = new List<Posicao>();
    }
}
