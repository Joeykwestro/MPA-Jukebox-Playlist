using System.Data;
using System.Data.SqlClient;

namespace MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models
{
    public class SqlFunctions
    {
        public static DataTable executeSqlGetDataTable(string query, string db)
        {
            DataTable dt = new DataTable();

            string conFms = "Data Source = LAPTOP-HGAMMLG8; Initial Catalog = " + db + "; Integrated Security = True";

            SqlConnection conFMs = new SqlConnection(conFms);
            SqlCommand commandFms = new SqlCommand(query, conFMs);
            SqlDataAdapter da = new SqlDataAdapter(commandFms);
            da.Fill(dt);

            return dt;
        }

        public static string executeSql(string query, string db, string action)
        {
            string returnValue = "";

            string conFms = "Data Source = LAPTOP-HGAMMLG8; Initial Catalog = " + db + "; Integrated Security = True";

            SqlConnection conFMs = new SqlConnection(conFms);
            SqlCommand commandFms = new SqlCommand(query, conFMs);
            conFMs.Open();

            if (action == "SELECT")
            {
                returnValue = commandFms.ExecuteScalar().ToString();

            }
            else
            {
                commandFms.ExecuteNonQuery();

            }
            conFMs.Close();


            return returnValue;
        }
    }
}
