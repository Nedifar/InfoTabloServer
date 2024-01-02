using InfoTabloServer.SiganlR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace InfoTabloServer.BackgroundServices
{
    public class AdminHostedSerivce : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        private Context.context context;

        public AdminHostedSerivce(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
            cache = memoryCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var scope = _serviceScopeFactory.CreateScope();
                    var _terminalService = scope.ServiceProvider.GetRequiredService<Context.context>();
                    context = _terminalService;
                    string result = context.MonthYear.FirstOrDefault(p => p.date.Year == DateTime.Now.Year && p.date.Month == DateTime.Now.Month)?.getSupervisorNow;
                    cache.Set("admin", result??"", new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) });
                    await _hubContext.Clients.All.SendAsync("SendAdmin", cache.Get("admin"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\tAdminHostedSerivce");
                }
                await Task.Delay(1000 * 15 * 60);
            }
        }
    }
}
