using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Logic
{
    public class DeveloperLogic : IDeveloperLogic
    {
        IRepository<Developer> repo;

        public DeveloperLogic(IRepository<Developer> repo)
        {
            this.repo = repo;
        }

        public void Create(Developer item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("Name too short");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Developer Read(int id)
        {
            var pub = this.repo.Read(id);
            if (pub == null)
            {
                throw new ArgumentException("Game doesn't exist");
            }
            return pub;
        }

        public IQueryable<Developer> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Developer item)
        {
            this.repo.Update(item);
        }

        // Non cruds
    }
}
