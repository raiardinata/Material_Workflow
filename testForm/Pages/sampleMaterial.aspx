<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="sampleMaterial.aspx.cs" Inherits="testForm.Pages.sampleMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="56000" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <script type="text/javascript" language="javascript">
        function sayKeyCode(event, v) {
            var TextBox = document.getElementById('<%=inputMatID.ClientID%>');
            var MatType = document.getElementById('<%=inputMatType.ClientID%>');
            //alert(event.keyCode);
            if (event.keyCode != 8) {
                if (TextBox.value.length == 9 && MatType.value.toUpperCase() != "SFAT") {
                    var str = TextBox.value;
                    var res = str.substring(0, 8);
                    var end = str.substring(8, 9);
                    var real = res + "-" + end;
                    TextBox.value = real;
                }
                else if (TextBox.value.length == 8 && MatType.value.toUpperCase() == "SFAT") {
                    var str = TextBox.value;
                    var res = str.substring(0, 7);
                    var end = str.substring(7, 8);
                    var real = res + "-" + end;
                    TextBox.value = real;
                }
                else {
                    TextBox.value = TextBox.value;
                }
            }
        }
    </script>

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

    <script type="text/javascript" language="javascript">
        function sayKeyCode2(event, v) {
            var TextBox = document.getElementById('<%=inputMatID.ClientID%>');
            var MatType = document.getElementById('<%=inputMatType.ClientID%>');

            if (MatType.value.toUpperCase() != "SFAT") {
                if (TextBox.value.substring(10, 9) == "") {
                    TextBox.value = TextBox.value.substring(0, 8);
                }
            }
            else if (MatType.value.toUpperCase() == "SFAT") {
                if (TextBox.value.substring(9, 8) == "") {
                    TextBox.value = TextBox.value.substring(0, 7);
                }
            }

        }
    </script>

    <span runat="server" id="spanTransID" style="position: absolute; top: 95px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" />
    </span>
    <asp:Label runat="server" ID="rmsffgMenuLabel" Text="RM" CssClass="hiddenLabel" />
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
        <asp:Label runat="server" ID="lblChkbx" Text="" />
        <asp:Label runat="server" ID="lblMatID" Text="0" />
        <asp:Label runat="server" ID="lblDDPriceCtrl" Text="" />
        <asp:Label runat="server" ID="lblRevision" Text="" />
        <asp:Label runat="server" ID="idxMenu" Text="0" />
        <asp:Label runat="server" ID="lblUpdate" Text="" />
        <asp:Label runat="server" ID="FilterSearch" Text="" />
    </span>
    <!--ListView-->
    <div runat="server" id="listViewFico" style="padding: 0px 5px">
        <h1>R&D - Sample Material Master Data</h1>
        <fieldset style="padding: 5px 5px">
            <table>
                <tr>
                    <td runat="server" id="tdCreateMaterial">
                        <asp:Button runat="server" ID="createMat" Text="Create New Material" CssClass="btn btn-info btn-sm" OnClick="createNewMat_Click" />

                    </td>
                    <%--<td>
                        <asp:Button runat="server" ID="CreateMat2" Text="Create New Material 2" CssClass="btn btn-info btn-sm" OnClick="CreateMat2_Click" />
                    </td>--%>
                    <td>
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
                        <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMatIDDESC" CssClass="txtBoxDesc" AutoComplete="off" />
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
                                <asp:BoundField DataField="MatType" HeaderText="MatType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Usnam" HeaderText="Initiate By" ItemStyle-Width="150" />
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
                                        <asp:Label runat="server" ID="lblGlobalStatus" Text="R&D" />
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
                                        <asp:LinkButton runat="server" ID="slcThsMatIDDESC" Text="Modify This" OnClick="modifyThisSample_Click" Visible="false" />
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
    <fieldset runat="server" id="rmContent" visible="false" style="width: auto; height: 1000px;">
        <legend style="padding: 0px 2px 0px 2px">Request New Sample Material</legend>

        <!--REPORT MENU-->
        <div runat="server" id="divApprMn" style="height: initial; padding: 0px 15px 0px 15px" visible="false">
            <asp:Menu Orientation="Horizontal" ID="FICONavigationMenu" runat="server" OnMenuItemDataBound="NavigationMenuReport_MenuItemDataBound" OnMenuItemClick="NavigationMenuReport_OnMenuItemClick">
                <StaticSelectedStyle CssClass="selected" />
                <LevelMenuItemStyles>
                    <asp:MenuItemStyle CssClass="main_menu" />
                    <asp:MenuItemStyle CssClass="level_menu" />
                </LevelMenuItemStyles>
                <Items>
                    <asp:MenuItem Text="R&D" Value="1" />
                    <asp:MenuItem Text="Procurement" Value="2" />
                    <asp:MenuItem Text="Planner" Value="3" />
                    <asp:MenuItem Text="QC" Value="4" />
                    <asp:MenuItem Text="QA" Value="5" />
                    <asp:MenuItem Text="QR" Value="6" />
                </Items>
            </asp:Menu>
        </div>

        <!--R&D REPORT-->
        <div runat="server" id="rndTBL" visible="true" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: initial; margin-top: 10px; margin-bottom: 5px;">
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset runat="server" visible="true" id="bscDt1GnrlDt" style="width: 100%">
                                <legend>Basic Data 1 - General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label11" Text="Material Type* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMatType" type="text" placeholder="Material Type" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMatTyp_onBlur" onkeypress="return this.value.length<=5" required="true" ReadOnly="true" CssClass="txtBoxRO" />

                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnMatType" visible="false">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalMatTyp"></button>
                                            <%-- <asp:LinkButton ID="btnSearchMatType"
                                                runat="server"
                                                CssClass="btn btn-default btn-sm"
                                                OnClick="btnSearchMatType_Click">
                                                   <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                 Search
                                            </asp:LinkButton>--%>
                                        </td>
                                        <td colspan="2" style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMatType" Text="Material Type Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absMatType" Text="Material Type Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label12" Text="Material ID* " />
                                        </td>
                                        <td class="txtBox" style="text-align: left;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMatID" type="text" placeholder="Material ID*" Required="true" autocomplete="off" onkeypress="return this.value.length<=17" onkeyup="sayKeyCode(event,this.value);" onchange="sayKeyCode2(event,this.value);" />
                                        </td>
                                        <td colspan="2" style="width: 100px">
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="inputMatID" ID="MinMaxMatID" ValidationExpression="^[-_,.A-Za-z0-9]{8,18}$" runat="server" ErrorMessage="Minimum 8, maximum 18 characters required and no special characters." ForeColor="Red" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorMatID" runat="server" TargetControlID="inputMatID" FilterType="Custom, Numbers" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label13" Text="Material Description* " />
                                        </td>
                                        <td class="txtBox" style="text-align: left">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMatDesc" type="text" placeholder="Material Description" autocomplete="off" onkeypress="return this.value.length<=39" required="true" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorMatDesc" runat="server" TargetControlID="inputMatDesc" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-`~!@#$%^&*()_+=[]\{}|;:,./<>?" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label15" Text="Base Unit of Meas.* " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputBsUntMeasRnd" type="text" placeholder="Base Unit of Measure" autocomplete="off" AutoPostBack="true" OnTextChanged="inputBsUntMeas_onBlur" onkeypress="return this.value.length<=2" required="true" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnBsUntMeas">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalBsUntMeas"></button>
                                        </td>
                                        <td style="width: 170px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblBsUntMeasRnd" Text="Base Unit Meas. Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absBsUntMeasRnd" Text="Base Unit Meas. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label18" Text="Material Group* " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMatGr" type="text" placeholder="Material Group" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMatGr_onBlur" onkeypress="return this.value.length<=8" required="true" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnMatGr">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalMatGr"></button>
                                        </td>
                                        <td style="width: 170px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMatGr" Text="MatGr Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absMatGr" Text="MatGr Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label19" Text="Old Material Number " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputOldMatNum" type="text" placeholder="Old Material Number" autocomplete="off" onkeypress="return this.value.length<=24" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorOldNumb" runat="server" TargetControlID="inputOldMatNum" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label20" Text="Division* " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputDivision" type="text" placeholder="Division*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputDivision_onBlur" onkeypress="return this.value.length<=1" required="true" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnDivision">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalDivision"></button>
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblDivision" Text="Division Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absDivision" Text="Division Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label22" Text="Packaging Material* " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPckgMat" type="text" placeholder="Packaging Material*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputPckgMat_onBlur" onkeypress="return this.value.length<=3" required="true" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnPckgMat">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPckgMat"></button>
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPckgMat" Text="Packaging Mat Desc.*" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absPckgMat" Text="Packaging Mat Desc.*" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Foreign Trade Import : Foreign Trade Data-->
                            <fieldset runat="server" visible="true" id="foreignTradeDt" style="width: 100%">
                                <legend>Foreign Trade Import - Foreign Trade Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd" style="width: 150px">
                                            <asp:Label runat="server" ID="Label24" Text="Comm/Imp Code No " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputCommImpCodeRnd" type="text" placeholder="Comm/Imp Code No" autocomplete="off" AutoPostBack="True" onkeypress="return this.value.length<=24" OnTextChanged="inputCommImpCodeRnd_onBlur" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnCommImpCode">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalCommImpRnd"></button>
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblCommImpCodeRnd" Text="Comm/Imp Code No. Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absCommImpCodeNoRnd" Text="Comm/Imp Code No. Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Plant Data / Stor 1 : Shelf Life Data-->
                            <fieldset runat="server" visible="true" id="plantShelfLifeDt" style="width: 100%">
                                <legend>Plant Data / Stor 1 - Shelf Life Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd" style="width: 150px">
                                            <asp:Label runat="server" ID="Label26" Text="Min. Rem. Shelf Life* " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMinRemShLf" type="text" placeholder="Min. Rem. Shelf Life" autocomplete="off" onkeypress="return this.value.length<=14" Text="1" required="true" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorMinRemShelf" runat="server" TargetControlID="inputMinRemShLf" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                        <td style="width: auto;">
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
                                            <asp:Label runat="server" ID="Label32" Text="Period Ind. for SLED " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:DropDownList runat="server" ID="ddListSLED" type="text" placeholder="Period Ind. for SLED" autocomplete="off" onkeypress="return this.value.length<=1" OnSelectedIndexChanged="SLED_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Text="D" Value="D" Selected="True" />
                                                <asp:ListItem Text="M" Value="M" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label51" Text="Total Shelf Life " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputTotalShelfLife" type="text" placeholder="Total Shelf Life" autocomplete="off" onkeypress="return this.value.length<=14" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorTotalShelfLife" runat="server" TargetControlID="inputTotalShelfLife" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                        <td style="width: auto;">
                                            <asp:Label runat="server" ID="Label121" Text="DAY" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                            <!--Basic Data 1 : Dimension/EAN-->
                            <fieldset runat="server" visible="true" id="bscDt1Dimension" style="width: 100%">
                                <legend>Basic Data 1 - Dimension/EAN</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 105px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label28" Text="Net Weight* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox runat="server" ID="inputNetWeight" type="text" placeholder="Net Weight*" autocomplete="off" required="true" onkeypress="return this.value.length<=17" Text="1" onkeydown="return(event.keyCode!=13);" onfocus="this.oldvalue = this.value;" onchange="Confirm(); this.oldvalue = this.value; validateFloatKeyPress(this);" OnTextChanged="inputNetWeight_TextChanged" AutoPostBack="true" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorNetWeight" runat="server" TargetControlID="inputNetWeight" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label runat="server" ID="Label30" Text="Net Weight Unit " />
                                        </td>
                                        <td>
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputNetWeightUnitRnd" type="text" placeholder="Net Weight Unit" autocomplete="off" AutoPostBack="True" OnTextChanged="inputNetWeightUnt_onBlur" onkeypress="return this.value.length<=4" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <%--<td runat="server" id="imgBtnNetWeightUntRnd">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalNetWeightUntRnd"></button>
                                        </td>--%>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblNetWeightUnitRnd" Text="Net Weight Unit Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absNetWeightUnitRnd" Text="Net Weight Unit Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Basic Data 2 : Other Data-->
                            <fieldset style="width: 100%">
                                <legend>Basic Data 2 - Other Data</legend>
                                <table runat="server" id="bscDt2OtrDt" style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 105px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label31" Text="Ind. Std. Desc. " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBox" runat="server" ID="inputIndStdDesc" type="text" placeholder="Ind. Std. Desc." autocomplete="off" AutoPostBack="true" OnTextChanged="inputIndStdDesc_onBlur" onkeypress="return this.value.length<=7" />
                                        </td>
                                        <td runat="server" id="imgBtnIndStdDesc">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalIndStdDesc"></button>
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblIndStdDesc" Text="Ind. Std. Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absIndStdDesc" Text="Ind. Std. Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--Organizational Level-->
                            <fieldset runat="server" visible="true" id="orgLv" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Organizational Level - Organizational Level</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl3" Text="Sales Org.* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputSalesOrg" type="text" placeholder="Sales Org.*" required="true" autocomplete="off" AutoPostBack="true" OnTextChanged="inputSalesOrg_onBlur" onkeypress="return this.value.length<=3" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnSalesOrg">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalSalesOrg"></button>
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSalesOrg" Text="Sales Org. Desc" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absSalesOrg" Text="Sales Org. Desc" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl4" Text="Distr.Chl* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputDistrChl" type="text" placeholder="Distr.Chl*" required="true" autocomplete="off" AutoPostBack="true" OnTextChanged="inputDistrChl_onBlur" onkeypress="return this.value.length<=1" />
                                        </td>
                                        <td runat="server" id="imgBtnDistrChl">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalDistrChl"></button>
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblDistrChl" Text="Distr. Chl. Desc" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absDistrChl" Text="Distr. Chl. Desc" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl1" Text="Plant* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPlant" type="text" placeholder="Plant*" required="true" autocomplete="off" AutoPostBack="true" OnTextChanged="inputPlant_onBlur" onkeypress="return this.value.length<=3" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnPlant">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPlantID"></button>
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPlant" Text="Plant Desc" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absPlant" Text="Plant Desc" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lbl2" Text="Stor. Location* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputStorLoc" type="text" placeholder="Stor. Location" required="true" autocomplete="off" AutoPostBack="true" OnTextChanged="inputStorLoc_onBlur" onkeypress="return this.value.length<=3" />
                                        </td>
                                        <td runat="server" id="imgBtnSloc">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalStorLoc"></button>
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblStorLoc" Text="Stor Loc Desc" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absStorLoc" Text="Stor Loc Desc" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP 1 : LOT Size Data-->
                            <fieldset runat="server" visible="true" id="MRP1LOTSizeDt" style="width: 100%">
                                <legend>MRP 1 : LOT Size Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label33" Text="Min. LOT Size " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMinLotSize" type="text" placeholder="Minimum LOT Size" autocomplete="off" onkeypress="return this.value.length<=14" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorMinLotSize" runat="server" TargetControlID="inputMinLotSize" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label34" Text="Round. Value " />
                                        </td>
                                        <td class="txtBox">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputRoundValue" type="text" placeholder="Rounding Value" autocomplete="off" onkeypress="return this.value.length<=4" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorRoundValue" runat="server" TargetControlID="inputRoundValue" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP2 Procurement-->
                            <fieldset runat="server" visible="true" id="MRP2Proc" style="width: 100%">
                                <legend>MRP2 - Procurement</legend>
                                <table style="margin: auto auto 5px 7px; width: initial">
                                    <tr runat="server" id="trCoProd" visible="true">
                                        <td style="width: 160px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label35" Text="Co-Product " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxCoProd" AutoPostBack="true" OnCheckedChanged="chkbxCoProd_CheckedChanged" Checked="false" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 160px; height: 30px; text-align: right;">
                                            <asp:Label runat="server" ID="Label41" Text="Procurement Type* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProcType" type="text" placeholder="Procurement Type*" autocomplete="off" required="true" AutoPostBack="true" OnTextChanged="inputProcType_onBlur" onkeypress="return this.value.length<=4" />
                                        </td>
                                        <td style="width: 15px" runat="server" id="imgBtnProcType">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProcType"></button>
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProcType" Text="No Procurement" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absProcType" Text="No Procurement" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label50" Text="Special Procurement " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputSpcProcRnd" type="text" placeholder="Special Procurement" autocomplete="off" AutoPostBack="true" OnTextChanged="inputSpcProcRnd_onBlur" onkeypress="return this.value.length<=4" />
                                        </td>
                                        <td style="width: 15px" runat="server" id="imgBtnSpcProc">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalSpcProcRnd"></button>
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSpcProcRnd" Text="Spc. Proc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absSpcProcRnd" Text="Spc. Proc." />
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
                                        <td runat="server" id="tdVolume" style="text-align: center; width: 70px;">
                                            <asp:Label runat="server" ID="Label67" Text="Volume " /></td>
                                        <td style="text-align: center; width: 70px">
                                            <asp:Label runat="server" ID="Label107" Text="Volume Unit " /></td>
                                        <td colspan="2" class="lblOtrDt" style="text-align: center; width: 70px; padding-left: 38px">X</td>
                                        <td style="text-align: center; width: 70px;">Aun</td>
                                        <td colspan="2" class="lblOtrDt" style="text-align: center; width: 70px; padding-left: 35px">Y</td>
                                        <td class="lblOtrDt" style="text-align: center; width: 70px;">Bun</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 70px">
                                            <asp:TextBox runat="server" ID="inputGrossWeightRnd" type="text" autocomplete="off" required="true" onkeypress="return this.value.length<=17" Text="0" Width="100px" onkeydown="return(event.keyCode!=13);" OnTextChanged="inputGrossWeight_TextChanged" AutoPostBack="true" onchange="validateFloatKeyPress(this);" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorGrossWeight" runat="server" TargetControlID="inputGrossWeightRnd" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                            <asp:Button runat="server" ID="dummyGrossWeight" OnClick="dummyGrossWeight_Click" Style="display: none;" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputWeightUntRnd" type="text" autocomplete="off" required="true" AutoPostBack="True" OnTextChanged="inputWeightUntRnd_onBlur" onkeypress="return this.value.length<=4" Text="-" Width="100px" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: 35px; height: 30px" runat="server" id="imgBtnWeightUntRnd" visible="false">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalWeightUntRnd"></button>
                                        </td>
                                        <td style="width: 150px; display: none">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblWeightUntRnd" Text="Weight Unit Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absWeightUntRnd" Text="Weight Unit Desc." />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputVolumeRnd" type="text" autocomplete="off" onkeypress="return this.value.length<=17" Text="0" Width="100px" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorVolume" runat="server" TargetControlID="inputVolumeRnd" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputVolUntRnd" type="text" autocomplete="off" AutoPostBack="True" OnTextChanged="inputVolUntRnd_onBlur" onkeypress="return this.value.length<=4" Width="100px" />
                                        </td>
                                        <td runat="server" id="imgBtnVolUntRnd">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalVolUnt"></button>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblVolUntRnd" Text="Vol Unit Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absVolUntRnd" Text="Vol Unit Desc." />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="inputXRnd" runat="server" Width="100px" autocomplete="off" onkeypress="return this.value.length<=9" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorX" runat="server" TargetControlID="inputXRnd" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="inputAunRnd" runat="server" Width="50px" autocomplete="off" AutoPostBack="true" OnTextChanged="inputAunRnd_onBlur" onkeypress="return this.value.length<=9" />
                                        </td>
                                        <td style="width: 35px;" runat="server" id="imgBtnAunRnd">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalAun"></button>
                                        </td>
                                        <td style="width: 50px; display: none">
                                            <asp:Label CssClass="absoluteLabel" ID="lblAMeasRnd" runat="server" Width="50px" Text="Aun Desc." />
                                            <asp:Label CssClass="hiddenLabel" ID="absAMeasRnd" runat="server" Width="50px" Text="Aun Desc." />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="inputYRnd" runat="server" Width="100px" autocomplete="off" onkeypress="return this.value.length<=9" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorY" runat="server" TargetControlID="inputYRnd" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                        </td>
                                        <td style="width: 50px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxROUoM" ID="inputBunRnd" runat="server" ReadOnly="true" autocomplete="off" onkeypress="return this.value.length<=9" />
                                        </td>
                                        <td style="display: none;">
                                            <asp:Label CssClass="lblDsc" ID="lblBMeasRnd" runat="server" Width="50px" Text="Bun Desc"></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Button ID="btnAddUntMeasRnd" OnClick="btnAddUntMeasRnd_Click" class="btn btn-info btn-sm" runat="server" Text="Add" />
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
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Weight Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Weight Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrVolume" runat="server" Text='<%#Eval("Volume") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtVolume" runat="server" Text='<%#Eval("Volume") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Weight Unit-->
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
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtAun" runat="server" Text='<%#Eval("Aun") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--AunMeas-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none;">
                                                                <asp:Label ID="lblAMeas" runat="server" Text="" />
                                                            </td>
                                                            <!--Y-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblY" runat="server" Text='<%#Eval("Y") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtY" runat="server" Text='<%#Eval("Y") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Bun-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblBun" runat="server" Text='<%#Eval("Bun") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtBun" runat="server" Text='<%#Eval("Bun") %>' Visible="false" Width="100px" autocomplete="off" />
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
                                                            <asp:Label CssClass="hiddenLabel" ID="lblOtrMaterialID" runat="server" Text='<%#Eval("MaterialID") %>' />
                                                            <!--Gross Weight-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtGrossWeight" runat="server" Text='<%#Eval("GrossWeight") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Weight Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtWeightUnit" runat="server" Text='<%#Eval("WeightUnit") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Weight Unit-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblOtrVolume" runat="server" Text='<%#Eval("Volume") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtVolume" runat="server" Text='<%#Eval("Volume") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Weight Unit-->
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
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtAun" runat="server" Text='<%#Eval("Aun") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--AunMeas-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px; display: none;">
                                                                <asp:Label ID="lblAMeas" runat="server" Text="" />
                                                            </td>
                                                            <!--Y-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblY" runat="server" Text='<%#Eval("Y") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtY" runat="server" Text='<%#Eval("Y") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Bun-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblBun" runat="server" Text='<%#Eval("Bun") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtBun" runat="server" Text='<%#Eval("Bun") %>' Visible="false" Width="100px" autocomplete="off" />
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
            </fieldset>
        </div>

        <!--PROC REPORT-->
        <div runat="server" id="procTBL" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: initial; margin-top: 10px; margin-bottom: 5px;">
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Purchasing - Purchasing Value & Order Data-->
                            <fieldset runat="server" visible="true" id="purchValNOrder" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Purchasing - Purchasing Value & Order Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label8" Text="Purchasing Group " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPurcGrp" type="text" placeholder="Purchasing Group" autocomplete="off" AutoPostBack="True" OnTextChanged="inputPurcGrp_onBlur" onkeypress="return this.value.length<=19" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnPurchGrp">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPurcGrp"></button>
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPurcGrp" Text="Purchasing Group" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absPurcGrp" Text="Purchasing Group" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label1" Text="Purc. Val. Key " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPurcValKey" type="text" placeholder="Purchasing Value Key" autocomplete="off" AutoPostBack="True" OnTextChanged="inputPurcValKey_onBlur" onkeypress="return this.value.length<=19" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnPurchValKey">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPurcValKey"></button>
                                        </td>
                                        <td style="width: 150px;" runat="server" visible="false">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPurcValKey" Text="" />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absPurcValKey" Text="Purc. Val. Key. Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="lblGR" Text="GR Proc. Time " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputGRProcTimeMRP1" type="text" placeholder="GR Processing Time" autocomplete="off" Text="1" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorGRProcTime" runat="server" TargetControlID="inputGRProcTimeMRP1" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                        <td class="lblTd" style="text-align: left">
                                            <asp:Label runat="server" ID="lblGRProcTimeMRP1" Text="DAY" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trPlantDeliveryTime" visible="true">
                                        <td class="lblTd" style="width: 150px">
                                            <asp:Label runat="server" ID="Label3" Text="Planned Delivery Time " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPlantDeliveryTime" type="text" placeholder="Planned Delivery Time" autocomplete="off" onkeypress="return this.value.length<=9" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorPlantDeliveryTime" runat="server" TargetControlID="inputPlantDeliveryTime" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label runat="server" ID="lblPlantDeliveryTime" Text="DAYS" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label4" Text="Mfr Part Num. " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMfrPrtNum" type="text" placeholder="Mfr Part Number" autocomplete="off" onkeypress="return this.value.length<=9" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                            <!--Sales Data : Sales Data-->
                            <fieldset runat="server" visible="true" id="SalesData" style="width: 100%">
                                <legend>Sales Data - Sales Data</legend>
                                <table style="margin: auto auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label5" Text="Loading Group " />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputLoadingGrp" type="text" placeholder="Loading Group" autocomplete="off" AutoPostBack="True" OnTextChanged="inputLoadingGrp_onBlur" onkeypress="return this.value.length<=9" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnLoadGrp">
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
                </table>
            </fieldset>
        </div>

        <!--Planner Report-->
        <div runat="server" id="plannerTbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: initial; margin-top: 10px; margin-bottom: 5px;">
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Basic Data 1 General Data List-->
                            <fieldset runat="server" visible="true" id="bscDt1GnrlDtPlanner" style="width: 100%;">
                                <legend>General Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblBsDt">
                                            <asp:Label runat="server" ID="Label27" Text="Lab/Office " />
                                        </td>
                                        <td style="text-align: left; width: 178px; height: 30px;">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBox" runat="server" ID="inputLabOffice" type="text" placeholder="Lab/Office" autocomplete="off" AutoPostBack="true" OnTextChanged="inputLabOffice_onBlur" onkeypress="return this.value.length<=4" />
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
                                            <asp:Label runat="server" ID="Label14" Text="MRP Group* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMRPGr" type="text" placeholder="MRP Group" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMRPGr_onBlur" onkeypress="return this.value.length<=3" required="true" />
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
                                            <asp:Label runat="server" ID="Label16" Text="MRP Type* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMRPTyp" type="text" placeholder="MRP Type" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMRPTyp_onBlur" onkeypress="return this.value.length<=9" required="true" />
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
                                            <asp:Label runat="server" ID="Label21" Text="MRP Controller* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMRPCtrl" type="text" placeholder="MRP Controller" autocomplete="off" AutoPostBack="true" OnTextChanged="inputMRPCtrl_onBlur" onkeypress="return this.value.length<=9" required />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnMRPCont">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalMRPCtrl"></button>
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPCtrl" Text="MRP Controller Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absMRPCtrl" Text="MRP Controller Desc." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="spcLblLOTSize" Text="LOT Size " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputLOTSize" type="text" placeholder="LOT Size" autocomplete="off" AutoPostBack="true" OnTextChanged="inputLOTSize_onBlur" onkeypress="return this.value.length<=9" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnLotSize">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalLOTSize"></button>
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLOTSize" Text="LOT Size Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absLOTSize" Text="LOT Size Desc." />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trFixLotSize" visible="false">
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label23" Text="Fix LOT Size* " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputFixLotSize" type="text" placeholder="Fix LOT Size*" autocomplete="off" onkeypress="return this.value.length<=9" required="true" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorFixLotSize" runat="server" TargetControlID="inputFixLotSize" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label25" Text="Max. Stock Lv. " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMaxStockLv" type="text" placeholder="Maximum Stock Level" autocomplete="off" onkeypress="return this.value.length<=9" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorMaxStocklLv" runat="server" TargetControlID="inputMaxStockLv" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                            <!--MRP 2-->
                            <fieldset runat="server" id="MRP2" visible="true" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>MRP2</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label10" Text="Prod. Stor. Location " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProdStorLoc" type="text" placeholder="Prod. Stor. Location" autocomplete="off" AutoPostBack="true" OnTextChanged="inputProdStorLoc_onBlur" onkeypress="return this.value.length<=4" />
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
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputSchedMargKey" type="text" placeholder="SchedMargin Key*" autocomplete="off" AutoPostBack="true" OnTextChanged="inputSchedMargKey_onBlur" onkeypress="return this.value.length<=9" required="true" />
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
                                            <asp:Label runat="server" ID="Label78" Text="Safety Stock " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputSftyStck" type="text" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorSftyStck" runat="server" TargetControlID="inputSftyStck" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label79" Text="Min. Safety Stock " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMinSftyStck" type="text" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorMinSftyStck" runat="server" TargetControlID="inputMinSftyStck" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <!--MRP 3-->
                            <fieldset runat="server" id="MRP3" visible="true" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>MRP3</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td style="width: 150px; text-align: right">
                                            <asp:Label runat="server" ID="Label9" Text="Strategy Group " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputStrtgyGr" type="text" placeholder="Strategy Group" autocomplete="off" onkeypress="return this.value.length<=14" OnTextChanged="inputStrategyGroup_onBlur" AutoPostBack="true" ReadOnly="true" CssClass="txtBoxRO" Text="40" />
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
                                            <asp:Label runat="server" ID="Label39" Text="Total Lead Time " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputTotalLeadTime" type="text" placeholder="Total Lead Time" autocomplete="off" onkeypress="return this.value.length<=14" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorTotalLeadTime" runat="server" TargetControlID="inputTotalLeadTime" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                        <td style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="Label40" Text="Days" />
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
                                            <asp:Label runat="server" ID="Label6" Text="Production Scheduler " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProdSched" type="text" placeholder="Production scheduler" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSched_TextChanged" />
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
                                            <asp:Label runat="server" ID="Label7" Text="Production Scheduler Profile " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProdSchedProfile" type="text" placeholder="Production scheduler" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSchedProfile_TextChanged" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                        <td style="width: auto" runat="server" id="imgBtnProdSchedProfile" visible="false">
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
                </table>
            </fieldset>
        </div>

        <!--QC REport-->
        <div runat="server" id="QCTbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: initial; margin-top: 10px; margin-bottom: 5px;">
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Quality Management - Quality Control Data-->
                            <fieldset style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Quality Management - Quality Control Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label88" Text="Inspect. Setup " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxInspectSet" AutoPostBack="true" Checked="false" OnCheckedChanged="chkbxInspectSet_CheckedChanged" Enabled="false" />
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
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputInspectIntrv" type="text" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="validatorInspectIntrv" runat="server" TargetControlID="inputInspectIntrv" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </td>
                                        <td colspan="2" style="width: 150px;">
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblInspectIntrv" Text="DAY" Width="180px" />
                                        </td>
                                    </tr>
                                </table>
                                <!--Inspect Interval Detail-->
                                <table style="width: initial; height: auto; margin: auto auto 0px auto;" runat="server" id="InspectInputTbl">
                                    <tr>
                                        <td class="lblIDInspectionType" style="visibility: hidden; position: absolute">ID</td>
                                        <td colspan="3" style="width: auto; height: 30px; padding-left: 18px">Inspection Type</td>
                                    </tr>
                                    <tr>
                                        <td style="visibility: hidden; position: absolute">
                                            <asp:Label ID="lblInspectionType" runat="server" Width="30px" Text="0" />
                                        </td>
                                        <td>
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtInspectionType" runat="server" Width="150px" autocomplete="off" placeholder="Inspection Type" CssClass="txtBox" AutoPostBack="true" OnTextChanged="txtInspectionType_TextChanged" />
                                        </td>
                                        <td style="width: 35px; height: 30px">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalInspectionTypeTyp"></button>
                                        </td>
                                        <td style="width: auto">
                                            <asp:Button ID="btnAddInspectionType" class="btn btn-info btn-sm" runat="server" Text="Add" OnClick="btnAddInspectionType_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                        </td>
                                    </tr>
                                </table>
                                <!--Repeater Item-->
                                <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Repeater ID="rptInspectionType" runat="server"
                                                OnItemCommand="reptInspectionType_ItemCommand">
                                                <HeaderTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td style="width: 100px">
                                                                <b>Inspection Type</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Description</b>
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
                                                            <!--IDClassType-->
                                                            <asp:Label ID="lblIDInspectionTypeTbl" CssClass="hiddenLabel" runat="server" Text='<%#Eval("IDInspectionType") %>' />

                                                            <asp:Label ID="lblInspectionTypeTransID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("TransID") %>' />
                                                            <asp:Label ID="lblInspectionTypeMaterialID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("MaterialID") %>' />
                                                            <!--ClassTyp-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblInspectType" runat="server" Text='<%#Eval("InspType") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtInspectType" runat="server" Text='<%#Eval("InspType") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblInspectTypeDesc" runat="server" Text='<%#Eval("InspTypeDesc") %>' />
                                                            </td>
                                                            <!--Link Button-->
                                                            <td style="vertical-align: middle; text-align: center; background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 200px">
                                                                <asp:Button CssClass="btn btn-info btn-sm" ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IDInspectionType") %>' CommandName="edit" Text="Edit" CausesValidation="false" UseSubmitBehavior="false" Visible="false" />
                                                                <asp:Button CssClass="btn btn-info btn-sm" Text="Delete" ID="lnkDelete" runat="server" CommandArgument='<%# Eval("IDInspectionType") %>' CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete?')" />
                                                                <asp:Button CssClass="btn btn-info btn-sm" Text="Update" ID="lnkUpdate" runat="server" CommandArgument='<%# Eval("IDInspectionType") %>' CommandName="update" Visible="false" CausesValidation="false" UseSubmitBehavior="false" />
                                                                <asp:Button CssClass="btn btn-info btn-sm" Text="Cancel" ID="lnkCancel" runat="server" CommandArgument='<%# Eval("IDInspectionType") %>' CommandName="cancel" Visible="false" CausesValidation="false" UseSubmitBehavior="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater ID="rptInspectionTypeMgr" runat="server"
                                                OnItemCommand="reptInspectionType_ItemCommand" Visible="false">
                                                <HeaderTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td style="width: 100px">
                                                                <b>Inspection Type</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Description</b>
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
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtInspectType" runat="server" Text='<%#Eval("InspType") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblInspectTypeDesc" runat="server" Text='<%#Eval("InspTypeDesc") %>' />
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
                                <table style="width: initial; height: auto; margin: auto auto 0px auto;" runat="server" id="ClassInputTbl">
                                    <tr>
                                        <td class="lblOtrDt" style="visibility: hidden; position: absolute">ID</td>
                                        <td colspan="3" style="width: auto; height: 30px; padding-left: 18px">Class Type</td>
                                        <td colspan="2" style="width: auto; height: 30px; padding-left: 37px">Class  </td>
                                    </tr>
                                    <tr>
                                        <td style="visibility: hidden; position: absolute">
                                            <asp:Label ID="lblIDClassnTyp" runat="server" Width="30px" Text="0" />
                                        </td>
                                        <td>
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="inputClassTyp" runat="server" Width="100px" autocomplete="off" placeholder="Class Type" OnTextChanged="inputClassTyp_TextChanged" AutoPostBack="true" ReadOnly="true" CssClass="txtBoxRO" Text="023" />
                                        </td>
                                        <%--<td style="width: 35px; height: 30px">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalClassTyp"></button>
                                        </td>--%>
                                        <td>
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="inputClass" runat="server" Width="100px" autocomplete="off" placeholder="Class" AutoPostBack="true" OnTextChanged="inputClass_TextChanged" />
                                        </td>
                                        <td style="width: 35px; height: 30px">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalClass"></button>
                                        </td>
                                        <td style="width: auto">
                                            <asp:Button ID="btnAddUntMeas" class="btn btn-info btn-sm" runat="server" Text="Add" OnClick="btnAddClassnTyp_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <!--Repeater Item-->
                                <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Repeater ID="rptClassType" runat="server"
                                                OnItemCommand="reptClassType_ItemCommand">
                                                <HeaderTemplate>
                                                    <table style="text-align: center; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                                        <tr style="background-color: #8AE0F2; color: #484848">
                                                            <td style="width: 100px">
                                                                <b>Class Type</b>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <b>Class</b>
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
                                                            <!--IDClassType-->
                                                            <asp:Label ID="lblIDClassnTypTbl" CssClass="hiddenLabel" runat="server" Text='<%#Eval("IDClassType") %>' />
                                                            <asp:Label ID="lblClassTransID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("TransID") %>' />
                                                            <asp:Label ID="lblClassMaterialID" CssClass="hiddenLabel" runat="server" Text='<%#Eval("MaterialID") %>' />
                                                            <!--ClassTyp-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblClassTypTbl" runat="server" Text='<%#Eval("ClassType") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtClassTyp" runat="server" Text='<%#Eval("ClassType") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Class-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblClassTbl" runat="server" Text='<%#Eval("ClassNo") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtClass" runat="server" Text='<%#Eval("ClassNo") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Link Button-->
                                                            <td style="vertical-align: middle; text-align: center; background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 200px">
                                                                <asp:Button CssClass="btn btn-info btn-sm" ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IDClassType") %>' CommandName="edit" Text="Edit" CausesValidation="false" UseSubmitBehavior="false" Visible="false" />
                                                                <asp:Button CssClass="btn btn-info btn-sm" Text="Delete" ID="lnkDelete" runat="server" CommandArgument='<%# Eval("IDClassType") %>' CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete?')" />
                                                                <asp:Button CssClass="btn btn-info btn-sm" Text="Update" ID="lnkUpdate" runat="server" CommandArgument='<%# Eval("IDClassType") %>' CommandName="update" Visible="false" CausesValidation="false" UseSubmitBehavior="false" />
                                                                <asp:Button CssClass="btn btn-info btn-sm" Text="Cancel" ID="lnkCancel" runat="server" CommandArgument='<%# Eval("IDClassType") %>' CommandName="cancel" Visible="false" CausesValidation="false" UseSubmitBehavior="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater ID="rptClassTypeMgr" runat="server"
                                                OnItemCommand="reptClassType_ItemCommand" Visible="false">
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
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtClassTyp" runat="server" Text='<%#Eval("ClassType") %>' Visible="false" Width="100px" autocomplete="off" />
                                                            </td>
                                                            <!--Class-->
                                                            <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                                <asp:Label ID="lblClassTbl" runat="server" Text='<%#Eval("ClassNo") %>' />
                                                                <asp:TextBox onkeydown="return(event.keyCode!=13);" ID="txtClass" runat="server" Text='<%#Eval("ClassNo") %>' Visible="false" Width="100px" autocomplete="off" />
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
                                            <asp:Label ID="Label29" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <!--QA Report-->
        <div runat="server" id="QATbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: initial; margin-top: 10px; margin-bottom: 5px;">
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Quality Management - Quality Control Data-->
                            <fieldset runat="server" visible="true" id="qmQualityControllDt" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Plant Data / Store 1 - Genereal Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label36" Text="Storage Condition " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputStoreCond" type="text" placeholder="Storage Condition" autocomplete="off" AutoPostBack="true" OnTextChanged="inputStoreCond_onBlur" onkeypress="return this.value.length<=9" />
                                        </td>
                                        <td style="width: 35px; height: 30px" runat="server" id="imgBtnStorCond">
                                            <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalStoreCond"></button>
                                        </td>
                                        <td>
                                            <asp:Label CssClass="absoluteLabel" runat="server" ID="lblStoreCond" Text="Condition Desc." />
                                            <asp:Label CssClass="hiddenLabel" runat="server" ID="absStoreCond" Text="Condition Desc." />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <!--QR Report-->
        <div runat="server" id="QRTbl" visible="false" style="padding: 0px 15px 0px 15px">
            <fieldset style="width: initial; margin-top: 10px; margin-bottom: 5px;">
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                    <tr style="height: initial;">
                        <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                            <!--Quality Management - Procurement Data-->
                            <fieldset runat="server" visible="true" id="QMProcData" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                <legend>Quality Management - Procurement Data</legend>
                                <table style="margin: 0px auto 5px 5px; width: initial">
                                    <tr>
                                        <td class="lblTd">
                                            <asp:Label runat="server" ID="Label37" Text="QM Procurement Active " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:CheckBox runat="server" ID="chkbxQMProcActive" AutoPostBack="true" OnCheckedChanged="chkbxQMProcActive_CheckedChanged" />
                                            <asp:Label CssClass="lblChkBox" runat="server" ID="lblQMProcActive" Text="Non Active" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px; text-align: right;">
                                            <asp:Label runat="server" ID="Label38" Text="QM Control Key " />
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputQMCtrlKey" type="text" placeholder="QM Control Key" autocomplete="off" AutoPostBack="true" onkeypress="return this.value.length<=4" ReadOnly="true" CssClass="txtBoxRO" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <!--Submit & Cancel Button-->
        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px 12px 5px auto">
            <tr>
                <td>
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnUpdate" Text="Update" OnClick="Update_Click" Visible="false" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelUpd" Text="Cancel" OnClick="CancelUpd_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />

                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnApprove" Text="Approve" OnClick="Approve_Click" Visible="false" UseSubmitBehavior="false" />
                    <button runat="server" id="btnReject" class="btn btn-info btn-danger" type="button" data-toggle="modal" data-target="#modalReject" visible="false">Reject</button>
                    <%--<asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnReject" Text="Reject" OnClick="Reject_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />--%>
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

        <!--Rnd Region-->
        <!--Ind. Std. Desc. Modal-->
        <div class="modal fade" id="modalIndStdDesc" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Ind. Std. Desc.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewIndStdDesc" runat="server"
                            DataKeyNames="IndStdCode, IndStdDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="IndStdCode" HeaderText="IndStdCode" ItemStyle-Width="150" />
                                <asp:BoundField DataField="IndStdDesc" HeaderText="Description" ItemStyle-Width="450" />
                                <asp:TemplateField ItemStyle-Width="250">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectIndStdDesc_Click">Select Ind. Std.</asp:LinkButton>
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
        <div class="modal fade" id="modalSpcProcRnd" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Special Procurement</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewSpcProcRnd" runat="server"
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
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSpcProcRnd_Click">Select This SpcProcurement</asp:LinkButton>
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
        <div class="modal fade" id="modalCommImpRnd" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Comm/Imp Code No.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewCommImpRnd" runat="server"
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
                                <asp:BoundField DataField="ForeignDesc" HeaderText="ForeignDesc" ItemStyle-Width="350" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectCommImpRnd_Click">Select This Plant</asp:LinkButton>
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
        <div class="modal fade" id="modalVolUntRnd" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Volume Unit</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewVolUntRnd" runat="server"
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
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectVolUntRnd_Click">Select This VolUnt</asp:LinkButton>
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
        <div class="modal fade" id="modalWeightUntRnd" role="dialog">
            <div class="modal-dialog" style="max-width: 680px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Weight Unit</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewWeightUntRnd" runat="server"
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
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectWeightUntRnd_Click">Select This Weight Unit</asp:LinkButton>
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
        <div class="modal fade" id="modalNetWeightUntRnd" role="dialog">
            <div class="modal-dialog" style="max-width: 680px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Net Weight Unit</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewNetWeightUntRnd" runat="server"
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
                                <asp:TemplateField ItemStyle-Width="220">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectNetWeightUntRnd_Click">Select This Net Weight Unit</asp:LinkButton>
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
        <!--Material Type Modal-->
        <div class="modal fade" id="modalMatTyp" role="dialog">
            <div class="modal-dialog" style="max-width: 940px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Material Type</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewMatTyp" runat="server"
                            DataKeyNames="MatType, MatTypeDesc, RefMatType, AuthorizationGroup, ItemCategoryGroup" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="MatType" HeaderText="Material Type" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MatTypeDesc" HeaderText="Material Type Desc." ItemStyle-Width="190" />
                                <asp:BoundField DataField="RefMatType" HeaderText="Ref. Material Type" ItemStyle-Width="150" />
                                <asp:BoundField DataField="AuthorizationGroup" HeaderText="Authorization Group" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ItemCategoryGroup" HeaderText="Item Cat. Group" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="230">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectMatTyp_Click">Select This Material Type</asp:LinkButton>
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
        <!--Material Group Modal-->
        <div class="modal fade" id="modalMatGr" role="dialog">
            <div class="modal-dialog" style="max-width: 900px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Material Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewMatGr" runat="server"
                            DataKeyNames="MatlGroup, MatlGroup_Desc, MatlGroup_Desc2" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="MatlGroup" HeaderText="Material Group" ItemStyle-Width="150" />
                                <asp:BoundField DataField="MatlGroup_Desc" HeaderText="Material Group Desc." ItemStyle-Width="250" />
                                <asp:BoundField DataField="MatlGroup_Desc2" HeaderText="Material Group Desc2." ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="200">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectMatGr_Click">Select This Material Group</asp:LinkButton>
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
        <!--Plant Modal-->
        <div class="modal fade" id="modalPlantID" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Plant</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewPlant" runat="server"
                            DataKeyNames="Plant" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="Plant" HeaderText="PlantID" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Plant_Desc" HeaderText="Plant Name" ItemStyle-Width="300" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" OnClick="selectPlant_Click">Select This Plant</asp:LinkButton>
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
        <!--SO Modal-->
        <div class="modal fade" id="modalSalesOrg" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Sales Org.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewSalesOrg" runat="server"
                            DataKeyNames="SOrg, SOrg_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="SOrg" HeaderText="Sales Org." ItemStyle-Width="150" />
                                <asp:BoundField DataField="SOrg_Desc" HeaderText="Sales Org. Desc." ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSalesOrg_Click">Select This SO</asp:LinkButton>
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
        <!--Stor Loc Modal-->
        <div class="modal fade" id="modalStorLoc" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Stor Loc</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewStorLoc" runat="server"
                            DataKeyNames="Plant,SLoc,SLoc_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                <asp:BoundField DataField="SLoc" HeaderText="Store Location" ItemStyle-Width="150" />
                                <asp:BoundField DataField="SLoc_Desc" HeaderText="Store Location Desc." ItemStyle-Width="300" />
                                <asp:TemplateField ItemStyle-Width="250">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton2" OnClick="selectStorLoc_Click">Select This Store Location</asp:LinkButton>
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
        <!--DistrChl Modal-->
        <div class="modal fade" id="modalDistrChl" role="dialog">
            <div class="modal-dialog" style="max-width: 580px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Distr. Chl.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewDistrChl" runat="server"
                            DataKeyNames="DistrChl, DistrChl_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="DistrChl" HeaderText="Distribution Channel" ItemStyle-Width="150" />
                                <asp:BoundField DataField="DistrChl_Desc" HeaderText="Distribution Channel Desc." ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="230">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton3" OnClick="selectDistrChl_Click">Select This Distribution Channel</asp:LinkButton>
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
                                        <asp:LinkButton runat="server" ID="LinkButton8" OnClick="selectAunRnd_Click">Select This Aun</asp:LinkButton>
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
        <!--BsUntMeas Modal-->
        <div class="modal fade" id="modalBsUntMeas" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Base Unit Measurement</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewBsUntMeas" runat="server"
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
                                        <asp:LinkButton runat="server" ID="LinkButton9" OnClick="selectBsUntMeas_Click">Select This Plant</asp:LinkButton>
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
        <!--Division Modal-->
        <div class="modal fade" id="modalDivision" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Division</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewDivision" runat="server"
                            DataKeyNames="Division, Dv_Desc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="Division" HeaderText="Division" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Dv_Desc" HeaderText="Division Desc." ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton10" OnClick="selectDivision_Click">Select This Division</asp:LinkButton>
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
        <!--Procurement Type Modal-->
        <div class="modal fade" id="modalProcType" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Procurement Type</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewProcType" runat="server"
                            DataKeyNames="ProcType, ProcTypeDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="ProcType" HeaderText="Procurement Type" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ProcTypeDesc" HeaderText="Procurement Desc." ItemStyle-Width="180" />
                                <asp:TemplateField ItemStyle-Width="210">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton11" OnClick="selectProcType_Click">Select This Procurement Type</asp:LinkButton>
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

        <!--Proc Region-->
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

        <!--Planner Region-->
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
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSchedMargKey_Click">Select This Plant</asp:LinkButton>
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

        <!--QC Region-->
        <!--Class Type Modal-->
        <div class="modal fade" id="modalClassTyp" role="dialog">
            <div class="modal-dialog" style="max-width: 615px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Class Type</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewClassTyp" runat="server"
                            DataKeyNames="ClassType, ClassTypDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="ClassType" HeaderText="ClassType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ClassTypDesc" HeaderText="ClassTypDesc" ItemStyle-Width="250" />
                                <asp:TemplateField ItemStyle-Width="160">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectClassTyp_Click">Select This Class Type</asp:LinkButton>
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
        <!--Class Modal-->
        <div class="modal fade" id="modalClass" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Class</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewClass" runat="server"
                            DataKeyNames="ClassType, Class" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" EnableViewState="true">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="Class" HeaderText="Class" ItemStyle-Width="150" />
                                <asp:BoundField DataField="ClassType" HeaderText="ClassType" ItemStyle-Width="350" />
                                <asp:TemplateField ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton2" OnClick="selectClass_Click">Select This Plant</asp:LinkButton>
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
        <!--InspectionType Modal-->
        <div class="modal fade" id="modalInspectionTypeTyp" role="dialog">
            <div class="modal-dialog" style="max-width: 680px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search InspectionType</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewInspectionType" runat="server"
                            DataKeyNames="InspType, InspTypeDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="InspType" HeaderText="InspType" ItemStyle-Width="150" />
                                <asp:BoundField DataField="InspTypeDesc" HeaderText="Description" ItemStyle-Width="300" />
                                <asp:TemplateField ItemStyle-Width="190">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton2" OnClick="selectInspectionType_Click">Select This InspectionType</asp:LinkButton>
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

        <!--QA Region-->
        <!--Store Condition Modal-->
        <div class="modal fade" id="modalStoreCond" role="dialog">
            <div class="modal-dialog" style="max-width: 560px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Storage Condition</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridViewStoreCond" runat="server"
                            DataKeyNames="StorConditions, StorConditionsDesc" AutoGenerateColumns="False"
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent">
                            <FooterStyle ForeColor="#8C4510"
                                BackColor="#F7DFB5"></FooterStyle>
                            <PagerStyle ForeColor="#8C4510"
                                HorizontalAlign="Center"></PagerStyle>
                            <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                BackColor="#8AE0F2"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="StorConditions" HeaderText="Storage Conditions" ItemStyle-Width="150" />
                                <asp:BoundField DataField="StorConditionsDesc" HeaderText="Description" ItemStyle-Width="150" />
                                <asp:TemplateField ItemStyle-Width="210">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectStoreCond_Click">Select This Storage Condition</asp:LinkButton>
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

        <!--Reject Type Modal-->
        <div class="modal fade" id="modalReject" role="dialog">
            <div class="modal-dialog" style="max-width: 590px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Reject Note</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label runat="server" ID="lblRejectReason" Text="Declined Reason*" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="inputRejectReason" placeholder="Declined Reason*" CssClass="txtBoxDesc" AutoComplete="off" onkeydown="return(event.keyCode!=199);" Height="150" TextMode="MultiLine" Columns="40" Rows="5" required="true" Text="For more information about the declined note, please contact R&D Manager." />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorRejectReason" runat="server" TargetControlID="inputRejectReason" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#%^&*()_+=-`~\|]}[{:,.<>/?" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="Button1" Text="Reject" OnClick="Reject_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
