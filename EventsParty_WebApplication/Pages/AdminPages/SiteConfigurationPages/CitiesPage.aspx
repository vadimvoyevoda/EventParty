<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CitiesPage.aspx.cs" Inherits="EventsParty_WebApplication.Pages.AdminPages.SiteConfigurationPages.CitiesPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Styles/Pages/CitiesStyles.css" />
    <script src="/Scripts/jquery-2.1.4.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnCity").css("background-color", "#316EBB").css("color", "white").css("font-size", "16px");
            $("#dConfig").show();

            $("#btnAddCountry").click(function () {
                if ($('#dCountry').css("visibility") == "hidden") {
                    $('#dCountry').css("visibility", "visible");
                }
                else {
                    $('#dCountry').css("visibility", "hidden");
                    $('#tbNewCountry').val('');
                    $('#lbAddCountryMsg').text('');
                }
                return false;
            });

            $("#btnEditCountry").click(function () {
                if ($('#EditCountry').css("visibility") == "hidden") {
                    $('#EditCountry').css("visibility", "visible");
                    $('#tbEditC').val($('#ddlCountries option:selected').text());
                }
                else {
                    $('#EditCountry').css("visibility", "hidden");
                    $('#tbEditC').val('');
                    $('#lbEditCMsg').text('');
                }
                return false;
            });

            $("#btnAddRegion").click(function () {
                if ($('#dRegion').css("visibility") == "hidden") {
                    $('#dRegion').css("visibility", "visible");
                }
                else {
                    $('#dRegion').css("visibility", "hidden");
                    $('#tbNewRegion').val('');
                    $('#lbAddRegionMsg').text('');
                }
                return false;
            });

            $("#btnEditRegion").click(function () {
                if ($('#EditRegion').css("visibility") == "hidden") {
                    $('#EditRegion').css("visibility", "visible");
                    $('#tbEditR').val($('#ddlRegions option:selected').text());
                }
                else {
                    $('#EditRegion').css("visibility", "hidden");
                    $('#tbEditR').val('');
                    $('#lbEditRMsg').text('');
                }
                return false;
            });

            $("#btnAddCity").click(function () {
                if ($('#S1').css("visibility") == "hidden") {
                    $('#S1').css("visibility", "visible");
                }
                else {
                    $('#S1').css("visibility", "hidden");
                    $('#tbNewCity').val('');
                    $('#lbAddCityMsg').text('');
                }
                return false;
            });

            $(".editClass").click(function (event) {
                $tr = $(event.target).closest("tr");
                $text = $tr.children('td:nth-child(2)').text();
                $top = $tr.position().top - 2;
                $("#dEdit").css("position", "absolute").css("left", $("#gvCities").width() + 20).css("top", $top);

                if ($("#tbEditNew").val() != $text) {
                    $("#tbEditNew").val($text);
                    if ($("#dEdit").css("display") == "none") {
                        $("#dEdit").toggle("slow");
                    }
                }
                else {
                    $("#dEdit").toggle("slow");
                }
                $("#hf").val($text);
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:HiddenField runat="server" ID="hf" ClientIDMode="Static"/>

    <div id="Country">
        <div id="C1">
            <asp:Label runat="server" ID="lbCountry" Text="Country"></asp:Label>
            <asp:UpdatePanel runat="server" ID="upDDLC" UpdateMode="Conditional" ClientIDMode="Static" ChildrenAsTriggers="false">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSaveCountry" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnEditC" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeleteCountry" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddlCountries" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlCountries_OnSelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="C2">
            <asp:Button runat="server" ID="btnAddCountry" Text="Add Country" ClientIDMode="Static" />
            <asp:Button runat="server" ID="btnEditCountry" Text="Edit Country" ClientIDMode="Static"/>
            <asp:Button runat="server" ID="btnDeleteCountry" Text="Delete Country" ClientIDMode="Static" OnClick="btnDeleteCountry_Click"/>
        </div>

        <div id="dCountry">
            <asp:Label runat="server" ID="lbNewC">New</asp:Label>
            <asp:TextBox runat="server" ID="tbNewCountry" ClientIDMode="Static"></asp:TextBox>
            <asp:Button runat="server" ID="btnSaveCountry" Text="Ok" OnClick="btnSaveCountry_Click" ClientIDMode="Static" />
            <asp:UpdatePanel runat="server" ID="upCountry" UpdateMode="Conditional" ClientIDMode="Static">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSaveCountry" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label runat="server" ID="lbAddCountryMsg" ClientIDMode="Static"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="EditCountry">
            <asp:Label runat="server" ID="lbEditC">Edit</asp:Label>
            <asp:TextBox runat="server" ID="tbEditC" ClientIDMode="Static"></asp:TextBox>
            <asp:Button runat="server" ID="btnEditC" Text="Ok" ClientIDMode="Static" OnClick="btnEditCountry_Click"/>
            <asp:UpdatePanel runat="server" ID="upEditC" UpdateMode="Conditional" ClientIDMode="Static">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEditC" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label runat="server" ID="lbEditCMsg" ClientIDMode="Static"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="Region">
        <div id="R1">
            <asp:Label runat="server" ID="lbRegion" Text="Region"></asp:Label>
            <asp:UpdatePanel runat="server" ID="upDDLReg" UpdateMode="Conditional" ClientIDMode="Static" ChildrenAsTriggers="false">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCountries" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveRegion" EventName="Click" />   
                    <asp:AsyncPostBackTrigger ControlID="btnDeleteRegion" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnEditR" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddlRegions" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlRegions_OnSelectedIndexChanged"></asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="R2">
            <asp:Button runat="server" ID="btnAddRegion" Text="Add Region" ClientIDMode="Static" />
            <asp:Button runat="server" ID="btnEditRegion" Text="Edit Region" ClientIDMode="Static" />
            <asp:Button runat="server" ID="btnDeleteRegion" Text="Delete Region" ClientIDMode="Static" OnClick="btnDeleteRegion_Click"/>
        </div>

        <div id="dRegion">
            <asp:Label runat="server" ID="lbNewR">New</asp:Label>
            <asp:TextBox runat="server" ID="tbNewRegion" ClientIDMode="Static"></asp:TextBox>
            <asp:Button runat="server" ID="btnSaveRegion" Text="Ok" OnClick="btnSaveRegion_Click" ClientIDMode="Static" />
            <asp:UpdatePanel runat="server" ID="upRegion" UpdateMode="Conditional" ClientIDMode="Static">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSaveRegion" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label runat="server" ID="lbAddRegionMsg" ClientIDMode="Static"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div id="EditRegion">
            <asp:Label runat="server" ID="lbEditR">Edit</asp:Label>
            <asp:TextBox runat="server" ID="tbEditR" ClientIDMode="Static"></asp:TextBox>
            <asp:Button runat="server" ID="btnEditR" Text="Ok" ClientIDMode="Static" OnClick="btnEditRegion_Click"/>
            <asp:UpdatePanel runat="server" ID="upEditR" UpdateMode="Conditional" ClientIDMode="Static">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEditR" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label runat="server" ID="lbEditRMsg" ClientIDMode="Static"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
        
    <div id="dCity">
        <asp:Button runat="server" ID="btnAddCity" Text="Add City" ClientIDMode="Static" />
        <div id="S1">
            <asp:TextBox runat="server" ID="tbNewCity" ClientIDMode="Static"></asp:TextBox>
            <asp:Button runat="server" ID="btnSaveCity" Text="Ok" OnClick="btnSaveCity_Click" ClientIDMode="Static" />
            <asp:UpdatePanel runat="server" ID="upCity" UpdateMode="Conditional" ClientIDMode="Static">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSaveCity" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label runat="server" ID="lbAddCityMsg" ClientIDMode="Static"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="upGrid" UpdateMode="Conditional" ClientIDMode="Static">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveCity" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlRegions" EventName="SelectedIndexChanged"/>   
            <asp:AsyncPostBackTrigger ControlID="ddlCountries" EventName="SelectedIndexChanged" />    
        </Triggers>
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvCities" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" ClientIDMode="Static" DataKeyNames="Id" OnRowCommand="gvCities_RowCommand">
                <AlternatingRowStyle BackColor="#C6DBF5"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="N">
                        <ItemTemplate>
                            <asp:Label ID="lbRowNumber" ForeColor="Black" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="City" DataField="CityName" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:TemplateField HeaderText="Operations">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="editClass" ToolTip="Edit">
                                <asp:Image ID="iEdit" runat="server" ImageUrl="/Images/edit.png" />
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
