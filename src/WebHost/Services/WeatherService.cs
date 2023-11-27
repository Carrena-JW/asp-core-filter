
using Microsoft.AspNetCore.Mvc;
using WebHost.Filters;

namespace WebHost.Services
{
    public interface IWeatherService
    {
        string GetWeatherName();
    }


    public class WeatherService
    {
        [TypeFilter(typeof(JustLoggingFilter))]
        public string GetWeatherName()
        {
            return "this is weather name";
        }
    }
}
