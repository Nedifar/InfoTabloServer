using ClosedXML.Excel;
using InfoTabloServer.ViewModels;
using System.Text.RegularExpressions;

namespace InfoTabloServer.LastDanceResources
{
    public class CabinetShedule
    {
        IXLWorksheet _worksheet;

        public CabinetShedule(IXLWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public List<string> ListCabinets()
        {
            bool exit = true;
            List<string> outputCabinetsList = new();
            int columnsCount = _worksheet.ColumnsUsed().Count();
            int rowsCount = 100;

            for (int i = 3; i <= columnsCount; i++)
            {
                for (int j = 6; j <= rowsCount; j++)
                {
                    string result = _worksheet.Cell(j, i).GetValue<string>();
                    if (result != "" && result != " " && result.Length > 3)
                    {
                        result = result.Remove(0, result.Length - 3).Trim();
                        if (result != "-" && result != "" && result.Length > 1)
                        {
                            Regex regex = new Regex("(^[0-9]{3}$)|(^[0-9]{2}[а-яА-Я]{0,1}$)");
                            if (regex.IsMatch(result))
                            {
                                foreach (string output in outputCabinetsList)
                                {
                                    if (output == result)
                                    {
                                        exit = false;
                                        break;
                                    }
                                }
                                if (exit)
                                {
                                    outputCabinetsList.Add(result);
                                }
                                exit = true;
                            }
                        }
                    }
                }
            }
            outputCabinetsList.Sort();
            return outputCabinetsList;
        }

        public List<DayWeekClass> GetSheduleCabinet(int row, string cabinet) 
        {
            bool exit = false;
            int number = 1;
            int columnsCount = _worksheet.ColumnsUsed().Count();
            List<DayWeekClass> cabinets = new List<DayWeekClass>();
            try
            {
                for (int i = row; i < row + 6; i++)
                {
                    for (int j = 3; j <= columnsCount; j++)
                    {
                        string result = _worksheet.Cell(i, j).GetValue<string>();
                        if (result.Contains(cabinet))
                        {
                            var newDayWeek = new DayWeekClass { 
                                Number = number, 
                                cabinet = cabinet, 
                                Day = result + $"\n{_worksheet.Cell(5, j).GetValue<string>()}", 
                                groupMobile = _worksheet.Cell(5, j).GetValue<string>() 
                            };

                            cabinets.Add(newDayWeek);
                            exit = false;
                            break;
                        }
                        else
                            exit = true;
                    }
                    if (exit)
                        cabinets.Add(new DayWeekClass { Number = number, Day = "-", cabinet = cabinet });
                    number++;
                }
            }
            catch { }
            return cabinets;
        }
    }
}
