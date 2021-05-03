using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOUpcomingMatch
    {
        public string MatchTime { get; set; }
        public string MatchMeta { get; set; }
        public string EventName { get; set; }
        public string MatchUrl { get; set; }
        public List<CSGOShortTeamInfoShort> Teams { get; set; }

        public CSGOUpcomingMatch()
        {
            Teams = new List<CSGOShortTeamInfoShort>();
        }
    }
}
