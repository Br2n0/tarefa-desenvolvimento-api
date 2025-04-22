using System;
using System.Collections.Generic;
using ApiLocadora.Models;

namespace ApiLocadora.DbContext
{
    public static class AppDbContext
    {
        // Lista estática de gêneros
        public static List<Genero> Generos { get; set; } = new List<Genero>
        {
            new Genero { Nome = "Ação" },
            new Genero { Nome = "Comédia" },
            new Genero { Nome = "Drama" },
            new Genero { Nome = "Ficção Científica" },
            new Genero { Nome = "Terror" }
        };

        // Lista estática de estúdios
        public static List<Estudio> Estudios { get; set; } = new List<Estudio>
        {
            new Estudio { Nome = "Warner Bros", Distribuidor = "Warner Media" },
            new Estudio { Nome = "Universal Pictures", Distribuidor = "Comcast" },
            new Estudio { Nome = "Paramount Pictures", Distribuidor = "ViacomCBS" },
            new Estudio { Nome = "20th Century Studios", Distribuidor = "Disney" }
        };

        // Lista estática de filmes
        public static List<ApiLocadora.Filme> Filmes { get; set; } = new List<ApiLocadora.Filme>();

        // Método estático construtor para inicializar os filmes com relacionamentos
        static AppDbContext()
        {
            // Adicionando alguns filmes iniciais
            Filmes.Add(new ApiLocadora.Filme
            {
                Titulo = "Velozes e Furiosos",
                AnoLancamento = 2001,
                Diretor = "Rob Cohen",
                GeneroId = Generos[0].Id,  // Ação
                GeneroObj = Generos[0],
                EstudioId = Estudios[1].Id,  // Universal Pictures
                EstudioObj = Estudios[1],
                AvaliacaoIMDB = 6.8m
            });

            Filmes.Add(new ApiLocadora.Filme
            {
                Titulo = "O Senhor dos Anéis: A Sociedade do Anel",
                AnoLancamento = 2001,
                Diretor = "Peter Jackson",
                GeneroId = Generos[3].Id,  // Ficção Científica
                GeneroObj = Generos[3],
                EstudioId = Estudios[0].Id,  // Warner Bros
                EstudioObj = Estudios[0],
                AvaliacaoIMDB = 8.8m
            });

            Filmes.Add(new ApiLocadora.Filme
            {
                Titulo = "O Poderoso Chefão",
                AnoLancamento = 1972,
                Diretor = "Francis Ford Coppola",
                GeneroId = Generos[2].Id,  // Drama
                GeneroObj = Generos[2],
                EstudioId = Estudios[2].Id,  // Paramount Pictures
                EstudioObj = Estudios[2],
                AvaliacaoIMDB = 9.2m
            });
        }
    }
} 