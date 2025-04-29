using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
// Comentado o import do contexto estático
// using ApiLocadora.DbContext;
using ApiLocadora.dataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("generos")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        // Novo - DbContext do Entity Framework
        private readonly AppDbContext _context;
        
        // Novo construtor com injeção de dependência
        public GeneroController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: generos
        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            // Código antigo:
            // return Ok(AppDbContext.Generos);
            
            // Novo código usando Entity Framework:
            var generos = await _context.Generos.ToListAsync();
            return Ok(generos);
        }

        // GET: generos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            // Código antigo:
            // var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == id);
            
            // Novo código usando Entity Framework:
            var genero = await _context.Generos.FindAsync(id);
            
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
        public async Task<IActionResult> Cadastrar([FromBody] GeneroDto generoDto)
        {
            // Cria um novo gênero com os dados do DTO
            var genero = new Genero
            {
                Nome = generoDto.Nome
            };
            
            // Código antigo:
            // AppDbContext.Generos.Add(genero);
            
            // Novo código usando Entity Framework:
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
            
            // Retorna o gênero criado com o status 201 Created
            return CreatedAtAction(nameof(BuscarPorId), new { id = genero.Id }, genero);
        }

        // PUT: generos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] GeneroDto generoDto)
        {
            // Código antigo:
            // var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == id);
            
            // Novo código usando Entity Framework:
            var genero = await _context.Generos.FindAsync(id);
            
            // Se não encontrou, retorna 404 Not Found
            if (genero == null)
            {
                return NotFound($"Gênero com ID {id} não encontrado");
            }
            
            // Atualiza os dados do gênero
            genero.Nome = generoDto.Nome;
            
            // Novo código usando Entity Framework:
            await _context.SaveChangesAsync();
            
            // Retorna o gênero atualizado
            return Ok(genero);
        }
        
        // DELETE: generos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            // Código antigo:
            // var genero = AppDbContext.Generos.FirstOrDefault(g => g.Id == id);
            
            // Novo código usando Entity Framework:
            var genero = await _context.Generos.FindAsync(id);
            
            // Se não encontrou, retorna 404 Not Found
            if (genero == null)
            {
                return NotFound($"Gênero com ID {id} não encontrado");
            }
            
            // Código antigo:
            // var filmeComGenero = AppDbContext.Filmes.FirstOrDefault(f => f.GeneroId == id);
            
            // Novo código usando Entity Framework:
            var filmeComGenero = await _context.Filmes.FirstOrDefaultAsync(f => f.GeneroId == id);
            
            if (filmeComGenero != null)
            {
                return BadRequest($"Não é possível remover o gênero pois ele está sendo usado pelo filme '{filmeComGenero.Titulo}'");
            }
            
            // Código antigo:
            // AppDbContext.Generos.Remove(genero);
            
            // Novo código usando Entity Framework:
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
            
            // Retorna 204 No Content para indicar sucesso sem conteúdo
            return NoContent();
        }
    }
} 