using System.ComponentModel.DataAnnotations;

namespace SharkKnives.API.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(255, ErrorMessage = "O nome não pode exceder 255 caracteres")]
        public string Nome { get; set; } = string.Empty;
    }
}