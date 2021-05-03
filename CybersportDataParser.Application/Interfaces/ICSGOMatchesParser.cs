﻿using CybersportDataParser.Application.Models.CSGO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.Interfaces
{
    public interface ICSGOMatchesParser
    {
        Task<List<CSGOLiveMatchesInfo>> GetAllLiveMatchesAsync();
        Task<List<CSGOUpcomingMatchesInfo>> GetAllUpcomingMatchesAsync();
        Task<CSGOMatchDetails> GetMatchDetailsByUrlAsync(string matchUrl);
    }
}
