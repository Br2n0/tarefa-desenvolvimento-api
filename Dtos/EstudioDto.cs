using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class EstudioDto
    {
        [Required(ErrorMessage = "O nome do estúdio é obrigatório")]
        public required string Nome { get; set; }
        
        [Required(ErrorMessage = "O nome do distribuidor é obrigatório")]
        public required string Distribuidor { get; set; }
    }
} 