﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Itacometragem.Models;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Itacometragem.Controllers
{
    public class RideController : Controller
    {
        private readonly ILogger<RideController> _logger;
        private readonly Helper _helper;
        private readonly IItacometragemUnitOfWork _data;

        public RideController(IItacometragemUnitOfWork data, ILogger<RideController> logger, Helper helper)
        {
            _logger = logger;
            _helper = helper;
            _data = data;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Drivers = _data.Drivers.List();
            ViewBag.Cars = _data.Cars.List();
            ViewBag.Motives = _data.Motives.List();
            ViewBag.Action = "Registrar corrida";
            return View("Edit", new Ride());
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("GET: Delete action was triggered.");
            QueryOptions<Ride> queryOptions = GetQueryOptionsForRide(id);
            return View(_data.Rides.Get(queryOptions));
        }

        [HttpPost]
        public IActionResult Delete(Ride ride)
        {
            _logger.LogInformation("POST: Delete action was triggered.");
            _data.Rides.Delete(ride);
            _data.Save();
            _logger.LogInformation($"RideId: {ride.RideId} was deleted from DB.");
            TempData["deleteMessage"] = "A corrida foi apagada!";
            return RedirectToAction("Add", "Ride");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Ride ride = _data.Rides.Get(GetQueryOptionsForRide(id));
            ViewBag.Drivers = _data.Drivers.List();
            ViewBag.Cars = _data.Cars.List();
            ViewBag.Motives = _data.Motives.List();
            ViewBag.Action = "Editar corrida";
            return View(ride);
        }

        [HttpPost]
        public IActionResult Edit(Ride ride)
        {
            string key = nameof(Ride.Date);
            if (ModelState.GetValidationState(key) == ModelValidationState.Valid)
            {
                if (ride.Date > DateTime.Now)
                {
                    ModelState.AddModelError(key, "A data não pode estar no futuro.");
                }
            }

            key = nameof(Ride.FinalMileage);
            if (ModelState.IsValid &&
                ModelState.GetFieldValidationState(key) == ModelValidationState.Valid)
            {
                int? lastMileage = _helper.GetMileage(ride);
                if (ride.FinalMileage < lastMileage)
                {
                    ModelState.AddModelError(key, $"A quilometragem final não pode ser menor que {lastMileage}.");
                }
            }

            if (ModelState.IsValid)
            {
                if (ride.RideId == 0)
                {
                    _data.Rides.Insert(ride);
                }
                else
                {
                    _data.Rides.Update(ride);
                }
                _data.Save();
                return RedirectToAction("Add");
            }
            else
            {
                ViewBag.Drivers = _data.Drivers.List();
                ViewBag.Cars = _data.Cars.List();
                ViewBag.Motives = _data.Motives.List();
                ViewBag.Action = ride.RideId == 0 ? "Registrar corrida" : "Editar corrida";
                return View("Edit", new Ride());
            }
        }


        [HttpGet]
        public ViewResult List(RidesGridDTO dto)
        {
            RidesGridBuilder builder = new RidesGridBuilder(dto);

            RideQueryOptions options = new RideQueryOptions()
            {
                Includes = "Motive, Car, Driver",
                PageNumber = builder.Route.PageNumber,
                PageSize = builder.Route.PageSize,
                OrderBy = ride => ride.Date
            };
            options.Filter(builder);


            IEnumerable<Ride> rides = _data.Rides.List(options);
            _helper.PopulateInitialMileage(rides);

            RideListViewModel model = new RideListViewModel
            {
                Rides = rides,
                Cars = _data.Cars.List(),
                Drivers = _data.Drivers.List(),
                Motives = _data.Motives.List(),
                TotalDistance = GetTotalDistance(builder),
                CurrentRoute = builder.Route,
                TotalPages = builder.GetTotalPages(_data.Rides.Count)
            };

            return View(model);

        }


        [HttpPost]
        public IActionResult Filter(FilterData filterData, bool clear = false)
        {
            RidesGridDTO dto;
            if (clear)
            {
                dto = new RidesGridDTO();
            }
            else
            {
                dto = new RidesGridDTO(filterData);
            }
            RidesGridBuilder gridBuiler = new RidesGridBuilder(dto);
            return RedirectToAction("List", gridBuiler.Route);
        }


        public IActionResult Details(int id)
        {
            Ride ride = _data.Rides.Get(GetQueryOptionsForRide(id));
            ride.InitialMileage = _helper.GetMileage(ride);
            return View(ride);
        }

        [NonAction]
        private QueryOptions<Ride> GetQueryOptionsForRide(int id)
        {
            QueryOptions<Ride> queryOptions = new QueryOptions<Ride>
            {
                Includes = "Car, Driver, Motive",
                Where = ride => ride.RideId == id
            };
            return queryOptions;
        }

        [NonAction]
        private int? GetTotalDistance(RidesGridBuilder builder)
        {
            RideQueryOptions options = new RideQueryOptions();
            options.Filter(builder);
            IEnumerable<Ride> rides = _data.Rides.List(options);
            _helper.PopulateInitialMileage(rides);

            int? distance = 0;
            foreach (Ride ride in rides)
            {
                distance += ride.GetDistance();
            }
            return distance;
        }

    }
}
