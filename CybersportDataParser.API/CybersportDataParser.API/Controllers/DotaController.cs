using CybersportDataParser.Application.DotaParser.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CybersportDataParser.API.Controllers
{
    [Route("api/dota")]
    [ApiController]
    public class DotaController : BaseController
    {
        [HttpGet("live")]
        public async Task<IActionResult> GetLiveMatchesInfo()
        {
            var result = await Mediator.Send(new GetLiveMatchesInfoQuery());

            return Ok(result);
        }

        [HttpGet("upcomming")]
        public async Task<IActionResult> GetUpcommingMatchesInfo()
        {
            var result = await Mediator.Send(new GetUpcommingMatchesInfoQuery());

            return Ok(result);
        }
    }
}
