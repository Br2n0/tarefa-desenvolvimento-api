using System;

namespace ApiLocadora.Models
{
    public class Filme
    {
        // Propriedade de identificação única para o filme
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Título do filme
        public required string Titulo { get; set; }
        
        // Ano de lançamento do filme
        public int AnoLancamento { get; set; }
        
        // Diretor do filme
        public required string Diretor { get; set; }
        
        // Relação com a classe Gênero
        public Guid GeneroId { get; set; }
        public Genero? Genero { get; set; }
        
        // Relação com a classe Estúdio
        public Guid EstudioId { get; set; }
        public Estudio? Estudio { get; set; }
        
        // Avaliação do IMDB (ex: 8.2)
        public decimal AvaliacaoIMDB { get; set; }
    }
} 