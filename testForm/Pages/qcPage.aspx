<%@ Page Title="Quality Control Site" Language="C#" AutoEventWireup="true" CodeBehind="qcPage.aspx.cs" Inherits="testForm.Pages.qcPage" MasterPageFile="~/Pages/Site.Master" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="56000" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <script type="text/javascript">
        function openModal() {  
            $('#modalClass').modal({ show: true });
            }
        </script>
    <script type="text/javascript">
        function callClass() {
            document.getElementById('<%=btnCallClass.ClientID%>').click();
        }
    </script>

    <span runat="server" id="spanTransID" style="position: absolute; top: 95px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" />
    </span>
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblPosition" Text="User" />
        <asp:Label runat="server" ID="lblLogID" Text="LD00000000" />
        <asp:Label runat="server" ID="lblLineInspectionType" Text="LN000" />
        <asp:Label runat="server" ID="lblLineClassType" Text="LN000" />
        <asp:Label runat="server" ID="FilterSearch" Text="" />
    </span>
    <!--ListView-->
    <div runat="server" id="listViewQC" style="padding: 0px 5px">
        <h1>QC - Master Data</h1>
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
                                <asp:BoundField DataField="MatType" HeaderText="MatType" ItemStyle-Width="150" />
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
    <fieldset runat="server" id="rmContent" visible="false" style="width: auto">
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
                                    <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputMtrlID" type="text" placeholder="Material ID*" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt">
                                    <asp:Label runat="server" ID="Label10" Text="Material Description* " />
                                </td>
                                <td style="text-align: left; width: 178px; height: 30px;">
                                    <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputMtrlDesc" type="text" placeholder="Material Description*" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt">
                                    <asp:Label runat="server" ID="Label2" Text="Base Unit of Meas.* " />
                                </td>
                                <td style="text-align: left; width: 178px; height: 30px;">
                                    <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputBsUntMeas" type="text" placeholder="Base Unit of Measure*" autocomplete="off" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
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
                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorOldNumb" runat="server" TargetControlID="inputOldMtrlNum" FilterType="Custom, Numbers" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt">
                                    <asp:Label runat="server" ID="Label5" Text="Plant " />
                                </td>
                                <td class="txtBox">
                                    <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputPlant" type="text" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
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
                                    <asp:Label runat="server" ID="Label22" Text="Inspect. Setup " />
                                </td>
                                <td style="width: 150px">
                                    <asp:CheckBox runat="server" ID="chkbxInspectSet" AutoPostBack="true" OnCheckedChanged="chkbxInspectSet_CheckedChanged" Checked="false" Enabled="false" />
                                </td>
                                <td colspan="2" style="width: 150px;">
                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblInspectSet" Text="Non Active" Width="180px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTd">
                                    <asp:Label runat="server" ID="Label3" Text="Inspect. Interval " />
                                </td>
                                <td style="width: 150px">
                                    <asp:TextBox runat="server" ID="inputInspectIntrv" type="text" placeholder="Inspection Interval" autocomplete="off" AutoPostBack="true" onkeydown="return(event.keyCode!=13);" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="validatorInspectIntrv" runat="server" TargetControlID="inputInspectIntrv" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                </td>
                                <td colspan="2" style="width: 150px;">
                                    <asp:Label CssClass="absoluteLabel" runat="server" ID="lblInspectIntrv" Text="DAY" Width="180px" />
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
                                                        <asp:TextBox ID="txtInspectType" runat="server" Text='<%#Eval("InspType") %>' Visible="false" Width="100px" autocomplete="off" />
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
                    <fieldset style="left: auto; top: auto; width: 100%; height: auto; right: auto; bottom: auto;">
                        <legend style="padding: 0px 2px 0px 2px">Classification</legend>
                        <table style="width: initial; height: auto; margin: auto auto 0px auto;" runat="server" id="tblClass">
                            <tr>
                                <td class="lblOtrDt" style="visibility: hidden; position: absolute">ID</td>
                                <td style="width: auto; height: 30px; padding-left: 18px">Class Type</td>
                                <td colspan="2" style="width: auto; height: 30px; padding-left: 37px">Class  </td>
                            </tr>
                            <tr>
                                <td style="visibility: hidden; position: absolute">
                                    <asp:Label ID="lblIDClassnTyp" runat="server" Width="30px" Text="0" />
                                </td>
                                <td>
                                    <asp:TextBox ID="inputClassTyp" runat="server" Width="100px" autocomplete="off" placeholder="Class Type" OnTextChanged="inputClassTyp_TextChanged" AutoPostBack="true" onkeydown="return(event.keyCode!=13);" ReadOnly="true" CssClass="txtBoxRO" Text="023" />
                                </td>
                                <%--<td style="width: 35px; height: 30px">
                                    <button style="background-image: url(../Images/src-white.png); background-repeat: no-repeat; background-position: center; background-size: 20px; height: 30px; width: 30px;" class="btn btn-info btn-lg" type="button" data-toggle="modal" data-target="#modalClassTyp"></button>
                                </td>--%>
                                <td>
                                    <asp:TextBox ID="inputClass" runat="server" Width="100px" autocomplete="off" placeholder="Class" AutoPostBack="true" OnTextChanged="inputClass_TextChanged" onkeydown="return(event.keyCode!=13);" />
                                </td>
                                <td style="width: 35px; height: 30px">
                                    <asp:UpdatePanel ID="Updatepanel2" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Button ID="LBSearchMatID" Visible="true" runat="server" CssClass="btn btn-info btn-sm srcIcon" data-toggle="modal" data-target="#modalClass" UseSubmitBehavior="true" OnClick="callingClassificationData" Height="30px" Width="35px"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Button ID="btnSync" class="btn btn-info btn-sm" runat="server" Text="Sync" OnClick="btnSync_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                    <asp:UpdatePanel  ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Button ID="btnCallClass" class="btn btn-info btn-sm" runat="server" Text="Sync" OnClick="callingClassificationData" CausesValidation="false" UseSubmitBehavior="false" Style="display: none;" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: auto">
                                    <asp:Button ID="btnAddUntMeas" class="btn btn-info btn-sm" runat="server" Text="Add" OnClick="btnAddClassnTyp_Click" CausesValidation="false" UseSubmitBehavior="false" />
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
                                                        <asp:TextBox ID="txtClassTyp" runat="server" Text='<%#Eval("ClassType") %>' Visible="false" Width="100px" autocomplete="off" />
                                                    </td>
                                                    <!--Class-->
                                                    <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                        <asp:Label ID="lblClassTbl" runat="server" Text='<%#Eval("ClassNo") %>' />
                                                        <asp:TextBox ID="txtClass" runat="server" Text='<%#Eval("ClassNo") %>' Visible="false" Width="100px" autocomplete="off" />
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
                                    <asp:Repeater ID="rptClassTypeDisplay" runat="server"
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
                                                        <asp:TextBox ID="txtClassTyp" runat="server" Text='<%#Eval("ClassType") %>' Visible="false" Width="100px" autocomplete="off" />
                                                    </td>
                                                    <!--Class-->
                                                    <td style="background-color: #EBEFF0; border-top: 1px dotted #8AE0F2; width: 100px">
                                                        <asp:Label ID="lblClassTbl" runat="server" Text='<%#Eval("ClassNo") %>' />
                                                        <asp:TextBox ID="txtClass" runat="server" Text='<%#Eval("ClassNo") %>' Visible="false" Width="100px" autocomplete="off" />
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
                </td>
            </tr>

            <!--Submit & Cancel Button-->
            <tr>
                <td colspan="2" style="text-align: right;">
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnUpdate" Text="Update" OnClick="Update_Click" Visible="false" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnReject" Text="Reject" OnClick="Reject_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelUpdate" Text="Cancel" OnClick="CancelUpdate_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="Save_Click" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancel" Text="Cancel" OnClick="Cancel_Click" CausesValidation="False" UseSubmitBehavior="false" />
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" CausesValidation="False" UseSubmitBehavior="false" Visible="false" />
                </td>
            </tr>
        </table>
    </fieldset>
    <!--LightBox Things-->
    <div class="container">
        <!-- Modal -->
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
                            BackColor="#EBEFF0" CellPadding="3" CellSpacing="2" BorderColor="Transparent" EnableViewState="true" >
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
                                        <asp:LinkButton runat="server" ID="LinkButton2" OnClick="selectClass_Click">Select This Class</asp:LinkButton>
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
    </div>
</asp:Content>
