using System.Collections.Generic;

namespace Itacometragem.Models
{
    public interface IHelper
    {
        public int? GetMileage(Ride ride);
        public void PopulateInitialMileage(IEnumerable<Ride> rides);
    }
}
