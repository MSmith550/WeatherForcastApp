using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForcastProgram
{
    public class ForcastWeatherData
    {
        public Location location { get; set; }
        public Forecast forecast { get; set; }
        // Add more properties as needed to represent other parts of the JSON response
    }

    public class Forecast
    {
        public List<ForecastDay> forecastday { get; set; }
    }

    public class ForecastDay
    {
        public DateTime date { get; set; }
        public Day day { get; set; }
    }

    public class Day
    {
        public double maxtemp_f { get; set; }
        public double mintemp_f { get; set; }
        public WeatherCondition condition { get; set; }
    }
}
