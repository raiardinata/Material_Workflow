<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="defaultFGPage.aspx.cs" Inherits="testForm.Pages.defaultFGPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="lblType" Text="FG" />
    <span runat="server" id="spanTransID" style="position: absolute; top: 95px; right: 125px; display: none">Transaction ID :&nbsp;<asp:Label runat="server" ID="lblTransID" Text="RM00000000" />
    </span>
    <span style="font-size: 2rem; display: none; position: absolute">
        <asp:Label runat="server" ID="lblUser" Text="User" />
        <asp:Label runat="server" ID="lblPosition" Text="User" />
        
    </span>
    <fieldset style="width: initial; padding: 5px 5px">
        <legend style="padding: 0px 2px 0px 2px">Material Default Value - Finished Goods</legend>
        <table style="border-collapse: separate; width: initial; height: initial; margin: 0px auto 5px auto">
            <tr style="height: initial;">
                <td style="width: auto; height: auto; vertical-align: top; top: auto; right: auto; bottom: auto; left: auto; padding: 5px 7px 5px 0px">
                    <!--Accounting 1-->
                    <fieldset runat="server" id="acc1" style="width: 100%">
                        <legend>Accounting 1</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label1" Text="[Current Valuation] - [Price Unit]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputPriceUnit" type="text" placeholder="Price Unit" autocomplete="off" onkeypress="return this.value.length<=14" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <!--Basic Data 1-->
                    <fieldset runat="server" id="basicDt1" style="width: 100%">
                        <legend>Basic Data 1</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label2" Text="[General Data] - [Gen Item Cat Group]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputGenItemCatGroup" type="text" placeholder="Gen Item Cat Group" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label3" Text="[General Data] - [X-Plant Matl Status]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputXPlantMatStatus" type="text" placeholder="X-Plant Matl Status" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <!--Classifications-->
                    <fieldset runat="server" id="classifications" style="width: 100%">
                        <legend>Classifications</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label4" Text="[Assignment] - [Status]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputStatus" type="text" placeholder="Status" autocomplete="off" onkeypress="return this.value.length<=49" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <!--MRP-->
                    <fieldset runat="server" id="mrp" style="width: 100%">
                        <legend>MRP</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label5" Text="[General Data] - [Plant-sp.matl status]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputPlantSPStatus" type="text" placeholder="Plant-sp.matl status" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label6" Text="[Procurement] - [Batch Entry]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputBatchEntry" type="text" placeholder="Batch Entry" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label7" Text="[Forecast Requirement] - [Period Indicator]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputPeriodIndicator" type="text" placeholder="Period Indicator" autocomplete="off" onkeypress="return this.value.length<=49" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <!--Plant Data / Storage 1-->
                    <fieldset runat="server" id="plantDTStor1" style="width: 100%">
                        <legend>Plant Data / Stor 1</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label10" Text="[Shelf Life Data] - [Batch Management]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputBatchManagement" type="text" placeholder="Batch Management" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label11" Text="[Shelf Life Data] - [Period Ind. for SELD]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputPeriodIndForSELD" type="text" placeholder="Period Ind. for SELD" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <!--Sales: General / Plant-->
                    <fieldset runat="server" id="salesGnrPlant" style="width: 100%">
                        <legend>Sales: General / Plant</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label8" Text="[Shipping Data] - [Trans Group.]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputTransGrp" type="text" placeholder="Trans Group." autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <!--Sales: Sales Org 1-->
                    <fieldset runat="server" id="salesOrg1" style="width: 100%">
                        <legend>Sales: Sales Org 1</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label9" Text="[General Data] - [X-distr.chain Status]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputXDistrChainStatus" type="text" placeholder="X-distr.chain Status" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label12" Text="[Tax Data] - [Cash Discount]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputCashDiscount" type="text" placeholder="Cash Discount" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label13" Text="[Tax Data] - [Tax Classification]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputTaxClassification" type="text" placeholder="Tax Classification" autocomplete="off" onkeypress="return this.value.length<=14" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <!--Sales: Sales Org 2-->
                    <fieldset runat="server" id="salesOrg2" style="width: 100%">
                        <legend>Sales: Sales Org 2</legend>
                        <table style="margin: 0px auto 5px 5px; width: initial">
                            <tr>
                                <td class="lblBsDt" style="width: 310px">
                                    <asp:Label runat="server" ID="Label14" Text="[Grouping Terms] - [Matl Statistics Group]&nbsp;" />
                                </td>
                                <td class="txtBox" style="text-align: left;">
                                    <asp:TextBox runat="server" ID="inputMatlStatisticGroup" type="text" placeholder="Matl Statistics Group" autocomplete="off" onkeypress="return this.value.length<=9" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <!--Submit & Cancel Button-->
            <tr>
                <td style="text-align: right;">
                    <asp:Button CssClass="btn btn-info btn-lg" runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button CssClass="btn btn-info btn-danger" runat="server" ID="btnCancelSave" Text="Cancel" CausesValidation="False" UseSubmitBehavior="false" OnClick="btnCancelSave_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
