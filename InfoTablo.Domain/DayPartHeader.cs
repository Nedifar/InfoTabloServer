namespace InfoTablo.Domain
{
    public class DayPartHeader
    {
        public int DayPartHeaderId { get; set; }

        public string Header { get; set; } = "Memories";

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
