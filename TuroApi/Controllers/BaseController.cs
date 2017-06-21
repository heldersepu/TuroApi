using RestSharp;
using System;
using System.Web.Http;
using Newtonsoft.Json;
using TuroApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuroApi.Controllers
{
    abstract public class BaseController : ApiController
    {
        const string DOMAIN = "https://turo.com/";

        private class TuroRequest : RestRequest
        {
            public TuroRequest(GeoPoint location, int items, string make, string model, string sortType) : base("api/search", Method.GET)
            {
                AddParameter("itemsPerPage", items);
                AddParameter("latitude", location.Latitude);
                AddParameter("longitude", location.Longitude);

                if (make != null)
                    AddParameter("makes", make);

                if (model != null)
                    AddParameter("models", model);

                var date = DateTime.Now.AddDays(1);
                AddParameter("startDate", date.ToString("d"));
                AddParameter("startTime", date.ToString("H:m"));
                AddParameter("endDate", date.AddDays(7).ToString("d"));
                AddParameter("endTime", date.AddDays(7).ToString("H:m"));

                AddParameter("category", "ALL");
                AddParameter("maximumDistanceInMiles", "30");
                AddParameter("sortType", sortType);

                AddParameter("isMapSearch", "false");
                AddParameter("defaultZoomLevel", "14");
                AddParameter("international", "true");

                AddHeader("Referer", $"{DOMAIN}search");
            }
        }

        protected async Task<List<Car>> TuroSearch(GeoPoint location, string make = null, string model = null)
        {
            var restTasks = new List<Task<IRestResponse>>();
            var client = new RestClient(DOMAIN);

            string[] sorts = { "RELEVANCE", "PRICE_LOW", "PRICE_HIGH" };
            double[,] array = {{0,0},{0.2,0},{0,0.2},{-0.2,0},{0,-0.2}};
            for (int i = 0; i < 5; i++)
            {
                var loc = location.Add(array[i, 0], array[i, 1]);
                foreach (var sort in sorts)
                {
                    var request = new TuroRequest(loc, 400, make, model, sort);
                    restTasks.Add(client.ExecuteTaskAsync(request));
                }
            }

            var cars = new List<Car>();
            foreach (var restTask in restTasks)
            {
                var resp = await restTask;
                dynamic data = JsonConvert.DeserializeObject(resp.Content);
                foreach (var item in data.list)
                {
                    long id = (long)item.vehicle.id;
                    if (!cars.Any(x => x.id == id))
                    {
                        cars.Add(new Car
                        {
                            id = id,
                            make = item.vehicle.make,
                            model = item.vehicle.model,
                            year = (int)item.vehicle.year,
                            tripsTaken = (int)item.renterTripsTaken,
                            dailyPrice = (double)item.rate.averageDailyPrice,
                            createdTime = FromUnixTime((double)item.vehicle.listingCreatedTime)
                        });
                    }
                }
            }
            return cars;
        }

        public static DateTime FromUnixTime(double unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }
    }

    public static class IEnumerableExtension
    {
        public static Dictionary<TKey, int> ToDict<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> source)
        {
            return source.ToDictionary(p => p.Key, p => p.Count());
        }
    }
}
