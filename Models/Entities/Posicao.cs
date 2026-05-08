namespace DataFut.Models.Entities
{
    public class Posicao
    {
        public int Id { get; set; } 
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        // Relacionamento com Jogador
        public List<Jogador> Jogadores { get; set; } = new List<Jogador>();
    }
}
