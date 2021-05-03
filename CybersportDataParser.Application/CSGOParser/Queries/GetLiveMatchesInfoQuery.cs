using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.CSGOParser.Queries
{
    public class GetLiveMatchesInfoQuery: IRequest<List<CSGOLiveMatchesInfo>>
    {

    }

    public class GetLiveMatchesInfoQueryHandler : IRequestHandler<GetLiveMatchesInfoQuery, List<CSGOLiveMatchesInfo>>
    {
        private readonly ICSGOMatchesParser _cSGOMatchesParser;

        public GetLiveMatchesInfoQueryHandler(ICSGOMatchesParser cSGOMatchesParser)
        {
            _cSGOMatchesParser = cSGOMatchesParser;
        }

        public async Task<List<CSGOLiveMatchesInfo>> Handle(GetLiveMatchesInfoQuery request, CancellationToken cancellationToken)
        {
            return await _cSGOMatchesParser.GetAllLiveMatchesAsync();
        }
    }
}
