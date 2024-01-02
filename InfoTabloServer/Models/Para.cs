using InfoTabloServer.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InfoTabloServer.Models
{
  public class Para
  {
    [System.ComponentModel.DataAnnotations.Key]
    public int idPara { get; set; }
    public string Name { get; set; }
    public Guid guid { get; set; } = Guid.NewGuid();
    public DateTime begin { get; set; }
    public DateTime end { get; set; }
    public int numberInList { get; set; }
    public int numberInterval { get; set; }
    [ForeignKey("TypeInterval")]
    public int idTypeInterval { get; set; }
    public virtual TypeInterval TypeInterval { get; set; }
    [ForeignKey("TimeShedule")]
    public int idTimeShedule { get; set; }
    [JsonIgnore]
    public virtual TimeShedule TimeShedule { get; set; }

    public string outGraphicNewTablo
    {
      get
      {
        return TypeInterval.name switch
        {
          "Перемена" => "П",
          "Пара" => numberInterval.ToString(),
          "Событие" => Name,
          "ЧКР" => "ЧКР",
          _ => throw new Exception()
        };
      }
    }

    public double toEndTimeInProcent
    {
      get
      {
        return 100 - 100 * (end.TimeOfDay.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes) / totalTime;
      }
    }

    public string toEndTime
    {
      get
      {
        double b = Math.Ceiling(end.TimeOfDay.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes);
        return TimeToHourAndMinute(b, Math.Truncate(b / 60));
      }
    }

    public string TimeToHourAndMinute(double minute, double hour)
    {
      if (minute - hour * 60 == 0)
        return hour + " час ";
      else if (hour != 0)
        return hour + " час " + (minute - hour * 60) + " мин.";
      else
        return minute + " мин.";
    }

    public bool runningNow
    {
      get
      {
        if (toEndTime.Contains("-"))
          return true;
        return false;
      }
    }

    public string beginEnd
    {
      get
      {
        return begin.ToString("HH.mm") + "-" + end.ToString("HH.mm");
      }
    }

    public string drHeight()
    {
      return (totalTime + StripeHeight.height).ToString();
    }

    public double heightText //?????
    {
      get
      {
        if (totalTime == 5)
          return 2.5 * totalTime;
        return 1.5 * totalTime;
      }
    }

    public string fontSize
    {
      get
      {
        if (TypeInterval.name == "Пара")
        {
          return "3.5em";
        }
        else
          return "1em";
      }
    }

    public double totalTime
    {
      get
      {
        return (end.TimeOfDay - begin.TimeOfDay).TotalMinutes;
      }

    }
  }
}
