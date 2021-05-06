using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Itacometragem.Models
{
    public class Report
    {
        public Report()
        {
            Rides = new List<Ride>();
        }
        
        [Required(ErrorMessage = "Informe o nmotivo.")]
        public int MotiveId { get; set; }

        [Required(ErrorMessage = "Informe a data inicial.")]
        [DataType(DataType.Date)]
        public DateTime? InitialDate { get; set; }

        [Required(ErrorMessage = "Informe a data final.")]
        [DataType(DataType.Date)]
        public DateTime? FinalDate { get; set; }

        [BindNever]
        public List<Ride> Rides { get; }

        [BindNever]
        public Driver Motive { get; set; }


        public void AddRide(Ride ride)
        {
            if (ride.InitialMileage == null)
            {
                throw new InvalidOperationException("Car.InitialMileage is null");
            }
            else
            {
                Rides.Add(ride);
            }
        }

        public int? TotalDistance()
        {
            if (Rides.Count == 0)
            {
                throw new InvalidOperationException("Report.Rides list is empty.");
            }
            else
            {
                int? totalDistance = 0;
                foreach (Ride ride in Rides)
                {
                    totalDistance += ride.GetDistance();
                }
                return totalDistance;
            }
        }

    }
}
