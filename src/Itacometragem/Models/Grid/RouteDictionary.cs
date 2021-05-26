using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class RouteDictionary : Dictionary<string, string>
    {
        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        public int PageNumber
        {
            get => Convert.ToInt32(Get(nameof(GridDTO.PageNumber)));
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Convert.ToInt32(Get(nameof(GridDTO.PageSize)));
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string DriverFilter
        {
            get => Get(nameof(RidesGridDTO.Driver))?.Replace("driver-", "");
            set => this[nameof(RidesGridDTO.Driver)] = value;
        }

        public string MotiveFilter
        {
            get => Get(nameof(RidesGridDTO.Motive))?.Replace("motive-", "");
            set => this[nameof(RidesGridDTO.Motive)] = value;
        }

        public string CarFilter
        {
            get => Get(nameof(RidesGridDTO.Car))?.Replace("car-", "");
            set => this[nameof(RidesGridDTO.Car)] = value;
        }

        public string InitialDate
        {
            get => Get(nameof(RidesGridDTO.InitialDate)).Replace("initial-", "");
            set => this[nameof(RidesGridDTO.InitialDate)] = value;
        }

        public string FinalDate
        {
            get => Get(nameof(RidesGridDTO.FinalDate)).Replace("final-", "");
            set => this[nameof(RidesGridDTO.FinalDate)] = value;
        }


        public RouteDictionary Clone()
        {
            RouteDictionary clone = new RouteDictionary();
            foreach (string key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }

    }
}
