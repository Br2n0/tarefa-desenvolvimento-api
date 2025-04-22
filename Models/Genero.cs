using System;

namespace ApiLocadora.Models
{
    public class Genero
    {
        // Propriedade de identificação única para o gênero
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Nome do gênero (ex: "Ação", "Comédia", "Drama")
        // Required indicaria que esta propriedade é obrigatória em um DTO
        public required string Nome { get; set; }
    }
} 