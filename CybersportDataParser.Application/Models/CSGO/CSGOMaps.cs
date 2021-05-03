using System.Collections.Generic;

namespace CybersportDataParser.Application.Models.CSGO
{
    public class CSGOMaps
    {
        public string MapDetails { get; set; }

        public List<string> PickedMaps { get; set; }

        public CSGOMaps()
        {
            PickedMaps = new List<string>();
        }
    }
}
