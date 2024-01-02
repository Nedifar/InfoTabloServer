namespace InfoTablo.Domain
{
    public class TypeInterval
    {
        public int IdInterval { get; set; }

        public string Name { get; set; }

        public virtual List<Para> Paras { get; set; } = new List<Para>();
    }
}
