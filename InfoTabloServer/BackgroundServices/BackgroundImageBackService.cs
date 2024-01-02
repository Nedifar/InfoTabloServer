using InfoTabloServer.SiganlR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace InfoTabloServer.BackgroundServices
{
    public class BackgroundImageBackService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        private Context.context context;
        public BackgroundImageBackService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
            cache = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Выполняем задачу пока не будет запрошена остановка приложения
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var scope = _serviceScopeFactory.CreateScope();
                    var _terminalService = scope.ServiceProvider.GetRequiredService<Context.context>();
                    context = _terminalService;
                    var currentPhoto = await context.SpecialBackgroundPhotos.FirstOrDefaultAsync(p => p.targetDate.HasValue && p.targetDate.Value.Date == DateTime.Now.Date);
                    var path = currentPhoto == null
                        ? context.SpecialBackgroundPhotos.FirstOrDefault(p => !p.targetDate.HasValue).fileName
                        : currentPhoto.fileName;
                    //cache.Set("backImage", File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "background\\" + path), new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) });
                    cache.Set("backImageStable", path, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) });
                    await _hubContext.Clients.All.SendAsync("SendBackImageStable", 1);
                    await _hubContext.Clients.All.SendAsync("SendBackImage", 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\tBackImageService");

                }
                await Task.Delay(1000 * 60 * 60);
            }
        }
    }
}
