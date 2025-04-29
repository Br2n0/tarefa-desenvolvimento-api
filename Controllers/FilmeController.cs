using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using System;
using System.Linq;
using System.Collections.Generic;
using ApiLocadora.Models;
// Comentado o import do contexto estático
// using ApiLocadora.DbContext;
using ApiLocadora.dataContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("filmes")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        // Propriedade antiga (lista estática)
        // private readonly List<ApiLocadora.Filme> _filmes;
        
        // Nova propriedade - DbContext do Entity Framework
        private readonly AppDbContext _context;

        // Construtor antigo
        /*
        public FilmeController()
        {
            _filmes = AppDbContext.Filmes;
        }
        */
        
        // Novo construtor com injeção de dependência
        public FilmeController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: filmes
        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            // Código antigo:
            // return Ok(_filmes);
            
            // Novo código usando Entity Framework:
            // Inclui os objetos relacionados (Genero e Estudio)
            var filmes = await _context.Filmes
                .Include(f => f.Genero)
                .Include(f => f.Estudio)
                .ToListAsync();
            
            return Ok(filmes);
        }

        // GET: filmes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            // Código antigo:
            // var filme = _filmes.FirstOrDefault(f => f.Id == id);
            
            // Novo código usando Entity Framework:
            var filme = await _context.Filmes
                .Include(f => f.Genero)
                .Include(f => f.Estudio)
                .FirstOrDefaultAsync(f => f.Id == id);
            
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
        public async Task<IActionResult> Cadastrar([FromBody] FilmeDto filmeDto)
        {
            // Código antigo:
            /*
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
            */
            
            // Novo código usando Entity Framework:
            // Validação do ID do gênero
            var genero = await _context.Generos.FindAsync(filmeDto.GeneroId);
            if (genero == null)
            {
                return BadRequest($"Gênero com ID {filmeDto.GeneroId} não encontrado");
            }
            
            // Validação do ID do estúdio
            var estudio = await _context.Estudios.FindAsync(filmeDto.EstudioId);
            if (estudio == null)
            {
                return BadRequest($"Estúdio com ID {filmeDto.EstudioId} não encontrado");
            }
            
            // Cria um novo filme com os dados do DTO
            var filme = new Filme
            {
                Titulo = filmeDto.Titulo,
                AnoLancamento = filmeDto.AnoLancamento,
                Diretor = filmeDto.Diretor,
                GeneroId = filmeDto.GeneroId,
                // GeneroObj renomeado para Genero na classe atualizada
                // GeneroObj = genero,
                EstudioId = filmeDto.EstudioId,
                // EstudioObj renomeado para Estudio na classe atualizada
                // EstudioObj = estudio,
                AvaliacaoIMDB = filmeDto.AvaliacaoIMDB
            };
            
            // Novo código usando Entity Framework:
            // Adiciona o filme ao contexto
            _context.Filmes.Add(filme);
            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();
            
            // Código antigo:
            // _filmes.Add(filme);
            
            // Retorna o filme criado com o status 201 Created
            return CreatedAtAction(nameof(BuscarPorId), new { id = filme.Id }, filme);
        }

        // PUT: filmes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] FilmeDto filmeDto)
        {
            // Código antigo:
            // var filme = _filmes.FirstOrDefault(f => f.Id == id);
            
            // Novo código usando Entity Framework:
            var filme = await _context.Filmes.FindAsync(id);
            
            // Se não encontrou, retorna 404 Not Found
            if (filme == null)
            {
                return NotFound($"Filme com ID {id} não encontrado");
            }
            
            // Código antigo:
            /*
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
            */
            
            // Novo código usando Entity Framework:
            // Validação do ID do gênero
            var genero = await _context.Generos.FindAsync(filmeDto.GeneroId);
            if (genero == null)
            {
                return BadRequest($"Gênero com ID {filmeDto.GeneroId} não encontrado");
            }
            
            // Validação do ID do estúdio
            var estudio = await _context.Estudios.FindAsync(filmeDto.EstudioId);
            if (estudio == null)
            {
                return BadRequest($"Estúdio com ID {filmeDto.EstudioId} não encontrado");
            }
            
            // Atualiza os dados do filme
            filme.Titulo = filmeDto.Titulo;
            filme.AnoLancamento = filmeDto.AnoLancamento;
            filme.Diretor = filmeDto.Diretor;
            filme.GeneroId = filmeDto.GeneroId;
            // GeneroObj renomeado para Genero na classe atualizada
            // filme.GeneroObj = genero;
            filme.EstudioId = filmeDto.EstudioId;
            // EstudioObj renomeado para Estudio na classe atualizada
            // filme.EstudioObj = estudio;
            filme.AvaliacaoIMDB = filmeDto.AvaliacaoIMDB;
            
            // Novo código usando Entity Framework:
            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();
            
            // Retorna o filme atualizado
            return Ok(filme);
        }
        
        // DELETE: filmes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            // Código antigo:
            // var filme = _filmes.FirstOrDefault(f => f.Id == id);
            
            // Novo código usando Entity Framework:
            var filme = await _context.Filmes.FindAsync(id);
            
            // Se não encontrou, retorna 404 Not Found
            if (filme == null)
            {
                return NotFound($"Filme com ID {id} não encontrado");
            }
            
            // Código antigo:
            // _filmes.Remove(filme);
            
            // Novo código usando Entity Framework:
            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            
            // Retorna 204 No Content para indicar sucesso sem conteúdo
            return NoContent();
        }
    }
}
