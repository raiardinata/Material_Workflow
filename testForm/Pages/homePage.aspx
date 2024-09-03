<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="homePage.aspx.cs" Inherits="testForm.Pages.homePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <span style="font-size: 2rem; padding: 0px 10px">
        <asp:Label runat="server" ID="lblWlcm" Text="Welcome" />
        <b>
            <asp:Label runat="server" ID="lblUser" Text="User" /></b>        
    </span>
    <asp:Image runat="server" ID="bckIMG" ImageUrl="~/Images/alternativePng2.png" ImageAlign="Right" Width="600px" />
</asp:Content>
