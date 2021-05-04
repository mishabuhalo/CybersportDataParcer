using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.LOL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.LoLParser.Queries
{
    public class GetMatchDetailsQuery: IRequest<LoLMatchDetails>
    {
        public string MatchUrl { get; set; }
    }

    public class GetMatchDetailsQueryHandler : IRequestHandler<GetMatchDetailsQuery, LoLMatchDetails>
    {
        private readonly ILoLMatchesParser _loLMatchesParser;

        public GetMatchDetailsQueryHandler(ILoLMatchesParser loLMatchesParser)
        {
            _loLMatchesParser = loLMatchesParser;
        }

        public async Task<LoLMatchDetails> Handle(GetMatchDetailsQuery request, CancellationToken cancellationToken)
        {
            return _loLMatchesParser.GetMatchDetailsByUrl(request.MatchUrl);
        }
    }
}
