using Microsoft.EntityFrameworkCore;
using VestCareApi.Models;

namespace VestCareApi.Data
{
    public class VestCareContext : DbContext
    {
        public VestCareContext(DbContextOptions<VestCareContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Traje> Trajes { get; set; }
        public DbSet<TrajePeca> TrajePecas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.Nome).HasColumnName("nome");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Senha).HasColumnName("senha");
                entity.Property(e => e.DataCadastro).HasColumnName("data_cadastro");
            });

            modelBuilder.Entity<Peca>(entity =>
            {
                entity.ToTable("PECA");

                entity.HasKey(e => e.IdPeca);

                entity.Property(e => e.IdPeca).HasColumnName("id_peca");
                entity.Property(e => e.NomePeca).HasColumnName("nome_peca");
                entity.Property(e => e.Ocasiao).HasColumnName("ocasiao");
                entity.Property(e => e.Categoria).HasColumnName("categoria");
                entity.Property(e => e.Cor).HasColumnName("cor");
                entity.Property(e => e.Estilo).HasColumnName("estilo");
                entity.Property(e => e.Clima).HasColumnName("clima");
                entity.Property(e => e.UrlFoto).HasColumnName("url_foto");
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Pecas)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Traje>(entity =>
            {
                entity.ToTable("TRAJE");

                entity.HasKey(e => e.IdTraje);

                entity.Property(e => e.IdTraje).HasColumnName("id_traje");
                entity.Property(e => e.NomeTraje).HasColumnName("nome_traje");
                entity.Property(e => e.OcasiaoDestino).HasColumnName("ocasiao_destino");
                entity.Property(e => e.ClimaDestino).HasColumnName("clima_destino");
                entity.Property(e => e.Favorito).HasColumnName("favorito");
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Trajes)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TrajePeca>(entity =>
            {
                entity.ToTable("TRAJE_PECA");

                entity.HasKey(e => new { e.IdTraje, e.IdPeca });

                entity.Property(e => e.IdTraje).HasColumnName("id_traje");
                entity.Property(e => e.IdPeca).HasColumnName("id_peca");

                entity.HasOne(e => e.Traje)
                    .WithMany(t => t.TrajePecas)
                    .HasForeignKey(e => e.IdTraje)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Peca)
                    .WithMany(p => p.TrajePecas)
                    .HasForeignKey(e => e.IdPeca)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}