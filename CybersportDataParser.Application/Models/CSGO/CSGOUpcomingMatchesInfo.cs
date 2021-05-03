using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOUpcomingMatchesInfo
    {
        public string MatchesDate { get; set; }
        public List<CSGOUpcomingMatch> UpcomingMatches { get; set; }

        public CSGOUpcomingMatchesInfo()
        {
            UpcomingMatches = new List<CSGOUpcomingMatch>();
        }
    }

}
