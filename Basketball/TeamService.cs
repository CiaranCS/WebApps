using Basketball.Controllers;
using Basketball.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basketball
{

    public interface ITeamService
    {
        public void Create(CreateTeamRequest newTeam);
        public void Update(UpdateTeamRequest updateTeam);
        public void Delete(int teamId);
        public Team Get(int teamId);
        public List<Team> GetAll();

    }
    public class TeamService : ITeamService
    {
        private DataContext _dataContext = new DataContext();

        public void Create(CreateTeamRequest newTeam)
        {
            Team team = new Team();
            team.Name = newTeam.TeamName;
            team.Location = newTeam.Location;
            var players = new List<Player>();

            foreach( var newPlayer in newTeam.NewPlayers)
            {
                var player = new Player();
                player.Name = newPlayer.Name;
                player.Number = newPlayer.Number;

                var stat = new Statistic();
                stat.ThreePts = newPlayer.ThreePts;
                stat.TwoPts = newPlayer.TwoPts;
                var sum = (stat.ThreePts + stat.TwoPts)/2.0;
                stat.TotalPercent = sum;

                player.Statistics = stat;
                players.Add(player);

            }

            team.Players = players;


            _dataContext.Teams.Add(team);
            _dataContext.SaveChanges();
        }



        public void Delete(int teamId)
        {
            var deleteTeam = Get(teamId);
            _dataContext.Remove(deleteTeam);
            _dataContext.SaveChanges();
        }

        public Team Get(int teamId)
        { 
            var team = _dataContext.Teams
                .Where(t => t.Id == teamId)
                .Include(t => t.Players)
                .ThenInclude(p => p.Statistics)
                .FirstOrDefault();

            return team;
        }

        public List<Team> GetAll()
        {

            var allTeams = _dataContext.Teams;
            return allTeams.ToList();
        }

        public void Update(UpdateTeamRequest updateTeam)
        {
           
            var oldTeam = Get(updateTeam.TeamId);
            oldTeam.Location = updateTeam.Location;
            _dataContext.SaveChanges();
            

        }
    }
}
