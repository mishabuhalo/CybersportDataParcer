using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.Dota;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CybersportDataParcer.Infrastructure.Services
{
    public class DotaMatchesParser: IDotaMatchesParser
    {
        private IWebDriver _webDriver;

        public DotaMatchesParser()
        {
            _webDriver = new ChromeDriver();
        }

        public List<DotaMatchesInfo> GetLiveMatches()
        {
            _webDriver.Manage().Timeouts().PageLoad= TimeSpan.FromSeconds(5);
            var result = new List<DotaMatchesInfo>();
            try
            {
                _webDriver.Navigate().GoToUrl("https://www.cybersport.ru/base/match?disciplines=21&status=active");

            }
            catch
            {
                var liveMatchesList = _webDriver.FindElement(By.ClassName("matches__list"));

                var matches = liveMatchesList.FindElements(By.ClassName("matches__item"));

                foreach (var match in matches.Skip(1))
                {
                    result.Add(GetDotaMatchesInfo(match));
                }

            }
            _webDriver.Close();
            return result;

        }

        public DotaMatchDetails GetMatchDetailsByUrl(string matchUrl)
        {
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(12);
            var result = new DotaMatchDetails();

            try
            {
                _webDriver.Navigate().GoToUrl(matchUrl);
            }
            catch
            {
                result.EventName = _webDriver.FindElement(By.ClassName("revers"))?.Text;
                result.EventDetails = _webDriver.FindElement(By.ClassName("match-duel-header__time"))?.Text;

                var matchDuelConteiner = _webDriver.FindElement(By.ClassName("match-duel-wrapper"));

                var firstTeam = matchDuelConteiner.FindElements(By.ClassName("match-duel__item")).First();
                var secondTeam = matchDuelConteiner.FindElements(By.ClassName("match-duel__item")).Last();

                result.FirstTeamDetails.TeamName = firstTeam.FindElement(By.ClassName("match-duel-team__name"))?.Text;
                var firstTeamPlayers = firstTeam.FindElements(By.ClassName("match-duel-player"));
                foreach(var firstTeamPlayer in firstTeamPlayers)
                {
                    var dotaplayerDetails = new DotaPlayerDetails()
                    {
                        NickName = firstTeamPlayer.FindElement(By.ClassName("match-duel-player__about-nickname"))?.Text,
                        Fullname = firstTeamPlayer.FindElement(By.ClassName("match-duel-player__about-name"))?.Text
                    };
                    result.FirstTeamDetails.Players.Add(dotaplayerDetails);
                }

                result.SecondTeamDetails.TeamName = secondTeam.FindElement(By.ClassName("match-duel-team__name"))?.Text;
                var secondTeamPlayers = secondTeam.FindElements(By.ClassName("match-duel-player"));
                foreach (var secondTeamPlayer in secondTeamPlayers)
                {
                    var dotaplayerDetails = new DotaPlayerDetails()
                    {
                        NickName = secondTeamPlayer.FindElement(By.ClassName("match-duel-player__about-nickname"))?.Text,
                        Fullname = secondTeamPlayer.FindElement(By.ClassName("match-duel-player__about-name"))?.Text
                    };
                    result.SecondTeamDetails.Players.Add(dotaplayerDetails);
                }

                var firstTeamMatchesContainer = _webDriver.FindElements(By.ClassName("matches"))[1];
                var secondTeamMatchesContainer = _webDriver.FindElements(By.ClassName("matches"))[2];

                var firstTeamMatches = firstTeamMatchesContainer.FindElements(By.ClassName("matches__item"));
                var secondTeamMatches = secondTeamMatchesContainer.FindElements(By.ClassName("matches__item"));

                foreach(var firstTeamMatch in firstTeamMatches.Skip(1))
                {
                    result.FirstTeamDetails.LastMatches.Add(GetDotaMatchesInfo(firstTeamMatch));
                }

                foreach (var secondTeamMatch in secondTeamMatches.Skip(1))
                {
                    result.SecondTeamDetails.LastMatches.Add(GetDotaMatchesInfo(secondTeamMatch));
                }

            }

            _webDriver.Close();
            return result;
        }

        public DotaMatchLiveStat GetMatchLiveStatByUrl(string matchUrl)
        {
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            var result = new DotaMatchLiveStat();

            try
            {
                _webDriver.Navigate().GoToUrl(matchUrl);
            }
            catch
            {
                var statisticContainer = _webDriver.FindElement(By.ClassName("statistic-dota2__tables"));


                var firstTeamStatistic = statisticContainer.FindElements(By.ClassName("statistic-dota2__tbody")).First();



                result.FirstTeamStat.TeamName = Regex.Match(firstTeamStatistic.FindElement(By.ClassName("text-align--left")).GetAttribute("outerHTML"), @"> .+<").Value.Trim(new char[] { '<', '>' });
                result.FirstTeamStat.Side = "Radiant";

                var firstTeamPlayerStats = firstTeamStatistic.FindElements(By.ClassName("statistic-dota2__row--background-color-green"));


                result.FirstTeamStat.PlayerStats = GetPlayersLiveStat(firstTeamPlayerStats);

                var secondTeamStatistic = statisticContainer.FindElements(By.ClassName("statistic-dota2__tbody")).Last();
                result.SecondTeamStat.TeamName = Regex.Match(secondTeamStatistic.FindElement(By.ClassName("text-align--left")).GetAttribute("outerHTML"), @"> .+<").Value.Trim(new char[] { '<', '>' });
                result.SecondTeamStat.Side = "Dire";

                var secondTeamPlayerStsts = secondTeamStatistic.FindElements(By.ClassName("statistic-dota2__row--background-color-red"));

                result.SecondTeamStat.PlayerStats = GetPlayersLiveStat(secondTeamPlayerStsts);

            }

            _webDriver.Close();
            return result;
        }

        public List<DotaMatchPlayerLiveStat> GetPlayersLiveStat(IReadOnlyCollection<IWebElement> playerStats)
        {
            var result = new List<DotaMatchPlayerLiveStat>();


            foreach(var playerStat in playerStats)
            {
                var playerLiveStats = new DotaMatchPlayerLiveStat();
                
                playerLiveStats.Nickname = Regex.Match(playerStat.FindElement(By.ClassName("statistic-dota2__title")).GetAttribute("outerHTML"), @">.+<").Value.Trim(new char[] {'<', '>' });
                playerLiveStats.Kills = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[0].GetAttribute("outerHTML"), @" \d+ ").Value.Trim();
                playerLiveStats.Deaths = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[1].GetAttribute("outerHTML"), @" \d+ ").Value.Trim();
                playerLiveStats.Assists = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[2].GetAttribute("outerHTML"), @" \d+ ").Value.Trim();
                 
                playerLiveStats.Denies = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[3].GetAttribute("outerHTML"), @" \d+ \/ \d+ ").Value.Trim();
                playerLiveStats.GPM_EPM = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[4].GetAttribute("outerHTML"), @" \d+ \/ \d+ ").Value.Trim();
                playerLiveStats.Damage = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[5].GetAttribute("outerHTML"), @"( \d+ | - )").Value.Trim();
                playerLiveStats.Heal = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[6].GetAttribute("outerHTML"), @"( \d+ | - )").Value.Trim();
                playerLiveStats.TowerDamage = Regex.Match(playerStat.FindElements(By.ClassName("tabs__content"))[7].GetAttribute("outerHTML"), @"( \d+ | - )").Value.Trim();

                result.Add(playerLiveStats);
            }

            return result;
        }

        public List<DotaMatchesInfo> GetUpcomingMatches()
        {
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            var result = new List<DotaMatchesInfo>();
            var paginationCount = 0;
            try
            {
                _webDriver.Navigate().GoToUrl("https://www.cybersport.ru/base/match?disciplines=21&status=future");
            }
            catch
            {
                paginationCount = _webDriver.FindElements(By.ClassName("pagination__item")).Count;
            }

            for(int i = 1; i <paginationCount; i++)
            {
                try
                {
                    _webDriver.Navigate().GoToUrl($"https://www.cybersport.ru/base/match?disciplines=21&status=future&page={i}");
                }
                catch
                {
                    var liveMatchesList = _webDriver.FindElement(By.ClassName("matches__list"));

                    var matches = liveMatchesList.FindElements(By.ClassName("matches__item"));

                    foreach (var match in matches.Skip(1))
                    {
                        result.Add(GetDotaMatchesInfo(match));
                    }
                }
            }

            _webDriver.Close();

            return result;
        }

        private DotaMatchesInfo GetDotaMatchesInfo(IWebElement match)
        {
            var matchDate = match.FindElement(By.ClassName("matche__date"))?.Text;
            var firstTeamName = match.FindElements(By.ClassName("team__name")).First()?.Text;
            var matchScore = match.FindElement(By.ClassName("matche__score"))?.Text;
            var secondTeamName = match.FindElements(By.ClassName("team__name")).Last()?.Text;
            var matchInfo = match.FindElement(By.ClassName("matche__meta"))?.Text;
            var matchUrl = match.FindElement(By.ClassName("matches__link")).GetAttribute("href");

            var liveMatchInfo = new DotaMatchesInfo()
            {
                MatchDate = matchDate,
                FirstTeamName = firstTeamName,
                MatchScore = matchScore,
                SecondTeamName = secondTeamName,
                MatchInfo = matchInfo,
                MatchUrl = matchUrl
            };
            return liveMatchInfo;
        }
    }
}
