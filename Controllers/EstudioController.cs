using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DbContext;
using System;
using System.Linq;

namespace ApiLocadora.Controllers
{
    [Route("estudios")]
    [ApiController]
    public class EstudioController : ControllerBase
    {
        // GET: estudios
        [HttpGet]
        public IActionResult Buscar()
        {
            // Retorna todos os estúdios
            return Ok(AppDbContext.Estudios);
        }

        // GET: estudios/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            // Busca o estúdio pelo ID
            var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == id);
            
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
        public IActionResult Cadastrar([FromBody] EstudioDto estudioDto)
        {
            // Cria um novo estúdio com os dados do DTO
            var estudio = new Estudio
            {
                Nome = estudioDto.Nome,
                Distribuidor = estudioDto.Distribuidor
            };
            
            // Adiciona o estúdio à lista
            AppDbContext.Estudios.Add(estudio);
            
            // Retorna o estúdio criado com o status 201 Created
            return CreatedAtAction(nameof(BuscarPorId), new { id = estudio.Id }, estudio);
        }

        // PUT: estudios/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] EstudioDto estudioDto)
        {
            // Busca o estúdio pelo ID
            var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (estudio == null)
            {
                return NotFound($"Estúdio com ID {id} não encontrado");
            }
            
            // Atualiza os dados do estúdio
            estudio.Nome = estudioDto.Nome;
            estudio.Distribuidor = estudioDto.Distribuidor;
            
            // Retorna o estúdio atualizado
            return Ok(estudio);
        }
        
        // DELETE: estudios/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            // Busca o estúdio pelo ID
            var estudio = AppDbContext.Estudios.FirstOrDefault(e => e.Id == id);
            
            // Se não encontrou, retorna 404 Not Found
            if (estudio == null)
            {
                return NotFound($"Estúdio com ID {id} não encontrado");
            }
            
            // Verifica se o estúdio está sendo usado por algum filme
            var filmeComEstudio = AppDbContext.Filmes.FirstOrDefault(f => f.EstudioId == id);
            if (filmeComEstudio != null)
            {
                return BadRequest($"Não é possível remover o estúdio pois ele está sendo usado pelo filme '{filmeComEstudio.Titulo}'");
            }
            
            // Remove o estúdio da lista
            AppDbContext.Estudios.Remove(estudio);
            
            // Retorna 204 No Content para indicar sucesso sem conteúdo
            return NoContent();
        }
    }
} 