using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Swagger.Net.Annotations;
using TuroApi.Models;

namespace TuroApi.Controllers
{
    public class StatsController : BaseController
    {
        /// <summary>
        /// Get car statistics in a given location
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CarStats))]
        public async Task<IHttpActionResult> Get([FromUri] Query q)
        {
            var cars = await TuroSearch(q.location, q.make, q.model);
            return Ok(new CarStats {
                total = cars.Count,
                year = cars.GroupBy(x => x.year).ToDict(),
                make = cars.GroupBy(x => x.make).ToDict(),
                model = cars.GroupBy(x => x.model).ToDict(),
            });
        }
    }
}
