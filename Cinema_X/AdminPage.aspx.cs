using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Cinema_X
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ConnectionString;
            string query = "SELECT MovieID, MovieName, Genre, ReleaseDate, EndDate FROM Movies";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                gvFilms.DataSource = dt;
                gvFilms.DataBind();
            }
        }

        protected void gvFilms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditFilm")
            {
                int movieID = Convert.ToInt32(e.CommandArgument);
                LoadFilmDetails(movieID);
            }
            else if (e.CommandName == "DeleteFilm")
            {
                int movieID = Convert.ToInt32(e.CommandArgument);
                DeleteFilm(movieID);
            }
        }

        private void LoadFilmDetails(int movieID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Movies WHERE MovieID = @MovieID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MovieID", movieID);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    txtMovieTitle.Text = reader["MovieName"].ToString();
                    txtMovieGender.Text = reader["Genre"].ToString();
                    txtMovieDescription.Text = reader["Description"].ToString();
                    txtReleaseDate.Text = reader["ReleaseDate"].ToString();
                    txtEndDate.Text = reader["EndDate"].ToString();
                    txtShowtime1.Text = reader["Showtime1"].ToString();
                    txtShowtime2.Text = reader["Showtime2"].ToString();
                    txtShowtime3.Text = reader["Showtime3"].ToString();
                    txtPosterURL.Text = reader["Poster"].ToString();
                    txtSalon.Text = reader["Salon"].ToString();
                }

                reader.Close();
            }
        }

        private void DeleteFilm(int movieID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ConnectionString;
            string query = "DELETE FROM Movies WHERE MovieID = @MovieID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MovieID", movieID);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            BindGrid();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            string movieGender = txtMovieGender.Text.Trim();
            string movieDescription = txtMovieDescription.Text.Trim();
            string actors = txtActors.Text.Trim();
            string releaseDate = txtReleaseDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            string showtime1 = txtShowtime1.Text.Trim();
            string showtime2 = txtShowtime2.Text.Trim();
            string showtime3 = txtShowtime3.Text.Trim();
            string posterURL = txtPosterURL.Text.Trim();
            string salon = txtSalon.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ConnectionString;
            string query = "INSERT INTO Movies (MovieName, Genre, Description, Actors, ReleaseDate, EndDate, Showtime1, Showtime2, Showtime3, Poster, Salon) " +
                           "VALUES (@MovieName, @Genre, @Description, @Actors, @ReleaseDate, @EndDate, @Showtime1, @Showtime2, @Showtime3, @Poster, @Salon)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MovieName", movieTitle);
                cmd.Parameters.AddWithValue("@Genre", movieGender);
                cmd.Parameters.AddWithValue("@Description", movieDescription);
                cmd.Parameters.AddWithValue("@Actors", actors);
                cmd.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@Showtime1", showtime1);
                cmd.Parameters.AddWithValue("@Showtime2", showtime2);
                cmd.Parameters.AddWithValue("@Showtime3", showtime3);
                cmd.Parameters.AddWithValue("@Poster", posterURL);
                cmd.Parameters.AddWithValue("@Salon", salon);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            BindGrid();
        }

        // View for the List Members button click event
        protected void btnListMembers_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM vw_ShowMembers"; // Use the view you mentioned

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gvMembers.DataSource = dt;
                gvMembers.DataBind();
            }
        }
    }
}
