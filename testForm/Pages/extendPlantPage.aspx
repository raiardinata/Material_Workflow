<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="extendPlantPage.aspx.cs" Inherits="testForm.Pages.extendPlantPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../ScriptModal/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../ScriptModal/bootstrap-3.3.7-dist/js/bootstrap.js"></script>
    <script src="../ScriptModal/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>

    <style>
        fieldset.group-border {
            border: 3px solid #000 !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }

        legend.group-border {
            font-size: 16pt !important;
            font-weight: normal !important;
            text-align: left !important;
            width: auto;
            padding: 0 10px;
        }

        select, input, textarea {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
        }

        .gridview-header {
            font-size: 24px;
        }

        span {
            font-size: 15px;
        }
    </style>

    <script>
        function ShowMessage(message) {
            alert(message);


            window.location.href = 'extendPlantPage';
            //$('#modaltest').modal();
        }

        function ShowMessage2(message) {
            alert(message);


            //$('#modaltest').modal();
        }

        function ShowMessageError(message) {
            alert(message);

        }
        function IsMaterialIDEmpty() {
            if ($('inputMatID').value == "") {
                return 'Material ID should not be empty';
            }
            else { return ""; }
        }
        function IsSalaryInValid() {
            if (isNaN(document.getElementById('TxtSalary').value)) {
                return 'Enter valid salary';
            }
            else { return ""; }
        }
        function IsValid() {


            var MaterialIDEmptyMessage = IsMaterialIDEmpty();

            var FinalErrorMessage = "Errors:";
            if (MaterialIDEmptyMessage != "")
                FinalErrorMessage += "\n" + MaterialIDEmptyMessage;

            if (FinalErrorMessage != "Errors:") {
                alert(FinalErrorMessage);
                return false;
            }
            else {
                return true;
            }
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <span runat="server" id="spanTransID" style="position: absolute; top: 85px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" />
            </span>
            <span style="font-size: 2rem; display: none; position: absolute">
                <asp:Label runat="server" ID="lblOldTransID" Text="RM00000000" />
                <asp:Label runat="server" ID="lblUser" Text="User" />
                <asp:Label runat="server" ID="lblPosition" Text="User" />
                <asp:Label runat="server" ID="lblLogID" Text="LD00000000" />
                <asp:Label runat="server" ID="lblLine" Text="LN000" />
                <asp:Label runat="server" ID="lblDont" Text="0" />
                <asp:Label runat="server" ID="lblWith" Text="0" />
                <asp:Label runat="server" ID="lblMat" Text="0" />
                <asp:Label runat="server" ID="lblMenu" Text="R&D" />
                <asp:Label runat="server" ID="lblLineInspectionType" Text="LN000" />
                <asp:Label runat="server" ID="lblLineClassType" Text="LN000" />
                <asp:Label runat="server" ID="lblChkbx" Text="Test" />
                <asp:Label runat="server" ID="lblMatID" Text="0" />
                <asp:Label runat="server" ID="FilterSearch" Text="" />
                <asp:Label runat="server" ID="lblExtend" Text="" />
                <asp:Label runat="server" ID="lblUpdate" Text="X" />
            </span>
            <!--ListView-->
            <div runat="server" id="listViewExtend" style="padding: 0px 5px">
                <h1>R&D Extend Plant - Master Data</h1>
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
                                <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMatIDDESC" placeholder="MaterialID/Material Description" CssClass="txtBoxDesc" AutoComplete="off" />
                            </td>
                            <td style="width: auto">
                                <asp:ImageButton runat="server" ID="searchBtn" ImageUrl="../Images/src-white.png" Style="height: 30px; width: 40px;" class="btn btn-info btn-sm" type="button" OnClick="srcListview_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                    <!--Material ID and Desc List View-->
                    <div style="max-width: 1300px">
                        <div class="modal-content">
                            <div class="modal-body">
                                <asp:GridView ID="GridViewListView" runat="server"
                                    DataKeyNames="MaterialID, MaterialDesc"
                                    AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2"
                                    BorderColor="Transparent" OnRowCommand="GridViewListView_RowCommand"
                                    OnRowDataBound="GridViewListView_RowDataBound" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="GridViewListView_PageIndexChanging">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True" Font-Size="Large"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <RowStyle Font-Size="Medium" />
                                    <Columns>
                                        <asp:BoundField DataField="TransID" HeaderText="Transaction ID" ItemStyle-Width="130" />
                                        <asp:BoundField DataField="MaterialID" HeaderText="Material ID" ItemStyle-Width="130" />
                                        <asp:BoundField DataField="MaterialDesc" HeaderText="Material Description" ItemStyle-Width="250" />
                                        <asp:BoundField DataField="MatType" HeaderText="MatType" ItemStyle-Width="80" />
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
                                        <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="MaterialApproved" HeaderText="MaterialApproved" ItemStyle-Width="150" />
                                        <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="RDApproveBy" HeaderText="RDApproveBy" ItemStyle-Width="150" />
                                        <asp:BoundField HeaderStyle-CssClass="hiddenLabel" ItemStyle-CssClass="hiddenLabel" DataField="ExtendPlant" HeaderText="ExtendPlant" ItemStyle-Width="150" />

                                        <%--   <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="150" />--%>
                                        <asp:BoundField DataField="GlobalStatus" HeaderText="GlobalStatus" ItemStyle-Width="130" />

                                        <asp:BoundField DataField="Notes" HeaderText="Notes" ItemStyle-Width="350" />



                                        <asp:TemplateField ItemStyle-Width="150px">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="slcExtendMaterial" Text="Select This Material" OnClick="slcExtendMaterial_Click" />
                                                <%--<asp:LinkButton runat="server" ID="LBSelectExtend" Text="Extend This Material" OnClick="slcExtendMaterial_Click" />--%>
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
            <fieldset runat="server" id="rmContent" visible="false" style="width: auto; top: 20px">
                <legend style="padding: 10px 2px 10px 2px">Request Extent Plant - R&D</legend>

                <!--REPORT MENU-->
                <%-- <div runat="server" id="divApprMn" style="height: initial; padding: 0px 15px 0px 15px" visible="false">
                    <asp:Menu Orientation="Horizontal" ID="FICONavigationMenu" runat="server" OnMenuItemDataBound="NavigationMenuReport_MenuItemDataBound" OnMenuItemClick="NavigationMenuReport_OnMenuItemClick">
                        <StaticSelectedStyle CssClass="selected" />
                        <LevelMenuItemStyles>
                            <asp:MenuItemStyle CssClass="main_menu" />
                            <asp:MenuItemStyle CssClass="level_menu" />
                        </LevelMenuItemStyles>
                        <Items>
                            <asp:MenuItem Text="RnD" Value="1" Selected="true" />
                            <asp:MenuItem Text="Procurement" Value="2" />
                            <asp:MenuItem Text="Planner" Value="3" />
                            <asp:MenuItem Text="QC" Value="4" />
                            <asp:MenuItem Text="QA" Value="5" />
                            <asp:MenuItem Text="QR" Value="6" />
                        
                        </Items>
                    </asp:Menu>
                </div>--%>


                <%--BRAND NEW REPORT MENU--%>
                <div class="container">

                    <ul class="nav nav-tabs">
                        <li class="active">
                            <asp:Button ID="MenuRnD" Text="R&D" runat="server" OnClick="MenuRnD_Click" /></li>
                        <li>
                            <asp:Button ID="MenuProcurement" Text="Procurement" runat="server" OnClick="MenuProcurement_Click" /></li>
                        <li>
                            <asp:Button ID="MenuPlanner" Text="Planner" runat="server" OnClick="MenuPlanner_Click" /></li>
                        <li>
                            <asp:Button ID="MenuQC" Text="QC" runat="server" OnClick="MenuQC_Click" /></li>
                        <li>
                            <asp:Button ID="MenuQA" Text="QA" runat="server" OnClick="MenuQA_Click" /></li>
                        <li>
                            <asp:Button ID="MenuQR" Text="QR" runat="server" OnClick="MenuQR_Click" /></li>

                    </ul>
                    <br>
                </div>
                <!--R&D REPORT-->
                <div runat="server" id="rndTBL" visible="true" style="padding: 0px 15px 0px 15px">
                    <fieldset style="width: initial;" class="group-border">
                        <legend class="group-border">Request Extend Plant - R&D Extend Plant</legend>
                        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                            <tr style="height: initial;">
                                <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                                    <!--Basic Data 1 General Data List-->

                                    <fieldset class="group-border" runat="server" id="bscDt1GnrlDt" style="width: 100%;">

                                        <legend class="group-border">Basic Data 1 - General Data</legend>

                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <%----%>
                                                <td class="lblTd" style="text-align: right;">
                                                    <asp:Label runat="server" ID="Label32" Text="Material Type " />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMatType" type="text" />
                                                </td>
                                                <td style="width: 40%; text-align: left;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMatTyp" Text="Material Type Desc." />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblBsDt">
                                                    <asp:Label runat="server" ID="Label11" Text="Material ID* " />
                                                </td>
                                                <td class="txtBox" style="text-align: left;">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMatID" type="textBoxRO" ReadOnly="true" OnTextChanged="inputMaterialID_onBlur" AutoPostBack="true" />
                                                </td>
                                                <td style="width: auto" runat="server" id="imgBtnBsUntMeas" visible="false">
                                                    <%-- <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalMatID"></button>--%>
                                                    <asp:LinkButton ID="LBSearchMatID" Visible="false" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchMatID_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                    <%-- <asp:Button ID="BSearchMatID" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchMatID_Click"
                                                        style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblBsDt">
                                                    <asp:Label runat="server" ID="Label12" Text="Material Description " />
                                                </td>
                                                <td colspan="2" class="txtBox" style="text-align: left">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" Style="width: 300px;" CssClass="txtBoxRODESC" ReadOnly="true" runat="server" ID="inputMatDesc" type="text" />
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
                                    <fieldset class="group-border" runat="server" id="foreignTradeDt" style="width: 100%" visible="true">
                                        <legend class="group-border">Foreign Trade Import - Foreign Trade Data</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label20" Text="Comm/Imp Code No " />
                                                </td>
                                                <td style="width: 105px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBox" runat="server" ID="inputCommImpCode" type="text" OnTextChanged="inputCommImpCode_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchCommImpCode" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchCommImpCode_Click">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>

                                                </td>
                                                <td style="width: 180px;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblCommImpCode" Text="Comm/Imp Code No. Desc." />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ValidateCommImpCode" runat="server" ControlToValidate="inputCommImpCode"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Comm Imp Code Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <!--Plant Data / Stor 1 : Shelf Life Data-->
                                    <fieldset class="group-border" runat="server" id="plantShelfLifeDt" style="width: 100%">
                                        <legend class="group-border">Plant Data / Stor 1 - Shelf Life Data</legend>
                                        <table style="margin: auto auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label22" Text="Min. Rem. Shelf Life* " />
                                                </td>
                                                <td style="width: 105px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputMinRemShLf" type="text" required="true" />
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
                                                    <asp:Label runat="server" ID="Label5" Text="Period Ind. for SLED " />
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
                                                    <asp:Label runat="server" ID="Label24" Text="Total Shelf Life " />
                                                </td>
                                                <td style="width: 105px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputTotalShelfLife" type="text" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorTotalShelfLife" runat="server" TargetControlID="inputTotalShelfLife" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label runat="server" ID="Label26" Text="DAY" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <%-- right: auto; 
                                 left: auto;--%>
                                <td style="width: auto; height: auto; vertical-align: top; top: auto; bottom: auto; padding: 5px 0px 5px 7px">
                                    <!--Basic Data 1 : Dimension/EAN-->
                                    <fieldset class="group-border" runat="server" id="bscDt1Dimension" style="width: 100%">
                                        <legend class="group-border">Basic Data 1 - Dimension/EAN</legend>
                                        <table style="margin: auto auto 5px 5px; width: initial">
                                            <tr>
                                                <td style="width: 105px; height: 30px; text-align: right;">
                                                    <asp:Label runat="server" ID="Label28" Text="Net Weight* " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputNetWeight" type="text" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <!--Organizational Level-->
                                    <%--right: auto;
                                    left: auto;--%>
                                    <fieldset class="group-border" runat="server" id="orgLv" style="width: 100%; height: auto; top: auto; bottom: auto;">
                                        <legend class="group-border">Organizational Level - Organizational Level</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="lbl3" Text="Sales Org.* " />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" runat="server" ID="inputSalesOrg" type="text" OnTextChanged="inputSalesOrg_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchSOrg" runat="server" OnClick="LBSearchSOrg_Click" CssClass="btn btn-default btn-sm">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 40%; text-align: left;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSalesOrg" Text="Sales Org. Desc" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="lbl4" Text="Distr.Chl* " />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" runat="server" ID="inputDistrChl" CssClass="txtBoxEdit" type="text" OnTextChanged="inputDistrChl_TextChanged" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchDistrChl" runat="server" OnClick="LBSearchDistrChl_Click" CssClass="btn btn-default btn-sm">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 40%; text-align: left;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblDistrChl" Text="Distr. Chl. Desc" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="lbl1" Text="Plant* " />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" runat="server" ID="inputPlant" type="text" OnTextChanged="inputPlant_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchPlant" runat="server" OnClick="LBSearchPlant_Click" CssClass="btn btn-default btn-sm">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>

                                                </td>
                                                <td style="width: 40%; text-align: left">

                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPlant" Text="Plant Desc" />
                                                </td>

                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputPlant"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Plant Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="lbl2" Text="Stor. Location *" />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" runat="server" ID="inputStorLoc" type="text" OnTextChanged="inputStorLoc_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchSLoc" runat="server" OnClick="LBSearchSLoc_Click" CssClass="btn btn-default btn-sm">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 40%; text-align: left;">

                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblStorLoc" Text="Stor Loc Desc" />
                                                </td>

                                            </tr>
                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inputStorLoc"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="S Loc Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>


                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="inputSalesOrg"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="S Org Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </fieldset>
                                    <!--MRP 1 : LOT Size Data-->
                                    <%--   <fieldset class="group-border" runat="server" id="MRP1LOTSizeDt" style="width: 100%">
                                        <legend class="group-border">MRP 1 : LOT Size Data</legend>
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
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="inputMinLotSize"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Min Lot Size Required" ForeColor="Red" ></asp:RequiredFieldValidator>
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
                                    </fieldset>--%>
                                    <!--MRP2 Procurement-->
                                    <fieldset class="group-border" runat="server" id="MRP2Proc" style="width: 100%">
                                        <legend class="group-border">MRP2 - Procurement</legend>
                                        <table style="margin: auto auto 5px 7px; width: initial">
                                            <tr>
                                                <td style="width: 160px; height: 30px; text-align: right;">
                                                    <asp:Label runat="server" ID="lblCoProd" Text="Co-Product " />
                                                </td>
                                                <td style="width: 150px">
                                                    <%--OnCheckedChanged="chkbxCoProd_CheckedChanged"--%>
                                                    <asp:CheckBox runat="server" ID="chkbxCoProd" AutoPostBack="true" Checked="false" OnCheckedChanged="chkbxCoProd_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>

                                                <td style="width: 130px; height: 30px; text-align: right;">
                                                    <asp:Label runat="server" ID="Label35" Text="Procurement Type* " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" runat="server" ID="inputProcType" type="text" OnTextChanged="inputProcType_TextChanged" AutoPostBack="true" />

                                                </td>
                                                <td style="width: auto">
                                                    <asp:LinkButton ID="LBSearchProc" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchProc_Click">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>

                                                </td>
                                                <td>
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProcType" Text="No Procurement" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="inputProcType"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Proc Type Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 150px; text-align: right">
                                                    <asp:Label runat="server" ID="lblinputSpcProcRnd" Text="Special Procurement " />
                                                </td>
                                                <td style="width: 150px" class="txtBox">
                                                    <%--OnTextChanged="inputSpcProc_onBlur"--%>
                                                    <%--placeholder="Special Procurement" autocomplete="off"--%>
                                                    <%--onkeypress="return this.value.length<=4"--%>
                                                    <%--AutoPostBack="true" --%>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputSpcProcRnd" type="text" OnTextChanged="inputSpcProcRnd_TextChanged" AutoPostBack="true" />

                                                </td>
                                                <td runat="server" id="imgBtnSpcProc">
                                                    <%--<button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalSpcProc"></button>--%>
                                                    <asp:LinkButton ID="LBSearchSpcProc" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchSpcProc_Click">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>

                                                </td>
                                                <td style="width: auto">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="Label52" Text="Spc. Proc." />
                                                    <asp:Label CssClass="hiddenLabel" runat="server" ID="absSpcProc" Text="Spc. Proc." />
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="inputSpcProcRnd"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Special Proc Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr style="height: initial;">
                                <!--Other Data-->
                                <td colspan="2">
                                    <fieldset class="group-border" style="width: 100%; height: initial">
                                        <legend class="group-border">Other Data - Unit Measurement</legend>
                                        <!--Input Table-->
                                        <table style="width: 500px; height: auto; margin: auto auto 0px auto" runat="server" id="otrInputTbl">
                                            <tr>
                                                <td class="lblOtrDt" style="text-align: center; width: 70px">
                                                    <asp:Label runat="server" ID="lblGrossWeight" Text="Gross Weight*" /></td>
                                                <td class="lblOtrDt" style="text-align: center; width: 70px">
                                                    <asp:Label runat="server" ID="lblWeightUnit" Text="Weight Unit*" /></td>
                                                <td runat="server" id="tdVolume" style="text-align: center; width: 70px;">
                                                    <asp:Label runat="server" ID="Label23" Text="Volume " /></td>
                                                <td style="text-align: center; width: 70px">
                                                    <asp:Label runat="server" ID="Label14" Text="Volume Unit " /></td>
                                                <td colspan="2" class="lblOtrDt" style="text-align: center; width: 70px; padding-left: 38px">X</td>
                                                <td style="text-align: center; width: 70px;">Aun</td>
                                                <td colspan="2" class="lblOtrDt" style="text-align: center; width: 70px; padding-left: 35px">Y</td>
                                                <td class="lblOtrDt" style="text-align: center; width: 70px;">Bun</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70px">
                                                    <asp:TextBox runat="server" ID="inputGrossWeight" type="text" autocomplete="off" required="true" onkeypress="return this.value.length<=17" Text="0" Width="100px" onkeydown="return(event.keyCode!=13);" OnTextChanged="inputGrossWeight_TextChanged" AutoPostBack="true" onchange="validateFloatKeyPress(this);" CssClass="txtBoxRO" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorGrossWeight" runat="server" TargetControlID="inputGrossWeight" FilterType="Custom, Numbers" ValidChars="0123456789." />
                                                </td>
                                                <td style="width: 70px">
                                                    <asp:TextBox runat="server" ID="inputWeightUnt" type="text" autocomplete="off" required="true" AutoPostBack="True" OnTextChanged="inputWeightUnt_onBlur" onkeypress="return this.value.length<=4" Text="-" Width="100px" ReadOnly="true" CssClass="txtBoxRO" onkeydown="return(event.keyCode!=13);" />
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
                                                    <asp:LinkButton ID="LBSearchVolUnt" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchVolUnit_Click">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
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
                                                    <asp:LinkButton ID="LBSearchAun" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchAun_Click">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
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
                                                    <asp:Button ID="btnAddUntMeas" class="btn btn-info btn-sm" runat="server" Text="Add" OnClick="btnAddUntMeas_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!--Repeater Item-->
                                        <table style="border: 1px solid #8AE0F2; width: 100%; margin: auto auto auto auto" cellpadding="0">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:Repeater ID="reptUntMeas" runat="server" OnItemCommand="rept_ItemCommand" Visible="true" OnItemDataBound="reptUntMeas_ItemDataBound">
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
                                    <asp:Label runat="server" ID="req" Text="" ForeColor="Red" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>

                <!--PROC REPORT-->
                <div runat="server" id="procTBL" visible="false" style="padding: 0px 15px 0px 15px">
                    <fieldset class="group-border">
                        <legend class="group-border">Request Extend Plant - Procurement</legend>
                        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                            <tr style="height: initial;">
                                <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                                    <!--Basic Data 1 General Data List-->
                                    <fieldset class="group-border" runat="server" visible="false" id="bscDt1GnrlDtProc" style="width: 100%;">
                                        <legend class="group-border">General Data</legend>
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
                                                <td class="lblBsDt">
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
                                                    <asp:Label runat="server" ID="Label112" Text="Plant " Visible="false" />
                                                </td>
                                                <td class="txtBox">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBoxRO" runat="server" ID="inputProcPlant" type="text" ReadOnly="true" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <!--Purchasing - Purchasing Value & Order Data-->
                                    <fieldset class="group-border" runat="server" visible="false" id="purchValNOrder" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                        <legend class="group-border">Purchasing - Purchasing Value & Order Data</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label8" Text="Purchasing Group" />
                                                </td>
                                                <td style="width: 150px">
                                                    <%--OnTextChanged="inputPurcGrp_onBlur"--%>
                                                    <%--placeholder="Purchasing Group"--%>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPurcGrp" type="text" autocomplete="off" AutoPostBack="True" onkeypress="return this.value.length<=19" OnTextChanged="inputPurcGrp_TextChanged" />
                                                </td>
                                                <td style="width: auto">
                                                    <%-- <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalPurcGrp"></button>
                                                    --%>
                                                    <asp:LinkButton ID="LBSearchPurchasingGroup" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchPurchasingGroup_Click">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPurcGrp" Text="Purchasing Group" />
                                                    <asp:Label CssClass="hiddenLabel" runat="server" ID="absPurcGrp" Text="Purchasing Group" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="inputPurcGrp"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Purch Group Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label43" Text="Purc. Val. Key " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPurcValKey" OnTextChanged="inputPurcValKey_onBlur" AutoPostBack="true" type="text" ReadOnly="true" CssClass="txtBox" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchPurchValKey" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchPurchValKey_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblPurcValKey" Text="" />
                                                    <asp:Label CssClass="hiddenLabel" runat="server" ID="absPurcValKey" Text="Purc. Val. Key. Desc." />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label44" Text="GR Proc. Time " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputGRProcTimeMRP1" type="text" ReadOnly="true" CssClass="txtBox" Text="1" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorGRProcTime" runat="server" TargetControlID="inputGRProcTimeMRP1" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                </td>
                                                <td class="lblTd" style="width: 30px;">
                                                    <asp:Label runat="server" ID="lblGRProcTimeMRP1" Text="DAY" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="inputGRProcTimeMRP1"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="GR Proc Time Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                            <tr runat="server" id="trPlantDeliv">
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label3" Text="Planned Delivery Time " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputPlantDeliveryTime" type="text" CssClass="txtBox" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorPlantDeliveryTime" runat="server" TargetControlID="inputPlantDeliveryTime" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                </td>
                                                <td class="lblTd" style="width: 30px;">
                                                    <asp:Label runat="server" ID="Label4" Text="DAY" />
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
                                    <fieldset class="group-border" runat="server" visible="false" id="MRPLotSize" style="width: 100%;">
                                        <legend class="group-border">MRP 1 : LOT Size Data</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label46" Text="Min. LOT Size " />
                                                </td>
                                                <td class="txtBox">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMinLotSizeProc" type="text" ReadOnly="true" CssClass="txtBox" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label47" Text="Round. Value " />
                                                </td>
                                                <td class="txtBox">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputRoundValueProc" type="text" ReadOnly="true" CssClass="txtBox" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>

                                    <!--Working Schedule-->
                                    <%--  <fieldset runat="server" id="Fieldset5" visible="true" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                            <legend>Working Schedule</legend>
                            <table style="margin: 0px auto 5px 5px; width: initial">
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label3" Text="Production Scheduler " />
                                    </td>
                                    <td style="width: 150px">
                                         <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="TextBox1" type="text" placeholder="Production scheduler" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSched_TextChanged" />
                                    </td>
                                    <td style="width: auto">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProdSched"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="Label4" Text="Production scheduler Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="Label5" Text="Production scheduler Desc." />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; text-align: right">
                                        <asp:Label runat="server" ID="Label6" Text="Production Scheduler Profile " />
                                    </td>
                                    <td style="width: 150px">
                                         <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="TextBox2" type="text" placeholder="Production scheduler" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSchedProfile_TextChanged" />
                                    </td>
                                    <td style="width: auto">
                                        <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProdSchedProfile"></button>
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Label CssClass="absoluteLabel" runat="server" ID="Label7" Text="Production Scheduler Profile Desc." />
                                        <asp:Label CssClass="hiddenLabel" runat="server" ID="Label10" Text="Production Scheduler Profile Desc." />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>--%>
                                </td>
                                <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                                    <!--Basic Data 1 : Dimension/EAN-->
                                    <fieldset class="group-border" runat="server" visible="false" id="BscDtDimension" style="width: 100%">
                                        <legend class="group-border">Basic Data 1 - Dimension/EAN</legend>
                                        <table style="margin: auto auto 5px 5px; width: initial">
                                            <tr>
                                                <td style="width: 105px; height: 30px; text-align: right;">
                                                    <asp:Label runat="server" ID="Label49" Text="Net Weight* " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputNetWeightProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <!--Foreign Trade Import : Foreign Trade Data-->
                                    <fieldset class="group-border" runat="server" visible="false" id="ForeignTradeData" style="width: 100%">
                                        <legend class="group-border">Foreign Trade Import - Foreign Trade Data</legend>
                                        <table style="margin: auto auto 5px 5px; width: initial">
                                            <tr>
                                                <td style="width: 80px" class="lblTd">
                                                    <asp:Label runat="server" ID="Label55" Text="Comm/Imp Code No " />
                                                </td>
                                                <td style="width: 105px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputCommImpCodeProc" type="text" ReadOnly="true" CssClass="txtBoxRO" OnTextChanged="inputCommImpCodeProc_TextChanged" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchCommImpCode2" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchCommImpCode2_Click">
                                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblCommImpProc" Text="Comm/Imp Code No. Desc." />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <!--Plant Data / Stor 1 : Shelf Life Data-->
                                    <%--<fieldset class="group-border" runat="server" visible="false" id="PlantShelfLifeDtProc" style="width: 100%">
                                        <legend class="group-border">Plant Data / Stor 1 - Shelf Life Data</legend>
                                        <table style="margin: auto auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label57" Text="Min. Rem. Shelf Life " />
                                                </td>
                                                <td style="width: 105px">
                                                     <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputMinRemShLfProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label runat="server" ID="Label58" Text="DAY" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label59" Text="Total Shelf Life " />
                                                </td>
                                                <td style="width: 105px">
                                                     <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputTotalShelfLifeProc" type="text" ReadOnly="true" CssClass="txtBoxRO" />
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label runat="server" ID="Label60" Text="DAY" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>--%>
                                    <!--Sales Data : Sales Data-->
                                    <fieldset class="group-border" runat="server" visible="false" id="SalesData" style="width: 100%">
                                        <legend class="group-border">Sales Data - Sales Data</legend>
                                        <table style="margin: auto auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label61" Text="Loading Group " />
                                                </td>
                                                <td style="width: 105px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" required="true" ID="inputLoadingGrp" type="text" ReadOnly="true" CssClass="txtBox" OnTextChanged="inputLoadingGrp_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchLoadingGroup" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchLoadingGroup_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px;">

                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLoadingGrp" Text="Loading Group Desc." />
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="inputLoadingGrp"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Loading Group Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr style="height: initial;">
                                <!--Other Data-->
                                <td colspan="2">
                                    <fieldset class="group-border" style="width: 100%; height: initial">
                                        <legend class="group-border">Other Data - Unit Measurement</legend>
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
                    </fieldset>
                </div>

                <!--Planner Report-->
                <div runat="server" id="plannerTbl" visible="false" style="padding: 0px 15px 0px 15px">
                    <fieldset style="width: auto" class="group-border">
                        <legend class="group-border" style="padding: 0px 2px 0px 2px">Request Extend Plant - Planner</legend>
                        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                            <tr style="height: initial;">
                                <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                                    <!--Basic Data 1 General Data List-->
                                    <fieldset class="group-border" runat="server" visible="false" id="bscDt1GnrlDtPlanner" style="width: 100%;">
                                        <legend class="group-border">General Data</legend>
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
                                                <td colspan="2" class="txtBox" style="text-align: left; height: 30px;">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" Style="width: 300px;" CssClass="txtBoxRO" runat="server" ID="inputMaterialDescPlanner" type="text" required="true" autocomplete="off" ReadOnly="true" />
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
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputLabOffice" type="text" OnTextChanged="inputLabOffice_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchLabOffice" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchLabOffice_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 160px">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLabOffice" Text="Lab/Office Desc." />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <!--MRP 1-->
                                    <fieldset class="group-border" runat="server" id="MRP1" visible="false" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                        <legend class="group-border">MRP1</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label70" Text="MRP Group *" />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" required="true" ReadOnly="true" CssClass="txtBox" runat="server" ID="inputMRPGr" type="text" OnTextChanged="inputMRPGr_TextChanged" AutoPostBack="true" />

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchMRPGroup" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchMRPGroup_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
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
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" CssClass="txtBox" runat="server" ID="inputMRPTyp" type="text" OnTextChanged="inputMRPTyp_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchMRPType" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchMRPType_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px;">

                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblMRPTyp" Text="MRP Type Desc." />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label72" Text="MRP Controller *" />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" CssClass="txtBox" runat="server" ID="inputMRPCtrl" type="text" OnTextChanged="inputMRPCtrl_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchMRPCtrl" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchMRPCtrl_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
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
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" CssClass="txtBox" runat="server" ID="inputLOTSize" type="text" OnTextChanged="inputLOTSize_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchLOTSize" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchLOTSize_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px;">

                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblLOTSize" Text="LOT Size Desc." />
                                                </td>
                                            </tr>

                                            <tr id="RowFixLOTSize" runat="server" visible="false">
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label51" Text="Fix LOT Size " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" CssClass="txtBox" runat="server" ID="inputFixLOTSize" type="text" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorFixLotSize" runat="server" TargetControlID="inputFixLOTSize" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label73" Text="Max. Stock Lv. " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBox" runat="server" ID="inputMaxStockLv" type="text" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorMaxStocklLv" runat="server" TargetControlID="inputMaxStockLv" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                </td>
                                            </tr>

                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="inputMRPGr"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="MRP Group Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="inputMRPTyp"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="MRP Type Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="inputMRPCtrl"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="MRP Control Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="inputLOTSize"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Lot Size Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="inputMaxStockLv"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Max Stock Lv Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 0px 5px 7px">
                                    <!--Basic Data 2 : Other Data-->
                                    <fieldset class="group-border" style="width: 100%">
                                        <legend class="group-border">Basic Data 2 - Other Data</legend>
                                        <table runat="server" id="bscDt2OtrDt" style="margin: auto auto 5px 5px; width: initial">
                                            <tr>
                                                <td style="width: 150px; height: 30px; text-align: right;">
                                                    <asp:Label runat="server" ID="Label74" Text="Ind. Std. Desc. " />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputIndStdDesc" type="text" OnTextChanged="inputIndStdDesc_TextChanged" AutoPostBack="true" />

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchIndStdDesc" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchIndStdDesc_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px">

                                                    <asp:Label ID="lblIndStdDesc" runat="server" Text="IndStd Desc"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <!--MRP 2-->
                                    <%--  right: auto; 
                                     left: auto;--%>
                                    <fieldset class="group-border" runat="server" id="MRP2" visible="false" style="width: 100%; height: auto; top: auto; bottom: auto;">
                                        <legend class="group-border">MRP2</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">

                                            <tr>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Label runat="server" ID="Label76" Text="Prod. Stor. Location " />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBox" runat="server" ID="inputProdStorLoc" type="text" OnTextChanged="inputProdStorLoc_TextChanged" AutoPostBack="true" />

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchProdStorLoc" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchProdStorLoc_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>

                                                </td>
                                                <td style="width: 40%;">

                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdStorLoc" Text="Prod Stor Loc" />
                                                </td>



                                            </tr>

                                            <tr>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Label runat="server" ID="Label77" Text="SchedMargin Key *" />
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" required="true" CssClass="txtBox" runat="server" ID="inputSchedMargKey" type="text" OnTextChanged="inputSchedMargKey_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchSchedMarginKey" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchSchedMarginKey_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 40%;">

                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblSchedMargKey" Text="SchedMargin Key" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 150px; text-align: right;">
                                                    <asp:Label runat="server" ID="Label78" Text="Safety Stock " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBox" runat="server" ID="inputSftyStck" type="text" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorSftyStck" runat="server" TargetControlID="inputSftyStck" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 150px; text-align: right">
                                                    <asp:Label runat="server" ID="Label79" Text="Min. Safety Stock " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBox" runat="server" ID="inputMinSftyStck" type="text" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorMinSftyStck" runat="server" TargetControlID="inputMinSftyStck" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                </td>
                                            </tr>

                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="inputProcTypePlanner"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Proc Type Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="inputSpcProc"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Special Proc Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>

                                            <%-- <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="inputSchedMargKey"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Sched Margin Key Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="inputSftyStck"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Safety Stock Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="inputMinSftyStck"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Min Safety Stock Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </fieldset>
                                    <!--MRP 3-->
                                    <fieldset class="group-border" runat="server" id="MRP3" visible="false" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                        <legend class="group-border">MRP3</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td style="width: 150px; text-align: right">
                                                    <asp:Label runat="server" ID="Label80" Text="Strategy Group " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" runat="server" ID="inputStrtgyGr" type="text" AutoPostBack="true" OnTextChanged="inputStrtgyGr_TextChanged" Text="40" />
                                                </td>
                                                <td runat="server" id="tdStrtgyGr" visible="false">
                                                    <asp:LinkButton ID="LBSearchStrategyGroup" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchStrategyGroup_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStrtgyGr" runat="server" Text="Strategy Gr. Desc">

                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="inputStrtgyGr"
                                                        ValidationGroup="ValGroupExtend" ErrorMessage="Strategy Group Required" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </fieldset>
                                    <!--Working Schedule-->
                                    <fieldset class="group-border" runat="server" id="WorkingSchedule" visible="true" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                        <legend class="group-border">Working Schedule</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td style="width: 150px; text-align: right">
                                                    <asp:Label runat="server" ID="Label1" Text="Production Scheduler " />
                                                </td>
                                                <td style="width: 150px">
                                                    <%--OnTextChanged="inputProdSched_TextChanged"--%>
                                                    <%--placeholder="Production scheduler"--%>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProdSched" type="text" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSched_TextChanged" />
                                                </td>
                                                <td style="width: auto">
                                                    <%--<button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProdSched"></button>--%>
                                                    <asp:LinkButton ID="LBSearchProdScheduler" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchProdScheduler_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblProdSched" Text="Production scheduler Desc." />
                                                    <asp:Label CssClass="hiddenLabel" runat="server" ID="absProdSched" Text="Production scheduler Desc." />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 150px; text-align: right">
                                                    <asp:Label runat="server" ID="Label2" Text="Production Scheduler Profile " />
                                                </td>
                                                <td style="width: 150px">
                                                    <%--OnTextChanged="inputProdSchedProfile_TextChanged"--%>
                                                    <%--placeholder="Production scheduler" --%>
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputProdSchedProfile" type="text" autocomplete="off" onkeypress="return this.value.length<=14" AutoPostBack="true" OnTextChanged="inputProdSchedProfile_TextChanged" ReadOnly="true" CssClass="txtBoxRO" />

                                                </td>
                                                <td style="width: auto; display: none">
                                                    <%--  <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalProdSchedProfile"></button>--%>
                                                    <asp:LinkButton ID="LBSearchProdSchedProfile" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchProdSchedProfile_Click" Visible="false">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
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
                    <fieldset class="group-border" runat="server" id="Fieldset1" visible="true" style="width: auto">
                        <legend class="group-border" style="padding: 0px 2px 0px 2px">Request Extend Plant - Quality Control</legend>
                        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                            <tr style="height: initial;">
                                <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                                    <!--Basic Data 1 General Data List-->
                                    <fieldset class="group-border" style="width: 100%;">
                                        <legend class="group-border">General Data</legend>
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
                                    <fieldset class="group-border" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                        <legend class="group-border">Quality Management - Quality Control Data</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label88" Text="Inspect. Setup " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:CheckBox runat="server" ID="chkbxInspectSet" AutoPostBack="true" OnCheckedChanged="chkbxInspectSet_CheckedChanged" Checked="false" Enabled="true" />
                                                </td>
                                                <td colspan="2" style="width: 150px;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblInspectSet" Text="Non Active" Width="180px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label91" Text="Inspect. Interval* " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputInspectIntrv" type="text" autocomplete="off" />
                                                </td>
                                                <td colspan="2" style="width: 150px;">
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblInspectIntrv" Text="Day" Width="180px" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!--Inspect Interval Detail-->
                                        <table style="width: initial; height: auto; margin: auto auto 0px auto;" runat="server" id="tblInspectType">
                                            <tr>
                                                <td class="lblIDInspectionType" style="visibility: hidden; position: absolute">ID</td>
                                                <td colspan="3" style="width: auto; height: 30px; padding-left: 18px">Inspection Type</td>
                                            </tr>
                                            <tr>
                                                <td style="visibility: hidden; position: absolute">
                                                    <asp:Label ID="lblInspectionType" runat="server" Width="30px" Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInspectionType" runat="server" Width="150px" autocomplete="off" placeholder="Inspection Type" CssClass="txtBox" AutoPostBack="true" OnTextChanged="txtInspectionType_TextChanged" onkeydown="return(event.keyCode!=13);" />
                                                </td>
                                                <td style="width: 35px; height: 30px">
                                                    <asp:LinkButton ID="LBSearchInspectionType" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchInspectionType_Click" Visible="true">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
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
                                                    <asp:Repeater ID="rptInspectionTypeDisplay" runat="server"
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
                                                                        <asp:TextBox ID="txtInspectType" runat="server" Text='<%#Eval("InspType") %>' Visible="false" Width="100px" autocomplete="off" />
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
                                    <fieldset class="group-border">
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
                    </fieldset>
                </div>

                <!--QA Report-->
                <div runat="server" id="QATbl" visible="false" style="padding: 0px 15px 0px 15px">
                    <fieldset class="group-border" style="width: auto">
                        <legend class="group-border" style="padding: 0px 2px 0px 2px">Request Extend Plant - Quality Assurance</legend>
                        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                            <tr style="height: initial;">
                                <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                                    <!--Basic Data 1 General Data List-->
                                    <fieldset class="group-border" runat="server" visible="true" id="Fieldset2" style="width: 100%;">
                                        <legend class="group-border">General Data</legend>
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
                                    <fieldset class="group-border" runat="server" visible="true" id="qmQualityControllDt" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                        <legend class="group-border">Plant Data / Store 1 - General Data</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label101" Text="Store Condition " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:TextBox onkeydown="return(event.keyCode!=13);" runat="server" ID="inputStoreCond" type="text" CssClass="txtBoxRO" ReadOnly="true" OnTextChanged="inputStoreCond_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LBSearchStoreCond" runat="server" CssClass="btn btn-default btn-sm" OnClick="LBSearchStoreCond_Click">
                                                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                    </asp:LinkButton>
                                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblStoreCond" Text="Condition Desc." />
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
                    <fieldset class="group-border" runat="server" id="Fieldset3" visible="true" style="position: relative;">
                        <legend class="group-border" style="padding: 0px 2px 0px 2px">Request Extend Plant - Quality Regulatory</legend>
                        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
                            <tr style="height: initial;">
                                <td style="width: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                                    <!--Basic Data 1 General Data List-->
                                    <fieldset class="group-border" runat="server" visible="true" id="Fieldset4" style="width: 100%;">
                                        <legend class="group-border">General Data</legend>
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
                                    <fieldset class="group-border" runat="server" visible="true" id="QMProcData" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                                        <legend class="group-border">Quality Management - Procurement Data</legend>
                                        <table style="margin: 0px auto 5px 5px; width: initial">
                                            <tr>
                                                <td class="lblTd">
                                                    <asp:Label runat="server" ID="Label109" Text="QM Procurement Active " />
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:CheckBox runat="server" ID="chkbxQMProcActive" AutoPostBack="true" Enabled="false" OnCheckedChanged="chkbxQMProcActive_CheckedChanged" />
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
                    </fieldset>
                </div>


                <!--Submit & Cancel Button-->
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px 12px 5px auto">
                    <tr>
                        <td>
                            <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnUpdate" Text="Update" Visible="false" />
                            <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelUpd" Text="Cancel" Visible="false" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnCancelUpd_Click" />
                            <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnApprove" Text="Approve" Visible="false" OnClick="btnApprove_Click" />
                            <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelApprove" Text="Reject" Visible="false" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnRejectModal_Click" />
                            <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CausesValidation="false" />
                            <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelSave" Text="Cancel" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnCancelSave_Click" />
                            &nbsp;
                           <%--<asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnClose" Text="Close" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnClose_Click" />--%>
                            <%--<asp:Button  CssClass="btn btn-info btn-lg" runat="server" ID="btntest" Text="Close" CausesValidation="False" OnClick="btnTes_Click"/>--%>
                            <%--ValidationGroup="ValGroupExtend"--%>
                        </td>

                    </tr>
                    <tr>
                        <asp:Label ID="lblerror" runat="server" Text="" ForeColor="Red" Visible="false">

                        </asp:Label>
                    </tr>
                </table>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>
    <!--LightBox Things-->
    <div class="container">
        <!-- Modal -->
        <!--Ind. Std. Desc. Modal-->
        <div class="modal fade" id="modaltest" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Ind. Std. Desc.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                Sukses !!!!
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Ind. Std. Desc. Modal-->
        <div class="modal fade" id="modalIndStdDesc" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Ind. Std. Desc.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewIndStdDesc" runat="server"
                                    DataKeyNames="IndStdCode, IndStdDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent"
                                    OnRowCommand="GridViewIndStdDesc_RowCommand">
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
                                                <%-- <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectIndStdDesc_Click">Select Ind. Std.</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("IndStdCode") + "," + Eval("IndStdDesc") %>' OnClick="selectIndStdDesc_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--DistrChl Modal-->
        <div class="modal fade" id="modalDistrChl" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Distr. Chl.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel31" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewDistrChl" runat="server"
                                    DataKeyNames="DistrChl, DistrChl_Desc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewDistrChl_RowCommand">
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
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("DistrChl") + "," + Eval("DistrChl_Desc") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--LabOffice Modal-->
        <div class="modal fade" id="modalLabOffice" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Lab/Office</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel29" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewLabOffice" runat="server"
                                    DataKeyNames="LabOffice, Lab_Desc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent"
                                    OnRowCommand="GridViewLabOffice_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="LabOffice" HeaderText="Lab/Office" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="Lab_Desc" HeaderText="Description" ItemStyle-Width="450" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectIndStdDesc_Click">Select Ind. Std.</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("LabOffice") + "," + Eval("Lab_Desc") %>' OnClick="selectLabOffice_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Store Condition Modal-->
        <div class="modal fade" id="modalStoreCond" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Store Condition</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel27" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewStoreCond" runat="server"
                                    DataKeyNames="StorConditions, StorConditionsDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent"
                                    OnRowCommand="GridViewStoreCond_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="StorConditions" HeaderText="StorConditions" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="StorConditionsDesc" HeaderText="Description" ItemStyle-Width="450" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectIndStdDesc_Click">Select Ind. Std.</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("StorConditions") + "," + Eval("StorConditionsDesc") %>' OnClick="selectStorCond_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!--MaterialID Modal-->
        <div class="modal fade" id="modalMatID" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MaterialID</h4>
                    </div>
                    <div class="modal-body" style="overflow-y: auto">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewMaterialID" runat="server"
                                    DataKeyNames="MaterialID, MaterialDesc, TransID" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewMaterialID_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="MaterialID" HeaderText="MaterialID" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="MaterialDesc" HeaderText="Description" ItemStyle-Width="450" />
                                        <asp:BoundField DataField="TransID" HeaderText="TransID" ItemStyle-Width="150" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectMaterialID_Click">Select This MaterialID</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("MaterialID") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewPlant" runat="server"
                                    DataKeyNames="Plant" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewPlant_RowCommand">
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
                                                <%-- OnClick="selectPlant_Click"--%>
                                                <%--<asp:LinkButton runat="server" ID="lnkView" >Select This Plant</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("Plant") + "," + Eval("Plant_Desc")  %>' OnClick="selectPlant_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                    <div class="modal-body" style="overflow-y: auto; height: 450px">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewStorLoc" runat="server"
                                    DataKeyNames="Plant,SLoc,SLoc_Desc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewStorLoc_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="SLoc" HeaderText="Store Location" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="SLoc_Desc" HeaderText="Store Location Desc." ItemStyle-Width="300" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%--OnClick="selectStorLoc_Click"--%>
                                                <%--  <asp:LinkButton runat="server" ID="LinkButton2" >Select This Store Location</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("SLoc") + "," + Eval("SLoc_Desc")%>' OnClick="selectStorLoc_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <h4 class="modal-title">
                        Search Sales Org.
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewSalesOrg" runat="server"
                                    DataKeyNames="SOrg, SOrg_Desc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewSalesOrg_RowCommand">
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
                                                <%--  <asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSalesOrg_Click">Select This SO</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("SOrg") + "," + Eval("SOrg_Desc") %>' OnClick="selectSalesOrg_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewSpcProc" runat="server"
                                    DataKeyNames="SpclProcurement, SpclProcDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewSpcProc_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="SpclProcurement" HeaderText="SpclProcurement" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="SpclProcDesc" HeaderText="Description" ItemStyle-Width="450" />
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="120" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSpcProc_Click">Select This SpcProcurement</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("SpclProcurement") + "," + Eval("SpclProcDesc") %>' OnClick="selectSpcProc_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Comm/Imp Code No Modal-->
        <div class="modal fade" id="modalCommImp" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Comm/Imp Code No.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewCommImp" runat="server"
                                    DataKeyNames="ForeignTrade, ForeignDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewCommImp_RowCommand">
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
                                                <%--  <asp:LinkButton runat="server" ID="lnkView" OnClick="selectCommImp_Click">Select This Plant</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ForeignTrade") + "," + Eval("ForeignDesc") %>' OnClick="selectCommImp_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Comm/Imp Code 2 No Modal-->
        <div class="modal fade" id="modalCommImp2" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Comm/Imp Code No.</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewCommImp2" runat="server"
                                    DataKeyNames="ForeignTrade, ForeignDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewCommImp2_RowCommand">
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
                                                <%--  <asp:LinkButton runat="server" ID="lnkView" OnClick="selectCommImp_Click">Select This Plant</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ForeignTrade") + "," + Eval("ForeignDesc") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Loading Group Modal-->
        <div class="modal fade" id="modalLoadingGroup" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Loading Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewLoadingGroup" runat="server"
                                    DataKeyNames="LoadGrp ,LoadGrpDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewLoadingGroup_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="LoadGrp" HeaderText="LoadGrp" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="LoadGrpDesc" HeaderText="LoadGrpDesc" ItemStyle-Width="350" />
                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <%--  <asp:LinkButton runat="server" ID="lnkView" OnClick="selectCommImp_Click">Select This Plant</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("LoadGrp") + "," + Eval("LoadGrpDesc") %>' OnClick="selectLoadingGrp_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Loading Purchasing Group Modal-->
        <div class="modal fade" id="modalPurchasingGroup" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Purchasing Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewPurchasingGroup" runat="server"
                                    DataKeyNames="PurchGrp ,PurchGrpDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewPurchasingGroup_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="PurchGrp" HeaderText="PurchGrp" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="PurchGrpDesc" HeaderText="PurchGrpDesc" ItemStyle-Width="350" />
                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <%--  <asp:LinkButton runat="server" ID="lnkView" OnClick="selectCommImp_Click">Select This Plant</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("PurchGrp") + "," + Eval("PurchGrpDesc") %>' OnClick="selectPurcGrp_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Loading MRP Group Modal-->
        <div class="modal fade" id="modalMRPGroup" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MRP Group</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewMRPGroup" runat="server"
                                    DataKeyNames="MRPGroup ,MRPGroupDesc, Plant" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewMRPGroup_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="MRPGroup" HeaderText="MRPGroup" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="MRPGroupDesc" HeaderText="MRPGroupDesc" ItemStyle-Width="350" />
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("MRPGroup") +"," + Eval("MRPGroupDesc") + "," + Eval("Plant")%>' OnClick="selectMRPGr_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Loading MRP Type Modal-->
        <div class="modal fade" id="modalMRPType" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MRP Type</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewMRPType" runat="server"
                                    DataKeyNames="MRPType ,MRPTypeDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewMRPType_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="MRPType" HeaderText="MRPType" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="MRPTypeDesc" HeaderText="MRPTypeDesc" ItemStyle-Width="350" />

                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("MRPType") + "," + Eval("MRPTypeDesc") %>' OnClick="selectMRPTyp_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Loading MRP Controller Modal-->
        <div class="modal fade" id="modalMRPCtrl" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MRP Controller</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewMRPCtrl" runat="server"
                                    DataKeyNames="MRPController ,MRPControllerDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewMRPCtrl_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="MRPController" HeaderText="MRPController" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="MRPControllerDesc" HeaderText="MRPControllerDesc" ItemStyle-Width="350" />
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("MRPController") + "," + Eval("MRPControllerDesc") %>' OnClick="selectMRPCtrl_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>


        <!--Loading MRP LOT Size Modal-->
        <div class="modal fade" id="modalMRPLotSize" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search MRP Lot Size</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewMRPLotSize" runat="server"
                                    DataKeyNames="LotSize ,LotSizeDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewMRPLotSize_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="LotSize" HeaderText="LotSize" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="LotSizeDesc" HeaderText="LotSizeDesc" ItemStyle-Width="350" />
                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("LotSize") + "," + Eval("LotSizeDesc")%>' OnClick="selectLOTSize_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Loading Procurement Type Planner Modal-->
        <div class="modal fade" id="modalProcTypePlanner" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Procurement Type Planner</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewProcTypePlanner" runat="server"
                                    DataKeyNames="ProcType ,ProcTypeDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewProcTypePlanner_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="ProcType" HeaderText="ProcType" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="ProcTypeDesc" HeaderText="ProcTypeDesc" ItemStyle-Width="350" />
                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ProcType") %>' OnClick="selectProcType_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Loading Procurement Type Planner Modal-->
        <div class="modal fade" id="modalProcTypeRnD" role="dialog">
            <div class="modal-dialog" style="max-width: 630px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Procurement Type</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewProcTypeRnD" runat="server"
                                    DataKeyNames="ProcType ,ProcTypeDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewProcTypeRnD_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="ProcType" HeaderText="ProcType" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="ProcTypeDesc" HeaderText="ProcTypeDesc" ItemStyle-Width="350" />
                                        <asp:TemplateField ItemStyle-Width="150">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ProcType") + "," + Eval("ProcTypeDesc")%>' OnClick="selectProcType_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!--Special Procurement Type Modal-->
        <div class="modal fade" id="modalSpcProcPlanner" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Special Procurement</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewSpcProcPlanner" runat="server"
                                    DataKeyNames="SpclProcurement, SpclProcDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewSpcProcPlanner_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="SpclProcurement" HeaderText="SpclProcurement" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="SpclProcDesc" HeaderText="Description" ItemStyle-Width="450" />
                                        <asp:BoundField DataField="Plant" HeaderText="Description" ItemStyle-Width="120" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton runat="server" ID="LinkButton1" OnClick="selectSpcProc_Click">Select This SpcProcurement</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("SpclProcurement") + "," + Eval("SpclProcDesc") %>' OnClick="selectSpcProc_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Prod Stor Loc Modal-->
        <div class="modal fade" id="modalProdStorLoc" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Stor Loc</h4>
                    </div>
                    <div class="modal-body" style="overflow-y: auto; height: 450px">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewProdStorLoc" runat="server"
                                    DataKeyNames="Plant,SLoc,SLoc_Desc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewProdStorLoc_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="SLoc" HeaderText="Store Location" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="SLoc_Desc" HeaderText="Store Location Desc." ItemStyle-Width="300" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%--OnClick="selectStorLoc_Click"--%>
                                                <%--  <asp:LinkButton runat="server" ID="LinkButton2" >Select This Store Location</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("SLoc") + ","+ Eval("SLoc_Desc") %>' OnClick="selectProdStorLoc_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Schedule Margin Key Modal-->
        <div class="modal fade" id="modalSchedMarginKey" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Sched Margin Key</h4>
                    </div>
                    <div class="modal-body" style="overflow-y: auto; height: 450px">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewSchedMarginKey" runat="server"
                                    DataKeyNames="SchedType,SchedMarginType,Plant" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewSchedMarginKey_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="SchedType" HeaderText="SchedType" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="SchedMarginType" HeaderText="SchedMarginType" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="300" />
                                        <asp:TemplateField ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <%--OnClick="selectStorLoc_Click"--%>
                                                <%--  <asp:LinkButton runat="server" ID="LinkButton2" >Select This Store Location</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("SchedType") + "," + Eval("SchedMarginType") %>' OnClick="selectSchedMargKey_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Strategy Group Modal-->
        <div class="modal fade" id="modalStrategyGroup" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Strategy Group</h4>
                    </div>
                    <div class="modal-body" style="overflow-y: auto; height: 450px">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewStrategyGroup" runat="server"
                                    DataKeyNames="PlanStrategyGroup,PlanStrategyGrpDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewStrategyGroup_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="PlanStrategyGroup" HeaderText="PlanStrategyGroup" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="PlanStrategyGrpDesc" HeaderText="PlanStrategyGrpDesc" ItemStyle-Width="450" />

                                        <asp:TemplateField ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <%--OnClick="selectStorLoc_Click"--%>
                                                <%--  <asp:LinkButton runat="server" ID="LinkButton2" >Select This Store Location</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("PlanStrategyGroup") + "," + Eval("PlanStrategyGrpDesc") %>' OnClick="selectStrategyGroup_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Production Scheduler Modal-->
        <div class="modal fade" id="modalProdScheduler" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Production Scheduler</h4>
                    </div>
                    <div class="modal-body" style="overflow-y: auto; height: 450px">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewProdScheduler" runat="server"
                                    DataKeyNames="Plant,ProdSched,ProdSchedDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewProdScheduler_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="ProdSched" HeaderText="ProdSched" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="ProdSchedDesc" HeaderText="ProdSchedDesc" ItemStyle-Width="250" />

                                        <asp:TemplateField ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <%--OnClick="selectStorLoc_Click"--%>
                                                <%--  <asp:LinkButton runat="server" ID="LinkButton2" >Select This Store Location</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ProdSched") + "," + Eval("ProdSchedDesc")  + "," + Eval("ProdSchedProfile")  %>' OnClick="selectProdSched_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Production Scheduler Profile Modal-->
        <div class="modal fade" id="modalProdSchedulerProfile" role="dialog">
            <div class="modal-dialog" style="max-width: 700px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Production Scheduler Profile</h4>
                    </div>
                    <div class="modal-body" style="overflow-y: auto; height: 450px">
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewProdSchedProfile" runat="server"
                                    DataKeyNames="Plant,ProdSchedProfile,ProdSchedProfileDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewProdSchedProfile_RowCommand">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="Plant" HeaderText="Plant" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="ProdSchedProfile" HeaderText="ProdSchedProfile" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="ProdSchedProfileDesc" HeaderText="ProdSchedProfileDesc" ItemStyle-Width="250" />

                                        <asp:TemplateField ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <%--OnClick="selectStorLoc_Click"--%>
                                                <%--  <asp:LinkButton runat="server" ID="LinkButton2" >Select This Store Location</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ProdSchedProfile") + "," + Eval("ProdSchedProfileDesc")%>' OnClick="selectProdSchedProfile_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--Profit Center Modal-->
        <div class="modal fade" id="modalProfitCent" role="dialog">
            <div class="modal-dialog" style="max-width: 600px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Search Profit Center</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewProfitCent" runat="server"
                                    DataKeyNames="ProfitCenter, ProfitCenterDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewProfitCent_RowCommand">
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
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ProfitCenter") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewValClass" runat="server"
                                    DataKeyNames="ValuationClass, AcctCatRef, ValuationClassDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" OnRowCommand="GridViewValClass_RowCommand">
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
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("ValuationClass") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel25" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewPurcValKey" runat="server"
                                    DataKeyNames="PurchValueKey, FstRemind, SndRemind, TrdRemind, UnderdelTolerance, OverdelivTolerance, UnltdOverdelivery" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent"
                                    OnRowCommand="GridViewPurcValKey_RowCommand">
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
                                                <%-- <asp:LinkButton runat="server" ID="lnkView" OnClick="selectPurcValKey_Click">Select Purchasing Value Key</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("PurchValueKey") %>' OnClick="selectPurcValKey_Click">
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server" UpdateMode="Always">
                            <ContentTemplate>
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
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("UoM") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel33" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewAun" runat="server"
                                    DataKeyNames="UoM, UnitText, UoM_Desc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent"
                                    OnRowCommand="GridViewAun_RowCommand">
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
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("UoM") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel32" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewVolUnt" runat="server"
                                    DataKeyNames="UoM, UoM_Desc, UnitText" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent"
                                    OnRowCommand="GridViewVolUnt_RowCommand">
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
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("UoM") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel34" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewInspectionType" runat="server"
                                    DataKeyNames="InspType, InspTypeDesc" AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent"
                                    OnRowCommand="GridViewInspectionType_RowCommand">
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
                                                <asp:LinkButton ID="lbAction" ClientIDMode="AutoID" runat="server" CommandName="Select" CommandArgument='<%#Eval("InspType") %>'>
                                                                 <span aria-hidden="true" class="glyphicon glyphicon-plus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnReject" Text="Reject" OnClick="btnCancelApprove_Click" Visible="true" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <script type="text/javascript">

        function userValid() {
            var UName = document.getElementById("inputMatID").value;
            // var Pwd= document.getElementById("txtPwd").value;    

            if (UName == '') {
                alert("Please enter User Name");
                return false;
            }


        }
    </script>
</asp:Content>

