using Itacometragem.Models;
using System;
using System.Linq;

namespace Itacometragem.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ItacometragemContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Cars.Any())
            {
                return;   // DB has been seeded
            }

            var cars = new Car[]
            {
            new Car{Name="Palio Prata", InitialMileage=16789, Constant=0.25f},
            new Car{Name="Fusca Preto", InitialMileage=15891, Constant=0.50f},
            new Car{Name="Camaro Amarelo", InitialMileage=12901, Constant=0.29f},
            new Car{Name="Opala", InitialMileage=12930, Constant=0.27f}
            };

            foreach (Car car in cars)
            {
                context.Cars.Add(car);
            }
            context.SaveChanges();

            var motives = new Motive[]
            {
                new Motive{Name="Casa"},
                new Motive{Name="Visita"},
                new Motive{Name="Clubinho"}
            };

            foreach (Motive motive in motives)
            {
                context.Motives.Add(motive);
            }
            context.SaveChanges();

            var drivers = new Driver[]
            {
            new Driver{Name="Adriana"},
            new Driver{Name="Isidoro"},
            new Driver{Name="Joaquim"},
            new Driver{Name="Daniel"},
            new Driver{Name="Humberto"},
            new Driver{Name="Mariana"},
            new Driver{Name="Heliodoro"},
            };

            foreach (Driver driver in drivers)
            {
                context.Drivers.Add(driver);
            }
            context.SaveChanges();
        }
    }
}