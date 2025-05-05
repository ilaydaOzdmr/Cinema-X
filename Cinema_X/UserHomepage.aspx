<%@ Page Title="Cinema-X" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Cinema_X.HomePage" %>

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
                <asp:Button ID="btnListMovies" runat="server" Text="List" OnClick="btnListMovies_Click" CssClass="btn-list" />
            </div>
        </div>

        <!-- Middle Menu -->
        <div class="middle-menu">
            <h3>Movies and Showtimes</h3>
            <div class="movie-list" id="movieListContainer" runat="server">
                <!-- Movies will be dynamically added here -->
            </div>
        </div>
    </div>
</asp:Content>
