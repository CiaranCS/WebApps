namespace Basketball.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public Player BestPlayer => GetBestPlayer();
        public List<Player> Players { get; set; }


        public Player GetBestPlayer()
        {
            var bestPerecent = Players.Max(p => p.Statistics.TotalPercent);
            var bestPlayer = Players.Where(p => p.Statistics.TotalPercent ==  bestPerecent).FirstOrDefault();
            return bestPlayer;
        }
        

    }

   
}
