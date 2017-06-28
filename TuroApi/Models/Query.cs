using System.ComponentModel.DataAnnotations;

namespace TuroApi.Models
{
    /// <summary>Query object</summary>
    public class Query
    {
        /// <summary>SoFL= 26.16,-80.20</summary>
        /// <example>26.16,-80.20</example>
        [Required()]
        public GeoPoint location { get; set; }

        /// <summary>Car make</summary>
        /// <example>Tesla</example>
        public string make { get; set; }

        /// <summary>Car model</summary>
        /// <example>Model S</example>
        public string model { get; set; }
    }
}