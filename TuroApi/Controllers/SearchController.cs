using Swagger.Net.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TuroApi.Models;

namespace TuroApi.Controllers
{
    public class SearchController : BaseController
    {
        /// <summary>
        /// Get all cars in a given location
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<Car>))]
        public async Task<IHttpActionResult> GetByLocation([FromUri] Query q)
        {
            var cars = await TuroSearch(q.location, q.make, q.model);
            return Ok(cars.OrderByDescending(c => c.tripsPerMonth));
        }
    }
}
