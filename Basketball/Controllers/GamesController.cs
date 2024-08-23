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

        [HttpPost("LogGame")]
        public IActionResult CreateGame(CreateGameInfo newGame)
        {
            _gameService.CreateGame(newGame);
            return Ok();
        }



    }


    public class CreateGameInfo
    {

        public int HometeamId { get; set; }
        public int AwayteamId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int WinnerTeamId { get; set; }
        public DateTimeOffset StartTime { get; set; }


    }


    public class NewGameTeam 
    {
        public int TeamId { get; set; }
        public int GameId { get; set; }
    }

}

