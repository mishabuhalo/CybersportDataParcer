using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOMatchLiveStats
    {
        public string RoundText { get; set; }
        public string CtScore { get; set; }
        public string TScore { get; set; }
        public string RoundTime { get; set; }
        public List<CSGOTeamLiveStat> TeamStats { get; set; }

        public CSGOMatchLiveStats()
        {
            TeamStats = new List<CSGOTeamLiveStat>();
        }
    }
}
