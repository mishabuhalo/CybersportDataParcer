namespace CybersportDataParser.Application.Models.Dota
{
    public class DotaMatchDetails
    {
        public string EventDetails { get; set; }
        public string EventName { get; set; }
        public DotaTeamDetails FirstTeamDetails { get; set; }
        public DotaTeamDetails SecondTeamDetails { get; set; }

        public DotaMatchDetails()
        {
            FirstTeamDetails = new DotaTeamDetails();
            SecondTeamDetails = new DotaTeamDetails();
        }
    }
}
