using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTabloServer.ViewModels
{
    public class FloorCabinet
    {
        public string Name { get; set; }
        public List<DayWeekClass> DayWeeks { get; set; } = new List<DayWeekClass>();
    }
}
