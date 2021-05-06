using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class RideQueryOptions : QueryOptions<Ride>
    {
        public void Filter(RidesGridBuilder builder)
        {
            if (builder.IsFilterByCar)
            {
                Where = ride => ride.CarId == Convert.ToInt32(builder.Route.CarFilter);
            }
            if (builder.IsFilterByDriver)
            {
                Where = ride => ride.DriverId == Convert.ToInt32(builder.Route.DriverFilter);
            }
            if (builder.IsFilterByMotive)
            {
                Where = ride => ride.MotiveId == Convert.ToInt32(builder.Route.MotiveFilter);
            }
            if (builder.IsFilterByDate)
            {
                Where = ride => ride.Date <= DateTime.Parse(builder.Route.FinalDate) &&
                ride.Date >= DateTime.Parse(builder.Route.InitialDate);
            }
        }
    }
}
