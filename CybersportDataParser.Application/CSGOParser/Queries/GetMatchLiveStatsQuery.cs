using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.CSGOParser.Queries
{
    public class GetMatchLiveStatsQuery: IRequest<CSGOMatchLiveStats>
    {
        public string MatchUrl { get; set; }
    }

    public class GetMatchLiveStatsQueryHandler : IRequestHandler<GetMatchLiveStatsQuery, CSGOMatchLiveStats>
    {
        private readonly ICSGOMatchesParser _cSGOMatchesParser;

        public GetMatchLiveStatsQueryHandler(ICSGOMatchesParser cSGOMatchesParser)
        {
            _cSGOMatchesParser = cSGOMatchesParser;
        }

        public async Task<CSGOMatchLiveStats> Handle(GetMatchLiveStatsQuery request, CancellationToken cancellationToken)
        {
            return await _cSGOMatchesParser.GetLiveMatchStats(request.MatchUrl);
        }
    }
}
