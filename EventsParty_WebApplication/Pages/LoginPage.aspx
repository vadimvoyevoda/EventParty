<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="EventsParty_WebApplication.Pages.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="../Styles/Pages/LoginStyles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <asp:Label ID="lbAuthenticate" runat="server" Text="User Authenticate" />
        </header>
        <div class="signForm">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Logo.png" />
            <asp:Label runat="server" ID="lbLogin" Text="Login" /><br />
            <asp:TextBox runat="server" ID="tbLogin" /><br />
            <asp:Label runat="server" ID="lbPass" Text="Password" /><br />
            <asp:TextBox runat="server" ID="tbPass" TextMode="Password" /><br />
            <asp:CheckBox runat="server" ID="cbRememberMe" Text="Remember me" />
            <asp:Label runat="server" ID="lbMsg" ClientIDMode="Static"></asp:Label><br />
            <asp:Button runat="server" ID="btnLogin" Text="Join" CssClass="joinClass" OnClick="btnLogin_Click" />
            <asp:Button runat="server" ID="btnSignUp" Text="SignUp" CssClass="signUpClass" OnClick="btnSignUp_Click" />
        </div>
    </form>
</body>
</html>
