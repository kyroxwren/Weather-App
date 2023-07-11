
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
namespace FA4SLD.Controllers
{
    public class WeatherController : Controller
    {
        private const string API_KEY = "e7d9515a7825db20ab8ed3f385690e5f";
        private const string API_BASE_URL = "https://api.openweathermap.org/data/2.5/weather";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeatherInfo(string city)
        {
            using (var client = new HttpClient())
            {
                string apiUrl = $"{API_BASE_URL}?q={city}&appid={API_KEY}";

                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var weather = JsonConvert.DeserializeObject<WeatherResponse>(content);

                    weather.Main.Temp = weather.Main.Temp - 273.15;

                    return View("WeatherInfo", weather);
                }
                else
                {
                    return View("Error");
                }
            }
        }
    }

    public class WeatherResponse
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
        public WindData Wind { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WindData
    {
        public double Speed { get; set; }
        public int Degree { get; set; }
    }
}
