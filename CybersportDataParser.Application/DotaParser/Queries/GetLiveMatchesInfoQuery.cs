﻿using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.Dota;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.DotaParser.Queries
{
    public class GetLiveMatchesInfoQuery: IRequest<List<DotaLiveMatchesInfo>>
    {
    }

    public class GetLiveMatchesInfoQueryHandler : IRequestHandler<GetLiveMatchesInfoQuery, List<DotaLiveMatchesInfo>>
    {
        private readonly IDotaMatchesParser _dotaMatchesParser;

        public GetLiveMatchesInfoQueryHandler(IDotaMatchesParser dotaMatchesParser)
        {
            _dotaMatchesParser = dotaMatchesParser;
        }

        public async Task<List<DotaLiveMatchesInfo>> Handle(GetLiveMatchesInfoQuery request, CancellationToken cancellationToken)
        {
            return _dotaMatchesParser.GetLiveMatches();
        }
    }
}
