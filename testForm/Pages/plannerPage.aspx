<%@ Page Title="Indesso Planner Site" Language="C#" AutoEventWireup="true" CodeBehind="plannerPage.aspx.cs" Inherits="testForm.Pages.plannerPage" EnableEventValidation="false" MasterPageFile="~/Pages/Site.Master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function Confirm() {
            var spcProc = document.getElementById('<%=inputSpcProc.ClientID%>').value;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (spcProc == "") {
                if (confirm("Special Procurement masih kosong, lanjutkan?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                }
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="56000" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <span runat="server" id="spanTransID" style="position: absolute; top: 100px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" /></span>
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblPosition" Text="User" />
        <asp:Label runat="server" ID="lblPlant" Text="Plant" />
        <asp:Label runat="server" ID="lblLogID" Text="LD00000000" />
        <asp:Label runat="server" ID="lblLine" Text="LN000" />
        <asp:Label runat="server" ID="lblDivision" Text="" />
        <asp:Label runat="server" ID="FilterSearch" Text="" />
    </span>
    <!--ListView-->
    <div runat="server" id="listViewPlan" style="padding: 0px 5px">
        <h1>Planner - Master Data</h1>
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
                                <asp:BoundField DataField="InitiateBy" HeaderText="Initiate By" ItemStyle-Width="120" />
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
                                <asp:TemplateField ItemStyle-Width="170">
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
                                        <asp:LinkButton runat="server" ID="slcThsMatIDDESC" Text="Modify This" OnClick="modifyThis_Click" />
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
    <div runat="server" id="rmContent" visible="false" style="position: relative;">
        <fieldset style="width: auto">
            <legend style="padding: 0px 2px 0px 2px">Request New Material - Planner</legend>
            <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                <tr style="height: initial;">
                    <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                        <!--Basic Data 1 General Data List-->
                        <fieldset runat="server" visible="false" id="bscDt1GnrlDt" style="width: 100%;">
                            <legend>General Data</legend>
                            <table style="margin: 0px auto 5px 5px; width: initial">
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label7" Text="Type " />
                                    </td>
                                    <td class="txtBox" style="text-align: left;">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputType" type="text" placeholder="Type" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label20" Text="Material Type " />
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
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputBsUntMeas" type="text" placeholder="Base Unit of Measure*" required="true" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td colspan="2" style="width: 160px">
                                        <asp:Label runat="server" ID="lblBsUntMeas" Text="Base Unit Meas. Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label4" Text="Old Material Number " />
                                    </td>
                                    <td style="text-align: left; width: 178px; height: 30px;">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputOldMtrlNum" type="text" placeholder="Old Material Number" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label11" Text="Plant " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputPlant" type="text" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label16" Text="Lab/Office " />
                                    </td>
                                    <td style="text-align: left; width: 178px; height: 30px;">
                                        <asp:TextBox CssClass="txtBox" runat="server" ID="inputLabOffice" type="text" placeholder="Lab/Office" autocomplete="off" AutoPostBack="true" OnTextChanged="inputLabOffice_onBlur" onkeypress="return this.value.length<=4" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: 34px" runat="server" id="imgBtnLabOffice">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalLabOffice"></button>
                                    </td>
                                    <td style="width: 160px">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLabOffice" Text="Lab/Office Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absLabOffice" Text="Lab/Office Desc." />
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
                                        <asp:Label runat="server" ID="lbl1" Text="MRP Group* " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputMRPGr" type="text" placeholder="MRP Group" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMRPGr_onBlur" onkeypress="return this.value.length<=3" required="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnMRPGr">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalMRPGr"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPGr" Text="MRP Group Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absMRPGr" Text="MRP Group Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="Label19" Text="MRP Type* " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputMRPTyp" type="text" placeholder="MRP Type" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMRPTyp_onBlur" onkeypress="return this.value.length<=9" required="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnMRPType">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalMRPTyp"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPTyp" Text="MRP Type Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absMRPTyp" Text="MRP Type Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="Label22" Text="MRP Controller* " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputMRPCtrl" type="text" placeholder="MRP Controller" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMRPCtrl_onBlur" onkeypress="return this.value.length<=9" required="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnMRPController">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalMRPCtrl"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPCtrl" Text="MRP Controller Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absMRPCtrl" Text="MRP Controller Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="spcLblLOTSize" Text="LOT Size* " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputLOTSize" type="text" placeholder="LOT Size*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputLOTSize_onBlur" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" required="true" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnLOTSize">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalLOTSize"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLOTSize" Text="LOT Size Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absLOTSize" Text="LOT Size Desc." />
                                    </td>
                                </tr>
                                <tr runat="server" id="trFixLotSize" visible="false">
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="Label6" Text="Fix LOT Size* " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputFixLotSize" type="text" placeholder="Fix LOT Size*" autocomplete="off" onkeypress="return this.value.length<=9" required="true" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorFixLotSize" runat="server" TargetControlID="inputFixLotSize" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="lbl3" Text="Max. Stock Lv. " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputMaxStockLv" type="text" placeholder="Maximum Stock Level" autocomplete="off" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorMaxStocklLv" runat="server" TargetControlID="inputMaxStockLv" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <!--Purchasing - Purchasing Value & Order Data-->
                        <fieldset runat="server" id="PurchasingValue" visible="false" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                            <legend>Purchasing - Purchasing Value & Order Data</legend>
                            <table style="margin: 0px auto 5px 5px; width: initial">
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="lblGR" Text="GR Proc. Time " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputGRProcTimeMRP1" type="text" placeholder="GR Processing Time" autocomplete="off" Text="1" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorGRProcTime" runat="server" TargetControlID="inputGRProcTimeMRP1" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                    <td colspan="2" class="lblTd" style="text-align: left">
                                        <asp:Label runat="server" ID="lblGRProcTimeMRP1" Text="DAY" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trPlantDeliveryTime">
                                    <td class="lblTd" style="width: 150px">
                                        <asp:Label runat="server" ID="Label24" Text="Planned Delivery Time " />
                                    </td>
                                    <td style="width: 105px">
                                        <asp:TextBox runat="server" ID="inputPlantDeliveryTime" type="text" placeholder="Planned Delivery Time" autocomplete="off" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorPlantDeliveryTime" runat="server" TargetControlID="inputPlantDeliveryTime" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                    <td colspan="2" style="width: 150px;">
                                        <asp:Label runat="server" ID="lblPlantDeliveryTime" Text="DAYS" />
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
                                        <asp:Label runat="server" ID="Label9" Text="Procurement Type " />
                                    </td>
                                    <td style="width: 150px" runat="server" id="imgBtnProcTypeTrue">
                                        <asp:TextBox runat="server" ID="inputProcType" type="text" placeholder="Procurement Type*" autocomplete="off" required="true" AutoPostBack="true" onkeypress="return this.value.length<=4" ReadOnly="true" CssClass="txtBoxRO" Width="200px" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td colspan="2" style="width: auto" runat="server" id="imgBtnProcType">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProcType" Text="No Procurement" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label3" Text="Special Procurement " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputSpcProc" type="text" placeholder="Special Procurement" autocomplete="off" AutoPostBack="true" OnTextChanged="inputSpcProc_onBlur" onkeypress="return this.value.length<=4" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnSpcProc">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalSpcProc"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSpcProc" Text="Spc. Proc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absSpcProc" Text="Spc. Proc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label8" Text="Prod. Stor. Location " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputProdStorLoc" type="text" placeholder="Prod. Stor. Location" autocomplete="off" AutoPostBack="true" OnTextChanged="inputProdStorLoc_onBlur" onkeypress="return this.value.length<=4" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnProdStorLoc">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProdStorLoc"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdStorLoc" Text="Prod. Stor. Location" />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absProdStorLoc" Text="Prod. Stor. Location" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label17" Text="SchedMargin Key* " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputSchedMargKey" type="text" placeholder="SchedMargin Key*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputSchedMargKey_onBlur" onkeypress="return this.value.length<=9" required="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnSchedMargKey">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalSchedMargKey"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSchedMargKey" Text="" />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absSchedMargKey" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right;">
                                        <asp:Label runat="server" ID="Label21" Text="Safety Stock " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputSftyStck" type="text" placeholder="Safety Stock" autocomplete="off" onkeypress="return this.value.length<=14" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorSftyStck" runat="server" TargetControlID="inputSftyStck" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label25" Text="Min. Safety Stock " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputMinSftyStck" type="text" placeholder="Minimum Safety Stock" autocomplete="off" onkeypress="return this.value.length<=14" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorMinSftyStck" runat="server" TargetControlID="inputMinSftyStck" FilterType="Custom, Numbers" ValidChars="0123456789" />
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
                                        <asp:Label runat="server" ID="Label5" Text="Strategy Group " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputStrtgyGr" type="text" placeholder="Strategy Group" autocomplete="off" onkeypress="return this.value.length<=14" OnTextChanged="inputStrategyGroup_onBlur" AutoPostBack="true" onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" Text="40" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnStrategyGroup" visible="false">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalStrategyGroup"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblStrategyGroup" Text="Strategy Gr. Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absStrategyGroup" Text="Strategy Gr. Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label14" Text="Total Lead Time " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputTotalLeadTime" type="text" placeholder="Total Lead Time" autocomplete="off" onkeypress="return this.value.length<=14" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorTotalLeadTime" runat="server" TargetControlID="inputTotalLeadTime" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="Label15" Text="Days" />
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
                                        <asp:Label runat="server" ID="Label12" Text="Production Scheduler " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputProdSched" type="text" placeholder="Production scheduler" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSched_TextChanged" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnProdSched">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProdSched"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdSched" Text="Production scheduler Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absProdSched" Text="Production scheduler Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label13" Text="Production Scheduler Profile " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputProdSchedProfile" type="text" placeholder="Production scheduler" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSchedProfile_TextChanged" ReadOnly="true" CssClass="txtBoxRO" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnProcSchedProfile" visible="false">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProdSchedProfile"></button>
                                    </td>
                                    <td colspan="2" style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdSchedProfile" Text="Production Scheduler Profile Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absProdSchedProfile" Text="Production Scheduler Profile Desc." />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>

                <!--Submit & Cancel Button-->
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnUpdate" Text="Update" OnClick="Update_Click" Visible="false" />
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelUpdate" Text="Cancel" OnClick="CancelUpdate_Click" CausesValidation="False" UseSubmitBehavior="false" Visible="false" />
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="Save_Click" OnClientClick="Confirm()" />
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancel" Text="Cancel" OnClick="Cancel_Click" CausesValidation="False" UseSubmitBehavior="false" />
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" CausesValidation="False" UseSubmitBehavior="false" Visible="false" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <!--LightBox Things-->
    <div class="container">
        <!-- Modal -->
        <!--Strategy Group Modal-->
        <div class="modal fade" id="modalStrategyGroup" role="dialog">
            <div class="modal-dialog" style="max-width: 580px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Strategy Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewStrategyGroup" runat="server"
                            DataKeyNames="PlanStrategyGroup, PlanStrategyGrpDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="PlanStrategyGroup" HeaderText="PlanStrategyGroup" ItemStyle-Width="150" />
                                <asp:BoundField DataField="PlanStrategyGrpDesc" HeaderText="Description" ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="180">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectStrategyGroup_Click">Select Strategy Group</asp:LinkButton>
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
        <!--Lab/Office Modal-->
        <div class="modal fade" id="modalLabOffice" role="dialog">
            <div class="modal-dialog" style="max-width: 580px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Lab/Office</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewLabOffice" runat="server"
                            DataKeyNames="LabOffice, Lab_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="LabOffice" HeaderText="Lab/Office" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Lab_Desc" HeaderText="Lab Desc." ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="180">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectLabOffice_Click">Select This Lab/Office</asp:LinkButton>
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
        <!--MRP Group Modal-->
        <div class="modal fade" id="modalMRPGr" role="dialog">
            <div class="modal-dialog" style="max-width: 800px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MRP Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="mrpGRNotExist" Text="Your material Plant did not have MRP Group." Visible="false" />
                        <asp:GridView ID="GridViewMRPGr" runat="server"
                            DataKeyNames="Plant, MRPGroup, MRPGroupDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="MRPGroup" HeaderText="MRPGroup" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Plant" HeaderText="PlantID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MRPGroupDesc" HeaderText="Description" ItemStyle-Width="350" />
                                <asp:TemplateField ItemStyle-Width="200">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectMRPGr_Click">Select This MRP Group</asp:LinkButton>
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
        <!--MRP Type Modal-->
        <div class="modal fade" id="modalMRPTyp" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MRP Type</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewMRPTyp" runat="server"
                            DataKeyNames="MRPType, MRPTypeDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="MRPType" HeaderText="MRPType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MRPTypeDesc" HeaderText="MRPTypeDesc" ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="170">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectMRPTyp_Click">Select This MRP Type</asp:LinkButton>
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
        <!--MRP Controller Modal-->
        <div class="modal fade" id="modalMRPCtrl" role="dialog">
            <div class="modal-dialog" style="max-width: 900px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MRP Controller</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewMRPCtrl" runat="server"
                            DataKeyNames="Plant, MRPController, MRPControllerDesc" AutoGenerateColumns="False" BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="MRPController" HeaderText="MRPController" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Plant" HeaderText="PlantID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MRPControllerDesc" HeaderText="Description" ItemStyle-Width="350" />
                                <asp:TemplateField ItemStyle-Width="200">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectMRPCtrl_Click">Select This MRP Controller</asp:LinkButton>
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
        <!--LOT Size Modal-->
        <div class="modal fade" id="modalLOTSize" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search LOT Size</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewLOTSize" runat="server"
                            DataKeyNames="LotSize, LotSizeDesc" AutoGenerateColumns="False" BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="LotSize" HeaderText="LotSize" ItemStyle-Width="150" />
                                <asp:BoundField DataField="LotSizeDesc" HeaderText="LotSizeProcedure" ItemStyle-Width="350" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectLOTSize_Click">Select This LotSize</asp:LinkButton>
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
        <!--Special Procurement Modal-->
        <div class="modal fade" id="modalSpcProc" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Special Procurement</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewSpcProc" runat="server"
                            DataKeyNames="SpclProcurement, SpclProcDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="SpclProcurement" HeaderText="SpclProcurement" ItemStyle-Width="150" />
                                <asp:BoundField DataField="SpclProcDesc" HeaderText="Description" ItemStyle-Width="450" />
                                <asp:TemplateField ItemStyle-Width="250">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSpcProc_Click">Select This SpcProcurement</asp:LinkButton>
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
        <!--Prod. Stor. Location Modal-->
        <div class="modal fade" id="modalProdStorLoc" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Prod. Stor. Location</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewProdStorLoc" runat="server"
                            DataKeyNames="Plant, SLoc, SLoc_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="Plant" HeaderText="PlantID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="SLoc" HeaderText="Stored Location" ItemStyle-Width="150" />
                                <asp:BoundField DataField="SLoc_Desc" HeaderText="Stored Location Desc." ItemStyle-Width="350" />
                                <asp:TemplateField ItemStyle-Width="200">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectProdStorLoc_Click">Select This StorLoc</asp:LinkButton>
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
        <!--SchedMargin Key Modal-->
        <div class="modal fade" id="modalSchedMargKey" role="dialog">
            <div class="modal-dialog" style="max-width: 970px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search SchedMargin Key</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewSchedMargKey" runat="server"
                            DataKeyNames="SchedType, Plant, SchedMarginType, OpeningPeriod, FloatAfterPeriod, FloatBeforePeriod" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="SchedType" HeaderText="SchedType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Plant" HeaderText="PlantID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="SchedMarginType" HeaderText="SchedMarginType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="OpeningPeriod" HeaderText="OpeningPeriod" ItemStyle-Width="150" />
                                <asp:BoundField DataField="FloatAfterPeriod" HeaderText="FloatAfterPeriod" ItemStyle-Width="150" />
                                <asp:BoundField DataField="FloatBeforePeriod" HeaderText="FloatBeforePeriod" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="250">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSchedMargKey_Click">Select This SchedMargKey</asp:LinkButton>
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
        <!--ProdSched Modal-->
        <div class="modal fade" id="modalProdSched" role="dialog">
            <div class="modal-dialog" style="max-width: 970px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search ProdSched</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewProdSched" runat="server"
                            DataKeyNames="Plant, ProdSched, ProdSchedDesc, ProdSchedProfile, ProdSchedProfileDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ProdSched" HeaderText="ProdSched" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ProdSchedDesc" HeaderText="ProdSchedDesc" ItemStyle-Width="450" />
                                <asp:BoundField DataField="ProdSchedProfile" HeaderText="ProdSchedProfile" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ProdSchedProfileDesc" HeaderText="ProdSchedProfileDesc" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectProdSched_Click">Select ProdSched</asp:LinkButton>
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
        <!--ProdSchedProfile Modal-->
        <div class="modal fade" id="modalProdSchedProfile" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search ProdSchedProfile</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewProdSchedProfile" runat="server"
                            DataKeyNames="Plant, ProdSchedProfile, ProdSchedProfileDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ProdSchedProfile" HeaderText="ProdSchedProfile" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ProdSchedProfileDesc" HeaderText="ProdSchedProfileDesc" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectProdSchedProfile_Click">Select ProdSchedProfile</asp:LinkButton>
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
    </div>
</asp:Content>
