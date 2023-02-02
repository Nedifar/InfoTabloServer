using System.ComponentModel.DataAnnotations;

namespace InfoTabloServer.Models
{
    public class WeekName
    {
        [Key]
        public int idWeekName { get; set; }

        public DateTime Begin { get; set; }

        public string Name { get; set; }
    }
}
