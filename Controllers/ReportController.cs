using Microsoft.AspNetCore.Mvc;
using System;
using Itacometragem.Library;
using Itacometragem.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Itacometragem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IItacometragemUnitOfWork _data;
        private readonly Helper _helper;

        public ReportController(IReportService reportService, IItacometragemUnitOfWork data)
        {
            _reportService = reportService;
            _data = data;
            _helper = new Helper(data);
        }

        [HttpGet]
        public IActionResult Generate()
        {
            ViewBag.Drivers = _data.Drivers.List();
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

                report.Driver = _data.Drivers.Get(Convert.ToInt32(report.DriverId));

                byte[] pdfFile = _reportService.GeneratePdfReport(report);

                return File(pdfFile, "application/octed-stream", $"report-{DateTime.Now:d}.pdf");
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
            queryOptions.Where = ride => ride.DriverId == report.DriverId;
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
