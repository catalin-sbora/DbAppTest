using Azure.Core;
using DbAppTest.datasource;
using DbAppTest.model;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Azure.Identity;
using Azure.Core;



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
        public async Task<IEnumerable<WeatherEntry>> Get()
        {
           
            // Uncomment the following lines corresponding to the authentication type you want to use.
            // For system-assigned identity.
             var sqlServerTokenProvider = new DefaultAzureCredential();

            // For user-assigned identity.
            // var sqlServerTokenProvider = new DefaultAzureCredential(
            //     new DefaultAzureCredentialOptions
            //     {
            //         ManagedIdentityClientId = Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CLIENTID");
            //     }
            // );

            // For service principal.
            // var tenantId = Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_TENANTID");
            // var clientId = Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CLIENTID");
            // var clientSecret = Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CLIENTSECRET");
            // var sqlServerTokenProvider = new ClientSecretCredential(tenantId, clientId, clientSecret);

            // Acquire the access token. 
            AccessToken accessToken = await sqlServerTokenProvider.GetTokenAsync(
                new TokenRequestContext(scopes: new string[]
                {
                        "https://ossrdbms-aad.database.windows.net/.default"
                }));

            // Combine the token with the connection string from the environment variables provided by Service Connector.
            string connectionString =
                $"{Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CONNECTIONSTRING")};Password={accessToken.Token}";

            // Establish the connection.
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Console.WriteLine("Opening connection using access token...");
                connection.Open();
                Console.WriteLine("DATABASE CONNECTION OPENED");
            }
            return _context.WeatherEntries.ToList(); 
           
        }
    }
}
