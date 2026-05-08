using DataFut.Models.Entities;
using DataFut.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataFut.Data
{
    public class DataFutDbContext : IdentityDbContext
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

            // Jogador -> Posicao (N:1)
            modelBuilder.Entity<Jogador>()
                .HasOne(j => j.Posicao)
                .WithMany(p => p.Jogadores)
                .HasForeignKey(j => j.PosicaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Jogador -> ClubeAtual (N:1, opcional)
            modelBuilder.Entity<Jogador>()
                .HasOne(j => j.ClubeAtual)
                .WithMany(c => c.Jogadores)
                .HasForeignKey(j => j.ClubeAtualId)
                .OnDelete(DeleteBehavior.SetNull);

            // Transferencia -> ClubeOrigem
            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ClubeOrigem)
                .WithMany(c => c.TransferenciasOrigem)
                .HasForeignKey(t => t.ClubeOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transferencia -> ClubeDestino
            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ClubeDestino)
                .WithMany(c => c.TransferenciasDestino)
                .HasForeignKey(t => t.ClubeDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}