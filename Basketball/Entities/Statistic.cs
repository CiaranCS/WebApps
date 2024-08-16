namespace Basketball.Entities
{
    public class Statistic
    {
        public int Id { get; set; }
        public double TwoPts { get; set; }
        public double ThreePts { get; set; }
        public double TotalPercent { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
