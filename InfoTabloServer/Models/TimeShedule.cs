using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTabloServer.Models
{
    public class TimeShedule
    {
        [Key]
        public int idTimeShedule { get; set; }

        public string Name { get; set; }

        public virtual List<Para> Paras { get; set; } = new List<Para>();

        public DateTime begin(int number, string type)
        {
            var selectedPara = Paras.Where(p => p.numberInterval == number && p.TypeInterval.name == type).FirstOrDefault();
            return selectedPara.begin;
        }

        public DateTime end(int number, string type)
        {
            var selectedPara = Paras.Where(p => p.numberInterval == number && p.TypeInterval.name == type).FirstOrDefault();
            return selectedPara.end;
        }

        public double times()
        {
            return (Paras.LastOrDefault().end.TimeOfDay - Paras[0].begin.TimeOfDay).TotalMinutes;
        }

        [NotMapped]
        public List<Para> OnlyParas
        {
            get
            {
                return Paras.Where(p => p.TypeInterval.name == "ЧКР" || p.TypeInterval.name == "Пара").ToList();
            }
        }

        public Para paraNow()
        {
            return Paras.OrderBy(p => p.numberInList).Where(p => p.begin.TimeOfDay <= DateTime.Now.TimeOfDay && p.end.TimeOfDay >= DateTime.Now.TimeOfDay).FirstOrDefault();
        }

        public Para onlyParaNow()
        {
            return OnlyParas.OrderBy(p => p.numberInList).Where(p => p.end.TimeOfDay >= DateTime.Now.TimeOfDay).FirstOrDefault();
        }

        public double TotalTime()
        {
            double total = 0;
            foreach (var para in Paras)
            {
                total += para.totalTime;
            }
            return total;
        }
    }
}
