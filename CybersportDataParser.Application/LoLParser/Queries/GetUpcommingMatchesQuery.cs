using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.LOL;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.LoLParser.Queries
{
    public class GetUpcommingMatchesQuery: IRequest<List<LoLMatchesInfo>>
    {
    }
    public class GetUpcommingMatchesQueryHandler : IRequestHandler<GetUpcommingMatchesQuery, List<LoLMatchesInfo>>
    {
        private readonly ILoLMatchesParser _loLMatchesParser;

        public GetUpcommingMatchesQueryHandler(ILoLMatchesParser loLMatchesParser)
        {
            _loLMatchesParser = loLMatchesParser;
        }

        public async Task<List<LoLMatchesInfo>> Handle(GetUpcommingMatchesQuery request, CancellationToken cancellationToken)
        {
            return _loLMatchesParser.GetUpcomingMatches();
        }
    }
}
