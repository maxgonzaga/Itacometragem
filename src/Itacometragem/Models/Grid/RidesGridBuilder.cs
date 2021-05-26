using Microsoft.AspNetCore.Http;

namespace Itacometragem.Models
{
    public class RidesGridBuilder : GridBuilder
    {
        public RidesGridBuilder(RidesGridDTO dto) : base(dto)
        {
            bool isInitial = dto.Motive.IndexOf("motive") == -1;
            _routes.DriverFilter = (isInitial) ? "driver-" + dto.Driver : dto.Driver;
            _routes.MotiveFilter = (isInitial) ? "motive-" + dto.Motive : dto.Motive;
            _routes.CarFilter = (isInitial) ? "car-" + dto.Car : dto.Car;
            _routes.InitialDate = (isInitial) ? "initial-" + dto.InitialDate : dto.InitialDate;
            _routes.FinalDate = (isInitial) ? "final-" + dto.FinalDate : dto.FinalDate;
        }

        public bool IsFilterByDriver => _routes.DriverFilter != "all";
        public bool IsFilterByMotive => _routes.MotiveFilter != "all";
        public bool IsFilterByCar => _routes.CarFilter != "all";
        public bool IsFilterByDate => _routes.InitialDate != "none" && _routes.FinalDate != "none";
    }
}
