namespace InfoTablo.WebApi.BackgroundHostedServices.HostedServiceDtos
{
    public class FloorCabinetDto
    {
        public string Name { get; set; }
        public List<DayWeekDto> DayWeeksDto { get; set; } = new();
    }
}
