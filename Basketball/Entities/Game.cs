namespace Basketball.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public DateTimeOffset DatePlayed { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int WinnerTeamID { get; set; }

        public List <Team> Teams { get; set; }
        public List<GameTeam> GameTeams { get; set; }

    }
}
