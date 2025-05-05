using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;

namespace Cinema_X
{
    public partial class PaymentPage : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string movieName = hfMovieName.Value;
                string sessionTime = hfSessionTime.Value;
                string seatInfo = hfSeatInfo.Value;

                lblMovieName.Text = movieName;
                lblSessionTime.Text = sessionTime;
                lblSeatInfo.Text = seatInfo;
            }
        }


        protected void btnPay_Click(object sender, EventArgs e)
        {
            string cardName = txtCardName.Text.Trim();
            string cardNumber = txtCardNumber.Text.Trim();
            string cvv = txtCVV.Text.Trim();
            string movieName = hfMovieName.Value;
            string sessionTime = hfSessionTime.Value;
            string seatInfo = hfSeatInfo.Value;

            string[] seats = seatInfo.Split(',');
            int[] seatNumbers = seats
                .Select(seat =>
                {
                    int.TryParse(seat.Trim(), out int seatNumberInt);
                    return seatNumberInt;
                })
                .ToArray();

            if (string.IsNullOrEmpty(cardName) || string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cvv))
            {
                lblError.Text = "Card details cannot be empty.";
                lblError.Visible = true;
                return;
            }

            //Function Calling for check card infos
            string connectionString = "Data Source=DESKTOP-4KKH5FH;Initial Catalog=Cinema_XDB;Integrated Security=True;";
            string query = "SELECT dbo.Check_Card_Infos(@CardName, @CardNumber, @CVV)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CardName", cardName);
                        command.Parameters.AddWithValue("@CardNumber", cardNumber);
                        command.Parameters.AddWithValue("@CVV", cvv);

                        connection.Open();
                        int isValid = Convert.ToInt32(command.ExecuteScalar());

                        if (isValid == 1) // Kart bilgileri geçerliyse
                        {
                            foreach (int seatNumberInt in seatNumbers)
                            {
                                string insertQuery = "INSERT INTO Reservations (SeatNumber, MovieName, SessionTime) VALUES (@SeatNumber, @MovieName, @SessionTime)";

                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@SeatNumber", seatNumberInt);
                                    insertCommand.Parameters.AddWithValue("@MovieName", movieName);
                                    insertCommand.Parameters.AddWithValue("@SessionTime", sessionTime);

                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            Response.Redirect("ShowTicket.aspx");
                        }
                        else
                        {
                            lblError.Text = "Invalid card info!";
                            lblError.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "There is an error. Try again later.";
                lblError.Visible = true;
                Console.WriteLine(ex.Message);
            }
        }

    }
}