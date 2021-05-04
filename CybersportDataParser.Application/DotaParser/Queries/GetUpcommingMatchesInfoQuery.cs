using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.Dota;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.DotaParser.Queries
{
    public class GetUpcommingMatchesInfoQuery: IRequest<List<DotaMatchesInfo>>
    {
    }

    public class GetUpcomingMatchesInfoQueryHandler : IRequestHandler<GetUpcommingMatchesInfoQuery, List<DotaMatchesInfo>>
    {
        private readonly IDotaMatchesParser _dotaMatchesParser;

        public GetUpcomingMatchesInfoQueryHandler(IDotaMatchesParser dotaMatchesParser)
        {
            _dotaMatchesParser = dotaMatchesParser;
        }

        public async Task<List<DotaMatchesInfo>> Handle(GetUpcommingMatchesInfoQuery request, CancellationToken cancellationToken)
        {
            return _dotaMatchesParser.GetUpcomingMatches();
        }
    }
}
