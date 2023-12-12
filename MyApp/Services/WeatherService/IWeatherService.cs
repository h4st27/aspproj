using MyApp.Models;

namespace MyApp.Services.WeatherService
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(double latitude, double longitude);
    }
}