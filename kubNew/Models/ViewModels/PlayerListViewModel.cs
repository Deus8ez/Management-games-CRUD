using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models;

namespace kubNew.Models.ViewModels
{
    public class PlayerListViewModel
    {
        public IEnumerable<Players> Players { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
