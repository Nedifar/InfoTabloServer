using InfoTabloServer.Models;
using InfoTabloServer.SiganlR;
using InfoTabloServer.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InfoTabloServer.BackgroundServices
{
    public class FullUpdateHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private Context.context context;
        private IMemoryCache cache;

        public FullUpdateHostedService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory _serviceScopeFactory)
        {
            serviceScopeFactory = _serviceScopeFactory;
            _hubContext = hubContext;
            cache = memoryCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var scope = serviceScopeFactory.CreateScope();
                    var _terminalService = scope.ServiceProvider.GetRequiredService<Context.context>();
                    context = _terminalService;
                    var result = await GetUpdate(context);
                    await _hubContext.Clients.All.SendAsync("SendRequest", result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\tFullUpdateHostedService");
                }
                await Task.Delay(5000);
            }
        }

        public static async Task<RequestForDynamicUpdate> GetUpdate(Context.context context)
        {
            RequestForDynamicUpdate request = new RequestForDynamicUpdate();
            request.timeNow = DateTime.Now.ToString("HH:mm");
            request.labelPara = "Сейчас идет";
            try
            {
                var list = await context.TimeShedules.ToListAsync();
                TimeShedule relevantTimeShedule;
                if (list.Where(p => p.Name == DateTime.Now.ToShortDateString()).Count() != 0)
                {
                    return FormingMainUpdate(list.FirstOrDefault(p => p.Name == DateTime.Now.ToShortDateString()), request);
                }
                else if ((int)DateTime.Now.DayOfWeek == context.SpecialDayWeekNames.FirstOrDefault().dayWeek)
                {
                    return FormingMainUpdate(relevantTimeShedule = list.FirstOrDefault(p => p.Name == "ЧКР"), request);
                }
                else if ((int)DateTime.Now.DayOfWeek == 0)
                {
                    request.labelPara = "";
                    request.tbNumberPara = "Сегодня выходной";
                    return request;
                }
                else
                {
                    return FormingMainUpdate(list.FirstOrDefault(p => p.Name == "Основной"), request);
                }
            }
            catch { return request; }
        }

        private static RequestForDynamicUpdate FormingMainUpdate(TimeShedule relevantTimeShedule, RequestForDynamicUpdate request)
        {
            try
            {
                request.paraNow = null;
                request.paraNow = relevantTimeShedule.onlyParaNow()?.outGraphicNewTablo;
            }
            catch { request.paraNow = null; }
            var rrList = relevantTimeShedule.Paras.OrderBy(p => p.numberInList).ToList();
            request.lv = rrList;
            request.grLineHeight = relevantTimeShedule.TotalTime();
            StripeHeight.height = request.grLineHeight;
            double height = (DateTime.Now.TimeOfDay.TotalMinutes - rrList[0].begin.TimeOfDay.TotalMinutes);
            if (height < 0)
            {
                request.lineMargin = 0;
                request.colorLineHeight = 0;
                if ((rrList[0].begin.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes <= 20)
                {
                    request.tbNumberPara = $"До начала пар {Math.Ceiling((rrList[0].begin.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes)} мин.";
                }
                else
                    request.tbNumberPara = "На сегодня пары еще не начались.";
                request.labelPara = "";
                return request;
            }
            request.lineMarginTop = height;
            request.colorLineHeight = height;
            if (rrList.LastOrDefault().end.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                request.lineMarginTop = request.grLineHeight - 1;
                request.tbNumberPara = "На сегодня занятия закончились.";
                request.labelPara = "";
                return request;
            }
            request.progressBarPara = relevantTimeShedule.paraNow().toEndTimeInProcent;
            request.toEndPara = relevantTimeShedule.paraNow().toEndTime;
            request.tbNumberPara = relevantTimeShedule.paraNow().Name;
            StripeHeight.fulltime = relevantTimeShedule.times();
            return request;
        }
    }
}
