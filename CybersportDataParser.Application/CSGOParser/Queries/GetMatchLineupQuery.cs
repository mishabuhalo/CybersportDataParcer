using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.CSGOParser.Queries
{
    public class GetMatchLineupQuery: IRequest<List<CSGOMatchLineup>>
    {
        public string matchUrl { get; set; }
    }

    public class GetMatchLineupQueryHandler : IRequestHandler<GetMatchLineupQuery, List<CSGOMatchLineup>>
    {
        private readonly ICSGOMatchesParser _cSGOMatchesParser;

        public GetMatchLineupQueryHandler(ICSGOMatchesParser cSGOMatchesParser)
        {
            _cSGOMatchesParser = cSGOMatchesParser;
        }

        public async Task<List<CSGOMatchLineup>> Handle(GetMatchLineupQuery request, CancellationToken cancellationToken)
        {
            return await _cSGOMatchesParser.GetMatchLineupByUrlAsync(request.matchUrl);
        }
    }
}
