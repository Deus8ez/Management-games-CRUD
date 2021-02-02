using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models;

namespace kubNew.Models.ViewModels
{
    public class MasterListViewModel
    {
        public Event Event { get; set; }
        public IEnumerable<Players> Players { get; set; }
        public IEnumerable<Jury> Juries { get; set; }
        public PagingInfo PagingInfoPlayers { get; set; }
        public PagingInfo PagingInfoJuries { get; set; }

    }
}
