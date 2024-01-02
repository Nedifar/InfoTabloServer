using InfoTablo.Application.Interfaces;
using Newtonsoft.Json.Linq;

namespace InfoTablo.Persistence
{
    public class WeatherService : IWeatherService
    {
        private const string UNITGROUP = "metric";
        private const string INCLUDE = "current";
        private const string KEY = "WBQY6NYB9YGMHSZHQYVEHLWBJ";
        private const string CONTENTTYPE = "json";

        public async Task<string> GetActualWeather()
        {
            using (var http = new HttpClient())
            {
                var url = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Orenburg" +
                    $"?unitGroup={UNITGROUP}&include={INCLUDE}&key{KEY}=&contentType={CONTENTTYPE}";

                var response = await http.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var jsonParse = JObject.Parse(result);
                string psevdoTemp = (string)jsonParse["currentConditions"]["temp"];
                string actualWeather = psevdoTemp.Split('.')[0] + "°C";

                return actualWeather;
            }
        }
    }
}
