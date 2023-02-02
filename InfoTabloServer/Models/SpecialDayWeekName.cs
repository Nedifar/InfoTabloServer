using System.ComponentModel.DataAnnotations;

namespace InfoTabloServer.Models
{
    public class SpecialDayWeekName
    {
        [Key]
        public int idSpecialDayWeekName { get; set; }
        public int dayWeek { get; set; }
    }
}
