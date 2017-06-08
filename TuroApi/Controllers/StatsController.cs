using System.Linq;
using System.Web.Http;
using TuroApi.Models;

namespace TuroApi.Controllers
{
    public class StatsController : BaseController
    {
        public IHttpActionResult Get(GeoPoint location, int items = 200)
        {
            var cars = TuroSearch(location, items);
            return Ok(new CarStats {
                total = cars.Count,
                year = cars.GroupBy(x => x.year).ToDict(),
                make = cars.GroupBy(x => x.make).ToDict(),
                model = cars.GroupBy(x => x.model).ToDict(),
            });
        }
    }
}
