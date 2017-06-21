using System.ComponentModel.DataAnnotations;

namespace TuroApi.Models
{
    /// <summary>Query object</summary>
    public class Query
    {
        /// <summary>SoFL= 26.16,-80.20</summary>
        [Required()]
        public GeoPoint location { get; set; }

        /// <summary>Car make</summary>
        public string make  { get; set; }

        /// <summary>Car model</summary>
        public string model  { get; set; }
    }
}