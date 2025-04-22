using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DbContext;
using System;
using System.Linq;

namespace ApiLocadora.Controllers
{
    [Route("generos")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        // GET: generos
        [HttpGet]
        public IActionResult Buscar()
        {
            // Retorna todos os gêneros
            return Ok(AppDbContext.Generos);
        }

        // GET: generos/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            // Busca o gênero pelo ID
            var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (genero == null)
            {
                return NotFound($"Gênero com ID {id} não encontrado");
            }
            
            // Retorna o gênero encontrado
            return Ok(genero);
        }

        // POST: generos
        [HttpPost]
        public IActionResult Cadastrar([FromBody] GeneroDto generoDto)
        {
            // Cria um novo gênero com os dados do DTO
            var genero = new Genero
            {
                Nome = generoDto.Nome
            };
            
            // Adiciona o gênero à lista
            AppDbContext.Generos.Add(genero);
            
            // Retorna o gênero criado com o status 201 Created
            return CreatedAtAction(nameof(BuscarPorId), new { id = genero.Id }, genero);
        }

        // PUT: generos/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] GeneroDto generoDto)
        {
            // Busca o gênero pelo ID
            var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (genero == null)
            {
                return NotFound($"Gênero com ID {id} não encontrado");
            }
            
            // Atualiza os dados do gênero
            genero.Nome = generoDto.Nome;
            
            // Retorna o gênero atualizado
            return Ok(genero);
        }
        
        // DELETE: generos/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            // Busca o gênero pelo ID
            var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (genero == null)
            {
                return NotFound($"Gênero com ID {id} não encontrado");
            }
            
            // Verifica se o gênero está sendo usado por algum filme
            var filmeComGenero = AppDbContext.Filmes.FirstOrDefault(f => f.GeneroId == id);
            if (filmeComGenero != null)
            {
                return BadRequest($"Não é possível remover o gênero pois ele está sendo usado pelo filme '{filmeComGenero.Titulo}'");
            }
            
            // Remove o gênero da lista
            AppDbContext.Generos.Remove(genero);
            
            // Retorna 204 No Content para indicar sucesso sem conteúdo
            return NoContent();
        }
    }
} 