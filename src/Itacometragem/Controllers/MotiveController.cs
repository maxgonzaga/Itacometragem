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
    public class MotiveController : Controller
    {
        public MotiveController(IItacometragemUnitOfWork data, IHelper helper)
        {
            _data = data;
            _helper = helper;
        }

        private readonly IItacometragemUnitOfWork _data;
        private readonly IHelper _helper;
        
        public IActionResult List()
        {
            Dictionary<int, int?> motiveDistance = new Dictionary<int, int?>();
            IEnumerable<Motive> motives = _data.Motives.List();

            MotiveListViewModel model = new MotiveListViewModel
            {
                Motives = motives,
            };

            return View(model);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "Adicionar motivo";
            return View("Edit", new Motive());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Motive motive = _data.Motives.Get(id);
            ViewBag.Action = "Editar motivo";
            return View(motive);
        }

        [HttpPost]
        public IActionResult Edit(Motive motive)
        {
            IEnumerable<Motive> motives = _data.Motives.List();
            IEnumerable<string> existingCarNames = from c in motives select c.Name;

            string key = nameof(Motive.Name);
            if (ModelState.GetFieldValidationState(key) == ModelValidationState.Valid)
            {
                if (motive.MotiveId == 0 && existingCarNames.Contains(motive.Name))
                {
                    ModelState.AddModelError(key, "Escolha outro nome");
                }
            }

            if (ModelState.IsValid)
            {
                if (motive.MotiveId == 0)
                {
                    _data.Motives.Insert(motive);
                }
                else
                {
                    _data.Motives.Update(motive);
                }
                _data.Save();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = motive.MotiveId == 0 ? "Adicionar motivo" : "Editar motivo";
                return View("Edit", new Motive());
            }
        }

        public IActionResult Delete(int id)
        {
            return View(_data.Motives.Get(id));
        }

        [HttpPost]
        public IActionResult Delete(Motive motive)
        {
            IEnumerable<Ride> rides = _data.Rides.List();
            IEnumerable<int?> motives = from ride in rides select ride.MotiveId;
            
            if (motives.Contains(motive.MotiveId))
            {
                TempData["deleteMessage"] = "Você não pode remover um motivo que tem corridas registradas.";
            }
            else
            {
                _data.Motives.Delete(motive);
                _data.Save();
                TempData["deleteMessage"] = "O motivo foi removido!";
            }
            return RedirectToAction("List");
        }

        
        
        public int? GetTotalDistancePerMotive(Motive motive)
        {
            QueryOptions<Ride> queryOptions = new QueryOptions<Ride>
            {
                Where = ride => ride.MotiveId == motive.MotiveId
            };
            IEnumerable<Ride> rides = _data.Rides.List(queryOptions);
            int? totalDistance = 0;
            _helper.PopulateInitialMileage(rides);
            foreach (Ride ride in rides)
            {
                totalDistance += ride.GetDistance();
            }
            return totalDistance;
        }

    }
}
