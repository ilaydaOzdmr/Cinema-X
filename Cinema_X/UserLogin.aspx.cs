using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Cinema_X
{
    public partial class UserLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // If fields are empty
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Email and Password cannot be empty.";
                lblError.Visible = true;
                return;
            }

            string connectionString = "Data Source=DESKTOP-4KKH5FH;Initial Catalog=Cinema_XDB;Integrated Security=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Stored Procedure for user login
                    using (SqlCommand command = new SqlCommand("UserLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        connection.Open();
                        int result = Convert.ToInt32(command.ExecuteScalar());

                        if (result == 1)
                        {
                            // If the login is successful
                            Response.Redirect("UserHomepage.aspx");
                        }
                        else
                        {
                           
                            lblError.Text = "Invalid Email or Password.";
                            lblError.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
           
                lblError.Text = "An error occurred. Please try again later.";
                lblError.Visible = true;

             
                Console.WriteLine(ex.Message);
            }
        }
    }
}
