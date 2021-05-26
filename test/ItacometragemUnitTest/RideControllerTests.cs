using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Moq;
using Itacometragem.Models;
using Itacometragem.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ItacometragemUnitTest
{
    public class RideControllerTests
    {
        private Mock<IRepository<Ride>> GetRideRep()
        {
            var ridesRep = new Mock<IRepository<Ride>>();
            ridesRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Ride());

            ridesRep.Setup(m => m.Delete(It.IsAny<Ride>()));

            ridesRep.Setup(m => m.List(It.IsAny<QueryOptions<Ride>>()))
                .Returns(new List<Ride>());

            ridesRep.Setup(m => m.List())
                .Returns(new List<Ride>());

            ridesRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Ride());

            ridesRep.Setup(m => m.Get(It.IsAny<QueryOptions<Ride>>()))
                .Returns(new Ride());

            ridesRep.Setup(m => m.Insert(It.IsAny<Ride>()));
            ridesRep.Setup(m => m.Update(It.IsAny<Ride>()));
            ridesRep.Setup(m => m.Delete(It.IsAny<Ride>()));

            return ridesRep;
        }

        private Mock<IRepository<Car>> GetCarRep()
        {
            var carsRep = new Mock<IRepository<Car>>();
            carsRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Car());

            carsRep.Setup(m => m.Delete(It.IsAny<Car>()));

            carsRep.Setup(m => m.List(It.IsAny<QueryOptions<Car>>()))
                .Returns(new List<Car>());

            carsRep.Setup(m => m.List())
                .Returns(new List<Car>());

            carsRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Car());

            carsRep.Setup(m => m.Get(It.IsAny<QueryOptions<Car>>()))
                .Returns(new Car());

            carsRep.Setup(m => m.Insert(It.IsAny<Car>()));
            carsRep.Setup(m => m.Update(It.IsAny<Car>()));
            carsRep.Setup(m => m.Delete(It.IsAny<Car>()));

            return carsRep;
        }

        private Mock<IRepository<Motive>> GetMotiveRep()
        {
            var motivesRep = new Mock<IRepository<Motive>>();
            motivesRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Motive());

            motivesRep.Setup(m => m.Delete(It.IsAny<Motive>()));

            motivesRep.Setup(m => m.List(It.IsAny<QueryOptions<Motive>>()))
                .Returns(new List<Motive>());

            motivesRep.Setup(m => m.List())
                .Returns(new List<Motive>());

            motivesRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Motive());

            motivesRep.Setup(m => m.Get(It.IsAny<QueryOptions<Motive>>()))
                .Returns(new Motive());

            motivesRep.Setup(m => m.Insert(It.IsAny<Motive>()));
            motivesRep.Setup(m => m.Update(It.IsAny<Motive>()));
            motivesRep.Setup(m => m.Delete(It.IsAny<Motive>()));

            return motivesRep;
        }

        private Mock<IRepository<Driver>> GetDriverRep()
        {
            var driversRep = new Mock<IRepository<Driver>>();
            driversRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Driver());

            driversRep.Setup(m => m.Delete(It.IsAny<Driver>()));

            driversRep.Setup(m => m.List(It.IsAny<QueryOptions<Driver>>()))
                .Returns(new List<Driver>());

            driversRep.Setup(m => m.List())
                .Returns(new List<Driver>());

            driversRep.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(new Driver());

            driversRep.Setup(m => m.Get(It.IsAny<QueryOptions<Driver>>()))
                .Returns(new Driver());

            driversRep.Setup(m => m.Insert(It.IsAny<Driver>()));
            driversRep.Setup(m => m.Update(It.IsAny<Driver>()));
            driversRep.Setup(m => m.Delete(It.IsAny<Driver>()));

            return driversRep;
        }

        public Mock<IItacometragemUnitOfWork> GetUnifOfWork()
        {
            var ridesRep = GetRideRep();
            var carsRep = GetCarRep();
            var driversRep = GetDriverRep();
            var motiveRep = GetMotiveRep();

            var unit = new Mock<IItacometragemUnitOfWork>();
            unit.Setup(m => m.Rides).Returns(ridesRep.Object);
            unit.Setup(m => m.Cars).Returns(carsRep.Object);
            unit.Setup(m => m.Drivers).Returns(driversRep.Object);
            unit.Setup(m => m.Motives).Returns(motiveRep.Object);
            
            return unit;
        }

        RideController controller;
        Mock<ILogger<RideController>> logger;
        Mock<IHelper> helper;
        Mock<IItacometragemUnitOfWork> unit;
        Mock<ITempDataDictionary> temp;

        public RideControllerTests()
        {
            unit = GetUnifOfWork();
            logger = new Mock<ILogger<RideController>>();
            temp = new Mock<ITempDataDictionary>();
            helper = new Mock<IHelper>();
            controller = new RideController(unit.Object, logger.Object, helper.Object);
            controller.TempData = temp.Object;
        }
        
        [Fact]
        public void PostDeleteReturnsRedirectToActionResult()
        {
            var ride = new Ride();
            var result = controller.Delete(ride);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void AddReturnViewResult()
        {
            var result = controller.Add();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetEditReturnsViewResult()
        {
            var result = controller.Edit(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PostEditReturnsRedirectToActionResultIfModelStateValid()
        {
            var result = controller.Edit(new Ride());
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void PostEditReturnsViewResultIfModelStateNotValid()
        {
            controller.ModelState.AddModelError("", "error");
            var result = controller.Edit(new Ride());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PostEditCallsInsertIfRideIdEqualsZero()
        {
            int insertCalled = 0;
            unit.Setup(m => m.Rides.Insert(It.IsAny<Ride>()))
                .Callback(() => insertCalled++);

            controller.Edit(new Ride { RideId = 0 });

            Assert.Equal(1, insertCalled);
        }

        [Fact]
        public void PostEditCallsUpdateIfRideIdNotEqualsToZero()
        {
            int updateCalled = 0;
            unit.Setup(m => m.Rides.Update(It.IsAny<Ride>()))
                .Callback(() => updateCalled++);

            controller.Edit(new Ride { RideId = 1 });

            Assert.Equal(1, updateCalled);
        }
    }
}
