<%@ Page Title="My Tickets" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ShowTicket.aspx.cs" Inherits="Cinema_X.ShowTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/ShowTicketStyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ticket-container">
        <div class="ticket-header">
            <h2>THE FOLLOWING TICKET(S) HAVE BEEN PAID FOR</h2>
        </div>


        <div class="ticket-content">
            <div class="ticket-image">
                <img src="imgs/Ticket-x.png" alt="Ticket Image" /><br class="Apple-interchange-newline">
            </div>
            <div class="ticket-info">
                <h3>Ticket Information</h3>
                <ul>
                    <li><strong>Name:</strong> <span id="ticketName">-</span></li>
                    <li><strong>Movie Name:</strong> <span id="movieName">-</span></li>
                    <li><strong>Session:</strong> <span id="session">-</span></li>
                    <li><strong>Seat:</strong> <span id="seat">-</span></li>
                    <li><strong>Promotions:</strong> <span id="promotions">-</span></li>
                </ul>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        window.onload = function () {
            const reservationData = JSON.parse(sessionStorage.getItem('seatDetails')) || [];
            const seatInfoElement = document.getElementById('seat');
            const promotionInfoElement = document.getElementById('promotions');
            const nameInfoElement = document.getElementById('ticketName');
            const movieName = localStorage.getItem('selectedMovie') || 'Not Available';
            const sessionTime = localStorage.getItem('selectedShowtime') || 'Not Available';
            const seatInfo = [];
            let seatText = '';
            let promotionsText = '';
            let namesText = '';

         
            reservationData.forEach((ticket, index) => {
                seatText += ticket.seatNumber + (index < reservationData.length - 1 ? ', ' : '');
                if (ticket.promotion && ticket.promotion !== 'No Promotion') {
                    promotionsText += ticket.promotion + (index < reservationData.length - 1 ? ', ' : '');
                }
                namesText += "Seat " + ticket.seatNumber + ": " + ticket.name + (index < reservationData.length - 1 ? ', ' : '');
            });

            seatText = seatText || 'No seats selected';
            promotionsText = promotionsText || 'No promotions added';
            namesText = namesText || 'No names entered';

           
            seatInfoElement.textContent = seatText;
            promotionInfoElement.textContent = promotionsText;
            nameInfoElement.textContent = namesText;

            
            document.getElementById('movieName').textContent = movieName;
            document.getElementById('session').textContent = sessionTime;


            localStorage.setItem('selectedSeats', seatText);
        };
    </script>
</asp:Content>
