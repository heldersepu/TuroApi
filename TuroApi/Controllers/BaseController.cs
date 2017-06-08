using RestSharp;
using System;
using System.Web.Http;
using Newtonsoft.Json;
using TuroApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TuroApi.Controllers
{
    abstract public class BaseController : ApiController
    {
        const string DOMAIN = "https://turo.com/";

        private class TuroRequest : RestRequest
        {
            public TuroRequest(GeoPoint location, int items, string make, string model) : base("api/search", Method.GET)
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
                AddParameter("maximumDistanceInMiles", "300");
                AddParameter("sortType", "RELEVANCE");

                AddParameter("isMapSearch", "false");
                AddParameter("defaultZoomLevel", "14");
                AddParameter("international", "true");

                AddHeader("Referer", $"{DOMAIN}search");
            }
        }

        protected List<Car> TuroSearch(GeoPoint location, int items, string make = null, string model = null)
        {
            var client = new RestClient(DOMAIN);
            var request = new TuroRequest(location, items, make, model);
            var resp = client.Execute(request);
            dynamic data = JsonConvert.DeserializeObject(resp.Content);

            var cars = new List<Car>();
            foreach (var item in data.list)
            {
                cars.Add(new Car
                {
                    make = item.vehicle.make,
                    model = item.vehicle.model,
                    year = (int)item.vehicle.year,
                    tripsTaken = (int)item.renterTripsTaken,
                    dailyPrice = (double)item.rate.averageDailyPrice,
                    createdTime = FromUnixTime((double)item.vehicle.listingCreatedTime)
                });
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
