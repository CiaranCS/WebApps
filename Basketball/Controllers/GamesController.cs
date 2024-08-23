using Basketball.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly IGameService _gameService;


        public GamesController (IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetAllGames()
        {
            var allGames = _gameService.GetAllGames();
            return Ok(allGames);
        }


        [HttpGet("GetGameById/{gameId}")]
        public IActionResult GetGame([FromRoute]int gameId)
        {

            var game = _gameService.GetGame(gameId);
            if (game is null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        public IActionResult CreateGame()
        {
            var allGames = _gameService.GetAllGames();
            return Ok(allGames);
        }


    }


    public class GameInfo
    {

    }


    public class CreateGame
    {

    }

}
