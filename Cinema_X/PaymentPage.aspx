<%@ Page Title="Payment Page" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="Cinema_X.PaymentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/PaymenStyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
        <div class="main-container">
            <div class="ticket-info-container">
                <img src="imgs/Ticket-x.png" alt="Ticket Icon" />
                <div class="ticket-info">
                    <h3>Your Ticket Information</h3>
                    <ul>
                        <li><strong>Movie Name:</strong> <asp:Label ID="lblMovieName" runat="server"></asp:Label></li>
                        <li><strong>Session Time:</strong> <asp:Label ID="lblSessionTime" runat="server"></asp:Label></li>
                        <li><strong>Selected Seats:</strong> <asp:Label ID="lblSeatInfo" runat="server"></asp:Label></li>
                        <li><strong>Names on Tickets:</strong> <asp:Label ID="lblNameInfo" runat="server"></asp:Label></li>
                        <li><strong>Added Promotions:</strong> <asp:Label ID="lblPromotionInfo" runat="server"></asp:Label></li>
                        <li><strong>Total Price:</strong> <asp:Label ID="lblTotalPrice" runat="server"></asp:Label></li>
                    </ul>
                </div>
            </div>

            <div class="payment-container">
                <h2>Make Payment</h2>
                <asp:TextBox ID="txtCardName" CssClass="form-control" runat="server" Placeholder="Card Name"></asp:TextBox>
                <asp:TextBox ID="txtCardNumber" CssClass="form-control" runat="server" Placeholder="Card Number" MaxLength="16"></asp:TextBox>
                <asp:TextBox ID="txtCVV" CssClass="form-control" runat="server" Placeholder="CVV" MaxLength="3"></asp:TextBox>
                <asp:Button ID="btnPay" CssClass="btn" runat="server" Text="Pay" OnClick="btnPay_Click" />
                <!-- Error message display -->
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Hidden Fields -->
    <asp:HiddenField ID="hfMovieName" runat="server" />
    <asp:HiddenField ID="hfSessionTime" runat="server" />
    <asp:HiddenField ID="hfSeatInfo" runat="server" />
    <asp:HiddenField ID="hfTotalPrice" runat="server" />

    <script>
        window.onload = function () {
            const hfMovieName = document.getElementById('<%= hfMovieName.ClientID %>');
            const hfSessionTime = document.getElementById('<%= hfSessionTime.ClientID %>');
            const hfSeatInfo = document.getElementById('<%= hfSeatInfo.ClientID %>');
            const hfTotalPrice = document.getElementById('<%= hfTotalPrice.ClientID %>');

            const selectedMovie = localStorage.getItem('selectedMovie') || 'Not Available';
            const selectedShowtime = localStorage.getItem('selectedShowtime') || 'Not Available';
            const selectedSeats = localStorage.getItem('selectedSeats') || '';

            hfMovieName.value = selectedMovie;
            hfSessionTime.value = selectedShowtime;
            hfSeatInfo.value = selectedSeats;

            
            const movieNameElement = document.getElementById('<%= lblMovieName.ClientID %>');
            const sessionTimeElement = document.getElementById('<%= lblSessionTime.ClientID %>');
            const seatInfoElement = document.getElementById('<%= lblSeatInfo.ClientID %>');
            const promotionInfoElement = document.getElementById('<%= lblPromotionInfo.ClientID %>');
            const totalPriceElement = document.getElementById('<%= lblTotalPrice.ClientID %>');
            const nameInfoElement = document.getElementById('<%= lblNameInfo.ClientID %>');

            // Retrieve data from LocalStorage and SessionStorage
            const reservationData = JSON.parse(sessionStorage.getItem('seatDetails')) || [];
            let seatsText = '';
            let promotionsText = '';
            let namesText = '';
            let totalPrice = 0;

           
            movieNameElement.textContent = selectedMovie;
            sessionTimeElement.textContent = selectedShowtime;
            seatInfoElement.textContent = selectedSeats;

            // Process reservation data
            reservationData.forEach((ticket, index) => {
                seatsText += ticket.seatNumber + (index < reservationData.length - 1 ? ', ' : '');
                if (ticket.promotion && ticket.promotion !== 'No Promotion') {
                    promotionsText += ticket.promotion + (index < reservationData.length - 1 ? ', ' : '');
                }
                namesText += "Seat " + ticket.seatNumber + ": " + ticket.name + (index < reservationData.length - 1 ? ', ' : '');
                totalPrice += ticket.ticketType === "Student" ? 90 : 110;
                if (ticket.promotion === "Popcorn-Small") {
                    totalPrice += 30;
                } else if (ticket.promotion === "Popcorn-Medium") {
                    totalPrice += 40;
                } else if (ticket.promotion === "Popcorn-Large") {
                    totalPrice += 55;
                }
            });

            seatInfoElement.textContent = seatsText || 'No seats selected';
            promotionInfoElement.textContent = promotionsText || 'No promotions added';
            totalPriceElement.textContent = totalPrice + ' TL';
            nameInfoElement.textContent = namesText || 'No names entered';

   
            hfTotalPrice.value = totalPrice;

         
            sessionStorage.setItem('movieName', selectedMovie);
            sessionStorage.setItem('sessionTime', selectedShowtime);
        };
    </script>
</asp:Content>
