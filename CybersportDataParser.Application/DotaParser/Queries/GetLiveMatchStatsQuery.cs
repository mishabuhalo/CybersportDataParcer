using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.Dota;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.DotaParser.Queries
{
    public class GetLiveMatchStatsQuery: IRequest<DotaMatchLiveStat>
    {
        public string MatchUrl { get; set; }
    }
    public class GetLiveMatchStatsQueryHandler : IRequestHandler<GetLiveMatchStatsQuery, DotaMatchLiveStat>
    {
        private readonly IDotaMatchesParser _dotaMatchesParser;

        public GetLiveMatchStatsQueryHandler(IDotaMatchesParser dotaMatchesParser)
        {
            _dotaMatchesParser = dotaMatchesParser;
        }

        public async Task<DotaMatchLiveStat> Handle(GetLiveMatchStatsQuery request, CancellationToken cancellationToken)
        {
            return _dotaMatchesParser.GetMatchLiveStatByUrl(request.MatchUrl);
        }
    }
}
