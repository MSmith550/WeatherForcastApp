using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForcastProgram
{
    public class WeatherData
    {
        public Location location { get; set; }
        public Current current { get; set; }
        // Add more properties as needed to represent other parts of the JSON response
    }

    public class Location
    {
        public string name { get; set; }
        public string country { get; set; }
        // Add more properties as needed to represent location data
    }

    public class Current
    {
        public double temp_f { get; set; }
        public WeatherCondition condition { get; set; }
        // Add more properties as needed to represent current weather data
    }

    public class WeatherCondition
    {
        public string text { get; set; }
        public string icon { get; set; }
    }
}
