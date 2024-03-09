using System.Data;
using System.Data.SqlClient;

namespace Demo.Model
{

    public class DAL
    {
        public List<Users> GetUsers(IConfiguration configuration)
        {
            List<Users> listUsers = new List<Users>();
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM StudentData", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Users user = new Users();
                        user.ID = Convert.ToString(dt.Rows[i]["Id"]);
                        user.Student_name = Convert.ToString(dt.Rows[i]["Student_name"]);
                        user.Student_uni = Convert.ToString(dt.Rows[i]["Student_uni"]);
                        user.Uni_Id = Convert.ToString(dt.Rows[i]["Uni_Id"]);
                        listUsers.Add(user);
                    }
                }
            }

            return listUsers;
        }
        public int AddUser(Users user, IConfiguration configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string query = "INSERT INTO StudentData (ID, Student_name, Student_uni, Uni_Id) VALUES (@ID, @Student_name, @Student_uni, @Uni_Id)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", user.ID);
                cmd.Parameters.AddWithValue("@Student_name", user.Student_name);
                cmd.Parameters.AddWithValue("@Student_uni", user.Student_uni);
                cmd.Parameters.AddWithValue("@Uni_Id", user.Uni_Id);

                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }

            return i;
        }

        public Users GetUser(int ID, IConfiguration configuration)
        {
            Users user = new Users();

            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM StudentData WHERE ID = '" + ID + "' ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    user.ID = Convert.ToString(dt.Rows[0]["ID"]);
                    user.Student_name = Convert.ToString(dt.Rows[0]["Student_name"]);
                    user.Student_uni = Convert.ToString(dt.Rows[0]["Student_uni"]);
                    user.Uni_Id = Convert.ToString(dt.Rows[0]["Uni_Id"]);
                }
            }

            return user;
        }

        public int UpdateUser(Users user, IConfiguration configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string query = "UPDATE StudentData SET Student_name = '" + user.Student_name + "', Student_uni = '" + user.Student_uni + "', Uni_Id = '" + user.Uni_Id + "' WHERE ID = '" + user.ID + "' ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Student_name", user.Student_name);
                cmd.Parameters.AddWithValue("@Student_uni", user.Student_uni);
                cmd.Parameters.AddWithValue("@Uni_Id", user.Uni_Id);

                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }

            return i;
        }

        public int DeleteUser(string id, IConfiguration configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string query = "DELETE FROM StudentData  WHERE ID = '" + id + "' ";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }

            return i;
        }

    }
}
