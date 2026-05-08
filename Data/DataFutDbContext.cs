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

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ClubeOrigem)
                .WithMany(c => c.TransferenciasOrigem)
                .HasForeignKey(t => t.ClubeOrigemId)
                .OnDelete(DeleteBehavior.Restrict); // Evita conflitos de cascata

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ClubeDestino)
                .WithMany(c => c.TransferenciasDestino)
                .HasForeignKey(t => t.ClubeDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
