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
        PublisherLogic logic;
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

            mockPublisherRepo.Setup(m => m.ReadAll()).Returns(pubs);
            logic = new PublisherLogic(mockPublisherRepo.Object);
        }
        [Test]
        public void DeleteTest()
        {
            var pub = new Publisher() { Id = 1, Name = "XD bro" };
            logic.Create(pub);
            logic.Delete(1);
            mockPublisherRepo.Verify(r => r.Delete(1), Times.Once);
        }
        [Test]
        public void UpdateTest()
        {
            var pub = new Publisher() { Id = 1, Name = "XD bro" };
            logic.Create(pub);
            pub.Name = "Lessgoo";
            logic.Update(pub);
            mockPublisherRepo.Verify(r => r.Update(pub), Times.Once);
        }
        [Test]
        public void CreateInCorrectPublisherTest()
        {
            var pub = new Publisher() { Name = "XD" };
            try
            {
                logic.Create(pub);
            }
            catch
            {

            }

            mockPublisherRepo.Verify(r => r.Create(pub), Times.Never);
        }
    }
}
