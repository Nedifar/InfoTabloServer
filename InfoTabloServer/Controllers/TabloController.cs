using InfoTabloServer.BackgroundServices;
using InfoTabloServer.Context;
using InfoTabloServer.Models;
using InfoTabloServer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TabloBlazorMain.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TabloApiController : ControllerBase
    {
        IMemoryCache cache;
        context context;
        public TabloApiController(context _context, IMemoryCache cache)
        {
            context = _context;
            this.cache = cache;
        }
        [HttpGet("weekName")]
        public string getWeekName() //возвращает нижнюю верхнюю неделю
        {
            return cache.Get("weekName").ToString();
        }
        [HttpGet("weather")]
        public ActionResult<string> getWeather() //вернуть погоду
        {
            while (cache.Get("weather") == null)
            { }
            return cache.Get("weather").ToString();
        }
        [HttpGet("admins")]
        public string getAdministrtorName() //вернуть админа этой недели
        {

            while (cache.Get("admin") == null)
            { }
            return cache.Get("admin").ToString();
        }

        [HttpGet("DayPartHeader")]
        public ActionResult getDayPartHeader() //вернуть заголовок для текущего времени дня
        {
            while (cache.Get("dayPart") == null)
            { }
            return Ok(cache.Get("dayPart").ToString());
        }

        [HttpGet("announc")]
        public List<Announcement> getAnnouncment() //вернуть все объявления
        {
            if (!cache.TryGetValue("ann", out List<Announcement> linka))
            {
                var list = context.Announcements.ToList();
                linka = new List<Announcement>();       
                int i = 1;
                foreach (var lili in list.Where(p => p.dateAdded < DateTime.Now && (p.dateClosed >= DateTime.Now || p.dateClosed == null) && p.isActive).OrderByDescending(p => p.Priority).ThenByDescending(p => p.idAnnouncement).Take(5))
                {
                    linka.Add(lili);
                    linka.LastOrDefault().idAnnouncement = i;
                    i++;
                }
                cache.Set("ann", linka, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });
            }
            return cache.Get("ann") as List<Announcement>;
        }

        [HttpGet("update")]
        public async Task<RequestForDynamicUpdate> DynamicUpdate() //обновить всю страницу
        {
            var update = await FullUpdateHostedService.GetUpdate(context);
            return update;
        }

        [HttpGet("getBackgroundMedia")]
        public FileResult GetBackgroundMedia()
        {
            while (cache.Get("backImage") == null)
            { }
            var cach = cache.Get("backImage") as byte[];
            return File(cach, "video/mp4");
        }

        [HttpGet("getBackgroundMediaStable")]
        public async Task<ActionResult> GetBackgroundMediaStable()
        {
            while (cache.Get("backImageStable") == null)
            { }
            return Ok(cache.Get("backImageStable").ToString());
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadBackgroundInfoTablo([FromForm] BackgroundUploadModelView model)
        {
            if (model.uploadFile == null)



            {
                return BadRequest("Выберите файл для загрузки.");
            }

            var extension = model.uploadFile.FileName.Split('.').LastOrDefault();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"background\";

            if (model.typeUpload == BackgroundUploadTypes.Main)
            {
                using (var fileStream = new FileStream(path + "main." + extension, FileMode.Create))
                {
                    await model.uploadFile.CopyToAsync(fileStream);
                    context.SpecialBackgroundPhotos.FirstOrDefault(p => p.targetDate == null).fileName = "main." + extension;
                }
            }

            else if (model.typeUpload == BackgroundUploadTypes.Other)
            {
                using (var fileStream = new FileStream(path + "special" + model.dateTarget.ToShortDateString() + "." + extension, FileMode.Create))
                {
                    await model.uploadFile.CopyToAsync(fileStream);
                    var currentModel = context.SpecialBackgroundPhotos
                        .ToList()
                        .FirstOrDefault(p => p.targetDate.HasValue && p.targetDate.Value.ToString("dd.MM.yyyy") == model.dateTarget.ToString("dd.MM.yyyy"));
                    if (currentModel == null)
                    {
                        currentModel = new SpecialBackgroundPhoto
                        {
                            fileName = "special" + model.dateTarget.ToShortDateString() + "." + extension,
                            targetDate = model.dateTarget.Date
                        };
                        context.SpecialBackgroundPhotos.Add(currentModel);
                    }
                    else
                    {
                        currentModel.fileName = "special" + model.dateTarget.ToShortDateString() + "." + extension;
                    }
                }
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch(Exception ex) 
            { 

            }
            return Ok();
        }
    }
}
