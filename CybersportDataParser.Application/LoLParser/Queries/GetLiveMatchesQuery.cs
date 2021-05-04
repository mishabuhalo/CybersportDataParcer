using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.LOL;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.LoLParser.Queries
{
    public class GetLiveMatchesQuery: IRequest<List<LoLMatchesInfo>>
    {
    }
    public class GetLiveMatchesQueryHandler : IRequestHandler<GetLiveMatchesQuery, List<LoLMatchesInfo>>
    {
        private readonly ILoLMatchesParser _loLMatchesParser;

        public GetLiveMatchesQueryHandler(ILoLMatchesParser loLMatchesParser)
        {
            _loLMatchesParser = loLMatchesParser;
        }

        public async Task<List<LoLMatchesInfo>> Handle(GetLiveMatchesQuery request, CancellationToken cancellationToken)
        {
            return _loLMatchesParser.GetLiveMatches();
        }
    }
}
