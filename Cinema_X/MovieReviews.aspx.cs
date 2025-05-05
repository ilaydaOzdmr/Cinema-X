using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Cinema_X
{
    public partial class MovieReviews : Page
    {
        
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            // List previous comments every time the page loads
            if (!IsPostBack)
            {
                LoadReviews();
            }
        }

        
        private void LoadReviews()
        {
            string query = "SELECT UserName, MovieName, Comment, Rate FROM MovieComment";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                rptReviews.DataSource = reader;
                rptReviews.DataBind();
            }
        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string movieName = txtMovieName.Text.Trim();
            string review = txtReview.Text.Trim();
            int rating = int.Parse(rblRating.SelectedValue); // Seçilen puan

            // Veritabanına yorum ekleme sorgusu
            string query = "INSERT INTO MovieComment (UserName, MovieName, Comment, Rate ) " +
                           "VALUES (@UserName, @MovieName, @Comment, @Rate)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@MovieName", movieName);
                cmd.Parameters.AddWithValue("@Comment", review);
                cmd.Parameters.AddWithValue("@Rate", rating);
                

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // After the comment is submitted, clear the form and reload the comments.
            txtUserName.Text = "";
            txtMovieName.Text = "";
            txtReview.Text = "";
            rblRating.ClearSelection();
            LoadReviews();
        }
    }
}
