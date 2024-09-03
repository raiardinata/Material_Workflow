<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="resetPassword.aspx.cs" Inherits="testForm.Pages.resetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <span style="font-size: 2rem; display: none; position: absolute">
    <asp:Label runat="server" ID="lblPosition" Text="User" />
    <asp:Label runat="server" ID="lblUser" Text="User" />
    </span>
    <div style="padding: 0px 5px; width: 100%; height: initial;">
        <h1>Change Password</h1>

        <fieldset runat="server" id="cancelForm" style="height: 230px; width: 43%; margin: auto" visible="true">
            <legend>Change Password</legend>
            <table style="width: initial; margin: 0px 5% 0px 5%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblUserID" Text="User ID" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputUserID" onkeypress="return this.value.length<=9" ReadOnly="true" CssClass="txtBoxRO" placeholder="User ID" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblOldPassword" Text="Old Password" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputOldPassword" onkeypress="return this.value.length<=9" ReadOnly="true" CssClass="txtBoxRO" placeholder="Old Password" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblNewPassword" Text="New Password" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputNewPassword" onkeypress="return this.value.length<=9" CssClass="txtBox" placeholder="New Password" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblConfirmPassword" Text="Confirm New Password" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputConfirmNewPassword" onkeypress="return this.value.length<=9" CssClass="txtBox" placeholder="Confirm New Password" />
                    </td>
                </tr>
                <tr style="text-align:right">
                    <td colspan="2">
                    </td>
                </tr>
            </table>
            <span style="position: relative; top: 10px; right: 10px; float: right">
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelSave" Text="Cancel" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnCancelSave_Click" />
            </span>
        </fieldset>
    </div>
</asp:Content>
