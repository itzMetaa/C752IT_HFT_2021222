using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.Repository;
using C752IT_HFT_2021222.Logic;
using System;
using System.Linq;
using ConsoleTools;

namespace C752IT_HFT_2021222.Client
{
    class Program
    {
        static GameLogic gameLogic;
        static DeveloperLogic developerLogic;
        static PublisherLogic publisherLogic;
        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            switch (entity)
            {
                case "Game":
                    var items = gameLogic.ReadAll();
                    Console.WriteLine("Id" + "\t" + "Title");
                    foreach (var item in items)
                    {
                        Console.WriteLine($"{item.Id}\t{item.Title}");
                    }
                    break;
                case "Publisher":
                    var items2 = publisherLogic.ReadAll();
                    Console.WriteLine("Id" + "\t" + "Name");
                    foreach (var item in items2)
                    {
                        Console.WriteLine($"{item.Id}\t{item.Name}");
                    }
                    break;
                case "Developer":
                    var items3 = developerLogic.ReadAll();
                    Console.WriteLine("Id" + "\t" + "Name");
                    foreach (var item in items3)
                    {
                        Console.WriteLine($"{item.Id}\t{item.Name}");
                    }
                    break;
            }
            Console.WriteLine(entity + " list");
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var ctx = new GameDbContext();
            var rg = new GameRepository(ctx);
            var rd = new DeveloperRepository(ctx);
            var rp = new PublisherRepository(ctx);

            gameLogic = new GameLogic(rg);
            developerLogic = new DeveloperLogic(rd);
            publisherLogic = new PublisherLogic(rp);

            var publisherSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Publisher"))
                .Add("Create", () => Create("Publisher"))
                .Add("Delete", () => Delete("Publisher"))
                .Add("Update", () => Update("Publisher"))
                .Add("Exit", ConsoleMenu.Close)
                ;
            var developerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Developer"))
                .Add("Create", () => Create("Developer"))
                .Add("Delete", () => Delete("Developer"))
                .Add("Update", () => Update("Developer"))
                .Add("Exit", ConsoleMenu.Close)
                ;

            var gameSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", ()=>List("Game"))
                .Add("Create", ()=>Create("Game"))
                .Add("Delete", ()=>Delete("Game"))
                .Add("Update", ()=>Update("Game"))
                .Add("Exit", ConsoleMenu.Close)
                ;

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Games", () => gameSubMenu.Show())
                .Add("Developers", () => publisherSubMenu.Show())
                .Add("Publishers", () => developerSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close)
                ;

            menu.Show();
        }
    }
}
