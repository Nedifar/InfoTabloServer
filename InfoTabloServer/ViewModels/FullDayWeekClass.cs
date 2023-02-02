using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTabloServer.ViewModels
{
    public class FullDayWeekClass
    {
        public string dayWeekName { get; set; }
        public List<DayWeekClass> dayWeekClasses { get; set; } = new List<DayWeekClass>();
    }
}
