namespace DataFut.Models.Entities
{
    public class Posicao
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string Sigla { get; set; }
        // Relacionamento com Jogador
        public List<Jogador> Jogadores { get; set; } = new List<Jogador>();
    }
}
