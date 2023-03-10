using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InfoTabloServer.Models
{
    public class TypeInterval
    {
        [Key]
        public int idInterval { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public virtual List<Para> Paras { get; set; } = new List<Para>();
    }
}
