using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.Dota
{
    public class DotaMatchTeamLiveStat
    {
        public string TeamName { get; set; }
        public string Side { get; set; }
        public List<DotaMatchPlayerLiveStat> PlayerStats { get; set; }

        public DotaMatchTeamLiveStat()
        {
            PlayerStats = new List<DotaMatchPlayerLiveStat>();
        }
    }
}
