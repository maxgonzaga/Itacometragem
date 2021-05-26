using System;
using Xunit;
using Itacometragem.Models;

namespace ItacometragemUnitTest
{
    public class RideTests
    {
        Ride ride;
        
        public RideTests()
        {
            ride = new Ride();
        }
        
        [Fact]
        public void GetMileageReturnsCorrectResult()
        {
            ride.InitialMileage = 1000;
            ride.FinalMileage = 1020;
            int? distance = ride.GetDistance();
            Assert.Equal(20, distance);
        }

        [Fact]
        public void GetDistanceShouldReturnInvalidOperationExceptionWhenInitialMileageIsNull()
        {
            Ride ride = new Ride();
            ride.FinalMileage = 1020;
            Assert.Throws<InvalidOperationException>(() => ride.GetDistance());
        }

    }
}
