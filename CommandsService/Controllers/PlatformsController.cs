using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandsServices.Controllers
{
    [Route("api/c/{controller}")]
    [ApiController]
    public class PlatformsController: ControllerBase{
        public PlatformsController()
        {
            
        }
        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("---> Inbound Post # Command Service");
            return Ok("Inbound test fo from platforms controller");
        }
    }

}