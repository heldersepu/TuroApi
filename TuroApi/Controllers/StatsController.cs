using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TuroApi.Models;

namespace TuroApi.Controllers
{
    public class StatsController : BaseController
    {
        public async Task<IHttpActionResult> Get(GeoPoint location, int items = 200, string make = null, string model = null)
        {
            var cars = await TuroSearch(location, items, make, model);
            return Ok(new CarStats {
                total = cars.Count,
                year = cars.GroupBy(x => x.year).ToDict(),
                make = cars.GroupBy(x => x.make).ToDict(),
                model = cars.GroupBy(x => x.model).ToDict(),
            });
        }
    }
}
