<%@ Page Title="Ratings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RatingsPage.aspx.cs" Inherits="EventsParty_WebApplication.Pages.AdminPages.RatingsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Styles/Pages/ConfigStyles.css" />
    <script src="/Scripts/jquery-2.1.4.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/PageConfig.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnRating").css("background-color", "#316EBB").css("color", "white").css("font-size", "16px");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hf" ClientIDMode="Static"/>
    <asp:Button runat="server" ID="btnAdd" Text="Add New Rating" ClientIDMode="Static" />
    <div id="dAdd">
        <asp:TextBox runat="server" ID="tbNewType" ClientIDMode="Static"></asp:TextBox>
        <asp:Button runat="server" ID="btnSave" Text="Ok" OnClick="btnSave_Click" />
        <asp:UpdatePanel runat="server" ID="upNew" UpdateMode="Conditional" ClientIDMode="Static">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Label runat="server" ID="lbMsg" ClientIDMode="Static"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>    
    <asp:UpdatePanel runat="server" ID="upGrid" UpdateMode="Conditional" ClientIDMode="Static" ChildrenAsTriggers="False">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditSave" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvItems" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="Id" OnRowCommand="gvItems_RowCommand" ClientIDMode="Static">
                <AlternatingRowStyle BackColor="#C6DBF5"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="N">
                        <ItemTemplate>
                            <asp:Label ID="lbRowNumber" ForeColor="Black" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Rating" DataField="Name" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:TemplateField HeaderText="Operations">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="editClass" ToolTip="Edit">
                                <asp:Image ID="iEdit" runat="server" ImageUrl="/Images/edit.png"/>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteRow" OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                    CommandArgument='<%#Eval("Id") %>' ToolTip="Delete">
                                <asp:Image ID="iDelete" runat="server" ImageUrl="/Images/delete.png" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div id="dEdit">
        <asp:TextBox runat="server" ID="tbEditNew" ClientIDMode="Static"></asp:TextBox>
        <asp:Button runat="server" ID="btnEditSave" ClientIDMode="Static" Text="Save" OnClick="btnEditSave_OnClick" />
    </div>
</asp:Content>
