using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.Repository;
using C752IT_HFT_2021222.Logic;
using System;
using System.Linq;

namespace C752IT_HFT_2021222.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new GameDbContext();
            var rg = new GameRepository(ctx);
            var rd = new DeveloperRepository(ctx);
            var rp = new PublisherRepository(ctx);

            var lg = new GameLogic(rg);
            var ld = new DeveloperLogic(rd);
            var lp = new PublisherLogic(rp);

            var item2 = lg.GetGameRevenueInfo();
            var i = item2.Max(i => i.TotalRevenue);
            var item3 = lg.GetMostProfitableGame();

            ;
        }
    }
}
