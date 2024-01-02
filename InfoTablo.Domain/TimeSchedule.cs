namespace InfoTablo.Domain
{
    public class TimeSchedule
    {
        public int IdTimeSchedule { get; set; }

        public string Name { get; set; }

        public virtual List<Para> Paras { get; set; } = new List<Para>();
    }
}
