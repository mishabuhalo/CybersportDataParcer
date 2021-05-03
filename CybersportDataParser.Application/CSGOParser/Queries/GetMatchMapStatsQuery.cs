using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.CSGOParser.Queries
{
    public class GetMatchMapStatsQuery: IRequest<List<CSGOMapStatsInfo>>
    {
        public string MatchUrl { get; set; }
    }
    public class GetMatchMapStatsQueryHandler : IRequestHandler<GetMatchMapStatsQuery, List<CSGOMapStatsInfo>>
    {
        private readonly ICSGOMatchesParser _cSGOMatchesParser;

        public GetMatchMapStatsQueryHandler(ICSGOMatchesParser cSGOMatchesParser)
        {
            _cSGOMatchesParser = cSGOMatchesParser;
        }

        public async Task<List<CSGOMapStatsInfo>> Handle(GetMatchMapStatsQuery request, CancellationToken cancellationToken)
        {
            return await _cSGOMatchesParser.GetMatchMapStatsInfoByUrlAsync(request.MatchUrl);
        }
    }
}
