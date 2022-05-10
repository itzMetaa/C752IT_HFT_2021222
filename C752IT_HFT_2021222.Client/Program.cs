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
            var repo = new GameRepository(ctx);
            var logic = new GameLogic(repo);

            /*
            Game g = new Game()
            {
                Title = "teszt",
                Price = 30,

            };*/

            var item = logic.ReadAll();
            ;
        }
    }
}
