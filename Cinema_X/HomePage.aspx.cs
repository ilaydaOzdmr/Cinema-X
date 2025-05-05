using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Cinema_X
{
    public partial class HomePage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string searchTerm = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    LoadSearchedMovies(searchTerm);
                    txtSelectedDate.Text = DateTime.Today.ToString("yyyy-MM-dd"); 
                }
                else
                {
                    DateTime today = DateTime.Today;
                    LoadMovies(today);
                    txtSelectedDate.Text = today.ToString("yyyy-MM-dd");
                }
            }
        }
        private void LoadSearchedMovies(string searchTerm)
        { //Stored procedure for Movie Searching
            string connectionString = "Data Source=DESKTOP-4KKH5FH;Initial Catalog=Cinema_XDB;Integrated Security=True;";
            string query = @"EXEC SearchMovies @SearchTerm";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                movieListContainer.InnerHtml = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string movieName = reader["MovieName"].ToString();
                        string description = reader["Description"].ToString();
                        string genre = reader["Genre"].ToString();
                        string actors = reader["Actors"].ToString();
                        string salon = reader["Salon"].ToString();
                        string poster = reader["Poster"].ToString();
                        string showtime1 = reader["Showtime1"].ToString();
                        string showtime2 = reader["Showtime2"].ToString();
                        string showtime3 = reader["Showtime3"].ToString();
                        int movieId = Convert.ToInt32(reader["MovieID"]);

                        AddMovieToPage(movieName, description, genre, actors, salon, poster, showtime1, showtime2, showtime3, movieId);
                    }
                }
                else
                {
                    movieListContainer.InnerHtml = "<p>No movies matched your search criteria.</p>";
                }

                reader.Close();
                con.Close();
            }
        }


        protected void btnListMovies_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtSelectedDate.Text, out DateTime selectedDate))
            {
                LoadMovies(selectedDate);
            }
            else
            {
                movieListContainer.InnerHtml = "<p>Please select a valid date.</p>";
            }
        }


        private void LoadMovies(DateTime filterDate)
        {
            string connectionString = "Data Source=DESKTOP-4KKH5FH;Initial Catalog=Cinema_XDB;Integrated Security=True;";
            string query = @"SELECT * FROM Movies WHERE @FilterDate BETWEEN ReleaseDate AND EndDate";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FilterDate", filterDate);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                movieListContainer.InnerHtml = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string movieName = reader["MovieName"].ToString();
                        string description = reader["Description"].ToString();
                        string genre = reader["Genre"].ToString();
                        string actors = reader["Actors"].ToString();
                        string salon = reader["Salon"].ToString();
                        string poster = reader["Poster"].ToString();
                        string showtime1 = reader["Showtime1"].ToString();
                        string showtime2 = reader["Showtime2"].ToString();
                        string showtime3 = reader["Showtime3"].ToString();
                        int movieId = Convert.ToInt32(reader["MovieID"]);

                        AddMovieToPage(movieName, description, genre, actors, salon, poster, showtime1, showtime2, showtime3, movieId);
                    }
                }
                else
                {
                    movieListContainer.InnerHtml = "<p>No movies available for the selected date.</p>";
                }

                reader.Close();
                con.Close();
            }
        }
        protected void btnShowRemainingDays_Click(object sender, EventArgs e)
        {
            // Call movies with ShowRemainingDays Stored procedure
            string connectionString = "Data Source=DESKTOP-4KKH5FH;Initial Catalog=Cinema_XDB;Integrated Security=True;";
            string query = @"
                           SELECT MovieName, ABS(dbo.ShowRemainingDays(EndDate)) AS RemainingDay
                           FROM Movies 
                           WHERE dbo.ShowRemainingDays(EndDate) < 0
                           ORDER BY RemainingDay";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                remainingDaysResults.InnerHtml = ""; 

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string movieName = reader["MovieName"].ToString();
                        int remainingDays = Convert.ToInt32(reader["RemainingDay"]);

                        
                        remainingDaysResults.InnerHtml += $"<p>{movieName} - Remaining Days: {remainingDays}</p>";
                    }
                }
                else
                {
                    remainingDaysResults.InnerHtml = "<p>No movies found with remaining days less than 0.</p>";
                }

                reader.Close();
                con.Close();
            }
        }


        private void AddMovieToPage(string name, string description, string genre, string actors, string salon, string poster, string showtime1, string showtime2, string showtime3, int movieId)
        {
            string moviePoster = string.IsNullOrEmpty(poster) ? "/path/to/default/image.jpg" : poster;
            string showtimeButtons = "";

            
            if (!string.IsNullOrEmpty(showtime1) && showtime1 != "00:00:00")
                showtimeButtons += $"<a href='TicketAndBuy.aspx?movieId={movieId}&showtime={showtime1}' class='btn-showtime' onclick='storeInLocalStorage(\"{name}\", \"{showtime1}\")'>{showtime1}</a>";
            if (!string.IsNullOrEmpty(showtime2) && showtime2 != "00:00:00")
                showtimeButtons += $"<a href='TicketAndBuy.aspx?movieId={movieId}&showtime={showtime2}' class='btn-showtime' onclick='storeInLocalStorage(\"{name}\", \"{showtime2}\")'>{showtime2}</a>";
            if (!string.IsNullOrEmpty(showtime3) && showtime3 != "00:00:00")
                showtimeButtons += $"<a href='TicketAndBuy.aspx?movieId={movieId}&showtime={showtime3}' class='btn-showtime' onclick='storeInLocalStorage(\"{name}\", \"{showtime3}\")'>{showtime3}</a>";

            // GetAverageRating function calling
            string averageRating = GetAverageRating(name);

            string movieHtml = $@"
        <div class='movie-item'>
            <img src='{moviePoster}' alt='Movie Poster' />
            <div class='movie-details'>
                <h5>{name}</h5>
                <p>{description}</p>
                <p>Genre: {genre}</p>
                <p>Actors: {actors}</p>
                <p><b>Salon: {salon}</b></p>
                <p><b style='color:green;'>Average Rating: {averageRating}</b></p> <!-- Puan kısmı burada -->
                <div class='showtimes'>{showtimeButtons}</div>
            </div>
        </div>";

            movieListContainer.InnerHtml += movieHtml;
        }

        private string GetAverageRating(string movieName)
        { 
            string connectionString = "Data Source=DESKTOP-4KKH5FH;Initial Catalog=Cinema_XDB;Integrated Security=True;";
            string query = @"SELECT dbo.GetAverageRating(@MovieName) AS AvgRating";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MovieName", movieName);

                con.Open();
                var result = cmd.ExecuteScalar();
                con.Close();

                return result?.ToString() ?? "N/A"; 
            }
        }

    }
}
