using System.Linq;
using kubNew.Models;
using System.Collections.Generic;
using System;

namespace kubNew.Models {
    public interface IJuryRepository {
        List<Jury> FetchJuries(string dbstring);
        void AddNewJury(Jury participant, string dbstring);
        Jury GetJury(Guid ID, string dbstring);
        void UpdateJury(Jury participant, string dbstring);
        void DeleteJury(Jury participant, string dbstring);
    }
}
