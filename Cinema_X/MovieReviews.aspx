<%@ Page Title="MovieReviews" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MovieReviews.aspx.cs" Inherits="Cinema_X.MovieReviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/MovieReviewsStyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="review-section">
        <!-- Comment area -->
        <h2>Write Your Movie Comment</h2>

        <asp:TextBox ID="txtUserName" runat="server" CssClass="input-field" placeholder="Your Name" />
        <asp:TextBox ID="txtMovieName" runat="server" CssClass="input-field" placeholder="Movie Name" />

        <asp:TextBox ID="txtReview" runat="server" CssClass="input-field" TextMode="MultiLine" Rows="4" placeholder="Write A Comment..."></asp:TextBox>

        <label for="rating">Rate (1-5):</label>
        <asp:RadioButtonList ID="rblRating" runat="server">
            <asp:ListItem Text="1" Value="1" />
            <asp:ListItem Text="2" Value="2" />
            <asp:ListItem Text="3" Value="3" />
            <asp:ListItem Text="4" Value="4" />
            <asp:ListItem Text="5" Value="5" />
        </asp:RadioButtonList>

        <asp:Button ID="btnSubmit" runat="server" Text="Send Your Comment" CssClass="btn-submit" OnClick="btnSubmit_Click" />

        <!-- Previus comment area (all users) -->
        <h3>Other Comments:</h3>
        <div id="previousReviews">
            <asp:Repeater ID="rptReviews" runat="server">
                <ItemTemplate>
                    <div class="review">
                        <h4><%# Eval("MovieName") %> - Rate: <%# Eval("Rate") %></h4>
                        <p><%# Eval("Comment") %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>

