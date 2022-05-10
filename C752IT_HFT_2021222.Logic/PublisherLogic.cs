using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Logic
{
    class PublisherLogic : IPublisherLogic
    {
        IRepository<Publisher> repo;
        public void Create(Publisher item)
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

        public Publisher Read(int id)
        {
            var pub = this.repo.Read(id);
            if (pub == null)
            {
                throw new ArgumentException("Game doesn't exist");
            }
            return pub;
        }

        public IQueryable<Publisher> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Publisher item)
        {
            this.repo.Update(item);
        }

        // Non cruds
    }
}
