using System.ComponentModel.DataAnnotations;

namespace InfoTabloServer.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        public string Name { get; set; }

        public string PasHash { get; set; }
    }
}
