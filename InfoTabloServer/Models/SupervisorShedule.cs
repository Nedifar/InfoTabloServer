using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTabloServer.Models
{
    public class SupervisorShedule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idSupervisorShedule { get; set; }

        public string NameSupervisor { get; set; }

        public string position { get; set; }

        public virtual List<MonthYear> MonthYears { get; set; } = new List<MonthYear>();

        public virtual List<DatesSupervisior> DatesSupervisiors { get; set; } = new List<DatesSupervisior>();
    }
}
