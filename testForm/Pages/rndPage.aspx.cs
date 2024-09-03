using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using testForm.SQLConnect;

namespace testForm.Pages
{
    public partial class rndPage : Page
    {
        SQLConnect.SQLConnect sqlC = new SQLConnect.SQLConnect();
        string stringFxdPrice = "";
        string stringDontCost = "";
        string stringWithQtyStruct = "";
        string stringMatOrigin = "";
        string stringInspectSet = "0";
        string chkbxValue;

        string stringCoProd = "";
        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            //Bind Other Data Repeater
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty((String)Session["Usnam"]))
                {
                    Response.Redirect("~/Pages/loginPage");
                    return;
                }
                else
                {
                    lblUser.Text = Session["Usnam"].ToString().Trim();
                    lblPosition.Text = Session["Devisi"].ToString().Trim();
                }
                if (lblPosition.Text != "R&D" && lblPosition.Text != "R&D MGR" && lblPosition.Text != "PD" && lblPosition.Text != "PD MGR" && lblPosition.Text != "BD MGR")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
                srcListViewBinding();
                if (Session["Devisi"].ToString().Trim() == "R&D MGR" || Session["Devisi"].ToString().Trim() == "BD MGR")
                {
                    createMat.Visible = false;
                    tdCreateMaterial.Visible = false;
                    //rmsffgAspMenu.Visible = false;
                }
                //lblGridViewPage.Text = "Page " + (GridViewListView.PageIndex + 1) + " of " + GridViewListView.PageCount;
            }
            else if (IsPostBack)
            {
                int intIndexMenu = Convert.ToInt32(idxMenu.Text);
                //srcListViewBinding();
                srcSpcProcModalBinding();
                srcStorLocModalBinding();
            }
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        //ListViewMenu Code
        protected DataTable GetData_srcListViewRM(string Module_User, string MatSample, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Module_User", Module_User);
            param[1] = new SqlParameter("@MatSample", MatSample);
            param[2] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewRM", param);

            return dt;
        }
        protected DataTable GetData_srcListViewSF(string Module_User, string MatSample, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Module_User", Module_User);
            param[1] = new SqlParameter("@MatSample", MatSample);
            param[2] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewSF", param);

            return dt;
        }
        protected DataTable GetData_srcListViewFG(string Module_User, string MatSample, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Module_User", Module_User);
            param[1] = new SqlParameter("@MatSample", MatSample);
            param[2] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewFG", param);

            return dt;
        }
        protected void NavigationMenu_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                System.Web.UI.WebControls.MenuItem item = e.Item;
                if (item.Text == SiteMap.CurrentNode.Title)
                {
                    if (item.Parent != null)
                    {
                        item.Parent.Selected = true;
                    }
                    else
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        protected void NavigationMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value == "0")
            {
                lblSort.Text = "";
                lblSortDirection.Text = "";
                lblSortExpression.Text = "";
                FilterSearch.Text = "RM";
                gridSearch(FilterSearch.Text);
                //if (conMatWorkFlow.State == ConnectionState.Closed)
                //{
                //    conMatWorkFlow.Open();
                //}
                //SqlCommand cmd = new SqlCommand("srcListViewRM", conMatWorkFlow);
                //cmd.Parameters.AddWithValue("Module_User", "R&D");
                //cmd.Parameters.AddWithValue("MatSample", "");
                //cmd.Parameters.AddWithValue("Division", lblPosition.Text.Trim());
                //cmd.CommandType = CommandType.StoredProcedure;
                //var dataAdapter = new SqlDataAdapter();
                //dataAdapter.SelectCommand = cmd;

                //var commandBuilder = new SqlCommandBuilder(dataAdapter);
                //var ds = new DataSet();
                //dataAdapter.Fill(ds);

                //GridViewListView.DataSource = ds.Tables[0];
                //GridViewListView.DataBind();
                //conMatWorkFlow.Close();
            }
            else if (e.Item.Value == "1")
            {
                lblSort.Text = "";
                lblSortDirection.Text = "";
                lblSortExpression.Text = "";
                FilterSearch.Text = "SF";
                gridSearch(FilterSearch.Text);
                //if (conMatWorkFlow.State == ConnectionState.Closed)
                //{
                //    conMatWorkFlow.Open();
                //}
                //SqlCommand cmd = new SqlCommand("srcListViewSF", conMatWorkFlow);
                //cmd.Parameters.AddWithValue("Module_User", "R&D");
                //cmd.Parameters.AddWithValue("MatSample", "");
                //cmd.Parameters.AddWithValue("Division", lblPosition.Text.Trim());
                //cmd.CommandType = CommandType.StoredProcedure;
                //var dataAdapter = new SqlDataAdapter();
                //dataAdapter.SelectCommand = cmd;

                //var commandBuilder = new SqlCommandBuilder(dataAdapter);
                //var ds = new DataSet();
                //dataAdapter.Fill(ds);

                //GridViewListView.DataSource = ds.Tables[0];
                //GridViewListView.DataBind();
                //conMatWorkFlow.Close();
            }
            else if (e.Item.Value == "2")
            {
                lblSort.Text = "";
                lblSortDirection.Text = "";
                lblSortExpression.Text = "";
                FilterSearch.Text = "FG";
                gridSearch(FilterSearch.Text);
                //if (conMatWorkFlow.State == ConnectionState.Closed)
                //{
                //    conMatWorkFlow.Open();
                //}
                //SqlCommand cmd = new SqlCommand("srcListViewFG", conMatWorkFlow);
                //cmd.Parameters.AddWithValue("Module_User", "R&D");
                //cmd.Parameters.AddWithValue("MatSample", "");
                //cmd.Parameters.AddWithValue("Division", lblPosition.Text.Trim());
                //cmd.CommandType = CommandType.StoredProcedure;
                //var dataAdapter = new SqlDataAdapter();
                //dataAdapter.SelectCommand = cmd;

                //var commandBuilder = new SqlCommandBuilder(dataAdapter);
                //var ds = new DataSet();
                //dataAdapter.Fill(ds);

                //GridViewListView.DataSource = ds.Tables[0];
                //GridViewListView.DataBind();
                //conMatWorkFlow.Close();
            }
            rmsffgMenuLabel.Text = rmsffgAspMenu.SelectedItem.Text.Trim().ToUpper();
        }

        //Binding List View Code
        protected void createNewMat_Click(object sender, EventArgs e)
        {
            srcStorLocModalBinding();
            autoGenLogID();

            srcMatTypModalBinding("");
            srcPckgMatModalBinding();
            srcIndStdDescModalBinding();
            srcMatGrModalBinding();
            srcSalesOrgModalBinding();
            srcDistrChlModalBinding();
            srcAunModalBinding();
            srcBsUntMeasModalBinding();
            srcDivisionModalBinding();
            srcProcTypeModalBinding();
            srcVolUntModalBinding();
            srcWeightUntModalBinding();
            srcCommImpModalBinding();
            srcSpcProcModalBinding();
            srcNetWeightUntModalBinding();
            autoGenTransID();

            spanTransID.Style.Add("display", "flex");
            tmpBindRepeater();
            Master.FindControl("NavigationMenu").Visible = false;
            Master.FindControl("btnLogOut").Visible = false;
            if (lblPosition.Text == "Admin")
            {
                if (rmsffgMenuLabel.Text == "RM")
                {
                    listViewRnD.Visible = false;

                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                }
                else
                {
                    listViewRnD.Visible = false;

                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                    bscDt1Dimension.Visible = true;
                    MRP1LOTSizeDt.Visible = true;
                    foreignTradeDt.Visible = true;
                    plantShelfLifeDt.Visible = true;
                    trCoProd.Visible = true;

                    Label7.Text = "Packaging Material*";
                    inputPckgMat.Attributes.Add("required", "true");
                    inputPckgMat.Attributes.Add("placeholder", "Packaging Material*");
                    lblPckgMat.Text = "Packaging Mat Desc.*";
                    absPckgMat.Text = "Packaging Mat Desc.*";
                }
            }
            else
            {
                if (rmsffgMenuLabel.Text == "RM")
                {
                    listViewRnD.Visible = false;

                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                }
                else
                {
                    listViewRnD.Visible = false;

                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                    bscDt1Dimension.Visible = true;
                    MRP1LOTSizeDt.Visible = true;
                    foreignTradeDt.Visible = true;
                    plantShelfLifeDt.Visible = true;
                    trCoProd.Visible = true;

                    Label7.Text = "Packaging Material*";
                    inputPckgMat.Attributes.Add("required", "true");
                    inputPckgMat.Attributes.Add("placeholder", "Packaging Material*");
                    lblPckgMat.Text = "Packaging Mat Desc.*";
                    absPckgMat.Text = "Packaging Mat Desc.*";
                }
            }

            conMatWorkFlow.Open();
            //panggil sp yg load data comon table yg nyimpem type2 MRPType
            DataTable dt = new DataTable();

            SqlCommand cmdT = new SqlCommand("bindCommonTableGrossNWeuightUnt", conMatWorkFlow);
            cmdT.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmdT;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dt);
            conMatWorkFlow.Close();
            /*Controllers.mrpTypeControllers MRPTypeValidation = new Controllers.mrpTypeControllers();
            var MRPTypeVal = MRPTypeValidation.MRPTypeValidation();
            if (MRPTypeVal.Count() > 0)*/
            if (rmsffgMenuLabel.Text == dt.Rows[0]["HIGH"].ToString())
            {
                lblGrossWeight.Text = "Gross Weight";
                lblWeightUnit.Text = "Weight Unit";
                lblGrossWeight.Attributes.Remove("required");
                lblWeightUnit.Attributes.Remove("required");
            }
            else
            {
                lblGrossWeight.Text = "Gross Weight*";
                lblWeightUnit.Text = "Weight Unit*";
            }
        }
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
            lblSort.Text = "";
            lblSortDirection.Text = "";
            lblSortExpression.Text = "";
            srcListViewBinding();
        }
        protected DataTable GetData_srcListViewMatID(string inputMatIDDESC, string Module_User, string MatSample, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Module_User", Module_User);
            param[2] = new SqlParameter("@MatSample", MatSample);
            param[3] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatID", param);

            return dt;
        }
        protected DataTable GetData_srcListViewMatDESC(string inputMatIDDESC, string Module_User, string MatSample, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Module_User", Module_User);
            param[2] = new SqlParameter("@MatSample", MatSample);
            param[3] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatDESC", param);

            return dt;
        }
        protected void srcListViewBinding()
        {
            FilterSearch.Text = "ALL";
            gridSearch(FilterSearch.Text);
        }
        protected void modifyThis_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            spanTransID.Style.Add("display", "flex");
            Master.FindControl("NavigationMenu").Visible = false;
            Master.FindControl("btnLogOut").Visible = false;
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            LinkButton display = (LinkButton)grdrow.FindControl("display");
            string TransID = display.Text;

            lblUpdate.Text = "X";
            //MainContent
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            if (lblPosition.Text == "Admin")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["Type"].ToString().Trim() == "RM")
                        {
                            listViewRnD.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            orgLv.Visible = true;
                            MRP2Proc.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpd.Visible = true;

                            inputMatID.ReadOnly = true;
                            inputMatTyp.ReadOnly = true;
                            inputMatTyp.CssClass = "txtBoxRO";
                            inputMatID.CssClass = "txtBoxRO";

                            rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                            lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                            inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                            inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputBun.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                            inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                            inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                            inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim().ToUpper();
                            inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                            inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                            inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                            inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                            inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                            stringCoProd = dr["COProd"].ToString().Trim();
                            inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();
                        }
                        else
                        {
                            listViewRnD.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            orgLv.Visible = true;
                            MRP2Proc.Visible = true;
                            bscDt1Dimension.Visible = true;
                            MRP1LOTSizeDt.Visible = true;
                            foreignTradeDt.Visible = true;
                            plantShelfLifeDt.Visible = true;
                            trCoProd.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpd.Visible = true;

                            inputMatID.ReadOnly = true;
                            inputMatID.CssClass = "txtBoxRO";

                            rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                            lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                            inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                            inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputBun.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                            inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                            inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                            inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim().ToUpper();
                            inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                            inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                            inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                            inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                            inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                            stringCoProd = dr["COProd"].ToString().Trim();
                            inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                            inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim().ToUpper();
                            inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim().ToUpper();
                            inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim().ToUpper();
                            inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim().ToUpper();
                            inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim().ToUpper();
                            inputRoundValue.Text = dr["RoundingValue"].ToString().Trim().ToUpper();
                            ddListSLED.Text = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                            inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();
                        }
                        Label7.Text = "Packaging Material*";
                        inputPckgMat.Attributes.Add("required", "true");
                        inputPckgMat.Attributes.Add("placeholder", "Packaging Material*");
                        lblPckgMat.Text = "Packaging Mat Desc.*";
                        absPckgMat.Text = "Packaging Mat Desc.*";
                        if (ddListSLED.Text == "D")
                        {
                            lblMinRemShelfLife.Text = "DAY";
                        }
                        else
                        {
                            lblMinRemShelfLife.Text = "MONTH";
                        }
                        if (stringCoProd == "X")
                        {
                            chkbxCoProd.Checked = true;
                        }
                        else
                        {
                            chkbxCoProd.Checked = false;
                        }
                    }
                    conMatWorkFlow.Close();

                    //conMatWorkFlow.Open();
                    //SqlCommand cmdLOGTRANS = new SqlCommand("SELECT * FROM Tbl_LogTrans WHERE TransID = @TransID AND MaterialID=@MaterialID AND Usnam=@Usnam", conMatWorkFlow);
                    //cmdLOGTRANS.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "TransID",
                    //    Value = TransID,
                    //    SqlDbType = SqlDbType.NVarChar,
                    //    Size = 10
                    //});
                    //cmdLOGTRANS.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "MaterialID",
                    //    Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                    //    SqlDbType = SqlDbType.NVarChar,
                    //    Size = 18
                    //});
                    //cmdLOGTRANS.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "Usnam",
                    //    Value = this.lblUser.Text.Trim(),
                    //    SqlDbType = SqlDbType.NVarChar,
                    //    Size = 10
                    //});
                    //SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    //while (drLOGTRANS.Read())
                    //{
                    //    lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim().ToUpper();
                    //}
                    //conMatWorkFlow.Close();

                    conMatWorkFlow.Open();
                    SqlCommand cmdUOM = new SqlCommand("SELECT * FROM Tbl_DetailUomMat WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdUOM.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdUOM.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader drUOM = cmdUOM.ExecuteReader();
                    while (drUOM.Read())
                    {
                        lblLine.Text = drUOM["Line"].ToString().Trim().ToUpper();
                        string num1 = lblLine.Text;
                        num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                        lblLine.Text = num1;
                    }
                    conMatWorkFlow.Close();
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim().ToUpper(), this.Page, this);
                }
                bindLblBsUntMeas();
                bindLblDivision();
                bindLblMatGr();
                bindLblMatType();
                bindLblPackMat();
                bindLblPlant();
                bindLblSalesOrg();
                bindLblStorLoc();
                bindLblCommImp();
                bindLblDistrChl();
                bindLblProcType();
                bindLblIndStdDesc();
                bindLblSpecialProc();
            }
            else if (lblPosition.Text == "R&D")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["Type"].ToString().Trim() == "RM")
                        {
                            listViewRnD.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            orgLv.Visible = true;
                            MRP2Proc.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpd.Visible = true;

                            inputMatID.ReadOnly = true;
                            inputMatTyp.ReadOnly = true;
                            inputMatTyp.CssClass = "txtBoxRO";
                            inputMatID.CssClass = "txtBoxRO";

                            rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                            lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                            inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                            inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputBun.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                            inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                            inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                            inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim().ToUpper();
                            inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                            inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                            inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim().ToUpper();
                            inputRoundValue.Text = dr["RoundingValue"].ToString().Trim().ToUpper();
                            inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                            inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                            inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                            stringCoProd = dr["COProd"].ToString().Trim();
                            inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();

                            #region new approval 27 03 2019 by Rai
                            inputSupplierName.Text = dr["SupplierName"].ToString().Trim().ToUpper();
                            ddPurposeofUses.SelectedValue = dr["PurposeofUses"].ToString().Trim().ToUpper();
                            inputDoseEstimation.Text = dr["DoseEst"].ToString().Trim().ToUpper();
                            inputRMPotentialUseMonth.Text = dr["RMPotentialUseMonth"].ToString().Trim().ToUpper();
                            inputRMPotentialUseYear.Text = dr["RMPotentialUseYear"].ToString().Trim().ToUpper();
                            inputFMName.Text = dr["FMName"].ToString().Trim().ToUpper();
                            inputPrice.Text = dr["AddonPrice"].ToString().Trim().ToUpper();
                            inputPriceMOQ.Text = dr["PriceMOQ"].ToString().Trim().ToUpper();
                            inputPriceAbsorbed.Text = dr["PriceAbsorbed"].ToString().Trim().ToUpper();
                            inputStatus.Text = dr["Status"].ToString().Trim().ToUpper();
                            inputClientName.Text = dr["ClientName"].ToString().Trim().ToUpper();
                            inputFMForecastMonth.Text = dr["FMForecastEstMonth"].ToString().Trim().ToUpper();
                            inputFMAdoptionEstimationYear.Text = dr["FMAdoptionEstYear"].ToString().Trim().ToUpper();
                            inputPDMGRComment.Text = dr["PDMgrComment"].ToString().Trim().ToUpper();
                            ddPDMGRDecission.SelectedValue = dr["PDMgrDecission"].ToString().Trim().ToUpper();
                            inputBDMMGRComment.Text = dr["BDMgrComment"].ToString().Trim().ToUpper();
                            ddBDMMGRDecission.SelectedValue = dr["BDMgrDecission"].ToString().Trim().ToUpper();

                            if (inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
                            {
                                fieldsetAddon.Visible = true;
                                MRP1LOTSizeDt.Visible = true;
                            }
                            else
                            {
                                fieldsetAddon.Visible = false;
                                MRP1LOTSizeDt.Visible = false;
                            }
                            #endregion new approval 27 03 2019 by Rai
                        }
                        else
                        {
                            listViewRnD.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            orgLv.Visible = true;
                            MRP2Proc.Visible = true;
                            bscDt1Dimension.Visible = true;
                            MRP1LOTSizeDt.Visible = true;
                            foreignTradeDt.Visible = true;
                            plantShelfLifeDt.Visible = true;
                            trCoProd.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpd.Visible = true;

                            inputMatID.ReadOnly = true;
                            inputMatID.CssClass = "txtBoxRO";

                            rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                            lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                            inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                            inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputBun.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                            inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                            inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                            inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim().ToUpper();
                            inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                            inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                            inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                            inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                            inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                            stringCoProd = dr["COProd"].ToString().Trim();
                            inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                            inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim().ToUpper();
                            inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim().ToUpper();
                            inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim().ToUpper();
                            inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim().ToUpper();
                            inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim().ToUpper();
                            inputRoundValue.Text = dr["RoundingValue"].ToString().Trim().ToUpper();
                            ddListSLED.Text = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                            inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();
                        }
                        Label7.Text = "Packaging Material*";
                        inputPckgMat.Attributes.Add("required", "true");
                        inputPckgMat.Attributes.Add("placeholder", "Packaging Material*");
                        lblPckgMat.Text = "Packaging Mat Desc.*";
                        absPckgMat.Text = "Packaging Mat Desc.*";
                        if (ddListSLED.Text == "D")
                        {
                            lblMinRemShelfLife.Text = "DAY";
                        }
                        else
                        {
                            lblMinRemShelfLife.Text = "MONTH";
                        }
                        if (stringCoProd == "X")
                        {
                            chkbxCoProd.Checked = true;
                        }
                        else
                        {
                            chkbxCoProd.Checked = false;
                        }
                    }
                    conMatWorkFlow.Close();

                    //conMatWorkFlow.Open();
                    //SqlCommand cmdLOGTRANS = new SqlCommand("SELECT * FROM Tbl_LogTrans WHERE TransID = @TransID AND MaterialID=@MaterialID AND Usnam=@Usnam", conMatWorkFlow);
                    //cmdLOGTRANS.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "TransID",
                    //    Value = TransID,
                    //    SqlDbType = SqlDbType.NVarChar,
                    //    Size = 10
                    //});
                    //cmdLOGTRANS.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "MaterialID",
                    //    Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                    //    SqlDbType = SqlDbType.NVarChar,
                    //    Size = 18
                    //});
                    //cmdLOGTRANS.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "Usnam",
                    //    Value = this.lblUser.Text.Trim(),
                    //    SqlDbType = SqlDbType.NVarChar,
                    //    Size = 10
                    //});
                    //SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    //while (drLOGTRANS.Read())
                    //{
                    //    lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim().ToUpper();
                    //}
                    //conMatWorkFlow.Close();

                    conMatWorkFlow.Open();
                    SqlCommand cmdUOM = new SqlCommand("SELECT * FROM Tbl_DetailUomMat WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdUOM.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdUOM.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader drUOM = cmdUOM.ExecuteReader();
                    while (drUOM.Read())
                    {
                        lblLine.Text = drUOM["Line"].ToString().Trim().ToUpper();
                        string num1 = lblLine.Text;
                        num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                        lblLine.Text = num1;
                    }
                    conMatWorkFlow.Close();
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim().ToUpper(), this.Page, this);
                }
                bindLblBsUntMeas();
                bindLblDivision();
                bindLblMatGr();
                bindLblMatType();
                bindLblPackMat();
                bindLblPlant();
                bindLblSalesOrg();
                bindLblStorLoc();
                bindLblCommImp();
                bindLblDistrChl();
                bindLblProcType();
                bindLblIndStdDesc();
                bindLblSpecialProc();

                if (inputProcType.Text.ToUpper().Trim() == "E")
                {
                    chkbxCoProd.Enabled = true;
                }
                else
                {
                    chkbxCoProd.Enabled = false;
                    chkbxCoProd.Checked = false;
                }
            }
            else if (lblPosition.Text == "R&D MGR" || lblPosition.Text == "BD MGR")
            {
                #region new approval 27 03 2019 by Rai
                inputSupplierName.ReadOnly = true;
                inputDoseEstimation.ReadOnly = true;
                ddPurposeofUses.Enabled = false;
                inputRMPotentialUseMonth.ReadOnly = true;
                inputFMName.ReadOnly = true;
                inputPrice.ReadOnly = true;
                inputClientName.ReadOnly = true;
                inputFMForecastMonth.ReadOnly = true;
                #endregion new approval 27 03 2019 by Rai

                inputTotalLeadTime.ReadOnly = true;
                inputMatID.ReadOnly = true;
                inputMatDesc.ReadOnly = true;
                inputBsUntMeas.ReadOnly = true;
                inputMatGr.ReadOnly = true;
                inputOldMatNum.ReadOnly = true;
                inputDivision.ReadOnly = true;
                inputPckgMat.ReadOnly = true;
                inputCommImpCode.ReadOnly = true;
                inputMinRemShLf.ReadOnly = true;
                inputIndStdDesc.ReadOnly = true;
                chkbxCoProd.Enabled = false;
                inputTotalShelfLife.ReadOnly = true;
                inputGrossWeight.ReadOnly = true;
                inputNetWeight.ReadOnly = true;
                inputNetWeightUnit.ReadOnly = true;
                inputWeightUnt.ReadOnly = true;
                inputVolume.ReadOnly = true;
                inputVolUnt.ReadOnly = true;
                inputMatTyp.ReadOnly = true;
                inputPlant.ReadOnly = true;
                inputStorLoc.ReadOnly = true;
                inputSalesOrg.ReadOnly = true;
                inputDistrChl.ReadOnly = true;
                inputMatDesc.ReadOnly = true;
                inputMinLotSize.ReadOnly = true;
                inputRoundValue.ReadOnly = true;
                inputProcType.ReadOnly = true;
                inputSpcProc.ReadOnly = true;
                inputTotalLeadTime.CssClass = "txtBoxRO";
                inputIndStdDesc.CssClass = "txtBoxRO";
                inputSpcProc.CssClass = "txtBoxRO";
                inputMatID.CssClass = "txtBoxRO";
                inputMatDesc.CssClass = "txtBoxRO";
                inputBsUntMeas.CssClass = "txtBoxRO";
                inputMatGr.CssClass = "txtBoxRO";
                inputOldMatNum.CssClass = "txtBoxRO";
                inputDivision.CssClass = "txtBoxRO";
                inputPckgMat.CssClass = "txtBoxRO";
                inputCommImpCode.CssClass = "txtBoxRO";
                inputMinRemShLf.CssClass = "txtBoxRO";
                ddListSLED.CssClass = "txtBoxRO";
                inputTotalShelfLife.CssClass = "txtBoxRO";
                inputGrossWeight.CssClass = "txtBoxRO";
                inputNetWeight.CssClass = "txtBoxRO";
                inputNetWeightUnit.CssClass = "txtBoxRO";
                inputWeightUnt.CssClass = "txtBoxRO";
                inputVolume.CssClass = "txtBoxRO";
                inputVolUnt.CssClass = "txtBoxRO";
                inputMatTyp.CssClass = "txtBoxRO";
                inputPlant.CssClass = "txtBoxRO";
                inputStorLoc.CssClass = "txtBoxRO";
                inputSalesOrg.CssClass = "txtBoxRO";
                inputDistrChl.CssClass = "txtBoxRO";
                inputMatDesc.CssClass = "txtBoxRO";
                inputMinLotSize.CssClass = "txtBoxRO";
                inputRoundValue.CssClass = "txtBoxRO";
                inputProcType.CssClass = "txtBoxRO";

                #region new approval 27 03 2019 by Rai
                inputSupplierName.CssClass = "txtBoxRO";
                inputDoseEstimation.CssClass = "txtBoxRO";
                ddPurposeofUses.CssClass = "txtBoxRO";
                inputRMPotentialUseMonth.CssClass = "txtBoxRO";
                inputFMName.CssClass = "txtBoxRO";
                inputPrice.CssClass = "txtBoxRO";
                inputClientName.CssClass = "txtBoxRO";
                inputFMForecastMonth.CssClass = "txtBoxRO";
                inputRMPotentialUseYear.Attributes.Remove("style");
                inputPriceAbsorbed.Attributes.Remove("style");
                inputPriceMOQ.Attributes.Remove("style");
                inputFMAdoptionEstimationYear.Attributes.Remove("style");
                inputStatus.Attributes.Remove("style");
                inputRMPotentialUseYear.CssClass = "txtBoxRO";
                inputPriceAbsorbed.CssClass = "txtBoxRO";
                inputPriceMOQ.CssClass = "txtBoxRO";
                inputFMAdoptionEstimationYear.CssClass = "txtBoxRO";
                inputStatus.CssClass = "txtBoxRO";
                #endregion new approval 27 03 2019 by Rai

                inputTotalLeadTime.Attributes.Add("placeholder", "");
                inputSpcProc.Attributes.Add("placeholder", "");
                inputMatID.Attributes.Add("placeholder", "");
                inputMatDesc.Attributes.Add("placeholder", "");
                inputBsUntMeas.Attributes.Add("placeholder", "");
                inputMatGr.Attributes.Add("placeholder", "");
                inputOldMatNum.Attributes.Add("placeholder", "");
                inputDivision.Attributes.Add("placeholder", "");
                inputPckgMat.Attributes.Add("placeholder", "");
                inputCommImpCode.Attributes.Add("placeholder", "");
                inputMinRemShLf.Attributes.Add("placeholder", "");
                ddListSLED.Attributes.Add("placeholder", "");
                inputTotalShelfLife.Attributes.Add("placeholder", "");
                inputGrossWeight.Attributes.Add("placeholder", "");
                inputNetWeight.Attributes.Add("placeholder", "");
                inputNetWeightUnit.Attributes.Add("placeholder", "");
                inputWeightUnt.Attributes.Add("placeholder", "");
                inputVolume.Attributes.Add("placeholder", "");
                inputVolUnt.Attributes.Add("placeholder", "");
                inputMatTyp.Attributes.Add("placeholder", "");
                inputPlant.Attributes.Add("placeholder", "");
                inputStorLoc.Attributes.Add("placeholder", "");
                inputSalesOrg.Attributes.Add("placeholder", "");
                inputDistrChl.Attributes.Add("placeholder", "");
                inputMatDesc.Attributes.Add("placeholder", "");
                inputMinLotSize.Attributes.Add("placeholder", "");
                inputRoundValue.Attributes.Add("placeholder", "");
                inputProcType.Attributes.Add("placeholder", "");
                inputIndStdDesc.Attributes.Add("placeholder", "");

                #region new approval 27 03 2019 by Rai
                inputSupplierName.Attributes.Add("placeholder", "");
                inputDoseEstimation.Attributes.Add("placeholder", "");
                inputRMPotentialUseMonth.Attributes.Add("placeholder", "");
                inputFMName.Attributes.Add("placeholder", "");
                inputPrice.Attributes.Add("placeholder", "");
                inputClientName.Attributes.Add("placeholder", "");
                inputFMForecastMonth.Attributes.Add("placeholder", "");
                inputRMPotentialUseYear.Attributes.Add("placeholder", "");
                inputPriceMOQ.Attributes.Add("placeholder", "");
                inputPriceAbsorbed.Attributes.Add("placeholder", "");
                inputFMAdoptionEstimationYear.Attributes.Add("placeholder", "");
                inputStatus.Attributes.Add("placeholder", "");
                #endregion new approval 27 03 2019 by Rai

                imgBtnMatType.Visible = false;
                imgBtnIndStdDesc.Visible = false;
                imgBtnAun.Visible = false;
                imgBtnBsUntMeas.Visible = false;
                imgBtnCommImpCode.Visible = false;
                imgBtnCommImpCode.Visible = false;
                imgBtnDistrChl.Visible = false;
                imgBtnDivision.Visible = false;
                imgBtnMatGr.Visible = false;
                imgBtnMatType.Visible = false;
                imgBtnPckgMat.Visible = false;
                imgBtnPlant.Visible = false;
                imgBtnProcType.Visible = false;
                imgBtnSalesOrg.Visible = false;
                imgBtnSloc.Visible = false;
                imgBtnVolUnt.Visible = false;
                imgBtnWeightUnt.Visible = false;
                //imgBtnNetWeightUnt.Visible = false;
                imgBtnSpcProc.Visible = false;
                otrInputTbl.Visible = false;
                reptUntMeas.Visible = false;

                reptUntMeasMgr.Visible = true;

                rangeValidatorMinRemShelf.Enabled = false;

                if(lblPosition.Text == "R&D MGR")
                {
                    Label75.Visible = false;
                    inputBDMMGRComment.Visible = false;
                    Label77.Visible = false;
                    ddBDMMGRDecission.Visible = false;

                    inputPDMGRComment.ReadOnly = false;
                    inputPDMGRComment.CssClass = "";
                }
                else
                {
                    inputBDMMGRComment.ReadOnly = false;
                    inputBDMMGRComment.CssClass = "";
                }

                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["Type"].ToString().Trim() == "RM")
                        {
                            listViewRnD.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            orgLv.Visible = true;
                            MRP2Proc.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            #region new approval 09 05 2019 by Rai
                            if (lblUser.Text == "bdmgr" && String.IsNullOrEmpty(dr["IsCommentInit"].ToString()))
                            {
                                btnApprove.Visible = false;
                                btnSave.Visible = true;
                            }
                            else
                            {
                                btnApprove.Visible = true;
                            }
                            #endregion new approval 09 05 2019 by Rai
                            btnRjMd.Visible = true;
                            btnCancelApprove.Visible = true;

                            rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                            lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                            inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                            inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputBun.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                            inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                            inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                            inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim().ToUpper();
                            inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                            inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                            inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                            inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                            inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                            stringCoProd = dr["COProd"].ToString().Trim();
                            inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();
                            inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim().ToUpper();
                            inputRoundValue.Text = dr["RoundingValue"].ToString().Trim().ToUpper();

                            #region new approval 27 03 2019 by Rai
                            inputSupplierName.Text = dr["SupplierName"].ToString().Trim().ToUpper();
                            ddPurposeofUses.SelectedValue = dr["PurposeofUses"].ToString().Trim().ToUpper();
                            inputDoseEstimation.Text = dr["DoseEst"].ToString().Trim().ToUpper();
                            inputRMPotentialUseMonth.Text = dr["RMPotentialUseMonth"].ToString().Trim().ToUpper();
                            inputRMPotentialUseYear.Text = dr["RMPotentialUseYear"].ToString().Trim().ToUpper();
                            inputFMName.Text = dr["FMName"].ToString().Trim().ToUpper();
                            inputPrice.Text = dr["AddonPrice"].ToString().Trim().ToUpper();
                            inputPriceMOQ.Text = dr["PriceMOQ"].ToString().Trim().ToUpper();
                            inputPriceAbsorbed.Text = dr["PriceAbsorbed"].ToString().Trim().ToUpper();
                            inputStatus.Text = dr["Status"].ToString().Trim().ToUpper();
                            inputClientName.Text = dr["ClientName"].ToString().Trim().ToUpper();
                            inputFMForecastMonth.Text = dr["FMForecastEstMonth"].ToString().Trim().ToUpper();
                            inputFMAdoptionEstimationYear.Text = dr["FMAdoptionEstYear"].ToString().Trim().ToUpper();
                            inputPDMGRComment.Text = dr["PDMgrComment"].ToString().Trim().ToUpper();
                            ddPDMGRDecission.SelectedValue = dr["PDMgrDecission"].ToString().Trim().ToUpper();
                            inputBDMMGRComment.Text = dr["BDMgrComment"].ToString().Trim().ToUpper();
                            ddBDMMGRDecission.SelectedValue = dr["BDMgrDecission"].ToString().Trim().ToUpper();

                            if (inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
                            {
                                fieldsetAddon.Visible = true;
                                MRP1LOTSizeDt.Visible = true;
                            }
                            else
                            {
                                fieldsetAddon.Visible = false;
                                MRP1LOTSizeDt.Visible = false;
                            }
                            if (ddPDMGRDecission.SelectedValue == "1" && ddBDMMGRDecission.SelectedValue == "2" && lblPosition.Text == "BD MGR")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please wait for PD Manager Approval to Proceed.'); window.location='rndPage';", true);
                                return;
                            }
                            else if (ddPDMGRDecission.SelectedValue == "1" && ddBDMMGRDecission.SelectedValue == "3" && lblPosition.Text == "R&D MGR")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please wait for BD Manager Approval to Proceed.'); window.location='rndPage';", true);
                                return;
                            }
                            #endregion new approval 27 03 2019 by Rai
                        }
                        else
                        {
                            listViewRnD.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            orgLv.Visible = true;
                            MRP2Proc.Visible = true;
                            bscDt1Dimension.Visible = true;
                            MRP1LOTSizeDt.Visible = true;
                            foreignTradeDt.Visible = true;
                            plantShelfLifeDt.Visible = true;
                            trCoProd.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            btnApprove.Visible = true;
                            btnCancelApprove.Visible = true;
                            btnRjMd.Visible = true;

                            rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                            lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();

                            inputMatTyp.Text = dr["MatType"].ToString().Trim().ToUpper();
                            inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                            inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputBun.Text = dr["UoM"].ToString().Trim().ToUpper();
                            inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                            inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                            inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                            inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();

                            inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                            inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim().ToUpper();

                            inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();

                            inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                            inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                            inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                            inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();

                            inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim().ToUpper();
                            inputRoundValue.Text = dr["RoundingValue"].ToString().Trim().ToUpper();

                            stringCoProd = dr["COProd"].ToString().Trim();
                            inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();

                            inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim().ToUpper();
                            ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                            inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim().ToUpper();

                            inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim().ToUpper();

                            if (lblPosition.Text == "BD MGR")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You do not have authorization for this material.'); window.location='rndPage';", true);
                                return;
                            }
                        }
                        Label7.Text = "Packaging Material*";
                        inputPckgMat.Attributes.Add("required", "true");
                        inputPckgMat.Attributes.Add("placeholder", "Packaging Material*");
                        lblPckgMat.Text = "Packaging Mat Desc.*";
                        absPckgMat.Text = "Packaging Mat Desc.*";
                        if (ddListSLED.Text == "D")
                        {
                            lblMinRemShelfLife.Text = "DAY";
                        }
                        else
                        {
                            lblMinRemShelfLife.Text = "MONTH";
                        }
                        if (stringCoProd == "X")
                        {
                            chkbxCoProd.Checked = true;
                        }
                        else if (stringCoProd == "")
                        {
                            chkbxCoProd.Checked = false;
                        }
                    }
                    conMatWorkFlow.Close();

                    //conMatWorkFlow.Open();
                    //SqlCommand cmdLOGTRANS = new SqlCommand("SELECT * FROM Tbl_LogTrans WHERE TransID = @TransID", conMatWorkFlow);
                    //cmdLOGTRANS.Parameters.Add(new SqlParameter
                    //{
                    //    ParameterName = "TransID",
                    //    Value = TransID,
                    //    SqlDbType = SqlDbType.NVarChar,
                    //    Size = 10
                    //});
                    //SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    //while (drLOGTRANS.Read())
                    //{
                    //    lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim().ToUpper();
                    //}
                    //conMatWorkFlow.Close();

                    conMatWorkFlow.Open();
                    SqlCommand cmdUOM = new SqlCommand("SELECT * FROM Tbl_DetailUomMat WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdUOM.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdUOM.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader drUOM = cmdUOM.ExecuteReader();
                    while (drUOM.Read())
                    {
                        lblLine.Text = drUOM["Line"].ToString().Trim().ToUpper();
                        string num1 = lblLine.Text;
                        num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                        lblLine.Text = num1;
                    }

                    ListItem li = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
                    ddListSLED.Items.Clear();
                    ddListSLED.Items.Add(li);

                    conMatWorkFlow.Close();

                    bindLblBsUntMeas();
                    bindLblDivision();
                    bindLblMatGr();
                    bindLblMatType();
                    bindLblPackMat();
                    bindLblPlant();
                    bindLblSalesOrg();
                    bindLblStorLoc();
                    bindLblCommImp();
                    bindLblDistrChl();
                    bindLblProcType();
                    bindLblIndStdDesc();
                    bindLblSpecialProc();
                }
                catch (Exception exMgr)
                {
                    MsgBox(exMgr.ToString().Trim().ToUpper(), this.Page, this);
                }
            }
            else
            {
                MsgBox(this.lblUser.Text + " username are not for R&D division", this.Page, this);
                Response.Redirect("~/Pages/rndPage");
            }
            //conMatWorkFlow.Close();

            tmpBindRepeater();
            srcMatTypModalBinding("");
            srcPckgMatModalBinding();
            srcIndStdDescModalBinding();
            srcMatGrModalBinding();
            srcSalesOrgModalBinding();
            srcStorLocModalBinding();
            srcDistrChlModalBinding();
            srcAunModalBinding();
            srcBsUntMeasModalBinding();
            srcDivisionModalBinding();
            srcProcTypeModalBinding();
            srcVolUntModalBinding();
            srcWeightUntModalBinding();
            srcCommImpModalBinding();
            srcSpcProcModalBinding();
            srcNetWeightUntModalBinding();
            srcPlantModalBinding();

            conMatWorkFlow.Open();
            SqlCommand cmdA = new SqlCommand("SELECT GrossWeight FROM Tbl_DetailUomMat WHERE TransID = @TransID AND MaterialID = @MaterialID AND Aun = @Bun", conMatWorkFlow);
            cmdA.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = TransID,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdA.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = grdrow.Cells[1].Text.Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdA.Parameters.Add(new SqlParameter
            {
                ParameterName = "Bun",
                Value = this.inputBun.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            SqlDataReader drA = cmdA.ExecuteReader();
            while (drA.Read())
            {
                inputGrossWeight.Text = drA["GrossWeight"].ToString().Trim();
                inputGrossWeight.CssClass = "txtBoxRO";
            }
            if (drA.HasRows == false)
            {
                inputGrossWeight.Text = "0";
            }
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            //panggil sp yg load data comon table yg nyimpem type2 MRPType
            DataTable dt = new DataTable();

            SqlCommand cmdT = new SqlCommand("bindCommonTableGrossNWeuightUnt", conMatWorkFlow);
            cmdT.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmdT;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dt);
            conMatWorkFlow.Close();
            /*Controllers.mrpTypeControllers MRPTypeValidation = new Controllers.mrpTypeControllers();
            var MRPTypeVal = MRPTypeValidation.MRPTypeValidation();
            if (MRPTypeVal.Count() > 0)*/
            if (rmsffgMenuLabel.Text == dt.Rows[0]["HIGH"].ToString())
            {
                lblGrossWeight.Text = "Gross Weight";
                lblWeightUnit.Text = "Weight Unit";
                lblGrossWeight.Attributes.Remove("required");
                lblWeightUnit.Attributes.Remove("required");
            }
            else
            {
                lblGrossWeight.Text = "Gross Weight*";
                lblWeightUnit.Text = "Weight Unit*";
            }
            if (inputMatTyp.Text == "SFAT" || inputMatTyp.Text == "sfat")
            {
                MinMaxMatID.ValidationExpression = "^[-_,.A-Za-z0-9]{7,18}$";
                MinMaxMatID.ErrorMessage = "Minimum 7, maximum 18 characters required and no special characters.";
            }
            else
            {
                MinMaxMatID.ValidationExpression = "^[-_,.A-Za-z0-9]{8,18}$";
                MinMaxMatID.ErrorMessage = "Minimum 8, maximum 18 characters required and no special characters.";
            }
        }
        protected void display_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            spanTransID.Style.Add("display", "flex");
            Master.FindControl("NavigationMenu").Visible = false;
            Master.FindControl("btnLogOut").Visible = false;
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            LinkButton display = (LinkButton)grdrow.FindControl("display");
            string TransID = display.Text;
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            #region new approval 27 03 2019 by Rai
            inputSupplierName.ReadOnly = true;
            inputDoseEstimation.ReadOnly = true;
            ddPurposeofUses.Enabled = false;
            inputRMPotentialUseMonth.ReadOnly = true;
            inputFMName.ReadOnly = true;
            inputPrice.ReadOnly = true;
            inputClientName.ReadOnly = true;
            inputFMForecastMonth.ReadOnly = true;
            #endregion new approval 27 03 2019 by Rai

            inputTotalLeadTime.ReadOnly = true;
            inputMatID.ReadOnly = true;
            inputMatDesc.ReadOnly = true;
            inputBsUntMeas.ReadOnly = true;
            inputMatGr.ReadOnly = true;
            inputOldMatNum.ReadOnly = true;
            inputDivision.ReadOnly = true;
            inputPckgMat.ReadOnly = true;
            inputCommImpCode.ReadOnly = true;
            inputMinRemShLf.ReadOnly = true;
            inputIndStdDesc.ReadOnly = true;
            chkbxCoProd.Enabled = false;
            inputTotalShelfLife.ReadOnly = true;
            inputGrossWeight.ReadOnly = true;
            inputNetWeight.ReadOnly = true;
            inputNetWeightUnit.ReadOnly = true;
            inputWeightUnt.ReadOnly = true;
            inputVolume.ReadOnly = true;
            inputVolUnt.ReadOnly = true;
            inputMatTyp.ReadOnly = true;
            inputPlant.ReadOnly = true;
            inputStorLoc.ReadOnly = true;
            inputSalesOrg.ReadOnly = true;
            inputDistrChl.ReadOnly = true;
            inputMatDesc.ReadOnly = true;
            inputMinLotSize.ReadOnly = true;
            inputRoundValue.ReadOnly = true;
            inputProcType.ReadOnly = true;
            inputSpcProc.ReadOnly = true;

            #region new approval 27 03 2019 by Rai
            inputSupplierName.CssClass = "txtBoxRO";
            inputDoseEstimation.CssClass = "txtBoxRO";
            ddPurposeofUses.CssClass = "txtBoxRO";
            inputRMPotentialUseMonth.CssClass = "txtBoxRO";
            inputFMName.CssClass = "txtBoxRO";
            inputPrice.CssClass = "txtBoxRO";
            inputClientName.CssClass = "txtBoxRO";
            inputFMForecastMonth.CssClass = "txtBoxRO";
            inputRMPotentialUseYear.Attributes.Remove("style");
            inputPriceAbsorbed.Attributes.Remove("style");
            inputPriceMOQ.Attributes.Remove("style");
            inputFMAdoptionEstimationYear.Attributes.Remove("style");
            inputStatus.Attributes.Remove("style");
            inputRMPotentialUseYear.CssClass = "txtBoxRO";
            inputPriceAbsorbed.CssClass = "txtBoxRO";
            inputPriceMOQ.CssClass = "txtBoxRO";
            inputFMAdoptionEstimationYear.CssClass = "txtBoxRO";
            inputStatus.CssClass = "txtBoxRO";
            #endregion new approval 27 03 2019 by Rai

            inputTotalLeadTime.CssClass = "txtBoxRO";
            inputIndStdDesc.CssClass = "txtBoxRO";
            inputSpcProc.CssClass = "txtBoxRO";
            inputMatID.CssClass = "txtBoxRO";
            inputMatDesc.CssClass = "txtBoxRO";
            inputBsUntMeas.CssClass = "txtBoxRO";
            inputMatGr.CssClass = "txtBoxRO";
            inputOldMatNum.CssClass = "txtBoxRO";
            inputDivision.CssClass = "txtBoxRO";
            inputPckgMat.CssClass = "txtBoxRO";
            inputCommImpCode.CssClass = "txtBoxRO";
            inputMinRemShLf.CssClass = "txtBoxRO";
            ddListSLED.CssClass = "txtBoxRO";
            inputTotalShelfLife.CssClass = "txtBoxRO";
            inputGrossWeight.CssClass = "txtBoxRO";
            inputNetWeight.CssClass = "txtBoxRO";
            inputNetWeightUnit.CssClass = "txtBoxRO";
            inputWeightUnt.CssClass = "txtBoxRO";
            inputVolume.CssClass = "txtBoxRO";
            inputVolUnt.CssClass = "txtBoxRO";
            inputMatTyp.CssClass = "txtBoxRO";
            inputPlant.CssClass = "txtBoxRO";
            inputStorLoc.CssClass = "txtBoxRO";
            inputSalesOrg.CssClass = "txtBoxRO";
            inputDistrChl.CssClass = "txtBoxRO";
            inputMatDesc.CssClass = "txtBoxRO";
            inputMinLotSize.CssClass = "txtBoxRO";
            inputRoundValue.CssClass = "txtBoxRO";
            inputProcType.CssClass = "txtBoxRO";

            #region new approval 27 03 2019 by Rai
            inputSupplierName.Attributes.Add("placeholder", "");
            inputDoseEstimation.Attributes.Add("placeholder", "");
            inputRMPotentialUseMonth.Attributes.Add("placeholder", "");
            inputFMName.Attributes.Add("placeholder", "");
            inputPrice.Attributes.Add("placeholder", "");
            inputClientName.Attributes.Add("placeholder", "");
            inputFMForecastMonth.Attributes.Add("placeholder", "");
            #endregion new approval 27 03 2019 by Rai

            inputTotalLeadTime.Attributes.Add("placeholder", "");
            inputSpcProc.Attributes.Add("placeholder", "");
            inputMatID.Attributes.Add("placeholder", "");
            inputMatDesc.Attributes.Add("placeholder", "");
            inputBsUntMeas.Attributes.Add("placeholder", "");
            inputMatGr.Attributes.Add("placeholder", "");
            inputOldMatNum.Attributes.Add("placeholder", "");
            inputDivision.Attributes.Add("placeholder", "");
            inputPckgMat.Attributes.Add("placeholder", "");
            inputCommImpCode.Attributes.Add("placeholder", "");
            inputMinRemShLf.Attributes.Add("placeholder", "");
            ddListSLED.Attributes.Add("placeholder", "");
            inputTotalShelfLife.Attributes.Add("placeholder", "");
            inputGrossWeight.Attributes.Add("placeholder", "");
            inputNetWeight.Attributes.Add("placeholder", "");
            inputNetWeightUnit.Attributes.Add("placeholder", "");
            inputWeightUnt.Attributes.Add("placeholder", "");
            inputVolume.Attributes.Add("placeholder", "");
            inputVolUnt.Attributes.Add("placeholder", "");
            inputMatTyp.Attributes.Add("placeholder", "");
            inputPlant.Attributes.Add("placeholder", "");
            inputStorLoc.Attributes.Add("placeholder", "");
            inputSalesOrg.Attributes.Add("placeholder", "");
            inputDistrChl.Attributes.Add("placeholder", "");
            inputMatDesc.Attributes.Add("placeholder", "");
            inputMinLotSize.Attributes.Add("placeholder", "");
            inputRoundValue.Attributes.Add("placeholder", "");
            inputProcType.Attributes.Add("placeholder", "");
            inputIndStdDesc.Attributes.Add("placeholder", "");

            imgBtnIndStdDesc.Visible = false;
            imgBtnAun.Visible = false;
            imgBtnBsUntMeas.Visible = false;
            imgBtnCommImpCode.Visible = false;
            imgBtnCommImpCode.Visible = false;
            imgBtnDistrChl.Visible = false;
            imgBtnDivision.Visible = false;
            imgBtnMatGr.Visible = false;
            imgBtnMatType.Visible = false;
            imgBtnPckgMat.Visible = false;
            imgBtnPlant.Visible = false;
            imgBtnProcType.Visible = false;
            imgBtnSalesOrg.Visible = false;
            imgBtnSloc.Visible = false;
            imgBtnVolUnt.Visible = false;
            imgBtnWeightUnt.Visible = false;
            //imgBtnNetWeightUnt.Visible = false;
            imgBtnSpcProc.Visible = false;
            otrInputTbl.Visible = false;
            reptUntMeas.Visible = false;

            divApprMn.Visible = true;
            reptUntMeasMgr.Visible = true;

            btnSave.Visible = false;
            btnCancelSave.Visible = false;
            btnClose.Visible = true;

            rptClassTypeMgr.Visible = true;
            rptInspectionTypeMgr.Visible = true;
            fieldsetAddon.Visible = true;

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = TransID,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = grdrow.Cells[1].Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listViewRnD.Visible = false;

                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                    bscDt1Dimension.Visible = true;
                    MRP1LOTSizeDt.Visible = true;
                    foreignTradeDt.Visible = true;
                    plantShelfLifeDt.Visible = true;
                    trCoProd.Visible = true;

                    #region new approval 27 03 2019 by Rai
                    inputSupplierName.Text              = dr["SupplierName"].ToString().Trim().ToUpper();
                    ddPurposeofUses.SelectedValue       = dr["PurposeofUses"].ToString().Trim().ToUpper();
                    inputDoseEstimation.Text            = dr["DoseEst"].ToString().Trim().ToUpper();
                    inputRMPotentialUseMonth.Text       = dr["RMPotentialUseMonth"].ToString().Trim().ToUpper();
                    inputRMPotentialUseYear.Text        = dr["RMPotentialUseYear"].ToString().Trim().ToUpper();
                    inputFMName.Text                    = dr["FMName"].ToString().Trim().ToUpper();
                    inputPrice.Text                     = dr["AddonPrice"].ToString().Trim().ToUpper();
                    inputPriceMOQ.Text                  = dr["PriceMOQ"].ToString().Trim().ToUpper();
                    inputPriceAbsorbed.Text             = dr["PriceAbsorbed"].ToString().Trim().ToUpper();
                    inputStatus.Text                    = dr["Status"].ToString().Trim().ToUpper();
                    inputClientName.Text                = dr["ClientName"].ToString().Trim().ToUpper();
                    inputFMForecastMonth.Text           = dr["FMForecastEstMonth"].ToString().Trim().ToUpper();
                    inputFMAdoptionEstimationYear.Text  = dr["FMAdoptionEstYear"].ToString().Trim().ToUpper();
                    inputPDMGRComment.Text              = dr["PDMgrComment"].ToString().Trim().ToUpper();
                    ddPDMGRDecission.SelectedValue      = dr["PDMgrDecission"].ToString().Trim().ToUpper();
                    inputBDMMGRComment.Text             = dr["BDMgrComment"].ToString().Trim().ToUpper();
                    ddBDMMGRDecission.SelectedValue     = dr["BDMgrDecission"].ToString().Trim().ToUpper();
                    #endregion new approval 27 03 2019 by Rai

                    rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                    lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                    inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                    inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                    inputBsUntMeas.Text = dr["UoM"].ToString().Trim().ToUpper();
                    inputBun.Text = dr["UoM"].ToString().Trim().ToUpper();
                    inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                    inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                    inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                    inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                    inputMatTyp.Text = dr["MatType"].ToString().Trim().ToUpper();
                    inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                    inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                    inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                    inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                    inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                    stringCoProd = dr["COProd"].ToString().Trim();
                    inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                    inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim().ToUpper();
                    inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim().ToUpper();
                    inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim().ToUpper();
                    inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim().ToUpper();
                    inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim().ToUpper();
                    inputRoundValue.Text = dr["RoundingValue"].ToString().Trim().ToUpper();
                    ddListSLED.Text = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                    inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();
                    inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                    inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                    inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                    inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                    inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();
                    inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();

                    if (stringCoProd == "X")
                    {
                        chkbxCoProd.Checked = true;
                    }
                    else if (stringCoProd == "")
                    {
                        chkbxCoProd.Checked = false;
                    }
                    if (ddListSLED.Text == "D")
                    {
                        lblMinRemShelfLife.Text = "DAY";
                    }
                    else
                    {
                        lblMinRemShelfLife.Text = "MONTH";
                    }

                    inputLabOffice.Text = dr["LabOffice"].ToString().Trim();
                    inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                    inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                    inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                    inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                    inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                    inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();
                    inputProdStorLoc.Text = dr["ProdSLoc"].ToString().Trim();
                    inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                    inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                    inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();
                    inputStrtgyGr.Text = dr["PlanStrategyGroup"].ToString().Trim();
                    inputTotalLeadTime.Text = dr["TotLeadTime"].ToString().Trim();
                    inputProdSched.Text = dr["ProdSched"].ToString().Trim();
                    inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString().Trim();

                    stringInspectSet = dr["InspectionSetup"].ToString().Trim();
                    inputInspectIntrv.Text = dr["InspectionInterval"].ToString().Trim();

                    if (stringInspectSet == "X")
                    {
                        chkbxInspectSet.Checked = true;
                        lblInspectSet.Text = "Active";
                    }
                    else
                    {
                        chkbxInspectSet.Checked = false;
                        lblInspectSet.Text = "Non Active";
                    }

                    inputStoreCond.Text = dr["StorConditions"].ToString().Trim();

                    inputQMCtrlKey.Text = dr["QMControlKey"].ToString().Trim();
                    lblChkbx.Text = dr["QMProcActive"].ToString().Trim();

                    if (lblChkbx.Text == "X")
                    {
                        chkbxQMProcActive.Checked = true;
                        lblQMProcActive.Text = "Active";
                    }
                    else
                    {
                        chkbxQMProcActive.Checked = false;
                        lblQMProcActive.Text = "Non Active";
                    }
                }
                conMatWorkFlow.Close();

                //conMatWorkFlow.Open();
                //SqlCommand cmdLOGTRANS = new SqlCommand("SELECT * FROM Tbl_LogTrans WHERE TransID = @TransID", conMatWorkFlow);
                //cmdLOGTRANS.Parameters.Add(new SqlParameter
                //{
                //    ParameterName = "TransID",
                //    Value = TransID,
                //    SqlDbType = SqlDbType.NVarChar,
                //    Size = 10
                //});
                //SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                //while (drLOGTRANS.Read())
                //{
                //    lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim().ToUpper();
                //}
                //conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdUOM = new SqlCommand("SELECT * FROM Tbl_DetailUomMat WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                cmdUOM.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = TransID,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdUOM.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                SqlDataReader drUOM = cmdUOM.ExecuteReader();
                while (drUOM.Read())
                {
                    lblLine.Text = drUOM["Line"].ToString().Trim().ToUpper();
                    string num1 = lblLine.Text;
                    num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                    lblLine.Text = num1;
                }
                conMatWorkFlow.Close();


                ListItem li = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
                ddListSLED.Items.Clear();
                ddListSLED.Items.Add(li);
            }
            catch (Exception exMgr)
            {
                MsgBox(exMgr.ToString().Trim().ToUpper(), this.Page, this);
            }
            tmpBindRepeater();

            bindLblBsUntMeas();
            bindLblDivision();
            bindLblMatGr();
            bindLblMatType();
            bindLblPackMat();
            bindLblPlant();
            bindLblSalesOrg();
            bindLblStorLoc();
            bindLblCommImp();
            bindLblDistrChl();
            bindLblProcType();
            bindLblIndStdDesc();
            bindLblSpecialProc();
            QCDataBindRepeater();
            ClassBindRepeater();
        }
        protected void checkStatsNGlobStats_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indexRDStart = GetColumnIndexByName(e.Row, "RDStart");
                string columnRDStart = e.Row.Cells[indexRDStart].Text;
                int indexRDEnd = GetColumnIndexByName(e.Row, "RDEnd");
                string columnRDEnd = e.Row.Cells[indexRDEnd].Text;
                int indexProcStart = GetColumnIndexByName(e.Row, "ProcStart");
                string columnProcStart = e.Row.Cells[indexProcStart].Text;
                int indexProcEnd = GetColumnIndexByName(e.Row, "ProcEnd");
                string columnProcEnd = e.Row.Cells[indexProcEnd].Text;
                int indexPlanStart = GetColumnIndexByName(e.Row, "PlanStart");
                string columnPlanStart = e.Row.Cells[indexPlanStart].Text;
                int indexPlanEnd = GetColumnIndexByName(e.Row, "PlanEnd");
                string columnPlanEnd = e.Row.Cells[indexPlanEnd].Text;
                int indexQCStart = GetColumnIndexByName(e.Row, "QCStart");
                string columnQCStart = e.Row.Cells[indexQCStart].Text;
                int indexQCEnd = GetColumnIndexByName(e.Row, "QCEnd");
                string columnQCEnd = e.Row.Cells[indexQCEnd].Text;
                int indexQAStart = GetColumnIndexByName(e.Row, "QAStart");
                string columnQAStart = e.Row.Cells[indexQAStart].Text;
                int indexQAEnd = GetColumnIndexByName(e.Row, "QAEnd");
                string columnQAEnd = e.Row.Cells[indexQAEnd].Text;
                int indexQRStart = GetColumnIndexByName(e.Row, "QRStart");
                string columnQRStart = e.Row.Cells[indexQRStart].Text;
                int indexQREnd = GetColumnIndexByName(e.Row, "QREnd");
                string columnQREnd = e.Row.Cells[indexQREnd].Text;
                int indexFicoStart = GetColumnIndexByName(e.Row, "FicoStart");
                string columnFicoStart = e.Row.Cells[indexFicoStart].Text;
                int indexFicoEnd = GetColumnIndexByName(e.Row, "FicoEnd");
                string columnFicoEnd = e.Row.Cells[indexFicoEnd].Text;
                int indexTransID = GetColumnIndexByName(e.Row, "TransID");
                LinkButton display = (LinkButton)e.Row.FindControl("display");
                string TransID = display.Text;
                string columnTransID = TransID;
                int indexMaterialID = GetColumnIndexByName(e.Row, "MaterialID");
                string columnMaterialID = e.Row.Cells[indexMaterialID].Text;
                int indexStatus = GetColumnIndexByName(e.Row, "Status");
                string columnStatus = e.Row.Cells[indexStatus].Text;
                int indexGlobalStatus = GetColumnIndexByName(e.Row, "GlobalStatus");
                string columnGlobalStatus = e.Row.Cells[indexGlobalStatus].Text;
                int indexNotes = GetColumnIndexByName(e.Row, "Notes");
                string columnNotes = e.Row.Cells[indexNotes].Text;
                int indexMaterialApproved = GetColumnIndexByName(e.Row, "MaterialApproved");
                string columnMaterialApproved = e.Row.Cells[indexMaterialApproved].Text;

                if (columnRDStart == "&nbsp;" && columnRDEnd == "&nbsp;" &&
                    columnProcStart == "&nbsp;" && columnProcEnd == "&nbsp;" &&
                    columnPlanStart == "&nbsp;" && columnPlanEnd == "&nbsp;" &&
                    columnQCStart == "&nbsp;" && columnQCEnd == "&nbsp;" &&
                    columnQAStart == "&nbsp;" && columnQAEnd == "&nbsp;" &&
                    columnQRStart == "&nbsp;" && columnQREnd == "&nbsp;" &&
                    columnFicoStart == "&nbsp;" && columnFicoEnd == "&nbsp;" &&
                    lblPosition.Text != "R&D MGR" && columnStatus == "&nbsp;")
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Open";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = true;
                }
                else if (columnRDStart != "&nbsp;" && columnRDEnd == "&nbsp;" &&
                    columnProcStart == "&nbsp;" && columnProcEnd == "&nbsp;" &&
                    columnPlanStart == "&nbsp;" && columnPlanEnd == "&nbsp;" &&
                    columnQCStart == "&nbsp;" && columnQCEnd == "&nbsp;" &&
                    columnQAStart == "&nbsp;" && columnQAEnd == "&nbsp;" &&
                    columnQRStart == "&nbsp;" && columnQREnd == "&nbsp;" &&
                    columnFicoStart == "&nbsp;" && columnFicoEnd == "&nbsp;" &&
                    lblPosition.Text != "R&D MGR" && columnStatus == "&nbsp;")
                {
                    if (columnGlobalStatus == "Waiting For PD Manager Approval" || columnGlobalStatus == "Waiting For BD Manager Approval")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = columnGlobalStatus;
                    }
                    else
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                    }
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                else if (columnRDStart != "&nbsp;" && columnStatus == "REVISIONRND" && lblPosition.Text != "R&D MGR")
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Open";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = true;
                }
                else if (columnRDStart != "&nbsp;" && columnStatus == "REVISIONRNDAPPROVAL" && lblPosition.Text != "R&D MGR")
                {
                    if (columnGlobalStatus == "Waiting For PD Manager Approval" || columnGlobalStatus == "Waiting For BD Manager Approval")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = columnGlobalStatus;
                    }
                    else
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                    }
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                else if (columnRDStart != "&nbsp;" && columnRDEnd != "&nbsp;" &&
                    columnProcStart != "&nbsp;" &&
                    columnPlanStart != "&nbsp;" &&
                    columnQCStart != "&nbsp;" &&
                    columnQAStart != "&nbsp;" &&
                    columnQRStart != "&nbsp;" &&
                    lblPosition.Text != "R&D MGR")
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;

                    if (columnPlanEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnProcEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnQCEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnQAEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnQREnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }

                    if (columnRDEnd != "&nbsp;" &&
                        columnProcEnd != "&nbsp;" &&
                        columnPlanEnd != "&nbsp;" &&
                        columnQCEnd != "&nbsp;" &&
                        columnQAEnd != "&nbsp;" &&
                        columnQREnd != "&nbsp;" &&
                        columnFicoStart != "&nbsp;")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Open";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        if (columnFicoEnd != "&nbsp;")
                        {
                            if (columnGlobalStatus == "Waiting For PD Manager Approval" || columnGlobalStatus == "Waiting For BD Manager Approval")
                            {
                                ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = columnGlobalStatus;
                            }
                            else
                            {
                                ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                            }
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                            ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        }
                    }

                    if (columnMaterialApproved == "x")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Closed";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "Queue";
                        string lblNotes = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        if (columnGlobalStatus == "Completed")
                        {
                            ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Completed";
                        }
                    }
                }

                if (columnRDStart == "&nbsp;" && columnRDEnd == "&nbsp;" &&
                    columnProcStart == "&nbsp;" && columnProcEnd == "&nbsp;" &&
                    columnPlanStart == "&nbsp;" && columnPlanEnd == "&nbsp;" &&
                    columnQCStart == "&nbsp;" && columnQCEnd == "&nbsp;" &&
                    columnQAStart == "&nbsp;" && columnQAEnd == "&nbsp;" &&
                    columnQRStart == "&nbsp;" && columnQREnd == "&nbsp;" &&
                    columnFicoStart == "&nbsp;" && columnFicoEnd == "&nbsp;" &&
                    (lblPosition.Text == "R&D MGR" || lblPosition.Text == "BD MGR"))
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                // cek jika material rnd sudah di dibuat dan siap di approve 
                else if (columnRDStart != "&nbsp" && columnRDEnd == "&nbsp;" &&
                    columnProcStart == "&nbsp;" && columnProcEnd == "&nbsp;" &&
                    columnPlanStart == "&nbsp;" && columnPlanEnd == "&nbsp;" &&
                    columnQCStart == "&nbsp;" && columnQCEnd == "&nbsp;" &&
                    columnQAStart == "&nbsp;" && columnQAEnd == "&nbsp;" &&
                    columnQRStart == "&nbsp;" && columnQREnd == "&nbsp;" &&
                    columnFicoStart == "&nbsp;" && columnFicoEnd == "&nbsp;" &&
                    (lblPosition.Text == "R&D MGR" || lblPosition.Text == "BD MGR"))
                {
                    if (columnStatus == "REVISIONRNDAPPROVAL")
                    {
                        if (columnGlobalStatus == "Waiting For PD Manager Approval" || columnGlobalStatus == "Waiting For BD Manager Approval")
                        {
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = columnGlobalStatus;
                        }
                        else
                        {
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                        }
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                        string lblNotes = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = true;
                    }
                    else
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                        string lblNotesR = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotesR;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                    }
                }
                // cek jika ada revisi di dept rnd
                else if (columnRDStart != "&nbsp" && columnStatus == "REVISIONRND" && (lblPosition.Text == "R&D MGR" || lblPosition.Text == "BD MGR"))
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                //cek jika revisi rnd sudah di save siap untuk di approve
                else if (columnRDStart != "&nbsp" && columnStatus == "REVISIONRNDAPPROVAL" && (lblPosition.Text == "R&D MGR" || lblPosition.Text == "BD MGR"))
                {
                    if (columnGlobalStatus == "Waiting For PD Manager Approval" || columnGlobalStatus == "Waiting For BD Manager Approval")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = columnGlobalStatus;
                    }
                    else
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                    }
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = true;
                }
                // cek jika dia masuk ke dalam dept cons
                else if (columnRDStart != "&nbsp" && columnRDEnd != "&nbsp;" &&
                    columnProcStart != "&nbsp;" &&
                    columnPlanStart != "&nbsp;" &&
                    columnQCStart != "&nbsp;" &&
                    columnQAStart != "&nbsp;" &&
                    columnQRStart != "&nbsp;" &&
                    (lblPosition.Text == "R&D MGR" || lblPosition.Text == "BD MGR"))
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Open";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;

                    if (columnPlanEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnProcEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnQCEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnQAEnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }
                    if (columnQREnd != "&nbsp;")
                    {
                        string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                    }

                    if (columnRDEnd != "&nbsp;" &&
                        columnProcEnd != "&nbsp;" &&
                        columnPlanEnd != "&nbsp;" &&
                        columnQCEnd != "&nbsp;" &&
                        columnQAEnd != "&nbsp;" &&
                        columnQREnd != "&nbsp;" &&
                        columnFicoStart != "&nbsp;")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Open";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        if (columnFicoEnd != "&nbsp;")
                        {
                            if (columnGlobalStatus == "Waiting For PD Manager Approval" || columnGlobalStatus == "Waiting For BD Manager Approval")
                            {
                                ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = columnGlobalStatus;
                            }
                            else
                            {
                                ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                            }
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                            ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        }
                    }

                    // cek jika material sudah selesai
                    if (columnMaterialApproved == "x")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Closed";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "Queue";
                        string lblNotesIn = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        //cek jika material sudah masuk sap
                        if (columnGlobalStatus == "Completed")
                        {
                            ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Completed";
                        }
                    }
                }


                if (columnGlobalStatus == "HOLD")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Req. Cancel";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "HOLD";
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                else if (columnGlobalStatus == "CANCELED")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Canceled";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "CANCELED";
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                else if (columnGlobalStatus == "Error")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Closed";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = columnGlobalStatus;
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = columnNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
            }
        }
        //Get Column Name Method in GridView
        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }

        //auto generate TransID
        protected DataTable GetLastTransIDByTypeProc(string TypeProc)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TypeProc", TypeProc);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("Tbl_Material_GetLastTransID", param);

            return dt;
        }
        protected void autoGenTransID()
        {
            string TypeProc = "";
            string num1 = "";
            DataTable dt = new DataTable();
            dt = GetLastTransIDByTypeProc(rmsffgMenuLabel.Text.Trim());

            if (dt.Rows.Count > 0)
            {
                num1 = dt.Rows[0]["TransID"].ToString();
                TypeProc = num1.Substring(0, 2);
                num1 = string.Format(TypeProc + "{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
                lblTransID.Text = num1;
            }
            else
            {
                num1 = "RM00000000";
                TypeProc = rmsffgMenuLabel.Text;
                num1 = string.Format(TypeProc + "{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
                lblTransID.Text = num1;
            }
        }
        //auto generate LogID
        protected DataTable GetLastLogID(string LogID)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@LogID", LogID);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[Tbl_LogTrans_GetLogTransID]", param);

            return dt;
        }
        protected void autoGenLogID()
        {
            string num1 = "";
            DataTable dt = new DataTable();
            dt = GetLastLogID("");
            if (dt.Rows.Count > 0)
            {
                num1 = dt.Rows[0]["LogID"].ToString();
                num1 = string.Format("LD{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
                lblLogID.Text = num1;
            }

        }

        //LightBox Data Code
        protected void srcSpcProcModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (inputProcType.Text == "X" || inputProcType.Text == "x")
            {
                var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE Plant=@Plant"; //return data from UserApp table
                var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Plant",
                    Value = this.inputPlant.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                GridViewSpcProc.DataSource = ds.Tables[0];
                GridViewSpcProc.DataBind();
                conMatWorkFlow.Close();
            }
            else if (inputProcType.Text == "E" || inputProcType.Text == "e")
            {
                var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE Plant=@Plant AND SpclProcurement='50'"; //return data from UserApp table
                var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Plant",
                    Value = this.inputPlant.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                GridViewSpcProc.DataSource = ds.Tables[0];
                GridViewSpcProc.DataBind();
                conMatWorkFlow.Close();
            }
            else if (inputProcType.Text == "F" || inputProcType.Text == "f")
            {
                var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE Plant=@Plant AND SpclProcurement NOT LIKE'50'"; //return data from UserApp table
                var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Plant",
                    Value = this.inputPlant.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                GridViewSpcProc.DataSource = ds.Tables[0];
                GridViewSpcProc.DataBind();
                conMatWorkFlow.Close();
            }
        }
        protected void srcVolUntModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_UoM"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewVolUnt.DataSource = ds.Tables[0];
            GridViewVolUnt.DataBind();
            conMatWorkFlow.Close();
        }
        protected DataTable Mstr_MaterialType_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();


            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_MaterialType_Select", param);

            return dt;

        }
        protected void srcMatTypModalBinding(string Filter)
        {
            //if (conMatWorkFlow.State == ConnectionState.Closed)
            //{
            //    conMatWorkFlow.Open();
            //}
            //var queryString = "SELECT * FROM Mstr_MaterialType"; //return data from UserApp table
            //var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            //var commandBuilder = new SqlCommandBuilder(dataAdapter);
            //var ds = new DataSet();
            //dataAdapter.Fill(ds);

            //GridViewMatTyp.DataSource = ds.Tables[0];
            //GridViewMatTyp.DataBind();
            //conMatWorkFlow.Close();

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcMatType", conMatWorkFlow);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = this.rmsffgMenuLabel.Text.Trim().ToUpper();
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewMatTyp.DataSource = ds.Tables[0];
            GridViewMatTyp.DataBind();
            conMatWorkFlow.Close();

            //GVMatType.PageSize = int.Parse(ddlPageSize.SelectedValue);
            //ViewState["Row"] = 0;
            //string StartRow = (GVMatType.PageIndex * GVMatType.PageSize).ToString();
            //string Row = ddlPageSize.SelectedValue;
            //DataTable dt = new DataTable();
            //  dt  = Mstr_MaterialType_GetData(Filter , StartRow , Row);



            //GVMatType.DataSource = dt;
            //GVMatType.DataBind();

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        if (ViewState["Row"].ToString().Trim() == "0")
            //        {
            //            ViewState["grandtotal"] = row["TotalRow"].ToString();
            //            lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
            //            DivGrid.Visible = true;
            //            // lblNoData.Visible = false;
            //            ViewState["Row"] = 1;
            //            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GVMatType.PageSize));
            //            lblTotalNumberOfPages.Text = pageCount.ToString();
            //            txtGoToPage.Text = (GVMatType.PageIndex + 1).ToString();
            //            GridViewField.Visible = true;
            //        }
            //    }

            //}
            //else
            //{
            //    GridViewField.Visible = false;
            //    ViewState["grandtotal"] = 0;
            //    DivGrid.Visible = false;
            //    //lblNoData.Visible = true;
            //}
        }
        protected void srcIndStdDescModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_IndStdDesc"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewIndStdDesc.DataSource = ds.Tables[0];
            GridViewIndStdDesc.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcPckgMatModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_MaterialGroupPack"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewPckgMat.DataSource = ds.Tables[0];
            GridViewPckgMat.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcMatGrModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (rmsffgMenuLabel.Text == "RM")
            {
                var queryString = "SELECT a.* FROM Mstr_MaterialGroup a INNER JOIN CommonTable b ON a.MatlGroup = b.LOW WHERE HIGH = 'RM'"; //return data from UserApp table
                var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                GridViewMatGr.DataSource = ds.Tables[0];
                GridViewMatGr.DataBind();
                conMatWorkFlow.Close();
            }
            else if (rmsffgMenuLabel.Text == "SF")
            {
                var queryString = "SELECT a.* FROM Mstr_MaterialGroup a INNER JOIN CommonTable b ON a.MatlGroup = b.LOW WHERE HIGH = 'SF'"; //return data from UserApp table
                var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                GridViewMatGr.DataSource = ds.Tables[0];
                GridViewMatGr.DataBind();
                conMatWorkFlow.Close();
            }
            else if (rmsffgMenuLabel.Text == "FG")
            {
                var queryString = "SELECT a.* FROM Mstr_MaterialGroup a INNER JOIN CommonTable b ON a.MatlGroup = b.LOW WHERE HIGH = 'FG'"; //return data from UserApp table
                var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                GridViewMatGr.DataSource = ds.Tables[0];
                GridViewMatGr.DataBind();
                conMatWorkFlow.Close();
            }

        }
        protected void srcPlantModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcPlant", conMatWorkFlow);
            cmd.Parameters.Add("@Usnam", SqlDbType.NVarChar).Value = this.lblUser.Text.Trim();
            cmd.Parameters.Add("@SOrg", SqlDbType.NVarChar).Value = this.inputSalesOrg.Text.Trim();
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewPlant.DataSource = ds.Tables[0];
            GridViewPlant.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcSalesOrgModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcSOrg", conMatWorkFlow);
            cmd.Parameters.Add("@Usnam", SqlDbType.NVarChar).Value = this.lblUser.Text.Trim();
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewSalesOrg.DataSource = ds.Tables[0];
            GridViewSalesOrg.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcStorLocModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_Location WHERE Plant = @inputPlant"; //return data from UserApp table                    
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
            dataAdapter.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "inputPlant",
                Value = this.inputPlant.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 4  // Assuming a 2000 char size of the field annotation (-1 for MAX)
            });

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewStorLoc.DataSource = ds.Tables[0];
            GridViewStorLoc.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcDistrChlModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_DistrChannel"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewDistrChl.DataSource = ds.Tables[0];
            GridViewDistrChl.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcAunModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "Select IT.*  from Mstr_UoM IT WHERE IT.UoM NOT IN (SELECT QD.Aun FROM Tbl_DetailUomMat QD WHERE QD.TransID = @TransID AND QD.MaterialID = @MaterialID)"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
            dataAdapter.SelectCommand.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            dataAdapter.SelectCommand.Parameters.AddWithValue("MaterialID", this.inputMatID.Text);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewAun.DataSource = ds.Tables[0];
            GridViewAun.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcBsUntMeasModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_UoM"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewBsUntMeas.DataSource = ds.Tables[0];
            GridViewBsUntMeas.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcDivisionModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_SalesDivision"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewDivision.DataSource = ds.Tables[0];
            GridViewDivision.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcProcTypeModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_ProcType"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewProcType.DataSource = ds.Tables[0];
            GridViewProcType.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcWeightUntModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_UoM"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewWeightUnt.DataSource = ds.Tables[0];
            GridViewWeightUnt.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcNetWeightUntModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcUoM", conMatWorkFlow);
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewNetWeightUnt.DataSource = ds.Tables[0];
            GridViewNetWeightUnt.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcCommImpModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_ForeignTrade"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewCommImp.DataSource = ds.Tables[0];
            GridViewCommImp.DataBind();
            conMatWorkFlow.Close();
        }
        //Modal
        protected void selectIndStdDesc_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputIndStdDesc.Text = grdrow.Cells[0].Text;
            lblIndStdDesc.Text = grdrow.Cells[1].Text;
            lblIndStdDesc.ForeColor = Color.Black;
            inputIndStdDesc.Focus();
        }
        protected void selectSpcProc_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputSpcProc.Text = grdrow.Cells[0].Text;
            lblSpcProc.Text = grdrow.Cells[1].Text;
            lblSpcProc.ForeColor = Color.Black;
            inputSpcProc.Focus();
        }
        protected void selectCommImp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputCommImpCode.Text = grdrow.Cells[0].Text;
            lblCommImpCode.Text = grdrow.Cells[1].Text;
            lblCommImpCode.ForeColor = Color.Black;
            inputVolUnt.Focus();
        }
        protected void selectVolUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputVolUnt.Text = grdrow.Cells[0].Text;
            lblVolUnt.Text = grdrow.Cells[1].Text;
            lblVolUnt.ForeColor = Color.Black;
            inputVolUnt.Focus();
        }
        protected void selectWeightUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputWeightUnt.Text = grdrow.Cells[0].Text;

            lblWeightUnt.Text = grdrow.Cells[1].Text;
            lblWeightUnt.ForeColor = Color.Black;
            inputWeightUnt.Focus();
        }
        protected void selectNetWeightUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputNetWeightUnit.Text = grdrow.Cells[0].Text;
            lblNetWeightUnit.Text = grdrow.Cells[2].Text;
            lblNetWeightUnit.ForeColor = Color.Black;
            inputNetWeightUnit.Focus();
        }
        protected void selectMatTyp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputMatTyp.Text = grdrow.Cells[0].Text;
            lblMatTyp.Text = grdrow.Cells[1].Text;
            lblMatTyp.ForeColor = Color.Black;
            inputMatTyp.Focus();
            if (inputMatTyp.Text == "SFAT" || inputMatTyp.Text == "sfat")
            {
                MinMaxMatID.ValidationExpression = "^[-_,.A-Za-z0-9]{7,18}$";
                MinMaxMatID.ErrorMessage = "Minimum 7, maximum 18 characters required and no special characters.";
            }
            else
            {
                MinMaxMatID.ValidationExpression = "^[-_,.A-Za-z0-9]{8,18}$";
                MinMaxMatID.ErrorMessage = "Minimum 8, maximum 18 characters required and no special characters.";
            }
            if (inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                fieldsetAddon.Visible = true;
                MRP1LOTSizeDt.Visible = true;
            }
            else
            {
                fieldsetAddon.Visible = false;
                MRP1LOTSizeDt.Visible = false;
            }
        }
        protected void selectPckgMat_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputPckgMat.Text = grdrow.Cells[0].Text;
            lblPckgMat.Text = grdrow.Cells[1].Text;
            lblPckgMat.ForeColor = Color.Black;
            inputPckgMat.Focus();
        }
        protected void selectMatGr_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputMatGr.Text = grdrow.Cells[0].Text;
            lblMatGr.Text = grdrow.Cells[1].Text;
            lblMatGr.ForeColor = Color.Black;
            inputMatGr.Focus();
        }
        protected void selectPlant_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputPlant.Text = grdrow.Cells[0].Text;
            lblPlant.Text = grdrow.Cells[1].Text;
            //Force Postback Script
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
            sbScript.Append("<!--\n");
            sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
            sbScript.Append("// -->\n");
            sbScript.Append("</script>\n");
            this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
            lblPlant.ForeColor = Color.Black;
            lblStorLoc.Text = absStorLoc.Text;
            lblStorLoc.ForeColor = Color.Black;
            inputStorLoc.Text = "";
            inputPlant.Focus();
            srcStorLocModalBinding();
            if (inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                fieldsetAddon.Visible = true;
                MRP1LOTSizeDt.Visible = true;
            }
            else
            {
                fieldsetAddon.Visible = false;
                MRP1LOTSizeDt.Visible = false;
            }
        }
        protected void selectSalesOrg_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputSalesOrg.Text = grdrow.Cells[0].Text;
            lblSalesOrg.Text = grdrow.Cells[1].Text;
            lblSalesOrg.ForeColor = Color.Black;
            inputSalesOrg.Focus();
            srcPlantModalBinding();

            lblPlant.Text = absPlant.Text;
            lblPlant.ForeColor = Color.Black;
            inputPlant.Text = "";
            lblStorLoc.Text = absStorLoc.Text;
            lblStorLoc.ForeColor = Color.Black;
            inputStorLoc.Text = "";
        }
        protected void selectStorLoc_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputStorLoc.Text = grdrow.Cells[1].Text;
            lblStorLoc.Text = grdrow.Cells[2].Text;
            lblStorLoc.ForeColor = Color.Black;
            inputStorLoc.Focus();
        }
        protected void selectDistrChl_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputDistrChl.Text = grdrow.Cells[0].Text;
            lblDistrChl.Text = grdrow.Cells[1].Text;
            lblDistrChl.ForeColor = Color.Black;
            inputDistrChl.Focus();
        }
        protected void selectAun_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputAun.Text = grdrow.Cells[0].Text;
            lblAMeas.Text = grdrow.Cells[1].Text;
            lblAMeas.ForeColor = Color.Black;
            inputAun.Focus();
        }
        protected void selectBsUntMeas_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputBsUntMeas.Text = grdrow.Cells[0].Text;
            inputWeightUnt.Text = grdrow.Cells[0].Text;
            inputBun.Text = grdrow.Cells[0].Text;
            inputNetWeightUnit.Text = grdrow.Cells[0].Text;
            inputWeightUnt.Text = grdrow.Cells[0].Text;
            lblBsUntMeas.Text = grdrow.Cells[1].Text;
            lblBMeas.Text = grdrow.Cells[1].Text;
            lblNetWeightUnit.Text = grdrow.Cells[1].Text;
            lblBsUntMeas.ForeColor = Color.Black;
            lblNetWeightUnit.ForeColor = Color.Black;
            inputBsUntMeas.Focus();
        }
        protected void selectDivision_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputDivision.Text = grdrow.Cells[0].Text;
            lblDivision.Text = grdrow.Cells[1].Text;
            lblDivision.ForeColor = Color.Black;
            inputDivision.Focus();
        }
        protected void selectProcType_Click(object sender, EventArgs e)
        {
            inputSpcProc.Text = "";
            lblSpcProc.Text = absSpcProc.Text;

            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputProcType.Text = grdrow.Cells[0].Text.Trim();
            lblProcType.Text = grdrow.Cells[1].Text.Trim();
            lblProcType.ForeColor = Color.Black;
            inputProcType.Focus();
            srcSpcProcModalBinding();
            if (inputProcType.Text.ToUpper().Trim() == "&nbsp;")
            {
                inputProcType.Text = "";
            }
            if (inputProcType.Text.ToUpper().Trim() == "E")
            {
                chkbxCoProd.Enabled = true;
            }
            else
            {
                chkbxCoProd.Enabled = false;
                chkbxCoProd.Checked = false;
            }
        }

        //Other Data Code
        //auto generate Line

        protected void dummyGrossWeight_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("DELETE FROM Tbl_DetailUomMat WHERE TransID = @TransID and MaterialID = @MaterialID", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmd.ExecuteNonQuery();
            conMatWorkFlow.Close();
        }
        protected DataTable GetLastLineDetailUom(string TransID, string MaterialID)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TransID", TransID);
            param[1] = new SqlParameter("@MaterialID", MaterialID);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[Tbl_DetailUom_GetLine]", param);

            return dt;
        }
        protected void autoGenLine()
        {
            string num1 = "";
            DataTable dt = new DataTable();
            dt = GetLastLineDetailUom(lblTransID.Text.Trim(), inputMatID.Text.Trim().ToUpper());
            if (dt.Rows.Count > 0)
            {
                num1 = dt.Rows[0]["Line"].ToString();
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLine.Text = num1;
            }
            else
            {
                num1 = lblLine.Text;
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLine.Text = num1;
            }
        }
        //protected void autoGenLine()
        //{
        //    string num1 = lblLine.Text;
        //    num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
        //    lblLine.Text = num1;
        //}
        protected void btnAddUntMeas_Click(object sender, EventArgs e)
        {
            decimal GrossWeight;
            decimal NetWeight;
            decimal.TryParse(inputGrossWeight.Text, out GrossWeight);
            decimal.TryParse(inputNetWeight.Text, out NetWeight);
            decimal plus;
            decimal.TryParse("0.001", out plus);

            if (GrossWeight <= NetWeight)
            {
                MsgBox("Your gross weight cannot be less or equal with net weight!", this.Page, this);
                inputGrossWeight.Text = (NetWeight + plus).ToString();
                return;
            }

            if (inputBun.Text == "")
            {
                MsgBox("Please insert your Base UoM first.", this.Page, this);
                inputBsUntMeas.Focus();
                return;
            }

            if (inputMatID.Text == "")
            {
                MsgBox("Please insert your Material ID first.", this.Page, this);
                inputMatID.Focus();
                return;
            }

            if (inputGrossWeight.Text == "0" || inputX.Text == "0" || inputY.Text == "0" || inputGrossWeight.Text == "" || inputX.Text == "" || inputY.Text == "" || inputAun.Text == "")
            {
                MsgBox("Gross Weight, X, Aun or Y cannot be empty data!", this.Page, this);
                return;
            }

            decimal GrossWeightTotal;
            int Y;
            decimal.TryParse(inputGrossWeight.Text, out GrossWeight);
            Int32.TryParse(inputY.Text, out Y);

            GrossWeightTotal = GrossWeight * Y;

            if (lblUpdate.Text != "X")
            {
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("selectAllMaterial_ByMaterialID", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your MaterialID " + this.inputMatID.Text + " is already Exist!", this.Page, this);
                    return;
                }
                else
                {
                    if (inputGrossWeight.Text != "0")
                    {
                        // doing checking
                        DataTable dtCheckB = new DataTable();
                        SqlParameter[] paramB = new SqlParameter[3];
                        paramB[0] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                        paramB[1] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                        paramB[2] = new SqlParameter("@Aun", this.inputAun.Text.Trim().ToUpper());

                        //send param to SP sql
                        dtCheckB = sqlC.ExecuteDataTable("CheckXExist", paramB);

                        if (dtCheckB.Rows.Count > 0)
                        {
                            // munculkan pesan bahwa sudah ada
                            MsgBox("Your Aun " + this.inputAun.Text + " is already Exist or did not match Aun Type master data!", this.Page, this);
                        }
                        else
                        {
                            //doing insert
                            SqlParameter[] paramm = new SqlParameter[14];
                            paramm[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
                            paramm[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim().ToUpper());
                            paramm[2] = new SqlParameter("@Line", lblLine.Text.Trim().ToUpper());
                            paramm[3] = new SqlParameter("@X", inputX.Text.Trim().ToUpper());
                            paramm[4] = new SqlParameter("@Aun", inputAun.Text.Trim().ToUpper());
                            paramm[5] = new SqlParameter("@Y", inputY.Text.Trim());
                            paramm[6] = new SqlParameter("@Bun", inputBun.Text.Trim().ToUpper());
                            paramm[7] = new SqlParameter("@GrossWeight", GrossWeightTotal);
                            paramm[8] = new SqlParameter("@WeightUnit", inputWeightUnt.Text.Trim().ToUpper());
                            paramm[9] = new SqlParameter("@Volume", inputVolume.Text.Trim().ToUpper());
                            paramm[10] = new SqlParameter("@VolUnit", inputVolUnt.Text.Trim().ToUpper());
                            paramm[11] = new SqlParameter("@CreateBy", lblUser.Text.Trim());
                            paramm[12] = new SqlParameter("@CreateTime", DateTime.Now);
                            paramm[13] = new SqlParameter("@New", "");
                            //send param to SP sql
                            sqlC.ExecuteNonQuery("DetailUomMat_InsertData", paramm);

                            inputMatID.ReadOnly = true;
                            inputMatID.CssClass = "txtBoxRO";
                        }
                    }
                    else
                    {
                        MsgBox("Gross Weight value cannot 0.", this.Page, this);
                    }
                }
                dtCheck.Clear();
                dtCheck = null;
            }
            else
            {
                if (inputGrossWeight.Text != "0")
                {
                    // doing checking
                    DataTable dtCheckB = new DataTable();
                    SqlParameter[] paramB = new SqlParameter[3];
                    paramB[0] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                    paramB[1] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                    paramB[2] = new SqlParameter("@Aun", this.inputAun.Text.Trim().ToUpper());

                    //send param to SP sql
                    dtCheckB = sqlC.ExecuteDataTable("CheckXExist", paramB);

                    if (dtCheckB.Rows.Count > 0)
                    {
                        // munculkan pesan bahwa sudah ada
                        MsgBox("Your Aun " + this.inputAun.Text + " is already Exist or did not match Aun master data!", this.Page, this);
                    }
                    else
                    {
                        //doing insert
                        SqlParameter[] paramm = new SqlParameter[14];
                        paramm[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
                        paramm[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim().ToUpper());
                        paramm[2] = new SqlParameter("@Line", lblLine.Text.Trim().ToUpper());
                        paramm[3] = new SqlParameter("@X", inputX.Text.Trim().ToUpper());
                        paramm[4] = new SqlParameter("@Aun", inputAun.Text.Trim().ToUpper());
                        paramm[5] = new SqlParameter("@Y", inputY.Text.Trim());
                        paramm[6] = new SqlParameter("@Bun", inputBun.Text.Trim().ToUpper());
                        paramm[7] = new SqlParameter("@GrossWeight", GrossWeightTotal);
                        paramm[8] = new SqlParameter("@WeightUnit", inputWeightUnt.Text.Trim().ToUpper());
                        paramm[9] = new SqlParameter("@Volume", inputVolume.Text.Trim().ToUpper());
                        paramm[10] = new SqlParameter("@VolUnit", inputVolUnt.Text.Trim().ToUpper());
                        paramm[11] = new SqlParameter("@CreateBy", lblUser.Text.Trim());
                        paramm[12] = new SqlParameter("@CreateTime", DateTime.Now);
                        paramm[13] = new SqlParameter("@New", "");
                        //send param to SP sql
                        sqlC.ExecuteNonQuery("DetailUomMat_InsertData", paramm);
                        inputMatID.ReadOnly = true;
                        inputMatID.CssClass = "txtBoxRO";
                    }
                }
                else
                {
                    MsgBox("Gross Weight value cannot 0.", this.Page, this);
                }
            }


            conMatWorkFlow.Close();
            Clear_Controls();
            tmpBindRepeater();
            srcAunModalBinding();
            autoGenLine();
        }
        private void Clear_Controls()
        {
            inputGrossWeight.CssClass = "txtBoxRO";
            inputWeightUnt.CssClass = "txtBoxRO";
            imgBtnWeightUnt.Visible = false;
            tdVolume.ColSpan = 1;
            tdVolume.Style.Add("padding-left", "0px");

            inputX.Text = string.Empty;
            inputAun.Text = string.Empty;
            lblAMeas.Text = "Aun Desc.";
            lblVolUnt.Text = "Volume Unit Desc.";
            inputY.Text = string.Empty;

            inputVolume.Text = "0";
            inputVolUnt.Text = string.Empty;
            inputX.Focus();
        }
        protected void tmpBindRepeater()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * from Tbl_DetailUomMat where TransID = @TransID and MaterialID = @MaterialID", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            reptUntMeas.DataSource = ds;
            reptUntMeas.DataBind();
            reptUntMeasMgr.DataSource = ds;
            reptUntMeasMgr.DataBind();
            conMatWorkFlow.Close();
        }
        protected void rept_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblX")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblAun")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblY")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBun")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrGrossWeight")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrWeightUnit")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrVolume")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrVolUnit")).Visible = false;

                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtX")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtAun")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtY")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtBun")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtGrossWeight")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtWeightUnit")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolume")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolUnit")).Visible = true;

                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkEdit")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkDelete")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkUpdate")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkCancel")).Visible = true;
            }
            if (e.CommandName == "update")
            {
                string x = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtX")).Text.Trim().ToUpper();
                string An = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtAun")).Text.Trim().ToUpper();
                string y = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtY")).Text.Trim().ToUpper();
                string Bn = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtBun")).Text.Trim().ToUpper();

                string gw = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtGrossWeight")).Text.Trim().ToUpper();
                string wu = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtWeightUnit")).Text.Trim().ToUpper();
                string v = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolume")).Text.Trim().ToUpper();
                string vu = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolUnit")).Text.Trim().ToUpper();

                string DtlID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDetailID")).Text.Trim();
                SqlDataAdapter adp = new SqlDataAdapter("Update Tbl_DetailUomMat set X = @X, Aun = @Aun, Y = @Y, Bun = @Bun, GrossWeight = @gw, WeightUnit = @wu, Volume = @v, VolUnit = @vu WHERE DetailID = @DetailID", conMatWorkFlow);
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@X",
                    Value = x,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Aun",
                    Value = An,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Y",
                    Value = y,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Bun",
                    Value = Bn,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });

                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@gw",
                    Value = gw,
                    SqlDbType = SqlDbType.Decimal,
                    Precision = 18,
                    Scale = 3
                });
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@wu",
                    Value = wu,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@v",
                    Value = v,
                    SqlDbType = SqlDbType.Decimal,
                    Precision = 18,
                    Scale = 3
                });
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@vu",
                    Value = vu,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                adp.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "DetailID",
                    Value = DtlID,
                    SqlDbType = SqlDbType.BigInt,
                });
                DataSet ds = new DataSet();
                adp.Fill(ds);
                tmpBindRepeater();
            }
            if (e.CommandName == "delete")
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                string DtlID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDetailID")).Text.Trim();
                SqlCommand cmd = new SqlCommand("delete from Tbl_DetailUomMat where DetailID = @DetailID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@DetailID",
                    Value = DtlID,
                    SqlDbType = SqlDbType.BigInt,
                });
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                tmpBindRepeater();
            }
            if (e.CommandName == "cancel")
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblX")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblAun")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblY")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBun")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrGrossWeight")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrWeightUnit")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrVolume")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblOtrVolUnit")).Visible = true;

                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtX")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtAun")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtY")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtBun")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtGrossWeight")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtWeightUnit")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolume")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolUnit")).Visible = false;

                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkEdit")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkDelete")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkUpdate")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkCancel")).Visible = false;
            }
        }

        //Save Code
        protected void Update_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            if (inputNetWeight.Text == "0")
            {
                MsgBox("Net Weight cannot be 0!", this.Page, this);
                return;
            }
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (rmsffgMenuLabel.Text.ToUpper().Trim() != "RM")
            {
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("CheckDetailUom", param);

                if (dtCheck.Rows.Count == 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your detail Uom table cannot empty!", this.Page, this);
                    return;
                }

            }
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            // doing checking
            DataTable dtCheckB = new DataTable();
            SqlParameter[] paramB = new SqlParameter[2];
            paramB[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
            paramB[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

            //send param to SP sql
            dtCheckB = sqlC.ExecuteDataTable("checkGrossWeight", paramB);

            if (dtCheckB.Rows.Count > 0)
            {
                // munculkan pesan bahwa sudah ada
                MsgBox("Your gross weight value at detail Uom table cannot be 0!", this.Page, this);
                return;
            }
            conMatWorkFlow.Close();

            if (rmsffgMenuLabel.Text.ToUpper().Trim() != "RM")
            {
                conMatWorkFlow.Open();
                // doing checking
                DataTable dtCheckC = new DataTable();
                SqlParameter[] paramC = new SqlParameter[3];
                paramC[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                paramC[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                paramC[2] = new SqlParameter("@Uom", this.inputBsUntMeas.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheckC = sqlC.ExecuteDataTable("CheckBun", paramC);

                if (dtCheckC.Rows.Count == 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your Bun value not exactly the same as Base Unit Measurement value. Please update the detail Uom table first.", this.Page, this);
                    return;
                }
                conMatWorkFlow.Close();
            }

            if (inputMatTyp.Text == "" || inputMatID.Text == "" || inputMatDesc.Text == "" || inputBsUntMeas.Text == "" || inputMatGr.Text == "" || inputSalesOrg.Text == "" || inputPlant.Text == "" || inputStorLoc.Text == "" || inputDistrChl.Text == "")
            {
                MsgBox("One of the required field still empty.", this.Page, this);
            }
            else
            {
                conMatWorkFlow.Open();
                if (rmsffgMenuLabel.Text.Trim() == "RM")
                {
                    if ((inputVolume.Text == "" && inputNetWeight.Text == "" && inputGrossWeight.Text == "") || (inputVolume.Text == "&nbsp;" && inputNetWeight.Text == "&nbsp;" && inputGrossWeight.Text == "&nbsp;"))
                    {
                        inputVolume.Text = "0";
                        inputNetWeight.Text = "0";
                        inputGrossWeight.Text = "0";
                    }
                    else if (inputVolume.Text == "0.000" && inputNetWeight.Text == "0.000" && inputGrossWeight.Text == "0.000")
                    {
                        inputVolume.Text = "0";
                        inputNetWeight.Text = "0";
                        inputGrossWeight.Text = "0";
                    }
                }
                if (chkbxCoProd.Checked == true)
                {
                    stringCoProd = "X";
                }
                else if (chkbxCoProd.Checked == false)
                {
                    stringCoProd = "";
                }

                //added by jone
                decimal roundvalue;
                if (inputRoundValue.Text == string.Empty)
                {
                    //OKE
                    inputRoundValue.Text = "0";
                }
                else
                {
                    if (decimal.TryParse(inputRoundValue.Text, out roundvalue)) roundvalue = decimal.Parse(inputRoundValue.Text);
                    else
                    {
                        MsgBox("Invalid round value", this.Page, this);

                        return;
                    }
                }
                //end added by jone
                
                SqlCommand cmd = new SqlCommand("UpdateRnd", conMatWorkFlow);
                #region new approval by Rai 01042019
                cmd.Parameters.AddWithValue("SupplierName", inputSupplierName.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("PurposeofUses", ddPurposeofUses.SelectedValue);
                cmd.Parameters.AddWithValue("FMName", inputFMName.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("ClientName", inputClientName.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("FMForecastEstMonth", inputFMForecastMonth.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("FMAdoptionEstYear", inputFMAdoptionEstimationYear.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("DoseEst", inputDoseEstimation.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("RMPotentialUseMonth", inputRMPotentialUseMonth.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("RMPotentialUseYear", inputRMPotentialUseYear.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("AddonPrice", inputPrice.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("PriceMOQ", inputPriceMOQ.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("PriceAbsorbed", inputPriceAbsorbed.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("Status", inputStatus.Text.Trim().ToUpper());
                #endregion new approval by Rai 01042019
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "IndStdCode",
                    Value = this.inputIndStdDesc.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 8
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "NetUnit",
                    Value = this.inputNetWeightUnit.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SpclProcurement",
                    Value = this.inputSpcProc.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "COProd",
                    Value = this.stringCoProd.ToString().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PeriodIndForSELD",
                    Value = this.ddListSLED.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MatType",
                    Value = this.inputMatTyp.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 6
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RoundingValue",
                    Value = this.inputRoundValue.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 19
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MinLotSize",
                    Value = this.inputMinLotSize.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TotalShelfLife",
                    Value = this.inputTotalShelfLife.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MinShelfLife",
                    Value = this.inputMinRemShLf.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ForeignTrade",
                    Value = this.inputCommImpCode.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "NetWeight",
                    Value = this.inputNetWeight.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.Decimal,
                    Precision = 18,
                    Scale = 3
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Type",
                    Value = rmsffgMenuLabel.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 3
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMatID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MatlGroup",
                    Value = this.inputMatGr.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 9
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MatlGrpPack",
                    Value = this.inputPckgMat.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialDesc",
                    Value = this.inputMatDesc.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 40
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "UoM",
                    Value = this.inputBsUntMeas.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 3
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "OldMatNumb",
                    Value = this.inputOldMatNum.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Division",
                    Value = this.inputDivision.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 2
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Plant",
                    Value = this.inputPlant.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Sloc",
                    Value = this.inputStorLoc.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SOrg",
                    Value = this.inputSalesOrg.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "DistrChl",
                    Value = this.inputDistrChl.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 2
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProcType",
                    Value = this.inputProcType.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ModifiedBy",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdTracking = new SqlCommand
                    ("IF ((SELECT Status FROM Tbl_Tracking WHERE TransID=@TransID AND MaterialID=@MaterialID) IS NULL) BEGIN UPDATE Tbl_Tracking SET MaterialDesc=@MaterialDesc WHERE TransID=@TransID AND MaterialID=@MaterialID END ELSE IF ((SELECT Status FROM Tbl_Tracking WHERE TransID=@TransID AND MaterialID=@MaterialID) ='REVISIONRND') BEGIN UPDATE Tbl_Tracking SET MaterialDesc=@MaterialDesc, Status = 'REVISIONRNDAPPROVAL' WHERE TransID=@TransID AND MaterialID=@MaterialID END", conMatWorkFlow);
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RDEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialDesc",
                    Value = this.inputMatDesc.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 40
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMatID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdTracking.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                if(chkbxCoProd.Checked == true)
                {
                    SqlCommand cmdCoProd = new SqlCommand("UPDATE Tbl_Material SET FxdPrice = 'X' WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdCoProd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = this.lblTransID.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCoProd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = this.inputMatID.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdCoProd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdCoProd = new SqlCommand("UPDATE Tbl_Material SET FxdPrice = '' WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdCoProd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = this.lblTransID.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCoProd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = this.inputMatID.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdCoProd.ExecuteNonQuery();
                }
                conMatWorkFlow.Close();

                Response.Redirect("../Pages/rndPage", true);
            }
        }
        protected void CancelUpd_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmdDetailNew = new SqlCommand("DELETE FROM Tbl_DetailUomMat WHERE TransID=TransID AND New=@New", conMatWorkFlow);
            cmdDetailNew.Parameters.AddWithValue("New", "");
            cmdDetailNew.Parameters.Add(new SqlParameter
            {
                ParameterName = "TrasnID",
                Value = this.lblTransID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdDetailNew.ExecuteNonQuery();
            conMatWorkFlow.Close();

            Response.Redirect("~/Pages/rndPage");
        }
        protected void btnCancelApprove_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/rndPage");
        }
        protected void Approve_Click(object sender, EventArgs e)
        {
            if (lblPosition.Text == "R&D MGR" && inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                ddPDMGRDecission.SelectedValue = "1";
                ddBDMMGRDecission.SelectedValue = "3";
            }
            else if (lblPosition.Text == "BD MGR" && inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                ddBDMMGRDecission.SelectedValue = "1";
            }
            
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            try
            {
                SqlCommand cmdAPPROVE = new SqlCommand("ApproveRDMGR", conMatWorkFlow);
                cmdAPPROVE.Parameters.AddWithValue("PDMgrComment", inputPDMGRComment.Text);
                cmdAPPROVE.Parameters.AddWithValue("PDMgrDecission", ddPDMGRDecission.SelectedValue);
                cmdAPPROVE.Parameters.AddWithValue("BDMgrComment", inputBDMMGRComment.Text);
                cmdAPPROVE.Parameters.AddWithValue("BDMgrDecission", ddBDMMGRDecission.SelectedValue);
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMatID.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RDApproveBy",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RDApproveTime",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RDEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProcStart",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PlanStart",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "QCStart",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "QAStart",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "QRStart",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.Session["Devisi"].ToString().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TypeStatus",
                    Value = "End",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdAPPROVE.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.Session["Usnam"].ToString().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdAPPROVE.CommandType = CommandType.StoredProcedure;
                cmdAPPROVE.ExecuteNonQuery();
                conMatWorkFlow.Close();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }

            Response.Redirect("~/Pages/rndPage");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            if (lblUser.Text == "bdmgr")
            {
                conMatWorkFlow.Open();
                try
                {
                    SqlCommand cmdBDMGR = new SqlCommand("saveBDMGRAfterComment", conMatWorkFlow);
                    cmdBDMGR.Parameters.AddWithValue("BDMgrComment", inputBDMMGRComment.Text.Trim());
                    cmdBDMGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = this.lblTransID.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdBDMGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = this.inputMatID.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdBDMGR.CommandType = CommandType.StoredProcedure;
                    cmdBDMGR.ExecuteNonQuery();
                    conMatWorkFlow.Close();

                    Response.Redirect("../Pages/rndPage", false);

                }
                catch (Exception exxxxxx)
                {

                }
                conMatWorkFlow.Close();
            }
            else
            {
                if (inputNetWeight.Text == "0")
                {
                    MsgBox("Net Weight cannot be 0!", this.Page, this);
                    return;
                }
                autoGenTransID();
                autoGenLogID();
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                if (rmsffgMenuLabel.Text.ToUpper().Trim() != "RM")
                {
                    // doing checking
                    DataTable dtCheck = new DataTable();
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                    param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

                    //send param to SP sql
                    dtCheck = sqlC.ExecuteDataTable("CheckDetailUom", param);

                    if (dtCheck.Rows.Count == 0)
                    {
                        // munculkan pesan bahwa sudah ada
                        MsgBox("Your detail Uom table cannot be empty!", this.Page, this);
                        return;
                    }
                }
                conMatWorkFlow.Close();

                if (rmsffgMenuLabel.Text != "RM")
                {
                    conMatWorkFlow.Open();
                    // doing checking
                    DataTable dtCheckB = new DataTable();
                    SqlParameter[] paramB = new SqlParameter[2];
                    paramB[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                    paramB[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

                    //send param to SP sql
                    dtCheckB = sqlC.ExecuteDataTable("checkGrossWeight", paramB);

                    if (dtCheckB.Rows.Count > 0)
                    {
                        // munculkan pesan bahwa sudah ada
                        MsgBox("Your gross weight value at detail Uom table cannot be 0!", this.Page, this);
                        return;
                    }
                    conMatWorkFlow.Close();
                }

                if (rmsffgMenuLabel.Text != "RM")
                {
                    conMatWorkFlow.Open();
                    // doing checking
                    DataTable dtCheckC = new DataTable();
                    SqlParameter[] paramC = new SqlParameter[3];
                    paramC[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                    paramC[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                    paramC[2] = new SqlParameter("@Uom", this.inputBsUntMeas.Text.Trim().ToUpper());

                    //send param to SP sql
                    dtCheckC = sqlC.ExecuteDataTable("CheckBun", paramC);

                    if (dtCheckC.Rows.Count == 0)
                    {
                        // munculkan pesan bahwa sudah ada
                        MsgBox("Your Bun value not exactly the same as Base Unit Measurement value. Please update the detail Uom table first.", this.Page, this);
                        return;
                    }
                    conMatWorkFlow.Close();
                }

                //added by jone
                decimal roundvalue;
                if (inputRoundValue.Text == string.Empty)
                {
                    //OKE
                    inputRoundValue.Text = "0";
                }
                else
                {
                    if (decimal.TryParse(inputRoundValue.Text, out roundvalue)) roundvalue = decimal.Parse(inputRoundValue.Text);
                    else
                    {
                        MsgBox("Invalid round value", this.Page, this);

                        return;
                    }
                }
                //end added by jone

                if (inputMatTyp.Text == "" || inputMatID.Text == "" || inputMatDesc.Text == "" || inputBsUntMeas.Text == "" || inputMatGr.Text == "" || inputSalesOrg.Text == "" || inputPlant.Text == "" || inputStorLoc.Text == "" || inputDistrChl.Text == "")
                {
                    MsgBox("One of the required field still empty.", this.Page, this);
                }
                else
                {
                    conMatWorkFlow.Open();
                    if (chkbxCoProd.Checked == true)
                    {
                        stringCoProd = "X";
                    }
                    else if (chkbxCoProd.Checked == false)
                    {
                        stringCoProd = "";
                    }

                    // doing checking
                    DataTable dtCheck = new DataTable();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());

                    //send param to SP sql
                    dtCheck = sqlC.ExecuteDataTable("selectAllMaterial_ByMaterialID", param);

                    if (dtCheck.Rows.Count > 0 && lblUser.Text != "bdmgr")
                    {
                        // munculkan pesan bahwa sudah ada
                        MsgBox("Your MaterialID " + this.inputMatID.Text + " is already Exist!", this.Page, this);
                        return;
                    }
                    else
                    {
                        try
                        {
                            SqlCommand cmdDetailNew = new SqlCommand("saveRnd", conMatWorkFlow);
                            cmdDetailNew.Parameters.AddWithValue("New", "x");
                            cmdDetailNew.Parameters.AddWithValue("SupplierName", inputSupplierName.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("PurposeofUses", ddPurposeofUses.SelectedValue);
                            cmdDetailNew.Parameters.AddWithValue("FMName", inputFMName.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("ClientName", inputClientName.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("FMForecastEstMonth", inputFMForecastMonth.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("FMAdoptionEstYear", inputFMAdoptionEstimationYear.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("DoseEst", inputDoseEstimation.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("RMPotentialUseMonth", inputRMPotentialUseMonth.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("RMPotentialUseYear", inputRMPotentialUseYear.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("AddonPrice", inputPrice.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("PriceMOQ", inputPriceMOQ.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("PriceAbsorbed", inputPriceAbsorbed.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.AddWithValue("Status", inputStatus.Text.Trim().ToUpper());
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "TransID",
                                Value = this.lblTransID.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 10
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "PriceControl",
                                Value = "",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 15
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "IndStdCode",
                                Value = this.inputIndStdDesc.Text.ToUpper().Trim(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 8
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NetUnit",
                                Value = this.inputNetWeightUnit.Text.ToUpper().Trim(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 5
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "SpclProcurement",
                                Value = this.inputSpcProc.Text.ToUpper().Trim(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 5
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "COProd",
                                Value = stringCoProd.ToString().Trim(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 1
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "PeriodIndForSELD",
                                Value = this.ddListSLED.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 10
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NewProc",
                                Value = "",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 1
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NewPlanner",
                                Value = "",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 1
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NewQC",
                                Value = "",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 1
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NewQA",
                                Value = "",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 1
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NewQR",
                                Value = "",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 1
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NewFICO",
                                Value = "",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 1
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "Type",
                                Value = rmsffgMenuLabel.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 3
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "MaterialID",
                                Value = this.inputMatID.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 18
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "MatType",
                                Value = this.inputMatTyp.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 6
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "MatlGrpPack",
                                Value = this.inputPckgMat.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 4
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "MaterialDesc",
                                Value = this.inputMatDesc.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 40
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "UoM",
                                Value = this.inputBsUntMeas.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 3
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "MatlGroup",
                                Value = this.inputMatGr.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 9
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "OldMatNumb",
                                Value = this.inputOldMatNum.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 25
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "Division",
                                Value = this.inputDivision.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 2
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "Plant",
                                Value = this.inputPlant.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 4
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "Sloc",
                                Value = this.inputStorLoc.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 4
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "SOrg",
                                Value = this.inputSalesOrg.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 4
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "DistrChl",
                                Value = this.inputDistrChl.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 2
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "ProcType",
                                Value = this.inputProcType.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 5
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "CreateBy",
                                Value = this.lblUser.Text.Trim(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 10
                            });
                            DateTime localDate = DateTime.Now;
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "CreateTime",
                                Value = localDate,
                                SqlDbType = SqlDbType.DateTime
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "RoundingValue",
                                Value = this.inputRoundValue.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 19
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "MinLotSize",
                                Value = this.inputMinLotSize.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 15
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "TotalShelfLife",
                                Value = this.inputTotalShelfLife.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 15
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "MinShelfLife",
                                Value = this.inputMinRemShLf.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 15
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "ForeignTrade",
                                Value = this.inputCommImpCode.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 25
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "NetWeight",
                                Value = this.inputNetWeight.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.Decimal,
                                Precision = 18,
                                Scale = 3
                            });

                            if (inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
                            {
                                cmdDetailNew.Parameters.Add(new SqlParameter
                                {
                                    ParameterName = "GlobalStatus",
                                    Value = "Waiting For PD Manager Approval",
                                    SqlDbType = SqlDbType.NVarChar,
                                    Size = 10
                                });
                            }
                            else
                            {
                                cmdDetailNew.Parameters.Add(new SqlParameter
                                {
                                    ParameterName = "GlobalStatus",
                                    Value = "",
                                    SqlDbType = SqlDbType.NVarChar,
                                    Size = 10
                                });
                            }

                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "LogID",
                                Value = this.lblLogID.Text.Trim().ToUpper(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 10
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "Module_User",
                                Value = this.Session["Devisi"].ToString().Trim(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 10
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "TypeStatus",
                                Value = "Start",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 10
                            });
                            cmdDetailNew.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "Usnam",
                                Value = this.Session["Usnam"].ToString().Trim(),
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 10
                            });
                            cmdDetailNew.CommandType = CommandType.StoredProcedure;
                            cmdDetailNew.ExecuteNonQuery();
                            conMatWorkFlow.Close();

                            Response.Redirect("../Pages/rndPage", false);
                        }
                        catch (Exception ex)
                        {
                            MsgBox(ex.ToString().Trim(), this.Page, this);
                        }
                    }
                }
            }
        }
        protected void Reject_Click(object sender, EventArgs e)
        {
            if (lblPosition.Text == "R&D MGR" && inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                ddPDMGRDecission.SelectedValue = "2";
                ddBDMMGRDecission.SelectedValue = "3";
            }
            else if (lblPosition.Text == "BD MGR" && inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                ddBDMMGRDecission.SelectedValue = "2";
            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("reject", conMatWorkFlow);
            cmd.Parameters.AddWithValue("MgrStatus", lblPosition.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("PDMgrComment", inputPDMGRComment.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("PDMgrDecission", ddPDMGRDecission.SelectedValue);
            cmd.Parameters.AddWithValue("BDMgrComment", inputBDMMGRComment.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("BDMgrDecission", ddBDMMGRDecission.SelectedValue);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMatID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "Module_User",
                Value = this.Session["Devisi"].ToString().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "LogID",
                Value = this.lblLogID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "Usnam",
                Value = this.Session["Usnam"].ToString().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "RejectRevisionNotes",
                Value = this.inputRejectReason.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 200
            });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conMatWorkFlow.Close();
            Response.Redirect("~/Pages/rndPage");
        }
        protected void CancelSave_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmdDelTmpDetail = new SqlCommand("DELETE FROM Tbl_DetailUomMat WHERE TransID=@TransID", conMatWorkFlow);
            cmdDelTmpDetail.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdDelTmpDetail.ExecuteNonQuery();
            conMatWorkFlow.Close();

            //conMatWorkFlow.Open();
            //SqlCommand cmdTRCLogTrans = new SqlCommand("DELETE FROM Tbl_LogTrans WHERE LogID=@LogID", conMatWorkFlow);
            //cmdTRCLogTrans.Parameters.Add(new SqlParameter
            //{
            //    ParameterName = "LogID",
            //    Value = this.lblLogID.Text.Trim().ToUpper(),
            //    SqlDbType = SqlDbType.NVarChar,
            //    Size = 10
            //});
            //cmdTRCLogTrans.ExecuteNonQuery();
            //conMatWorkFlow.Close();
            Response.Redirect("~/Pages/rndPage");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/rndPage");
        }

        //checkbox Co-Product
        protected void chkbxCoProd_CheckedChanged(object senders, EventArgs e)
        {
            if (chkbxCoProd.Checked == true)
            {
                stringCoProd = "X";
            }
            else
            {
                stringCoProd = "";
            }
        }

        //inputIndStdDesc_onBlur
        protected void inputIndStdDesc_onBlur(object sender, EventArgs e)
        {
            if (inputIndStdDesc.Text == "")
            {
                lblIndStdDesc.Text = absIndStdDesc.Text;
                lblIndStdDesc.ForeColor = Color.Black;
            }
            else if (inputIndStdDesc.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_IndStdDesc WHERE IndStdCode = @IndStdCode";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("IndStdCode", this.inputIndStdDesc.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblIndStdDesc.Text = dr["IndStdDesc"].ToString();
                    lblIndStdDesc.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblIndStdDesc.Text = "Wrong Input!";
                    lblIndStdDesc.ForeColor = Color.Red;
                    inputIndStdDesc.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputSpcProc_onBlur
        protected void inputSpcProc_onBlur(object sender, EventArgs e)
        {
            if (inputSpcProc.Text == "")
            {
                lblSpcProc.Text = absSpcProc.Text;
                lblSpcProc.ForeColor = Color.Black;
            }
            else if (inputSpcProc.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                if (inputProcType.Text == "X" || inputProcType.Text == "x")
                {
                    var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE SpclProcurement = @inputSpcProc";
                    SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                    cmd.Parameters.AddWithValue("inputSpcProc", this.inputSpcProc.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lblSpcProc.Text = dr["SpclProcDesc"].ToString();
                        lblSpcProc.ForeColor = Color.Black;
                    }
                    if (dr.HasRows == false)
                    {
                        lblSpcProc.Text = "Wrong Input!";
                        lblSpcProc.ForeColor = Color.Red;
                        inputSpcProc.Text = "";
                        MsgBox("Wrong Input", this.Page, this);
                    }
                }
                else if (inputProcType.Text == "E" || inputProcType.Text == "e")
                {
                    var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE SpclProcurement = @inputSpcProc AND SpclProcurement = '50'";
                    SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                    cmd.Parameters.AddWithValue("inputSpcProc", this.inputSpcProc.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lblSpcProc.Text = dr["SpclProcDesc"].ToString();
                        lblSpcProc.ForeColor = Color.Black;
                    }
                    if (dr.HasRows == false)
                    {
                        lblSpcProc.Text = "Wrong Input!";
                        lblSpcProc.ForeColor = Color.Red;
                        inputSpcProc.Text = "";
                        MsgBox("Wrong Input", this.Page, this);
                    }
                }
                else if (inputProcType.Text == "F" || inputProcType.Text == "f")
                {
                    var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE SpclProcurement = @inputSpcProc AND SpclProcurement NOT LIKE'50'";
                    SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                    cmd.Parameters.AddWithValue("inputSpcProc", this.inputSpcProc.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lblSpcProc.Text = dr["SpclProcDesc"].ToString();
                        lblSpcProc.ForeColor = Color.Black;
                    }
                    if (dr.HasRows == false)
                    {
                        lblSpcProc.Text = "Wrong Input!";
                        lblSpcProc.ForeColor = Color.Red;
                        inputSpcProc.Text = "";
                        MsgBox("Wrong Input", this.Page, this);
                    }
                }
                conMatWorkFlow.Close();
            }
        }
        //inputCommImpCode_onBlur
        protected void inputCommImpCode_onBlur(object sender, EventArgs e)
        {
            if (inputCommImpCode.Text == "")
            {
                lblCommImpCode.Text = absCommImpCodeNo.Text;
                lblCommImpCode.ForeColor = Color.Black;
            }
            else if (inputCommImpCode.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblCommImp", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputCommImpCode", this.inputCommImpCode.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblCommImpCode.Text = dr["ForeignDesc"].ToString();
                    lblCommImpCode.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblCommImpCode.Text = "Wrong Input!";
                    lblCommImpCode.ForeColor = Color.Red;
                    inputCommImpCode.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputVolUnt_onBlur
        protected void inputVolUnt_onBlur(object sender, EventArgs e)
        {
            if (inputVolUnt.Text == "")
            {
                lblVolUnt.Text = absVolUnt.Text;
                lblVolUnt.ForeColor = Color.Black;
            }
            else if (inputVolUnt.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputVolUnt";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputVolUnt", this.inputVolUnt.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblVolUnt.Text = dr["UoM_Desc"].ToString();
                    lblVolUnt.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblVolUnt.Text = "Wrong Input!";
                    lblVolUnt.ForeColor = Color.Red;
                    inputVolUnt.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputMatTyp_onBlur
        protected void inputMatTyp_onBlur(object sender, EventArgs e)
        {
            if (inputMatTyp.Text == "")
            {
                lblMatTyp.Text = absMatType.Text;
                lblMatTyp.ForeColor = Color.Black;
                inputMatTyp.Focus();
            }
            else if (inputMatTyp.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblMatType", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputMatTyp", this.inputMatTyp.Text);
                cmd.Parameters.AddWithValue("Type", this.rmsffgMenuLabel.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblMatTyp.Text = dr["MatTypeDesc"].ToString();
                    lblMatTyp.ForeColor = Color.Black;
                    inputMatID.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblMatTyp.Text = "Wrong Input!";
                    lblMatTyp.ForeColor = Color.Red;
                    inputMatTyp.Focus();
                    inputMatTyp.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            if (inputMatTyp.Text == "SFAT" || inputMatTyp.Text == "sfat")
            {
                MinMaxMatID.ValidationExpression = "^[-_,.A-Za-z0-9]{7,18}$";
                MinMaxMatID.ErrorMessage = "Minimum 7, maximum 18 characters required and no special characters.";
            }
            else
            {
                MinMaxMatID.ValidationExpression = "^[-_,.A-Za-z0-9]{8,18}$";
                MinMaxMatID.ErrorMessage = "Minimum 8, maximum 18 characters required and no special characters.";
            }
            if (inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                fieldsetAddon.Visible = true;
                MRP1LOTSizeDt.Visible = true;
            }
            else
            {
                fieldsetAddon.Visible = false;
                MRP1LOTSizeDt.Visible = false;
            }
        }
        //inputPckgMat_onBlur
        protected void inputPckgMat_onBlur(object sender, EventArgs e)
        {
            if (inputPckgMat.Text == "")
            {
                lblPckgMat.Text = absPckgMat.Text;
                lblPckgMat.ForeColor = Color.Black;
                inputPckgMat.Focus();
            }
            else if (inputPckgMat.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblPackMat", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputPckgMat", this.inputPckgMat.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPckgMat.Text = dr["MatlGrpPack_Desc"].ToString();
                    lblPckgMat.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblPckgMat.Text = "Wrong Input!";
                    lblPckgMat.ForeColor = Color.Red;
                    inputPckgMat.Focus();
                    inputPckgMat.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputMatGr_onBlur
        protected void inputMatGr_onBlur(object sender, EventArgs e)
        {
            if (inputMatGr.Text == "")
            {
                lblMatGr.Text = absMatGr.Text;
                lblMatGr.ForeColor = Color.Black;
                inputMatGr.Focus();
            }
            else if (inputMatGr.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblMatGr", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputMatGr", this.inputMatGr.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblMatGr.Text = dr["MatlGroup_Desc"].ToString();
                    lblMatGr.ForeColor = Color.Black;
                    inputOldMatNum.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblMatGr.Text = "Wrong Input!";
                    lblMatGr.ForeColor = Color.Red;
                    inputMatGr.Focus();
                    inputMatGr.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputBsUntMeas_onBlur
        protected void inputBsUntMeas_onBlur(object sender, EventArgs e)
        {
            if (inputBsUntMeas.Text == "")
            {
                inputBun.Text = "";
                lblBsUntMeas.Text = absBsUntMeas.Text;
                lblBMeas.Text = absBsUntMeas.Text;
                lblBsUntMeas.ForeColor = Color.Black;
                lblBMeas.ForeColor = Color.Black;
                inputBsUntMeas.Focus();
            }
            else if (inputBsUntMeas.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblBsUntMeas", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputBsUntMeas.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    inputBun.Text = dr["UoM"].ToString();
                    inputNetWeightUnit.Text = dr["UoM"].ToString();
                    inputWeightUnt.Text = dr["UoM"].ToString();
                    lblNetWeightUnit.Text = dr["UoM_Desc"].ToString();
                    lblBsUntMeas.Text = dr["UoM_Desc"].ToString();
                    lblBMeas.Text = dr["UoM_Desc"].ToString();
                    lblNetWeightUnit.ForeColor = Color.Black;
                    lblBsUntMeas.ForeColor = Color.Black;
                    lblBMeas.ForeColor = Color.Black;
                    inputMatGr.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblBsUntMeas.Text = "Wrong Input!";
                    lblBsUntMeas.ForeColor = Color.Red;
                    inputBsUntMeas.Focus();
                    inputBsUntMeas.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputDivision_onBlur
        protected void inputDivision_onBlur(object sender, EventArgs e)
        {
            if (inputDivision.Text == "")
            {
                lblDivision.Text = absDivision.Text;
                lblDivision.ForeColor = Color.Black;
                inputDivision.Focus();
            }
            else if (inputDivision.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblDivision", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputDivision", this.inputDivision.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblDivision.Text = dr["Dv_Desc"].ToString();
                    lblDivision.ForeColor = Color.Black;
                    inputPckgMat.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblDivision.Text = "Wrong Input!";
                    lblDivision.ForeColor = Color.Red;
                    inputDivision.Focus();
                    inputDivision.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputPlant_onBlur
        protected void inputPlant_onBlur(object sender, EventArgs e)
        {
            if (inputPlant.Text == "")
            {
                lblPlant.Text = absPlant.Text;
                lblPlant.ForeColor = Color.Black;
                lblStorLoc.Text = absStorLoc.Text;
                lblStorLoc.ForeColor = Color.Black;
                inputStorLoc.Text = "";
                inputPlant.Focus();
            }
            else if (inputPlant.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblPlant", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputPlant", this.inputPlant.Text);
                cmd.Parameters.AddWithValue("SOrg", this.inputSalesOrg.Text);
                cmd.Parameters.AddWithValue("Usnam", this.lblUser.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPlant.Text = dr["Plant_Desc"].ToString();
                    lblPlant.ForeColor = Color.Black;
                    lblStorLoc.Text = absStorLoc.Text;
                    lblStorLoc.ForeColor = Color.Black;
                    inputStorLoc.Text = "";
                    inputStorLoc.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblPlant.Text = "Wrong Input!";
                    lblPlant.ForeColor = Color.Red;
                    inputPlant.Focus();
                    inputPlant.Text = "";
                    lblStorLoc.Text = absStorLoc.Text;
                    lblStorLoc.ForeColor = Color.Black;
                    inputStorLoc.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            if (inputMatTyp.Text.Trim().ToUpper() == "RMSV" && inputPlant.Text == "5200")
            {
                fieldsetAddon.Visible = true;
                MRP1LOTSizeDt.Visible = true;
            }
            else
            {
                fieldsetAddon.Visible = false;
                MRP1LOTSizeDt.Visible = false;
            }
        }
        //inputSalesOrg_onBlur
        protected void inputSalesOrg_onBlur(object sender, EventArgs e)
        {
            if (inputSalesOrg.Text == "")
            {
                lblSalesOrg.Text = absSalesOrg.Text;
                lblSalesOrg.ForeColor = Color.Black;

                lblPlant.Text = absPlant.Text;
                lblPlant.ForeColor = Color.Black;
                inputPlant.Text = "";
                lblStorLoc.Text = absStorLoc.Text;
                lblStorLoc.ForeColor = Color.Black;
                inputStorLoc.Text = "";

                inputSalesOrg.Focus();
            }
            else if (inputSalesOrg.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblSalesOrg", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputSalesOrg", this.inputSalesOrg.Text);
                cmd.Parameters.AddWithValue("Usnam", this.lblUser.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblSalesOrg.Text = dr["SOrg_Desc"].ToString();
                    lblSalesOrg.ForeColor = Color.Black;
                    inputDistrChl.Focus();

                    lblPlant.Text = absPlant.Text;
                    lblPlant.ForeColor = Color.Black;
                    inputPlant.Text = "";
                    lblStorLoc.Text = absStorLoc.Text;
                    lblStorLoc.ForeColor = Color.Black;
                    inputStorLoc.Text = "";
                }
                if (dr.HasRows == false)
                {
                    lblSalesOrg.Text = "Wrong Input!";
                    lblSalesOrg.ForeColor = Color.Red;
                    inputSalesOrg.Focus();
                    inputSalesOrg.Text = "";

                    lblPlant.Text = absPlant.Text;
                    lblPlant.ForeColor = Color.Black;
                    inputPlant.Text = "";
                    lblStorLoc.Text = absStorLoc.Text;
                    lblStorLoc.ForeColor = Color.Black;
                    inputStorLoc.Text = "";

                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            srcPlantModalBinding();
        }
        //inputStorLoc_onBlur
        protected void inputStorLoc_onBlur(object sender, EventArgs e)
        {
            if (inputStorLoc.Text == "")
            {
                lblStorLoc.Text = absStorLoc.Text;
                lblStorLoc.ForeColor = Color.Black;
                inputStorLoc.Focus();
            }
            else if (inputStorLoc.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblStorLoc", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputStorLoc", this.inputStorLoc.Text);
                cmd.Parameters.AddWithValue("inputPlant", this.inputPlant.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblStorLoc.Text = dr["SLoc_Desc"].ToString();
                    lblStorLoc.ForeColor = Color.Black;
                    inputStorLoc.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblStorLoc.Text = "Wrong Input!";
                    lblStorLoc.ForeColor = Color.Red;
                    inputStorLoc.Focus();
                    inputStorLoc.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputDistrChl_onBlur
        protected void inputDistrChl_onBlur(object sender, EventArgs e)
        {
            if (inputDistrChl.Text == "")
            {
                lblDistrChl.Text = absDistrChl.Text;
                lblDistrChl.ForeColor = Color.Black;
                inputDistrChl.Focus();
            }
            else if (inputDistrChl.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblDistrChl", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputDistrChl", this.inputDistrChl.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblDistrChl.Text = dr["DistrChl_Desc"].ToString();
                    lblDistrChl.ForeColor = Color.Black;
                    inputPlant.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblDistrChl.Text = "Wrong Input!";
                    lblDistrChl.ForeColor = Color.Red;
                    inputPlant.Focus();
                    inputDistrChl.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputProcType_onBlur
        protected void inputProcType_onBlur(object sender, EventArgs e)
        {
            if (inputProcType.Text == "")
            {
                lblProcType.Text = absProcType.Text;
                lblProcType.ForeColor = Color.Black;
                inputProcType.Focus();
            }
            else if (inputProcType.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblProcType", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputProcType", this.inputProcType.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    inputSpcProc.Text = "";
                    lblSpcProc.Text = absSpcProc.Text;
                    lblProcType.Text = dr["ProcTypeDesc"].ToString();
                    lblProcType.ForeColor = Color.Black;
                    inputProcType.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblProcType.Text = "Wrong Input!";
                    lblProcType.ForeColor = Color.Red;
                    inputProcType.Focus();
                    inputProcType.Text = "";
                    inputSpcProc.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            if (inputProcType.Text.ToUpper().Trim() == "E")
            {
                chkbxCoProd.Enabled = true;
            }
            else
            {
                chkbxCoProd.Enabled = false;
                chkbxCoProd.Checked = false;
            }
            srcSpcProcModalBinding();
        }
        //inputAun_onBlur
        protected void inputAun_onBlur(object sender, EventArgs e)
        {
            if (inputAun.Text == "")
            {
                lblAMeas.Text = absAMeas.Text;
                lblAMeas.ForeColor = Color.Black;
                inputAun.Focus();
            }
            else if (inputAun.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputAun";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputAun", this.inputAun.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblAMeas.Text = dr["UoM_Desc"].ToString();
                    lblAMeas.ForeColor = Color.Black;
                    inputY.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblAMeas.Text = "Wrong Input!";
                    lblAMeas.ForeColor = Color.Red;
                    inputAun.Focus();
                    inputAun.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //txtAun_onBlur
        protected void txtAun_onBlur(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.TextBox tb1 = ((System.Web.UI.WebControls.TextBox)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));
            System.Web.UI.WebControls.TextBox txtAun = (System.Web.UI.WebControls.TextBox)rp1.FindControl("txtAun");
            System.Web.UI.WebControls.Label lblAMeas = (System.Web.UI.WebControls.Label)rp1.FindControl("lblAMeas");
            System.Web.UI.WebControls.TextBox txtY = (System.Web.UI.WebControls.TextBox)rp1.FindControl("txtY");

            if (txtAun.Text == "")
            {
                lblAMeas.Text = "";
                lblAMeas.ForeColor = Color.Black;
                txtAun.Focus();
            }
            else if (txtAun.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @txtAun";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("txtAun", txtAun.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblAMeas.Text = dr["UoM_Desc"].ToString();
                    lblAMeas.ForeColor = Color.Black;
                    txtY.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblAMeas.Text = "Wrong Input!";
                    lblAMeas.ForeColor = Color.Red;
                    txtAun.Focus();
                }
                conMatWorkFlow.Close();
            }
        }
        //txtBun_onBlur
        protected void txtBun_onBlur(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.TextBox tb1 = ((System.Web.UI.WebControls.TextBox)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));
            System.Web.UI.WebControls.TextBox txtBun = (System.Web.UI.WebControls.TextBox)rp1.FindControl("txtBun");
            System.Web.UI.WebControls.Label lblBMeas = (System.Web.UI.WebControls.Label)rp1.FindControl("lblBMeas");

            if (txtBun.Text == "")
            {
                lblBMeas.Text = "";
                lblBMeas.ForeColor = Color.Black;
                txtBun.Focus();
            }
            else if (txtBun.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @txtBun";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("txtBun", txtBun.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblBMeas.Text = dr["UoM_Desc"].ToString();
                    lblBMeas.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblBMeas.Text = "Wrong Input!";
                    lblBMeas.ForeColor = Color.Red;
                    txtBun.Focus();
                }
                conMatWorkFlow.Close();
            }
        }
        //inputWeightUnt_onBlur
        protected void inputWeightUnt_onBlur(object sender, EventArgs e)
        {
            if (inputWeightUnt.Text == "")
            {
                lblWeightUnt.Text = absWeightUnt.Text;
                lblWeightUnt.ForeColor = Color.Black;
            }
            else if (inputWeightUnt.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputWeightUnt";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputWeightUnt", this.inputWeightUnt.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblWeightUnt.Text = dr["UoM_Desc"].ToString();
                    lblWeightUnt.ForeColor = Color.Black;
                    inputVolume.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblWeightUnt.Text = "Wrong Input!";
                    lblWeightUnt.ForeColor = Color.Red;
                    inputWeightUnt.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputNetWeightUnt_onBlur
        protected void inputNetWeightUnt_onBlur(object sender, EventArgs e)
        {
            if (inputNetWeightUnit.Text == "")
            {
                lblNetWeightUnit.Text = absNetWeightUnit.Text;
                lblNetWeightUnit.ForeColor = Color.Black;
            }
            else if (inputNetWeightUnit.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputNetWeightUnt";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputNetWeightUnt", this.inputNetWeightUnit.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblNetWeightUnit.Text = dr["UoM_Desc"].ToString();
                    lblNetWeightUnit.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblNetWeightUnit.Text = "Wrong Input!";
                    lblNetWeightUnit.ForeColor = Color.Red;
                    inputNetWeightUnit.Text = "";
                    MsgBox("Wrong Input", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputGrossWeight_onBlur
        protected void inputGrossWeight_TextChanged(object sender, EventArgs e)
        {
            decimal GrossWeight;
            decimal NetWeight;
            decimal.TryParse(inputGrossWeight.Text, out GrossWeight);
            decimal.TryParse(inputNetWeight.Text, out NetWeight);
            decimal plus;
            decimal.TryParse("0.001", out plus);

            if (GrossWeight <= NetWeight)
            {
                MsgBox("Your gross weight cannot be less or equal with net weight!", this.Page, this);
                inputGrossWeight.Text = (NetWeight + plus).ToString();
                return;
            }
        }
        //inputNetWeight_TextChange
        protected void inputNetWeight_TextChanged(object sender, EventArgs e)
        {
            decimal GrossWeight;
            decimal NetWeight;
            decimal.TryParse(inputGrossWeight.Text, out GrossWeight);
            decimal.TryParse(inputNetWeight.Text, out NetWeight);
            decimal plus;
            decimal.TryParse("0.001", out plus);

            tmpBindRepeater();
            inputGrossWeight.Text = (NetWeight + plus).ToString();
        }
        //inputFMAdoptionEstimationYear_TextChanged
        protected void inputFMAdoptionEstimationMonth_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            Int32.TryParse(inputFMForecastMonth.Text, out x);
            int y = x * 12;
            inputFMAdoptionEstimationYear.Text = y.ToString();
        }
        //inputRMPotentialUseYear_TextChanged
        protected void inputRMPotentialUseMonth_TextChanged(object sender, EventArgs e)
        {
            long x = 0;
            Int64.TryParse(inputRMPotentialUseMonth.Text, out x);
            long y = x * 12;
            inputRMPotentialUseYear.Text = y.ToString();
            
            long a;
            long b;
            Int64.TryParse(inputRMPotentialUseYear.Text, out a);
            Int64.TryParse(inputPrice.Text, out b);
            long c = a * b;
            inputPriceAbsorbed.Text = c.ToString();

            long d;
            long f;
            Int64.TryParse(inputPriceMOQ.Text, out d);
            Int64.TryParse(inputPriceAbsorbed.Text, out f);
            if (d <= f)
            {
                inputStatus.Text = "No Surcharge";
            }
            else
            {
                inputStatus.Text = "Surcharge";
            }
        }
        //inputPrice_TextChanged
        protected void inputPrice_TextChanged(object sender, EventArgs e)
        {
            long x = 0;
            Int64.TryParse(inputMinLotSize.Text, out x);
            long y = 0;
            Int64.TryParse(inputPrice.Text, out y);
            long z = x * y;
            inputPriceMOQ.Text = z.ToString();

            long a;
            long b;
            Int64.TryParse(inputRMPotentialUseYear.Text, out a);
            Int64.TryParse(inputPrice.Text, out b);
            long c = a * b;
            inputPriceAbsorbed.Text = c.ToString();

            long d;
            long f;
            Int64.TryParse(inputPriceMOQ.Text, out d);
            Int64.TryParse(inputPriceAbsorbed.Text, out f);
            if (d <= f)
            {
                inputStatus.Text = "No Surcharge";
            }
            else
            {
                inputStatus.Text = "Surcharge";
            }
        }
        //SLED TextChanged
        protected void SLED_TextChanged(object sender, EventArgs e)
        {
            if (ddListSLED.Text == "D")
            {
                inputMinRemShLf.Text = "";
                lblMinRemShelfLife.Text = "DAYS";
                rangeValidatorMinRemShelf.MaximumValue = "999";
                rangeValidatorMinRemShelf.ErrorMessage = "The value must be from 1 to 999!";
            }
            else
            {
                inputMinRemShLf.Text = "";
                lblMinRemShelfLife.Text = "MONTH";
                rangeValidatorMinRemShelf.MaximumValue = "12";
                rangeValidatorMinRemShelf.ErrorMessage = "The value must be from 1 to 12!";
            }
        }
        //inputPDMGRComment_TextChanged TextChanged
        protected void inputPDMGRComment_TextChanged(object sender, EventArgs e)
        {
            inputRejectReason.Text = inputPDMGRComment.Text;
        }
        //inputBDMMGRComment_TextChanged TextChanged
        protected void inputBDMMGRComment_TextChanged(object sender, EventArgs e)
        {
            inputRejectReason.Text = inputBDMMGRComment.Text;
        }

        //rnd Manager
        protected void bindLblBsUntMeas()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblBsUntMeas", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputBsUntMeas.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                inputBun.Text = dr["UoM"].ToString();
                inputWeightUnt.Text = dr["UoM"].ToString();
                lblBsUntMeas.Text = dr["UoM_Desc"].ToString();
                lblBMeas.Text = dr["UoM_Desc"].ToString();
                lblBsUntMeas.ForeColor = Color.Black;
                lblBMeas.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            SqlCommand cmdP = new SqlCommand("bindLblBsUntMeas", conMatWorkFlow);
            cmdP.Parameters.AddWithValue("inputBsUntMeas", this.inputNetWeightUnit.Text);
            cmdP.CommandType = CommandType.StoredProcedure;
            SqlDataReader drP = cmdP.ExecuteReader();
            while (drP.Read())
            {
                lblNetWeightUnit.Text = drP["UoM_Desc"].ToString();
                lblNetWeightUnit.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblIndStdDesc()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblIndStdDesc", conMatWorkFlow);
            cmd.Parameters.AddWithValue("IndStdCode", this.inputIndStdDesc.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblIndStdDesc.Text = dr["IndStdDesc"].ToString();
                lblIndStdDesc.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblMatGr()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblMatGr", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputMatGr", this.inputMatGr.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMatGr.Text = dr["MatlGroup_Desc"].ToString();
                lblMatGr.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblDivision()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblDivision", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputDivision", this.inputDivision.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblDivision.Text = dr["Dv_Desc"].ToString();
                lblDivision.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblPackMat()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblPackMat", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputPckgMat", this.inputPckgMat.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblPckgMat.Text = dr["MatlGrpPack_Desc"].ToString();
                lblPckgMat.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblMatType()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblMatType", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputMatTyp", this.inputMatTyp.Text);
            cmd.Parameters.AddWithValue("Type", this.rmsffgMenuLabel.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMatTyp.Text = dr["MatTypeDesc"].ToString();
                lblMatTyp.ForeColor = Color.Black;
                inputPlant.Focus();
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblPlant()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblPlant", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputPlant", this.inputPlant.Text);
            cmd.Parameters.AddWithValue("SOrg", this.inputSalesOrg.Text);
            cmd.Parameters.AddWithValue("Usnam", this.lblUser.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblPlant.Text = dr["Plant_Desc"].ToString();
                lblPlant.ForeColor = Color.Black;
                inputStorLoc.Focus();
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblStorLoc()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblStorLoc", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputStorLoc", this.inputStorLoc.Text);
            cmd.Parameters.AddWithValue("inputPlant", this.inputPlant.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblStorLoc.Text = dr["SLoc_Desc"].ToString();
                lblStorLoc.ForeColor = Color.Black;
                inputSalesOrg.Focus();
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblSalesOrg()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblSalesOrg", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputSalesOrg", this.inputSalesOrg.Text);
            cmd.Parameters.AddWithValue("Usnam", this.lblUser.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblSalesOrg.Text = dr["SOrg_Desc"].ToString();
                lblSalesOrg.ForeColor = Color.Black;
                inputDistrChl.Focus();
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblCommImp()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblCommImp", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputCommImpCode", this.inputCommImpCode.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblCommImpCode.Text = dr["ForeignDesc"].ToString();
                lblCommImpCode.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblDistrChl()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblDistrChl", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputDistrChl", this.inputDistrChl.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblDistrChl.Text = dr["DistrChl_Desc"].ToString();
                lblDistrChl.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close(); ;
        }
        protected void bindLblProcType()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblProcType", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputProcType", this.inputProcType.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblProcType.Text = dr["ProcTypeDesc"].ToString();
                lblProcType.ForeColor = Color.Black;
                inputProcType.Focus();
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblSpecialProc()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblSpecialProc", conMatWorkFlow);
            cmd.Parameters.AddWithValue("SpclProcurement", this.inputSpcProc.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblSpcProc.Text = dr["SpclProcDesc"].ToString();
                lblSpcProc.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }

        protected void CreateMat2_Click(object sender, EventArgs e)
        {
            autoGenTransID();

            spanTransID.Style.Add("display", "flex");
            tmpBindRepeater();
            Master.FindControl("NavigationMenu").Visible = false;
            Master.FindControl("btnLogOut").Visible = false;

            if (rmsffgMenuLabel.Text == "RM")
            {
                listViewRnD.Visible = false;

                rmContent.Visible = true;
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;
            }
            else
            {
                listViewRnD.Visible = false;

                rmContent.Visible = true;
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;
                bscDt1Dimension.Visible = true;
                MRP1LOTSizeDt.Visible = true;
                foreignTradeDt.Visible = true;
                plantShelfLifeDt.Visible = true;
            }
        }

        protected void btnSearchMatType_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMatTyp", "$('#ItemModal').modal();", true);
            //txtSearchCriteria.Text = string.Empty;
            // BindDataGrid("");
            srcMatTypModalBinding("");
        }

        //Display Region
        //Navigation Menu Inside
        protected void NavigationMenuReport_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                MenuItem item = e.Item;
                if (item.Text == lblMenu.Text)
                {
                    if (item.Parent != null)
                    {
                        item.Parent.Selected = true;
                    }
                    else
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        protected void NavigationMenuReport_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value == "1")
            {
                lblMenu.Text = "R&D";
                rmContent.Visible = true;

                procTBL.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
                idxMenu.Text = "0";

                //RM Unlock All
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;
                bscDt1Dimension.Visible = true;
                MRP1LOTSizeDt.Visible = true;
                foreignTradeDt.Visible = true;
                plantShelfLifeDt.Visible = true;
                trCoProd.Visible = true;
                btnClose.Visible = true;
            }
            else if (e.Item.Value == "2")
            {
                lblMenu.Text = "Procurement";
                procTBL.Visible = true;

                rmContent.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
                idxMenu.Text = "1";
                btnClose.Visible = true;
            }
            else if (e.Item.Value == "3")
            {
                lblMenu.Text = "Planner";
                plannerTbl.Visible = true;

                procTBL.Visible = false;
                rmContent.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;

                bscDt1GnrlDtPlanner.Visible = true;
                MRP1.Visible = true;
                MRP2.Visible = true;
                MRP3.Visible = true;
                idxMenu.Text = "2";
                btnClose.Visible = true;
            }
            else if (e.Item.Value == "4")
            {
                lblMenu.Text = "QC";
                QCTbl.Visible = true;

                procTBL.Visible = false;
                rmContent.Visible = false;
                plannerTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
                idxMenu.Text = "3";
                btnClose.Visible = true;
            }
            else if (e.Item.Value == "5")
            {
                lblMenu.Text = "QA";
                QATbl.Visible = true;

                QCTbl.Visible = false;
                procTBL.Visible = false;
                rmContent.Visible = false;
                plannerTbl.Visible = false;
                QRTbl.Visible = false;
                idxMenu.Text = "4";
                btnClose.Visible = true;
            }
            else if (e.Item.Value == "6")
            {
                lblMenu.Text = "QR";
                QRTbl.Visible = true;

                QATbl.Visible = false;
                QCTbl.Visible = false;
                procTBL.Visible = false;
                rmContent.Visible = false;
                plannerTbl.Visible = false;
                idxMenu.Text = "5";
                btnClose.Visible = true;
            }
        }
        protected void ClassBindRepeater()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * from Tbl_QCClass WHERE TransID = @TransID and MaterialID = @MaterialID", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            rptClassTypeMgr.DataSource = ds;
            rptClassTypeMgr.DataBind();
            conMatWorkFlow.Close();
        }
        protected void QCDataBindRepeater()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("Select QD.*, IT.* from Tbl_QCData AS QD INNER JOIN Mstr_InspectionType AS IT ON QD.InspType = IT.InspType WHERE TransID = @TransID and MaterialID = @MaterialID", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            rptInspectionTypeMgr.DataSource = ds;
            rptInspectionTypeMgr.DataBind();
            conMatWorkFlow.Close();
        }

        //protected void GridViewListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    if(conMatWorkFlow.State == ConnectionState.Closed)
        //    {
        //        conMatWorkFlow.Open();
        //    }
        //    var dt = new DataTable();
        //    dt = GetData_srcListViewMatID("%%", "R&D", "");

        //    GridViewListView.DataSource = dt;
        //    GridViewListView.PageIndex = e.NewPageIndex;
        //    GridViewListView.DataBind();
        //    //lblGridViewPage.Text = "Page " + (GridViewListView.PageIndex + 1) + " of " + GridViewListView.PageCount;
        //    conMatWorkFlow.Close();
        //}

        private void gridSearch(string Filter)
        {
            if (Filter.Trim().ToUpper() == "ALL")
            {
                if (lstbxMatIDDESC.SelectedItem.Text == "Material ID")
                {
                    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                    ViewState["Row"] = 0;
                    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                    string Row = ddlPageSize.SelectedValue;
                    var dt = GetData_srcListViewMatID("%" + inputMatIDDESC.Text + "%", "R&D", "", lblPosition.Text.Trim());

                    GridViewListView.DataSource = dt;
                    GridViewListView.DataBind();


                    if (dt.Rows.Count > 0)
                    {
                        var select = dt.Rows.Count;

                        if (ViewState["Row"].ToString().Trim() == "0")
                        {
                            ViewState["grandtotal"] = select.ToString();
                            lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
                            // DivGrid.Visible = true;
                            // lblNoData.Visible = false;
                            ViewState["Row"] = 1;
                            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
                            lblTotalNumberOfPages.Text = pageCount.ToString();
                            txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
                            //  GridViewField.Visible = true;
                        }
                    }
                    else
                    {
                        //  GridViewField.Visible = false;
                        ViewState["grandtotal"] = 0;
                        // DivGrid.Visible = false;
                        //lblNoData.Visible = true;
                    }
                }
                else
                {
                    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                    ViewState["Row"] = 0;
                    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                    string Row = ddlPageSize.SelectedValue;
                    var dt = GetData_srcListViewMatDESC("%" + inputMatIDDESC.Text + "%", "R&D", "", lblPosition.Text.Trim());

                    GridViewListView.DataSource = dt;
                    GridViewListView.DataBind();


                    if (dt.Rows.Count > 0)
                    {
                        var select = dt.Rows.Count;

                        if (ViewState["Row"].ToString().Trim() == "0")
                        {
                            ViewState["grandtotal"] = select.ToString();
                            lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
                            // DivGrid.Visible = true;
                            // lblNoData.Visible = false;
                            ViewState["Row"] = 1;
                            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
                            lblTotalNumberOfPages.Text = pageCount.ToString();
                            txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
                            //  GridViewField.Visible = true;
                        }
                    }
                    else
                    {
                        //  GridViewField.Visible = false;
                        ViewState["grandtotal"] = 0;
                        // DivGrid.Visible = false;
                        //lblNoData.Visible = true;
                    }
                }
            }
            else if (Filter.Trim().ToUpper() == "RM")
            {
                GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                ViewState["Row"] = 0;
                string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                string Row = ddlPageSize.SelectedValue;
                var dt = GetData_srcListViewRM("R&D", "", lblPosition.Text.Trim());

                GridViewListView.DataSource = dt;
                GridViewListView.DataBind();


                if (dt.Rows.Count > 0)
                {
                    var select = dt.Rows.Count;

                    if (ViewState["Row"].ToString().Trim() == "0")
                    {
                        ViewState["grandtotal"] = select.ToString();
                        lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
                        // DivGrid.Visible = true;
                        // lblNoData.Visible = false;
                        ViewState["Row"] = 1;
                        int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
                        lblTotalNumberOfPages.Text = pageCount.ToString();
                        txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
                        //  GridViewField.Visible = true;
                    }
                }
                else
                {
                    //  GridViewField.Visible = false;
                    ViewState["grandtotal"] = 0;
                    // DivGrid.Visible = false;
                    //lblNoData.Visible = true;
                }
            }
            else if (Filter.Trim().ToUpper() == "SF")
            {
                GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                ViewState["Row"] = 0;
                string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                string Row = ddlPageSize.SelectedValue;
                var dt = GetData_srcListViewSF("R&D", "", lblPosition.Text.Trim());

                GridViewListView.DataSource = dt;
                GridViewListView.DataBind();


                if (dt.Rows.Count > 0)
                {
                    var select = dt.Rows.Count;

                    if (ViewState["Row"].ToString().Trim() == "0")
                    {
                        ViewState["grandtotal"] = select.ToString();
                        lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
                        // DivGrid.Visible = true;
                        // lblNoData.Visible = false;
                        ViewState["Row"] = 1;
                        int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
                        lblTotalNumberOfPages.Text = pageCount.ToString();
                        txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
                        //  GridViewField.Visible = true;
                    }
                }
                else
                {
                    //  GridViewField.Visible = false;
                    ViewState["grandtotal"] = 0;
                    // DivGrid.Visible = false;
                    //lblNoData.Visible = true;
                }
            }
            else if (Filter.Trim().ToUpper() == "FG")
            {
                GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                ViewState["Row"] = 0;
                string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                string Row = ddlPageSize.SelectedValue;
                var dt = GetData_srcListViewFG("R&D", "", lblPosition.Text.Trim());

                GridViewListView.DataSource = dt;
                GridViewListView.DataBind();


                if (dt.Rows.Count > 0)
                {
                    var select = dt.Rows.Count;

                    if (ViewState["Row"].ToString().Trim() == "0")
                    {
                        ViewState["grandtotal"] = select.ToString();
                        lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
                        // DivGrid.Visible = true;
                        // lblNoData.Visible = false;
                        ViewState["Row"] = 1;
                        int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
                        lblTotalNumberOfPages.Text = pageCount.ToString();
                        txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
                        //  GridViewField.Visible = true;
                    }
                }
                else
                {
                    //  GridViewField.Visible = false;
                    ViewState["grandtotal"] = 0;
                    // DivGrid.Visible = false;
                    //lblNoData.Visible = true;
                }
            }
        }

        protected void GoToPage_TextChanged(object sender, EventArgs e)
        {
            int pageNumber;
            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
            if (int.TryParse(txtGoToPage.Text.Trim(), out pageNumber) && pageNumber > 0 && pageNumber <= pageCount)
            {
                LoadPage(pageNumber - 1);
            }
            else
            {
                LoadPage(0);
            }
        }

        protected void btnPrev_OnClick(object sender, EventArgs e)
        {
            int pageNumber;
            if (int.TryParse(txtGoToPage.Text.Trim(), out pageNumber) && pageNumber > 1)
            {
                LoadPage(pageNumber - 2);
            }
            else
            {
                LoadPage(GridViewListView.PageIndex);
            }
        }

        protected void btnNext_OnClick(object sender, EventArgs e)
        {
            int pageNumber;
            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
            if (int.TryParse(txtGoToPage.Text.Trim(), out pageNumber) && pageNumber < pageCount)
            {
                LoadPage(pageNumber);
            }
            else
            {
                LoadPage(GridViewListView.PageIndex);
            }
        }

        private void LoadPage(int pageNumber)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageNumber);
            GridViewListView_PageIndexChanging(this, e);
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);
            GridViewListView.PageIndex = 0;
            gridSearch(FilterSearch.Text);
        }

        protected void GridViewListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewListView.PageIndex = e.NewPageIndex;
            if (lblSort.Text == "")
            {
                gridSearch(FilterSearch.Text);
            }
            else
            {
                SortGridView(lblSortExpression.Text, lblSortDirection.Text);
            }
        }

        protected void btnRejectModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReject", "$('#modalReject').dialog();", true);
        }

        //Gridview Sorting Method
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void GridViewListView_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
                lblSortDirection.Text = DESCENDING;
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
                lblSortDirection.Text = ASCENDING;
            }
            lblSort.Text = "X";
            lblSortExpression.Text = sortExpression;
        }

        private void SortGridView(string sortExpression, string direction)
        {
            GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


            ViewState["Row"] = 0;
            string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
            string Row = ddlPageSize.SelectedValue;

            //  You can cache the DataTable for improving performance
            DataTable dt = GetData_srcListViewMatID("%%", "R&D", "", lblPosition.Text.Trim());

            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;

            GridViewListView.DataSource = dv;
            GridViewListView.DataBind();

            if (dt.Rows.Count > 0)
            {
                var select = dt.Rows.Count;

                if (ViewState["Row"].ToString().Trim() == "0")
                {
                    ViewState["grandtotal"] = select.ToString();
                    lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
                    // DivGrid.Visible = true;
                    // lblNoData.Visible = false;
                    ViewState["Row"] = 1;
                    int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
                    lblTotalNumberOfPages.Text = pageCount.ToString();
                    txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
                    //  GridViewField.Visible = true;
                }
            }
            else
            {
                //  GridViewField.Visible = false;
                ViewState["grandtotal"] = 0;
                // DivGrid.Visible = false;
                //lblNoData.Visible = true;
            }
        }
    }
}