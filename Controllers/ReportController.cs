using Microsoft.AspNetCore.Mvc;
using System;
using Itacometragem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Itacometragem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IItacometragemUnitOfWork _data;
        private readonly Helper _helper;

        public ReportController(IItacometragemUnitOfWork data, Helper helper)
        {
            _data = data;
            _helper = helper;
        }

        [HttpGet]
        public IActionResult Generate()
        {
            ViewBag.Motives = _data.Motives.List();
            ViewBag.Action = "Gerar relatório";
            return View(new Report());
        }
        
        [HttpPost]
        public IActionResult Generate(Report report)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Ride> rides = GetRides(report);

                foreach (Ride ride in rides)
                {
                    report.AddRide(ride);
                }

                report.Motive = _data.Motives.Get(Convert.ToInt32(report.MotiveId));

                return View("Report", report);
            }
            else
            {
                ViewBag.Drivers = _data.Drivers.List();
                ViewBag.Action = "Gerar relatório";
                return View(report);
            }
        }


        private IEnumerable<Ride> GetRides(Report report)
        {
            QueryOptions<Ride> queryOptions = new QueryOptions<Ride>();
            queryOptions.Includes = "Car, Driver, Motive";
            queryOptions.Where = ride => ride.MotiveId == report.MotiveId;
            queryOptions.Where = ride => ride.Date <= report.FinalDate &&
                ride.Date >= report.InitialDate;

            IEnumerable<Ride> rides = _data.Rides.List(queryOptions);

            foreach (Ride ride in rides)
            {
                ride.InitialMileage = _helper.GetMileage(ride);
            }

            return rides;
        }
    }
}
