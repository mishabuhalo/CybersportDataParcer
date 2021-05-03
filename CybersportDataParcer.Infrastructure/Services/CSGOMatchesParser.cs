using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CybersportDataParcer.Infrastructure.Services
{
    public class CSGOMatchesParser : ICSGOMatchesParser
    {
        private readonly IWebDriver _webDriver;
        public CSGOMatchesParser()
        {
            _webDriver = new ChromeDriver();
        }
        public async Task<List<CSGOLiveMatchesInfo>> GetAllLiveMatchesAsync()
        {
            var result = new List<CSGOLiveMatchesInfo>();
            _webDriver.Navigate().GoToUrl("https://www.hltv.org/matches");

            var liveMatches = _webDriver.FindElements(By.XPath("//div[@class='liveMatches']//div[@class='liveMatch']"));

            foreach(var liveMatch in liveMatches)
            {
                CSGOLiveMatchesInfo liveMatchInfo = new CSGOLiveMatchesInfo();

                var matchMeta = liveMatch.FindElement(By.XPath("//div[@class='matchMeta']"))?.Text;

                liveMatchInfo.MatchMeta = matchMeta;

                var matchTeams = liveMatch.FindElements(By.ClassName("matchTeam"));

                foreach(var team in matchTeams)
                {
                    var csgoTeam = new CSGOMatchTeam();

                    var teamName = team.FindElement(By.ClassName("matchTeamName"))?.Text;
                    var currentMatchScore = team.FindElement(By.ClassName("currentMapScore"))?.Text;
                    var mapScore = team.FindElement(By.ClassName("mapScore"))?.Text;

                    liveMatchInfo.CSGOMatchTeams.Add(new CSGOMatchTeam()
                    {
                        TeamName = teamName,
                        MapScore = mapScore,
                        MatchScore = currentMatchScore
                    });
                }

                var matchEventName = liveMatch.FindElement(By.ClassName("matchEventName"))?.Text;
                liveMatchInfo.MatchEventName = matchEventName;

                result.Add(liveMatchInfo);
            }

            _webDriver.Close();
            return result;
        }
    }
}
