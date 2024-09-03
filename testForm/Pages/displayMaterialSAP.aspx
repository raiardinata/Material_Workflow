<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="displayMaterialSAP.aspx.cs" Inherits="testForm.Pages.displayMaterialSAP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="56000" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

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
        <h1>Create Material Master Data</h1>
        <fieldset style="padding: 5px 5px">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Menu Orientation="Horizontal" ID="rmsffgAspMenu" runat="server" OnMenuItemDataBound="NavigationMenu_MenuItemDataBound" OnMenuItemClick="NavigationMenu_OnMenuItemClick" CssClass="menuRMSFFG">
                            <StaticSelectedStyle CssClass="selected" />
                            <LevelMenuItemStyles>
                                <asp:MenuItemStyle CssClass="main_menu" />
                                <asp:MenuItemStyle CssClass="level_menu" />
                            </LevelMenuItemStyles>
                            <Items>
                                <asp:MenuItem Text="RM" Value="0" />
                                <asp:MenuItem Text="SF" Value="1" />
                                <asp:MenuItem Text="FG" Value="2" />
                            </Items>
                        </asp:Menu>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="lsbxMatIDDESC" CssClass="btn btn-info btn-sm">
                            <asp:ListItem Text="Material ID" Selected="True" />
                            <asp:ListItem Text="Material Description" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: auto; padding-right: 5px">
                        <asp:TextBox runat="server" ID="inputMatIDDESC" placeholder="MaterialID/Material Description" CssClass="txtBoxDesc" AutoComplete="off" OnTextChanged="Page_Load" onkeydown="return(event.keyCode!=13);" />
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
                                <asp:TemplateField ItemStyle-Width="15px" HeaderText="TransID">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="display" Text='<%#Eval("TransID")%>' OnClick="display_Click" />
                                        <%--<asp:ImageButton runat="server" ID="display" ImageUrl="~/Images/src.png" OnClick="display_Click" Width="15" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="TransID" HeaderText="Transaction ID" ItemStyle-Width="150" />--%>
                                <asp:BoundField DataField="MaterialID" HeaderText="Material ID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MaterialDesc" HeaderText="Material Description" ItemStyle-Width="350" />
                                <asp:BoundField DataField="MatType" HeaderText="MatType" ItemStyle-Width="50" />
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
                                <asp:TemplateField ItemStyle-Width="220">
                                    <HeaderTemplate>
                                        Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStatus" Text="Waiting" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150">
                                    <HeaderTemplate>
                                        Global Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGlobalStatus" Text="Dept Const" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150">
                                    <HeaderTemplate>
                                        Notes
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNotes" Text="Proc/Plan/QC/QA/QR" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100px">
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="slcThsMatIDDESCFico" Text="Modify This" OnClick="modifyThisFico_Click" Visible="false" />
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

    <!--Main Content-->
    <fieldset runat="server" id="rmContent" visible="false" style="width: auto">
        <legend style="padding: 0px 2px 0px 2px">Request New Material - Create Material Master Display</legend>

        <!--REPORT MENU-->
        <div runat="server" id="divApprMn" style="height: initial; padding: 0px 15px 0px 15px" visible="false">
            <asp:Menu Orientation="Horizontal" ID="FICONavigationMenu" runat="server" OnMenuItemDataBound="NavigationMenuReport_MenuItemDataBound" OnMenuItemClick="NavigationMenuReport_OnMenuItemClick">
                <StaticSelectedStyle CssClass="selected" />
                <LevelMenuItemStyles>
                    <asp:MenuItemStyle CssClass="main_menu" />
                    <asp:MenuItemStyle CssClass="level_menu" />
                </LevelMenuItemStyles>
                <Items>
                    <asp:MenuItem Text="Finance" Value="0" Selected="true" />
                    <asp:MenuItem Text="R&D" Value="1" />
                    <asp:MenuItem Text="Procurement" Value="2" />
                    <asp:MenuItem Text="Planner" Value="3" />
                    <asp:MenuItem Text="QC" Value="4" />
                    <asp:MenuItem Text="QA" Value="5" />
                    <asp:MenuItem Text="QR" Value="6" />
                </Items>
            </asp:Menu>
        </div>

        <!--FICO REPORT TABLE-->
        <div runat="server" id="ficoTBL" visible="true" style="padding: 0px 15px 0px 15px">
            <fieldset>
                <legend style="padding: 0px 2px 0px 2px">Request New Material - FICO</legend>
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset style="width: 100%;">
                                <legend>General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label89" Text="Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputType" type="text" placeholder="Type" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label90" Text="Material Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputMatTyp" type="text" placeholder="Material Type" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label1" Text="Material ID* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputMtrlID" type="text" placeholder="Material ID*" Required="true" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label10" Text="Material Description* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputMtrlDesc" type="text" placeholder="Material Description*" required="true" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label2" Text="Base Unit of Meas.* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputBsUntMeas" type="text" placeholder="Base Unit of Measure*" required="true" autocomplete="off" ReadOnly="true" />
                                        </td>
                                        <td colspan="2" style="width: 160px">
                                            <asp:Label runat="server" ID="lblBsUntMeas" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt" style="width: 160px">
                                            <asp:Label runat="server" ID="Label4" Text="Old Material Number " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputOldMtrlNum" type="text" placeholder="Old Material Number" autocomplete="off" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt" style="width: 160px">
                                            <asp:Label runat="server" ID="Label124" Text="Division " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                             <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputDivisionFico" type="text" placeholder="Division" autocomplete="off" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label111" Text="Plant " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputFicoPlant" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Finance Management - Accounting Data-->
                            <fieldset style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Finance Management - Accounting Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label3" Text="Profit Center* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" CssClass="txtBox" ID="inputProfitCent" type="text" placeholder="Profit Center*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputProfitCent_onBlur" required="true" />
                                        </td>
                                        <td runat="server" id="tdImgBtnFico1" style="width: auto">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProfitCent"></button>
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProfitCent" Text="Profit Center*" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absProfitCent" Text="Profit Center*" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label8" Text="Valuation Class* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputValClass" CssClass="txtBox" type="text" placeholder="Valuation Class*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputValClass_onBlur" required="true" />
                                        </td>
                                        <td runat="server" id="tdImgBtnFico2" style="width: auto">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalValClass"></button>
                                        </td>
                                        <td runat="server" id="tdValClass">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblValClass" Text="Valuation Class*" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absValClass" Text="Valuation Class*" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label17" Text="Price Control* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:DropDownList runat="server" ID="ddPriceCtrl" AutoPostBack="true" CssClass="txtBox">
                                                <asp:ListItem Text="" Value="" Selected="True" />
                                                <asp:ListItem Text="S" Value="S" />
                                                <asp:ListItem Text="V" Value="V" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label21" Text="Moving Price " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" CssClass="txtBox" ID="inputMovPrice" type="text" placeholder="Moving Price" autocomplete="off" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorMovPrice" runat="server" TargetControlID="inputMovPrice" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label25" Text="Standard Price " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" CssClass="txtBox" ID="inputStndrdPrice" type="text" placeholder="Standard Price" autocomplete="off" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorStndrdPrice" runat="server" TargetControlID="inputStndrdPrice" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                            <!--Finance Management - Costing Data-->
                            <fieldset style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Finance Management - Costing Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr runat="server" id="trCoProdFICO">
                                        <td style="width: 160px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label48" Text="Co-Product " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxCoProdFICO" AutoPostBack="true" OnCheckedChanged="chkbxCoProd_CheckedChanged" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label14" Text="Fixed Price " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxFxdPrice" AutoPostBack="true" OnCheckedChanged="chkbxFxdPrice_CheckedChanged" Checked="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label5" Text="Do Not Cost " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxDontCost" AutoPostBack="true" OnCheckedChanged="chkbxDontCost_CheckedChanged" Checked="true" />
                                            <asp:Label CssClass="lblChkBox" runat="server" ID="lblDontCost" Text="Active" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label6" Text="With Qty Structure " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxWithQtyStruct" AutoPostBack="true" OnCheckedChanged="chkbxWithQtyStruct_CheckedChanged" Checked="true" />
                                            <asp:Label CssClass="lblChkBox" runat="server" ID="lblWithQtyStruct" Text="Active" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label7" Text="Material Origin " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxMatOrigin" AutoPostBack="true" OnCheckedChanged="chkbxMatOrigin_CheckedChanged" Checked="true" />
                                            <asp:Label CssClass="lblChkBox" runat="server" ID="lblMatOrigin" Text="Active" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="label9" Text="Costing LOT Size" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputCostingLotSize" placeholder="Costing LOT Size" autocomplete="off" Text="1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd" style="width: 150px">
                                            <asp:Label runat="server" ID="label98" Text="Valuation Category" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputValCat" placeholder="Valuation Category" autocomplete="off" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <asp:Image runat="server" ID="revisionFicoImg" ImageUrl="~/Images/revision (1).png" Visible="false" CssClass="revisionImage" />
                <span style="float: right; padding-right: 10px; padding-bottom: 10px">
                    <button runat="server" id="btnRevisionFico" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalRejectFICO" visible="false">Revision</button>
                    <%--<asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnRevisionFico" Text="Revision" OnClick="RevisionFico_Click" Visible="false" />--%>
                </span>
            </fieldset>
        </div>

        <!--R&D REPORT-->
        <div runat="server" id="rndTBL" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: initial;">
                <legend style="padding: 0px 2px 0px 2px">Request New Material - R&D</legend>
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset runat="server" visible="false" id="bscDt1GnrlDt" style="width: 100%">
                                <legend>Basic Data 1 - General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label32" Text="Material Type " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMatType" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMatTyp" Text="Material Type Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label11" Text="Material ID* " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMatID" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label12" Text="Material Description " />
                                        </td>
                                        <td class="txtBox" style="text-align: left">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMatDesc" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label13" Text="Base Unit of Meas. " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputUoM" type="text" />
                                        </td>
                                        <td style="width: 170px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblUoMRnD" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label15" Text="Material Group " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMatGr" type="text" />
                                        </td>
                                        <td style="width: 170px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMatGr" Text="MatGr Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label16" Text="Old Material Number " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputOldMatNum" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label18" Text="Division " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputDivision" type="text" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblDivision" Text="Division Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label19" Text="Packaging Material " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputPckgMat" type="text" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPckgMat" Text="Packaging Mat Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Foreign Trade Import : Foreign Trade Data-->
                            <fieldset runat="server" visible="false" id="foreignTradeDt" style="width: 100%">
                                <legend>Foreign Trade Import - Foreign Trade Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label20" Text="Comm/Imp Code No " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputCommImpCode" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblCommImpCode" Text="Comm/Omp Code No. Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Plant Data / Stor 1 : Shelf Life Data-->
                            <fieldset runat="server" visible="false" id="plantShelfLifeDt" style="width: 100%">
                                <legend>Plant Data / Stor 1 - Shelf Life Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd" style="width: 150px">
                                            <asp:Label runat="server" ID="Label22" Text="Min. Rem. Shelf Life " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMinRemShLf" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label runat="server" ID="lblMinRemShelfLife" Text="DAY" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label51" Text="Period Ind. for SLED " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:DropDownList runat="server" ID="ddListSLED" type="text" placeholder="Period Ind. for SLED" CssClass="txtBoxRO">
                                                <asp:ListItem Text="D" Value="D" Selected="True" />
                                                <asp:ListItem Text="M" Value="M" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label24" Text="Total Shelf Life* " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputTotalShelfLife" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label runat="server" ID="Label26" Text="DAY" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                            <!--Basic Data 1 : Dimension/EAN-->
                            <fieldset runat="server" visible="false" id="bscDt1Dimension" style="width: 100%">
                                <legend>Basic Data 1 - Dimension/EAN</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 105px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label28" Text="Net Weight* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputNetWeight" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="Label50" Text="Net Weight Unit " />
                                        </td>
                                        <td>
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputNetWeightUnitRnd" type="text" autocomplete="off" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblNetWeightUnitRnd" Text="Net Weight Unit Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Basic Data 2 : Other Data-->
                            <fieldset style="width: 100%">
                                <legend>Basic Data 2 - Other Data</legend>
                                <table runat="server" id="Table1" style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 105px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label41" Text="Ind. Std. Desc. " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputIndStdDesc" type="text" autocomplete="off" ReadOnly="true" />
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblIndStdDesc" Text="Ind. Std. Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Organizational Level-->
                            <fieldset runat="server" visible="false" id="orgLv" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Organizational Level - Organizational Level</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl3" Text="Sales Org.* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputSalesOrg" type="text" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSalesOrg" Text="Sales Org. Desc" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl4" Text="Distr.Chl* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputDistrChl" type="text" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblDistrChl" Text="Distr. Chl. Desc" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl1" Text="Plant* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputPlant" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPlant" Text="Plant Desc" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl2" Text="Stor. Location " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputStorLoc" type="text" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblStorLoc" Text="Stor Loc Desc" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP 1 : LOT Size Data-->
                            <fieldset runat="server" visible="false" id="MRP1LOTSizeDt" style="width: 100%">
                                <legend>MRP 1 : LOT Size Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label33" Text="Min. LOT Size " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMinLotSize" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label34" Text="Round. Value " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputRoundValue" type="text" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP2 Procurement-->
                            <fieldset runat="server" visible="false" id="MRP2Proc" style="width: 100%">
                                <legend>MRP2 - Procurement</legend>
                                <table style="margin: auto auto 5px 7px; width: initial">
                                    <tr runat="server" id="trCoProd" visible="false">
                                        <td style="width: 160px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label30" Text="Co-Product " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxCoProd" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 130px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label35" Text="Procurement Type* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputProcType" type="text" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: auto">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProcType" Text="No Procurement" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label31" Text="Special Procurement " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputSpcProcRnd" type="text" autocomplete="off" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnSpcProc">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSpcProcRnd" Text="Spc. Proc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr style="height: initial;">
                        <!--Other Data-->
                        <td colspan="2">
                            <fieldset style="width: 100%; height: initial">
                                <legend>Other Data - Unit Measurement</legend>
                                <!--Input Table-->
                                <!--Repeater Item-->
                                <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Repeater ID="reptUntMeas" runat="server" Visible="true">
                                                <HeaderTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td colspan="9">
                                                                <b>
                                                                    <center>Other Data - Unit Measurement Details</center>
                                                                </b>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td style="width: 100px">
                                                                <b>Gross Weight</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Weight Unit</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Volume</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Volume Unit</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>X</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Aun</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Y</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Bun</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #EBEFF0">
                                                            <asp:Label CssClass="hiddenLabel" ID="lblDetailID" runat="server" Text='<%#Eval("DetailID") %>' />
                                                            <asp:Label CssClass="hiddenLabel" ID="lblOtrTransID" runat="server" Text='<%#Eval("TransID") %>' />
                                                            <asp:Label CssClass="hiddenLabel" ID="lblOtrMaterialID" runat="server" Text='<%#Eval("MaterialID") %>' />
                                                            <!--Gross Weight-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Weight Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Volume-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrVolume" runat="server" Text='<%#Eval("Volume") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtVolume" runat="server" Text='<%#Eval("Volume") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Vol Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--X-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblX" runat="server" Text='<%#Eval("X") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtX" runat="server" Text='<%#Eval("X") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Aun-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblAun" runat="server" Text='<%#Eval("Aun") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtAun" runat="server" Text='<%#Eval("Aun") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--AunMeas-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none;">
                                                                <asp:Label ID="lblAMeas" runat="server" Text="" />
                                                            </td>
                                                            <!--Y-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblY" runat="server" Text='<%#Eval("Y") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtY" runat="server" Text='<%#Eval("Y") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Bun-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblBun" runat="server" Text='<%#Eval("Bun") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtBun" runat="server" Text='<%#Eval("Bun") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--BunMeas-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none">
                                                                <asp:Label ID="lblBMeas" runat="server" Text="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblPageStatus" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <asp:Label runat="server" ID="req" Text="* (required)" ForeColor="Red" />
                        </td>
                    </tr>
                </table>
                <asp:Image runat="server" ID="revisionRnDImg" ImageUrl="~/Images/revision (1).png" Visible="false" CssClass="revisionImage" />
                <span style="float: right; padding-right: 10px; padding-bottom: 10px">
                    <button runat="server" id="btnRevisionRnD" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalRejectRND" visible="false">Revision</button>
                    <%--<asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnRevisionRnD" Text="Revision" OnClick="RevisionRnD_Click" Visible="false" />--%>
                </span>
            </fieldset>
        </div>

        <!--PROC REPORT-->
        <div runat="server" id="procTBL" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset>
                <legend style="padding: 0px 2px 0px 2px">Request New Material - Procurement</legend>
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset runat="server" visible="false" id="bscDt1GnrlDtProc" style="width: 100%;">
                                <legend>General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label36" Text="Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputTypeProc" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label37" Text="Material Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatTypeProc" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label38" Text="Material ID " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatIDProc" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt" style="width: 250px">
                                            <asp:Label runat="server" ID="Label39" Text="Material Description " />
                                        </td>
                                        <td colspan="2" class="txtBox" style="text-align: left">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRODESC" runat="server" ID="inputMatDescProc" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label40" Text="Base Unit of Meas. " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputUoMProc" type="text" ReadOnly="true" />
                                        </td>
                                        <td style="width: 170px">
                                            <asp:Label runat="server" ID="lblUoMProc" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label42" Text="Old Material Number " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputOldMatNumProc" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label112" Text="Plant " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputProcPlant" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label23" Text="Packaging Material* " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPckgMatProc" type="text" placeholder="Packaging Material*" autocomplete="off" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 170px" runat="server" id="imgBtnPckgMat">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPckgMatProc" Text="Packaging Mat Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Purchasing - Purchasing Value & Order Data-->
                            <fieldset runat="server" visible="false" id="purchValNOrder" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Purchasing - Purchasing Value & Order Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label52" Text="Purchasing Group " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPurcGrp" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPurcGrp" Text="Purchasing Group" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label43" Text="Purc. Val. Key " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPurcValKey" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label44" Text="GR Proc. Time " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputGRProcTimeMRP1" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label runat="server" ID="lblGRProcTimeMRP1" Text="DAY" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trPlantDeliveryTime" visible="false">
                                        <td class="lblTd" style="width: 150px">
                                            <asp:Label runat="server" ID="Label67" Text="Planned Delivery Time " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPlantDeliveryTime" type="text" placeholder="Plant Delivery Time" autocomplete="off" onkeypress="return this.value.length<=9" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label runat="server" ID="lblPlantDeliveryTime" Text="DAYS" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label45" Text="Mfr Part Num. " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMfrPrtNum" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP 1 : LOT Size Data-->
                            <fieldset runat="server" visible="false" id="MRPLotSize" style="width: 100%;">
                                <legend>MRP 1 : LOT Size Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label46" Text="Min. LOT Size " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMinLotSizeProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label47" Text="Round. Value " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputRoundValueProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                            <!--Basic Data 1 : Dimension/EAN-->
                            <fieldset runat="server" visible="false" id="BscDtDimension" style="width: 100%">
                                <legend>Basic Data 1 - Dimension/EAN</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 105px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label49" Text="Net Weight* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputNetWeightProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="Label53" Text="Net Weight Unit " />
                                        </td>
                                        <td>
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputNetWeightUnitProc" type="text" placeholder="Net Weight Unit" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblNetWeightUnitProc" Text="Net Weight Unit Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Foreign Trade Import : Foreign Trade Data-->
                            <fieldset runat="server" visible="false" id="ForeignTradeData" style="width: 100%">
                                <legend>Foreign Trade Import - Foreign Trade Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label55" Text="Comm/Imp Code No " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputCommImpCodeProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="Label56" Text="Comm/Omp Code No. Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Plant Data / Stor 1 : Shelf Life Data-->
                            <fieldset runat="server" visible="false" id="PlantShelfLifeDtProc" style="width: 100%">
                                <legend>Plant Data / Stor 1 - Shelf Life Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label57" Text="Min. Rem. Shelf Life " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMinRemShLfProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label runat="server" ID="lblMinRemShelfLifeProc" Text="DAY" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label54" Text="Period Ind. for SLED " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:DropDownList runat="server" ID="ddListSLEDProc" type="text" placeholder="Period Ind. for SLED" CssClass="txtBoxRO">
                                                <asp:ListItem Text="D" Value="D" Selected="True" />
                                                <asp:ListItem Text="M" Value="M" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label59" Text="Total Shelf Life* " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputTotalShelfLifeProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label runat="server" ID="Label60" Text="DAY" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Sales Data : Sales Data-->
                            <fieldset runat="server" visible="false" id="SalesData" style="width: 100%">
                                <legend>Sales Data - Sales Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label61" Text="Loading Group " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputLoadingGrp" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLoadingGrp" Text="Loading Group Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr style="height: initial;">
                        <!--Other Data-->
                        <td colspan="2">
                            <fieldset style="width: 100%; height: initial">
                                <legend>Other Data - Unit Measurement</legend>
                                <!--Input Table-->
                                <!--Repeater Item-->
                                <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Repeater ID="reptUntMeasProc" runat="server" Visible="true">
                                                <HeaderTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td colspan="9">
                                                                <b>
                                                                    <center>Other Data - Unit Measurement Details</center>
                                                                </b>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td style="width: 100px">
                                                                <b>Gross Weight</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Weight Unit</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Volume</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Volume Unit</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>X</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Aun</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Y</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Bun</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #EBEFF0">
                                                            <asp:Label CssClass="hiddenLabel" ID="lblDetailID" runat="server" Text='<%#Eval("DetailID") %>' />
                                                            <asp:Label CssClass="hiddenLabel" ID="lblOtrTransID" runat="server" Text='<%#Eval("TransID") %>' />
                                                            <asp:Label CssClass="hiddenLabel" ID="lblOtrMaterialID" runat="server" Text='<%#Eval("MaterialID") %>' />
                                                            <!--Gross Weight-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Weight Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Volume-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrVolume" runat="server" Text='<%#Eval("Volume") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtVolume" runat="server" Text='<%#Eval("Volume") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Vol Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--X-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblX" runat="server" Text='<%#Eval("X") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtX" runat="server" Text='<%#Eval("X") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Aun-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblAun" runat="server" Text='<%#Eval("Aun") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtAun" runat="server" Text='<%#Eval("Aun") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--AunMeas-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none;">
                                                                <asp:Label ID="lblAMeas" runat="server" Text="" />
                                                            </td>
                                                            <!--Y-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblY" runat="server" Text='<%#Eval("Y") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtY" runat="server" Text='<%#Eval("Y") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--Bun-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblBun" runat="server" Text='<%#Eval("Bun") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtBun" runat="server" Text='<%#Eval("Bun") %>' Visible="false" Width="100px" />
                                                            </td>
                                                            <!--BunMeas-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none">
                                                                <asp:Label ID="lblBMeas" runat="server" Text="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label27" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <asp:Label runat="server" ID="Label29" Text="* (required)" ForeColor="Red" />
                        </td>
                    </tr>
                </table>
                <asp:Image runat="server" ID="revisionProcImg" ImageUrl="~/Images/revision (1).png" Visible="false" CssClass="revisionImage" />
                <span style="float: right; padding-right: 10px; padding-bottom: 10px">
                    <button runat="server" id="btnRevisionProc" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalRejectProc" visible="false">Revision</button>
                    <%--<asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnRevisionProc" Text="Revision" OnClick="RevisionProc_Click" Visible="false" />--%>
                </span>
            </fieldset>
        </div>

        <!--Planner Report-->
        <div runat="server" id="plannerTbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: auto">
                <legend style="padding: 0px 2px 0px 2px">Request New Material - Planner</legend>
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset runat="server" visible="false" id="bscDt1GnrlDtPlanner" style="width: 100%;">
                                <legend>General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label62" Text="Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputTypePlanner" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label63" Text="Material Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatTypePlanner" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label64" Text="Material ID* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatIDPlanner" type="text" autocomplete="off" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label65" Text="Material Description* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMaterialDescPlanner" type="text" required="true" autocomplete="off" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label66" Text="Base Unit of Meas.* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputUoMPlanner" type="text" required="true" autocomplete="off" ReadOnly="true" />
                                        </td>
                                        <td colspan="2" style="width: 160px">
                                            <asp:Label runat="server" ID="lblUoMPlanner" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label68" Text="Old Material Number " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputOldMatNumbPlanner" type="text" autocomplete="off" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label113" Text="Plant " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputPlannerPlant" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label69" Text="Lab/Office " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputLabOffice" type="text" ReadOnly="true" />
                                        </td>
                                        <td style="width: 160px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLabOffice" Text="Lab/Office Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP 1-->
                            <fieldset runat="server" id="MRP1" visible="false" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>MRP1</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label70" Text="MRP Group " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputMRPGr" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPGr" Text="MRP Group Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label71" Text="MRP Type " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputMRPTyp" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPTyp" Text="MRP Type Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label72" Text="MRP Controller " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputMRPCtrl" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPCtrl" Text="MRP Controller Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="spcLblLOTSize" Text="LOT Size " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputLOTSize" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLOTSize" Text="Lot Size Desc." />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trFixLotSize">
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label99" Text="Fix LOT Size " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputFixLotSize" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label73" Text="Max. Stock Lv. " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputMaxStockLv" type="text" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                            <!--MRP 2-->
                            <fieldset runat="server" id="MRP2" visible="false" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>MRP2</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 160px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label58" Text="Procurement Type " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProcTypePlanner" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td colspan="2" style="width: auto" runat="server" id="imgBtnProcType">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProcTypePlanner" Text="No Procurement" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label75" Text="Special Procurement " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputSpcProc" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSpcProc" Text="Spc. Proc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label76" Text="Prod. Stor. Location " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputProdStorLoc" type="text" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdStorLoc" Text="Prod. Stor. Location" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label77" Text="SchedMargin Key " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputSchedMargKey" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right;">
                                            <asp:Label runat="server" ID="Label78" Text="Safety Stock " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputSftyStck" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label79" Text="Min. Safety Stock " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputMinSftyStck" type="text" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP 3-->
                            <fieldset runat="server" id="MRP3" visible="false" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>MRP3</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label80" Text="Strategy Group " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputStrtgyGr" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label107" Text="Total Lead Time " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox runat="server" ID="inputTotalLeadTime" type="text" autocomplete="off" onkeypress="return this.value.length<=14" onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" ReadOnly="true" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorTotalLeadTime" runat="server" TargetControlID="inputTotalLeadTime" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="Label117" Text="Days" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Working Schedule-->
                            <fieldset runat="server" id="WorkingSchedule" visible="true" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Working Schedule</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label74" Text="Production Scheduler " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProdSched" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdSched" Text="Production scheduler Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label86" Text="Production Scheduler Profile " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProdSchedProfile" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdSchedProfile" Text="Production Scheduler Profile Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <asp:Image runat="server" ID="revisionPlannerImg" ImageUrl="~/Images/revision (1).png" Visible="false" CssClass="revisionImage" />
                <span style="float: right; padding-right: 10px; padding-bottom: 10px">
                    <button runat="server" id="btnRevisionPlanner" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalRejectPlanner" visible="false">Revision</button>
                    <%--<asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnRevisionPlanner" Text="Revision" OnClick="RevisionPlanner_Click" Visible="false" />--%>
                </span>
            </fieldset>
        </div>

        <!--QC REport-->
        <div runat="server" id="QCTbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset runat="server" id="Fieldset1" visible="true" style="width: auto">
                <legend style="padding: 0px 2px 0px 2px">Request New Material - Quality Control</legend>
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset style="width: 100%;">
                                <legend>General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label81" Text="Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputTypeQC" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label82" Text="Material Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatTypeQC" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label83" Text="Material ID* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatIDQC" type="text" autocomplete="off" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label84" Text="Material Description* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMaterialDescQC" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label85" Text="Base Unit of Meas.* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputUoMQC" type="text" ReadOnly="true" />
                                        </td>
                                        <td colspan="2" style="width: 160px">
                                            <asp:Label runat="server" ID="lblUoMQC" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label87" Text="Old Material Number " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputOldMatNumbQC" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label114" Text="Plant " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputQCPlant" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Quality Management - Quality Control Data-->
                            <fieldset style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Quality Management - Quality Control Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label88" Text="Inspect. Setup " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxInspectSet" AutoPostBack="true" Checked="false" Enabled="false" />
                                        </td>
                                        <td colspan="2" style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblInspectSet" Text="Active / Non Active" Width="180px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label91" Text="Inspect. Interval* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputInspectIntrv" type="text" CssClass="txtBoxRO" ReadOnly="true" />
                                        </td>
                                        <td colspan="2" style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblInspectIntrv" Text="DAY" Width="180px" />
                                        </td>
                                    </tr>
                                </table>
                                <!--Inspect Interval Detail-->
                                <!--Repeater Item-->
                                <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Repeater ID="rptInspectionType" runat="server">
                                                <HeaderTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td style="width: 100px">
                                                                <b>Inspection Type</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #EBEFF0">
                                                            <!--IDClassType-->
                                                            <asp:Label ID="lblIDInspectionTypeTbl" CssClass="hiddenLabel" runat="server" Text='<%#Eval("IDInspectionType") %>' />
                                                            <asp:Label ID="lblInspectionTypeTransID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("TransID") %>' />
                                                            <asp:Label ID="lblInspectionTypeMaterialID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("MaterialID") %>' />
                                                            <!--ClassTyp-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblInspectType" runat="server" Text='<%#Eval("InspType") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblPageStatusInspectType" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Class and Class Type Input Table-->
                            <fieldset style="left: auto; top: auto; width: 100%; height: auto; right: auto; bottom: auto;">
                                <legend style="padding: 0px 2px 0px 2px">Classification</legend>
                                <!--Repeater Item-->
                                <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Repeater ID="rptClassType" runat="server">
                                                <HeaderTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td style="width: 100px">
                                                                <b>Class Type</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Class</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #EBEFF0">
                                                            <!--IDClassType-->
                                                            <asp:Label ID="lblIDClassnTypTbl" CssClass="hiddenLabel" runat="server" Text='<%#Eval("IDClassType") %>' />
                                                            <asp:Label ID="lblClassTransID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("TransID") %>' />
                                                            <asp:Label ID="lblClassMaterialID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("MaterialID") %>' />
                                                            <!--ClassTyp-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblClassTypTbl" runat="server" Text='<%#Eval("ClassType") %>' />
                                                            </td>
                                                            <!--Class-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblClassTbl" runat="server" Text='<%#Eval("ClassNo") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label92" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <asp:Image runat="server" ID="revisionQCImg" ImageUrl="~/Images/revision (1).png" Visible="false" CssClass="revisionImage" />
                <span style="float: right; padding-right: 10px; padding-bottom: 10px">
                    <button runat="server" id="btnRevisionQC" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalRejectQC" visible="false">Revision</button>
                    <%--<asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnRevisionQC" Text="Revision" OnClick="RevisionQC_Click" Visible="false" />--%>
                </span>
            </fieldset>
        </div>

        <!--QA Report-->
        <div runat="server" id="QATbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: auto">
                <legend style="padding: 0px 2px 0px 2px">Request New Material - Quality Assurance</legend>
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset runat="server" visible="true" id="Fieldset2" style="width: 100%;">
                                <legend>General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label93" Text="Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputTypeQA" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label94" Text="Material Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatTypeQA" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label95" Text="Material ID* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatIDQA" type="text" Required="true" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label96" Text="Material Description* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMaterialDescQA" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label97" Text="Base Unit of Meas.* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputUoMQA" type="text" ReadOnly="true" />
                                        </td>
                                        <td colspan="2" style="width: 160px">
                                            <asp:Label runat="server" ID="lblUoMQA" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label100" Text="Old Material Number " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputOldMatNumbQA" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label115" Text="Plant " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputQAPlant" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Quality Management - Quality Control Data-->
                            <fieldset runat="server" visible="true" id="qmQualityControllDt" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Plant Data / Store 1 - Genereal Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label101" Text="Storage Condition " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputStoreCond" type="text" CssClass="txtBoxRO" ReadOnly="true" />
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblStoreCond" Text="Condition Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <asp:Image runat="server" ID="revisionQAImg" ImageUrl="~/Images/revision (1).png" Visible="false" CssClass="revisionImageQAQC" />
                <span style="float: right; padding-right: 10px; padding-bottom: 10px">
                    <button runat="server" id="btnRevisionQA" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalRejectQA" visible="false">Revision</button>
                    <%--<asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnRevisionQA" Text="Revision" OnClick="RevisionQA_Click" Visible="false" />--%>
                </span>
            </fieldset>
        </div>

        <!--QR Report-->
        <div runat="server" id="QRTbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset runat="server" id="Fieldset3" visible="true" style="position: relative;">
                <legend style="padding: 0px 2px 0px 2px">Request New Material - Quality Regulatory</legend>
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset runat="server" visible="true" id="Fieldset4" style="width: 100%;">
                                <legend>General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label102" Text="Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputTypeQR" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label103" Text="Material Type " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatTypeQR" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label104" Text="Material ID* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMatIDQR" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label105" Text="Material Description* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputMaterialDescQR" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label106" Text="Base Unit of Meas.* " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputUoMQR" type="text" ReadOnly="true" />
                                        </td>
                                        <td colspan="2" style="width: 160px">
                                            <asp:Label runat="server" ID="lblUoMQR" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label108" Text="Old Material Number " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputOldMatNumbQR" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label116" Text="Plant " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputQRPlant" type="text" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Quality Management - Procurement Data-->
                            <fieldset runat="server" visible="true" id="QMProcData" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Quality Management - Procurement Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label109" Text="QM Procurement Active " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxQMProcActive" AutoPostBack="true" Enabled="false" />
                                            <asp:Label CssClass="lblChkBox" runat="server" ID="lblQMProcActive" Text="Non Active" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px; text-align: right;">
                                            <asp:Label runat="server" ID="Label110" Text="QM Control Key " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputQMCtrlKey" type="text" CssClass="txtBoxRO" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <asp:Image runat="server" ID="revisionQRImg" ImageUrl="~/Images/revision (1).png" Visible="false" CssClass="revisionImageQAQC" />
                <span style="float: right; padding-right: 10px; padding-bottom: 10px">
                    <%--<asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnRevisionQR" Text="Revision" OnClick="RevisionQR_Click" Visible="false" />--%>
                    <button runat="server" id="btnRevisionQR" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalRejectQR" visible="false">Revision</button>
                </span>
            </fieldset>
        </div>

        <!--Submit & Cancel Button-->
        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px 12px 5px auto">
            <tr>
                <td>
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnUpdate" Text="Update" OnClick="Update_Click" Visible="false" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelUpd" Text="Cancel" OnClick="CancelUpd_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnApprove" Text="Approve" OnClick="Approve_Click" Visible="false" UseSubmitBehavior="false" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelApprove" Text="Cancel" OnClick="CancelApprove_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="Save_Click" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelSave" Text="Cancel" OnClick="CancelSave_Click" CausesValidation="False" UseSubmitBehavior="false" />
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" CausesValidation="False" UseSubmitBehavior="false" Visible="false" />

                </td>
            </tr>
        </table>
    </fieldset>
    <!--LightBox Things-->
    <div class="container">
        <!-- Modal -->
        <!--Profit Center Modal-->
        <div class="modal fade" id="modalProfitCent" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Profit Center</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewProfitCent" runat="server"
                            DataKeyNames="ProfitCenter, ProfitCenterDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="ProfitCenter" HeaderText="ProfitCenter" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ProfitCenterDesc" HeaderText="Description" ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="190">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectProfitCent_Click">Select This Profit Center</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!--Valuation Class Modal-->
        <div class="modal fade" id="modalValClass" role="dialog">
            <div class="modal-dialog" style="max-width: 800px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Valuation Class</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewValClass" runat="server"
                            DataKeyNames="ValuationClass, AcctCatRef, ValuationClassDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="ValuationClass" HeaderText="Valuation Class" ItemStyle-Width="150" />
                                <asp:BoundField DataField="AcctCatRef" HeaderText="AcctCatRef" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ValuationClassDesc" HeaderText="Description" ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="190">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectValClass_Click">Select This Valuation Class</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!--Revision R&D Type Modal-->
        <div class="modal fade" id="modalRejectRND" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Revision Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="Label119" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReasonRnd" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact FICO Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="inputRejectReasonRnd" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnRND" Text="Revision" OnClick="RevisionRnD_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <!--Revision Proc Type Modal-->
        <div class="modal fade" id="modalRejectProc" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Revision Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="Label120" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReasonProc" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact FICO Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="inputRejectReasonProc" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnProc" Text="Revision" OnClick="RevisionProc_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <!--Revision Planner Type Modal-->
        <div class="modal fade" id="modalRejectPlanner" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Revision Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="Label121" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReasonPlanner" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact FICO Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="inputRejectReasonPlanner" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnPlanner" Text="Revision" OnClick="RevisionPlanner_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <!--Revision QC Type Modal-->
        <div class="modal fade" id="modalRejectQC" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Revision Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="Label122" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReasonQC" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact FICO Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="inputRejectReasonQC" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnQC" Text="Revision" OnClick="RevisionQC_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <!--Revision QA Type Modal-->
        <div class="modal fade" id="modalRejectQA" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Revision Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="Label123" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReasonQA" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact FICO Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="inputRejectReasonQA" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnQA" Text="Revision" OnClick="RevisionQA_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <!--Revision QR Type Modal-->
        <div class="modal fade" id="modalRejectQR" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Revision Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="lblRejectReason" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReasonQR" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact FICO Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorRejectReason" runat="server" TargetControlID="inputRejectReasonQR" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnQR" Text="Revision" OnClick="RevisionQR_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <!--Revision FICO Type Modal-->
        <div class="modal fade" id="modalRejectFICO" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Revision Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="Label118" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReasonFICO" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact FICO Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="inputRejectReasonFICO" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnFICO" Text="Revision" OnClick="RevisionFico_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
