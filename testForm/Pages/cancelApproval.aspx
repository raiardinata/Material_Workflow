<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="cancelApproval.aspx.cs" Inherits="testForm.Pages.cancelApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <span runat="server" id="spanTransID" style="position: absolute; top: 85px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" />
    </span>
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblPosition" Text="User" />
        <asp:Label runat="server" ID="lblLogID" Text="LD00000000" />
        <asp:Label runat="server" ID="lblLine" Text="LN000" />
        <asp:Label runat="server" ID="lblFxdP" Text="0" />
        <asp:Label runat="server" ID="lblDont" Text="0" />
        <asp:Label runat="server" ID="lblWith" Text="0" />
        <asp:Label runat="server" ID="lblMat" Text="0" />
        <asp:Label runat="server" ID="lblMenu" Text="Finance" />
        <asp:Label runat="server" ID="lblLineInspectionType" Text="LN000" />
        <asp:Label runat="server" ID="lblLineClassType" Text="LN000" />
        <asp:Label runat="server" ID="lblChkbx" Text="Test" />
        <asp:Label runat="server" ID="lblMatID" Text="0" />
        <asp:Label runat="server" ID="lblDDPriceCtrl" Text="" />
        <asp:Label runat="server" ID="lblRevision" Text="" />
        <asp:Label runat="server" ID="FilterSearch" Text="" />
    </span>
    <!--ListView-->
    <div runat="server" id="listViewFico" style="padding: 0px 5px">
        <h1>FICO - Master Data</h1>
        <fieldset style="padding: 5px 5px">
            <table>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="lsbxMatIDDESC" CssClass="btn btn-info btn-sm">
                            <asp:ListItem Text="Material ID" Selected="True" />
                            <asp:ListItem Text="Material Description" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: auto; padding-right: 5px">
                        <asp:TextBox runat="server" ID="inputMatIDDESC" placeholder="MaterialID/Material Description" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=13);" />
                    </td>
                    <td style="width: auto">
                        <asp:ImageButton runat="server" ID="searchBtn" ImageUrl="../Images/src-white.png" Style="height: 30px; width: 30px;" class="btn btn-info btn-sm" type="button" OnClick="srcListview_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <!--Material ID and Desc List View-->
            <div style="max-width: 1300px">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:GridView ID="GridViewListView" runat="server"
                            DataKeyNames="MaterialID, MaterialDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowDataBound="checkStatsNGlobStats_RowDataBound" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="GridViewListView_PageIndexChanging">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="TransID" HeaderText="Transaction ID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MaterialID" HeaderText="Material ID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MaterialDesc" HeaderText="Material Description" ItemStyle-Width="350" />
                                <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="50" />
                                <asp:BoundField DataField="Usnam" HeaderText="Initiate By" ItemStyle-Width="110" />
                                <asp:BoundField DataField="ExtendPlant" HeaderText="Extend Plant" ItemStyle-Width="50" />
                                <%--<asp:BoundField DataField="InputBy" HeaderText="Input By" ItemStyle-Width="150" />--%>

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="RDStart" HeaderText="RDStart" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="RDEnd" HeaderText="RDEnd" ItemStyle-Width="150" />

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="ProcStart" HeaderText="ProcStart" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="ProcEnd" HeaderText="ProcEnd" ItemStyle-Width="150" />

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="PlanStart" HeaderText="PlanStart" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="PlanEnd" HeaderText="PlanEnd" ItemStyle-Width="150" />

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="QCStart" HeaderText="QCStart" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="QCEnd" HeaderText="QCEnd" ItemStyle-Width="150" />

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="QAStart" HeaderText="QAStart" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="QAEnd" HeaderText="QAEnd" ItemStyle-Width="150" />

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="QRStart" HeaderText="QRStart" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="QREnd" HeaderText="QREnd" ItemStyle-Width="150" />

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="FicoStart" HeaderText="FicoStart" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="FicoEnd" HeaderText="FicoEnd" ItemStyle-Width="150" />

                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="Status" HeaderText="Status" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="GlobalStatus" HeaderText="GlobalStatus" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="Notes" HeaderText="Notes" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="MaterialApproved" HeaderText="MaterialApproved" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStatus" Text="Waiting" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100">
                                    <HeaderTemplate>
                                        Global Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGlobalStatus" Text="Dept Const" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="400">
                                    <HeaderTemplate>
                                        Notes
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNotes" Text="Proc/Plan/QC/QA/QR" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150px">
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="slcThsMatIDDESCFico" Text="Cancel Approve" OnClick="modifyThisFico_Click" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="Numeric" Visible="False"></PagerSettings>
                            <PagerStyle Font-Bold="True" />
                        </asp:GridView>
                    </div>
                    <div id="dataPager" runat="server" class="modal-footer">
                        <asp:Label ID="lblTotalRecords" runat="server" Text="Total Records:" CssClass="totalRecords" />
                        <div id="paging" runat="server">
                            <asp:Label ID="lblShowRows" runat="server" Text="Show rows:" />
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" ForeColor="#8C4510">
                                <asp:ListItem Value="5" />
                                <asp:ListItem Value="10" Selected="True" />
                                <asp:ListItem Value="15" />
                                <asp:ListItem Value="20" />
                                <asp:ListItem Value="25" />
                                <asp:ListItem Value="50" />
                                <asp:ListItem Value="100" />
                            </asp:DropDownList>
                            &nbsp; Page
                        <asp:TextBox ID="txtGoToPage" runat="server" AutoPostBack="true" OnTextChanged="GoToPage_TextChanged"
                            CssClass="gotopage" Width="15px" ForeColor="#8C4510" />
                            of
                        <asp:Label ID="lblTotalNumberOfPages" runat="server" />
                            &nbsp;
                        <asp:Button ID="btnPrev" runat="server" Text="<" ToolTip="Previous Page" OnClick="btnPrev_OnClick" class="btn btn-info btn-sm" />
                            <asp:Button ID="btnNext" runat="server" Text=">" CommandName="Page" ToolTip="Next Page" CommandArgument="Next"
                                OnClick="btnNext_OnClick" class="btn btn-info btn-sm" />

                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
