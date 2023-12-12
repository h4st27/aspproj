using Microsoft.AspNetCore.Mvc;
using MyApp.Services.WeatherService;
using MyApp.Models;

namespace MyApp.ViewComponents
{
    public class WeatherViewComponent:ViewComponent
    {
        private readonly IWeatherService _weatherService;

        public WeatherViewComponent(IWeatherService myService)
        {
            _weatherService = myService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Coord coord)
        {
            WeatherData weather = await _weatherService.GetWeatherAsync(coord.lat, coord.lon);
            return View(weather);
        }

    }
}
