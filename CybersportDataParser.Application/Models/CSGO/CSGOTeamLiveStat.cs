using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOTeamLiveStat
    {
        public string Side { get; set; }
        public string TeamName { get; set; }
        public List<CSGOPlayerLiveStat> PlayerLiveStats { get; set; }

        public CSGOTeamLiveStat()
        {
            PlayerLiveStats = new List<CSGOPlayerLiveStat>();
        }
    }
}
