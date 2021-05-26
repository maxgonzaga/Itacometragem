using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Itacometragem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Itacometragem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DriverController : Controller
    {
        private readonly IItacometragemUnitOfWork _data;

        public DriverController(IItacometragemUnitOfWork data)
        {
            _data = data;
        }
        
        public IActionResult Add()
        {
            ViewBag.Action = "Adicionar motorista";
            return View("Edit", new Driver());
        }

        [HttpPost]
        public IActionResult Edit(Driver driver)
        {
            string key = nameof(Driver.Name);
            if (ModelState.GetValidationState(key) == ModelValidationState.Valid)
            {
                IEnumerable<string> driverNames = from d in _data.Drivers.List()
                                                    select d.Name.ToLower();
                if (driver.DriverId == 0 && driverNames.Contains(driver.Name.ToLower()))
                {
                    ModelState.AddModelError(key, "Escolha outro nome.");
                }
            }

            if (ModelState.IsValid)
            {
                if (driver.DriverId == 0)
                {
                    _data.Drivers.Insert(driver);
                }
                else
                {
                    _data.Drivers.Update(driver);
                }
                _data.Save();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = driver.DriverId == 0 ? "Adicionar motorista" : "Editar motorista";
                return View("Edit", new Driver());
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Driver driver = _data.Drivers.Get(id);
            ViewBag.Action = "Editar motorista";
            return View("Edit", driver);
        }

        public IActionResult List()
        {
            IEnumerable<Driver> drivers = _data.Drivers.List();
            return View(drivers);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Driver driver = _data.Drivers.Get(id);
            return View(driver);
        }

        [HttpPost]
        public IActionResult Delete(Driver driver)
        {
            IEnumerable<int?> drivers = from ride in _data.Rides.List()
                                        select ride.DriverId;
            if (drivers.Contains(driver.DriverId))
            {
                TempData["deleteMessage"] = "Você não pode remover um motorista que tem corridas registradas.";
            }
            else
            {
                _data.Drivers.Delete(driver);
                _data.Save();
                TempData["deleteMessage"] = "O motorista foi removido!";
            }
            return RedirectToAction("List");

        }
    }
}
