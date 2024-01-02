namespace InfoTablo.Domain
{
    public class SupervisiorSchedule
    {
        public int IdSupervisiorSchedule { get; set; }

        public string NameSupervisior { get; set; }

        public string Position { get; set; }

        public virtual List<MonthYear> MonthYears { get; set; } = new List<MonthYear>();

        public virtual List<DatesSupervisior> DatesSupervisiors { get; set; } = new List<DatesSupervisior>();
    }
}
