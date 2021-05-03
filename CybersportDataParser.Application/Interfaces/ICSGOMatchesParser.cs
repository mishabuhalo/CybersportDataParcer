using CybersportDataParser.Application.Models.CSGO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CybersportDataParser.Application.Interfaces
{
    public interface ICSGOMatchesParser
    {
        Task<List<CSGOLiveMatchesInfo>> GetAllLiveMatchesAsync();
    }
}
