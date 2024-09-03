<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="CommonTable.aspx.cs" Inherits="testForm.Pages.CommonTable" %>

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

    <script type="text/javascript" language="javascript">
        function ConfirmYesNo() {
            if (confirm("Are you sure delete?") == true) {
                return true;
            }
            else {
                return false;
            }
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
            </span>
            <!--ListView-->
            <div runat="server" id="listViewExtend" style="padding: 0px 5px">
                <h1>Common Table</h1>
                <fieldset style="padding: 5px 5px">
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" ID="lsbxMatIDDESC" CssClass="btn btn-info btn-sm">
                                    <asp:ListItem Text="ProgramID" Selected="True" />
                                    <asp:ListItem Text="FunctionID" />
                                </asp:DropDownList>
                            </td>
                            <td style="width: auto; padding-right: 5px">
                                <asp:TextBox runat="server" ID="inputProgramFunctionID" placeholder="ProgramID/FunctionID" CssClass="txtBoxDesc" AutoComplete="off" />
                            </td>
                            <td style="width: auto">
                                <asp:ImageButton runat="server" ID="searchBtn" ImageUrl="../Images/src-white.png" Style="height: 30px; width: 40px;" class="btn btn-info btn-sm" type="button" OnClick="searchBtn_Click"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr></tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAdd" class="btn btn-default" runat="server" OnClick="btnAdd_Click" Text="Add New" />
                            </td>
                        </tr>
                    </table>



                    <!--Material ID and Desc List View-->
                    <div style="max-width: 1300px">
                        <div class="modal-content">
                            <div class="modal-body">
                                <asp:GridView ID="GridViewListView" runat="server"
                                    DataKeyNames="ProgramID, FunctionID , Seq"
                                    AutoGenerateColumns="False"
                                    BackColor="#EBEFF0" CellPadding="3" CellSpacing="2"
                                    BorderColor="Transparent" OnRowCommand="GridViewListView_RowCommand"
                                    OnPageIndexChanging="GridViewListView_PageIndexChanging">
                                    <FooterStyle ForeColor="#8C4510"
                                        BackColor="#F7DFB5"></FooterStyle>
                                    <PagerStyle ForeColor="#8C4510"
                                        HorizontalAlign="Center"></PagerStyle>
                                    <HeaderStyle ForeColor="#484848" Font-Bold="True" Font-Size="Large"
                                        BackColor="#8AE0F2"></HeaderStyle>
                                    <RowStyle Font-Size="Medium" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="140px" HeaderText="Transaction ID">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnEdit" Text="Update" CommandName="Edittttt" CommandArgument='<%#Eval("ProgramID") + "," + Eval("FunctionID") + "," + Eval("Seq") + "," + Eval("Opt") + "," + Eval("LOW") + "," + Eval("HIGH") %>' />



                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ProgramID" HeaderText="ProgramID" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="FunctionID" HeaderText="FunctionID" ItemStyle-Width="180" />
                                        <asp:BoundField DataField="Seq" HeaderText="Seq" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="Opt" HeaderText="Opt" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="LOW" HeaderText="LOW" ItemStyle-Width="130" />
                                        <asp:BoundField DataField="HIGH" HeaderText="HIGH" ItemStyle-Width="130" />

                                    </Columns>
                                </asp:GridView>

                                <div id="dataPager" runat="server" class="pagerstyle">
                                    <asp:Label ID="lblTotalRecords" runat="server" Text="Total Records:" CssClass="totalRecords" />
                                    <div id="paging" runat="server">
                                        <asp:Label ID="lblShowRows" runat="server" Text="Show rows:" />
                                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
                            CssClass="gotopage" Width="15px" />
                                        of
                        <asp:Label ID="lblTotalNumberOfPages" runat="server" />
                                        &nbsp;
                        <asp:Button ID="btnPrev" runat="server" Text="<" ToolTip="Previous Page" OnClick="btnPrev_OnClick" />
                                        <asp:Button ID="btnNext" runat="server" Text=">" CommandName="Page" ToolTip="Next Page" CommandArgument="Next"
                                            OnClick="btnNext_OnClick" />

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

            <!--Main Content-->
            <fieldset runat="server" id="rmContent" visible="false" style="width: auto; top: 20px">
                <legend style="padding: 10px 2px 10px 2px">Request Extent Plant - R&D</legend>




                <%--BRAND NEW REPORT MENU--%>
                <div class="container">

                    <%-- <ul class="nav nav-tabs">
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

                    </ul>--%>
                    <br>
                </div>

                <!--Submit & Cancel Button-->
                <table style="border-collapse: separate; width: initial; height: initial; margin: 0px 12px 5px auto">
                    <tr>
                        <td>&nbsp;
                           <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnClose" Text="Close" CausesValidation="False" UseSubmitBehavior="false" />

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
        <!--Insert And Update Common Table Modal-->
        <div class="modal fade" id="modalCommonTable" role="dialog">
            <div class="modal-dialog" style="max-width: 720px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" runat="server"></h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server" UpdateMode="Always">
                            <ContentTemplate>


                                <fieldset style="width: 500px">
                                    <legend id="legend1" runat="server">Form Insert</legend>
                                    <table style="width: 500px;" border='0' cellpadding="3" cellspacing="0">
                                        <tr>
                                            <td class="lblBsDt" style="width: 150px">Program ID</td>
                                            <td class="txtBox" style="width: 350px">
                                                <asp:TextBox ID="txtProgramID" runat="server" Text=""></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBsDt" style="width: 150px">Function ID</td>
                                            <td class="txtBox" style="width: 350px">
                                                <asp:TextBox ID="txtFunctionID" runat="server" Text=""></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBsDt" style="width: 150px">SeqNo</td>
                                            <td class="txtBox" style="width: 350px">
                                                <asp:TextBox ID="txtSeqNo" runat="server" Text=""></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBsDt" style="width: 150px">Option</td>
                                            <td class="txtBox" style="width: 350px">
                                                <asp:TextBox ID="txtOption" runat="server" Text=""></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBsDt" style="width: 150px">LOW</td>
                                            <td class="txtBox" style="width: 350px">
                                                <asp:TextBox ID="txtLow" runat="server" Text=""></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBsDt" style="width: 150px">HIGH</td>
                                            <td class="txtBox" style="width: 350px">
                                                <asp:TextBox ID="txtHigh" runat="server" Text=""></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnSave" class="btn btn-default" runat="server" Text="Save" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="Cancel" data-dismiss="modal" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="Lblsukses_update" runat="server" Text="Label" Visible="false"></asp:Label>
                                </fieldset>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>




    </div>



</asp:Content>

