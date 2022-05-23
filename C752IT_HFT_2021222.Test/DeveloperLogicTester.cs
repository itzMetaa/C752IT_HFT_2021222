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
    class DeveloperLogicTester
    {
        DeveloperLogic logic;
        Mock<IRepository<Developer>> mockDeveloperRepo;
        [SetUp]
        public void Init()
        {
            mockDeveloperRepo = new Mock<IRepository<Developer>>();

            var devs = new List<Developer>()
            {
                new Developer(){
                    Id = 1,
                    Name = "Yes"
                },
                new Developer(){
                    Id = 2,
                    Name = "Nope"
                }

            }.AsQueryable();

            mockDeveloperRepo.Setup(m => m.ReadAll()).Returns(devs);
            mockDeveloperRepo.Setup(m => m.Read(1)).Returns(devs.FirstOrDefault(x => x.Id.Equals(1)));
            logic = new DeveloperLogic(mockDeveloperRepo.Object);
        }
        [Test]
        public void DeleteTest()
        {
            var dev = new Developer() { Id = 1, Name = "XD bro" };
            logic.Create(dev);
            logic.Delete(1);
            mockDeveloperRepo.Verify(r => r.Delete(1), Times.Once);
        }
        [Test]
        public void UpdateTest()
        {
            var dev = new Developer() { Id = 1, Name = "XD bro" };
            logic.Create(dev);
            dev.Name = "Lessgoo";
            logic.Update(dev);
            mockDeveloperRepo.Verify(r => r.Update(dev), Times.Once);
        }
        [Test]
        public void CreateInCorrectPublisherTest()
        {
            var dev = new Developer() { Name = "XD" };
            try
            {
                logic.Create(dev);
            }
            catch
            {
                 
            }

            mockDeveloperRepo.Verify(r => r.Create(dev), Times.Never);
        }
        [Test]
        public void GamesOfDevelopersTest()
        {
            Assert.IsEmpty(logic.GamesOfDeveloper(1));
        }
    }
}
