using InfoTablo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTablo.Application.TimeShedules.Queries
{
    public class GetAllTimeScheduleVm
    {
        public IList<TimeSchedule> TimeSchedules { get; set; }
    }
}
