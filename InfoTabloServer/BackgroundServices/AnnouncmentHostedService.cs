using InfoTabloServer.Models;
using InfoTabloServer.SiganlR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InfoTabloServer.BackgroundServices
{
    public class AnnouncmentHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<GoodHubGbl> _hubContext;
        private IMemoryCache cache;
        private Context.context context;

        public AnnouncmentHostedService(IHubContext<GoodHubGbl> hubContext, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
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
                    var list = context.Announcements.ToList();
                    var linka = new List<Announcement>();
                    int i = 1;
                    foreach (var lili in list.Where(p => p.dateAdded < DateTime.Now && (p.dateClosed >= DateTime.Now || p.dateClosed == null) && p.isActive).OrderByDescending(p => p.Priority).ThenByDescending(p => p.idAnnouncement).Take(5))
                    {
                        linka.Add(lili);
                        linka.LastOrDefault().idAnnouncement = i;
                        i++;
                    }
                    cache.Set("ann", linka, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
                    await _hubContext.Clients.All.SendAsync("SendAnn", cache.Get("ann"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\tAnnouncmentHostedService");

                }
                await Task.Delay(1000 * 10 * 60);
            }
        }
    }
}
