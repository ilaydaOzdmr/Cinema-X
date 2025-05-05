<%@ Page Title="Logout" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Cinema_X.Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Logout</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>You have been logged out successfully.</h2>
        <a href="HomePage.aspx">Return to Home Page</a>
    </div>
</asp:Content>
