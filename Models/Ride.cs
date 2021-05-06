using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itacometragem.Models
{
    public class Ride
    {
        public int RideId { get; set; }

        [Required(ErrorMessage = "Selecione o carro.")]
        public int? CarId { get; set; }

        [Required(ErrorMessage = "Selecione o motorista.")]
        public int? DriverId { get; set; }

        [Required(ErrorMessage = "Selectione o motivo")]
        public int? MotiveId { get; set; }

        public string Note { get; set; }

        [Required(ErrorMessage = "Insira a quilometragem final.")]
        [Range(0, 100000, ErrorMessage = "Insira um valor entre 0 e 100 000.")]
        public int? FinalMileage { get; set; }

        [Required(ErrorMessage = "Insira a data.")]
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public Car Car { get; set; }
        public Driver Driver { get; set; }
        public Motive Motive { get; set; }

        [NotMapped]
        public int? InitialMileage { get; set; }

        public int? GetDistance()
        {
            CheckInitialMileageIsNull();
            return FinalMileage - InitialMileage;
        }

        public double? GetCost()
        {
            CheckInitialMileageIsNull();
            return Car.Constant * GetDistance();
        }

        private void CheckInitialMileageIsNull()
        {
            if (InitialMileage == null)
            {
                throw new InvalidOperationException("InitialMileage cannot be null");
            }
        }
    }
}
