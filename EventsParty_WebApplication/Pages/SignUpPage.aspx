<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpPage.aspx.cs" Inherits="EventsParty_WebApplication.Pages.SignUpPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link rel="stylesheet" type="text/css" href="../Styles/Pages/SignUpStyles.css" />

    <link rel="stylesheet" href="../Styles/DatePicker/jquery-ui.css" />
    <script src="../Scripts/DatePicker/jquery-1.10.2.js"></script>
    <script src="../Scripts/DatePicker/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            $("#tbBirthday").datepicker(
                {
                    defaultDate: "-25y",
                    dateFormat: "dd-mm-yy",
                    inline: true,
                    showAnim: 'fadeIn',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1940:-3",
                    minDate: new Date(1940,0,1),
                    maxDate: "-3y",
                    firstDay: 1
                });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <asp:Image runat="server" ImageUrl="~/Images/Logo.png" />
            <asp:Label runat="server" Text="New User Registration" />
        </header>
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbLogin">Login</asp:Label></td>
                    <td>
                        <asp:TextBox runat="server" ID="tbLogin" MaxLength="50" ToolTip="Login must consist of letters, digits, '-', '_', '.' and ' '" OnTextChanged="tbLogin_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upLogin" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbLogin" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label runat="server" ID="lbCheckLogin"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbPassword" runat="server">Password</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" OnTextChanged="tbPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upPassword" runat="server" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbPassword" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbConfirm" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckPassword" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbConfirm" runat="server">Confirm Password</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbConfirm" TextMode="Password" OnTextChanged="tbPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upConfirm" runat="server" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbPassword" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbConfirm" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckConfirm" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbName" runat="server">Name</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbName" MaxLength="50" ToolTip="Name must starts from upper letter and could contains letters, defises and spaces" OnTextChanged="tbName_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upName" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbName" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckName" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lbLastName" runat="server">Last Name</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbLastName" MaxLength="50" ToolTip="LastName must starts from upper letter and could contains letters, defises and spaces" OnTextChanged="tbLastName_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upLastName" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbLastName" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckLastName" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbMobile" runat="server">Mobile</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbMobile" MaxLength="13" ToolTip="In format +xxxyyyyyyyyy, xxx - country phone code, yyyyyyyyy - phone number" OnTextChanged="tbMobile_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upMobile" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbMobile" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckMobile" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbEmail" runat="server">Email</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbEmail" ToolTip="Email in format 'xxx@yyy.zzz', where 'xxx' could consist of letters, digits, symbols -+.', 'yyy' and 'zzz' - letters and '-.'" OnTextChanged="tbEmail_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upEmail" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbEmail" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckEmail" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbBirthday" runat="server">Birthday</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbBirthday" ToolTip="Birth Date must be in range between 1940 and (this year - 2)" OnTextChanged="tbBirthday_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upBirthday" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbBirthday" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckBirthday" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbCountry">Country</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbCountry" ToolTip="Country must consist of letters, defises and spaces" OnTextChanged="tbCountry_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="valid">
                        <asp:UpdatePanel ID="upCountry" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbCountry" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbCheckCountry" runat="server"></asp:Label>                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbAddress">Address</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbAddress"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnCancel" CssClass="cancelClass" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnSignUp" CssClass="signUpClass" Text="Sign Up" OnClick="btnSignUp_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
