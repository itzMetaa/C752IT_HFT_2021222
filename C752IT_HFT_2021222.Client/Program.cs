using C752IT_HFT_2021222.Models;
using System;
using System.Linq;
using ConsoleTools;

namespace C752IT_HFT_2021222.Client
{
    class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Game")
            {
                Console.WriteLine("Enter game title: ");
                string title = Console.ReadLine();
                Console.WriteLine("Enter game price (whole number): ");
                int price = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter game rating(0.0-10.0 please use hungarian comma \",\" ): ");
                double rating = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter number of copies sold (whole number): ");
                int copies = int.Parse(Console.ReadLine());
                Console.WriteLine($"Enter a description (max 200): ");
                string description = Console.ReadLine();
                rest.Post(new Game() { Title = title, Price = price, Rating = rating, CopiesSold = copies, Description = description }, "game");
            }
            if (entity == "Publisher")
            {
                Console.WriteLine("Enter publisher name: ");
                string name = Console.ReadLine();
                rest.Post(new Publisher() { Name = name}, "api/publisher");
            }
            if (entity == "Developer")
            {
                Console.WriteLine("Enter developer team's name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter developer team's size (whole number): ");
                int size = int.Parse(Console.ReadLine());
                rest.Post(new Developer() { Name = name, TeamSize = size, }, "api/developer");
            }
        }
        static void List(string entity)
        {
            if (entity == "Game")
            {
                var games = rest.Get<Game>("game");
                foreach (var item in games)
                {
                    Console.WriteLine($"{item.Id} {item.Title}");
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Game")
            {
                Console.Write("Enter game's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Game temp = rest.Get<Game>(id, "game");
                Console.Write($"New title: [old: {temp.Title}]: ");
                string title = Console.ReadLine();
                temp.Title = title;
                rest.Put(temp, "game");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Game")
            {
                Console.Write("Enter Game's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "game");
            }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:54503/", "game");

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
