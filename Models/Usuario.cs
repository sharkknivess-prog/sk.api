using System.ComponentModel.DataAnnotations;

namespace SharkKnives.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        [StringLength(255, ErrorMessage = "O email não pode exceder 255 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(255, ErrorMessage = "A senha não pode exceder 255 caracteres")]
        public string SenhaHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(255, ErrorMessage = "O nome não pode exceder 255 caracteres")]
        public string Nome { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}