<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomersPage.aspx.cs" Inherits="EventsParty_WebApplication.Pages.AdminPages.UsersList" %>
<%@ Import Namespace="DataModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Styles/Pages/CustomersStyles.css"/>
    <script src="/Scripts/jquery-2.1.4.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#btnCustomersList").css("background-color", "#316EBB").css("color", "white").css("font-size","16px");
            $("#dCustomers").show();

            $('td:contains("Admin")').css("color", "green").css("font-weight","bold");

            $("#btnSearch").click(function () {
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_OnClick" CssClass="btnSearchClass" ToolTip="Search" ></asp:Button>
    <asp:TextBox runat="server" ID="tbSearch" ToolTip="Search Text"></asp:TextBox>
    <asp:Label runat="server" ID="lbRatings" ToolTip="Choose Rating" Text="Rating"></asp:Label>
    <asp:DropDownList runat="server" ID="ddlRatings"/>
    <asp:CheckBox runat="server" ID="cbRatingFilter" ToolTip="Use rating filter"></asp:CheckBox>
    <asp:Label runat="server" ID="lbPermissions" ToolTip="Choose Permission" Text="Permission"></asp:Label>
    <asp:DropDownList runat="server" ID="ddlPermissions"/>
    <asp:CheckBox runat="server" ID="cbPermissionFilter" ToolTip="Use permission filter"></asp:CheckBox>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"/>
       </Triggers>
         <ContentTemplate>
            <asp:Label runat="server" ID="lbSearchInfo" ToolTip="Search Info"></asp:Label>
            <asp:GridView runat="server" ID="gvUsers" DataKeyNames="Id" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                <AlternatingRowStyle BackColor="#C6DBF5"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="N">
                        <ItemTemplate>
                            <asp:Label ID="lbRowNumber" ForeColor="Black" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Photo">
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%#GetImage(Eval("Photo")) %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Login">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" Text='<%#Eval("Login")%>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Id", "~/Pages/AdminPages/CustomerInfoPage.aspx?id={0}") %>' ItemStyle-HorizontalAlign="Center" ToolTip="Show More Info"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Name" DataField="Name" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="LastName" DataField="LastName" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Rating" DataField="Rating.Name" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Permission" DataField="Permissions.Type" ItemStyle-HorizontalAlign="Center"/>               
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
