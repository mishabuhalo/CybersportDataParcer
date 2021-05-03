using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOMatchLineup
    {
        public string TeamName { get; set; }
        public List<CSGOPlayerInfo> PlayerInfos { get; set; }

        public CSGOMatchLineup()
        {
            PlayerInfos = new List<CSGOPlayerInfo>();
        }
    }
}
