<%@ Page Title="Admin Page" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Cinema_X.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/AdminPageStyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-container">
        <!-- Film Upload Section -->
        <h2>Upload Film Information</h2>
        <asp:Panel ID="AdminPanel" runat="server">
            <div class="form-group">
                <label for="movieTitle">Film Name:</label>
                <asp:TextBox ID="txtMovieTitle" runat="server" CssClass="form-control" Placeholder="Enter the movie title"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="movieGender">Genre:</label>
                <asp:TextBox ID="txtMovieGender" runat="server" CssClass="form-control" Placeholder="Enter the movie genre"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="movieDescription">Description:</label>
                <asp:TextBox ID="txtMovieDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Placeholder="Enter the movie description"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="actors">Actors:</label>
                <asp:TextBox ID="txtActors" runat="server" CssClass="form-control" Placeholder="Enter the actors (comma-separated)"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="releaseDate">Release Date:</label>
                <asp:TextBox ID="txtReleaseDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="endDate">End Date:</label>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="showtime1">Showtime 1:</label>
                <asp:TextBox ID="txtShowtime1" runat="server" CssClass="form-control" Placeholder="Enter showtime (e.g., 14:00)"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="showtime2">Showtime 2:</label>
                <asp:TextBox ID="txtShowtime2" runat="server" CssClass="form-control" Placeholder="Enter showtime (e.g., 18:00)"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="showtime3">Showtime 3:</label>
                <asp:TextBox ID="txtShowtime3" runat="server" CssClass="form-control" Placeholder="Enter showtime (e.g., 20:00)"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="poster">Poster URL:</label>
                <asp:TextBox ID="txtPosterURL" runat="server" CssClass="form-control" Placeholder="Enter the poster URL"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="salon">Salon:</label>
                <asp:TextBox ID="txtSalon" runat="server" CssClass="form-control" Placeholder="Enter the salon"></asp:TextBox>
            </div>

            <asp:Button ID="btnSubmit" runat="server" Text="Upload" CssClass="btn-submit" OnClick="btnSubmit_Click" />
        </asp:Panel>

        <!-- Manage Films Section -->
        <h2>Manage Films</h2>
        <asp:GridView ID="gvFilms" runat="server" AutoGenerateColumns="False" CssClass="gridview" OnRowCommand="gvFilms_RowCommand" style="color: black;">
            <Columns>
                <asp:BoundField DataField="MovieName" HeaderText="Film Name" SortExpression="MovieName" />
                <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" SortExpression="ReleaseDate" />
                <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" CommandName="EditFilm" CommandArgument='<%# Eval("MovieID") %>' Text="Edit" CssClass="btn-edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" CommandName="DeleteFilm" CommandArgument='<%# Eval("MovieID") %>' Text="Delete" CssClass="btn-delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- List Members Button and Section -->
        <h2>Manage Members</h2>
        <asp:Button ID="btnListMembers" runat="server" Text="List Members" OnClick="btnListMembers_Click" CssClass="btn-submit" />

        <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="True" CssClass="gridview" style="margin-top: 20px;">
        </asp:GridView>
        
    </div>
</asp:Content>
