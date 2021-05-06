using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class FilterData
    {
        public string DriverId { get; set; }

        public string MotiveId { get; set; }

        public string CarId { get; set; }

        [DataType(DataType.Date)]
        public string InitialDate { get; set; }

        [DataType(DataType.Date)]
        public string FinalDate { get; set; }

        [BindNever]
        public IEnumerable<Driver> Drivers { get; set; }

        [BindNever]
        public IEnumerable<Car> Cars { get; set; }

        [BindNever]
        public IEnumerable<Motive> Motives { get; set; }

    }
}
