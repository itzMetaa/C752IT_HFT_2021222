using C752IT_HFT_2021222.Models;
using System.Linq;

namespace C752IT_HFT_2021222.Logic
{
    interface IGameLogic
    {
        void Create(Game item);
        void Delete(int id);
        double? GetAveragePriceOfGames();
        Game GetMostProfitableGame();
        Game Read(int id);
        IQueryable<Game> ReadAll();
        void Update(Game item);
    }
}