using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Cinema_X
{
    public partial class TicketAndBuy : Page
    {
       
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Cinema_XDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTicketPrices();
                UpdateTakenSeats(); 
            }
        }

       
        private void LoadTicketPrices()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PricesForTicket";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt); 

                foreach (DataRow row in dt.Rows)
                {
                    string product = row["Product"].ToString();
                    decimal price = Convert.ToDecimal(row["Price"]);

                    // Update the appropriate tag based on product and price information
                    if (product == "FullTicket")
                    {
                        fullTicketLabel.Text = $"** Full Ticket: {price} TL";
                    }
                    else if (product == "StudentTicket")
                    {
                        studentTicketLabel.Text = $"** Student Ticket: {price} TL";
                    }
                    else if (product == "PopcornSmall")
                    {
                        popcornSmallLabel.Text = $"Popcorn-Small: {price} TL";
                    }
                    else if (product == "PopcornMedium")
                    {
                        popcornMediumLabel.Text = $"Popcorn-Medium: {price} TL";
                    }
                    else if (product == "PopcornLarge")
                    {
                        popcornLargeLabel.Text = $"Popcorn-Large: {price} TL";
                    }
                }
            }
        }
        protected void UpdateTakenSeats()
        {
            List<int> takenSeats = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SeatNumber FROM Reservations"; 
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int seatNumber = Convert.ToInt32(reader["SeatNumber"]);
                        takenSeats.Add(seatNumber);
                    }
                    conn.Close();
                }
            }

            string takenSeatsString = string.Join(",", takenSeats);
            ClientScript.RegisterStartupScript(this.GetType(), "SetTakenSeats",
                $"setTakenSeats('{takenSeatsString}');", true);
        }



        protected void btnPay_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("PaymentPage.aspx");
        }
    }
}
