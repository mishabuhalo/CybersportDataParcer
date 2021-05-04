﻿using CybersportDataParser.Application.Models.Dota;
using System.Collections.Generic;

namespace CybersportDataParser.Application.Interfaces
{
    public interface IDotaMatchesParser
    {
        public List<DotaMatchesInfo> GetLiveMatches();
        public List<DotaMatchesInfo> GetUpcomingMatches();
    }
}
