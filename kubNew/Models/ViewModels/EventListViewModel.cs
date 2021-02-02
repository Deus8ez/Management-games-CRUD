using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models;

namespace kubNew.Models.ViewModels
{
    public class EventListViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
