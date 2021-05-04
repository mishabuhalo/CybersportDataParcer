using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.LOL;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersportDataParcer.Infrastructure.Services
{
    public class LoLMatchesParser : ILoLMatchesParser
    {
        private readonly IWebDriver _webDriver;
        public LoLMatchesParser()
        {
            _webDriver = new ChromeDriver();
        }
        public List<LoLMatchesInfo> GetLiveMatches()
        {
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            var result = new List<LoLMatchesInfo>();
            try
            {
                _webDriver.Navigate().GoToUrl("https://www.cybersport.ru/base/match?disciplines=23955&status=active");

            }
            catch
            {
                var liveMatchesList = _webDriver.FindElement(By.ClassName("matches__list"));

                var matches = liveMatchesList.FindElements(By.ClassName("matches__item"));

                foreach (var match in matches.Skip(1))
                {
                    result.Add(GetLolMatchesInfo(match));
                }

            }
            _webDriver.Close();
            return result;
        }

        public LoLMatchDetails GetMatchDetailsByUrl(string matchUrl)
        {
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(12);
            var result = new LoLMatchDetails();

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
                foreach (var firstTeamPlayer in firstTeamPlayers)
                {
                    var dotaplayerDetails = new LoLPlayerDetails()
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
                    var dotaplayerDetails = new LoLPlayerDetails()
                    {
                        NickName = secondTeamPlayer.FindElement(By.ClassName("match-duel-player__about-nickname"))?.Text,
                        Fullname = secondTeamPlayer.FindElement(By.ClassName("match-duel-player__about-name"))?.Text
                    };
                    result.SecondTeamDetails.Players.Add(dotaplayerDetails);
                }

                var firstTeamMatchesContainer = _webDriver.FindElements(By.ClassName("matches"))[0];
                var secondTeamMatchesContainer = _webDriver.FindElements(By.ClassName("matches"))[1];

                var firstTeamMatches = firstTeamMatchesContainer.FindElements(By.ClassName("matches__item"));
                var secondTeamMatches = secondTeamMatchesContainer.FindElements(By.ClassName("matches__item"));

                foreach (var firstTeamMatch in firstTeamMatches.Skip(1))
                {
                    result.FirstTeamDetails.LastMatches.Add(GetLolMatchesInfo(firstTeamMatch));
                }

                foreach (var secondTeamMatch in secondTeamMatches.Skip(1))
                {
                    result.SecondTeamDetails.LastMatches.Add(GetLolMatchesInfo(secondTeamMatch));
                }

            }

            _webDriver.Close();
            return result;
        }

        public List<LoLMatchesInfo> GetUpcomingMatches()
        {
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

            var result = new List<LoLMatchesInfo>();

            var paginationCount = 0;
            try
            {
                _webDriver.Navigate().GoToUrl("https://www.cybersport.ru/base/match?disciplines=23955&status=future");
            }
            catch
            {
                paginationCount = _webDriver.FindElements(By.ClassName("pagination__item")).Count;
            }

            for (int i = 1; i < paginationCount; i++)
            {
                try
                {
                    _webDriver.Navigate().GoToUrl($"https://www.cybersport.ru/base/match?disciplines=23955&status=future&page={i}");
                }
                catch
                {
                    var liveMatchesList = _webDriver.FindElement(By.ClassName("matches__list"));

                    var matches = liveMatchesList.FindElements(By.ClassName("matches__item"));

                    foreach (var match in matches.Skip(1))
                    {
                        result.Add(GetLolMatchesInfo(match));
                    }
                }
            }

            _webDriver.Close();

            return result;
        }

        private LoLMatchesInfo GetLolMatchesInfo(IWebElement match)
        {
            var matchDate = match.FindElement(By.ClassName("matche__date"))?.Text;
            var firstTeamName = match.FindElements(By.ClassName("team__name")).First()?.Text;
            var matchScore = match.FindElement(By.ClassName("matche__score"))?.Text;
            var secondTeamName = match.FindElements(By.ClassName("team__name")).Last()?.Text;
            var matchInfo = match.FindElement(By.ClassName("matche__meta"))?.Text;
            var matchUrl = match.FindElement(By.ClassName("matches__link")).GetAttribute("href");

            var liveMatchInfo = new LoLMatchesInfo()
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
