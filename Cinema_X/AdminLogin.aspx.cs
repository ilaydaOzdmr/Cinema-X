using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Cinema_X
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                lblError.Visible = false; //If no error
            }
        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            
            string adminID = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Eğer alanlar boşsa hata mesajı göster
            if (string.IsNullOrEmpty(adminID) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter both Admin ID and Password.";
                lblError.Visible = true;
                return;
            }

            
            string connectionString = ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Stored Procedure for Admin Login
                SqlCommand cmd = new SqlCommand("AdminLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("@AdminID", adminID);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    con.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    if (result == 1) // If login is successful
                    {
                        Response.Redirect("AdminPage.aspx");
                    }
                    else
                    {
                        lblError.Text = "Login failed. Check your information.";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "An error occurred: " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }
    }
}
