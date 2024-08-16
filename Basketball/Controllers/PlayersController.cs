using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {

        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpPut]
        public IActionResult UpdatePlayerTeam([FromBody] UpdatePlayer playerId)
        {
            _playerService.Update(playerId);

            return Ok();
        }

    }


    public class UpdatePlayer
    {
        public int Id { get; set; }
        public int Number { get; set; }
        
    }

}
