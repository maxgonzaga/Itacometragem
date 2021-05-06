using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Itacometragem.Models
{
    public class Helper
    {
        private readonly IItacometragemUnitOfWork _data;

        public Helper([FromServices] IItacometragemUnitOfWork data)
        {
            _data = data;
        }

        public int? GetMileage(Ride ride)
        {
            QueryOptions<Ride> queryOptions = new QueryOptions<Ride>();
            queryOptions.Where = item => item.CarId == ride.CarId;
            queryOptions.Where = item => item.Date < ride.Date;
            queryOptions.OrderBy = item => item.Date;
            queryOptions.OrderByDirection = "desc";
            IEnumerable<Ride> rides = _data.Rides.List(queryOptions);

            int? mileage;
            if (rides.Count() == 0)
            {
                mileage = _data.Cars.Get(Convert.ToInt32(ride.CarId)).InitialMileage;
                return mileage;
            }
            mileage = rides.First().FinalMileage;
            return mileage;
        }

        public void PopulateInitialMileage(IEnumerable<Ride> rides)
        {
            foreach (Ride ride in rides)
            {
                ride.InitialMileage = GetMileage(ride);
            }
        }
    }
}
