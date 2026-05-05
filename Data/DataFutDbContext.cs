using DataFut.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataFut.Data
{
    public class DataFutDbContext : DbContext
    {
        public DataFutDbContext(DbContextOptions<DataFutDbContext> options) : base(options)
        {
        }
        public DbSet<Clube> Clubes { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Posicao> Posicoes { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuração da relação Muitos-para-Muitos com entidade explícita
            modelBuilder.Entity<Transferencia>()
                .HasKey(t => new { t.JogadorId, t.ClubeDestinoId, t.DataTransferencia, t.ClubeOrigemId }); // Chave composta para histórico

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.Jogador)
                .WithMany(j => j.Id)
                .HasForeignKey(t => t.JogadorId);

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ClubeDestino)
                .WithOne(c => c.Jogador)
                .HasForeignKey(t => t.ClubeDestinoId);


        }
    }
}
