using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Models
{
    public class GameInfo
    {
        public Game Game { get; set; }
        public int TotalRevenue { get; set; }

        public override bool Equals(object obj)
        {
            GameInfo b = obj as GameInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Game.Equals(b.Game) && this.TotalRevenue.Equals(b.TotalRevenue);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Game, this.TotalRevenue);
        }
    }
}
