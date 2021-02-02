using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models;
using kubNew.Models.ViewModels;


namespace kubNew.Models.ViewModels
{
    public class JuryListViewModel
    {
        public IEnumerable<Jury> Juries { get; set; }
        public Jury Jury { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
