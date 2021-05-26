using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class FilterViewModel
    {
        public IEnumerable<Ride> Rides { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Motive> Motives { get; set; }
        public RouteDictionary CurrentRoute { get; set; }

        public string SelectedCar { get; set;}
        public string SelectedDriver { get; set; }
        public string SelectedMotive { get; set; }
        public string SelectedInitialDate { get; set; }
        public string SelectedFinalDate { get; set; }
    }
}
