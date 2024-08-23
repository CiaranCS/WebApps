namespace Basketball.Entities
{
    public class GameTeam
    {
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
