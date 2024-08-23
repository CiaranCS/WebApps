using Basketball.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Basketball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamsController(ITeamService teamService) 
        { 
            _teamService = teamService;
        }


        [HttpGet]
        public IActionResult GetTeams()
       {
            var allTeams = _teamService.GetAll();
            return Ok(allTeams);
        }


        [HttpGet("TeamId")]
        public IActionResult GetTeamsWithQuery([FromQuery] TeamInfo teamInfo)
        {
            return Ok(teamInfo);

        }


        [HttpGet("GetTeamById/{teamId}")]
        public IActionResult GetTeamById([FromRoute] int teamId)
        {
            
            var team = _teamService.Get(teamId);
            if (team is null)
            {
                return NotFound();
            }
            return Ok(team);
        }



        [HttpPost("CreateTeam")]
        public IActionResult CreateTeam([FromBody] CreateTeamRequest newTeam)
        {
           _teamService.Create(newTeam);
            return Ok();
        }



        [HttpPut("UpdateTeamInfo")]
        public IActionResult UpdateLocationAndCount([FromBody] UpdateTeamRequest team)
        {
            _teamService.Update(team);
            var updatedTeam = GetTeamById(team.TeamId);

            return Ok(updatedTeam);
           
        }



        [HttpDelete("DeleteTeamById/{teamId}")]
        public IActionResult DeleteByID([FromRoute] int teamId)
        {
            _teamService.Delete(teamId);
            return Ok();

        }

    }

    public class TeamInfo
    {
        public string TeamName { get; set; }
        public string Location { get; set; }
        public int TeamId { get; set; }
        public int Count { get; set; }
    }


    public class CreateTeamRequest
    {
        [StringLength(16)]
        [MinLength(1)]
        public string TeamName { get; set; }
        public string? Location { get; set; }

        [MinLength(4, ErrorMessage = "Minimum Number of players required is 4")]
        public List<NewPlayer> NewPlayers { get; set; }

    }

    public class NewPlayer
    {
        public string Name { get; set; }

        [Range(0, 99)]
        public int Number { get; set; }

        public double ThreePts { get; set; }
        public double TwoPts { get; set; }

    }
    public class UpdateTeamRequest
    {
        public string Location { get; set; }
        public int TeamId { get; set; }



    }

}
