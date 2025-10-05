using System.ComponentModel.DataAnnotations;

namespace SharkKnives.API.Models
{
    public class Faca
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório")]
        [StringLength(255, ErrorMessage = "O modelo não pode exceder 255 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "O material não pode exceder 255 caracteres")]
        public string? Material { get; set; }

        [StringLength(100, ErrorMessage = "As camadas não podem exceder 100 caracteres")]
        public string? Camadas { get; set; }

        [StringLength(100, ErrorMessage = "A pegada não pode exceder 100 caracteres")]
        public string? Pegada { get; set; }

        [StringLength(100, ErrorMessage = "O cabo não pode exceder 100 caracteres")]
        public string? Cabo { get; set; }

        [StringLength(100, ErrorMessage = "As dimensões não podem exceder 100 caracteres")]
        public string? Dimensoes { get; set; }

        [StringLength(100, ErrorMessage = "A referência não pode exceder 100 caracteres")]
        public string? Referencia { get; set; }

        // 3 imagens (URLs ou Base64)
        public string? FotoUrl1 { get; set; }
        public string? FotoUrl2 { get; set; }
        public string? FotoUrl3 { get; set; }

        // Controle
        public bool Ativo { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}