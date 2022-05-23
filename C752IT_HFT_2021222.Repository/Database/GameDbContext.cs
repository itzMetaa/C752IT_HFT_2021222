using Microsoft.EntityFrameworkCore;
using C752IT_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Repository
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public GameDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|game.mdf;Integrated Security=True,MultipleActiveResultSets = true";
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("game");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(game => game
            .HasOne(game => game.Developer)
            .WithMany(dev => dev.Games)
            .HasForeignKey(game => game.DeveloperId)
            .OnDelete(DeleteBehavior.Cascade)
            );

            modelBuilder.Entity<Developer>(x => x
            .HasOne(x => x.Publisher)
            .WithMany(x => x.Developers)
            .HasForeignKey(x => x.PublisherId)
            .OnDelete(DeleteBehavior.Cascade)
            );

            //modelBuilder.Entity<Publisher>(x => x
            //.HasMany(x => x.Developers)
            //.WithOne(x => x.Publisher)
            //.OnDelete(DeleteBehavior.Cascade)
            //);

            modelBuilder.Entity<Publisher>().HasData(new Publisher[]
                {
                    new Publisher(1,"Activision"),
                    new Publisher(2,"2K Games"),
                    new Publisher(3,"BattleState Games"),
                    new Publisher(4,"Bandai Namco"),
                    new Publisher(5,"Capcom"),
                    new Publisher(6,"Digital Extremes"),
                    new Publisher(7,"HuniePot"),
                    new Publisher(8,"ppy"),
                });

            modelBuilder.Entity<Developer>().HasData(new Developer[]
                {
                    new Developer(1,"Infinity Ward",500,1),
                    new Developer(2,"Gearbox Studios",300,2),
                    new Developer(3,"Treyarch",700,1),
                    new Developer(4,"BattleState Games",50,3),
                    new Developer(5,"FromSoftware",450,4),
                    new Developer(6,"Capcom",1250,5),
                    new Developer(7,"Digital Extremes",300,6),
                    new Developer(8,"HuniePot",4,7),
                    new Developer(9,"Dean Herbert",1,8),
                });

            modelBuilder.Entity<Game>().HasData(new Game[]
                {
                    new Game(1,"Call of Duty: Black Ops III",90,7.3,2000000,"best zombi",GameType.FPS,3),
                    new Game(2,"Call of Duty: Black Ops",20,7.6,1400000,"second legjobb zombi",GameType.FPS,3),
                    new Game(3,"Call of Duty 5: World at War",15,8.3,450000,"og zombik",GameType.FPS,3),
                    new Game(4,"Call of Duty 4: Modern Warfare ",20,9.2,4000000,"Legjobb esport",GameType.FPS,1),
                    new Game(5,"Call of Duty Warzone",60,7.9,3200000,"fuj battleroyale",GameType.FPS,1),
                    new Game(6,"Borderlands 2",15,8.2,1100000,"best game ever frfr ++ Moxxxy sheesh",GameType.Looter,2),
                    new Game(7,"Escape from Tarkov",40,7.5,500000,"best game rn no cap extra daddy Prapor",GameType.FPS,4),
                    new Game(8,"Elden Ring",60,9.6,7000000,"down bad npc",GameType.RPG,5),
                    new Game(9,"Resident Evil Village",50,8.4,600000,"vampire mommy",GameType.Horror,6),
                    new Game(10,"Warframe",0,8.3,30000000,"space ninjas and alien mommy",GameType.Looter,7),
                    new Game(11,"HuniePop",10,8.1,40000,"waifu dating simulator",GameType.Puzzle,8),
                    new Game(12,"osu!",0,9,27000000,"rhythm is just a click away",GameType.Rhythm,9),
                });
        }
    }
}
