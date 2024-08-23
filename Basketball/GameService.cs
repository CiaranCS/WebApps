using Basketball.Controllers;
using Basketball.Entities;

namespace Basketball
{
    public interface IGameService 
    {
        public List<Game> GetAllGames();
    }

    public class GameService : IGameService
    {
        private DataContext _dataContext = new DataContext();

        public List<Game> GetAllGames()
        {

            var allGames = _dataContext.Games;
            return allGames.ToList();

        }

    }


    
}
