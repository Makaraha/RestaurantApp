using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;


namespace RestaurantApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IRepository<MeasurementUnit> _repository;

        public WeatherForecastController(IRepository<MeasurementUnit> repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task Test()
        {
            var list = await _repository.ListAsync();
        }
    }
}
