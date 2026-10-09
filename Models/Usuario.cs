using System.ComponentModel.DataAnnotations;

namespace SistemaAcademicoMVC.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public bool Activo { get; set; }
    }
}