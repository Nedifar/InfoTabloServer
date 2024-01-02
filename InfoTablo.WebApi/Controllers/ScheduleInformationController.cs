using InfoTablo.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace InfoTablo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleInformationController : ControllerBase
    {
        //        IMemoryCache _cache;
        //        private readonly IHubContext<ScheduleHub> _hubContext;
        //        public ScheduleInformationController(IHubContext<ScheduleHub> hub, IMemoryCache cache) 
        //        {
        //            _hubContext = hub;
        //            _cache = cache;
        //            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
        //        }

        //        [HttpGet("getnes")]
        //        public ActionResult<string> GetInformationAboutNewSchedule() //Метод для возвращения информации о наличии нового расписания
        //        {
        //            IXLWorksheet result = null;
        //            if (cache.TryGetValue("xLNew", out result))
        //            {
        //                if (result == null)
        //                {
        //                    return Ok("нет нового расписания");
        //                }
        //                else
        //                {
        //                    return Ok("есть новое расписание");
        //                }
        //            }
        //            else
        //            {
        //                return Ok("нет нового расписания");
        //            }
        //        }

        //        [HttpGet("getgroupMobile")]
        //        public ActionResult GetGroupMobile(string group, DateTime date)
        //        {
        //            var fullDayWeekClasses = new List<FullDayWeekClass>();
        //            var currentShedule = new Shedule(date);
        //            if (group == null)
        //            { return Ok("Введите название группы."); }
        //            IXLWorksheet result = null;

        //            while (result is null)
        //            {
        //                if (DateTime.UtcNow.AddHours(5).Date == date.Date)
        //                {
        //                    result = (IXLWorksheet)cache.Get("xLMain");
        //                }
        //                else if (DateTime.UtcNow.AddMinutes(5).Date.AddDays(7) == date.Date)
        //                {
        //                    result = (IXLWorksheet)cache.Get("xLNew");
        //                    if (result == null)
        //                    {
        //                        return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                    }
        //                    int column = new GroupShedule(result).IndexGroup(group);
        //                    if (column == 0)
        //                    {
        //                        return NotFound("Расписания для данной группы не найдено. Повторить поиск?");
        //                    }
        //                }
        //                else
        //                {

        //                    try
        //                    {
        //                        try
        //                        {
        //                            result = Shedule.dictSpecial[date].Item1;
        //                        }
        //                        catch
        //                        {
        //                            if (currentShedule.SpecialSheduleReturn == null)
        //                            {
        //                                return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                            }
        //                            result = Shedule.dictSpecial[date].Item1;
        //                        }
        //                        if (result == null)
        //                        {
        //                            return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                        }
        //                        int column = new GroupShedule(result).IndexGroup(group);
        //                        if (column == 0)
        //                        {
        //                            return NotFound("Расписания для данной группы не найдено. Повторить поиск?");
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //            }
        //            fullDayWeekClasses = GetGroupUniversal(group, result, currentShedule);
        //            return Ok(fullDayWeekClasses);
        //        }

        //        [HttpGet]
        //        [Route("getteacherMobile")]
        //        public ActionResult GetTeachersNobile(string teacher, DateTime date)
        //        {
        //            if (teacher == null)
        //            { return Ok("Введите имя преподавателя."); }
        //            var currentShedule = new Shedule(date);
        //            IXLWorksheet result = null;
        //            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();

        //            while (result is null)
        //            {
        //                if (DateTime.UtcNow.AddHours(5).Date == date.Date)
        //                {
        //                    result = (IXLWorksheet)cache.Get("xLMain");
        //                }
        //                else if (DateTime.UtcNow.AddMinutes(5).Date.AddDays(7) == date.Date)
        //                {
        //                    result = (IXLWorksheet)cache.Get("xLNew");
        //                    if (result == null)
        //                    {
        //                        return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                    }
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        try
        //                        {
        //                            result = Shedule.dictSpecial[date].Item1;
        //                        }
        //                        catch
        //                        {
        //                            if (currentShedule.SpecialSheduleReturn == null)
        //                            {
        //                                return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                            }
        //                            result = Shedule.dictSpecial[date].Item1;
        //                        }
        //                        if (result == null)
        //                        {
        //                            return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //            }
        //            fullDayWeekClasses = GetTeacherUniversal(teacher, result, currentShedule);
        //            return Ok(fullDayWeekClasses);
        //        }

        //        [HttpGet]
        //        [Route("getcabinetMobile")]
        //        public ActionResult GetCabinetMobile(string cabinet, DateTime date)
        //        {
        //            if (cabinet == null)
        //            { return Ok("Введите номер кабинета."); }
        //            var currentShedule = new Shedule(date);
        //            IXLWorksheet result = null;
        //            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();

        //            while (result is null)
        //            {
        //                if (DateTime.UtcNow.AddHours(5).Date == date.Date)
        //                {
        //                    result = (IXLWorksheet)cache.Get("xLMain");
        //                }
        //                else if (DateTime.UtcNow.AddMinutes(5).Date.AddDays(7) == date.Date)
        //                {
        //                    result = (IXLWorksheet)cache.Get("xLNew");
        //                    if (result == null)
        //                    {
        //                        return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                    }

        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        try
        //                        {
        //                            result = Shedule.dictSpecial[date].Item1;
        //                        }
        //                        catch
        //                        {
        //                            if (currentShedule.SpecialSheduleReturn == null)
        //                            {
        //                                return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                            }
        //                            result = Shedule.dictSpecial[date].Item1;
        //                        }
        //                        if (result == null)
        //                        {
        //                            return NotFound("Расписание для данной недели не найдено. Повторить поиск?");
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //            }
        //            fullDayWeekClasses = GetCabinetUniversal(cabinet, result, currentShedule);
        //            return Ok(fullDayWeekClasses);
        //        }

        //        [HttpGet]
        //        [Route("getcabinetsList")]
        //        public ActionResult<IEnumerable<List<string>>> Get(DateTime date) //вернуть список кабиентов
        //        {
        //            try
        //            {
        //                var listResult = new List<string>();
        //                if (DateTime.UtcNow.AddHours(5).Date == date)
        //                {
        //                    while (listResult.Count() == 0)
        //                    {
        //                        listResult = (List<string>)cache.Get("MainListCabinets");
        //                    }
        //                }
        //                else if (DateTime.UtcNow.AddHours(5).Date.AddDays(7) == date)
        //                {
        //                    while (listResult.Count() == 0)
        //                    {
        //                        listResult = (List<string>)cache.Get("NewListCabinets");
        //                        if (listResult == null)
        //                        {
        //                            return Ok(new List<string>());
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    var lp = new Shedule(date).SpecialSheduleReturn();
        //                    if (lp == null)
        //                        return Ok(null);
        //                    listResult = lp.Cabinets;
        //                    while (lp.Cabinets == null)
        //                    {
        //                        listResult = lp.Cabinets;
        //                    }
        //                }

        //                return Ok(listResult);
        //            }
        //            catch (Exception ex) { return BadRequest(ex.Message); }
        //        }

        //        [HttpGet]
        //        [Route("getteachersList")]
        //        public ActionResult<IEnumerable<List<string>>> GetTeachersList(DateTime date) //вернуть список кабиентов
        //        {
        //            try
        //            {
        //                var listResult = new List<string>();
        //                if (DateTime.UtcNow.AddHours(5).Date == date)
        //                {
        //                    while (listResult.Count() == 0)
        //                    {
        //                        listResult = (List<string>)cache.Get("MainListTeachers");
        //                    }
        //                }
        //                else if (DateTime.UtcNow.AddHours(5).Date.AddDays(7) == date)
        //                {
        //                    while (listResult.Count() == 0)
        //                    {
        //                        listResult = (List<string>)cache.Get("NewListTeachers");
        //                        if (listResult == null)
        //                        {
        //                            return Ok(new List<string>());
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    var lp = new Shedule(date).SpecialSheduleReturn();
        //                    if (lp == null)
        //                        return Ok(null);
        //                    listResult = lp.Teachers;
        //                    while (lp.Teachers == null)
        //                    {
        //                        listResult = lp.Teachers;
        //                    }
        //                }

        //                return Ok(listResult);
        //            }
        //            catch (Exception ex) { return BadRequest(ex.Message); }
        //        }

        //        [HttpGet]
        //        [Route("getgroupsList")]
        //        public ActionResult<IEnumerable<List<string>>> get(DateTime date) //вернуть список групп
        //        {
        //            try
        //            {
        //                var listResult = new List<string>();
        //                if (DateTime.UtcNow.AddHours(5).Date == date)
        //                {
        //                    while (listResult.Count() == 0)
        //                    {
        //                        listResult = (List<string>)cache.Get("MainListGroups");
        //                    }
        //                }
        //                else if (DateTime.UtcNow.AddHours(5).Date.AddDays(7) == date)
        //                {
        //                    while (listResult.Count() == 0)
        //                    {
        //                        listResult = (List<string>)cache.Get("NewListGroups");
        //                        if (listResult == null)
        //                        {
        //                            return Ok(new List<string>());
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    var lp = new Shedule(date).SpecialSheduleReturn();
        //                    if (lp == null)
        //                        return Ok(null);
        //                    listResult = lp.Groups;
        //                    while (lp.Groups == null)
        //                    {
        //                        listResult = lp.Groups;
        //                    }
        //                }

        //                return Ok(listResult);
        //            }
        //            catch (Exception ex) { return BadRequest(ex.Message); }
        //        }

        //        [HttpPost]
        //        [Route("getFloorShedule")]
        //        public ActionResult GetFlooreShedule(PostFloorModel models)
        //        {
        //            try
        //            {
        //                List<DayWeekClass> weekClasses = new List<DayWeekClass>();
        //                while ((List<string>)cache.Get("MainListCabinets") is null)
        //                {

        //                }
        //                while ((IXLWorksheet)cache.Get("xLMain") is null)
        //                {

        //                }
        //                while ((List<FloorCabinet>)cache.Get("FullFloorShedule") is null)
        //                {

        //                }
        //                var iX = (IXLWorksheet)cache.Get("xLMain");
        //                List<string> cabinets = (List<string>)cache.Get("MainListCabinets");
        //                List<FloorCabinet> floorsCabinets = (List<FloorCabinet>)cache.Get("FullFloorShedule");
        //                List<string> filtredCabinets = new List<string>();
        //                Regex regex;
        //                switch (models.floor)
        //                {
        //                    case "11":
        //                        regex = new Regex("^1[0-9]{2}");
        //                        break;
        //                    case "12":
        //                        regex = new Regex("^1[0-9]{1}[^0-9]{0,1}$");
        //                        break;
        //                    case "21":
        //                        regex = new Regex("^2[0-9]{2}");
        //                        break;
        //                    case "22":
        //                        regex = new Regex("^2[0-9]{1}[^0-9]{0,1}$");
        //                        break;
        //                    case "31":
        //                        regex = new Regex("^3[0-9]{2}");
        //                        break;
        //                    case "32":
        //                        regex = new Regex("^3[0-9]{1}[^0-9]{0,1}$");
        //                        break;
        //                    case "41":
        //                        regex = new Regex("^4[0-9]{2}");
        //                        break;
        //                    case "42":
        //                        regex = new Regex("^4[0-9]{1}[^0-9]{0,1}$");
        //                        break;
        //                    default:
        //                        return Ok(weekClasses);
        //                }
        //                if (int.TryParse(models.paraNow, out int numberPara) || models.paraNow == "ЧКР")
        //                {
        //                    try
        //                    {
        //                        foreach (string cabinet in cabinets)
        //                        {
        //                            if (regex.IsMatch(cabinet))
        //                            {
        //                                var psevdores = floorsCabinets.Where(p => p.Name == cabinet).FirstOrDefault();
        //                                if (models.CHKR.Where(p => p.TypeInterval.name == "ЧКР").FirstOrDefault() != null)
        //                                {
        //                                    var item = new DayWeekClass { Day = "ЧКР" };
        //                                    var usl = models.CHKR.Where(p => p.TypeInterval.name == "ЧКР" || p.TypeInterval.name == "Пара").ToList();
        //                                    for (int i = 0; i < usl.Count(); i++)
        //                                    {
        //                                        if (usl[i].TypeInterval.name == "ЧКР")
        //                                        {
        //                                            if (i == 0)
        //                                            {
        //                                                item.cabinet = cabinet;
        //                                                item.pp = "Сейчас идёт ЧКР";
        //                                            }
        //                                            else
        //                                            {
        //                                                item.pp = weekClasses[i - 1].pp;
        //                                                psevdores.DayWeeks[i - 1].pp = "Сейчас будет ЧКР\n" + psevdores.DayWeeks[i - 1].pp;
        //                                            }
        //                                            psevdores.DayWeeks.Insert(i, item);
        //                                            break;
        //                                        }
        //                                    }
        //                                }
        //                                if (models.paraNow == "ЧКР") { weekClasses.Add(psevdores.DayWeeks.Where(p => p.Number == null).FirstOrDefault()); }
        //                                else { weekClasses.Add(psevdores.DayWeeks.Where(p => p.Number == numberPara).FirstOrDefault()); }
        //                            }
        //                        }
        //                    }
        //                    catch { }
        //                }


        //                else if (models.paraNow is null)
        //                {
        //                    foreach (string cabinet in cabinets)
        //                    {
        //                        if (regex.IsMatch(cabinet))
        //                        {
        //                            weekClasses.Add(new DayWeekClass { cabinet = cabinet, Day = "", teacherMobile = "-" });
        //                        }
        //                    }
        //                }

        //                return Ok(weekClasses);
        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest(ex.Message);
        //            }
        //        }

        //        [HttpGet]
        //        [Route("GetCabinentsWithDetail")]
        //        public ActionResult GetCabinentsWithDetail(string cabinet)
        //        {
        //            List<DayWeekClass> weekClasses = new List<DayWeekClass>();
        //            while ((List<string>)cache.Get("MainListCabinets") is null)
        //            {

        //            }
        //            while ((IXLWorksheet)cache.Get("xLMain") is null)
        //            {

        //            }
        //            while ((List<FloorCabinet>)cache.Get("FullFloorShedule") is null)
        //            {

        //            }
        //            var iX = (IXLWorksheet)cache.Get("xLMain");
        //            List<FloorCabinet> floorsCabinets = (List<FloorCabinet>)cache.Get("FullFloorShedule");
        //            var resultWeekClass = new List<DayWeekClass>();
        //            resultWeekClass = floorsCabinets.Where(p => p.Name == cabinet).FirstOrDefault()
        //                ?.DayWeeks.ToList();
        //            return Ok(resultWeekClass);
        //        }

        //        [HttpGet("update")] //запрос для обновления данных между сервером и объявлениями
        //        public async Task<ActionResult> PostUpdateChangesAnnouncment()
        //        {
        //            var list = context.Announcements.ToList();
        //            var linka = new List<Announcement>();
        //            int i = 1;
        //            foreach (var lili in list.Where(p => p.dateAdded < DateTime.Now && (p.dateClosed >= DateTime.Now || p.dateClosed == null) && p.isActive).OrderByDescending(p => p.Priority).ThenByDescending(p => p.idAnnouncement).Take(5))
        //            {
        //                linka.Add(lili);
        //                linka.LastOrDefault().idAnnouncement = i;
        //                i++;
        //            }
        //            cache.Set("ann", linka, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) });
        //            await _hubContext.Clients.All.SendAsync("SendAnn", linka);
        //            return Ok();
        //        }

        //        [HttpGet]
        //        [Route("searchEmptycabinet/{numberPara}")]
        //        public async Task<ActionResult> SearchEmptyCabinet(int numberPara)
        //        {
        //            if (DateTime.Now.DayOfWeek == 0)
        //                return Ok(new string[] { "Имей уважение, сегодня воскресенье." });
        //            while ((List<FloorCabinet>)cache.Get("FullFloorShedule") is null)
        //            {

        //            }
        //            List<FloorCabinet> floorsCabinets = (List<FloorCabinet>)cache.Get("FullFloorShedule");
        //            int actualRow = (int)DateTime.UtcNow.AddHours(5).DayOfWeek * 6 + numberPara - 1;
        //            var cabinets = (List<string>)cache.Get("MainListCabinets");
        //            var emptyCabinetsNow = new List<string>();
        //            foreach (var cabinet in cabinets)
        //            {
        //                bool cont = false;
        //                var cab = floorsCabinets.FirstOrDefault(p => p.Name == cabinet);
        //                if (cab != null)
        //                {
        //                    if (cab.DayWeeks.FirstOrDefault(p => p.Number == numberPara && p.Day != "-") != null)
        //                    {
        //                        cont = true;
        //                    }
        //                }
        //                if (cont)
        //                {
        //                    cont = false;
        //                    continue;
        //                }
        //                emptyCabinetsNow.Add(cabinet);
        //            }
        //            return Ok(emptyCabinetsNow);
        //        }

        //        private List<FullDayWeekClass> GetGroupUniversal(string group, IXLWorksheet result, Shedule currentShedule)
        //        {
        //            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();
        //            var groupShedule = new GroupShedule(result);
        //            int column = groupShedule.IndexGroup(group);
        //            for (int j = 1; j <= 6; j++)
        //            {
        //                List<DayWeekClass> metrics = groupShedule.GetSheduleGroup(j * 6, column);
        //                string day = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime(2022, 12, 11).AddDays(j).DayOfWeek);
        //                fullDayWeekClasses.Add(new FullDayWeekClass
        //                {
        //                    dayWeekName = day.ToUpper()[0] + day.Substring(1),
        //                    dayWeekClasses = metrics
        //                });

        //                List<Para> paras = new List<Para>();
        //                var list = context.TimeShedules.ToList();
        //                if (list.Where(p => p.Name == currentShedule.DdownDay.AddDays(j - 1).ToShortDateString()).Count() != 0)
        //                {
        //                    paras = list.Where(p => p.Name == currentShedule.DdownDay.AddDays(j - 1).ToShortDateString()).FirstOrDefault().OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                }
        //                else if ((int)currentShedule.DdownDay.AddDays(j - 1).DayOfWeek == context.SpecialDayWeekNames.FirstOrDefault().dayWeek)
        //                {
        //                    paras = list.Where(p => p.Name == "ЧКР").FirstOrDefault().OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                    for (int i = 0; i < paras.Count; i++)
        //                    {
        //                        if (paras[i].outGraphicNewTablo == "ЧКР")
        //                            fullDayWeekClasses.FirstOrDefault()?.dayWeekClasses.Insert(i, new DayWeekClass { Day = "ЧКР" });
        //                    }
        //                }
        //                else
        //                {
        //                    paras = list.Where(p => p.Name == "Основной").FirstOrDefault().OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                }
        //                var cab = fullDayWeekClasses[j - 1].dayWeekClasses;
        //                var teachers = (List<string>)cache.Get("MainListTeachers");
        //                for (int i = 0; i < cab.Count(); i++)
        //                {
        //                    cab[i].beginMobile = paras[i].begin.ToShortTimeString();
        //                    cab[i].endMobile = paras[i].end.ToShortTimeString();
        //                    cab[i].teacherMobile = cab[i].teacher(teachers);
        //                    cab[i].Date = currentShedule.DdownDay.AddDays(j - 1);
        //                }
        //                foreach (var item in context.Lessons.Where(p => p.groupName == group && (int)(new DateTime(2022, 12, 11).AddDays(j).DayOfWeek) == p.idDayWeek).ToList())
        //                {

        //                    for (int i = 0; i < paras.Count(); i++)
        //                    {
        //                        if (paras[i].end.TimeOfDay >= item.Time.beginTime.TimeOfDay)
        //                        {
        //                            if (paras[i].outGraphicNewTablo == "ЧКР")
        //                                continue;
        //                            cab.Where(p => p.Number.ToString() == paras[i].outGraphicNewTablo).FirstOrDefault().Day = "Начало: " + item.Time.beginTime.ToString("HH:mm") + "\n" + item.Time.SheduleAdditionalLesson.name + "\n" + item.teacherName + "\n" + item.groupName;
        //                            for (int l = i; l < paras.Count(); l++)
        //                            {
        //                                if (paras[l].begin.TimeOfDay < item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
        //                                {
        //                                    cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day = item.Time.SheduleAdditionalLesson.name + "\n" + item.teacherName + "\n" + item.groupName;
        //                                    if (i == l)
        //                                        cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day = "Начало: " + item.Time.beginTime.ToString("HH:mm") + "\n" + cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day;
        //                                    if (paras[l].end.TimeOfDay >= item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
        //                                    {
        //                                        cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day += "\n" + "Конец: " + item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).ToString("HH:mm");
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            return fullDayWeekClasses;
        //        }

        //        private List<FullDayWeekClass> GetTeacherUniversal(string teacher, IXLWorksheet result, Shedule currentShedule)
        //        {
        //            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();
        //            var teacherShedule = new TeacherShedule(result);

        //            for (int j = 1; j <= 6; j++)
        //            {
        //                List<DayWeekClass> metrics = teacherShedule.GetSheduleTeacher(j * 6, teacher);
        //                string day = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime(2022, 12, 11).AddDays(j).DayOfWeek);
        //                fullDayWeekClasses.Add(new FullDayWeekClass
        //                {
        //                    dayWeekName = day.ToUpper()[0] + day.Substring(1),
        //                    dayWeekClasses = metrics
        //                });

        //                List<Para> paras = new List<Para>();
        //                var list = context.TimeShedules.ToList();
        //                if (list.Where(p => p.Name == currentShedule.DdownDay.AddDays(j - 1).ToShortDateString()).Count() != 0)
        //                {
        //                    paras = list.Where(p => p.Name == currentShedule.DdownDay.AddDays(j - 1).ToShortDateString()).FirstOrDefault().OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                }
        //                else if ((int)currentShedule.DdownDay.AddDays(j - 1).DayOfWeek == context.SpecialDayWeekNames.FirstOrDefault().dayWeek)
        //                {
        //                    paras = list.FirstOrDefault(p => p.Name == "ЧКР").OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                    for (int i = 0; i < paras.Count; i++)
        //                    {
        //                        if (paras[i].outGraphicNewTablo == "ЧКР")
        //                            fullDayWeekClasses.FirstOrDefault()?.dayWeekClasses.Insert(i, new DayWeekClass { Day = "ЧКР" });
        //                    }
        //                }
        //                else
        //                {
        //                    paras = list.FirstOrDefault(p => p.Name == "Основной").OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                }
        //                var cab = fullDayWeekClasses[j - 1].dayWeekClasses;
        //                var teachers = (List<string>)cache.Get("MainListTeachers");
        //                for (int i = 0; i < cab.Count(); i++)
        //                {
        //                    cab[i].beginMobile = paras[i].begin.ToShortTimeString();
        //                    cab[i].endMobile = paras[i].end.ToShortTimeString();
        //                    cab[i].teacherMobile = cab[i].teacher(teachers);
        //                    cab[i].Date = currentShedule.DdownDay.AddDays(j - 1);
        //                }
        //                foreach (var item in context.Lessons.Where(p => p.teacherName == teacher && (int)(new DateTime(2022, 12, 11).AddDays(j).DayOfWeek) == p.idDayWeek).ToList())
        //                {
        //                    for (int i = 0; i < paras.Count(); i++)
        //                    {
        //                        if (paras[i].end.TimeOfDay >= item.Time.beginTime.TimeOfDay)
        //                        {
        //                            if (paras[i].outGraphicNewTablo == "ЧКР")
        //                                continue;

        //                            var currentCabinet = cab.FirstOrDefault(p => p.Number.ToString() == paras[i].outGraphicNewTablo);
        //                            currentCabinet.teacherMobile = item.teacherName;
        //                            currentCabinet.Day = "Начало: " + item.Time.beginTime.ToString("HH:mm") + "\n" + item.Time.SheduleAdditionalLesson.name + "\n" + item.teacherName + "\n" + item.groupName;
        //                            for (int l = i; l < paras.Count(); l++)
        //                            {
        //                                var currentCabinetNext = cab.FirstOrDefault(p => p.Number.ToString() == paras[l].outGraphicNewTablo);
        //                                if (paras[l].begin.TimeOfDay < item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
        //                                {
        //                                    currentCabinetNext.Day = item.Time.SheduleAdditionalLesson.name + "\n" + item.teacherName + "\n" + item.groupName;
        //                                    currentCabinetNext.teacherMobile = item.teacherName;
        //                                    if (i == l)
        //                                        currentCabinetNext.Day = "Начало: " + item.Time.beginTime.ToString("HH:mm") + "\n" + currentCabinetNext.Day;
        //                                    if (paras[l].end.TimeOfDay >= item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
        //                                    {
        //                                        currentCabinetNext.Day += "\n" + "Конец: " + item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).ToString("HH:mm");
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            return fullDayWeekClasses;
        //        }

        //        private List<FullDayWeekClass> GetCabinetUniversal(string cabinet, IXLWorksheet result, Shedule currentShedule)
        //        {
        //            List<FullDayWeekClass> fullDayWeekClasses = new List<FullDayWeekClass>();
        //            var cabinetShedule = new CabinetShedule(result);
        //            for (int j = 1; j <= 6; j++)
        //            {
        //                List<DayWeekClass> metrics = cabinetShedule.GetSheduleCabinet(j * 6, cabinet);
        //                string day = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime(2022, 12, 11).AddDays(j).DayOfWeek);
        //                fullDayWeekClasses.Add(new FullDayWeekClass
        //                {
        //                    dayWeekName = day.ToUpper()[0] + day.Substring(1),
        //                    dayWeekClasses = metrics
        //                });

        //                List<Para> paras = new List<Para>();
        //                var list = context.TimeShedules.ToList();
        //                if (list.Where(p => p.Name == currentShedule.DdownDay.AddDays(j - 1).ToShortDateString()).Count() != 0)
        //                {
        //                    paras = list.Where(p => p.Name == currentShedule.DdownDay.AddDays(j - 1).ToShortDateString()).FirstOrDefault().OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                }
        //                else if ((int)currentShedule.DdownDay.AddDays(j - 1).DayOfWeek == context.SpecialDayWeekNames.FirstOrDefault().dayWeek)
        //                {
        //                    paras = list.Where(p => p.Name == "ЧКР").FirstOrDefault().OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                    for (int i = 0; i < paras.Count; i++)
        //                    {
        //                        if (paras[i].outGraphicNewTablo == "ЧКР")
        //                            fullDayWeekClasses.FirstOrDefault()?.dayWeekClasses.Insert(i, new DayWeekClass { Day = "ЧКР" });
        //                    }
        //                }
        //                else
        //                {
        //                    paras = list.Where(p => p.Name == "Основной").FirstOrDefault().OnlyParas.OrderBy(p => p.numberInterval).ToList();
        //                }
        //                var cab = fullDayWeekClasses[j - 1].dayWeekClasses;
        //                var teachers = (List<string>)cache.Get("MainListTeachers");
        //                for (int i = 0; i < cab.Count(); i++)
        //                {
        //                    cab[i].beginMobile = paras[i].begin.ToShortTimeString();
        //                    cab[i].endMobile = paras[i].end.ToShortTimeString();
        //                    cab[i].teacherMobile = cab[i].teacher(teachers);
        //                    cab[i].Date = currentShedule.DdownDay.AddDays(j - 1);
        //                }
        //                foreach (var item in context.Lessons.Where(p => p.cabinet == cabinet && (int)(new DateTime(2022, 12, 11).AddDays(j).DayOfWeek) == p.idDayWeek).ToList())
        //                {
        //                    for (int i = 0; i < paras.Count(); i++)
        //                    {
        //                        if (paras[i].end.TimeOfDay >= item.Time.beginTime.TimeOfDay)
        //                        {
        //                            if (paras[i].outGraphicNewTablo == "ЧКР")
        //                                continue;
        //                            cab.Where(p => p.Number.ToString() == paras[i].outGraphicNewTablo).FirstOrDefault().Day = "Начало: " + item.Time.beginTime.ToString("HH:mm") + "\n" + item.Time.SheduleAdditionalLesson.name + "\n" + item.teacherName + "\n" + item.groupName;
        //                            for (int l = i; l < paras.Count(); l++)
        //                            {
        //                                if (paras[l].begin.TimeOfDay < item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
        //                                {
        //                                    cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day = item.Time.SheduleAdditionalLesson.name + "\n" + item.teacherName + "\n" + item.groupName;
        //                                    if (i == l)
        //                                        cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day = "Начало: " + item.Time.beginTime.ToString("HH:mm") + "\n" + cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day;
        //                                    if (paras[l].end.TimeOfDay >= item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).TimeOfDay)
        //                                    {
        //                                        cab.Where(p => p.Number.ToString() == paras[l].outGraphicNewTablo).FirstOrDefault().Day += "\n" + "Конец: " + item.Time.beginTime.AddMinutes(item.Time.SheduleAdditionalLesson.durationLesson).ToString("HH:mm");
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            return fullDayWeekClasses;
        //        }
    }
}
