using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace InfoTabloServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetVideo(string video)
        {
            //var filename = "";
            ////Build the File Path.  
            //string path = Path.Combine(_hostenvironment.WebRootPath, "files/") + filename;  // the video file is in the wwwroot/files folder  

            //var filestream = System.IO.File.OpenRead(path);
            //return Results.File(filestream, contentType: "video/mp4", fileDownloadName: filename, enableRangeProcessing: true);
            return Ok();
        }
    }
}
