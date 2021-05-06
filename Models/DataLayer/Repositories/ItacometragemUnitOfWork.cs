using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class ItacometragemUnitOfWork : IItacometragemUnitOfWork
    {
        private ItacometragemContext _context;
        private Repository<Ride> _rides;
        private Repository<Motive> _motives;
        private Repository<Car> _cars;
        private Repository<Driver> _drivers;

        public ItacometragemUnitOfWork(ItacometragemContext context)
        {
            _context = context;
        }

        public Repository<Ride> Rides
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

        public Repository<Motive> Motives
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

        public Repository<Car> Cars
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

        public Repository<Driver> Drivers
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
