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
    [Route("estudios")]
    [ApiController]
    public class EstudioController : ControllerBase
    {
        // Novo - DbContext do Entity Framework
        private readonly AppDbContext _context;
        
        // Novo construtor com injeção de dependência
        public EstudioController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: estudios
        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            // Código antigo:
            // return Ok(AppDbContext.Estudios);
            
            // Novo código usando Entity Framework:
            var estudios = await _context.Estudios.ToListAsync();
            return Ok(estudios);
        }

        // GET: estudios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            // Código antigo:
            // var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == id);
            
            // Novo código usando Entity Framework:
            var estudio = await _context.Estudios.FindAsync(id);
            
            // Se não encontrou, retorna 404 Not Found
            if (estudio == null)
            {
                return NotFound($"Estúdio com ID {id} não encontrado");
            }
            
            // Retorna o estúdio encontrado
            return Ok(estudio);
        }

        // POST: estudios
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] EstudioDto estudioDto)
        {
            // Cria um novo estúdio com os dados do DTO
            var estudio = new Estudio
            {
                Nome = estudioDto.Nome,
                Distribuidor = estudioDto.Distribuidor
            };
            
            // Código antigo:
            // AppDbContext.Estudios.Add(estudio);
            
            // Novo código usando Entity Framework:
            _context.Estudios.Add(estudio);
            await _context.SaveChangesAsync();
            
            // Retorna o estúdio criado com o status 201 Created
            return CreatedAtAction(nameof(BuscarPorId), new { id = estudio.Id }, estudio);
        }

        // PUT: estudios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] EstudioDto estudioDto)
        {
            // Código antigo:
            // var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == id);
            
            // Novo código usando Entity Framework:
            var estudio = await _context.Estudios.FindAsync(id);
            
            // Se não encontrou, retorna 404 Not Found
            if (estudio == null)
            {
                return NotFound($"Estúdio com ID {id} não encontrado");
            }
            
            // Atualiza os dados do estúdio
            estudio.Nome = estudioDto.Nome;
            estudio.Distribuidor = estudioDto.Distribuidor;
            
            // Novo código usando Entity Framework:
            await _context.SaveChangesAsync();
            
            // Retorna o estúdio atualizado
            return Ok(estudio);
        }
        
        // DELETE: estudios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            // Código antigo:
            // var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == id);
            
            // Novo código usando Entity Framework:
            var estudio = await _context.Estudios.FindAsync(id);
            
            // Se não encontrou, retorna 404 Not Found
            if (estudio == null)
            {
                return NotFound($"Estúdio com ID {id} não encontrado");
            }
            
            // Código antigo:
            // var filmeComEstudio = AppDbContext.Filmes.FirstOrDefault(f => f.EstudioId == id);
            
            // Novo código usando Entity Framework:
            var filmeComEstudio = await _context.Filmes.FirstOrDefaultAsync(f => f.EstudioId == id);
            
            if (filmeComEstudio != null)
            {
                return BadRequest($"Não é possível remover o estúdio pois ele está sendo usado pelo filme '{filmeComEstudio.Titulo}'");
            }
            
            // Código antigo:
            // AppDbContext.Estudios.Remove(estudio);
            
            // Novo código usando Entity Framework:
            _context.Estudios.Remove(estudio);
            await _context.SaveChangesAsync();
            
            // Retorna 204 No Content para indicar sucesso sem conteúdo
            return NoContent();
        }
    }
} 