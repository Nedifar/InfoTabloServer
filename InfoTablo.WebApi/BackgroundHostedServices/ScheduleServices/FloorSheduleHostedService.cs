using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using InfoTablo.Application.Lessons.Queries.GetLessonsToday;
using InfoTablo.Application.Paras.Queries.GetParasToday;
using InfoTablo.Application.TimeShedules.Queries;
using InfoTablo.WebApi.BackgroundHostedServices.HostedServiceDtos;
using InfoTabloServer.LastDanceResources;
using InfoTabloServer.ViewModels;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace InfoTabloServer.BackgroundServices.SheduleHostedServices
{
    public class FloorSheduleHostedService : BackgroundService
    {
        private readonly IMemoryCache _cache;
        private readonly IMediator _mediator;

        public FloorSheduleHostedService(IMemoryCache cache, IMediator mediator) =>
            (_cache, _mediator) = (cache, mediator);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Run(async () =>
                    {
                        List<FloorCabinetDto> floorCabinets = GetFloorCabinetUnFiltred();

                        var parasQuery = new GetParasTodayQuery();
                        var onlyParasCollection = await _mediator.Send(parasQuery);

                        var lessonsQuery = new GetLessonsTodayQuery();
                        var todayLessons = await _mediator.Send(lessonsQuery);

                        foreach (var item in todayLessons)
                        {
                            if (item.Cabinet == null)
                                continue;

                            var cab = floorCabinets.FirstOrDefault(p => p.Name == item.Cabinet)?.DayWeeksDto;
                            if (cab == null)
                                continue;
                            //for (int i = 0; i < onlyParasCollection.Count; i++)
                            //{
                            //    if (onlyParasCollection[i].End.TimeOfDay >= item.Time.BeginTime.TimeOfDay)
                            //    {
                            //        if (onlyParasCollection[i].outGraphicNewTablo == "ЧКР")
                            //            continue;

                            //        var currentCabinet = cab.FirstOrDefault(p => p.Number.ToString() == onlyParasCollection[i].outGraphicNewTablo);
                            //        currentCabinet.teacherMobile = item.TeacherName;

                            //        currentCabinet.Day = "Начало: "
                            //        + item.Time.beginTime.ToString("HH:mm")
                            //        + "\n"
                            //        + item.Time.SheduleAdditionalLesson.name
                            //        + "\n"
                            //        + item.teacherName
                            //        + "\n"
                            //        + item.cabinet
                            //        + "\n"
                            //        + item.groupName;

                            //        for (int j = i; j < paras.Count(); j++)
                            //        {
                            //            var currentCabinetNext = cab.FirstOrDefault(p => p.Number.ToString() == paras[j].outGraphicNewTablo);
                            //            if (paras[j].begin.TimeOfDay < item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
                            //            {
                            //                currentCabinetNext.Day = item.Time.SheduleAdditionalLesson.name
                            //                + "\n"
                            //                + item.teacherName
                            //                + "\n"
                            //                + item.cabinet
                            //                + "\n"
                            //                + item.groupName;
                            //                currentCabinetNext.teacherMobile = item.teacherName;
                            //                if (i == j)
                            //                    currentCabinetNext.Day = "Начало: "
                            //                    + item.Time.beginTime.ToString("HH:mm")
                            //                    + "\n"
                            //                    + currentCabinetNext.Day;
                            //                if (paras[j].end.TimeOfDay >= item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
                            //                {
                            //                    currentCabinetNext.Day += "\n" +
                            //                    "Конец: "
                            //                    + item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).ToString("HH:mm");
                            //                }
                            //            }
                            //        }
                            //        break;
                            //    }
                            //}

                        }

                        for (int i = 0; i < floorCabinets.Count; i++)
                        {
                            int l = 1;
                            if (i is 0)
                                l--;
                            for (int j = 0; j < floorCabinets[i].DayWeeksDto.Count; j++)
                            {
                                if (floorCabinets[i].DayWeeksDto[j].GetDeciplineFromDay().Trim().Length <= 2 || floorCabinets[i].DayWeeksDto[j].GetDeciplineFromDay() == "ЧКР")
                                {
                                    floorCabinets[i].DayWeeksDto[j].Pause = "Пусто";
                                    if (j + 1 == floorCabinets[i].DayWeeksDto.Count)
                                        break;
                                    if (floorCabinets[i].DayWeeksDto[j + 1].Number == l + 1 || floorCabinets[i].DayWeeksDto[j].Number == null)
                                    {
                                        if (floorCabinets[i].DayWeeksDto[j + 1].GetDeciplineFromDay().Trim().Length <= 2)
                                            floorCabinets[i].DayWeeksDto[j].Pause = "Пусто";
                                        else
                                        {
                                            floorCabinets[i].DayWeeksDto[j].Pause = "Есть следующее занятие";
                                            floorCabinets[i].DayWeeksDto[j].Gr1 = $"Группа: {floorCabinets[i].DayWeeksDto[j + 1].GetGroupFromDay()}";
                                            floorCabinets[i].DayWeeksDto[j].Dec1 = $"Дисциплина: {floorCabinets[i].DayWeeksDto[j + 1].GetDeciplineFromDay()}";
                                        }
                                    }
                                    if (floorCabinets[i].DayWeeksDto[j + 1].Number != l + 1 && floorCabinets[i].DayWeeksDto[j + 1].Number != null)
                                    {
                                        l = 1;
                                        continue;
                                    }
                                    if (floorCabinets[i].DayWeeksDto[j + 1].Number == null) l--;
                                    l++;
                                }
                                else
                                {
                                    floorCabinets[i].DayWeeksDto[j].Pause = "Есть";
                                    if (j + 1 == floorCabinets[i].DayWeeksDto.Count())
                                        break;
                                    if (floorCabinets[i].DayWeeksDto[j + 1].Number == l + 1 || floorCabinets[i].DayWeeksDto[j + 1].Number == null)
                                    {
                                        if (floorCabinets[i].DayWeeksDto[j + 1].GetDeciplineFromDay().Trim().Length <= 2 || floorCabinets[i].DayWeeksDto[j + 1].GetDeciplineFromDay() == "ЧКР")
                                            floorCabinets[i].DayWeeksDto[j].Pause = "Дальше пусто";
                                        else
                                        {
                                            floorCabinets[i].DayWeeksDto[j].Pause = $"Есть следующее занятие";
                                            floorCabinets[i].DayWeeksDto[j].Gr1 = $"Группа: {floorCabinets[i].DayWeeksDto[j + 1].GetGroupFromDay()}";
                                            floorCabinets[i].DayWeeksDto[j].Dec1 = $"Дисциплина: {floorCabinets[i].DayWeeksDto[j + 1].GetDeciplineFromDay()}";
                                        }
                                    }
                                    if (floorCabinets[i].DayWeeksDto[j + 1].Number != l + 1 && floorCabinets[i].DayWeeksDto[j + 1].Number != null)
                                    {
                                        l = 1;
                                        continue;
                                    }
                                    if (floorCabinets[i].DayWeeksDto[j + 1].Number == null) l--;
                                    l++;
                                }
                            }
                        }
                        for (int i = 0; i < floorCabinets.Count; i++)
                        {
                            for (int j = 0; j < floorCabinets[i].DayWeeksDto.Count; j++)
                            {
                                if (floorCabinets[i].DayWeeksDto[j].Pause.Contains("Сегодня кабинет свободен"))
                                { continue; }

                                if (!floorCabinets[i].DayWeeksDto[j].Pause.Contains("Есть"))
                                {
                                    string str = floorCabinets[i].DayWeeksDto[j].Number.ToString();
                                    bool b = true;
                                    int g = 1;
                                    while (str != "6")
                                    {
                                        if (floorCabinets[i].DayWeeksDto[j + g].Pause.Contains("Есть"))
                                        {
                                            floorCabinets[i].DayWeeksDto[j].Pause = "Нет следующего занятия, но кабинет будет захвачен позже";
                                            b = false;
                                            break;
                                        }
                                        str = floorCabinets[i].DayWeeksDto[j + g].Number.ToString();
                                        g++;
                                    }
                                    if (!b || floorCabinets[i].DayWeeksDto[j].Number != 1)
                                    { }
                                    else
                                    {
                                        str = floorCabinets[i].DayWeeksDto[j].Number.ToString();
                                        floorCabinets[i].DayWeeksDto[j].Pause = "Сегодня кабинет свободен";
                                        g = 1;
                                        while (str != "6")
                                        {
                                            floorCabinets[i].DayWeeksDto[j + g].Pause = "Сегодня кабинет свободен";
                                            str = floorCabinets[i].DayWeeksDto[j + g].Number.ToString();
                                            g++;
                                        }
                                    }
                                }
                                if (floorCabinets[i].DayWeeksDto[j].Pause.Contains("Дальше пусто"))
                                { floorCabinets[i].DayWeeksDto[j].Pause = "Последнее занятие, пожалуйста, поднимите за собой стулья."; }
                                if (floorCabinets[i].DayWeeksDto[j].Pause == "Пусто")
                                { floorCabinets[i].DayWeeksDto[j].Pause = "На сегодня занятия закончились."; }
                                if (floorCabinets[i].DayWeeksDto[j].Pause == "Есть")
                                { floorCabinets[i].DayWeeksDto[j].Pause = "Последнее занятие, пожалуйста, поднимите за собой стулья."; }
                            }
                        }
                        _cache.Set("FullFloorShedule", floorCabinets);

                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\nFloorShedule");
                }
                await Task.Delay(10000 * 10);
            }
        }

        private List<FloorCabinetDto> GetFloorCabinetUnFiltred()
        {
            var floorCabinets = new List<FloorCabinetDto>();

            while ((List<string>)_cache.Get("MainListCabinets") is null)
            {

            }
            while ((IXLWorksheet)_cache.Get("xLMain") is null)
            {

            }
            while ((List<string>)_cache.Get("MainListTeachers") is null)
            {

            }
            var iX = (IXLWorksheet)_cache.Get("xLMain");
            List<string> cabinets = (List<string>)_cache.Get("MainListCabinets");
            List<string> teachers = (List<string>)_cache.Get("MainListTeachers");

            foreach (string cabinet in cabinets)
            {
                List<DayWeekDto> days = new();
                List<DayWeekDto> cabinetsShedule = /*new CabinetShedule(iX).GetSheduleCabinet(6 * (int)DateTime.Now.DayOfWeek, cabinet)*/null;

                cabinetsShedule.ForEach(cabinetShedule =>
                {
                    cabinetShedule.TeacherMobile = cabinetShedule.Teacher(teachers);
                });

                days.AddRange(cabinetsShedule.ToArray());
                floorCabinets.Add(new FloorCabinetDto
                {
                    DayWeeksDto = days,
                    Name = cabinet
                });
            }

            return floorCabinets;
        }
    }
}
