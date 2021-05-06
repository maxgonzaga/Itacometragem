using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itacometragem.Models
{
    public class GridBuilder
    {
        protected RouteDictionary _routes;

        public GridBuilder(GridDTO values)
        {
            _routes = new RouteDictionary();
            _routes.PageNumber = values.PageNumber;
            _routes.PageSize = values.PageSize;
        }

        public int GetTotalPages(int count)
        {
            int size = _routes.PageSize;
            return (count + size - 1) / size;
        }

        public RouteDictionary Route => _routes;
    }
}
