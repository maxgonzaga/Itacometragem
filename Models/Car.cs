using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itacometragem.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required(ErrorMessage = "Insira o nome do carro.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insira o valor atual do odômetro.")]
        [Range(0, 100000, ErrorMessage = "Insira um valor entre 0 e 100 000.")]
        public int? InitialMileage { get; set; }

        [Required(ErrorMessage = "Insira a constante de cálculo.")]
        public double? Constant { get; set; }
    }
}
