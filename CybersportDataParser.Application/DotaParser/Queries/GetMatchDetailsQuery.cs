using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.Dota;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.DotaParser.Queries
{
    public class GetMatchDetailsQuery: IRequest<DotaMatchDetails>
    {
        public string MatchUrl { get; set; }
    }

    public class GetMatchDetailsQueryHandler : IRequestHandler<GetMatchDetailsQuery, DotaMatchDetails>
    {
        private readonly IDotaMatchesParser _dotaMatchesParser;

        public GetMatchDetailsQueryHandler(IDotaMatchesParser dotaMatchesParser)
        {
            _dotaMatchesParser = dotaMatchesParser;
        }

        public async Task<DotaMatchDetails> Handle(GetMatchDetailsQuery request, CancellationToken cancellationToken)
        {
            return _dotaMatchesParser.GetMatchDetailsByUrl(request.MatchUrl);
        }
    }
}
