namespace InfoTablo.Domain
{
    public class Para
    {
        public int IdPara { get; set; }

        public string Name { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public int NumberInList { get; set; }

        public int NumberInterval { get; set; }

        public int IdTypeInterval { get; set; }

        public virtual TypeInterval TypeInterval { get; set; }

        public int IdTimeSchedule { get; set; }

        public virtual TimeSchedule TimeSchedule { get; set; }
    }
}
