<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="masterUser.aspx.cs" Inherits="testForm.Pages.masterUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblPosition" Text="User" />
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblReset" Text="" />
    </span>
    <div style="padding: 0px 5px; width: 100%; height: initial;">
        <h1>Master User</h1>

        <fieldset runat="server" id="cancelForm" style="height: 450px; width: 43%; margin: 0px auto 20px auto" visible="true">
            <legend>Change Master User</legend>
            <table style="width: initial; margin: 0px auto 0px auto">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblUserID" Text="User ID" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="inputUserID" CssClass="txtBoxRO" AutoPostBack="true" OnSelectedIndexChanged="inputUserID_SelectedIndexChanged" Width="100%">
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblPassword" Text="Password" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputPassword" onkeypress="return this.value.length<=9" ReadOnly="true" CssClass="txtBoxRO" placeholder="Password" Width="100%" onkeydown="return(event.keyCode!=13);" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblName" Text="Name" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputName" onkeypress="return this.value.length<=49" CssClass="txtBoxRO" placeholder="Name" ReadOnly="true" Width="100%" onkeydown="return(event.keyCode!=13);" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblDivision" Text="Division" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputDivision" onkeypress="return this.value.length<=9" CssClass="txtBoxRO" placeholder="Division" ReadOnly="true" Width="100%" onkeydown="return(event.keyCode!=13);" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblSOrg" Text="Sales Org." />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputSOrg" onkeypress="return this.value.length<=9" CssClass="txtBoxRO" placeholder="Sales Org." ReadOnly="true" Width="100%" onkeydown="return(event.keyCode!=13);" />
                    </td>
                </tr>
                <tr style="text-align: right">
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblPlant" Text="Plant" />
                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox runat="server" ID="chkbx1100" Text="1100" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx2100" Text="2100" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx2200" Text="2200" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx2300" Text="2300" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx2400" Text="2400" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx3100" Text="3100" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx3200" Text="3200" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx3300" Text="3300" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx3400" Text="3400" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx5100" Text="5100" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx5200" Text="5200" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx6100" Text="6100" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />&nbsp;
                        <asp:CheckBox runat="server" ID="chkbx6200" Text="6200" OnCheckedChanged="chkbxReset_CheckedChanged" AutoPostBack="true" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblEmail" Text="Email" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputEmail" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" onkeydown="return(event.keyCode!=13);" />
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
