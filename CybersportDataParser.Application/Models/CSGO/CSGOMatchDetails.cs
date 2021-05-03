using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOMatchDetails
    {
        public string EventName { get; set; }
        public List<TeamDetails> Teams { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string MatchDetails { get; set; }
        public CSGOMaps Maps { get; set; }

        public CSGOMatchDetails()
        {
            Teams = new List<TeamDetails>();
            Maps = new CSGOMaps();
        }
    }
}
