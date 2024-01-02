using Aspose.Cells;
using ClosedXML.Excel;
using HtmlAgilityPack;
using InfoTabloServer.Context;
using InfoTabloServer.LastDanceResources;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace InfoTabloServer.BackgroundServices.SheduleHostedServices
{
  public class MainSheduleHostedService : BackgroundService
  {
    private IMemoryCache cache;
    private context context;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MainSheduleHostedService(IMemoryCache cache, IServiceScopeFactory serviceScopeFactory)
    {
      this.cache = cache;
      _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        try
        {
          var scope = _serviceScopeFactory.CreateScope();
          var _terminalService = scope.ServiceProvider.GetRequiredService<context>();
          context = _terminalService;
          Shedule shedule = new(DateTime.UtcNow.AddHours(5));
          XLWorkbook xL = null;
          string smallMonth = shedule.upMonth.Substring(0, 3);
          HtmlDocument doc = new HtmlDocument();
          string path = @$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/_{shedule.downDay}_{shedule.upDay}_{smallMonth}";
          var web1 = new HtmlWeb();
          doc = web1.Load("https://oksei.ru/studentu/raspisanie_uchebnykh_zanyatij");
          if (File.Exists(path + ".xlsx"))
          {
            xL = new XLWorkbook(path + ".xlsx");
          }
          else
          {
            var nodes = doc.DocumentNode.SelectNodes("//*[@class='container bg-white p-25 box-shadow-right radius mt50']/div/div/p/a");
            foreach (var node in nodes.Reverse())
            {
              if (node.InnerText.Contains("верх") || node.InnerText.Contains("нижн"))
              {
                WebClient web = new();
                var href = node.Attributes["href"].Value;
                var value = node.InnerText;
                web.DownloadFile($"https://oksei.ru{href}", path + ".xls");
                Workbook workbook2 = new($"{path}.xls");
                workbook2.Save($"{path}.xlsx", SaveFormat.Xlsx);
                xL = new XLWorkbook($"{path}.xlsx");
                cache.Set("xLMain", xL.Worksheets.First(), new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
                using (var streame = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/Formats.txt", false))
                {
                  streame.Write(value);
                  streame.Close();
                }
                break;
              }
            }
          }
          IXLWorksheet workSheet = xL.Worksheets.First();
          shedule.DownloadFeatures(workSheet);
          xL.Save();
          cache.Set("xLMain", workSheet);
          await Task.Run(() =>
          {
            var resultOfCabinets = new CabinetShedule(workSheet).ListCabinets();
            cache.Set("MainListCabinets", resultOfCabinets);
          }); //список кабинетов

          await Task.Run(() =>
          {
            var resultOfTeachers = new TeacherShedule(workSheet).ListTeachers();
            context.Lessons.GroupBy(p => p.teacherName).ToList().ForEach(p =>
                      {
                if (p.Key != null && p.Key.Trim() != "" && !resultOfTeachers.Any(s => s == p.Key))
                {
                  resultOfTeachers.Add(p.Key);
                }
              });
            cache.Set("MainListTeachers", resultOfTeachers.OrderBy(p => p).ToList());
          });

          await Task.Run(() =>
          {
            var resultOfGroups = new GroupShedule(workSheet).ListGroups();
            cache.Set("MainListGroups", resultOfGroups);
          });
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message + "\nMainSheduleHostedService");
        }
        await Task.Delay(10000 * 10);
      }
    }
  }
}
