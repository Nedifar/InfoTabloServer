using Aspose.Cells;
using ClosedXML.Excel;
using HtmlAgilityPack;
using InfoTabloServer.LastDanceResources;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace InfoTabloServer.BackgroundServices.SheduleHostedServices
{
    public class NewSheduleHostedService : BackgroundService
    {
        private IMemoryCache cache;

        public NewSheduleHostedService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    XLWorkbook xl = null;
                    Shedule sheduleNextWeek = new(DateTime.UtcNow.AddHours(5).AddDays(7));
                    string smallMonthNextWeek = sheduleNextWeek.upMonth.Substring(0, 3);
                    Shedule shedule = new(DateTime.UtcNow.AddHours(5));
                    string smallMonth = shedule.upMonth.Substring(0, 3);
                    string nextWeekPath = @$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/_{sheduleNextWeek.downDay}_{sheduleNextWeek.upDay}_{smallMonthNextWeek}";
                    string path = @$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/_{shedule.downDay}_{shedule.upDay}_{smallMonth}";

                    if (File.Exists(path + ".xlsx"))
                    {
                        if (File.Exists(nextWeekPath + ".xlsx"))
                        {
                            xl = new XLWorkbook(nextWeekPath + ".xlsx");
                            cache.Set("xLNew", xl.Worksheets.First(), new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
                        }
                        else
                        {
                            HtmlDocument doc = new HtmlDocument();
                            var web1 = new HtmlWeb();
                            doc = web1.Load("https://oksei.ru/studentu/raspisanie_uchebnykh_zanyatij");
                            var href = String.Empty;
                            var value = String.Empty;
                            using (var stream = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/Formats.txt"))
                            {
                                string l = stream.ReadToEnd();
                                stream.Close();
                                var nodes = doc.DocumentNode.SelectNodes("//*[@class='container bg-white p-25 box-shadow-right radius mt50']/div/div/p/a");
                                foreach (var node in nodes.Reverse())
                                {
                                    if (node.InnerText.Contains("верх") || node.InnerText.Contains("нижн"))
                                    {
                                        if (node.InnerText == l)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            href = node.Attributes["href"].Value;
                                            value = node.InnerText;
                                            using (WebClient web = new())
                                            {
                                                web.DownloadFile($"https://oksei.ru{href}", nextWeekPath + ".xls");
                                                Workbook workbook2 = new($"{nextWeekPath}.xls");
                                                workbook2.Save($"{nextWeekPath}.xlsx", SaveFormat.Xlsx);
                                                xl = new XLWorkbook(nextWeekPath + ".xlsx");
                                                cache.Set("xLNew", xl.Worksheets.First(), new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
                                                using (var streame = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/Formats.txt", false))
                                                {
                                                    streame.Write(value);
                                                    streame.Close();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (xl is not null)
                            {
                                var workSheet = xl.Worksheets.First();
                                await Task.Run(() =>
                                {
                                    var resultOfCabinets = new CabinetShedule(workSheet).ListCabinets();
                                    cache.Set("NewListCabinets", resultOfCabinets);
                                });

                                await Task.Run(() =>
                                {
                                    var resultOfTeachers = new TeacherShedule(workSheet).ListTeachers();
                                    cache.Set("NewListTeachers", resultOfTeachers);
                                });

                                await Task.Run(() =>
                                {
                                    var resultOfGroups = new GroupShedule(workSheet).ListGroups();
                                    cache.Set("NewListGroups", resultOfGroups);
                                });
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message + "\nNewSheduleHostedService");
                }
                await Task.Delay(10000 * 10);
            }
        }
    }
}
