using C752IT_HFT_2021222.Models;
using System.Collections.Generic;
using System.Linq;

namespace C752IT_HFT_2021222.Logic
{
    public interface IGameLogic
    {
        void Create(Game item);
        void Delete(int id);
        double? GetAveragePriceOfGames();
        IEnumerable<GameInfo> GetGameRevenueInfo();
        GameInfo GetMostProfitableGame();
        Game Read(int id);
        IQueryable<Game> ReadAll();
        void Update(Game item);
    }
}