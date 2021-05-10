using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class ItacometragemUnitOfWork : IItacometragemUnitOfWork
    {
        private ItacometragemContext _context;
        private IRepository<Ride> _rides;
        private IRepository<Motive> _motives;
        private IRepository<Car> _cars;
        private IRepository<Driver> _drivers;

        public ItacometragemUnitOfWork(ItacometragemContext context)
        {
            _context = context;
        }

        public IRepository<Ride> Rides
        {
            get
            {
                if (_rides == null)
                {
                    _rides = new Repository<Ride>(_context);
                }
                return _rides;
            }
        }

        public IRepository<Motive> Motives
        {
            get
            {
                if (_motives == null)
                {
                    _motives = new Repository<Motive>(_context);
                }
                return _motives;
            }
        }

        public IRepository<Car> Cars
        {
            get
            {
                if (_cars == null)
                {
                    _cars = new Repository<Car>(_context);
                }
                return _cars;
            }
        }

        public IRepository<Driver> Drivers
        {
            get
            {
                if (_drivers == null)
                {
                    _drivers = new Repository<Driver>(_context);
                }
                return _drivers;
            }
        }

        public void Save() => _context.SaveChanges();
    }
}
