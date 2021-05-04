using CybersportDataParser.Application.DotaParser.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CybersportDataParser.API.Controllers
{
    [Route("api/dota")]
    [Authorize]
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

        [HttpPost("match-details")]
        public async Task<IActionResult> GetMatchDetails(GetMatchDetailsQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("live-stats")]
        public async Task<IActionResult> GetMatchLiveStats(GetLiveMatchStatsQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
