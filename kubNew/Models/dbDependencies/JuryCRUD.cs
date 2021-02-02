using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace kubNew.Models
{
    public class JuryCRUD : IJuryRepository
    {
        public List<Jury> FetchJuries(string dbstring)
        {
            DataTable db = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Судьи", sqlCon);
                sqlDa.Fill(db);
            }

            List<Jury> juries = new List<Jury>() { };

            for (int i = 0; i < db.Rows.Count; i++)
            {
                juries.Add(new Jury()
                {
                    ID = (Guid)db.Rows[i][0],
                    Name = (string)db.Rows[i][1],
                    Surname = (string)db.Rows[i][2],
                });
            }

            return juries;
        }

        public void AddNewJury(Jury participant, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                string query = "INSERT INTO Судьи (Имя, Фамилия) VALUES (@Name,@Surname)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Name", participant.Name);
                cmd.Parameters.AddWithValue("@Surname", participant.Surname);
                cmd.ExecuteNonQuery();
            }
        }

        public Jury GetJury(Guid ID, string dbstring)
        {
            DataTable db = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Судьи WHERE [ID судьи] = @ID", sqlCon);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(db);
            }

            Jury jury = new Jury()
            {
                ID = (Guid)db.Rows[0][0],
                Name = (string)db.Rows[0][1],
                Surname = (string)db.Rows[0][2],
            };

            return jury;
        }

        public void UpdateJury(Jury participant, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                string query = "UPDATE Судьи SET Имя = @Name, Фамилия = @Surname WHERE [ID судьи] = @ID";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@ID", participant.ID);
                cmd.Parameters.AddWithValue("@Name", participant.Name);
                cmd.Parameters.AddWithValue("@Surname", participant.Surname);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteJury(Jury participant, string dbstring)
        {
            using (SqlConnection sqlCon = new SqlConnection(dbstring))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Судьи WHERE [ID судьи] = @ID", sqlCon);
                cmd.Parameters.AddWithValue("@ID", participant.ID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
