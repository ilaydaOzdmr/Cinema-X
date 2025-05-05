using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Cinema_X
{
    public partial class HomePage : Page
    {
        protected void Page_Loadu(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string searchTermu = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(searchTermu))
                {
                    LoadSearchedMoviesu(searchTermu);
                    txtSelectedDate.Text = DateTime.Today.ToString("yyyy-MM-dd"); 
                }
                else
                {
                    DateTime today = DateTime.Today;
                    LoadMoviesu(today);
                    txtSelectedDate.Text = today.ToString("yyyy-MM-dd");
                }
            }
        }

        private void LoadSearchedMoviesu(string searchTerm)
        {
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

        protected void btnListMoviesu_Click(object sender, EventArgs e)
        {
            DateTime selectedDate;

            
            if (DateTime.TryParse(txtSelectedDate.Text, out selectedDate))
            {
                // Stored Procedure 
                LoadMoviesu(selectedDate);
            }
            else
            {
               
                movieListContainer.InnerHtml = "<p>Please select a valid date.</p>";
            }
        }

        //List Movies by stored procedure
        private void LoadMoviesu(DateTime filterDate)
        {
            string connectionString = "Data Source=DESKTOP-4KKH5FH;Initial Catalog=Cinema_XDB;Integrated Security=True;";
            string query = "EXEC GetMoviesByDate @FilterDate";

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

                 
                        AddMovieToPageu(movieName, description, genre, actors, salon, poster, showtime1, showtime2, showtime3);
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


   
        private void AddMovieToPageu(string name, string description, string genre, string actors, string salon, string poster, string showtime1, string showtime2, string showtime3)
        {
           
            string movieTitle = string.IsNullOrEmpty(name) ? "-" : name;
         
            string movieDescription = string.IsNullOrEmpty(description) ? "-" : description;
           
            string movieActors = string.IsNullOrEmpty(actors) ? "-" : actors;
            string movieSalon = string.IsNullOrEmpty(salon) ? "-" : salon;
            string moviePoster = string.IsNullOrEmpty(poster) ? "/path/to/default/image.jpg" : poster;

            string showtimeButtons = "";
            if (!string.IsNullOrEmpty(showtime1) && showtime1 != "00:00:00")
                showtimeButtons += $"<a href='TicketAndBuy.aspx' class='btn-showtime'>{showtime1}</a>";
            if (!string.IsNullOrEmpty(showtime2) && showtime2 != "00:00:00")
                showtimeButtons += $"<a href='TicketAndBuy.aspx' class='btn-showtime'>{showtime2}</a>";
            if (!string.IsNullOrEmpty(showtime3) && showtime3 != "00:00:00")
                showtimeButtons += $"<a href='TicketAndBuy.aspx' class='btn-showtime'>{showtime3}</a>";

            string movieHtml = $@"
                <div class='movie-item'>
                    <img src='{moviePoster}' alt='Movie Poster' />
                    <div class='movie-details'>
                        <h5>{movieTitle}</h5>
                        <p>{movieDescription}</p>
                        <p>Genre: {genre}</p>
                        <p>Actors: {movieActors}</p>
                        <p><b>Salon: {movieSalon}</b></p>
                        <div class='showtimes'>
                            {showtimeButtons}
                        </div>
                    </div>
                </div>";

            movieListContainer.InnerHtml += movieHtml;
        }
    }
}
