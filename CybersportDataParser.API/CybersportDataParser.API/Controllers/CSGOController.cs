using CybersportDataParser.Application.CSGOParser.Queries;
using CybersportDataParser.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CybersportDataParser.API.Controllers
{
    [Route("api/csgo")]
    [ApiController]
    public class CSGOController : BaseController
    {
        [HttpGet("matches/live")]
        public async Task<IActionResult> GetLiveMatchesInfo()
        {
            var result = await Mediator.Send(new GetLiveMatchesInfoQuery());
            return Ok(result);
        }

        [HttpGet("matches/upcoming")]
        public async Task<IActionResult> GetUpcommingMatchesInfo()
        {
            var result = await Mediator.Send(new GetUpcommingMatchesInfoQuery());
            return Ok(result);
        }
    }
}
