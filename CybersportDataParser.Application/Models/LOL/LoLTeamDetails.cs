using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.LOL
{
    public class LoLTeamDetails
    {
        public string TeamName { get; set; }
        public List<LoLPlayerDetails> Players { get; set; }
        public List<LoLMatchesInfo> LastMatches { get; set; }

        public LoLTeamDetails()
        {
            Players = new List<LoLPlayerDetails>();
            LastMatches = new List<LoLMatchesInfo>();
        }
    }
}
