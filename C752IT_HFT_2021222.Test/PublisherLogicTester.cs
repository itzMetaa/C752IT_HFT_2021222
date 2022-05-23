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
    class PublisherLogicTester
    {
        PublisherLogic pubLogic;
        Mock<IRepository<Publisher>> mockPublisherRepo;
        [SetUp]
        public void Init()
        {
            mockPublisherRepo = new Mock<IRepository<Publisher>>();

            var pubs = new List<Publisher>()
            {
                new Publisher(){
                    Id = 1,
                    Name = "Yes"
                },
                new Publisher(){
                    Id = 2,
                    Name = "Nope"
                }

            }.AsQueryable();

            //var devs = new List<Developer>()
            //{
            //    new Developer(){
            //        Id = 1,
            //        Name = "YesDev",
            //        PublisherId = 1
            //    },
            //    new Developer(){
            //        Id = 2,
            //        Name = "NopeDev",
            //        PublisherId = 2
            //    }
            //}.AsQueryable();

            //var games = new List<Game>()
            //{
            //    new Game(){
            //        Id = 1,
            //        Title = "YesGame",
            //        DeveloperId = 1
            //    },
            //    new Game(){
            //        Id = 2,
            //        Title = "NopeGame",
            //        DeveloperId = 2
            //    }
            //}.AsQueryable();

            mockPublisherRepo.Setup(m => m.ReadAll()).Returns(pubs);
            mockPublisherRepo.Setup(m => m.Read(1)).Returns(pubs.FirstOrDefault(x=>x.Id.Equals(1)));
            pubLogic = new PublisherLogic(mockPublisherRepo.Object);
        }
        [Test]
        public void DeleteTest()
        {
            var pub = new Publisher() { Id = 1, Name = "XD bro" };
            pubLogic.Create(pub);
            pubLogic.Delete(1);
            mockPublisherRepo.Verify(r => r.Delete(1), Times.Once);
        }
        [Test]
        public void UpdateTest()
        {
            var pub = new Publisher() { Id = 1, Name = "XD bro" };
            pubLogic.Create(pub);
            pub.Name = "Lessgoo";
            pubLogic.Update(pub);
            mockPublisherRepo.Verify(r => r.Update(pub), Times.Once);
        }
        [Test]
        public void CreateInCorrectPublisherTest()
        {
            var pub = new Publisher() { Name = "XD" };
            try
            {
                pubLogic.Create(pub);
            }
            catch
            {
                 
            }

            mockPublisherRepo.Verify(r => r.Create(pub), Times.Never);
        }
        [Test]
        public void GamesOfPublisherTest() 
        {
            Assert.IsEmpty(pubLogic.GamesOfPublisher(1));
        }
    }
}
