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

        public FullUpdateHostedService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory _serviceScopeFactory)
        {
            serviceScopeFactory = _serviceScopeFactory;
            _hubContext = hubContext;
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
                    await _hubContext.Clients.All.SendAsync("SendRequest", result, cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\tFullUpdateHostedService");
                }
                await Task.Delay(5000, stoppingToken);
            }
        }

        public static async Task<RequestForDynamicUpdate> GetUpdate(Context.context context)
        {
            RequestForDynamicUpdate request = new()
            {
                timeNow = DateTime.Now.ToString("HH:mm"),
            };
            try
            {
                var list = await context.TimeShedules.ToListAsync();

                if (list.Where(p => p.Name == DateTime.Now.ToShortDateString()).Any())
                {
                    return FormingMainUpdate(list.FirstOrDefault(p => p.Name == DateTime.Now.ToShortDateString()), request);
                }
                else if ((int)DateTime.Now.DayOfWeek == context.SpecialDayWeekNames.FirstOrDefault().dayWeek)
                {
                    return FormingMainUpdate(list.FirstOrDefault(p => p.Name == "ЧКР"), request);
                }
                else if (DateTime.Now.DayOfWeek == 0)
                {
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
            request.paraNow = relevantTimeShedule?.onlyParaNow()?.outGraphicNewTablo;

            var rrList = relevantTimeShedule.Paras.OrderBy(p => p.numberInList).ToList();
            request.lv = rrList;
            request.grLineHeight = relevantTimeShedule.TotalTime();
            //StripeHeight.height = request.grLineHeight;
            double height = (DateTime.Now.TimeOfDay.TotalMinutes - rrList[0].begin.TimeOfDay.TotalMinutes);
            if (height < 0)
            {
                //request.colorLineHeight = 0;
                if ((rrList[0].begin.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes <= 20)
                {
                    request.tbNumberPara = $"До начала пар {Math.Ceiling((rrList[0].begin.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes)} мин.";
                }
                else
                    request.tbNumberPara = "Пары не начались";
                return request;
            }
            request.colorLineHeight = height;
            if (rrList.LastOrDefault().end.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                request.tbNumberPara = "Занятия закончились";
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
