namespace Core.InfoTablo.Domain
{
    public class Announcment
    {
        public int IdAnnouncement { get; set; }

        public string Header { get; set; }

        public string Name { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateClosed { get; set; }

        public string Priority { get; set; }

        public bool IsActive { get; set; }

        public string Status { get; set; }
    }
}
