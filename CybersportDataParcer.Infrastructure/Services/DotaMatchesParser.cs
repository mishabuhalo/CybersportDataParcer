using CybersportDataParser.Application.Interfaces;
using CybersportDataParser.Application.Models.Dota;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                _webDriver.Navigate().GoToUrl("https://www.cybersport.ru/base/match?status=active");

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

        public List<DotaMatchesInfo> GetUpcomingMatches()
        {
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            var result = new List<DotaMatchesInfo>();
            var paginationCount = 0;
            try
            {
                _webDriver.Navigate().GoToUrl("https://www.cybersport.ru/base/match?status=future");
            }
            catch
            {
                paginationCount = _webDriver.FindElements(By.ClassName("pagination__item")).Count;
            }

            for(int i = 1; i <paginationCount; i++)
            {
                try
                {
                    _webDriver.Navigate().GoToUrl($"https://www.cybersport.ru/base/match?status=future&page={i}");
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

            var liveMatchInfo = new DotaMatchesInfo()
            {
                MatchDate = matchDate,
                FirstTeamName = firstTeamName,
                MatchScore = matchScore,
                SecondTeamName = secondTeamName,
                MatchInfo = matchInfo
            };
            return liveMatchInfo;
        }
    }
}
