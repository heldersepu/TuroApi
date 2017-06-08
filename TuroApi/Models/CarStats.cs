using System.Collections.Generic;

namespace TuroApi.Models
{
    public class CarStats
    {
        public int total;
        public Dictionary<int, int> year;
        public Dictionary<string, int> make;
        public Dictionary<string, int> model;
    }
}