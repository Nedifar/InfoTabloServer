using Aspose.Cells;
using ClosedXML.Excel;
using HtmlAgilityPack;
using InfoTabloServer.ViewModels;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace InfoTabloServer.LastDanceResources
{
    public class Shedule
    {
        public static XLWorkbook _workbook;
        public int upDay = 0;
        public int downDay = 0;
        public DateTime DupDay;
        public DateTime DdownDay;
        public string downMonth;
        public string upMonth;
        private DateTime selectedDate;
        public static Dictionary<DateTime, (IXLWorksheet, DateTime)> dictSpecial = new();

        public Shedule(DateTime date)
        {
            selectedDate = date;
            this.DateOut(date);
        }

        public ListPack SpecialSheduleReturn() ////???:)
        {
            string smallMonth = upMonth.Substring(0, 3);
            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/_{downDay}_{upDay}_{smallMonth}.xlsx";
            if (File.Exists(path))
            {
                foreach (var dd in dictSpecial)
                {
                    if (dd.Value.Item2.AddHours(1) < DateTime.Now)
                    { dictSpecial.Remove(dd.Key); }
                }

                var worksheet = new XLWorkbook(path).Worksheets.First();

                if (!dictSpecial.TryAdd(selectedDate.Date, (worksheet, DateTime.Now)))
                {
                    dictSpecial[selectedDate.Date].Item2.AddHours(1);
                }

                ListPack newListPack = new();

                CabinetShedule cabinetShedule = new(worksheet);
                newListPack.Cabinets = cabinetShedule.ListCabinets();

                TeacherShedule teacherShedule = new(worksheet);
                newListPack.Teachers = teacherShedule.ListTeachers();

                GroupShedule groupShedule = new(worksheet);
                newListPack.Groups = groupShedule.ListGroups();

                return newListPack;
            }
            return null;
        }

        public void DateOut(DateTime dt) //метод для определения начала и конца недели
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    downDay = dt.AddDays(1).Day;
                    upDay = dt.AddDays(6).Day;
                    DupDay = dt.AddDays(6);
                    DdownDay = dt.AddDays(1);
                    break;
                case DayOfWeek.Monday:
                    downDay = dt.Day;
                    upDay = dt.AddDays(5).Day;
                    DupDay = dt.AddDays(5);
                    DdownDay = dt;
                    break;
                case DayOfWeek.Tuesday:
                    downDay = dt.AddDays(-1).Day;
                    upDay = dt.AddDays(4).Day;
                    DupDay = dt.AddDays(4);
                    DdownDay = dt.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    downDay = dt.AddDays(-2).Day;
                    upDay = dt.AddDays(3).Day;
                    DupDay = dt.AddDays(3);
                    DdownDay = dt.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    downDay = dt.AddDays(-3).Day;
                    upDay = dt.AddDays(2).Day;
                    DupDay = dt.AddDays(2);
                    DdownDay = dt.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    downDay = dt.AddDays(-4).Day;
                    upDay = dt.AddDays(1).Day;
                    DupDay = dt.AddDays(1);
                    DdownDay = dt.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    downDay = dt.AddDays(-5).Day;
                    upDay = dt.Day;
                    DupDay = dt;
                    DdownDay = dt.AddDays(-5);
                    break;
            }
            downMonth = Mouth(DdownDay);
            upMonth = Mouth(DupDay);
        }

        public string Mouth(DateTime date)
        {
            var lines = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/Month.txt");
            string[] massiv = lines.Split('\n');
            switch (date.Month)
            {
                case 9:
                    return massiv[8];
                case 10:
                    return massiv[9];
                case 11:
                    return massiv[10];
                case 12:
                    return massiv[11];
                case 1:
                    return massiv[0];
                case 2:
                    return massiv[1];
                case 3:
                    return massiv[2];
                case 4:
                    return massiv[3];
                case 5:
                    return massiv[4];
                case 6:
                    return massiv[5];
                case 7:
                    return massiv[6];
                case 8:
                    return massiv[7];
                default:
                    break;
            }
            return null;
        }

        public void DownloadFeatures(IXLWorksheet xi) //Метод для скачивания изменений
        {   
            string data = "";
            using (var stream = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie/allizm.txt"))
            {
                data = stream.ReadToEnd();
                stream.Close();
                DateTime dateIZM = selectedDate;
                int dayWeek = (int)selectedDate.DayOfWeek;
                using (WebClient web = new WebClient())
                {
                    CultureInfo culture = new CultureInfo("ru-RU");
                    HtmlDocument doc = new HtmlDocument();
                    var web1 = new HtmlWeb();
                    doc = web1.Load("https://oksei.ru/studentu/raspisanie_uchebnykh_zanyatij");
                    var nodes = doc.DocumentNode.SelectNodes("//*[@class='attachment a-xls']/p/a");

                    for (int i = 1; i <= dayWeek + 1; i++)
                    {
                        string selectedDateString = dateIZM.AddDays(i - dayWeek).ToString("d", culture);
                        string savePath = @$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\{selectedDateString}";

                        if (data.Contains($"{selectedDateString}.xlsx"))
                        {
                            continue;
                        }
                        else
                        {
                            var selectedDate = dateIZM.AddDays(i - dayWeek);
                            var nodeForNeedDate = nodes.FirstOrDefault(p => p.InnerText.Contains(selectedDateString)
                            || p.InnerText.Contains(selectedDate.ToString("dd.MM.yyyy")));
                            if (nodeForNeedDate == null)
                            {
                                continue;
                            }
                            else
                            {
                                try
                                {
                                    var href = nodeForNeedDate.Attributes["href"].Value;
                                    web.DownloadFile(@$"https://oksei.ru{href}", $"{savePath}.xls");
                                    Workbook workbook2 = new($"{savePath}.xls");
                                    workbook2.Save($"{savePath}.xlsx", SaveFormat.Xlsx);
                                    XLWorkbook xL = new ($"{savePath}.xlsx");
                                    RaspisanieIzm(xL, i, xi);

                                    data += $"{selectedDateString}.xlsx\n";
                                    using (StreamWriter streamWriter = new (@$"{AppDomain.CurrentDomain.BaseDirectory}Raspisanie\allizm.txt", false, System.Text.Encoding.Default))
                                    {
                                        streamWriter.Write(data);
                                        streamWriter.Close();
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                
            }
        }

        public static void RaspisanieIzm(XLWorkbook _workbook1, int h, IXLWorksheet ix) //Метод для перезаписи в расписании изменений
        {
            DateTime dateIZM = DateTime.Today;
            int dayWeek = (int)DateTime.Today.DayOfWeek;
            var worksheet = _workbook1.Worksheets.First();
            int worksheetColumns = worksheet.ColumnsUsed().Count();
            int worksheetRows = worksheet.RowsUsed().Count();
            int ixColumns = ix.ColumnsUsed().Count();

            for (int i = 1; i <= worksheetColumns; i++)
            {
                for (int j = 11; j <= worksheetRows + 10; j++)
                {
                    for (int l = 3; l <= ixColumns; l++)
                    {
                        if (ix.Cell(5, l).GetValue<string>() == worksheet.Cell(j, i).GetValue<string>())
                        {
                            bool a = false;
                            int g = 6;
                            for (int m = 1; m <= g; m++)
                            {
                                IXLCell leg = worksheet.Cell(j + m, i);
                                if (leg.Style.Font.FontSize >= 22 || leg.Value.ToString() == "" || a || leg.Value.ToString().Length == 4)
                                {
                                    if (ix.Cell(27, 2).Value.ToString() != "4")
                                        ix.Cell((6 * h) + m, l).Value = " ";
                                    else
                                        ix.Cell((6 * h) + m - 1, l).Value = " ";
                                    a = true;
                                }
                                else
                                {
                                    if (ix.Cell(27, 2).Value.ToString() != "4")
                                        ix.Cell((6 * h) + m, l).Value = worksheet.Cell(j + m, i).Value;
                                    else
                                        ix.Cell((6 * h) + m - 1, l).Value = worksheet.Cell(j + m, i).Value;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
