using CybersportDataParser.Application.LoLParser.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CybersportDataParser.API.Controllers
{
    [Route("api/lol")]
    [Authorize]
    [ApiController]
    public class LoLController : BaseController
    {
        [HttpGet("live")]
        public async Task<IActionResult> GetLiveMatchesInfo()
        {
            var result = await Mediator.Send(new GetLiveMatchesQuery());

            return Ok(result);
        }

        [HttpGet("upcomming")]
        public async Task<IActionResult> GetUpcommingMatchesInfo()
        {
            var result = await Mediator.Send(new GetUpcommingMatchesQuery());

            return Ok(result);
        }

        [HttpPost("match-details")]
        public async Task<IActionResult> GetMatchDetails(GetMatchDetailsQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
