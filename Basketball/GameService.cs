using Basketball.Controllers;
using Basketball.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basketball
{
    public interface IGameService 
    {
        public List<Game> GetAllGames();
        public GameDto GetGame(int gameId);
        public void CreateGame(CreateGameInfo newGame);

    }

    public class GameService : IGameService
    {

        private DataContext _dataContext = new DataContext();
        private ILogger<GameService> _logger;

        public GameService(ILogger<GameService> logger)
        {
            _logger = logger;
        }



        private TeamDto GetTeamInfo(Team team)
        {
            var teamDto = new TeamDto
            {
                Id = team.Id,
                TeamName = team.Name,
                Players = []
            };

            foreach (var player in team.Players)
            {
                var playerDto = new PlayerDto
                {
                    Id = player.Id,
                    Name = player.Name,
                    Number = player.Number
                };

                if (player.Statistics is not null)
                {
                    playerDto.Stats = new StatisticDto
                    {
                        Id = player.Statistics.Id,
                        TotalPercent = player.Statistics.TotalPercent,
                        TwoPts = player.Statistics.TwoPts,
                        ThreePts = player.Statistics.ThreePts
                    };
                }

                teamDto.Players.Add(playerDto);
            }
            return teamDto;
        }


        public List<Game> GetAllGames()
        {

            var allGames = _dataContext.Games;
            return allGames.ToList();

        }

        public GameDto GetGame(int gameId)
        {

            var gameTeams = _dataContext.GameTeams.Where(g => g.GameId == gameId)
                .Include(gt => gt.Game)
                .Include(gt => gt.Team)
                .ThenInclude(t => t.Players)
                .ThenInclude(p => p.Statistics)
                .ToList();

            var gameDto = new GameDto
            {
                Id = gameId,
                HomeTeamScore = gameTeams[0].Game.HomeTeamScore,
                AwayTeamScore = gameTeams[0].Game.AwayTeamScore,
                StartTime = gameTeams[0].Game.DatePlayed
            };

            Team homeTeamEntity = gameTeams.Where(gt => gt.Team.Id == gt.Game.HomeTeamID).First().Team;
            gameDto.HomeTeam = GetTeamInfo(homeTeamEntity);


            Team awayTeamEntity = gameTeams.Where(gt => gt.Team.Id == gt.Game.AwayTeamID).First().Team;
            gameDto.AwayTeam = GetTeamInfo(awayTeamEntity);



            return (gameDto);

        }


        public void CreateGame(CreateGameInfo newGame)
        {

            var game = new Game
            {
                HomeTeamID = newGame.HometeamId,
                AwayTeamID = newGame.AwayteamId,
                HomeTeamScore = newGame.HomeTeamScore,
                AwayTeamScore = newGame.AwayTeamScore,
                WinnerTeamID = newGame.WinnerTeamId,
                DatePlayed = newGame.StartTime
            };

            var gameTeams = new List<GameTeam> 
            {
                new GameTeam
                {
                    Game = game,
                    TeamId = game.HomeTeamID
                },
                new GameTeam
                {
                    Game= game,
                    TeamId= game.AwayTeamID
                }

            };

            try
            {

                foreach (var gameTeam in gameTeams)
                {
                    _dataContext.GameTeams.Add(gameTeam);

                }
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Did not save changes to database");
                throw;
            }



        }



    }

    public class GameDto
    {
        public int Id { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public DateTimeOffset StartTime { get; set; }

        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }


    }

    public class TeamDto
    { 
        public int Id { get; set; }
        public string TeamName { get; set; }

        public PlayerDto WorstPlayer => GetWorstPlayer();
        public List<PlayerDto> Players { get; set; }


        private PlayerDto GetWorstPlayer()
        {

            var worstPercent = Players.Min(p => p.Stats?.TotalPercent);
            var worstPlayer = Players.Where(p => p.Stats?.TotalPercent == worstPercent).FirstOrDefault();

            return worstPlayer;
        }
    }
    

    public class StatisticDto
    {
        public int Id { get; set; }
        public double TwoPts { get; set; }
        public double ThreePts { get; set; }
        public double TotalPercent { get; set; }

    }

    public class PlayerDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }

        public StatisticDto Stats { get; set; }
    }
}
