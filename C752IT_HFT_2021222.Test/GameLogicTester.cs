using C752IT_HFT_2021222.Logic;
using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Test
{
    [TestFixture]
    class GameLogicTester
    {
        GameLogic logic;
        Mock<IRepository<Game>> mockGameRepo;

        [SetUp]
        public void Init()
        {
            mockGameRepo = new Mock<IRepository<Game>>();

            var games = new List<Game>()
            {
                new Game(){
                    Id = 1,
                    Title = "Test1",
                    Price = 20,
                    Rating = 9.2,
                    CopiesSold = 4000,
                    Description= "Revolutionising",
                    Type = GameType.FPS
                },
                new Game(){
                    Id = 2,
                    Title = "Test2",
                    Price = 40,
                    Rating = 6.2,
                    CopiesSold = 500,
                    Description= "Halo",
                    Type = GameType.Indie
                },
                new Game(){
                    Id = 3,
                    Title = "Test3",
                    Price = 50,
                    Rating = 7.4,
                    CopiesSold = 2,
                    Description= "Nope",
                    Type = GameType.Horror
                }
            }.AsQueryable();

            mockGameRepo.Setup(m => m.ReadAll()).Returns(games);
            logic = new GameLogic(mockGameRepo.Object);
        }
        [Test]
        public void GetAveragePriceOfGamesTest()
        {
            var avg = logic.GetAveragePriceOfGames();
            Assert.That(avg, Is.EqualTo((20 + 40 + 50.0) / 3));
        }
        [Test]
        public void GetMostProfitableGameTest()
        {
            var avg = logic.GetMostProfitableGame();
            Assert.That(avg.TotalRevenue, Is.EqualTo(4000*20));
        }

        [Test]
        public void GetGameRevenueInfo()
        {
            var g1 = new Game()
            {
                Id = 1,
                Title = "Test1",
                Price = 20,
                Rating = 9.2,
                CopiesSold = 4000,
                Description = "Revolutionising",
                Type = GameType.FPS
            };
            var g2 = new Game()
            {
                Id = 2,
                Title = "Test2",
                Price = 40,
                Rating = 6.2,
                CopiesSold = 500,
                Description = "Halo",
                Type = GameType.Indie
            };
            var g3 = new Game()
            {
                Id = 3,
                Title = "Test3",
                Price = 50,
                Rating = 7.4,
                CopiesSold = 2,
                Description = "Nope",
                Type = GameType.Horror
            };

            var actual = logic.GetGameRevenueInfo();
            var expected = new List<GameInfo>() {
                new GameInfo(){ 
                    Game = g1,
                    TotalRevenue = g1.CopiesSold*g1.Price 
                },
                new GameInfo(){
                    Game = g2,
                    TotalRevenue = g2.CopiesSold*g2.Price
                },
                new GameInfo(){
                    Game = g3,
                    TotalRevenue = g3.CopiesSold*g3.Price
                },
            }.AsQueryable();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CreateInCorrectGameTest()
        {
            var game = new Game() { Title = "XD" };
            try
            {
                logic.Create(game);
            }
            catch
            {

            }

            mockGameRepo.Verify(r => r.Create(game),Times.Never);
        }
        [Test]
        public void CreateCorrectGameTest()
        {
            var game = new Game() { Title = "XD3" };
            logic.Create(game);
            mockGameRepo.Verify(r => r.Create(game), Times.Once);
        }
    }
}
