using Microsoft.EntityFrameworkCore;
using ApiLocadora.Models;
// Referência ao namespace antigo para compatibilidade
// using api_locadora.Models;

namespace ApiLocadora.dataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets para as entidades do projeto
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Estudio> Estudios { get; set; }

        // Código antigo comentado
        // Defina seus DbSets aqui
        // public DbSet<Filmes> Filmes { get; set; }
        // Exemplo: public DbSet<Modelo> Modelos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed inicial de dados
            SeedData(modelBuilder);
        }

        // Método para popular dados iniciais no banco de dados
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed para Gêneros
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = Guid.NewGuid(), Nome = "Ação" },
                new Genero { Id = Guid.NewGuid(), Nome = "Comédia" },
                new Genero { Id = Guid.NewGuid(), Nome = "Drama" },
                new Genero { Id = Guid.NewGuid(), Nome = "Ficção Científica" },
                new Genero { Id = Guid.NewGuid(), Nome = "Terror" }
            );

            // Seed para Estúdios
            modelBuilder.Entity<Estudio>().HasData(
                new Estudio { Id = Guid.NewGuid(), Nome = "Warner Bros", Distribuidor = "Warner Media" },
                new Estudio { Id = Guid.NewGuid(), Nome = "Universal Pictures", Distribuidor = "Comcast" },
                new Estudio { Id = Guid.NewGuid(), Nome = "Paramount Pictures", Distribuidor = "ViacomCBS" },
                new Estudio { Id = Guid.NewGuid(), Nome = "20th Century Studios", Distribuidor = "Disney" }
            );

            // Não é possível seed de Filmes com relacionamentos facilmente sem IDs conhecidos
            // Os filmes serão adicionados via migração ou pelo aplicativo
        }
    }
}