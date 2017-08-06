using System;
using System.ComponentModel;
using System.Globalization;

namespace TuroApi.Models
{
    [TypeConverter(typeof(GeoPointConverter))]
    public class GeoPoint
    {
        public GeoPoint() { }
        public GeoPoint(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public GeoPoint Add(double lat, double lon)
        {
            return new GeoPoint(Latitude + lat, Longitude + lon);
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static bool TryParse(string s, out GeoPoint result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            double lat, lon;
            if (double.TryParse(parts[0], out lat) &&
                double.TryParse(parts[1], out lon))
            {
                if (lat <= 90 && lat >= -90 &&
                    lon <= 180 && lon >= -180)
                {
                    result = new GeoPoint(lat, lon);
                    return true;
                }
            }
            return false;
        }
    }

    class GeoPointConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                GeoPoint point;
                if (GeoPoint.TryParse((string)value, out point))
                {
                    return point;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}