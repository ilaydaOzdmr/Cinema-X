<%@ Page Title="Ticket and Promotion Selection" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TicketAndBuy.aspx.cs" Inherits="Cinema_X.TicketAndBuy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/TicketBuyStyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="seat-selection-container">
        <h2>Select Seats, Tickets, and Promotions</h2>
        <div class="info">
    <p><asp:Label ID="studentTicketLabel" runat="server" Text="** Student Ticket: TL"></asp:Label></p>
    <p><asp:Label ID="fullTicketLabel" runat="server" Text="** Full Ticket: TL"></asp:Label></p>
    <p><asp:Label ID="popcornSmallLabel" runat="server" Text="Popcorn-Small: TL"></asp:Label></p>
    <p><asp:Label ID="popcornMediumLabel" runat="server" Text="Popcorn-Medium: TL"></asp:Label></p>
    <p><asp:Label ID="popcornLargeLabel" runat="server" Text="Popcorn-Large: TL"></asp:Label></p>
</div>



        <!-- Legend -->
        <div class="legend">
            <div class="legend-item">
                <div class="legend-box available"></div>
                <span>Empty Seat</span>
            </div>
            <div class="legend-item">
                <div class="legend-box selected"></div>
                <span>Selected Seat</span>
            </div>
            <div class="legend-item">
                <div class="legend-box taken"></div>
                <span>Taken Seat</span>
            </div>
        </div>

        <!-- Seats Grid -->
        <h4>Please choose a seat/seats</h4>
        <div class="seats-grid">
            <% for (int i = 1; i <= 50; i++) { %>
                <div class="seat" id="seat_<%= i %>" onclick="selectSeat('<%= i %>')">
                    <%= i %>
                </div>
            <% } %>
        </div>

        <!-- Seat Details Form -->
        <div id="seatDetailsForm">
            <h3>Selected Seats</h3>
            <p id="selectedSeatsLabel">Selected Seats: None</p>
            <div id="seatDetailsContainer"></div>
        </div>

        <div class="action-buttons">
            <asp:Button ID="btnPay" runat="server" CssClass="btn btn-primary" Text="PAY" OnClientClick="proceedToPayment(); return false;" />
        </div>
    </div>

    <div class="back-home">
        <a href="HomePage.aspx" class="btn-back-home">Back to Home</a>
    </div>

    <script>;

        const selectedSeats = [];
        const seatDetails = {};

        function selectSeat(seatNumber) {
            const seatElement = document.getElementById(`seat_${seatNumber}`);
            const seatIndex = selectedSeats.indexOf(seatNumber);

            if (seatIndex === -1) {
                selectedSeats.push(seatNumber);
                seatElement.style.backgroundColor = 'green';
                addSeatDetailForm(seatNumber);
            } else {
                selectedSeats.splice(seatIndex, 1);
                seatElement.style.backgroundColor = '';
                removeSeatDetailForm(seatNumber);
            }
            updateSelectedSeatsLabel();
            saveSelectedSeats(); 
        }

        function updateSelectedSeatsLabel() {
            document.getElementById('selectedSeatsLabel').innerText =
                `Selected Seats: ${selectedSeats.length > 0 ? selectedSeats.join(', ') : 'None'}`;
        }

        function addSeatDetailForm(seatNumber) {
            const container = document.getElementById('seatDetailsContainer');
            const seatForm = document.createElement('div');
            seatForm.setAttribute('id', `seatDetail_${seatNumber}`);
            seatForm.innerHTML = `
                <h4>Seat ${seatNumber}</h4>
                <label for="txtName_${seatNumber}">Name on Ticket:</label>
                <input type="text" id="txtName_${seatNumber}" class="form-control" placeholder="Enter your name" />
                <div>
                    <label class="radio-label">
                        <input type="radio" name="ticketType_${seatNumber}" value="Student" /> Student Ticket
                    </label>
 <label class="radio-label">
                        <input type="radio" name="ticketType_${seatNumber}" value="Full" /> Full Ticket
                    </label>
                </div>
                <div>
                    <label for="promotionSelect_${seatNumber}">Select Promotion:</label>
                    <select id="promotionSelect_${seatNumber}" class="form-control">
                        <option value="No Promotion">No Promotion</option>
                        <option value="Popcorn-Small">Popcorn-Small</option>
                        <option value="Popcorn-Medium">Popcorn-Medium</option>
                        <option value="Popcorn-Large">Popcorn-Large</option>
                    </select>
                </div>
            `;
            container.appendChild(seatForm);
        }

        function removeSeatDetailForm(seatNumber) {
            const seatForm = document.getElementById(`seatDetail_${seatNumber}`);
            if (seatForm) seatForm.remove();
        }

   
        function saveSelectedSeats() {
            const selectedSeatsString = selectedSeats.join(','); 
            localStorage.setItem('selectedSeats', selectedSeatsString);
            sessionStorage.setItem('selectedSeats', selectedSeatsString);
        }


        function saveReservationData() {
            const reservationData = selectedSeats.map(seatNumber => {
                const name = document.getElementById(`txtName_${seatNumber}`).value;
                const ticketType = document.querySelector(`input[name="ticketType_${seatNumber}"]:checked`)?.value || '';
                const promotion = document.getElementById(`promotionSelect_${seatNumber}`).value;

                return {
                    seatNumber,
                    name,
                    ticketType,
                    promotion
                };
            });

            sessionStorage.setItem('seatDetails', JSON.stringify(reservationData));
        }

        // Proceed to payment page
        function proceedToPayment() {
            saveReservationData();
            window.location.href = "PaymentPage.aspx";
        }
        // Mark seat numbers received from Backend as "taken"
        function setTakenSeats(takenSeatsString) {
            const takenSeats = takenSeatsString.split(',').map(Number); 

            takenSeats.forEach(seatNumber => {
                const seatElement = document.getElementById(`seat_${seatNumber}`);
                if (seatElement) {
                    seatElement.classList.add('taken'); 
                    seatElement.onclick = null; // Taken seats become unclickable
                }
            });
        }

        window.onload = function () {
            const savedSeats = JSON.parse(sessionStorage.getItem('selectedSeats'));
            if (savedSeats) {
                savedSeats.forEach(seatNumber => {
                    selectSeat(seatNumber);
                });
            }
        };
    </script>

</asp:Content>