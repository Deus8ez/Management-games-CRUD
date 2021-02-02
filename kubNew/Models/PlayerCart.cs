using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kubNew.Models
{
    public class PlayerCart
    {
        private List<PlayerCartLine> lineCollection = new List<PlayerCartLine>();

        public virtual void AddPlayer(Players player)
        {
            //PlayerCartLine line = lineCollection
            //    .Where(p => p.Player.ID == player.ID)
            //    .FirstOrDefault();

            //if (line == null)
            //{
                lineCollection.Add(new PlayerCartLine
                {
                    Player = player
                });
            //} 
        }

        public virtual void RemoveLine(Players player)
        {
            lineCollection.RemoveAll(l => l.Player.ID == player.ID);
        }

        public virtual void Clear()
        {
            lineCollection.Clear();
        }

        public virtual List<PlayerCartLine> Lines => lineCollection;
    }

    public class PlayerCartLine
    {
        public int PlayerCartLineID { get; set; }
        public Players Player { get; set; }
    }
}
