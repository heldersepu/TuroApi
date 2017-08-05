using System.Web.Http;
using TuroApi.Models;

namespace TuroApi.Controllers
{
    public class EchoController : ApiController
    {
        // GET: api/Echo
        /// <summary>
        /// Echo test for the location
        /// </summary>
        /// <param name="location">SoFL= 26.16,-80.20</param>
        /// <returns>String with Latitude and Longitude</returns>
        public string Get(GeoPoint location)
        {
            return $"Latitude={location.Latitude}, Longitude={location.Longitude}";
        }
    }
}
