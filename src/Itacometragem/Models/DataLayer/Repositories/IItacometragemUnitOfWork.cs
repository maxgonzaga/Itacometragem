using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public interface IItacometragemUnitOfWork
    {
        public IRepository<Ride> Rides { get; }
        public IRepository<Motive> Motives { get; }
        public IRepository<Car> Cars { get; }
        public IRepository<Driver> Drivers { get; }
        public void Save();
    }
}
