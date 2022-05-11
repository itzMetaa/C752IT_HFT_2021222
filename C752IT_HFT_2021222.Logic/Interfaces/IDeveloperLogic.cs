using C752IT_HFT_2021222.Models;
using System.Linq;

namespace C752IT_HFT_2021222.Logic
{
    public interface IDeveloperLogic
    {
        void Create(Developer item);
        void Delete(int id);
        Developer Read(int id);
        IQueryable<Developer> ReadAll();
        void Update(Developer item);
    }
}