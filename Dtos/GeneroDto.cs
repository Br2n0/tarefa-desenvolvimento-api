using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class GeneroDto
    {
        [Required(ErrorMessage = "O nome do gênero é obrigatório")]
        public required string Nome { get; set; }
    }
} 