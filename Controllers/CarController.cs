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
    public class CarController : Controller
    {
        public CarController(IItacometragemUnitOfWork data)
        {
            _data = data;
        }

        private readonly IItacometragemUnitOfWork _data;
        
        public IActionResult List()
        {
            IEnumerable<Car> cars = _data.Cars.List();
            return View(cars);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "Adicionar carro";
            return View("Edit", new Car());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Car car = _data.Cars.Get(id);
            ViewBag.Action = "Editar carro";
            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            IEnumerable<Car> cars = _data.Cars.List();
            IEnumerable<string> existingCarNames = from c in cars
                                           select c.Name;
            string key = nameof(Car.Name);
            if (ModelState.GetFieldValidationState(key) == ModelValidationState.Valid)
            {
                if (car.CarId == 0 && existingCarNames.Contains(car.Name))
                {
                    ModelState.AddModelError(key, "Escolha outro nome");
                }
            }

            if (ModelState.IsValid)
            {
                if (car.CarId == 0)
                {
                    _data.Cars.Insert(car);
                }
                else
                {
                    _data.Cars.Insert(car);
                }
                _data.Save();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = car.CarId == 0 ? "Adicionar carro" : "Editar carro";
                return View("Edit", new Car());
            }
        }

        public IActionResult Delete(int id)
        {
            return View(_data.Cars.Get(id));
        }

        [HttpPost]
        public IActionResult Delete(Car car)
        {
            IEnumerable<int?> cars = from ride in _data.Rides.List()
                                         select ride.CarId;
            if (cars.Contains(car.CarId))
            {
                TempData["deleteMessage"] = "Você não pode remover um carro que tem corridas registradas.";
            }
            else
            {
                _data.Cars.Delete(car);
                _data.Save();
                TempData["deleteMessage"] = "O carro foi removido!";
            }
            return RedirectToAction("List");
        }

    }
}
