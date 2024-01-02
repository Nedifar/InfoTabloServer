namespace InfoTablo.Application.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetActualWeather();
    }
}
