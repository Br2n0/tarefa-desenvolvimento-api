using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class FilmeDto
    {
        [Required(ErrorMessage = "O título do filme é obrigatório")]
        public required string Titulo { get; set; }
        
        [Required(ErrorMessage = "O ano de lançamento é obrigatório")]
        [Range(1895, 2100, ErrorMessage = "O ano de lançamento deve estar entre 1895 e 2100")]
        public int AnoLancamento { get; set; }
        
        [Required(ErrorMessage = "O nome do diretor é obrigatório")]
        public required string Diretor { get; set; }
        
        [Required(ErrorMessage = "O ID do gênero é obrigatório")]
        public Guid GeneroId { get; set; }
        
        [Required(ErrorMessage = "O ID do estúdio é obrigatório")]
        public Guid EstudioId { get; set; }
        
        [Required(ErrorMessage = "A avaliação IMDB é obrigatória")]
        [Range(0, 10, ErrorMessage = "A avaliação IMDB deve estar entre 0 e 10")]
        public decimal AvaliacaoIMDB { get; set; }
    }
}
