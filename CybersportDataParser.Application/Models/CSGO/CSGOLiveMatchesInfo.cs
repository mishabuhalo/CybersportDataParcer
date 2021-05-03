using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOLiveMatchesInfo
    {
        [JsonPropertyName("eventName")]
        public string MatchEventName { get; set; }
        public string MatchUrl { get; set; }
        public string MatchMeta { get; set; }
        [JsonPropertyName("teams")]
        public List<CSGOMatchTeam> CSGOMatchTeams { get; set; }

        public CSGOLiveMatchesInfo()
        {
            CSGOMatchTeams = new List<CSGOMatchTeam>();
        }
    }
}
