using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itacometragem.Models;

namespace Itacometragem.Components
{
    public class FilterListViewComponent : ViewComponent
    {
        private readonly ItacometragemContext _context;
        public FilterListViewComponent(ItacometragemContext  context)
        {
            _context = context;
        }
        
        public IViewComponentResult Invoke(string driverId, string motiveId, string carId, string initialDate, string finalDate)
        {
            FilterData model = new FilterData
            {
                DriverId = driverId,
                MotiveId = motiveId,
                CarId = carId,
                InitialDate = initialDate,
                FinalDate = finalDate,
                Cars = _context.Cars.ToList(),
                Motives = _context.Motives.ToList(),
                Drivers = _context.Drivers.ToList()
            };

            return View(model);

        }
    }
}
