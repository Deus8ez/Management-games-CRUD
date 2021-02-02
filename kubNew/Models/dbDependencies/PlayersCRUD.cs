using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace kubNew.Models
{
    public class PlayersCRUD : IPlayerRepository
    {
        public List<Players> FetchPlayers(string dbstring)
        {
            DataTable db = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Игроки", sqlCon);
                sqlDa.Fill(db);
            }

            List<Players> players = new List<Players>() { };

            for (int i = 0; i < db.Rows.Count; i++)
            {
                players.Add(new Players()
                {
                    ID = (Guid)db.Rows[i][0],
                    Name = (string)db.Rows[i][1],
                    Surname = (string)db.Rows[i][2],
                    Patronym = Convert.IsDBNull(db.Rows[i][3]) ? string.Empty : db.Rows[i][3].ToString(),
                    DateOfBirth = (DateTime)db.Rows[i][4],
                    ClassicRank = Convert.IsDBNull(db.Rows[i][5]) ? string.Empty : db.Rows[i][5].ToString(),
                    BlitzRank = Convert.IsDBNull(db.Rows[i][6]) ? string.Empty : db.Rows[i][6].ToString()
                });
            }

            return players;
        }

        public void AddNewPlayer(Players participant, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                string query = "INSERT INTO Игроки (Имя, Фамилия, Отчество, [Дата рождения], [Разряд в классической борьбе], [Разряд в быстрой управленческой борьбе]) VALUES (@Name,@Surname,@Patronym,@DateOfBirth,@ClassicRank,@BlitzRank)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Name", participant.Name);
                cmd.Parameters.AddWithValue("@Surname", participant.Surname);
                cmd.Parameters.AddWithValue("@Patronym", string.IsNullOrEmpty(participant.Patronym) ? (object)DBNull.Value : participant.Patronym);
                cmd.Parameters.AddWithValue("@DateOfBirth", participant.DateOfBirth);
                cmd.Parameters.AddWithValue("@ClassicRank", string.IsNullOrEmpty(participant.ClassicRank) ? (object)DBNull.Value : participant.ClassicRank);
                cmd.Parameters.AddWithValue("@BlitzRank", string.IsNullOrEmpty(participant.BlitzRank) ? (object)DBNull.Value : participant.BlitzRank);
                cmd.ExecuteNonQuery();
            }
        }

        public Players GetPlayer(Guid ID, string dbstring)
        {
            DataTable db = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Игроки WHERE [ID игрока] = @ID", sqlCon);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(db);
            }

            Players player = new Players()
            {
                ID = (Guid)db.Rows[0][0],
                Name = (string)db.Rows[0][1],
                Surname = (string)db.Rows[0][2],
                Patronym = Convert.IsDBNull(db.Rows[0][3]) ? string.Empty : db.Rows[0][3].ToString(),
                DateOfBirth = (DateTime)db.Rows[0][4],
                ClassicRank = Convert.IsDBNull(db.Rows[0][5]) ? string.Empty : db.Rows[0][5].ToString(),
                BlitzRank = Convert.IsDBNull(db.Rows[0][6]) ? string.Empty : db.Rows[0][6].ToString()
            };

            return player;
        }

        public void UpdatePlayer(Players participant, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                string query = "UPDATE Игроки SET Имя = @Name, Фамилия = @Surname, Отчество = @Patronym, [Дата рождения] = @DateOfBirth, [Разряд в классической борьбе] = @ClassicRank, [Разряд в быстрой управленческой борьбе] = @BlitzRank WHERE [ID игрока] = @ID";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@ID", participant.ID);
                cmd.Parameters.AddWithValue("@Name", participant.Name);
                cmd.Parameters.AddWithValue("@Surname", participant.Surname);
                cmd.Parameters.AddWithValue("@Patronym", string.IsNullOrEmpty(participant.Patronym) ? (object)DBNull.Value : participant.Patronym);
                cmd.Parameters.AddWithValue("@DateOfBirth", participant.DateOfBirth);
                cmd.Parameters.AddWithValue("@ClassicRank", string.IsNullOrEmpty(participant.ClassicRank) ? (object)DBNull.Value : participant.ClassicRank);
                cmd.Parameters.AddWithValue("@BlitzRank", string.IsNullOrEmpty(participant.BlitzRank) ? (object)DBNull.Value : participant.BlitzRank);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePlayer(Players participant, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Игроки WHERE [ID игрока] = @ID", sqlCon);
                cmd.Parameters.AddWithValue("@ID", participant.ID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
