<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="cancelPage.aspx.cs" Inherits="testForm.Pages.cancelPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <span style="font-size: 2rem; display: none; position: absolute">
    <asp:Label runat="server" ID="lblPosition" Text="User" />
    <asp:Label runat="server" ID="lblUser" Text="User" />
    </span>
    <!--ListView-->
    <div style="padding: 0px 5px; width: 100%; height: initial;">
        <h1>Cancel Material</h1>
        <fieldset style="padding: 5px 5px" runat="server" id="listViewCancel">
            <table>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="lstbxMatIDDESC" CssClass="btn btn-info btn-sm">
                            <asp:ListItem Text="Material ID" Selected="True" />
                            <asp:ListItem Text="Material Description" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 300px; padding-right: 5px">
                        <asp:TextBox runat="server" ID="inputMatIDDESC" CssClass="txtBoxDesc" AutoComplete="off" AutoPostBack="true" OnTextChanged="Page_Load" onkeydown="return(event.keyCode!=13);" />
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
                            DataKeyNames="TransID, MaterialID, MaterialDesc" AutoGenerateColumns="False"
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
                                <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="50" />
                                <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="50" />
                                <asp:BoundField DataField="MatType" HeaderText="MatType" ItemStyle-Width="80" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel"  DataField="CancelStatusApprovement" HeaderText="CancelStatusApprovement" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="RemarkCancel" HeaderText="RemarkCancel" ItemStyle-Width="150" />
                                <asp:BoundField HeaderStyle-CssClass="" ItemStyle-CssClass="" DataField="GlobalStatus" HeaderText="GlobalStatus" ItemStyle-Width="250" />

                                <asp:TemplateField ItemStyle-Width="160px">
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="slcThsMatIDDESC" Text="Cancel This Material" OnClick="cancelThis_Click" />
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

        <fieldset runat="server" id="cancelForm" style="width: 43%; margin: auto" visible="false">
            <legend>Cancel Material</legend>
            <table style="width: initial; margin: 0px 5% 0px 5%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTransID" Text="TransID" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputTransID" onkeypress="return this.value.length<=9" ReadOnly="true" CssClass="txtBoxRO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblMaterialID" Text="Material ID" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputMatID" onkeypress="return this.value.length<=17" ReadOnly="true" CssClass="txtBoxRO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblMaterialDesc" Text="Material Description" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputMaterialDesc" onkeypress="return this.value.length<=24" ReadOnly="true" CssClass="txtBoxRO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblPlant" Text="Plant" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputPlant" onkeypress="return this.value.length<=2" ReadOnly="true" CssClass="txtBoxRO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblType" Text="Type" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputType" onkeypress="return this.value.length<=2" ReadOnly="true" CssClass="txtBoxRO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblMaterialType" Text="Material Type" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputMaterialType" onkeypress="return this.value.length<=5" ReadOnly="true" CssClass="txtBoxRO" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: right">
                        <asp:Label runat="server" ID="lblRemark" Text="Remark" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="inputRemark" Height="150" TextMode="MultiLine" Columns="40" Rows="5"  required="true" />
                    </td>
                </tr>
                <tr style="text-align:right">
                    <td colspan="2">
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelSave" Text="Cancel" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnCancelSave_Click" />

                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click" Visible="false" />
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelApprove" Text="Reject" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnCancelApprove_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
