using System.ComponentModel.DataAnnotations;

namespace InfoTabloServer.Models
{
    public class SpecialBackgroundPhoto
    {
        [Key]
        public int idSpecialBackgroundPhoto { get; set; }

        public DateTime? targetDate { get; set; }

        public string fileName { get; set; }
    }
}
