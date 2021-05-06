using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public interface IItacometragemUnitOfWork
    {
        public Repository<Ride> Rides { get; }
        public Repository<Motive> Motives { get; }
        public Repository<Car> Cars { get; }
        public Repository<Driver> Drivers { get; }
        public void Save();
    }
}
