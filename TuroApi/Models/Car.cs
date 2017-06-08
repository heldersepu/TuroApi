using System;

namespace TuroApi.Models
{
    public class Car
    {
        public long id;
        public string make;
        public string model;
        public int year;
        public int tripsTaken;
        public double dailyPrice;
        public DateTime createdTime;

        public double tripsPerMonth
        {
            get { return 30 * tripsTaken / daysSinceCreated; }
        }

        private double daysSinceCreated
        {
            get { return (DateTime.Now - createdTime).TotalDays; }
        }
    }
}