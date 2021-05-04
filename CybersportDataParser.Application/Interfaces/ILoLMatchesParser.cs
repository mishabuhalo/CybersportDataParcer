using CybersportDataParser.Application.Models.LOL;
using System.Collections.Generic;

namespace CybersportDataParser.Application.Interfaces
{
    public interface ILoLMatchesParser
    {
        public List<LoLMatchesInfo> GetLiveMatches();
        public List<LoLMatchesInfo> GetUpcomingMatches();
        public LoLMatchDetails GetMatchDetailsByUrl(string matchUrl);
    }
}
