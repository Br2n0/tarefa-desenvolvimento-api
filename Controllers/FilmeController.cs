using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using System;
using System.Linq;
using System.Collections.Generic;
using ApiLocadora.Models;
using ApiLocadora.DbContext;

namespace ApiLocadora.Controllers
{
    [Route("filmes")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        // GET: filmes
        [HttpGet]
        public IActionResult Buscar()
        {
            // Retorna todos os filmes
            return Ok(AppDbContext.Filmes);
        }

        // GET: filmes/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            // Busca o filme pelo ID
            var filme = AppDbContext.Filmes.FirstOrDefault(f => f.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (filme == null)
            {
                return NotFound($"Filme com ID {id} não encontrado");
            }
            
            // Retorna o filme encontrado
            return Ok(filme);
        }

        // POST: filmes
        [HttpPost]
        public IActionResult Cadastrar([FromBody] FilmeDto filmeDto)
        {
            // Validação do ID do gênero
            var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == filmeDto.GeneroId);
            if (genero == null)
            {
                return BadRequest($"Gênero com ID {filmeDto.GeneroId} não encontrado");
            }
            
            // Validação do ID do estúdio
            var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == filmeDto.EstudioId);
            if (estudio == null)
            {
                return BadRequest($"Estúdio com ID {filmeDto.EstudioId} não encontrado");
            }
            
            // Cria um novo filme com os dados do DTO
            var filme = new ApiLocadora.Filme
            {
                Titulo = filmeDto.Titulo,
                AnoLancamento = filmeDto.AnoLancamento,
                Diretor = filmeDto.Diretor,
                GeneroId = filmeDto.GeneroId,
                GeneroObj = genero,
                EstudioId = filmeDto.EstudioId,
                EstudioObj = estudio,
                AvaliacaoIMDB = filmeDto.AvaliacaoIMDB
            };
            
            // Adiciona o filme à lista
            AppDbContext.Filmes.Add(filme);
            
            // Retorna o filme criado com o status 201 Created
            return CreatedAtAction(nameof(BuscarPorId), new { id = filme.Id }, filme);
        }

        // PUT: filmes/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] FilmeDto filmeDto)
        {
            // Busca o filme pelo ID
            var filme = AppDbContext.Filmes.FirstOrDefault(f => f.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (filme == null)
            {
                return NotFound($"Filme com ID {id} não encontrado");
            }
            
            // Validação do ID do gênero
            var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == filmeDto.GeneroId);
            if (genero == null)
            {
                return BadRequest($"Gênero com ID {filmeDto.GeneroId} não encontrado");
            }
            
            // Validação do ID do estúdio
            var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == filmeDto.EstudioId);
            if (estudio == null)
            {
                return BadRequest($"Estúdio com ID {filmeDto.EstudioId} não encontrado");
            }
            
            // Atualiza os dados do filme
            filme.Titulo = filmeDto.Titulo;
            filme.AnoLancamento = filmeDto.AnoLancamento;
            filme.Diretor = filmeDto.Diretor;
            filme.GeneroId = filmeDto.GeneroId;
            filme.GeneroObj = genero;
            filme.EstudioId = filmeDto.EstudioId;
            filme.EstudioObj = estudio;
            filme.AvaliacaoIMDB = filmeDto.AvaliacaoIMDB;
            
            // Retorna o filme atualizado
            return Ok(filme);
        }
        
        // DELETE: filmes/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            // Busca o filme pelo ID
            var filme = AppDbContext.Filmes.FirstOrDefault(f => f.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (filme == null)
            {
                return NotFound($"Filme com ID {id} não encontrado");
            }
            
            // Remove o filme da lista
            AppDbContext.Filmes.Remove(filme);
            
            // Retorna 204 No Content para indicar sucesso sem conteúdo
            return NoContent();
        }
    }
}
