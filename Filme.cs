// ARQUIVO OBSOLETO
// Este arquivo foi substituído pelo Models/Filme.cs
// Mantido apenas para referência e compatibilidade

using System;
using ApiLocadora.Models;

namespace ApiLocadora
{
    // Classe obsoleta. Use ApiLocadora.Models.Filme em vez disso.
    /*
    public class Filme
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Título do filme
        public required string Titulo { get; set; }
        
        // Ano de lançamento do filme
        public int AnoLancamento { get; set; }
        
        // Diretor do filme
        public required string Diretor { get; set; }
        
        // Relação com a classe Gênero
        public Guid GeneroId { get; set; }
        public Genero? GeneroObj { get; set; }
        
        // Relação com a classe Estúdio
        public Guid EstudioId { get; set; }
        public Estudio? EstudioObj { get; set; }
        
        // Avaliação do IMDB (ex: 8.2)
        public decimal AvaliacaoIMDB { get; set; }
        
        // Propriedades legadas para compatibilidade com código existente
        public string Nome { 
            get => Titulo; 
            set => Titulo = value; 
        }
        
        // Propriedade legada para compatibilidade - agora apenas string
        public string Genero { 
            get => this.GeneroObj?.Nome ?? string.Empty; 
            set {} 
        }
    }
    */
    
    // Classe de compatibilidade que redireciona para o modelo correto
    public class Filme : ApiLocadora.Models.Filme
    {
        // Esta classe herda todas as propriedades de ApiLocadora.Models.Filme
        // e adiciona propriedades de compatibilidade

        public Genero? GeneroObj 
        { 
            get => this.Genero; 
            set => this.Genero = value; 
        }
        
        public Estudio? EstudioObj 
        { 
            get => this.Estudio; 
            set => this.Estudio = value; 
        }
    }
}
