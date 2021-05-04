namespace CybersportDataParser.Application.Models.LOL
{
    public class LoLMatchDetails
    {
        public string EventDetails { get; set; }
        public string EventName { get; set; }
        public LoLTeamDetails FirstTeamDetails { get; set; }
        public LoLTeamDetails SecondTeamDetails { get; set; }

        public LoLMatchDetails()
        {
            FirstTeamDetails = new LoLTeamDetails();
            SecondTeamDetails = new LoLTeamDetails();
        }
    }
}
