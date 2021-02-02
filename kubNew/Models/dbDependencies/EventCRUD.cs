using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using kubNew.Models.ViewModels;

namespace kubNew.Models
{
    public class EventCRUD : IEventRepository
    {
        public List<Event> FetchEvents(string dbstring)
        {
            DataTable db = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Турниры", sqlCon);
                sqlDa.Fill(db);
            }

            List<Event> events = new List<Event>() { };

            for (int i = 0; i < db.Rows.Count; i++)
            {
                events.Add(new Event()
                {
                    ID = (Guid)db.Rows[i][0],
                    Location = (string)db.Rows[i][1],
                    DateHeld = (DateTime)db.Rows[i][2],
                    Type = (string)db.Rows[i][3],
                    Name = (string)db.Rows[i][4],
                    Online = Convert.IsDBNull(db.Rows[i][5]) ? false : (bool)db.Rows[i][5],
                });
            }

            return events;
        }

        public void AddNewEvent(Event anEvent, string dbstring, List<PlayerCartLine> players, List<JuryCartLine> juries)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                string query = "INSERT INTO Турниры ([Место проведения], [Дата проведения], Тип, [Название турнира], [Онлайн]) OUTPUT INSERTED.[ID турнира] VALUES (@Location,@DateHeld,@Type,@Name,@Online)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Location", anEvent.Location);
                cmd.Parameters.AddWithValue("@DateHeld", anEvent.DateHeld);
                cmd.Parameters.AddWithValue("@Type", anEvent.Type);
                cmd.Parameters.AddWithValue("@Name", anEvent.Name);
                cmd.Parameters.AddWithValue("@Online", anEvent.Online);
                sqlCon.Open();
                Guid EventID = (Guid)cmd.ExecuteScalar();
                //cmd.ExecuteNonQuery();

                for (int i = 0; i < juries.Count(); i++)
                {
                    for (int j = 0; j < players.Count(); j++)
                    {
                        query = "INSERT INTO [Участники в турнирах] VALUES (@EventID, @JuryID, @PlayerID)";
                        cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@EventID", EventID);
                        cmd.Parameters.AddWithValue("@JuryID", juries[i].Jury.ID);
                        cmd.Parameters.AddWithValue("@PlayerID", players[j].Player.ID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public Event GetEvent(Guid ID, string dbstring)
        {
            DataTable db = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Турниры WHERE [ID турнира] = @ID", sqlCon);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(db);
            }

            Event anEvent = new Event()
            {
                ID = (Guid)db.Rows[0][0],
                Location = (string)db.Rows[0][1],
                DateHeld = (DateTime)db.Rows[0][2],
                Type = db.Rows[0][3].ToString(),
                Name = (string)db.Rows[0][4],
                Online = (bool)db.Rows[0][5]
            };

            return anEvent;
        }

        public void UpdateEvent(Event newEvent, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                string query = "UPDATE Турниры SET [Место проведения] = @Location, [Дата проведения] = @DateHeld, Тип = @Type, [Название турнира] = @Name, [Онлайн] = @Online WHERE [ID Турнира] = @ID";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@ID", newEvent.ID);
                cmd.Parameters.AddWithValue("@Location", newEvent.Location);
                cmd.Parameters.AddWithValue("@DateHeld", newEvent.DateHeld);
                cmd.Parameters.AddWithValue("@Type", newEvent.Type);
                cmd.Parameters.AddWithValue("@Name", newEvent.Name);
                cmd.Parameters.AddWithValue("@Online", newEvent.Online);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEvent(Event newEvent, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Турниры WHERE [ID турнира] = @ID", sqlCon);
                cmd.Parameters.AddWithValue("@ID", newEvent.ID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
