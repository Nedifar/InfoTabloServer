using ClosedXML.Excel;
using InfoTabloServer.ViewModels;
using System.Text.RegularExpressions;

namespace InfoTabloServer.LastDanceResources
{
    public class GroupShedule
    {
        IXLWorksheet _worksheet;

        public GroupShedule(IXLWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public List<string> ListGroups()
        {
            List<string> outputGroupsList = new List<string>();

            for (int i = 3; i <= _worksheet.ColumnsUsed().Count(); i++)
            {
                if (!String.IsNullOrWhiteSpace(_worksheet.Cell(5, i).GetValue<string>()))
                    outputGroupsList.Add(_worksheet.Cell(5, i).GetValue<string>());
            }
            outputGroupsList.Sort();
            return outputGroupsList;
        }

        public List<DayWeekClass> GetSheduleGroup(int row, int column)
        {
            int count = 1;
            List<DayWeekClass> dayWeeks = new List<DayWeekClass>();
            for (int i = row; i < row + 6; i++)
            {
                string result = _worksheet.Cell(i, column).GetValue<string>();

                var metric = new DayWeekClass
                {
                    Number = count,
                    Day = result
                };

                if (result != "" && result != " " && result.Length > 3)
                {
                    result = result.Remove(0, result.Length - 3).Trim();
                    if (result != "-" && result != "" && result.Length > 1)
                    {
                        Regex regex = new Regex("(^[0-9]{3}$)|(^[0-9]{2}[а-яА-Я]{0,1}$)");
                        if (regex.IsMatch(result))
                        {
                            metric.cabinet = result;
                        }
                    }
                }

                count++;
                dayWeeks.Add(metric);
            }
            return dayWeeks;
        }

        public int IndexGroup(string group)
        {
            int columnsCount = _worksheet.ColumnCount();
            for (int i = 1; i < columnsCount; i++)
            {
                if (_worksheet.Cell(5, i).GetValue<string>() == group)
                {
                    return i;
                }
                else
                    continue;
            }
            return 0;
        }
    }
}
