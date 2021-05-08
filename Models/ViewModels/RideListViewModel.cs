using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class RideListViewModel
    {
        public IEnumerable<Ride> Rides { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Motive> Motives { get; set; }
        public int? TotalDistance { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
        public double? TotalCost { get; set; }
    }
}
