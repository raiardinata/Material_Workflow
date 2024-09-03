<%@ Page Title="Quality Assurance Site" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="qaPage.aspx.cs" Inherits="testForm.Pages.qaPage" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="56000" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <script type = "text/javascript">
        function Confirm() {
            var oldMatNum = document.getElementById('<%=inputOldMtrlNum.ClientID%>').value;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (oldMatNum == "") {
                if (confirm("Old Material Number masih kosong, lanjutkan??")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                }
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <span runat="server" id="spanTransID" style="position: absolute; top: 95px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" />
    </span>
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblPosition" Text="User" />
        <asp:Label runat="server" ID="lblLogID" Text="LD00000000" />
        <asp:Label runat="server" ID="lblLine" Text="LN000" />
        <asp:Label runat="server" ID="FilterSearch" Text="" />
    </span>
    <!--ListView-->
    <div runat="server" id="listViewQA" style="padding: 0px 5px">
        <h1>QA - Master Data</h1>
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
    <div runat="server" id="rmContent" visible="false" style="position: relative;">
        <fieldset style="width: auto">
            <legend style="padding: 0px 2px 0px 2px">Request New Material - Quality Assurance</legend>
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
                                        <asp:TextBox runat="server" ID="inputOldMtrlNum" type="text" placeholder="Old Material Number" autocomplete="off" onkeydown="return(event.keyCode!=13);" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="validatorOldNumb" runat="server" TargetControlID="inputOldMtrlNum" FilterType="Custom, Numbers" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblBsDt">
                                        <asp:Label runat="server" ID="Label3" Text="Plant " />
                                    </td>
                                    <td class="txtBox">
                                        <asp:TextBox CssClass="txtBoxRO" runat="server" ID="inputPlant" type="text" ReadOnly="true" onkeydown="return(event.keyCode!=13);" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <!--Quality Management - Quality Control Data-->
                        <fieldset runat="server" visible="false" id="qmQualityControllDt" style="width: 100%; height: auto; top: auto; right: auto; bottom: auto; left: auto;">
                            <legend>Plant Data / Store 1 - Genereal Data</legend>
                            <table style="margin: 0px auto 5px 5px; width: initial">
                                <tr>
                                    <td class="lblTd">
                                        <asp:Label runat="server" ID="Label22" Text="Storage Condition " />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox runat="server" ID="inputStoreCond" type="text" placeholder="Storage Condition" autocomplete="off" AutoPostBack="true" OnTextChanged="inputStoreCond_onBlur" onkeypress="return this.value.length<=9" onkeydown="return(event.keyCode!=13);" />
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

                <!--Submit & Cancel Button-->
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnUpdate" Text="Update" OnClick="Update_Click" Visible="false" />
                        <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnReject" Text="Reject" OnClick="Reject_Click" Visible="false" CausesValidation="False" UseSubmitBehavior="false" />
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
    </div>
</asp:Content>
