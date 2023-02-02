using System.ComponentModel.DataAnnotations;

namespace InfoTabloServer.Models
{
    public class DayPartHeader
    {
        [Key]
        public int DayPartHeaderId { get; set; }

        public string Header { get; set; } = "Memories";

        public DateTime beginTime { get; set; }

        public DateTime endTime { get; set; }
    }
}
