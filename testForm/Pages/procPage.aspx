<%@ Page Title="Indesso Procurement Site" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="procPage.aspx.cs" Inherits="testForm.Pages.procPage" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function Confirm() {
            var oldMatNum = document.getElementById('<%=inputNetWeight.ClientID%>').value;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Netweight will be change, all the remaining detail Uom will be delete. Proceed?")) {
                confirm_value.value = "Yes";
                document.getElementById('<%=dummyGrossWeight.ClientID%>').click();
            }
            else {
                confirm_value.value = "No";
                document.getElementById('<%=inputNetWeight.ClientID%>').value = document.getElementById('<%=inputNetWeight.ClientID%>').oldvalue;
                }
                document.forms[0].appendChild(confirm_value);
            }
    </script>
    <script type='text/javascript'>
        function validateFloatKeyPress(el) {
            var v = parseFloat(el.value);
            el.value = (isNaN(v)) ? '' : v.toFixed(3);
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="56000" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <span runat="server" id="spanTransID" style="position: absolute; top: 95px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" />
    </span>
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblPosition" Text="User" />
        <asp:Label runat="server" ID="lblLogID" Text="LD00000000" />
        <asp:Label runat="server" ID="lblLine" Text="LN000" />
        <asp:Label runat="server" ID="lblUpdate" Text="" />
        <asp:Label runat="server" ID="FilterSearch" Text="" />
        <asp:Label runat="server" ID="lblSort" Text="" />
        <asp:Label runat="server" ID="lblSortExpression" Text="" />
        <asp:Label runat="server" ID="lblSortDirection" Text="" />
    </span>
    <!--ListView-->
    <div runat="server" id="listViewProc" style="padding: 0px 5px">
        <h1>Procurement - Master Data</h1>
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
                        <asp:DropDownList runat="server" ID="lstbxMatIDDESC" CssClass="btn btn-info btn-sm">
                            <asp:ListItem Text="Material ID" Selected="True" />
                            <asp:ListItem Text="Material Description" />
                            <asp:ListItem Text="Plant" />
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
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowDataBound="checkStatsNGlobStats_RowDataBound" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="GridViewListView_PageIndexChanging" OnSorting="GridViewListView_Sorting">
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
                                <asp:BoundField DataField="MatType" HeaderText="MatType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" SortExpression="Plant" />
                                <asp:BoundField DataField="InitiateBy" HeaderText="Initiate By" ItemStyle-Width="150" />
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
                                <asp:TemplateField ItemStyle-Width="150">
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
                                <asp:TemplateField ItemStyle-Width="150px">
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
        <fieldset>
            <legend style="padding: 0px 2px 0px 2px">Request New Material - Procurement</legend>
            <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                <tr style="height: initial;">
                    <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                        <!--Basic Data 1 General Data List-->
                        <fieldset runat="server" visible="false" id="bscDt1GnrlDt" style="width: 100%;">
                            <legend>General Data</legend>
                            <table style="margin: 0px auto 5px 5px; width: initial">
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label16" Text="Type " />
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
                                        <asp:Label runat="server" ID="Label1" Text="Material ID " />
                                    </td>
                                    <td class="lblBsDt" style="text-align: left;">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputMtrlID" type="text" placeholder="Material ID" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt" style="width: 250px">
                                        <asp:Label runat="server" ID="Label10" Text="Material Description " />
                                    </td>
                                    <td colspan="2" class="txtBox" style="text-align: left">
                                        <asp:TextBox CssClass="txtBoxRODESC" runat="server" ID="inputMtrlDesc" type="text" placeholder="Material Description" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label2" Text="Base Unit of Meas. " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputBsUntMeas" type="text" placeholder="Base Unit of Measure" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" ID="lblBsUntMeas" Text="Base Unit Meas. Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label4" Text="Old Material Number " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputOldMtrlNum" type="text" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label7" Text="Plant " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputPlant" type="text" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label11" Text="Packaging Material* " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox runat="server" ID="inputPckgMat" type="text" placeholder="Packaging Material*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputPckgMat_onBlur" onkeypress="return this.value.length<=3" required="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: 300px" runat="server" id="imgBtnPckgMat">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPckgMat"></button>
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPckgMat" Text="Packaging Mat Desc.*" />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absPckgMat" Text="Packaging Mat Desc.*" />
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
                                        <asp:Label runat="server" ID="Label8" Text="Purchasing Group " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputPurcGrp" type="text" placeholder="Purchasing Group" autocomplete="off" AutoPostBack="True" OnTextChanged="inputPurcGrp_onBlur" onkeypress="return this.value.length<=19" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnPurcGroup">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPurcGrp"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPurcGrp" Text="Purchasing Group" />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absPurcGrp" Text="Purchasing Group" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="lbl1" Text="Purc. Val. Key " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputPurcValKey" type="text" placeholder="Purchasing Value Key" autocomplete="off" AutoPostBack="True" OnTextChanged="inputPurcValKey_onBlur" onkeypress="return this.value.length<=19" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnPurcValKey">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPurcValKey"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPurcValKey" Text="" />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absPurcValKey" Text="Purc. Val. Key. Desc." />
                                    </td>
                                </tr>
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
                                <tr runat="server" id="trPlantDeliveryTime" visible="false">
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
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="lbl2" Text="Mfr Part Num. " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputMfrPrtNum" type="text" placeholder="Mfr Part Number" autocomplete="off" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
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
                                        <asp:Label runat="server" ID="Label17" Text="Min. LOT Size " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox runat="server" ID="inputMinLotSize" type="text" placeholder="Minimum LOT Size" autocomplete="off" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorMinLOTSize" runat="server" TargetControlID="inputMinLotSize" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="Label18" Text="Round. Value " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox runat="server" ID="inputRoundValue" type="text" placeholder="Rounding Value" autocomplete="off" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorRoundValue" runat="server" TargetControlID="inputRoundValue" FilterType="Custom, Numbers" ValidChars="0123456789." />
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
                                        <asp:Label runat="server" ID="Label3" Text="Net Weight* " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputNetWeight" type="text" placeholder="Net Weight*" autocomplete="off" required="true" onkeypress="return this.value.length<=17" Text="1" onkeydown="return(event.keyCode!=13);" onfocus="this.oldvalue = this.value;" onchange="Confirm(); this.oldvalue = this.value; validateFloatKeyPress(this);" OnTextChanged="inputNetWeight_TextChanged" AutoPostBack="true" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorNetWeight" runat="server" TargetControlID="inputNetWeight" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label runat="server" ID="Label19" Text="Net Weight Unit " />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="inputNetWeightUnit" type="text" placeholder="Net Weight Unit" autocomplete="off" AutoPostBack="True" OnTextChanged="inputNetWeightUnt_onBlur" onkeypress="return this.value.length<=4" onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" />
                                    </td>
                                    <%--<td runat="server" id="Td1">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalNetWeightUnt"></button>
                                    </td>--%>
                                    <td>
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblNetWeightUnit" Text="Net Weight Unit Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absNetWeightUnit" Text="Net Weight Unit Desc." />
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
                                        <asp:Label runat="server" ID="Label12" Text="Comm/Imp Code No " />
                                    </td>
                                    <td style="width: 105px">
                                        <asp:TextBox runat="server" ID="inputCommImpCode" type="text" placeholder="HS-Code" autocomplete="off" AutoPostBack="True" OnTextChanged="inputCommImpCode_onBlur" onkeypress="return this.value.length<=14" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnCommImp">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalCommImp"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblCommImpCode" Text="Comm/Omp Code No. Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absCommImpCodeNo" Text="Comm/Imp Code No. Desc." />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <!--Plant Data / Stor 1 : Shelf Life Data-->
                        <fieldset runat="server" visible="false" id="PlantShelfLifeDt" style="width: 100%">
                            <legend>Plant Data / Stor 1 - Shelf Life Data</legend>
                            <table style="margin: auto auto 5px 5px; width: initial">
                                <tr>
                                    <td class="lblTd" style="width: 150px">
                                        <asp:Label runat="server" ID="Label9" Text="Min. Rem. Shelf Life* " />
                                    </td>
                                    <td style="width: 105px">
                                        <asp:TextBox runat="server" ID="inputMinRemShLf" type="text" placeholder="Min. Rem. Shelf Life*" autocomplete="off" onkeypress="return this.value.length<=4" required="true" Text="1" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorMinRemShelf" runat="server" TargetControlID="inputMinRemShLf" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label runat="server" ID="lblMinRemShelfLife" Text="DAY" />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:RangeValidator ID="rangeValidatorMinRemShelf"
                                            ControlToValidate="inputMinRemShLf"
                                            MinimumValue="1"
                                            MaximumValue="999"
                                            Type="Integer"
                                            EnableClientScript="true"
                                            ForeColor="Red"
                                            ErrorMessage="The value must be from 1 to 999!"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="Label22" Text="Period Ind. for SLED " />
                                    </td>
                                    <td style="width: 105px">
                                        <asp:DropDownList runat="server" ID="ddListSLED" type="text" placeholder="Period Ind. for SLED" autocomplete="off" onkeypress="return this.value.length<=1" OnSelectedIndexChanged="SLED_TextChanged" AutoPostBack="true">
                                            <asp:ListItem Text="D" Selected="True" />
                                            <asp:ListItem Text="M" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="Label13" Text="Total Shelf Life " />
                                    </td>
                                    <td style="width: 105px">
                                        <asp:TextBox runat="server" ID="inputTotalShelfLife" type="text" placeholder="Total Shelf Life" autocomplete="off" onkeypress="return this.value.length<=4" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorTotalShelfLife" runat="server" TargetControlID="inputTotalShelfLife" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label runat="server" ID="Label14" Text="DAY" />
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
                                        <asp:Label runat="server" ID="Label15" Text="Loading Group " />
                                    </td>
                                    <td style="width: 105px">
                                        <asp:TextBox runat="server" ID="inputLoadingGrp" type="text" placeholder="Loading Group" autocomplete="off" AutoPostBack="True" OnTextChanged="inputLoadingGrp_onBlur" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: auto" runat="server" id="imgBtnLoadingGrp">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalLoadingGrp"></button>
                                    </td>
                                    <td style="width: 150px; display: none">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLoadingGrp" Text="Loading Group Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absLoadingGrp" Text="Loading Group Desc." />
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
                            <table style="width: 500px; height: auto; margin: auto auto 0px auto" runat="server" id="otrInputTbl">
                                <tr>
                                    <td class="lblOtrDt" style="text-align: center; width: 70px">
                                        <asp:Label runat="server" ID="lblGrossWeight" Text="Gross Weight*" /></td>
                                    <td class="lblOtrDt" style="text-align: center; width: 70px">
                                        <asp:Label runat="server" ID="lblWeightUnit" Text="Weight Unit*" /></td>
                                    <td runat="server" id="tdVolume" style="text-align: center; width: 70px; padding-left: 38px">
                                        <asp:Label runat="server" ID="Label5" Text="Volume " /></td>
                                    <td style="text-align: center; width: 70px">
                                        <asp:Label runat="server" ID="Label6" Text="Volume Unit " /></td>
                                    <td colspan="2" class="lblOtrDt" style="text-align: center; width: 70px; padding-left: 38px">X</td>
                                    <td style="text-align: center; width: 70px;">Aun</td>
                                    <td colspan="2" class="lblOtrDt" style="text-align: center; width: 70px; padding-left: 35px">Y</td>
                                    <td class="lblOtrDt" style="text-align: center; width: 70px;">Bun</td>
                                </tr>
                                <tr>
                                    <td style="width: 70px">
                                        <asp:TextBox runat="server" ID="inputGrossWeight" type="text" autocomplete="off" required="true" onkeypress="return this.value.length<=17" Text="0" Width="100px" onkeydown="return(event.keyCode!=13);" OnTextChanged="inputGrossWeight_TextChanged" AutoPostBack="true" onchange="validateFloatKeyPress(this);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorGrossWeight" runat="server" TargetControlID="inputGrossWeight" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                        <asp:Button runat="server" ID="dummyGrossWeight" OnClick="dummyGrossWeight_Click" Style="display: none;" />
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox runat="server" ID="inputWeightUnt" type="text" placeholder="Weight Unit*" autocomplete="off" required="true" AutoPostBack="True" OnTextChanged="inputWeightUnt_onBlur" onkeypress="return this.value.length<=4" Text="-" Width="100px" ReadOnly="true" CssClass="txtBoxRO" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: 35px; height: 30px" runat="server" id="imgBtnWeightUnt" visible="false">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalWeightUnt"></button>
                                    </td>
                                    <td style="width: 150px; display: none">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblWeightUnt" Text="Weight Unit Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absWeightUnt" Text="Weight Unit Desc." />
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox runat="server" ID="inputVolume" type="text" placeholder="Volume" autocomplete="off" onkeypress="return this.value.length<=17" Text="0" Width="100px" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorVolume" runat="server" TargetControlID="inputVolume" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox runat="server" ID="inputVolUnt" type="text" placeholder="Volume Unit" autocomplete="off" AutoPostBack="True" OnTextChanged="inputVolUnt_onBlur" onkeypress="return this.value.length<=4" Width="100px" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td runat="server" id="imgBtnVolUnt">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalVolUnt"></button>
                                    </td>
                                    <td style="display: none">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="lblVolUnt" Text="Vol Unit Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="absVolUnt" Text="Vol Unit Desc." />
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox ID="inputX" runat="server" Width="100px" autocomplete="off" placeholder="X" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorX" runat="server" TargetControlID="inputX" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox ID="inputAun" runat="server" Width="50px" autocomplete="off" placeholder="Aun" AutoPostBack="true" OnTextChanged="inputAun_onBlur" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="width: 35px;" runat="server" id="imgBtnAun">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalAun"></button>
                                    </td>
                                    <td style="width: 50px; display: none">
                                        <asp:Label CssClass="absoluteLabel" ID="lblAMeas" runat="server" Width="50px" Text="Aun Desc." />
                                        <asp:Label CssClass="hiddenLabel" ID="absAMeas" runat="server" Width="50px" Text="Aun Desc." />
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox ID="inputY" runat="server" Width="100px" autocomplete="off" placeholder="Y" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorY" runat="server" TargetControlID="inputY" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                    </td>
                                    <td style="width: 50px">
                                        <asp:TextBox CssClass="txtBoxROUoM" ID="inputBun" runat="server" ReadOnly="true" autocomplete="off" placeholder="Bun" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                    <td style="display: none;">
                                        <asp:Label CssClass="lblDsc" ID="lblBMeas" runat="server" Width="50px" Text="Bun Desc"></asp:Label>
                                    </td>
                                    <td style="width: 50px">
                                        <asp:Button ID="btnAddUntMeas" class="btn btn-info btn-sm" runat="server" Text="Add" OnClick="btnAddUntMeas_Click" CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                            <!--Repeater Item-->
                            <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:Repeater ID="reptUntMeas" runat="server"
                                            OnItemCommand="rept_ItemCommand" Visible="true">
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
                                                        <td style="width: 200px">
                                                            <b>Delete</b>
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
                                                            <asp:TextBox ID="txtGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Weight Unit-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblOtrWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' />
                                                            <asp:TextBox ID="txtWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Weight Unit-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblOtrVolume" runat="server" Text='<%#Eval("Volume") %>' />
                                                            <asp:TextBox ID="txtVolume" runat="server" Text='<%#Eval("Volume") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Weight Unit-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblOtrVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' />
                                                            <asp:TextBox ID="txtVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--X-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblX" runat="server" Text='<%#Eval("X") %>' />
                                                            <asp:TextBox ID="txtX" runat="server" Text='<%#Eval("X") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Aun-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblAun" runat="server" Text='<%#Eval("Aun") %>' />
                                                            <asp:TextBox ID="txtAun" runat="server" Text='<%#Eval("Aun") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--AunMeas-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none;">
                                                            <asp:Label ID="lblAMeas" runat="server" Text="" />
                                                        </td>
                                                        <!--Y-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblY" runat="server" Text='<%#Eval("Y") %>' />
                                                            <asp:TextBox ID="txtY" runat="server" Text='<%#Eval("Y") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Bun-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblBun" runat="server" Text='<%#Eval("Bun") %>' />
                                                            <asp:TextBox ID="txtBun" runat="server" Text='<%#Eval("Bun") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--BunMeas-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none">
                                                            <asp:Label ID="lblBMeas" runat="server" Text="" />
                                                        </td>
                                                        <!--Link Button-->
                                                        <td style="vertical-align: middle; text-align: center; background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 200px" runat="server" id="imgBtnLinkButton">
                                                            <asp:Button CssClass="btn btn-info btn-sm" ID="lnkEdit" runat="server" CommandArgument='<%# Eval("TransID") %>' CommandName="edit" Text="Edit" CausesValidation="False" UseSubmitBehavior="false" Visible="false" />
                                                            <asp:Button CssClass="btn btn-info btn-sm" Text="Delete" ID="lnkDelete" runat="server" CommandArgument='<%# Eval("TransID") %>' CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete?')" CausesValidation="False" />
                                                            <asp:Button CssClass="btn btn-info btn-sm" Text="Update" ID="lnkUpdate" runat="server" CommandArgument='<%# Eval("TransID") %>' CommandName="update" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
                                                            <asp:Button CssClass="btn btn-info btn-sm" Text="Cancel" ID="lnkCancel" runat="server" CommandArgument='<%# Eval("TransID") %>' CommandName="cancel" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="reptUntMeasMgr" runat="server"
                                            OnItemCommand="rept_ItemCommand" Visible="false">
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
                                                        <asp:Label CssClass="hiddenLabel" ID="lblOtrMaterialID" runat="server" Text='<%#Eval("MaterialID") %>' /><!--Gross Weight-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblOtrGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' />
                                                            <asp:TextBox ID="txtGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Weight Unit-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblOtrWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' />
                                                            <asp:TextBox ID="txtWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' Visible="false" Width="100px" autocomplete="off" AutoPostBack="true" OnTextChanged="inputWeightUnt_onBlur" />
                                                        </td>
                                                        <!--Weight Unit-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblOtrVolume" runat="server" Text='<%#Eval("Volume") %>' />
                                                            <asp:TextBox ID="txtVolume" runat="server" Text='<%#Eval("Volume") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Weight Unit-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblOtrVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' />
                                                            <asp:TextBox ID="txtVolUnit" runat="server" Text='<%#Eval("VolUnit") %>' Visible="false" Width="100px" autocomplete="off" AutoPostBack="true" OnTextChanged="inputVolUnt_onBlur" />
                                                        </td>
                                                        <!--X-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblX" runat="server" Text='<%#Eval("X") %>' />
                                                            <asp:TextBox ID="txtX" runat="server" Text='<%#Eval("X") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Aun-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblAun" runat="server" Text='<%#Eval("Aun") %>' />
                                                            <asp:TextBox ID="txtAun" runat="server" Text='<%#Eval("Aun") %>' Visible="false" Width="100px" autocomplete="off" AutoPostBack="true" OnTextChanged="txtAun_onBlur" />
                                                        </td>
                                                        <!--AunMeas-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none;">
                                                            <asp:Label ID="lblAMeas" runat="server" Text="" />
                                                        </td>
                                                        <!--Y-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblY" runat="server" Text='<%#Eval("Y") %>' />
                                                            <asp:TextBox ID="txtY" runat="server" Text='<%#Eval("Y") %>' Visible="false" Width="100px" autocomplete="off" />
                                                        </td>
                                                        <!--Bun-->
                                                        <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                            <asp:Label ID="lblBun" runat="server" Text='<%#Eval("Bun") %>' />
                                                            <asp:TextBox ID="txtBun" runat="server" Text='<%#Eval("Bun") %>' Visible="false" Width="100px" autocomplete="off" AutoPostBack="true" OnTextChanged="txtBun_onBlur" />
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
                        <asp:Label runat="server" ID="req" Text="* (required) and Decimal using .(point)" ForeColor="Red" />
                    </td>
                </tr>


                <!--Submit & Cancel Button-->
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnUpdate" Text="Update" OnClick="Update_Click" Visible="false" />
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnReject" Text="Reject" OnClick="Reject_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="Save_Click" />
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
        <!--Aun Modal-->
        <div class="modal fade" id="modalAun" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Aun</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewAun" runat="server"
                            DataKeyNames="UoM, UnitText, UoM_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="UoM" HeaderText="Unit of Measurement" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UnitText" HeaderText="Unit Text" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UoM_desc" HeaderText="Unit of Measurement Desc." ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton8" OnClick="selectAun_Click">Select This Aun</asp:LinkButton>
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
        <!--Loading Group Modal-->
        <div class="modal fade" id="modalLoadingGrp" role="dialog">
            <div class="modal-dialog" style="max-width: 540px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Loading Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewLoadingGrp" runat="server"
                            DataKeyNames="LoadGrp, LoadGrpDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="LoadGrp" HeaderText="LoadGrp" ItemStyle-Width="150" />
                                <asp:BoundField DataField="LoadGrpDesc" HeaderText="Description" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="190">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectLoadingGrp_Click">Select This Loading Group</asp:LinkButton>
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
        <!--Purchasing Group Modal-->
        <div class="modal fade" id="modalPurcGrp" role="dialog">
            <div class="modal-dialog" style="max-width: 550px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Purchasing Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewPurcGrp" runat="server"
                            DataKeyNames="PurchGrp, PurchGrpDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="PurchGrp" HeaderText="PurchGrp" ItemStyle-Width="150" />
                                <asp:BoundField DataField="PurchGrpDesc" HeaderText="Description" ItemStyle-Width="180" />
                                <asp:TemplateField ItemStyle-Width="180">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectPurcGrp_Click">Select Purchasing Group</asp:LinkButton>
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
        <!--Purchasing Value Key Modal-->
        <div class="modal fade" id="modalPurcValKey" role="dialog">
            <div class="modal-dialog" style="max-width: 1200px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Purchasing Value Key</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewPurcValKey" runat="server"
                            DataKeyNames="PurchValueKey, FstRemind, SndRemind, TrdRemind, UnderdelTolerance, OverdelivTolerance, UnltdOverdelivery" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="PurchValueKey" HeaderText="PurchValueKey" ItemStyle-Width="150" />
                                <asp:BoundField DataField="FstRemind" HeaderText="First Reminder" ItemStyle-Width="150" />
                                <asp:BoundField DataField="SndRemind" HeaderText="Second Reminder" ItemStyle-Width="150" />
                                <asp:BoundField DataField="TrdRemind" HeaderText="Third Reminder" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UnderdelTolerance" HeaderText="Under Tolerance" ItemStyle-Width="150" />
                                <asp:BoundField DataField="OverdelivTolerance" HeaderText="Over Tolerance" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UnltdOverdelivery" HeaderText="Unlimited Tolerance" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="250">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectPurcValKey_Click">Select Purchasing Value Key</asp:LinkButton>
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
        <!--Weight Unit Modal-->
        <div class="modal fade" id="modalWeightUnt" role="dialog">
            <div class="modal-dialog" style="max-width: 680px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Weight Unit</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewWeightUnt" runat="server"
                            DataKeyNames="UoM, UnitText, UoM_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="UoM" HeaderText="Unit of Measurement" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UnitText" HeaderText="Unit Text" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UoM_Desc" HeaderText="Unit of Measurement Desc." ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="170">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectWeightUnt_Click">Select This Weight Unit</asp:LinkButton>
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
        <!--Volume Unit Modal-->
        <div class="modal fade" id="modalVolUnt" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Volume Unit</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewVolUnt" runat="server"
                            DataKeyNames="UoM, UoM_Desc, UnitText" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="UoM" HeaderText="Unit of Measurement" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UnitText" HeaderText="Unit Text" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UoM_Desc" HeaderText="Unit of Measurement Desc." ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectVolUnt_Click">Select This VolUnt</asp:LinkButton>
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
        <!--Net Weight Unit Modal-->
        <div class="modal fade" id="modalNetWeightUnt" role="dialog">
            <div class="modal-dialog" style="max-width: 680px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Net Weight Unit</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewNetWeightUnt" runat="server"
                            DataKeyNames="UoM, UnitText, UoM_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="UoM" HeaderText="Unit of Measurement" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UnitText" HeaderText="Unit Text" ItemStyle-Width="150" />
                                <asp:BoundField DataField="UoM_Desc" HeaderText="Unit of Measurement Desc." ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="170">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectNetWeightUnt_Click">Select This Net Weight Unit</asp:LinkButton>
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
        <!--Packaging Material Modal-->
        <div class="modal fade" id="modalPckgMat" role="dialog">
            <div class="modal-dialog" style="max-width: 660px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Packaging Material</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewPckgMat" runat="server"
                            DataKeyNames="MatlGrpPack, MatlGrpPack_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="MatlGrpPack" HeaderText="Material Group" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MatlGrpPack_Desc" HeaderText="Material Group Desc." ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="220">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectPckgMat_Click">Select This Packaging Material</asp:LinkButton>
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
        <!--Comm/Imp Code No Modal-->
        <div class="modal fade" id="modalCommImp" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Comm/Imp Code No.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewCommImp" runat="server"
                            DataKeyNames="ForeignTrade, ForeignDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="ForeignTrade" HeaderText="ForeignTrade" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ForeignDesc" HeaderText="Description" ItemStyle-Width="320" />
                                <asp:TemplateField ItemStyle-Width="180">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectCommImp_Click">Select This Foreign Trade</asp:LinkButton>
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
