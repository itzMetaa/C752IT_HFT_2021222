using C752IT_HFT_2021222.Models;
using System;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

namespace C752IT_HFT_2021222.Client
{
    class Program
    {
        static RestService rest;
        static void Action(string entity)
        {
            if (entity== "AveragePriceOfGames")
            {
                var x = rest.GetSingle<double?>("api/Stat/AveragePriceOfGames");
                Console.WriteLine($"{x.Value}");
                Console.ReadLine();
            }
            if (entity== "NumberOfGamesPerType")
            {
                var x = rest.Get<KeyValuePair<GameType, int>>("api/Stat/NumberOfGamesPerType");
                foreach (var item in x)
                {
                    Console.WriteLine($"{item.Key}, {item.Value}");
                }
                
                Console.ReadLine();
            }
        }
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
            if (entity == "Publisher")
            {
                var publishers = rest.Get<Publisher>("api/publisher");
                foreach (var item in publishers)
                {
                    Console.WriteLine($"{item.Id} {item.Name}");
                }
            }
            if (entity == "Developer")
            {
                var devs = rest.Get<Developer>("api/developer");
                foreach (var item in devs)
                {
                    Console.WriteLine($"{item.Id} {item.Name}");
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
                Console.WriteLine("You can skip by writing \'s\'");
                Console.WriteLine($"New title [old: {temp.Title}]: ");
                string title = Console.ReadLine();
                if (title!="s")
                {
                    temp.Title = title;
                }
                Console.WriteLine($"New price [old: {temp.Price}] (whole number): ");
                string price = Console.ReadLine();
                if (price != "s")
                {
                    temp.Price = int.Parse(price);
                }
                Console.WriteLine($"New rating: [old: {temp.Rating}](0.0-10.0 please use hungarian comma \',\' ): ");
                string rating = Console.ReadLine();
                if (rating != "s")
                {
                    temp.Rating = double.Parse(rating);
                }
                Console.WriteLine($"New number of copies sold: [old: {temp.CopiesSold}] (whole number): ");
                string copies = Console.ReadLine();
                if (copies != "s")
                {
                    temp.CopiesSold = int.Parse(copies);
                }
                Console.WriteLine($"New description: [old: {temp.Description}] (max 200): ");
                string desc = Console.ReadLine();
                if (desc != "s")
                {
                    temp.Description = desc;
                }
                Console.WriteLine($"New type: [old: {temp.Type}]: ");
                foreach (var item in  (GameType[]) Enum.GetValues(typeof(GameType)))
                {
                    Console.Write($"{item}[{item.ToString().Substring(0,2).ToLower()}] ");
                }
                string type = Console.ReadLine();
                switch (type.ToLower())
                {
                    case "fp":
                        temp.Type = GameType.FPS;
                        break;
                    case "rp":
                        temp.Type = GameType.RPG;
                        break;
                    case "pu":
                        temp.Type = GameType.Puzzle;
                        break;
                    case "ho":
                        temp.Type = GameType.Horror;
                        break;
                    case "lo":
                        temp.Type = GameType.Looter;
                        break;
                    case "rh":
                        temp.Type = GameType.Rhythm;
                        break;
                    case "in":
                        temp.Type = GameType.Indie;
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"New DeveloperId: [old: {temp.DeveloperId}]: ");
                string devId = Console.ReadLine();
                if (devId != "s")
                {
                    temp.DeveloperId = int.Parse(devId);
                }
                rest.Put(temp, "game");
            }
            if (entity == "Publisher")
            {
                Console.Write("Enter publisher's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Publisher temp = rest.Get<Publisher>(id, "api/publisher");
                Console.WriteLine($"New name [old: {temp.Name}]: ");
                string name = Console.ReadLine();
                temp.Name = name;
                rest.Put(temp, "api/publisher");
            }
            if (entity == "Developer")
            {
                Console.Write("Enter developer's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Developer temp = rest.Get<Developer>(id, "api/developer");
                Console.WriteLine("You can skip by writing \'s\'");
                Console.WriteLine($"New name [old: {temp.Name}]: ");
                string name = Console.ReadLine();
                if (name != "s")
                {
                    temp.Name = name;
                }
                Console.WriteLine($"New team size [old: {temp.TeamSize}]: (whole number)");
                string size = Console.ReadLine();
                if (size != "s")
                {
                    temp.TeamSize = int.Parse(size);
                }
                Console.WriteLine($"New PublisherId [old: {temp.PublisherId}]: (whole number)");
                string pubId = Console.ReadLine();
                if (pubId != "s")
                {
                    temp.PublisherId = int.Parse(pubId);
                }
                rest.Put(temp, "api/developer");
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
            if (entity == "Developer")
            {
                Console.Write("Enter Developer's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "api/developer");
            }
            if (entity == "Publisher")
            {
                Console.Write("Enter Publisher's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "api/publisher");
            }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:54503/", "game");

            // First level submenus
            var publisherSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Publisher"))
                .Add("Create", () => Create("Publisher"))
                .Add("Delete", () => Delete("Publisher"))
                .Add("Update", () => Update("Publisher"))
                .Add("Get games of a publisher", () => Action("PublisherGames"))
                .Add("Exit", ConsoleMenu.Close)
                ;
            var developerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Developer"))
                .Add("Create", () => Create("Developer"))
                .Add("Delete", () => Delete("Developer"))
                .Add("Update", () => Update("Developer"))
                .Add("Get games of a developer", () => Action("DeveloperGames"))
                .Add("Exit", ConsoleMenu.Close)
                ;

            var gameSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", ()=>List("Game"))
                .Add("Create", ()=>Create("Game"))
                .Add("Delete", ()=>Delete("Game"))
                .Add("Update", ()=>Update("Game"))
                .Add("Get average price of games", ()=>Action("AveragePriceOfGames"))
                .Add("Get number of games per type", ()=> Action("NumberOfGamesPerType"))
                .Add("Exit", ConsoleMenu.Close)
                ;

            //Main menu
            var menu = new ConsoleMenu(args, level: 0)
                .Add("Games", () => gameSubMenu.Show())
                .Add("Developers", () => developerSubMenu.Show())
                .Add("Publishers", () => publisherSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close)
                ;

            menu.Show();
        }
    }
}
