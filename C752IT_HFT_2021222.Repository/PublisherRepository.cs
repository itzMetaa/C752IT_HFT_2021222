using C752IT_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Repository
{
    public class PublisherRepository : Repository<Publisher>, IRepository<Publisher>
    {
        public PublisherRepository(GameDbContext ctx) : base(ctx)
        {
        }

        public override Publisher Read(int id)
        {
            return ctx.Publishers.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Publisher item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
