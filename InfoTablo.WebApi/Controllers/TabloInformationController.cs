using AutoMapper;
using InfoTablo.Application.Announcments.Queries.GetAnnouncments;
using InfoTablo.Application.BackgroundImage.Commands.UploadImage;
using InfoTablo.Application.Interfaces;
using InfoTablo.Application.TimeShedules.Queries.GetTimeScheduleForDynamicUpdate;
using InfoTablo.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InfoTablo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabloInformationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public TabloInformationController(IMediator mediator, IMemoryCache memoryCache, IMapper mapper)
        {
            (_mediator, _memoryCache, _mapper) = (mediator, memoryCache, mapper);
        }

        [HttpGet]
        [Route("weekName")]
        public ActionResult<string> GetWeekName() //возвращает нижнюю верхнюю неделю
        {
            return _memoryCache.Get("weekName").ToString();
        }

        [HttpGet]
        [Route("weather")]
        public ActionResult<string> GetWeather() //вернуть погоду
        {
            while (_memoryCache.Get("weather") == null)
            { }
            return _memoryCache.Get("weather").ToString();
        }

        [HttpGet]
        [Route("admins")]
        public string GetAdministrtorName() //вернуть админа этой недели
        {
            while (_memoryCache.Get("admin") == null)
            { }
            return _memoryCache.Get("admin").ToString();
        }

        [HttpGet]
        [Route("DayPartHeader")]
        public ActionResult GetDayPartHeader() //вернуть заголовок для текущего времени дня
        {
            while (_memoryCache.Get("dayPart") == null)
            { }
            return Ok(_memoryCache.Get("dayPart").ToString());
        }

        [HttpGet("announc")]
        public async Task<ActionResult<AnnouncmentsVm>> GetAnnouncments() //вернуть все объявления
        {
            if (!_memoryCache.TryGetValue("ann", out AnnouncmentsVm announcmentsVm))
            {
                var query = new GetAnnouncmentsQuery();
                announcmentsVm = await _mediator.Send(query);
                _memoryCache.Set("ann", announcmentsVm,
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                    });
            }
            return Ok(announcmentsVm);
        }

        [HttpGet("update")]
        public async Task<GetTimeScheduleForDynamicUpdateVm> DynamicUpdate() //обновить всю страницу
        {
            var query = new GetTimeScheduleForDynamicUpdateQuery();
            var update = await _mediator.Send(query);
            return update;
        }

        [HttpGet("getBackgroundMedia")]
        public ActionResult GetBackgroundMedia()
        {
            while (_memoryCache.Get("backImage") == null)
            { }
            return Ok(_memoryCache.Get("backImage").ToString());
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadBackgroundInfoTablo([FromForm] UploadBackgroundImageDto model)
        {
            if (model.UploadFile == null)
            {
                return BadRequest("Выберите файл для загрузки.");
            }
            var command = _mapper.Map<UploadBackgroundImageCommand>(model);

            await _mediator.Send(command);
            return Ok();
        }
    }
}
