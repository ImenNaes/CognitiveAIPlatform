using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ChatCompletionRequest
    {
        [Required(ErrorMessage = "Le champ 'Prompt' est obligatoire.")]
        [StringLength(2000, ErrorMessage = "Le prompt ne doit pas dépasser 2000 caractères.")]
        public required string Prompt { get; set; }
    }
}
