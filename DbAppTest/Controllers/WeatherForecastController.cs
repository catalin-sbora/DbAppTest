using DbAppTest.datasource;
using DbAppTest.model;
using Microsoft.AspNetCore.Mvc;

namespace DbAppTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly WeatherAppDbContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(WeatherAppDbContext dbContext, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _context = dbContext;
            _context.Database.EnsureCreated();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherEntry> Get()
        {
            return _context.WeatherEntries.ToList(); 
           
        }
    }
}
