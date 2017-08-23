using Swagger.Net.Annotations;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(BadRequestErrorMessageResult))]
        public IHttpActionResult Get(GeoPoint location)
        {
            if (location == null)
                return BadRequest("Invalid location.");
            return Ok($"Latitude={location.Latitude}, Longitude={location.Longitude}");
        }

        [KeyAuthorize]
        public IHttpActionResult Post()
        {
            return Ok("Testing API Key Authentication in header");
        }
    }
}
