using C752IT_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Repository
{
    public class DeveloperRepository : Repository<Developer>, IRepository<Developer>
    {
        public DeveloperRepository(GameDbContext ctx) : base(ctx)
        {
        }

        public override Developer Read(int id)
        {
            return ctx.Developers.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Developer item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
