using System.Text.Json.Serialization;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOPlayerInfo
    {
        [JsonPropertyName("nickname")]
        public string NickName { get; set; }
        [JsonPropertyName("rating")]
        public string Rating { get; set; }
        [JsonPropertyName("kpr")]
        public string KilsPerRound { get; set; }
        [JsonPropertyName("dpr")]
        public string DeathPerRound { get; set; }
        [JsonPropertyName("kast")]
        public string Kast { get; set; }
        [JsonPropertyName("impact")]
        public string Impact { get; set; }
        [JsonPropertyName("adr")]
        public string ADR { get; set; }
    }
}
