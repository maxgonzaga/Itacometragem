namespace Itacometragem.Models
{
    public class RidesGridDTO : GridDTO
    {
        public string Driver { get; set; }
        public string Motive { get; set; }
        public string Car { get; set; }
        public string InitialDate { get; set; } = "none";
        public string FinalDate { get; set; } = "none";

        public RidesGridDTO()
        {
            Driver = Motive = Car = "all";
            InitialDate = FinalDate = "none";
        }

        public RidesGridDTO(FilterData filterData)
        {
            Driver = filterData.DriverId;
            Car = filterData.CarId;
            Motive = filterData.MotiveId;
            InitialDate = filterData.InitialDate ?? "none";
            FinalDate = filterData.FinalDate ?? "none";
        }
    }
}
