﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TuroApi.Models;

namespace TuroApi.Controllers
{
    public class SearchController : BaseController
    {
        // GET: api/Search
        public async Task<IHttpActionResult> GetByLocation(GeoPoint location, int items = 200, string make = null, string model = null)
        {
            var cars = await TuroSearch(location, items, make, model);
            return Ok(cars.OrderByDescending(c => c.tripsPerMonth));
        }
    }
}
