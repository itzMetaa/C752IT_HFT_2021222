using C752IT_HFT_2021222.Models;
using System.Linq;

namespace C752IT_HFT_2021222.Logic
{
    public interface IPublisherLogic
    {
        void Create(Publisher item);
        void Delete(int id);
        Publisher Read(int id);
        IQueryable<Publisher> ReadAll();
        void Update(Publisher item);
    }
}