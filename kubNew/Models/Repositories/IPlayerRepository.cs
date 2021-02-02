using System.Linq;
using kubNew.Models;
using System.Collections.Generic;
using System;

namespace kubNew.Models {
    public interface IPlayerRepository {
        List<Players> FetchPlayers(string dbstring);
        void AddNewPlayer(Players participant, string dbstring);
        Players GetPlayer(Guid ID, string dbstring);
        void UpdatePlayer(Players participant, string dbstring);
        void DeletePlayer(Players participant, string dbstring);
    }
}
