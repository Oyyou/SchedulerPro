using Microsoft.AspNetCore.Mvc;

namespace SchedulerPro.API.Controllers
{
  [ApiController]
  [Route("api/healthcheck")]
  public class HealthCheckController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("Healthy");
    }
  }
}
