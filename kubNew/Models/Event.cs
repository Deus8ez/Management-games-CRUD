using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kubNew.Models
{
    public class Event
    {
        public Guid ID { get; set; }
        public string Location { get; set; }
        public DateTime DateHeld { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Online { get; set; }
    }
}
