using System.ComponentModel.DataAnnotations;

namespace Basketball.Entities
{
    public class Player
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Range(0,99)]
        public int Number { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        
        public Statistic Statistics { get; set; }

    }
}
