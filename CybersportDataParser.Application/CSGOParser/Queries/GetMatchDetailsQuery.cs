using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.CSGOParser.Queries
{
    public class GetMatchDetailsQuery: IRequest<CSGOMatchDetails>
    {
        public string MatchUrl {get; set;}
    }

    public class GetMatchDetailsQueryHandler : IRequestHandler<GetMatchDetailsQuery, CSGOMatchDetails>
    {
        private readonly ICSGOMatchesParser _cSGOMatchesParser;

        public GetMatchDetailsQueryHandler(ICSGOMatchesParser cSGOMatchesParser)
        {
            _cSGOMatchesParser = cSGOMatchesParser;
        }

        public async Task<CSGOMatchDetails> Handle(GetMatchDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _cSGOMatchesParser.GetMatchDetailsByUrlAsync(request.MatchUrl);
        }
    }
}
