namespace DataFut.Models.Entities
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public int ClubeId { get; set; }

        // Relacionamento com Clube
        public List<Clube> Clubes { get; set; }

        // Relacionamento com Posicao
        public List<Posicao> Posicoes { get; set; }
    }
}
