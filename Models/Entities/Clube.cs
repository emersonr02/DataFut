namespace DataFut.Models.Entities
{
    public class Clube
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Cidade { get; set; }
        public string EscudoUrl { get; set; }
        public bool IsAmador { get; set; }
        public bool IsAtivo { get; set; }
        // Relacionamento com Jogador
        public ICollection<Jogador> Jogadores { get; set; } = new List<Jogador>();
    }
}
