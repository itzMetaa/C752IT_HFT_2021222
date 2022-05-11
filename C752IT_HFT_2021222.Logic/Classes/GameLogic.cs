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

        public IEnumerable<GameInfo> GetGameRevenueInfo()
        {
            return from x in this.repo.ReadAll()
                   where x.CopiesSold * x.Price > 0
                   select new GameInfo()
                   {
                       Game = x,
                       TotalRevenue = x.CopiesSold * x.Price
                   };
        }

        public GameInfo GetMostProfitableGame()
        {
            var q = from x in this.repo.ReadAll()
                    where x.CopiesSold * x.Price > 0
                    select new GameInfo()
                    {
                        Game = x,
                        TotalRevenue = x.CopiesSold * x.Price
                    };
            var i = q.Max(x => x.TotalRevenue);
            return q.FirstOrDefault(t => t.TotalRevenue.Equals(i));
        }

        public IEnumerable<KeyValuePair<GameType, int>> GetNumberOfGamesPerType()
        {
            return this.repo
                .ReadAll()
                .GroupBy(x => x.Type)
                .Select(x => new KeyValuePair<GameType, int>(x.Key, x.Count()));
        }
    }

    public class GameInfo
    {
        public Game Game { get; set; }
        public int TotalRevenue { get; set; }

        public override bool Equals(object obj)
        {
            GameInfo b = obj as GameInfo;
            if (b==null)
            {
                return false;
            }
            else
            {
                return this.Game.Equals(b.Game) && this.TotalRevenue.Equals(b.TotalRevenue);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Game, this.TotalRevenue);
        }
    }
}
