using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Logic
{
    public class GameLogic : IGameLogic
    {
        IRepository<Game> repo;

        public GameLogic(IRepository<Game> repo)
        {
            this.repo = repo;
        }

        public void Create(Game item)
        {
            if (item.Title.Length < 3)
            {
                throw new ArgumentException("Title too short");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Game Read(int id)
        {
            var game = this.repo.Read(id);
            if (game == null)
            {
                throw new ArgumentException("Game doesn't exist");
            }
            return game;
        }

        public IQueryable<Game> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Game item)
        {
            this.repo.Update(item);
        }

        //Non cruds

        public double? GetAveragePriceOfGames()
        {
            return this.repo
                .ReadAll()
                .Where(t => t.Price != 0)
                .Average(t => t.Price);
        }

        public Game GetMostProfitableGame()
        {
            var q1 = this.repo
                .ReadAll()
                .Where(t => t.Price * t.CopiesSold > 0);
            return q1.Max();
        }
    }
}
