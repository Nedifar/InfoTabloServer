namespace InfoTablo.WebApi.BackgroundHostedServices.HostedServiceDtos
{
    public class DayWeekDto
    {
        public int? Number { get; set; }
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public string Cabinet { get; set; }
        public string StatusPara { get; set; }
        public string BeginMobile { get; set; }
        public string EndMobile { get; set; }
        public string TeacherMobile { get; set; }
        public string Pause { get; set; }
        public string GroupMobile { get; set; }
        public string Gr1 { get; set; }
        public string Dec1 { get; set; }

        public string GetGroupFromDay()
        {
            if (Day.Contains("Конец"))
            {
                int s = (Day?.Split('\n').Length - 2).Value;
                return Day?.Split('\n')[s];
            }
            return Day?.Split('\n').LastOrDefault();
        }

        public string GetDeciplineFromDay()
        {
            string result = Day?.Split('\n')[0];
            if (result != null)
            {
                if (result.Contains("ДОП"))
                    return "ДОП";
            }
            if (Day == null)
                return result;
            if (Day.Contains("Начало"))
            {
                result += "<br>" + Day?.Split('\n')[1];
            }
            if (Day.Contains("Конец"))
            {
                result += "<br>" + Day?.Split('\n').Last();
            }
            return result;
        }

        public string Teacher(List<string> teachers)
        {
            if (teachers == null)
                return "-";
            foreach (var teacher in teachers)
            {
                if (Day != null)
                {
                    if (Day.Contains(teacher))
                    {
                        return teacher;
                    }
                }
            }
            return "-";
        }
    }
}
