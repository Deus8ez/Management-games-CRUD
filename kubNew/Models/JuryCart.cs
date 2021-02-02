using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kubNew.Models
{
    public class JuryCart
    {
        private List<JuryCartLine> lineJuryCollection = new List<JuryCartLine>();

        public virtual void AddJury(Jury jury)
        {
            //PlayerCartLine line = lineCollection
            //    .Where(p => p.Player.ID == player.ID)
            //    .FirstOrDefault();

            //if (line == null)
            //{
            lineJuryCollection.Add(new JuryCartLine
                {
                    Jury = jury
                });
            //} 
        }

        public virtual void RemoveLine(Jury jury)
        {
            lineJuryCollection.RemoveAll(l => l.Jury.ID == jury.ID);
        }

        public virtual void Clear()
        {
            lineJuryCollection.Clear();
        }

        public virtual List<JuryCartLine> Lines => lineJuryCollection;
    }

    public class JuryCartLine
    {
        public int JuryCartLineID { get; set; }
        public Jury Jury { get; set; }
    }
}
