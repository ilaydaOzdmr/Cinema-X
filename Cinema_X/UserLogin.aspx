<%@ Page Title="User Login" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="Cinema_X.UserLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/UserLoginStyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login-form">
            <h2>User Login</h2>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" placeholder="User Mail" CssClass="form-control" /><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control" /><br />

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click"/>

            <div class="link">
                <span>Don't Have an Account? <a href="UserSignUp.aspx">Sign Up</a></span>
            </div>

          
            <div class="back-link">
                <a href="HomePage.aspx">Back to Home</a>
            </div>
        </div>
    </div>
</asp:Content>
