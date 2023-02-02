using ClosedXML.Excel;
using InfoTabloServer.ViewModels;
using System.Text.RegularExpressions;

namespace InfoTabloServer.LastDanceResources
{
    public class TeacherShedule
    {
        IXLWorksheet _worksheet;

        public TeacherShedule(IXLWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public List<string> ListTeachers()
        {
            bool exit = true;
            int columnsCount = _worksheet.ColumnsUsed().Count();
            int rowsCount = 100;
            List<string> outputTeachersList = new();

            for (int i = 3; i <= columnsCount; i++)
            {
                for (int j = 6; j <= rowsCount; j++)
                {
                    string result = _worksheet.Cell(j, i).GetValue<string>();
                    if (result != "" && result != " " && result.Length > 3)
                    {
                        if (result.Contains("ДОП"))
                        {
                            try
                            {
                                string[] massiv = result.Split(new char[] { '(', ')' });
                                result = massiv[1].Trim();
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            try
                            {
                                if (result.Length == 4)
                                {
                                    continue;
                                }
                                string[] massiv = result.Split('\n');
                                if (massiv.Length == 1)
                                { continue; }
                                result = massiv[1].Trim();
                            }
                            catch
                            { continue; }
                        }
                        if (result != "-" && result != "")
                        {
                            Regex regex = new Regex(@"[а-яА-Я]+\s[А-Я]{1}\.[А-Я]{1}\.?$");
                            if (regex.IsMatch(result))
                            {
                                foreach (string output in outputTeachersList)
                                {
                                    if (output == result)
                                    {
                                        exit = false;
                                        break;
                                    }
                                }
                                if (exit)
                                {
                                    outputTeachersList.Add(result);
                                }
                                exit = true;
                            }
                        }
                    }
                }
            }
            outputTeachersList.Sort();
            return outputTeachersList;
        }

        public List<DayWeekClass> GetSheduleTeacher(int row, string teach)
        {
            bool exit = false;
            int number = 1;
            int columnsCount = _worksheet.ColumnsUsed().Count();
            List<DayWeekClass> teachers = new List<DayWeekClass>();
            for (int i = row; i < row + 6; i++)
            {
                for (int j = 3; j <= columnsCount; j++)
                {
                    string result = _worksheet.Cell(i, j).GetValue<string>();
                    string cab = "";
                    if (result.Contains(teach))
                    {
                        if (result != "" && result != " " && result.Length > 3)
                        {
                            cab = result.Remove(0, result.Length - 3).Trim();
                            if (cab != "-" && cab != "" && cab.Length > 1)
                            {
                                Regex regex = new Regex("(^[0-9]{3}$)|(^[0-9]{2}[а-яА-Я]{0,1}$)");
                                if (!regex.IsMatch(cab))
                                {
                                    cab = "";
                                }
                            }
                        }
                        teachers.Add(new DayWeekClass
                        {
                            Number = number,
                            cabinet = cab,
                            Day = result + $"\n{_worksheet.Cell(5, j).GetValue<string>()}"
                        });
                        exit = false;
                        break;
                    }
                    else
                        exit = true;
                }
                if (exit)
                    teachers.Add(new DayWeekClass
                    {
                        Number = number,
                        Day = "-"
                    });
                number++;
            }
            return teachers;
        }
    }
}
