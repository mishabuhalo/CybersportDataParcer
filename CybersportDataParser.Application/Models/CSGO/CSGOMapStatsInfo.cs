using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOMapStatsInfo
    {
        public string TeamName { get; set; }
        public List<CSGOMapStat> MapStats { get; set; }

        public CSGOMapStatsInfo()
        {
            MapStats = new List<CSGOMapStat>();
        }
    }
}
