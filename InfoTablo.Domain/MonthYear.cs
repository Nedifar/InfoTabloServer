namespace InfoTablo.Domain
{
    public class MonthYear
    {
        public int IdMonthYear { get; set; }

        public DateTime Date { get; set; }

        public virtual List<SupervisiorSchedule> SupervisorSchedules { get; set; } = new List<SupervisiorSchedule>();
    }
}
