<%@ Page Title="Cinema-X" Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Cinema_X.HomePage" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/HomePageStyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
        <!-- Left Menu -->
        <div class="left-menu">
            <h3>Choose a Date</h3>
            <div class="date-picker">
                <asp:TextBox ID="txtSelectedDate" runat="server" TextMode="Date" CssClass="date-input"></asp:TextBox>
                <asp:Button ID="btnListMovies" runat="server" Text="List Movies" OnClick="btnListMovies_Click" CssClass="btn-list" />
            </div>
            <a href="MovieReviews.aspx" class="btn-reviews">
                <i class="fas fa-comment-alt"></i> Movie Reviews
            </a>
            <asp:Button ID="btnShowRemainingDays" runat="server" Text="Show Remaining Days" 
                CssClass="btn-show-remaining-days" OnClick="btnShowRemainingDays_Click" />
            <div id="remainingDaysResults" runat="server" class="remaining-days-results">
       
           </div>
        </div>

        <!-- Middle Menu -->
        <div class="middle-menu">
            <h3>Movies and Showtimes</h3>
            <div class="movie-list" id="movieListContainer" runat="server"> <!-- Movies will show -->
            </div>
        </div>
    </div>

    <script type="text/javascript">
        <!-- Local Storage area -->
        function storeInLocalStorage(movieName, showtime) {
            localStorage.setItem('selectedMovie', movieName); 
            localStorage.setItem('selectedShowtime', showtime); 
/*            alert("Movie and Showtime stored in local storage: " + movieName + " at " + showtime);*/
        }
    </script>
</asp:Content>
