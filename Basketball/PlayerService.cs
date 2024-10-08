﻿using Basketball.Controllers;
using Basketball.Entities;

namespace Basketball
{

    public interface IPlayerService
    {
        public void Update(UpdatePlayer updatePlayer);
        public Player Get(int playerId);
    }

    public class PlayerService : IPlayerService
    {
        private readonly DataContext _dataContext;

        public PlayerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Update(UpdatePlayer updatePlayer) 
        { 
            var oldPlayer = Get(updatePlayer.Id);
            oldPlayer.Number = updatePlayer.Number;
            _dataContext.SaveChanges();
        }

        public Player Get(int playerId)
        {
            var player = _dataContext.Players.Where(p => p.Id == playerId).FirstOrDefault();
            return player;
        }


    }
}
