using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.CSGO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
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

                var matchUrl = liveMatch.FindElements(By.ClassName("match")).First().GetAttribute("href");

                liveMatchInfo.MatchUrl = matchUrl;

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

        public async Task<List<CSGOUpcomingMatchesInfo>> GetAllUpcomingMatchesAsync()
        {
            var result = new List<CSGOUpcomingMatchesInfo>();
            _webDriver.Navigate().GoToUrl("https://www.hltv.org/matches");

            var upcomingMatchesSections = _webDriver.FindElements(By.ClassName("upcomingMatchesSection"));

            foreach(var upcomingMathcesSection in upcomingMatchesSections)
            {
                var upcomingMatchesInfo = new CSGOUpcomingMatchesInfo();

                var matchDate = upcomingMathcesSection.FindElement(By.ClassName("matchDayHeadline"))?.Text;

                upcomingMatchesInfo.MatchesDate = matchDate;

                var upcomingMatches = upcomingMathcesSection.FindElements(By.ClassName("upcomingMatch"));

                foreach(var upcomingMatch in upcomingMatches)
                {
                    var newMatch = new CSGOUpcomingMatch();

                    newMatch.MatchTime = upcomingMatch.FindElement(By.ClassName("matchTime"))?.Text;
                    newMatch.MatchMeta = upcomingMatch.FindElement(By.ClassName("matchMeta"))?.Text;

                    var matchUrl = upcomingMatch.FindElements(By.ClassName("match")).First().GetAttribute("href");
                    newMatch.MatchUrl = matchUrl;
                    try
                    {
                        newMatch.EventName = upcomingMatch.FindElement(By.ClassName("matchEventName"))?.Text;
                    }
                    catch
                    {
                        newMatch.EventName = upcomingMatch.FindElement(By.ClassName("matchInfoEmpty"))?.Text;
                        upcomingMatchesInfo.UpcomingMatches.Add(newMatch);
                        continue;
                    }

                    var matchTeams = upcomingMatch.FindElements(By.ClassName("matchTeam"));

                    foreach(var team in matchTeams)
                    {
                        string teamName = string.Empty;
                        try
                        {
                            teamName = team.FindElement(By.ClassName("matchTeamName"))?.Text;
                        }
                        catch
                        {
                            teamName = team.Text;
                        }
                        newMatch.Teams.Add(new CSGOShortTeamInfoShort { TeamName = teamName });
                    }
                    upcomingMatchesInfo.UpcomingMatches.Add(newMatch);
                }

                result.Add(upcomingMatchesInfo);
            }
            _webDriver.Close();
            return result;
        }

        public async Task<CSGOMatchLiveStats> GetLiveMatchStats(string matchUrl)
        {
            _webDriver.Navigate().GoToUrl(matchUrl);
            var result = new CSGOMatchLiveStats();

            var scoreboard = _webDriver.FindElement(By.ClassName("scoreboard"));

            result.RoundText = scoreboard.FindElement(By.ClassName("round"))?.Text;
            result.RoundTime = scoreboard.FindElement(By.ClassName("timeText"))?.Text;
            result.CtScore = scoreboard.FindElement(By.ClassName("ctScore"))?.Text;
            result.TScore = scoreboard.FindElement(By.ClassName("tScore"))?.Text;

            var teams = scoreboard.FindElements(By.ClassName("team"));

            foreach(var team in teams)
            {
                var teamStat = new CSGOTeamLiveStat();

                teamStat.TeamName = team.FindElement(By.ClassName("teamName"))?.Text;
                var players = team.FindElements(By.ClassName("player"));
                foreach(var player in players)
                {
                    var playerStats = new CSGOPlayerLiveStat();
                    playerStats.HP = player.FindElement(By.ClassName("hp-text"))?.Text;
                    playerStats.Money = player.FindElement(By.ClassName("moneyCell"))?.Text;
                    playerStats.Kills = player.FindElement(By.ClassName("killCell"))?.Text;
                    playerStats.Assists = player.FindElement(By.ClassName("assistCell"))?.Text;
                    playerStats.Deaths = player.FindElement(By.ClassName("deathCell"))?.Text;
                    playerStats.ADR = player.FindElement(By.ClassName("adrCell"))?.Text;
                    playerStats.Nickname = player.FindElement(By.ClassName("nameCell"))?.Text;
                    teamStat.PlayerLiveStats.Add(playerStats);
                }

                result.TeamStats.Add(teamStat);
            }

            result.TeamStats.First().Side = "CT";
            result.TeamStats.Last().Side = "T";


            _webDriver.Close();
            return result;
        }

        public async Task<CSGOMatchDetails> GetMatchDetailsByUrlAsync(string matchUrl)
        {
            _webDriver.Navigate().GoToUrl(matchUrl);

            var result = new CSGOMatchDetails();

            var matchPage = _webDriver.FindElement(By.ClassName("match-page"));
            var timeAndEvent = matchPage.FindElement(By.ClassName("timeAndEvent"));
            result.EventDate = timeAndEvent.FindElement(By.ClassName("date"))?.Text;
            result.EventTime = timeAndEvent.FindElement(By.ClassName("time"))?.Text;
            result.EventName = timeAndEvent.FindElement(By.ClassName("event"))?.Text;

            var teamsBox = matchPage.FindElement(By.ClassName("teamsBox"));

            var teams = teamsBox.FindElements(By.ClassName("team"));

            for(int i = 0; i< teams.Count; i++)
            {
                var team = teams[i];
                var teamDetails = new TeamDetails();
                teamDetails.TeamName = team.FindElement(By.ClassName("teamName"))?.Text;
                teamDetails.TeamCountry =  team.FindElements(By.ClassName($"team{i+1}")).First().GetAttribute("title");

                result.Teams.Add(teamDetails);
            }

            var mapsContainer =  matchPage.FindElement(By.ClassName("maps"));

            result.MatchDetails = mapsContainer.FindElement(By.ClassName("veto-box"))?.Text;

            var maps = mapsContainer.FindElements(By.ClassName("mapname"));

            foreach (var map in maps)
            {
                result.Maps.PickedMaps.Add(map?.Text);
            }

            _webDriver.Close();

            return result;
        }

        public async Task<List<CSGOMatchLineup>> GetMatchLineupByUrlAsync(string matchUrl)
        {
            _webDriver.Navigate().GoToUrl(matchUrl);

            var result = new List<CSGOMatchLineup>();

            var lineupContainer = _webDriver.FindElement(By.ClassName("lineups"));

            var teamNames = lineupContainer.FindElements(By.ClassName("flex-align-center"));

            var firstTeamName = teamNames[1].Text;

            var comparerCotainer = lineupContainer.FindElement(By.ClassName("lineups-compare-container"));

            CSGOMatchLineup firstTeamLineup = new CSGOMatchLineup();

            firstTeamLineup.TeamName = firstTeamName;

            var lineupRegex = @"{""playerId"":[^}]*}";

            var firstTeamLineupData = comparerCotainer.GetAttribute("data-team1-players-data");

            var regex = new Regex(lineupRegex);
            foreach(Match match in regex.Matches(firstTeamLineupData))
            {
                CSGOPlayerInfo cSGOPlayerInfo = JsonSerializer.Deserialize<CSGOPlayerInfo>(match.Value);
                firstTeamLineup.PlayerInfos.Add(cSGOPlayerInfo);
            }
            result.Add(firstTeamLineup);

            var secondTeamName = teamNames[3]?.Text;

            CSGOMatchLineup secondTeamLineup = new CSGOMatchLineup();
            secondTeamLineup.TeamName = secondTeamName;
            var secondTeamLineupData = comparerCotainer.GetAttribute("data-team2-players-data");
            foreach (Match match in regex.Matches(secondTeamLineupData))
            {
                CSGOPlayerInfo cSGOPlayerInfo = JsonSerializer.Deserialize<CSGOPlayerInfo>(match.Value);
                secondTeamLineup.PlayerInfos.Add(cSGOPlayerInfo);
            }

            result.Add(secondTeamLineup);

            _webDriver.Close();
            return result;
        }

        public async Task<List<CSGOMapStatsInfo>> GetMatchMapStatsInfoByUrlAsync(string matchUrl)
        {
            _webDriver.Navigate().GoToUrl(matchUrl);

            var result = new List<CSGOMapStatsInfo>();

            var mapStatsContainer = _webDriver.FindElement(By.ClassName("map-stats-infobox"));

            var teams = mapStatsContainer.FindElements(By.ClassName("team"));

            foreach(var team in teams)
            {
                result.Add(new CSGOMapStatsInfo()
                {
                    TeamName = team?.Text
                });
            }
            var maps = mapStatsContainer.FindElements(By.ClassName("map-stats-infobox-maps"));
            foreach(var map in maps)
            {
                var mapName = map.FindElement(By.ClassName("mapname"))?.Text;
                var firstTeamMapStatData = map.FindElements(By.ClassName("map-stats-infobox-stats")).First();
                var secondTeamMapStatData = map.FindElements(By.ClassName("map-stats-infobox-stats")).Last();

                var firstTeamMapStat = new CSGOMapStat()
                {
                    MapName = mapName,
                    Winrate = firstTeamMapStatData.FindElement(By.ClassName("map-stats-infobox-winpercentage"))?.Text,
                    MapsPlayed = firstTeamMapStatData.FindElement(By.ClassName("map-stats-infobox-maps-played"))?.Text,

                };

                var secondTeamMapStat = new CSGOMapStat()
                {
                    MapName = mapName,
                    Winrate = secondTeamMapStatData.FindElement(By.ClassName("map-stats-infobox-winpercentage"))?.Text,
                    MapsPlayed = secondTeamMapStatData.FindElement(By.ClassName("map-stats-infobox-maps-played"))?.Text,

                };

                result.First().MapStats.Add(firstTeamMapStat);
                result.Last().MapStats.Add(secondTeamMapStat);
            }



            _webDriver.Close();
            return result;
        }
    }
}
