using ClosedXML.Excel;
using InfoTabloServer.LastDanceResources;
using InfoTabloServer.Models;
using InfoTabloServer.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace XUnitInfoTablo
{
    public class XUnitForInfoTabloServer
    {
        private readonly string[] teachersList = new string[] {
            "Аладина О.Н.","Андреева Л.В.", "Андреянова О.А.", "Балакин О.Д.",
                "Балышева Е.В.", "Белан В.Е.", "Беляев А.А.", "Бикимов А.Ж.", "Борисов В.В.", "Боровцова Е.В.", "Бухарова Э.Э.", "Вареников Л.А.",
                "Великороднова А.В.", "Гиниятулина Н.В.", "Гонышев И.А.", "Дубская Е.С.", "Егурнова Е.Н.","Жеколдина Р.Р.", "Жукова О.В.","Журба С.С.",
                "Зверева О.М.", "Зеленина С.В.", "Землянская К.В.", "Игликова А.Н.", "Илюсизова А.А.", "Имашева К.Б.", "Каблукова С.Ю.", "Калиберда Е.Л.",
                "Колотвина М.Г.", "Кондратьева М.А.", "Коршиков Д.С.", "Кравченко И.П.", "Кравченко С.О.", "Крамаренко Т.С.", "Криворучко А.В.", "Литвиненко О.Д.",
                "Лопатина М.М.", "Лукьяненко О.В.", "Лыжина Л.А.", "Лягута В.В.", "Малышев А.О.", "Мунасыпова К.Р.", "Муфазалова Ж.Б.", "Мякушина И.В.",
                "Нащекина Ю.С.", "Никитина В.Л.", "Новиков Р.Е.", "Носова Ю.В.", "Нургалеева И.Ю.", "Павельев А.А.", "Палагина Л.В.", "Паламарчук И.В.",
                "Парасовченко Г.И.", "Пахилько О.Н.", "Плотникова Ю.А.", "Раудина О.В.", "Сагдиев Т.Ф.", "Сазонов А.Н.", "Саликова М.А.", "Самсонова Д.А.",
                "Селищева О.И.", "Сергеева Н.В.", "Силкина М.Г.", "Синельникова К.С.", "Синявская А.П.", "Спиридонов Н.А.", "Трушина И.Ю.", "Умирова А.Н.",
                "Чебрукова Т.А.", "Швиндина Н.А.", "Шестаков В.А.", "Шестакова А.М."};

        [Theory]
        [InlineData("История", "История\nШвиндина Н.А.\nОсновной корпус-Аудитория 201")]
        [InlineData("Обществознание", "Обществознание\nКриворучко А.В.\nПроизводственный корпус-Аудитория 37а")]
        [InlineData("Физическая культура", "Физическая культура\nНовиков Р.Е.\n-")]
        public void GetDeciplineVerify_CorrectData_ReturnTrue(string expected, string nonFormattedDay)
        {
            var testClass = new DayWeekClass { Day = nonFormattedDay };

            var actual = testClass.GetDeciplineWithVerify(teachersList.ToList());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1 час ", 1, 60)]
        [InlineData("1 мин.", 0, 1)]
        [InlineData("1 час 1 мин.", 1, 61)]
        public void TimeToHourAndMinute_AllCase_ReturnTrue(string expected, int hour, int minute)
        {

            var actual = new Para().TimeToHourAndMinute(minute, hour);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Швиндина Н.А.", "История\nШвиндина Н.А.\nОсновной корпус-Аудитория 201")]
        [InlineData("Криворучко А.В.", "Обществознание\nКриворучко А.В.\nПроизводственный корпус-Аудитория 37а")]
        [InlineData("Новиков Р.Е.", "Физическая культура\nНовиков Р.Е.\n-")]
        public void Teacher_TrimTeacherInDay_ReturnTrue(string expected, string nonFormattedDay)
        {

            var testClass = new DayWeekClass { Day = nonFormattedDay };

            var actual = testClass.teacher(teachersList.ToList());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CabienetSchedule_GetCabinetsCountWithCorrectWorksheet_ReturnTrue()
        {
            const string path = @"C:\Users\Su\Downloads\17_22_04.xlsx";
            CabinetShedule testClass = new(new XLWorkbook(path).Worksheets.First());

            var actual = testClass.ListCabinets();

            Assert.InRange(actual.Count(), 15, 100);
        }

        [Fact]
        public void CabienetSchedule_GetCabinetCountWithEmptyWorksheet_ReturnTrue()
        {
            const string path = @"C:\Users\Su\Downloads\Empty.xlsx";
            CabinetShedule testClass = new(new XLWorkbook(path).Worksheets.First());

            var actual = testClass.ListCabinets();

            Assert.Equal(0, actual.Count);
        }

        [Fact]
        public void SpecialShedule_GetSpecialSheduleWithInCorrectDate_ReturnTrue()
        {
            Shedule notExistentSchedule = new(new DateTime(2024, 1, 1));

            var actual = notExistentSchedule.SpecialSheduleReturn();

            Assert.False(actual is ListPack);
        }

        [Fact]
        public void SpecialShedule_GetSpecialSheduleWithCorrectDate_ReturnTrue()
        {
            Shedule notExistentSchedule = new(new DateTime(2023, 3, 29));

            var actual = notExistentSchedule.SpecialSheduleReturn();

            Assert.True(actual is ListPack);
        }

        [Theory]
        [InlineData("28.04.2023", 24, 29)]
        [InlineData("13.09.2022", 12, 17)]
        public void DateOut_GetCorrectBeginEndBorders_ReturnTrue(string stringDate, int down, int up)
        {
            DateTime currentDate = DateTime.Parse(stringDate);
            Shedule dateCheckSheduleClass = new(currentDate);

            //Метод DateOut срабатывает сразу в конструкторе класса Shedule, поэтому вызов метода еще раз не требуется.

            Assert.Equal(down, dateCheckSheduleClass.downDay);
            Assert.Equal(up, dateCheckSheduleClass.upDay);
        }

        [Theory]
        [InlineData("28.04.2023", "april")]
        [InlineData("13.09.2022", "september")]
        public void Month_GetMonthCustomFormat_ReturnTrue(string stringDate, string correct)
        {
            DateTime currentDate = DateTime.Parse(stringDate);
            Shedule dateCheckSheduleClass = new(currentDate);

            var actual = dateCheckSheduleClass.Mouth(currentDate);

            Assert.Equal(correct, actual);
        }
    }
}
