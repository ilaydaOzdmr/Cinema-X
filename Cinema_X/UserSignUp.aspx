<%@ Page Title="User Sign Up" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserSignUp.aspx.cs" Inherits="Cinema_X.UserSignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/UserSignUpStyleSheet.css" rel="stylesheet" />
    <script>
        // Register succes message
        function showSuccessPopup() {
            alert("Register is successfull!");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="signup-container">
        <div class="signup-form">
            <h2>Sign Up</h2>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:TextBox ID="txtFirstName" runat="server" placeholder="Name" CssClass="form-control" /><br />
            <asp:TextBox ID="txtLastName" runat="server" placeholder="Surname" CssClass="form-control" /><br />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="E-Mail" CssClass="form-control" /><br />
            <asp:TextBox ID="txtPhoneNumber" runat="server" placeholder="Phone Number" CssClass="form-control" /><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control" /><br />
            <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" CssClass="btn btn-primary" OnClick="btnSignUp_Click" />
           
            <div class="go-login">
               <span> Already have an account? <a href="UserLogin.aspx"> Login </a></span>
            </div>
             <div class="back-home">
                <a href="HomePage.aspx">Back to HomePage</a>
            </div>
        </div>
    </div>
</asp:Content>
