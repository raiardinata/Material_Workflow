<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/reportPage.aspx.cs" Inherits="testForm.Pages.reportPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../Script/jquery.maskedinput1.4.1.js"></script>
    <link href="../DatepickerJs/jquery-ui-css.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Viewer.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/Viewer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_txtDateStart").datepicker(
                {
                    dateFormat: 'dd-mm-yy',
                    showOtherMonths: true,
                    changeMonth: true,
                    changeYear: true,
                    selectOtherMonths: true
                });
            $("#MainContent_txtDateEnd").datepicker(
                {
                    dateFormat: 'dd-mm-yy',
                    showOtherMonths: true,
                    changeMonth: true,
                    changeYear: true,
                    selectOtherMonths: true
                });

        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblPosition" Text="User" />
    </span>
    <div id="header1" class="HeaderMain" style="border-bottom-color: currentColor; border-bottom-width: medium; border-bottom-style: none;">
        <h1>Report - Lead Time</h1>
        <%--<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>--%>
    </div>
    <div>
        <table border="0" cellpadding="3" cellspacing="0">
            <tr>
                <%--<td>
                                <asp:Label ID="lblCompanyID" runat="server" Text="Company ID" style="text-align: left"></asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" Font-Size="10pt">
                                </asp:DropDownList></td>
                            <td style="width:30px"></td>--%>
                <td>
                    <asp:Label ID="lblMatID" runat="server" Text="MaterialID" Style="text-align: left" />
                </td>
                <td>
                    <asp:TextBox ID="inputMatID" runat="server" Width="150" />
                </td>
                <td style="width: 30px"></td>
                <td>
                    <td>
                        <asp:Label ID="lDate1" runat="server" Text="Date Start (dd-mm-yyyy)" Style="text-align: left" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateStart" runat="server" Width="100" />
                    </td>
                    <td style="width: 30px"></td>
                    <td>
                        <asp:Label ID="lDate2" runat="server" Text="Date End (dd-mm-yyyy)" Style="text-align: left" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateEnd" runat="server" Width="100" />
                    </td>
                    <td>
                        <%--<asp:Button ID="btnPrint" runat="Server" Text="Print" CssClass="ui-button" OnClick="btnPrint_Click" Style="display: none" />--%>
                        <asp:Button ID="btnSubmit" runat="Server" Text="Generate" CssClass="ui-button" OnClick="btnSubmit_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Size="Small" Visible="false" /></td>
            </tr>
        </table>
    </div>
    <div>
        <fieldset style="padding: 5px 5px 5px 5px; background-color: white">
            <table border="0" cellpadding="0" cellspacing="0" style="height: initial; width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <rsweb:ReportViewer ID="rViewer" runat="server" ProcessingMode="Remote" AsyncRendering="true" SizeToReportContent="false" Width="95%" Height="95%"></rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
