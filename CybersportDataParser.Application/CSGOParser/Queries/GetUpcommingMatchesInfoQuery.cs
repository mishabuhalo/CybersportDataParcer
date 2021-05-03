using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.CSGOParser.Queries
{
    public class GetUpcommingMatchesInfoQuery: IRequest<List<CSGOUpcomingMatchesInfo>>
    {
    }

    public class GetUpcommingMatchesInfoQueryHandler : IRequestHandler<GetUpcommingMatchesInfoQuery, List<CSGOUpcomingMatchesInfo>>
    {
        private ICSGOMatchesParser _cSGOMatchesParser;

        public GetUpcommingMatchesInfoQueryHandler(ICSGOMatchesParser cSGOMatchesParser)
        {
            _cSGOMatchesParser = cSGOMatchesParser;
        }

        public async Task<List<CSGOUpcomingMatchesInfo>> Handle(GetUpcommingMatchesInfoQuery request, CancellationToken cancellationToken)
        {
            return await _cSGOMatchesParser.GetAllUpcomingMatchesAsync();
        }
    }
}
