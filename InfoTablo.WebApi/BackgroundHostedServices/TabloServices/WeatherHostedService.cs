using InfoTabloServer.SiganlR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace InfoTabloServer.BackgroundServices
{
    public class WeatherHostedService : BackgroundService
    {
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        public WeatherHostedService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache)
        {
            _hubContext = hubContext;
            cache = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var http = new HttpClient())
                    {
                        var response = await http.GetAsync("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Orenburg?unitGroup=metric&include=current&key=WBQY6NYB9YGMHSZHQYVEHLWBJ&contentType=json");
                        response.EnsureSuccessStatusCode();
                        var result = response.Content.ReadAsStringAsync().Result;
                        var jsonParse = JObject.Parse(result);
                        string s = (string)jsonParse["currentConditions"]["temp"];
                        string actualWeather = s.Split('.')[0] + "°C";
                        cache.Set("weather", actualWeather, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) });
                    }
                    await _hubContext.Clients.All.SendAsync("SendWeather", cache.Get("weather"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\tWeatherHostedService");
                }

                await Task.Delay(1000 * 15 * 60);
            }
        }
    }
}
