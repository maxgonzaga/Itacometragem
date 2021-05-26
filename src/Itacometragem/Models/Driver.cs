using System.ComponentModel.DataAnnotations;

namespace Itacometragem.Models
{
    public class Driver
    {
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Insira o nome do motorista")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Insira um endereço de e-mail válido.")]
        public string Email { get; set; }
    }
}
