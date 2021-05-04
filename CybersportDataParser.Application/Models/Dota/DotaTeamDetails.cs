using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.Dota
{
    public class DotaTeamDetails
    {
        public string TeamName { get; set; }
        public List<DotaPlayerDetails> Players { get; set; }
        public List<DotaMatchesInfo> LastMatches { get; set; }

        public DotaTeamDetails()
        {
            Players = new List<DotaPlayerDetails>();
            LastMatches = new List<DotaMatchesInfo>();
        }
    }
}
