namespace CybersportDataParser.Application.Models.Dota
{
    public class DotaMatchLiveStat
    {
        public DotaMatchTeamLiveStat FirstTeamStat { get; set; }
        public DotaMatchTeamLiveStat SecondTeamStat { get; set; }

        public DotaMatchLiveStat()
        {
            FirstTeamStat = new DotaMatchTeamLiveStat();
            SecondTeamStat = new DotaMatchTeamLiveStat();
        }
    }
}
