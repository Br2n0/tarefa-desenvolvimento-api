using System;

namespace ApiLocadora.Models
{
    public class Estudio
    {
        // Propriedade de identificação única para o estúdio
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Nome do estúdio (ex: "Warner Bros", "Universal")
        public required string Nome { get; set; }
        
        // Nome da distribuidora do estúdio
        public required string Distribuidor { get; set; }
    }
} 