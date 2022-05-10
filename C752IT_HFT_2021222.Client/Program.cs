using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.Repository;
using System;
using System.Linq;

namespace C752IT_HFT_2021222.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<Game> repo = new GameRepository(new GameDbContext());
            //var items = repo.ReadAll().ToArray();

            var g = new Game()
            {
                Title = "tesztGame",
                Description = "Ez egy teszt elem",
                Type = GameType.Indie,
                Price = 40,
            };
            repo.Create(g);

            var another = repo.Read(1);
            another.Title = "teszt2";
            repo.Update(another);

            var items = repo.ReadAll().ToArray();
            ;
        }
    }
}
