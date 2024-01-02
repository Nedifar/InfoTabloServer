using InfoTabloServer.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoTabloServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {
        private readonly context db;

        public AdminPanelController(context db) =>
            this.db = db;

        [HttpPost]
        [Route("Auth")]
        public async Task<ActionResult> Auth([FromBody]string hashPas)
        {
            bool result = await db.Users.AnyAsync(p => p.PasHash == hashPas);
            return Ok(result ? "Данные корректны" : "Данные некорректны");
        }
    }
}
