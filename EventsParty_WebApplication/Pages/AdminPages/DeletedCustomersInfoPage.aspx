<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeletedCustomersInfoPage.aspx.cs" Inherits="EventsParty_WebApplication.Pages.AdminPages.SiteConfigurationPages.DeletedCustomersInfoPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-2.1.4.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Styles/Pages/ProfileInfoStyles.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnBanList").css("background-color", "#316EBB").css("color", "white").css("font-size", "16px");
            $("#dCustomers").show();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Image runat="server" ID="imgAvatar" />
    <div id="info">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Login:" />
            <asp:Label runat="server" ID="lbLogin" />
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Name:" />
            <asp:Label runat="server" ID="lbName" />
        </div>
        <div>
            <asp:Label ID="Label3" runat="server" Text="LastName:" />
            <asp:Label runat="server" ID="lbLastName" />
        </div>
        <div>
            <asp:Label ID="Label4" runat="server" Text="Rating:" />
            <asp:Label runat="server" ID="lbRating" />
        </div>
        <div>
            <asp:Label ID="Label5" runat="server" Text="Mobile:" />
            <asp:Label runat="server" ID="lbMobile" />
        </div>
        <div>
            <asp:Label ID="Label6" runat="server" Text="Email:" />
            <asp:Label runat="server" ID="lbEmail" />
        </div>
        <div>
            <asp:Label ID="Label7" runat="server" Text="Birthday:" />
            <asp:Label runat="server" ID="lbBirthday" />
        </div>
        <div>
            <asp:Label ID="Label8" runat="server" Text="Country:" />
            <asp:Label runat="server" ID="lbCountry" />
        </div>
        <div>
            <asp:Label ID="Label9" runat="server" Text="Address:" />
            <asp:Label runat="server" ID="lbAddress" />
        </div>
        <div>
            <asp:Label ID="Label10" runat="server" Text="OrganizedEventCount:" />
            <asp:Label runat="server" ID="lbOrgEventCount" />
        </div>
        <div>
        <asp:Label ID="Label11" runat="server" Text="Permission:" />
        <asp:DropDownList runat="server" ID="ddlPermission" />
        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="saveClass" OnClick="btnSave_Click"/>
        </div>
    </div>
    <br />
    <div>
        <asp:Button runat="server" ID="btnBack" Text="Back" CssClass="backClass" OnClick="btnBack_Click"/>
        <asp:Button runat="server" ID="btnDel" Text="Return" CssClass="banClass" OnClick="btnDel_Click"/>
    </div>
</asp:Content>
