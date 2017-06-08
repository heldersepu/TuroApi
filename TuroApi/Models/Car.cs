using System;

namespace TuroApi.Models
{
    public class Car
    {
        public string make;
        public string model;
        public int year;
        public int tripsTaken;
        public double dailyPrice;
        public DateTime createdTime;

        public double tripsPerMonth
        {
            get => 30 * tripsTaken / daysSinceCreated;
        }

        private double daysSinceCreated
        {
            get => (DateTime.Now - createdTime).TotalDays;
        }
    }
}