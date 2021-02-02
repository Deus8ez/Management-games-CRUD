using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models;
using kubNew.Models.ViewModels;

namespace kubNew.Models
{
    public interface IEventRepository
    {
        List<Event> FetchEvents(string dbstring);
        void AddNewEvent(Event anEvent, string dbstring, List<PlayerCartLine> players, List<JuryCartLine> juries);
        Event GetEvent(Guid ID, string dbstring);
        void UpdateEvent(Event newEvent, string dbstring);
        void DeleteEvent(Event newEvent, string dbstring);
    }
}
