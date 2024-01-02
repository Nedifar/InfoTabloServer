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
            "������� �.�.","�������� �.�.", "���������� �.�.", "������� �.�.",
                "�������� �.�.", "����� �.�.", "������ �.�.", "������� �.�.", "������� �.�.", "��������� �.�.", "�������� �.�.", "��������� �.�.",
                "������������� �.�.", "����������� �.�.", "������� �.�.", "������� �.�.", "�������� �.�.","��������� �.�.", "������ �.�.","����� �.�.",
                "������� �.�.", "�������� �.�.", "���������� �.�.", "�������� �.�.", "��������� �.�.", "������� �.�.", "��������� �.�.", "��������� �.�.",
                "��������� �.�.", "����������� �.�.", "�������� �.�.", "��������� �.�.", "��������� �.�.", "���������� �.�.", "���������� �.�.", "���������� �.�.",
                "�������� �.�.", "���������� �.�.", "������ �.�.", "������ �.�.", "������� �.�.", "���������� �.�.", "���������� �.�.", "�������� �.�.",
                "�������� �.�.", "�������� �.�.", "������� �.�.", "������ �.�.", "���������� �.�.", "�������� �.�.", "�������� �.�.", "���������� �.�.",
                "������������ �.�.", "�������� �.�.", "���������� �.�.", "������� �.�.", "������� �.�.", "������� �.�.", "�������� �.�.", "��������� �.�.",
                "�������� �.�.", "�������� �.�.", "������� �.�.", "������������ �.�.", "��������� �.�.", "���������� �.�.", "������� �.�.", "������� �.�.",
                "��������� �.�.", "�������� �.�.", "�������� �.�.", "��������� �.�."};

        [Theory]
        [InlineData("�������", "�������\n�������� �.�.\n�������� ������-��������� 201")]
        [InlineData("��������������", "��������������\n���������� �.�.\n���������������� ������-��������� 37�")]
        [InlineData("���������� ��������", "���������� ��������\n������� �.�.\n-")]
        public void GetDeciplineVerify_CorrectData_ReturnTrue(string expected, string nonFormattedDay)
        {
            var testClass = new DayWeekClass { Day = nonFormattedDay };

            var actual = testClass.GetDeciplineWithVerify(teachersList.ToList());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1 ��� ", 1, 60)]
        [InlineData("1 ���.", 0, 1)]
        [InlineData("1 ��� 1 ���.", 1, 61)]
        public void TimeToHourAndMinute_AllCase_ReturnTrue(string expected, int hour, int minute)
        {

            var actual = new Para().TimeToHourAndMinute(minute, hour);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("�������� �.�.", "�������\n�������� �.�.\n�������� ������-��������� 201")]
        [InlineData("���������� �.�.", "��������������\n���������� �.�.\n���������������� ������-��������� 37�")]
        [InlineData("������� �.�.", "���������� ��������\n������� �.�.\n-")]
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

            //����� DateOut ����������� ����� � ������������ ������ Shedule, ������� ����� ������ ��� ��� �� ���������.

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
