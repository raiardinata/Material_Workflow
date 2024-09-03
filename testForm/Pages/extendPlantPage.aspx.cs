using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using testForm.SQLConnect;

namespace testForm.Pages
{
    public partial class extendPlantPage : System.Web.UI.Page
    {
        SQLConnect.SQLConnect sqlC = new SQLConnect.SQLConnect();

        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());

        string stringInspectSet = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (lblPosition.Text != "R&D" && lblPosition.Text != "R&D MGR" && lblPosition.Text != "PD" && lblPosition.Text != "PD MGR")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
                srcListViewBinding();
            }
            else if (IsPostBack)
            {
                conMatWorkFlow.Open();
                //panggil sp yg load data comon table yg nyimpem type2 MRPType
                DataTable dt = new DataTable();

                SqlCommand cmd = new SqlCommand("bindCommonTableMRPType", conMatWorkFlow);
                cmd.CommandType = CommandType.StoredProcedure;
                var dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.Fill(dt);
                conMatWorkFlow.Close();
                /*Controllers.mrpTypeControllers MRPTypeValidation = new Controllers.mrpTypeControllers();
                var MRPTypeVal = MRPTypeValidation.MRPTypeValidation();
                if (MRPTypeVal.Count() > 0)*/
                {

                }
                // 
                if (inputMRPTyp.Text.ToUpper().Trim() == dt.Rows[0]["HIGH"].ToString())
                {
                    spcLblLOTSize.Text = "LOT Size ";
                    inputLOTSize.Attributes.Remove("required");
                    inputLOTSize.Attributes.Add("placeholder", "LOT Size");
                }
                else
                {
                    spcLblLOTSize.Text = "LOT Size* ";
                    inputLOTSize.Attributes.Add("required", "required");
                    inputLOTSize.Attributes.Add("placeholder", "LOT Size*");
                }
            }
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        protected DataTable GetData_srcListViewRMSFFG(string TypeUser, string Type)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TypeUser", TypeUser);
            param[1] = new SqlParameter("@Type", Type);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewRMSFFGExtendMat", param);

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
        }


        private DataTable Tbl_DetailUomMat_GetData(string TransID, string MaterialID)
        {
            SqlParameter[] param = new SqlParameter[2];

            // DataSet dt = new DataSet();
            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@TransID", TransID);
            param[1] = new SqlParameter("@MaterialID", MaterialID);


            //send param to SP sql
            // dt = sqlC.ExecuteDataSet("[Repeater_Tbl_DetailUomMat]", param);
            dt = sqlC.ExecuteDataTable("[Repeater_Tbl_DetailUomMat]", param);
            return dt;
        }

        protected void tmpBindRepeater()
        {

            // DataSet dt = new DataSet();
            DataTable dt = new DataTable();
            dt = Tbl_DetailUomMat_GetData(lblOldTransID.Text, lblMatID.Text);

            foreach (DataRow dr in dt.Rows)
            {
                if(dr["X"].ToString() == "1" && dr["Y"].ToString() == "1")
                {
                    inputGrossWeight.Text = dr["GrossWeight"].ToString();
                }
            }

            reptUntMeas.DataSource = dt;
            reptUntMeasProc.DataSource = dt;
            reptUntMeasMgr.DataSource = dt;
            reptUntMeas.DataBind();
            reptUntMeasProc.DataBind();
            reptUntMeasMgr.DataBind();


        }
        protected void tmpBindRepeaterNew()
        {

            // DataSet dt = new DataSet();
            DataTable dt = new DataTable();
            dt = Tbl_DetailUomMat_GetData(lblTransID.Text, inputMatID.Text);

            reptUntMeas.DataSource = dt;
            reptUntMeasProc.DataSource = dt;
            reptUntMeasMgr.DataSource = dt;
            reptUntMeas.DataBind();
            reptUntMeasProc.DataBind();
            reptUntMeasMgr.DataBind();


        }
        private DataSet Tbl_QCClass_GetData(string TransID, string MaterialID)
        {
            SqlParameter[] param = new SqlParameter[2];

            DataSet ds = new DataSet();
            param[0] = new SqlParameter("@TransID", TransID);
            param[1] = new SqlParameter("@MaterialID", MaterialID);


            //send param to SP sql
            ds = sqlC.ExecuteDataSet("[Repeater_Tbl_QCClass]", param);

            return ds;
        }

        protected void ClassBindRepeater()
        {

            DataSet ds = new DataSet();
            ds = Tbl_QCClass_GetData(lblOldTransID.Text, inputMatID.Text);

            rptClassType.DataSource = ds;
            rptClassType.DataBind();

        }
        protected void ClassBindRepeaterNew()
        {

            DataSet ds = new DataSet();
            ds = Tbl_QCClass_GetData(lblTransID.Text, inputMatID.Text);

            rptClassType.DataSource = ds;
            rptClassType.DataBind();

        }
        private DataSet Tbl_QCData_GetData(string TransID, string MaterialID)
        {
            SqlParameter[] param = new SqlParameter[2];

            DataSet ds = new DataSet();
            param[0] = new SqlParameter("@TransID", TransID);
            param[1] = new SqlParameter("@MaterialID", MaterialID);


            //send param to SP sql
            ds = sqlC.ExecuteDataSet("[Repeater_Tbl_QCData]", param);

            return ds;
        }
        protected void QCDataBindRepeater()
        {

            DataSet ds = new DataSet();
            ds = Tbl_QCData_GetData(lblOldTransID.Text, inputMatID.Text);

            rptInspectionType.DataSource = ds;
            rptInspectionType.DataBind();
            rptInspectionTypeDisplay.DataSource = ds;
            rptInspectionTypeDisplay.DataBind();

        }
        protected void QCDataBindRepeaterNew()
        {

            DataSet ds = new DataSet();
            ds = Tbl_QCData_GetData(lblTransID.Text, inputMatID.Text);

            rptInspectionType.DataSource = ds;
            rptInspectionType.DataBind();
            rptInspectionTypeDisplay.DataSource = ds;
            rptInspectionTypeDisplay.DataBind();

        }
        protected DataTable GetLastTransIDByTypeProc(string TypeProc)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TypeProc", TypeProc);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("Tbl_Material_GetLastTransID", param);

            return dt;
        }
        //auto generate TransID
        protected void autoGenTransID()
        {
            string TypeProc = "";
            string num1 = "";
            DataTable dt = new DataTable();
            dt = GetLastTransIDByTypeProc(inputTypeProc.Text.Trim());

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
                TypeProc = inputTypeProc.Text.Trim();
                num1 = string.Format(TypeProc + "{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
                lblTransID.Text = num1;
            }
        }


        //protected void autoGenTransID()
        //{
        //if (conMatWorkFlow.State == System.Data.ConnectionState.Closed)
        //{
        //    conMatWorkFlow.Open();
        //}
        //if (inputTypeProc.Text.Trim() == "RM")
        //{
        //    string num1 = lblTransID.Text;
        //    string qry1 = "select TOP 1 TransID from Tbl_Material WHERE TransID LIKE 'RM%' ORDER BY CreateTime DESC";
        //    SqlCommand cmd1 = new SqlCommand(qry1, conMatWorkFlow);
        //    SqlDataReader NumRdr1 = null;
        //    NumRdr1 = cmd1.ExecuteReader();
        //    while (NumRdr1.Read())
        //    {
        //        num1 = NumRdr1["TransID"].ToString();
        //    }
        //    num1 = string.Format("RM{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
        //    lblTransID.Text = num1;
        //    conMatWorkFlow.Close();
        //}
        //else if (inputTypeProc.Text.Trim() == "SF")
        //{
        //    lblTransID.Text = "SF00000000";
        //    string num1 = lblTransID.Text;
        //    string qry1 = "select TOP 1 TransID from Tbl_Material WHERE TransID LIKE 'SF%' ORDER BY CreateTime DESC";
        //    SqlCommand cmd1 = new SqlCommand(qry1, conMatWorkFlow);
        //    SqlDataReader NumRdr1 = null;
        //    NumRdr1 = cmd1.ExecuteReader();
        //    while (NumRdr1.Read())
        //    {
        //        num1 = NumRdr1["TransID"].ToString();
        //    }
        //    num1 = string.Format("SF{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
        //    lblTransID.Text = num1;
        //    conMatWorkFlow.Close();
        //}
        //else if (inputTypeProc.Text.Trim() == "FG")
        //{
        //    lblTransID.Text = "FG00000000";
        //    string num1 = lblTransID.Text;
        //    string qry1 = "select TOP 1 TransID from Tbl_Material WHERE TransID LIKE 'FG%' ORDER BY CreateTime DESC";
        //    SqlCommand cmd1 = new SqlCommand(qry1, conMatWorkFlow);
        //    SqlDataReader NumRdr1 = null;
        //    NumRdr1 = cmd1.ExecuteReader();
        //    while (NumRdr1.Read())
        //    {
        //        num1 = NumRdr1["TransID"].ToString();
        //    }
        //    num1 = string.Format("FG{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
        //    lblTransID.Text = num1;
        //    conMatWorkFlow.Close();
        //}                               
        //}

        //protected void autoGenLogID()
        //{
        //    if (conMatWorkFlow.State == System.Data.ConnectionState.Closed)
        //    {
        //        conMatWorkFlow.Open();
        //    }
        //    string num1 = lblLogID.Text;
        //    string qry1 = "select LogID from Tbl_LogTrans";
        //    SqlCommand cmd1 = new SqlCommand(qry1, conMatWorkFlow);
        //    SqlDataReader NumRdr1 = null;
        //    NumRdr1 = cmd1.ExecuteReader();
        //    while (NumRdr1.Read())
        //    {
        //        num1 = NumRdr1["LogID"].ToString();
        //    }
        //    num1 = string.Format("LD{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D8"));
        //    lblLogID.Text = num1;
        //    conMatWorkFlow.Close();
        //}

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
                rndTBL.Visible = true;

                procTBL.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
                //ficoTBL.Visible = false;
                if (inputTypeProc.Text == "RM")
                {
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                    //foreignTradeDt.Visible = false;
                    WorkingSchedule.Visible = true;
                }
                else
                {
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                    bscDt1Dimension.Visible = true;
                    //MRP1LOTSizeDt.Visible = true;
                    //foreignTradeDt.Visible = true;
                    plantShelfLifeDt.Visible = true;
                }
            }
            else if (e.Item.Value == "2")
            {
                lblMenu.Text = "Procurement";
                procTBL.Visible = true;

                rndTBL.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
                //ficoTBL.Visible = false;
                if (inputTypeProc.Text == "RM")
                {
                    bscDt1GnrlDtProc.Visible = true;
                    BscDtDimension.Visible = true;
                    purchValNOrder.Visible = true;
                    MRPLotSize.Visible = true;
                    //ForeignTradeData.Visible = true;
                    //PlantShelfLifeDtProc.Visible = true;
                    SalesData.Visible = true;

                }
                else
                {
                    bscDt1GnrlDtProc.Visible = true;
                    purchValNOrder.Visible = true;
                    SalesData.Visible = true;

                }
            }
            else if (e.Item.Value == "3")
            {
                lblMenu.Text = "Planner";
                plannerTbl.Visible = true;

                procTBL.Visible = false;
                rndTBL.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;

                bscDt1GnrlDtPlanner.Visible = true;
                bscDt2OtrDt.Visible = true;
                MRP1.Visible = true;
                MRP2.Visible = true;
                MRP3.Visible = true;
                //ficoTBL.Visible = false;
            }
            else if (e.Item.Value == "4")
            {
                lblMenu.Text = "QC";
                QCTbl.Visible = true;

                procTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
                //ficoTBL.Visible = false;
            }
            else if (e.Item.Value == "5")
            {
                lblMenu.Text = "QA";
                QATbl.Visible = true;

                QCTbl.Visible = false;
                procTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
                QRTbl.Visible = false;
                //ficoTBL.Visible = false;
            }
            else if (e.Item.Value == "6")
            {
                lblMenu.Text = "QR";
                QRTbl.Visible = true;

                QATbl.Visible = false;
                QCTbl.Visible = false;
                procTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
                //   ficoTBL.Visible = false;
            }
            else if (e.Item.Value == "7")
            {
                lblMenu.Text = "FICO";
                // ficoTBL.Visible = true;

                QATbl.Visible = false;
                QRTbl.Visible = false;
                QCTbl.Visible = false;
                procTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
            }
        }

        //Binding List View Code Public
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
            //Force Postback Script
            //StringBuilder sbScript = new StringBuilder();
            //sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
            //sbScript.Append("<!--\n");
            //sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
            //sbScript.Append("// -->\n");
            //sbScript.Append("</script>\n");
            //this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());

            srcListViewBinding();

        }


        protected void srcListViewBinding()
        {
            //if (conMatWorkFlow.State == ConnectionState.Closed)
            //{
            //    conMatWorkFlow.Open();
            //}

            //if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
            //{
            //    SqlCommand cmd = new SqlCommand("srcListViewExtendMat", conMatWorkFlow);
            //    cmd.Parameters.AddWithValue("inputMatIDDESC", "%" + this.inputMatIDDESC.Text + "%");
            //    cmd.Parameters.AddWithValue("TypeUser", "%" + lblPosition.Text + "%");
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    var dataAdapter = new SqlDataAdapter();
            //    dataAdapter.SelectCommand = cmd;

            //    var commandBuilder = new SqlCommandBuilder(dataAdapter);
            //    var ds = new DataSet();
            //    dataAdapter.Fill(ds);
            //    //= ds.Tables[0];
            //    GridViewListView.DataSource = ds;
            //    GridViewListView.DataBind();
            //    conMatWorkFlow.Close();
            //}
            //else if (lsbxMatIDDESC.SelectedItem.Text == "Material Description")
            //{
            //    SqlCommand cmd = new SqlCommand("srcListViewExtendMat", conMatWorkFlow);
            //    cmd.Parameters.AddWithValue("inputMatIDDESC", "%" + this.inputMatIDDESC.Text + "%");
            //    cmd.Parameters.AddWithValue("TypeUser", "%" + lblPosition.Text + "%");
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    var dataAdapter = new SqlDataAdapter();
            //    dataAdapter.SelectCommand = cmd;

            //    var commandBuilder = new SqlCommandBuilder(dataAdapter);
            //    var ds = new DataSet();
            //    dataAdapter.Fill(ds);

            //    GridViewListView.DataSource = ds.Tables[0];
            //    GridViewListView.DataBind();
            //    conMatWorkFlow.Close();
            //}
            //ExtendPlant_GetData();
            FilterSearch.Text = "ALL";
            gridSearch(FilterSearch.Text);
        }


        private void ExtendPlant_GetData()
        {
            SqlParameter[] param = new SqlParameter[2];

            DataTable dt = new DataTable();
            //if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
            //{

            //}

            param[0] = new SqlParameter("@inputMatIDDESC", "%" + this.inputMatIDDESC.Text + "%");
            param[1] = new SqlParameter("@TypeUser", lblPosition.Text);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewExtendMat", param);

            GridViewListView.DataSource = dt;
            GridViewListView.DataBind();
        }
        //Specific
        protected void slcExtendMaterial_Click(object sender, EventArgs e)
        {
            this.MenuRnD.BackColor = Color.DeepSkyBlue;

            spanTransID.Style.Add("display", "flex");
            //Master.FindControl("NavigationMenu").Visible = false;
            //Master.FindControl("btnLogOut").Visible = false;



            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            lblExtend.Text = grdrow.Cells[20].Text.ToString();
            //MainContent
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            //ambil data dr Tbl_Material_Select

            DataTable dt_TblMaterial;
            dt_TblMaterial = GetData_Tbl_Material(grdrow.Cells[0].Text.Trim(), grdrow.Cells[1].Text.Trim());

            if (dt_TblMaterial.Rows.Count > 0)
            {
                if (lblPosition.Text == "R&D")
                {
                    if (grdrow.Cells[20].Text.ToString() == null || grdrow.Cells[20].Text.ToString() == "&nbsp;")
                    {
                        SetData_From_Tbl_Material(dt_TblMaterial, "R&D");
                        CloseOtherComponent("R&D");

                        SetProperty("R&D");

                        try
                        {
                            conMatWorkFlow.Close();

                            tmpBindRepeater();
                            ClassBindRepeater();
                            QCDataBindRepeater();
                            srcMaterialIDModalBinding();

                            //R&D Lbl
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
                            bindLblCommImpProc();
                            bindLblLabOffice();
                            bindLblMRPGroup();
                            bindLblMRPType();
                            bindLblMRPController();
                            bindLblLotSize();
                            bindLblQA();
                            bindLblIndStdDesc();
                            bindLblStrategyGroup();
                            //inputIndStdDesc_TextChanged(this, e);
                            //inputSchedMargKey_TextChanged(this, e);
                            //inputStrtgyGr_TextChanged(this, e);
                            bindLblStorCondition();
                            //inputStoreCond_TextChanged(this, e);
                            //inputProdSched_TextChanged(this, e);
                            //inputProdSchedProfile_TextChanged(this, e);
                            //inputLoadingGrp_TextChanged(this, e);
                            //inputPurcGrp_TextChanged(this, e);
                            autoGenTransID();
                            autoGenLogID();

                            //if (reptUntMeas.Items.Count >= 1)
                            //{
                            //    otrInputTbl.Visible = false;
                            //    reptUntMeas.Visible = false;
                            //    reptUntMeasMgr.Visible = true;
                            //}
                            //if (rptInspectionType.Items.Count >= 1)
                            //{
                            //    tblInspectType.Visible = false;
                            //    rptInspectionType.Visible = false;
                            //    rptInspectionTypeDisplay.Visible = true;
                            //}

                        }
                        catch (Exception ex)
                        {
                            MsgBox(ex.ToString(), this.Page, this);
                        }
                    }
                    else
                    {
                        SetData_From_Tbl_Material(dt_TblMaterial, "R&D MGR");
                        CloseOtherComponent("R&D");

                        SetProperty("R&D");

                        try
                        {
                            conMatWorkFlow.Close();

                            tmpBindRepeaterNew();
                            ClassBindRepeaterNew();
                            QCDataBindRepeaterNew();
                            srcMaterialIDModalBinding();

                            //R&D Lbl
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
                            bindLblCommImpProc();
                            bindLblLabOffice();
                            bindLblMRPGroup();
                            bindLblMRPType();
                            bindLblMRPController();
                            bindLblLotSize();
                            bindLblQA();
                            bindLblIndStdDesc();
                            bindLblStrategyGroup();
                            //inputIndStdDesc_TextChanged(this, e);
                            //inputSchedMargKey_TextChanged(this, e);
                            //inputStrtgyGr_TextChanged(this, e);
                            bindLblStorCondition();
                            //inputStoreCond_TextChanged(this, e);
                            //inputProdSched_TextChanged(this, e);
                            //inputProdSchedProfile_TextChanged(this, e);
                            //inputLoadingGrp_TextChanged(this, e);
                            //inputPurcGrp_TextChanged(this, e);
                            //autoGenTransID();
                            autoGenLogID();

                        }
                        catch (Exception ex)
                        {
                            MsgBox(ex.ToString(), this.Page, this);
                        }
                    }
                }
                else if (lblPosition.Text == "R&D MGR")
                {
                    // kalo R&D doang, dia save aja, buat transaksi baru
                    CloseOtherComponent("R&D MGR");

                    // semua nya display only , button d bawah nya hanya approval.

                    //show data
                    SetData_From_Tbl_Material(dt_TblMaterial, "R&D MGR");

                    //////////////////////
                    conMatWorkFlow.Close();
                    // tmpBindRepeater();
                    //  ClassBindRepeater();
                    // QCDataBindRepeater();

                    tmpBindRepeaterNew();
                    ClassBindRepeaterNew();
                    QCDataBindRepeaterNew();
                    srcMaterialIDModalBinding();
                    //R&D Lbl
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
                    bindLblCommImpProc();
                    bindLblLabOffice();
                    bindLblMRPGroup();
                    bindLblMRPType();
                    bindLblMRPController();
                    bindLblLotSize();
                    bindLblQA();
                    bindLblIndStdDesc();
                    bindLblStorCondition();
                    bindLblStrategyGroup();
                    //inputIndStdDesc_TextChanged(this, e);
                    //inputSchedMargKey_TextChanged(this, e);
                    //inputStrtgyGr_TextChanged(this, e);
                    //inputStoreCond_TextChanged(this, e);

                    //inputProdSched_TextChanged(this, e);
                    //inputProdSchedProfile_TextChanged(this, e);
                    //inputLoadingGrp_TextChanged(this, e);
                    //inputPurcGrp_TextChanged(this, e);
                    //inputProdStorLoc_TextChanged(this, e);
                    //inputSchedMargKey_TextChanged(this, e);
                    // autoGenTransID();
                    autoGenLogID();
                    ////////////////////////

                    //set property to display only
                    SetProperty("R&D MGR");


                }
                else
                {
                    MsgBox(this.lblUser.Text.Trim() + " username are not for FICO division", this.Page, this);
                    Response.Redirect("~/Pages/extendPlantPage");
                }
            }


            //System.Web.UI.HtmlControls.HtmlGenericControl currdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("mainHeader");
            //currdiv.Style.Add("display", "none");
            // currdiv.Visible = false;
        }

        protected void CloseOtherComponent(string TypeRD)
        {

            if (TypeRD == "R&D")
            {
                listViewExtend.Visible = false;
                btnSave.Visible = true;
                btnCancelSave.Visible = true;

                rmContent.Visible = true;
                // divApprMn.Visible = true;
                btnApprove.Visible = false;
                btnCancelApprove.Visible = false;

                if (inputTypeProc.Text.Trim() == "RM") { }
                else
                {
                    foreignTradeDt.Visible = true;
                }
            }
            else
            {
                listViewExtend.Visible = false;
                btnSave.Visible = false;
                btnCancelSave.Visible = false;
                otrInputTbl.Visible = false;
                reptUntMeas.Visible = false;
                rptInspectionType.Visible = false;
                tblInspectType.Visible = false;

                rmContent.Visible = true;
                //divApprMn.Visible = true;
                btnApprove.Visible = true;
                btnCancelApprove.Visible = true;
                reptUntMeasMgr.Visible = true;
                rptInspectionTypeDisplay.Visible = true;

            }

        }
        protected DataTable GetData_Tbl_Material(string TransID, string MaterialID)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TransID", TransID);
            param[1] = new SqlParameter("@MaterialID", MaterialID);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("Tbl_Material_Select", param);

            return dt;
        }

        protected void SetProperty(string RDType)
        {
            if (RDType == "R&D")
            {
                inputMatID.CssClass = "txtBoxRO";
                //Planner
                inputMatDesc.CssClass = "txtBoxRO";
                inputUoM.CssClass = "txtBoxRO";
                inputMatGr.CssClass = "txtBoxRO";
                inputOldMatNum.CssClass = "txtBoxRO";
                inputDivision.CssClass = "txtBoxRO";
                inputPckgMat.CssClass = "txtBoxRO";
                inputMatType.CssClass = "txtBoxRO";

                inputNetWeight.CssClass = "txtBoxRO";
                inputCommImpCode.CssClass = "txtBoxRO";

                // inputMinRemShLfProc.CssClass = "txtBoxEdit";
                //inputTotalShelfLifeProc.CssClass = "txtBoxEdit";
                inputMinRemShLf.CssClass = "txtBoxEdit";
                inputTotalShelfLife.CssClass = "txtBoxEdit";
                //   inputMinLotSize.CssClass = "txtBoxEdit";
                //    inputRoundValue.CssClass = "txtBoxEdit";
                inputProcType.CssClass = "txtBoxEdit";
                //inputMinRemShLfProc.ReadOnly = false;
                //inputTotalShelfLifeProc.ReadOnly = false;
                inputMinRemShLf.ReadOnly = true;
                inputTotalShelfLife.ReadOnly = true;

                inputProcType.ReadOnly = true;


                inputMatDesc.ReadOnly = true;
                inputUoM.ReadOnly = true;
                inputMatGr.ReadOnly = true;
                inputOldMatNum.ReadOnly = true;
                inputDivision.ReadOnly = true;
                inputPckgMat.ReadOnly = true;
                inputMatType.ReadOnly = true;

                inputNetWeight.ReadOnly = true;
                inputCommImpCode.ReadOnly = true;
                inputMinRemShLf.ReadOnly = false;
                inputTotalShelfLife.ReadOnly = false;

                inputPurcGrp.ReadOnly = false;
                inputPurcValKey.ReadOnly = false;
                inputGRProcTimeMRP1.ReadOnly = false;
                inputMfrPrtNum.ReadOnly = false;


                inputPurcGrp.CssClass = "txtBoxEdit";
                inputPurcValKey.CssClass = "txtBoxEdit";
                inputGRProcTimeMRP1.CssClass = "txtBoxEdit";
                inputMfrPrtNum.CssClass = "txtBoxEdit";

                inputIndStdDesc.CssClass = "txtBoxEdit";
                inputIndStdDesc.ReadOnly = false;

                inputStoreCond.CssClass = "txtBoxEdit";
                inputStoreCond.ReadOnly = false;
                if (inputTypeProc.Text.Trim() == "RM")
                {
                    //RND
                    inputPlant.CssClass = "txtBoxEdit";
                    inputPlant.ReadOnly = false;

                    inputStorLoc.CssClass = "txtBoxEdit";
                    inputStorLoc.ReadOnly = false;

                    inputSalesOrg.CssClass = "txtBoxEdit";
                    inputSalesOrg.ReadOnly = false;

                    //inputProcType.CssClass = "txtBoxRO";
                    //inputProcType.ReadOnly = true;
                    inputProcType.CssClass = "txtBoxEdit";
                    inputProcType.ReadOnly = false;
                    //inputMinLotSize.CssClass = "txtBoxRO";
                    // inputMinLotSize.ReadOnly = true;

                    //inputRoundValue.CssClass = "txtBoxRO";
                    //inputRoundValue.ReadOnly = true;

                    //Planner
                    //inputMRPTyp.CssClass = "txtBoxEdit";
                    inputMRPTyp.ReadOnly = false;

                    inputMRPCtrl.ReadOnly = false;
                    inputMRPGr.ReadOnly = false;
                    inputSchedMargKey.ReadOnly = false;
                    inputLOTSize.ReadOnly = false;
                    inputMaxStockLv.ReadOnly = false;
                    inputProdStorLoc.ReadOnly = false;
                    inputSftyStck.ReadOnly = false;
                    inputMinSftyStck.ReadOnly = false;
                    //inputStrtgyGr.ReadOnly = false;
                    //inputProcTypePlanner.ReadOnly = false;
                    //input Fix size
                    // inputSpcProc.ReadOnly = false;

                    //Procurement
                    inputPurcGrp.CssClass = "txtBoxEdit";
                    inputPurcGrp.ReadOnly = false;

                    inputCommImpCode.CssClass = "txtBoxEdit";
                    inputCommImpCode.ReadOnly = false;
                    inputGRProcTimeMRP1.ReadOnly = false;


                    inputCommImpCodeProc.CssClass = "txtBoxEdit";
                    inputCommImpCodeProc.ReadOnly = false;


                    inputMinLotSizeProc.ReadOnly = false;
                    inputRoundValueProc.ReadOnly = false;
                    inputLoadingGrp.ReadOnly = false;

                }
                else
                {
                    //SF / FG
                    //RND
                    inputPlant.CssClass = "txtBoxEdit";
                    inputPlant.ReadOnly = false;

                    inputStorLoc.CssClass = "txtBoxEdit";
                    inputStorLoc.ReadOnly = false;

                    inputSalesOrg.CssClass = "txtBoxEdit";
                    inputSalesOrg.ReadOnly = false;

                    inputCommImpCode.CssClass = "txtBoxEdit";
                    inputCommImpCode.ReadOnly = false;

                    //inputMinLotSize.CssClass = "txtBoxEdit";
                    //inputMinLotSize.ReadOnly = false;
                    //inputRoundValue.CssClass = "txtBoxEdit";
                    //inputRoundValue.ReadOnly = false;
                    inputProcType.CssClass = "txtBoxEdit";
                    inputProcType.ReadOnly = false;

                    inputSpcProcRnd.CssClass = "txtBoxEdit";
                    inputSpcProcRnd.ReadOnly = false;

                    //Planner
                    inputMRPTyp.ReadOnly = false;

                    inputMRPCtrl.ReadOnly = false;
                    inputMRPGr.ReadOnly = false;
                    inputSchedMargKey.ReadOnly = false;
                    inputLOTSize.ReadOnly = false;
                    inputMaxStockLv.ReadOnly = false;
                    inputProdStorLoc.ReadOnly = false;
                    inputSftyStck.ReadOnly = false;
                    inputMinSftyStck.ReadOnly = false;
                    //inputStrtgyGr.ReadOnly = false;
                    //inputProcTypePlanner.ReadOnly = false;
                    //input Fix size
                    //inputSpcProc.ReadOnly = false;

                    //Procurement
                    //inputCommImpCode.CssClass = "txtBoxEdit";
                    //inputCommImpCode.ReadOnly = false;

                    inputGRProcTimeMRP1.ReadOnly = false;

                    inputLoadingGrp.ReadOnly = false;
                    trPlantDeliv.Visible = false;

                }



            }
            //Display only , set all readonly true
            else if (RDType == "R&D MGR")
            {
                LBSearchCommImpCode.Visible = false;
                LBSearchLoadingGroup.Visible = false;
                LBSearchLOTSize.Visible = false;
                LBSearchMatID.Visible = false;
                LBSearchMRPCtrl.Visible = false;
                LBSearchMRPGroup.Visible = false;
                LBSearchMRPType.Visible = false;
                LBSearchPlant.Visible = false;
                LBSearchProc.Visible = false;
                LBSearchDistrChl.Visible = false;
                //LBSearchProcTypePlanner.Visible = false;
                LBSearchProdSchedProfile.Visible = false;
                LBSearchProdScheduler.Visible = false;
                LBSearchProdStorLoc.Visible = false;
                LBSearchPurchasingGroup.Visible = false;
                LBSearchSchedMarginKey.Visible = false;
                LBSearchSLoc.Visible = false;
                LBSearchSOrg.Visible = false;
                LBSearchSpcProc.Visible = false;
                LBSearchStrategyGroup.Visible = false;
                LBSearchPurchValKey.Visible = false;
                LBSearchStoreCond.Visible = false;
                LBSearchIndStdDesc.Visible = false;
                LBSearchCommImpCode.Visible = false;
                LBSearchCommImpCode2.Visible = false;
                LBSearchLabOffice.Visible = false;
                //RND
                inputMatID.CssClass = "txtBoxRO";
                inputMatDesc.CssClass = "txtBoxRO";
                inputUoM.CssClass = "txtBoxRO";
                inputMatGr.CssClass = "txtBoxRO";
                inputOldMatNum.CssClass = "txtBoxRO";
                inputDivision.CssClass = "txtBoxRO";
                inputPckgMat.CssClass = "txtBoxRO";
                inputMatType.CssClass = "txtBoxRO";
                inputPlant.CssClass = "txtBoxRO";
                inputDistrChl.CssClass = "txtBoxRO";
                inputStorLoc.CssClass = "txtBoxRO";
                inputSalesOrg.CssClass = "txtBoxRO";
                inputProcType.CssClass = "txtBoxRO";
                inputNetWeight.CssClass = "txtBoxRO";
                inputCommImpCode.CssClass = "txtBoxRO";
                inputMinRemShLf.CssClass = "txtBoxRO";
                inputTotalShelfLife.CssClass = "txtBoxRO";
                //inputMinLotSize.CssClass = "txtBoxRO";
                //inputRoundValue.CssClass = "txtBoxRO";
                inputSpcProcRnd.CssClass = "txtBoxRO";

                inputMRPTyp.CssClass = "txtBoxRO";
                inputMRPCtrl.CssClass = "txtBoxRO";
                inputLOTSize.CssClass = "txtBoxRO";

                inputPlant.CssClass = "txtBoxRO";
                inputStorLoc.CssClass = "txtBoxRO";
                inputPurcValKey.CssClass = "txtBoxRO";
                inputGRProcTimeMRP1.CssClass = "txtBoxRO";
                inputLabOffice.CssClass = "txtBoxRO";

                //inputSpcProc.CssClass = "txtBoxRO";
                inputProdStorLoc.CssClass = "txtBoxRO";
                inputSchedMargKey.CssClass = "txtBoxRO";
                inputSftyStck.CssClass = "txtBoxRO";
                inputMinSftyStck.CssClass = "txtBoxRO";
                inputStrtgyGr.CssClass = "txtBoxRO";
                inputMRPGr.CssClass = "txtBoxRO";
                inputMaxStockLv.CssClass = "txtBoxRO";
                inputPurcGrp.CssClass = "txtBoxRO";
                inputLoadingGrp.CssClass = "txtBoxRO";
                //  inputProcTypePlanner.CssClass = "txtBoxRO";
                inputProdSched.CssClass = "txtBoxRO";
                inputProdSchedProfile.CssClass = "txtBoxRO";
                inputLOTSize.CssClass = "txtBoxRO";
                inputFixLOTSize.CssClass = "txtBoxRO";
                inputInspectIntrv.CssClass = "txtBoxRO";



                chkbxCoProd.Enabled = false;
                chkbxInspectSet.Enabled = false;
                inputMRPCtrl.ReadOnly = true;
                inputLOTSize.ReadOnly = true;
                inputTypeQA.ReadOnly = true;
                inputFixLOTSize.ReadOnly = true;
                inputProdSched.ReadOnly = true;
                inputProdSchedProfile.ReadOnly = true;
                //  inputProcTypePlanner.ReadOnly = true;
                inputPurcGrp.ReadOnly = true;
                inputLoadingGrp.ReadOnly = true;
                inputPlant.ReadOnly = true;
                inputStorLoc.ReadOnly = true;

                inputMaxStockLv.ReadOnly = true;
                inputSftyStck.ReadOnly = true;
                inputMinSftyStck.ReadOnly = true;
                inputStrtgyGr.ReadOnly = true;
                inputSpcProcRnd.ReadOnly = true;
                //inputSpcProc.ReadOnly = true;
                inputGRProcTimeMRP1.ReadOnly = true;
                inputPurcValKey.ReadOnly = true;
                inputMinLotSizeProc.ReadOnly = true;
                inputRoundValueProc.ReadOnly = true;
                inputLoadingGrp.ReadOnly = true;

                inputMRPGr.ReadOnly = true;
                inputProdStorLoc.ReadOnly = true;
                inputSchedMargKey.ReadOnly = true;
                inputStoreCond.ReadOnly = true;

                inputFixLOTSize.CssClass = "txtBoxRO";
                inputFixLOTSize.ReadOnly = true;

                inputMinLotSizeProc.CssClass = "txtBoxRO";
                inputRoundValueProc.CssClass = "txtBoxRO";
                if (inputTypeProc.Text.Trim() == "RM")
                {
                    trPlantDeliv.Visible = true;
                    inputPlantDeliveryTime.ReadOnly = true;
                    inputPlantDeliveryTime.CssClass = "txtBoxRO";
                }
                else
                {
                    trPlantDeliv.Visible = false;
                }
            }

            //For All Type

        }
        protected void SetData_From_Tbl_Material(DataTable dt, string TypeRD)
        {
            //Set property for all Type
            foreach (DataRow row in dt.Rows)
            {
                //RND
                inputMatID.Text = row["MaterialID"].ToString().Trim();
                inputMatDesc.Text = row["MaterialDesc"].ToString().Trim();
                inputUoM.Text = row["UoM"].ToString().Trim();
                inputBun.Text = row["UoM"].ToString().Trim();
                inputWeightUnt.Text = row["UoM"].ToString().Trim();
                inputMatGr.Text = row["MatlGroup"].ToString().Trim();
                inputOldMatNum.Text = row["OldMatNumb"].ToString().Trim();
                inputDivision.Text = row["Division"].ToString().Trim();
                inputPckgMat.Text = row["MatlGrpPack"].ToString().Trim();
                inputMatType.Text = row["MatType"].ToString().Trim();

                inputMinRemShLf.Text = row["MinShelfLife"].ToString().Trim();
                inputTotalShelfLife.Text = row["TotalShelfLife"].ToString().Trim();
                //inputMinRemShLfProc.Text = row["MinShelfLife"].ToString().Trim();
                //inputTotalShelfLifeProc.Text = row["TotalShelfLife"].ToString().Trim();
                //inputMinLotSize.Text = row["MinLotSize"].ToString().Trim();
                // inputRoundValue.Text = row["RoundingValue"].ToString().Trim();


                inputDistrChl.Text = row["DistrChl"].ToString().Trim();
                inputProcType.Text = row["ProcType"].ToString().Trim();
                inputNetWeight.Text = row["NetWeight"].ToString().Trim();
                inputCommImpCode.Text = row["ForeignTrade"].ToString().Trim();
                inputMinRemShLf.Text = row["MinShelfLife"].ToString().Trim();
                inputTotalShelfLife.Text = row["TotalShelfLife"].ToString().Trim();
                //inputMinLotSize.Text = row["MinLotSize"].ToString().Trim();
                // inputRoundValue.Text = row["RoundingValue"].ToString().Trim();
                //PROC
                //Purchasing - Purchasing Value & Order Data
                inputPurcGrp.Text = row["PurchGrp"].ToString().Trim();

                inputPurcValKey.Text = row["PurchValueKey"].ToString().Trim();
                inputGRProcTimeMRP1.Text = row["GRProcessingTime"].ToString().Trim();
                if (inputGRProcTimeMRP1.Text == "")
                {
                    inputGRProcTimeMRP1.Text = "1";
                }
                inputMfrPrtNum.Text = row["MfrPartNumb"].ToString().Trim();

                inputTypeProc.Text = row["Type"].ToString().Trim();
                inputMatTypeProc.Text = row["MatType"].ToString().Trim();
                inputMatIDProc.Text = row["MaterialID"].ToString().Trim();
                inputMatDescProc.Text = row["MaterialDesc"].ToString().Trim();
                inputUoMProc.Text = row["UoM"].ToString().Trim();
                inputOldMatNumProc.Text = row["OldMatNumb"].ToString().Trim();
                inputProcPlant.Text = row["Plant"].ToString().Trim();
                inputNetWeightProc.Text = row["NetWeight"].ToString().Trim();
                inputCommImpCodeProc.Text = row["ForeignTrade"].ToString().Trim();
                //inputMinRemShLfProc.Text = row["MinShelfLife"].ToString().Trim();
                //inputTotalShelfLifeProc.Text = row["TotalShelfLife"].ToString().Trim();
                inputMinLotSizeProc.Text = row["MinLotSize"].ToString().Trim();
                inputRoundValueProc.Text = row["RoundingValue"].ToString().Trim();

                inputMfrPrtNum.Text = row["MfrPartNumb"].ToString().Trim();
                inputLoadingGrp.Text = row["LoadGrp"].ToString().Trim();
                //  lblLoadingGrp.Text = row["LoadGrpDesc"].ToString().Trim();
                inputPlantDeliveryTime.Text = row["PlantDeliveryTime"].ToString().Trim();
                inputIndStdDesc.Text = row["IndStdCode"].ToString().Trim();
                //PLANNER
                inputTypePlanner.Text = row["Type"].ToString().Trim();
                inputMatTypePlanner.Text = row["MatType"].ToString().Trim();
                inputMatIDPlanner.Text = row["MaterialID"].ToString().Trim();
                inputMaterialDescPlanner.Text = row["MaterialDesc"].ToString().Trim();
                inputUoMPlanner.Text = row["UoM"].ToString().Trim();
                inputOldMatNumbPlanner.Text = row["OldMatNumb"].ToString().Trim();
                inputPlannerPlant.Text = row["Plant"].ToString().Trim();
                inputLabOffice.Text = row["LabOffice"].ToString().Trim();
                inputIndStdDesc.Text = row["IndStdCode"].ToString().Trim();
                inputMRPTyp.Text = row["MRPType"].ToString().Trim();
                inputLOTSize.Text = row["LotSize"].ToString().Trim();
                inputFixLOTSize.Text = row["FixLotSize"].ToString().Trim();
                inputMaxStockLv.Text = row["MaxStockLvl"].ToString().Trim();
                inputIndStdDesc.Text = row["IndStdCode"].ToString().Trim();
                inputSftyStck.Text = row["SafetyStock"].ToString().Trim();
                inputMinSftyStck.Text = row["MinSafetyStock"].ToString().Trim();
                inputStrtgyGr.Text = row["PlanStrategyGroup"].ToString().Trim();
                if (inputStrtgyGr.Text == "")
                {
                    inputStrtgyGr.Text = "40";
                }
                //QC
                string stringInspectSet = "0";
                inputTypeQC.Text = row["Type"].ToString().Trim();
                inputMatTypeQC.Text = row["MatType"].ToString().Trim();
                inputMatIDQC.Text = row["MaterialID"].ToString().Trim();
                inputMaterialDescQC.Text = row["MaterialDesc"].ToString().Trim();
                inputUoMQC.Text = row["UoM"].ToString().Trim();
                inputOldMatNumbQC.Text = row["OldMatNumb"].ToString().Trim();
                inputQCPlant.Text = row["Plant"].ToString().Trim();
                stringInspectSet = row["InspectionSetup"].ToString().Trim();
                inputInspectIntrv.Text = row["InspectionInterval"].ToString().Trim();
                if (stringInspectSet == "X")
                {
                    chkbxInspectSet.Checked = true;
                }
                else
                {
                    chkbxInspectSet.Checked = false;
                }
                if (inputLOTSize.Text == "FX")
                {
                    RowFixLOTSize.Visible = true;

                }
                //QA
                inputTypeQA.Text = row["Type"].ToString().Trim();
                inputMatTypeQA.Text = row["MatType"].ToString().Trim();
                inputMatIDQA.Text = row["MaterialID"].ToString().Trim();
                inputMaterialDescQA.Text = row["MaterialDesc"].ToString().Trim();
                inputUoMQA.Text = row["UoM"].ToString().Trim();
                inputOldMatNumbQA.Text = row["OldMatNumb"].ToString().Trim();
                inputQAPlant.Text = row["Plant"].ToString().Trim();
                inputStoreCond.Text = row["StorConditions"].ToString().Trim();
                //QR
                inputTypeQR.Text = row["Type"].ToString().Trim();
                inputMatTypeQR.Text = row["MatType"].ToString().Trim();
                inputMatIDQR.Text = row["MaterialID"].ToString().Trim();
                inputMaterialDescQR.Text = row["MaterialDesc"].ToString().Trim();
                inputUoMQR.Text = row["UoM"].ToString().Trim();
                inputOldMatNumbQR.Text = row["OldMatNumb"].ToString().Trim();
                inputQRPlant.Text = row["Plant"].ToString().Trim();
                inputQMCtrlKey.Text = row["QMControlKey"].ToString().Trim();
                lblChkbx.Text = row["QMProcActive"].ToString().Trim();


                //FICO
                //inputTypeFICO.Text = row["Type"].ToString().Trim();
                //inputMatTypFICO.Text = row["MatType"].ToString().Trim();
                //inputMtrlIDFICO.Text = row["MaterialID"].ToString().Trim();
                //inputMtrlDescFICO.Text = row["MaterialDesc"].ToString().Trim();
                //inputBsUntMeasFICO.Text = row["UoM"].ToString().Trim();
                //inputOldMtrlNumFICO.Text = row["OldMatNumb"].ToString().Trim();
                //inputFicoPlant.Text = row["Plant"].ToString().Trim();

                chkbxQMProcActive.Enabled = true;
                if (lblChkbx.Text.ToUpper() == "X")
                {
                    chkbxQMProcActive.Checked = true;
                    inputQMCtrlKey.Text = row["QMControlKey"].ToString().Trim();
                    lblQMProcActive.Text = "Active";
                }
                else
                {
                    chkbxQMProcActive.Checked = false;
                    lblQMProcActive.Text = "Non Active";
                }

                if (row["COProd"].ToString().Trim() == "X")
                {
                    chkbxCoProd.Checked = true;
                }
                else chkbxCoProd.Checked = false;
            }

            if (TypeRD == "R&D")
            {
                if (inputTypeProc.Text.Trim() == "RM")
                {
                    lblOldTransID.Text = dt.Rows[0]["TransID"].ToString().Trim();
                    lblMatID.Text = dt.Rows[0]["MaterialID"].ToString().Trim();

                    //R&D
                    inputPlant.Text = string.Empty;
                    inputStorLoc.Text = string.Empty;
                    inputSalesOrg.Text = string.Empty;
                    //inputProcType.Text = string.Empty;
                    //inputMinLotSize.Text = string.Empty;
                    //Planner - MRP2

                    //inputSpcProc.Text = string.Empty;
                    inputSchedMargKey.Text = string.Empty;


                    //Planner - MRP2


                    inputMRPGr.Text = string.Empty;

                    inputMRPCtrl.Text = string.Empty;



                    //inputPurcValKey.Text = string.Empty;


                    //Proc

                    inputLoadingGrp.Text = string.Empty;


                    lblChkbx.Text = dt.Rows[0]["QMProcActive"].ToString().Trim();


                    chkbxCoProd.Visible = false;
                    lblCoProd.Visible = false;



                }
                else
                {
                    lblOldTransID.Text = dt.Rows[0]["TransID"].ToString().Trim();
                    lblMatID.Text = dt.Rows[0]["MaterialID"].ToString().Trim();

                    //R&D
                    inputPlant.Text = string.Empty;
                    inputStorLoc.Text = string.Empty;
                    inputSalesOrg.Text = string.Empty;



                    //Planner - MRP2

                    //inputSpcProc.Text = string.Empty;
                    inputSchedMargKey.Text = string.Empty;

                    //Planner - MRP2

                    inputMRPGr.Text = string.Empty;
                    inputMRPCtrl.Text = string.Empty;

                    inputLoadingGrp.Text = string.Empty;
                    //inputRoundValue.Text = string.Empty;
                    lblChkbx.Text = dt.Rows[0]["QMProcActive"].ToString().Trim();
                    chkbxQMProcActive.Enabled = true;
                    if (lblChkbx.Text.ToUpper().Trim() == "X")
                    {
                        chkbxQMProcActive.Checked = true;
                        inputQMCtrlKey.Text = dt.Rows[0]["QMControlKey"].ToString().Trim();
                        lblQMProcActive.Text = "Active";
                    }
                    else
                    {
                        chkbxQMProcActive.Checked = false;
                        lblQMProcActive.Text = "Non Active";
                    }


                    chkbxCoProd.Visible = true;
                    lblCoProd.Visible = true;


                }
            }
            else if (TypeRD == "R&D MGR")
            {
                inputPlant.Text = dt.Rows[0]["Plant"].ToString().Trim();
                inputStorLoc.Text = dt.Rows[0]["Sloc"].ToString().Trim();
                inputSalesOrg.Text = dt.Rows[0]["SOrg"].ToString().Trim();


                inputProdStorLoc.Text = dt.Rows[0]["ProdSLoc"].ToString().Trim();
                //inputSpcProc.Text = dt.Rows[0]["SpclProcurement"].ToString().Trim();
                inputSchedMargKey.Text = dt.Rows[0]["SchedType"].ToString().Trim();
                inputSftyStck.Text = dt.Rows[0]["SafetyStock"].ToString().Trim();
                inputMinSftyStck.Text = dt.Rows[0]["MinSafetyStock"].ToString().Trim();
                inputStrtgyGr.Text = dt.Rows[0]["PlanStrategyGroup"].ToString().Trim();


                inputMRPGr.Text = dt.Rows[0]["MRPGroup"].ToString().Trim();
                inputMRPTyp.Text = dt.Rows[0]["MRPType"].ToString().Trim();
                inputMRPCtrl.Text = dt.Rows[0]["MRPController"].ToString().Trim();
                inputLOTSize.Text = dt.Rows[0]["LotSize"].ToString().Trim();
                inputMaxStockLv.Text = dt.Rows[0]["MaxStockLvl"].ToString().Trim();

                inputPurcValKey.Text = dt.Rows[0]["PurchValueKey"].ToString().Trim();
                inputGRProcTimeMRP1.Text = dt.Rows[0]["GRProcessingTime"].ToString().Trim();
                lblTransID.Text = dt.Rows[0]["TransID"].ToString().Trim();
                lblChkbx.Text = dt.Rows[0]["QMProcActive"].ToString().Trim();
                inputProdSched.Text = dt.Rows[0]["ProdSched"].ToString().Trim();
                inputProdSchedProfile.Text = dt.Rows[0]["ProdSchedProfile"].ToString().Trim();

                chkbxQMProcActive.Enabled = false;
                if (lblChkbx.Text.ToUpper().Trim() == "X")
                {
                    chkbxQMProcActive.Checked = true;
                    inputQMCtrlKey.Text = dt.Rows[0]["QMControlKey"].ToString().Trim();
                    lblQMProcActive.Text = "Active";
                }
                else
                {
                    chkbxQMProcActive.Checked = false;
                    lblQMProcActive.Text = "Non Active";
                }
            }



        }

        //LightBox Data Code
        protected void srcMaterialIDModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("bindMaterialID", conMatWorkFlow);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMatID.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewMaterialID.DataSource = ds.Tables[0];
            GridViewMaterialID.DataBind();
            conMatWorkFlow.Close();
        }

        //Modal
        protected void selectMaterialID_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputMatID.Text = grdrow.Cells[0].Text;
            inputMatID.Focus();
        }
        //inputSpcProc_onBlur
        protected void inputMaterialID_onBlur(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            //MaterialID Exist
            SqlCommand cmdMatIDExist = new SqlCommand("bindMaterialID", conMatWorkFlow);
            cmdMatIDExist.CommandType = CommandType.StoredProcedure;
            cmdMatIDExist.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMatID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdMatIDExist.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmdMatIDExist;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            da.Fill(ds);
            int i = ds.Tables[0].Rows.Count;
            if (i > 0)
            {
                da.Fill(dt);
                inputMatID.Text = dt.Rows[0]["MaterialID"].ToString().Trim();
                ds.Clear();
                dt.Clear();
                return;
            }
            else if (i == 0)
            {
                MsgBox("Youre MaterialID Doesn't Exist.", this.Page, this);
                ds.Clear();
                dt.Clear();
                return;
            }
            conMatWorkFlow.Close();
        }

        protected void bindLblStorCondition()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblStorCondition", conMatWorkFlow);
            cmd.Parameters.AddWithValue("StorConditions", this.inputStoreCond.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblStoreCond.Text = dr["StorConditionsDesc"].ToString();
                lblStoreCond.ForeColor = Color.Black;
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
        protected void bindLblProdSchedProfile()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblProdSchedProfile", conMatWorkFlow);
            cmd.Parameters.AddWithValue("ProdSchedProfile", this.inputProdSchedProfile.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblProdSchedProfile.Text = dr["ProdSchedProfileDesc"].ToString();
                lblProdSchedProfile.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblProdSched()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblProdSched", conMatWorkFlow);
            cmd.Parameters.AddWithValue("ProdSched", this.inputProdSched.Text);
            cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            cmd.Parameters.AddWithValue("MaterialID", this.inputMatID.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblProdSched.Text = dr["ProdSchedDesc"].ToString();
                lblProdSched.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblLoadingGroup()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblLoadingGroup", conMatWorkFlow);
            cmd.Parameters.AddWithValue("LoadGrp", this.inputLoadingGrp.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblLoadingGrp.Text = dr["LoadGrpDesc"].ToString();
                lblLoadingGrp.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblBsUntMeas()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblBsUntMeas", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputUoM.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblUoMRnD.Text = dr["UoM_Desc"].ToString();
                lblUoMProc.Text = dr["UoM_Desc"].ToString();
                lblUoMPlanner.Text = dr["UoM_Desc"].ToString();
                lblUoMQC.Text = dr["UoM_Desc"].ToString();
                lblUoMQA.Text = dr["UoM_Desc"].ToString();
                lblUoMQR.Text = dr["UoM_Desc"].ToString();
                lblUoMRnD.ForeColor = Color.Black;
                lblUoMProc.ForeColor = Color.Black;
                lblUoMPlanner.ForeColor = Color.Black;
                lblUoMQC.ForeColor = Color.Black;
                lblUoMQA.ForeColor = Color.Black;
                lblUoMQR.ForeColor = Color.Black;
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
            cmd.Parameters.AddWithValue("inputMatTyp", this.inputMatType.Text);
            cmd.Parameters.AddWithValue("Type", this.inputTypePlanner.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMatTyp.Text = dr["MatTypeDesc"].ToString();
                lblMatTyp.ForeColor = Color.Black;
                inputPlant.Focus();
                inputPlant.ReadOnly = false;

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
                inputStorLoc.ReadOnly = false;

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

                // lblProdStorLoc.Text = dr["SLoc_Desc"].ToString();
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
        protected void bindLblCommImpProc()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblCommImp", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputCommImpCode", this.inputCommImpCodeProc.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblCommImpProc.Text = dr["ForeignDesc"].ToString();
                lblCommImpProc.ForeColor = Color.Black;
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
        protected void bindLblProcurement()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindProcurement", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputUoM.Text);
            cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblUoMProc.Text = dr["UoM_Desc"].ToString();
                lblLoadingGrp.Text = dr["LoadGrpDesc"].ToString();
                lblUoMProc.ForeColor = Color.Black;
                lblCommImpProc.ForeColor = Color.Black;
                lblLoadingGrp.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblPlanner()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindPlanner", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputUoMPlanner.Text);
            cmd.Parameters.AddWithValue("LabOffice", this.inputLabOffice.Text);
            cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            cmd.Parameters.AddWithValue("MaterialID", this.inputMatIDPlanner.Text);
            cmd.Parameters.AddWithValue("LoTSize", this.inputLOTSize.Text);
            cmd.Parameters.AddWithValue("SLoc", this.inputProdStorLoc.Text);
            cmd.Parameters.AddWithValue("SchedType", this.inputSchedMargKey.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblUoMProc.Text = dr["UoM_Desc"].ToString();
                lblLabOffice.Text = dr["LabOffice"].ToString();
                //lblLOTSize.Text = dr["LotSize"].ToString();
                //lblProdStorLoc.Text = dr["SLoc"].ToString();
                // lblSchedMargKey.Text = dr["SchedType"].ToString();
                lblUoMProc.ForeColor = Color.Black;
                lblLabOffice.ForeColor = Color.Black;
                // lblLOTSize.ForeColor = Color.Black;
                // lblProdStorLoc.ForeColor = Color.Black;
                // lblSchedMargKey.ForeColor = Color.Black;



            }
            conMatWorkFlow.Close();
        }
        protected void bindLblLabOffice()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblLabOffice", conMatWorkFlow);
            cmd.Parameters.AddWithValue("LabOffice", this.inputLabOffice.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblLabOffice.Text = dr["LabOffice"].ToString();
                lblLabOffice.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblMRPGroup()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblMRPGroup", conMatWorkFlow);
            cmd.Parameters.AddWithValue("MRPGroup", this.inputMRPGr.Text);
            cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            cmd.Parameters.AddWithValue("MaterialID", this.inputMatIDPlanner.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMRPGr.Text = dr["MRPGroupDesc"].ToString();
                lblMRPGr.ForeColor = Color.Black;
            }

            conMatWorkFlow.Close();
        }
        protected void bindLblMRPType()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblMRPType", conMatWorkFlow);
            cmd.Parameters.AddWithValue("MRPType", this.inputMRPTyp.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMRPTyp.Text = dr["MRPTypeDesc"].ToString();
                lblMRPTyp.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblMRPController()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblMRPController", conMatWorkFlow);
            cmd.Parameters.AddWithValue("MRPController", this.inputMRPCtrl.Text);
            cmd.Parameters.AddWithValue("Plant", this.inputPlannerPlant.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMRPCtrl.Text = dr["MRPControllerDesc"].ToString();
                lblMRPCtrl.ForeColor = Color.Black;

                inputMRPCtrl.ReadOnly = false;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblLotSize()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblLotSize", conMatWorkFlow);
            cmd.Parameters.AddWithValue("LotSize", this.inputLOTSize.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblLOTSize.Text = dr["LotSizeDesc"].ToString();
                lblLOTSize.ForeColor = Color.Black;

                inputLOTSize.ReadOnly = false;
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
            cmd.Parameters.AddWithValue("LotSize", this.inputLOTSize.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblLOTSize.Text = dr["MRPControllerDesc"].ToString();
                lblLOTSize.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblQA()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindQA", conMatWorkFlow);
            cmd.Parameters.AddWithValue("UoM", this.inputUoMQA.Text);
            cmd.Parameters.AddWithValue("StorConditions", this.inputStoreCond.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblUoMQA.Text = dr["UoM_Desc"].ToString();
                lblStoreCond.Text = dr["StorConditionsDesc"].ToString();
                lblUoMQA.ForeColor = Color.Black;
                lblStoreCond.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblStrategyGroup()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblStrategyGroup", conMatWorkFlow);
            cmd.Parameters.AddWithValue("PlanStrategyGroup", this.inputStrtgyGr.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblStrtgyGr.Text = dr["PlanStrategyGrpDesc"].ToString();
                lblStrtgyGr.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }

        protected void bindlblIndStd()
        {
            DataTable dt = new DataTable();
            //  "ForeignTrade, ForeignDesc"
            string Filter = "ForeignTrade = '" + inputCommImpCode.Text.Trim() + "'";
            dt = Mstr_ForeignTrade_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblCommImpCode.Text = dt.Rows[0]["ForeignDesc"].ToString();
            }
            else
            {
                lblCommImpCode.Text = "Comm/Imp Code No. Desc.";
            }

        }
        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
            param[1] = new SqlParameter("MaterialID", inputMatID.Text.Trim().ToUpper());

            sqlC.ExecuteNonQuery("extendDetailCancel", param);

            Response.Redirect("~/Pages/extendPlantPage");
        }

        protected void btnCancelUpd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/extendPlantPage");
        }
        protected void btnRejectModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReject", "$('#modalReject').modal();", true);
        }

        protected void btnCancelApprove_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable sukses;
                SqlParameter[] param = new SqlParameter[8];

                //master
                param[0] = new SqlParameter("@TransID", lblTransID.Text.Trim());
                param[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim());
                param[2] = new SqlParameter("@CreatedBy", lblUser.Text.Trim());
                param[3] = new SqlParameter("@ApprovalStatus", "Reject");
                param[4] = new SqlParameter("@RejectRevisionNotes", inputRejectReason.Text.Trim());
                param[5] = new SqlParameter("@Module_User", lblPosition.Text.Trim());
                param[6] = new SqlParameter("@Usnam", lblUser.Text.Trim());
                param[7] = new SqlParameter("@LogID", lblLogID.Text.Trim());

                //send param to SP sql
                sukses = sqlC.ExecuteDataTable("ExtendPlant_Approval", param);
                if (sukses.Rows[0]["Return"].ToString() == "1")
                {
                    // countsuccess++;
                    showMessage("Success Cancel Approval Extend Plant");

                }
                else
                { //countfailed++;
                    ShowMessageError("Error Cancel Approval Extend Plant");

                }
            }
            catch (Exception ezz)
            {
                ShowMessageError("Error Cancel Approval Extend Plant");
            }
        }

        protected void showMessage(string message)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //        "alert",
            //        "alert('" + message + "');",
            //        true);

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "key",
                    "ShowMessage('" + message + "')",
                    true);
        }
        protected void showMessage2(string message)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //        "alert",
            //        "alert('" + message + "');",
            //        true);

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "key",
                    "ShowMessage2('" + message + "')",
                    true);
        }
        protected void ShowMessageError(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "key",
                    "ShowMessageError('" + message + "')",
                    true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblExtend.Text == "")
            { 
                autoGenTransID();
            }
            //added by jone
            decimal roundvalue;
            if (inputRoundValueProc.Text == string.Empty)
            {
                //OKE
                inputRoundValueProc.Text = "0";
            }
            else
            {
                if (decimal.TryParse(inputRoundValueProc.Text, out roundvalue)) roundvalue = decimal.Parse(inputRoundValueProc.Text);
                else
                {
                    MsgBox("Invalid round value", this.Page, this);

                    return;
                }
            }
            //end added by jone
            if (inputSalesOrg.Text == "" || inputPlant.Text == "" || inputStorLoc.Text == "" || inputMRPGr.Text == "" || inputMRPTyp.Text == "" || inputMRPCtrl.Text == "" || inputSchedMargKey.Text == "" || inputProcType.Text == "")
            {
                ShowMessageError("One of your required field is still empty!");
                return;
            }
            try
            {
                DataTable sukses;
                sukses = SaveData_ExtendPlant();
                if (int.Parse(sukses.Rows[0]["Return"].ToString()) == 1)
                { //success
                    showMessage("Success Save Extend Plant");
                }
                else
                {//failed
                    ShowMessageError("Error Save Extend Plant");
                }
            }
            catch (Exception eez)
            {
                MsgBox(eez.ToString(), this.Page, this);
            }
        }


        protected DataTable SaveData_ExtendPlant()
        {

            DataTable sukses;
            SqlParameter[] param = new SqlParameter[51];
            string ischkbxCoProd = "";
            if (chkbxCoProd.Checked)
            {
                ischkbxCoProd = "X";
            }
            else
            {
                ischkbxCoProd = "";
            }

            string ischkbxQMProActive = "";
            if (chkbxQMProcActive.Checked)
            {
                ischkbxQMProActive = "X";
            }
            else
            {
                ischkbxQMProActive = "";
            }
            if (chkbxInspectSet.Checked == true)
            {
                stringInspectSet = "X";
            }
            else
            {
                stringInspectSet = "";
            }

            //master
            param[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
            param[1] = new SqlParameter("@OldMatNumb", inputOldMatNum.Text.Trim().ToUpper());
            param[2] = new SqlParameter("@Division", inputDivision.Text.Trim().ToUpper());
            param[3] = new SqlParameter("@CreatedBy", lblUser.Text.Trim().ToUpper());
            param[4] = new SqlParameter("@OldTransID", lblOldTransID.Text.Trim().ToUpper());
            param[5] = new SqlParameter("@LogID", lblLogID.Text.Trim().ToUpper());
            param[6] = new SqlParameter("@Module_User", lblPosition.Text.Trim());
            //RnD
            param[7] = new SqlParameter("@MaterialID", inputMatID.Text.Trim().ToUpper());
            param[8] = new SqlParameter("@CommImpCode", inputCommImpCode.Text.Trim().ToUpper());
            param[9] = new SqlParameter("@Plant", inputPlant.Text.Trim().ToUpper());
            param[10] = new SqlParameter("@SLoc", inputStorLoc.Text.Trim().ToUpper());
            param[11] = new SqlParameter("@SalesOrg", inputSalesOrg.Text.Trim().ToUpper());
            param[12] = new SqlParameter("@MinLotSize", inputMinLotSizeProc.Text.Trim().ToUpper());
            param[13] = new SqlParameter("@RoundingValue", inputRoundValueProc.Text.Trim().ToUpper());
            param[14] = new SqlParameter("@ProcType", inputProcType.Text.Trim().ToUpper());
            param[15] = new SqlParameter("@SpcProcRnd", inputSpcProcRnd.Text.Trim().ToUpper());
            //Procurement
            param[16] = new SqlParameter("@PurcGrp", inputPurcGrp.Text.Trim().ToUpper());
            param[17] = new SqlParameter("@GRProcessingTime", inputGRProcTimeMRP1.Text.Trim().ToUpper());
            param[18] = new SqlParameter("@LoadingGrp", inputLoadingGrp.Text.Trim().ToUpper());
            //Planner
            param[19] = new SqlParameter("@MRPGr", inputMRPGr.Text.Trim().ToUpper());
            param[20] = new SqlParameter("@MRPTyp", inputMRPTyp.Text.Trim().ToUpper());
            param[21] = new SqlParameter("@MRPCtrl", inputMRPCtrl.Text.Trim().ToUpper());
            param[22] = new SqlParameter("@LOTSize", inputLOTSize.Text.Trim().ToUpper());
            param[23] = new SqlParameter("@FixLotSize", inputFixLOTSize.Text.Trim().ToUpper());
            param[24] = new SqlParameter("@MaxStockLv", inputMaxStockLv.Text.Trim().ToUpper());
            param[25] = new SqlParameter("@ProcTypePlanner", inputProcType.Text.Trim().ToUpper());
            param[26] = new SqlParameter("@SpcProc", inputSpcProcRnd.Text.Trim().ToUpper());
            param[27] = new SqlParameter("@ProdStorLoc", inputProdStorLoc.Text.Trim().ToUpper());
            param[28] = new SqlParameter("@SchedMargKey", inputSchedMargKey.Text.Trim().ToUpper());
            param[29] = new SqlParameter("@SftyStck", inputSftyStck.Text.Trim().ToUpper());
            param[30] = new SqlParameter("@PlanDeliv", inputPlantDeliveryTime.Text.Trim().ToUpper());

            param[31] = new SqlParameter("@MinSftyStck", inputMinSftyStck.Text.Trim().ToUpper());
            param[32] = new SqlParameter("@StrtgyGr", inputStrtgyGr.Text.Trim().ToUpper());
            param[33] = new SqlParameter("@ProdSched", inputProdSched.Text.Trim().ToUpper());
            param[34] = new SqlParameter("@ProdSchedProfile", inputProdSchedProfile.Text.Trim().ToUpper());

            param[35] = new SqlParameter("@UoM", inputUoM.Text.Trim().ToUpper());
            param[36] = new SqlParameter("@MatGr", inputMatGr.Text.Trim().ToUpper());
            param[37] = new SqlParameter("@PurchValueKey", inputPurcValKey.Text.Trim().ToUpper());
            param[38] = new SqlParameter("@MinShelfLife", inputMinRemShLf.Text.Trim().ToUpper());
            param[39] = new SqlParameter("@TotalShelfLife", inputTotalShelfLife.Text.Trim().ToUpper());

            param[40] = new SqlParameter("@MfrPartNumb", inputMfrPrtNum.Text.Trim().ToUpper());
            param[41] = new SqlParameter("@IndStdCode", inputIndStdDesc.Text.Trim().ToUpper());
            param[42] = new SqlParameter("@QMControlKey", inputQMCtrlKey.Text.Trim().ToUpper());
            param[43] = new SqlParameter("@StorConditions", inputStoreCond.Text.Trim().ToUpper());
            param[44] = new SqlParameter("@COProd", ischkbxCoProd);
            param[45] = new SqlParameter("@QMProcActive", ischkbxQMProActive);

            param[46] = new SqlParameter("@LabOffice", inputLabOffice.Text.Trim().ToUpper());
            param[47] = new SqlParameter("@PeriodIndForSELD", ddListSLED.SelectedValue.Trim().ToUpper());
            param[48] = new SqlParameter("@DistrChl", inputDistrChl.Text.Trim().ToUpper());
            param[49] = new SqlParameter("@InspectionSetup", stringInspectSet.ToString().Trim().ToUpper());
            param[50] = new SqlParameter("@inputInspectIntrv", inputInspectIntrv.Text.Trim().ToUpper());

            //send param to SP sql
            sukses = sqlC.ExecuteDataTable("ExtendPlant_SaveData", param);

            return sukses;

        }

        //checkbox Inspection Type
        protected void chkbxInspectSet_CheckedChanged(object senders, EventArgs e)
        {
            if (chkbxInspectSet.Checked == true)
            {
                stringInspectSet = "X";
                lblInspectSet.Text = "Active";
            }
            else
            {
                stringInspectSet = "";
                lblInspectSet.Text = "Non Active";
            }
        }

        #region Inspection Setup
        protected void LBSearchInspectionType_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInspectionTypeTyp", "$('#modalInspectionTypeTyp').modal();", true);
            SearchInspectionType("");
        }
        protected void GridViewInspectionType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] InspType = e.CommandArgument.ToString().Split(',');

                txtInspectionType.Text = InspType[0];
                //inputProdStorLoc.Text = SLoc[0];
                //lblProdStorLoc.Text = SLoc[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInspectionTypeTyp", "$('#modalInspectionTypeTyp').modal('hide');", true);

            }
        }
        protected void btnAddInspectionType_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (txtInspectionType.Text == "")
            {
                showMessage2("Inspection Type is empty, cannot add empty data.");
                return;
            }
            else
            {
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                param[2] = new SqlParameter("@InspectionType", this.txtInspectionType.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("CheckInspectionType", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // munculkan pesan bahwa sudah ada
                    showMessage2("Your Inspection Type " + this.txtInspectionType.Text + " is already Exist or did not match Inspection Type master data!");
                    return;
                }
                else
                {
                    //doing insert
                    SqlParameter[] paramm = new SqlParameter[8];
                    paramm[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
                    paramm[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim().ToUpper());
                    paramm[2] = new SqlParameter("@Line", lblLine.Text.Trim().ToUpper());
                    paramm[3] = new SqlParameter("@InspType", txtInspectionType.Text.Trim().ToUpper());
                    paramm[4] = new SqlParameter("@CreateBy", lblUser.Text.Trim());
                    paramm[5] = new SqlParameter("@CreateTime", DateTime.Now);
                    paramm[6] = new SqlParameter("@New", "");
                    paramm[7] = new SqlParameter("@OldTransID", lblOldTransID.Text.Trim().ToUpper());
                    //send param to SP sql
                    sqlC.ExecuteNonQuery("InspectSetup_InsertData", paramm);

                    conMatWorkFlow.Close();
                    Clear_ControlsQCData();
                    QCDataBindRepeaterNew();
                    chkbxInspectSet.Checked = true;
                    lblInspectSet.Text = "Active";
                }
            }
            srcInspectionTypeModalBinding();
            autoGenLineInspectType();
        }
        protected void SearchInspectionType(string Filter)
        {

            DataTable dt = new DataTable();

            dt = Mstr_InspectionType_GetData(Filter, "", "");


            GridViewInspectionType.DataSource = dt;
            GridViewInspectionType.DataBind();


        }
        private DataTable Mstr_InspectionType_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_InspectionType_Select", param);

            return dt;
        }
        protected void srcInspectionTypeModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "Select IT.*  from Mstr_InspectionType IT WHERE IT.InspType NOT IN(SELECT QD.InspType FROM Tbl_QCData QD WHERE QD.TransID = @TransID AND QD.MaterialID = @MaterialID)"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@TransID", this.lblTransID.Text.ToUpper().Trim());
            dataAdapter.SelectCommand.Parameters.AddWithValue("@MaterialID", this.inputMatID.Text.ToUpper().Trim());

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewInspectionType.DataSource = ds.Tables[0];
            GridViewInspectionType.DataBind();
            conMatWorkFlow.Close();
        }
        protected void selectInspectionType_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            txtInspectionType.Text = grdrow.Cells[0].Text;
            txtInspectionType.Focus();
        }
        protected void txtInspectionType_TextChanged(object sender, EventArgs e)
        {
            if (txtInspectionType.Text == "")
            {

            }
            else if (txtInspectionType.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_InspectionType WHERE InspType = @txtInspectionType";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("txtInspectionType", this.txtInspectionType.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                }
                if (dr.HasRows == false)
                {
                    txtInspectionType.Text = "";
                    showMessage2("Wrong Input at Inspection Type!");
                }
                conMatWorkFlow.Close();
            }
        }
        protected void autoGenLineInspectType()
        {
            string num1 = "";
            DataTable dt = new DataTable();
            dt = GetLastLineInspectType(lblTransID.Text.Trim(), inputMatID.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                num1 = dt.Rows[0]["Line"].ToString();
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLineInspectionType.Text = num1;
            }
            else
            {
                num1 = lblLineInspectionType.Text;
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLineInspectionType.Text = num1;
            }
        }
        protected DataTable GetLastLineInspectType(string TransID, string MaterialID)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TransID", TransID);
            param[1] = new SqlParameter("@MaterialID", MaterialID);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[Tbl_QCData_GetLine]", param);

            return dt;
        }
        private void Clear_ControlsQCData()
        {
            txtInspectionType.Text = string.Empty;
            txtInspectionType.Focus();
        }
        protected void reptInspectionType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblInspectType")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtInspectType")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkEdit")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkDelete")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkUpdate")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkCancel")).Visible = true;
            }
            if (e.CommandName == "update")
            {
                string Ct = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtInspectType")).Text.Trim().ToUpper();
                string ID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIDInspectionTypeTbl")).Text.Trim().ToUpper();
                SqlDataAdapter adp = new SqlDataAdapter("Update Tbl_QCData set InspType= @InspType, ModifiedBy=@ModifiedBy, ModifiedTime=@ModifiedTime where IDInspectionType = @IDInspectionType", conMatWorkFlow);
                adp.SelectCommand.Parameters.AddWithValue("@InspType", Ct);
                adp.SelectCommand.Parameters.AddWithValue("@ModifiedBy", this.lblUser.Text.Trim());
                adp.SelectCommand.Parameters.AddWithValue("@ModifiedTime", DateTime.Now);
                adp.SelectCommand.Parameters.AddWithValue("@IDInspectionType", ID);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                QCDataBindRepeater();
            }
            if (e.CommandName == "delete")
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                string ID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIDInspectionTypeTbl")).Text.Trim();
                SqlCommand cmd = new SqlCommand("delete from Tbl_QCData where IDInspectionType = @IDInspectionType", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IDInspectionType",
                    Value = ID,
                    SqlDbType = SqlDbType.BigInt,
                });
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("CheckInspectionTypeExist", param);

                if (dtCheck.Rows.Count == 0)
                {
                    // untick checkbox Inspection Type
                    chkbxInspectSet.Checked = false;
                    lblInspectSet.Text = "Non Active";
                }
                else
                {
                    // tick checkbox Inspection Type
                    chkbxInspectSet.Checked = true;
                    lblInspectSet.Text = "Active";
                }
                conMatWorkFlow.Close();

                QCDataBindRepeater();
            }
            if (e.CommandName == "cancel")
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblInspectType")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtInspectType")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkEdit")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkDelete")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkUpdate")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkCancel")).Visible = false;
            }
        }
        #endregion Inspection Setup

        #region Other Data
        protected void LBSearchVolUnit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalVolUnt", "$('#modalVolUnt').modal();", true);
            SearchVolUnt("");
        }
        protected void SearchVolUnt(string Filter)
        {

            DataTable dt = new DataTable();

            dt = Mstr_VolUnt_GetData(Filter, "", "");


            GridViewVolUnt.DataSource = dt;
            GridViewVolUnt.DataBind();


        }
        private DataTable Mstr_VolUnt_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_VolUnt_Select", param);

            return dt;
        }
        protected void GridViewVolUnt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] VolUnt = e.CommandArgument.ToString().Split(',');

                inputVolUnt.Text = VolUnt[0];
                //inputProdStorLoc.Text = SLoc[0];
                //lblProdStorLoc.Text = SLoc[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalVolUnt", "$('#modalVolUnt').modal('hide');", true);

            }
        }

        protected void LBSearchAun_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalAun", "$('#modalAun').modal();", true);
            SearchAun("");
        }
        protected void SearchAun(string Filter)
        {

            DataTable dt = new DataTable();

            dt = Mstr_Aun_GetData(Filter, "", "");


            GridViewAun.DataSource = dt;
            GridViewAun.DataBind();


        }
        private DataTable Mstr_Aun_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_VolUnt_Select", param);

            return dt;
        }
        protected void GridViewAun_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] Aun = e.CommandArgument.ToString().Split(',');

                inputAun.Text = Aun[0];
                //inputProdStorLoc.Text = SLoc[0];
                //lblProdStorLoc.Text = SLoc[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalAun", "$('#modalAun').modal('hide');", true);

            }
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
                showMessage2("Your gross weight cannot be less or equal with net weight!");
                inputGrossWeight.Text = (NetWeight + plus).ToString();
                return;
            }

            if (inputBun.Text == "")
            {
                showMessage2("Please insert your Base UoM first.");
                inputUoM.Focus();
                return;
            }

            if (inputMatID.Text == "")
            {
                showMessage2("Please insert your Material ID first.");
                inputMatID.Focus();
                return;
            }

            if (inputGrossWeight.Text == "0" || inputX.Text == "0" || inputY.Text == "0" || inputGrossWeight.Text == "" || inputX.Text == "" || inputY.Text == "" || inputAun.Text == "")
            {
                showMessage2("Gross Weight, X, Aun or Y cannot be empty data!");
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
                    showMessage2("Your MaterialID " + this.inputMatID.Text + " is already Exist!");
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
                            showMessage2("Your Aun " + this.inputAun.Text + " is already Exist or did not match Aun Type master data!");
                            return;
                        }
                        else
                        {
                            //doing insert
                            SqlParameter[] paramm = new SqlParameter[15];
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
                            paramm[14] = new SqlParameter("@OldTransID", lblOldTransID.Text.Trim().ToUpper());
                            //send param to SP sql
                            sqlC.ExecuteNonQuery("DetailUomMat_InsertData", paramm);

                            inputMatID.ReadOnly = true;
                            inputMatID.CssClass = "txtBoxRO";
                        }
                    }
                    else
                    {
                        showMessage2("Gross Weight value cannot 0.");
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
                        showMessage2("Your Aun " + this.inputAun.Text + " is already Exist or did not match Aun master data!");
                    }
                    else
                    {
                        //doing insert
                        SqlParameter[] paramm = new SqlParameter[15];
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
                        paramm[14] = new SqlParameter("@OldTransID", lblOldTransID.Text.Trim().ToUpper());
                        //send param to SP sql
                        sqlC.ExecuteNonQuery("DetailUomMat_InsertData", paramm);
                        inputMatID.ReadOnly = true;
                        inputMatID.CssClass = "txtBoxRO";
                    }
                }
                else
                {
                    showMessage2("Gross Weight value cannot 0.");
                }
            }


            conMatWorkFlow.Close();
            Clear_Controls();
            tmpBindRepeaterNew();
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
                tmpBindRepeaterNew();
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
                showMessage2("Your gross weight cannot be less or equal with net weight!");
                inputGrossWeight.Text = (NetWeight + plus).ToString();
                return;
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
        protected void selectWeightUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputWeightUnt.Text = grdrow.Cells[0].Text;

            lblWeightUnt.Text = grdrow.Cells[1].Text;
            lblWeightUnt.ForeColor = Color.Black;
            inputWeightUnt.Focus();
        }
        protected void selectVolUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputVolUnt.Text = grdrow.Cells[0].Text;
            lblVolUnt.Text = grdrow.Cells[1].Text;
            lblVolUnt.ForeColor = Color.Black;
            inputVolUnt.Focus();
        }
        protected void selectAun_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputAun.Text = grdrow.Cells[0].Text;
            lblAMeas.Text = grdrow.Cells[1].Text;
            lblAMeas.ForeColor = Color.Black;
            inputAun.Focus();
        }
        #endregion Other Data

        #region Search Material ID
        protected void LBSearchMatID_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMatID", "$('#modalMatID').modal();", true);

            SearchMaterialID("");
        }

        protected void SearchMaterialID(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Tbl_Material_GetData(Filter, "", "");

            GridViewMaterialID.DataSource = dt;
            GridViewMaterialID.DataBind();
        }

        protected DataTable Tbl_Material_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];
            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Tbl_Material_Select", param);

            return dt;

        }

        protected void GridViewMaterialID_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] MatID = e.CommandArgument.ToString().Split(',');
                inputMatID.Text = MatID[0];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMatTyp", "$('#modalMatID').modal('hide');", true);

            }
        }


        #endregion Search Material ID

        #region Search Store Location
        protected void LBSearchSLoc_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalStorLoc", "$('#modalStorLoc').modal();", true);

            SearchSLoc(" Plant LIKE '%" + inputPlant.Text.Trim() + "%'");
        }

        protected void SearchSLoc(string Filter)
        {

            DataTable dt = new DataTable();

            dt = Mstr_Location_GetData(Filter, "", "");


            GridViewStorLoc.DataSource = dt;
            GridViewStorLoc.DataBind();


        }

        private DataTable Mstr_Location_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_Location_Select", param);

            return dt;
        }

        protected void GridViewStorLoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] SLoc = e.CommandArgument.ToString().Split(',');

                inputStorLoc.Text = SLoc[0];
                lblStorLoc.Text = SLoc[1];
                //inputProdStorLoc.Text = SLoc[0];
                //lblProdStorLoc.Text = SLoc[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalStorLoc", "$('#modalStorLoc').modal('hide');", true);

            }
        }
        protected void selectStorLoc_Click(object sender, EventArgs e)
        {
            lblStorLoc.ForeColor = Color.Black;
        }
        protected void inputStorLoc_TextChanged(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            string Filter = " SLoc = '" + inputStorLoc.Text.Trim() + "' AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            dt = Mstr_Location_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {

                lblStorLoc.Text = dt.Rows[0]["SLoc_Desc"].ToString();
                lblStorLoc.ForeColor = Color.Black;
            }
            else
            {
                lblStorLoc.Text = "Wrong Input!";
                lblStorLoc.ForeColor = Color.Red;
                inputStorLoc.Focus();
                inputStorLoc.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }

        #endregion Search Store Location 

        #region Search ProcType

        protected void selectProcType_Click(object sender, EventArgs e)
        {
            lblProcType.ForeColor = Color.Black;
            lblinputSpcProcRnd.Text = "No Procurement";
            lblinputSpcProcRnd.ForeColor = Color.Black;
            inputSpcProcRnd.Text = "";
        }
        protected void LBSearchProc_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProcTypeRnD", "$('#modalProcTypeRnD').modal();", true);

            SearchProcTypeRnD("");
        }
        protected void SearchProcTypeRnD(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ProcType_GetData(Filter, "", "");
            GridViewProcTypeRnD.DataSource = dt;
            GridViewProcTypeRnD.DataBind();
        }
        protected void GridViewProcTypeRnD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] ProcTypePlanner = e.CommandArgument.ToString().Split(',');

                //inputProcTypePlanner.Text = ProcTypePlanner[0];
                // lblProcTypePlanner.Text = ProcTypePlanner[1];

                lblProcType.Text = ProcTypePlanner[1];
                inputProcType.Text = ProcTypePlanner[0];

                if (inputProcType.Text.Trim() == "E")
                {
                    chkbxCoProd.Enabled = true;
                }
                else
                {
                    chkbxCoProd.Enabled = false;
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProcTypeRnD", "$('#modalProcTypeRnD').modal('hide');", true);

            }
        }
        protected void inputProcType_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " ProcType = '" + inputProcType.Text.Trim() + "'";
            dt = Mstr_ProcType_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {
                lblProcType.Text = dt.Rows[0]["ProcTypeDesc"].ToString();
                lblProcType.ForeColor = Color.Black;

                inputSpcProcRnd.Text = "";
                lblinputSpcProcRnd.Text = absSpcProc.Text;

                if (inputProcType.Text.Trim().ToUpper() == "E")
                {
                    chkbxCoProd.Enabled = true;
                }
                else
                {
                    chkbxCoProd.Enabled = false;
                }

            }
            else
            {
                lblProcType.Text = "Wrong Input!";
                lblProcType.ForeColor = Color.Red;
                inputProcType.Focus();
                inputProcType.Text = "";
                inputSpcProcRnd.Text = "";
                ShowMessageError("Wrong Input");
                if (inputProcType.Text.Trim().ToUpper() == "E")
                {
                    chkbxCoProd.Enabled = true;
                }
                else
                {
                    chkbxCoProd.Enabled = false;
                }
            }
        }
        #endregion Search ProcType

        #region Search Plant
        protected void LBSearchPlant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlantID", "$('#modalPlantID').modal();", true);

            //' Plant NOT IN ( select plant FROM [MatWorkFlow].[dbo].[Tbl_Material]  where MaterialID = ''17000116-0025'' ) '
            string filter = " SOrg = '" + inputSalesOrg.Text.Trim() + "' AND " + " Plant NOT IN ( select plant FROM [MatWorkFlow].[dbo].[Tbl_Material]  where MaterialID = '" + inputMatID.Text + "' AND GlobalStatus <> 'CANCELED' ) ";

            SearchPlant(filter);
        }

        protected void SearchPlant(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_Plant_GetData(Filter, "", "");
            GridViewPlant.DataSource = dt;
            GridViewPlant.DataBind();
        }

        private DataTable Mstr_Plant_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_Plant_Select", param);

            return dt;
        }
        protected void selectPlant_Click(object sender, EventArgs e)
        {
            lblPlant.ForeColor = Color.Black;
            lblStorLoc.Text = "Stor Loc Desc";
            lblStorLoc.ForeColor = Color.Black;
            inputStorLoc.Text = "";

            if (inputPlant.Text == "5200")
            {
                inputStrtgyGr.Text = "52";
            }
        }
        protected void GridViewPlant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] Plant = e.CommandArgument.ToString().Split(',');

                inputPlant.Text = Plant[0];
                lblPlant.Text = Plant[1];
                inputPlannerPlant.Text = Plant[0];
                inputProcPlant.Text = Plant[0];
                inputQAPlant.Text = Plant[0];
                inputQCPlant.Text = Plant[0];
                inputQRPlant.Text = Plant[0];

                inputLoadingGrp.Text = Plant[0];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlantID", "$('#modalPlantID').modal('hide');", true);

            }
        }
        protected void inputPlant_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //  string Filter = " Plant = '" + inputPlant.Text +"'";
            string Filter = " Plant = '" + inputPlant.Text.Trim() + "' AND " + " SOrg = '" + inputSalesOrg.Text.Trim() + "' AND " + " Plant NOT IN ( select plant FROM [MatWorkFlow].[dbo].[Tbl_Material]  where MaterialID = '" + inputMatID.Text.Trim() + "' AND GlobalStatus <> 'CANCELED' ) ";

            dt = Mstr_Plant_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblPlant.Text = dt.Rows[0]["Plant_Desc"].ToString();
                inputPlannerPlant.Text = dt.Rows[0]["Plant"].ToString();
                inputProcPlant.Text = dt.Rows[0]["Plant"].ToString();
                inputQAPlant.Text = dt.Rows[0]["Plant"].ToString();
                inputQCPlant.Text = dt.Rows[0]["Plant"].ToString();
                inputQRPlant.Text = dt.Rows[0]["Plant"].ToString();
                lblPlant.ForeColor = Color.Black;

                inputLoadingGrp.Text = dt.Rows[0]["Plant"].ToString();
                lblStorLoc.Text = "Stor Loc Desc";
                lblStorLoc.ForeColor = Color.Black;
                inputStorLoc.Text = "";
                bindLblLoadingGroup();
            }
            else
            {
                lblPlant.Text = "Wrong Input!";
                lblPlant.ForeColor = Color.Red;
                inputPlant.Focus();
                inputPlant.Text = "";
                inputPlannerPlant.Text = "";
                inputProcPlant.Text = "";
                inputQAPlant.Text = "";
                inputQCPlant.Text = "";
                inputQRPlant.Text = "";
                lblStorLoc.Text = "Stor Loc Desc";
                lblStorLoc.ForeColor = Color.Black;
                inputStorLoc.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }
        #endregion Search Plant

        #region Search Distribution Channel
        protected void LBSearchDistrChl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalDistrChl", "$('#modalDistrChl').modal();", true);

            //' Plant NOT IN ( select plant FROM [MatWorkFlow].[dbo].[Tbl_Material]  where MaterialID = ''17000116-0025'' ) '
            

            SearchDistrChl("");
        }

        protected void SearchDistrChl(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_DistrChl_GetData(Filter, "", "");
            GridViewDistrChl.DataSource = dt;
            GridViewDistrChl.DataBind();
        }

        private DataTable Mstr_DistrChl_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_DistrChannel_Select", param);

            return dt;
        }
        protected void GridViewDistrChl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] DistrChl = e.CommandArgument.ToString().Split(',');

                inputDistrChl.Text = DistrChl[0];
                lblDistrChl.Text = DistrChl[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalDistrChl", "$('#modalDistrChl').modal('hide');", true);
            }
        }
        protected void inputDistrChl_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " DistrChl = '" + inputDistrChl.Text.Trim() + "' ";

            dt = Mstr_DistrChl_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                inputDistrChl.Text = dt.Rows[0]["DistrChl"].ToString();
                lblDistrChl.Text = dt.Rows[0]["DistrChl_Desc"].ToString();
            }
            else
            {
                lblDistrChl.Text = "Wrong Input!";
                lblDistrChl.ForeColor = Color.Red;
                inputDistrChl.Focus();
                inputDistrChl.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }
        #endregion Search Distribution Channel

        #region Search LabOffice
        protected void LBSearchLabOffice_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalLabOffice", "$('#modalLabOffice').modal();", true);

            //' Plant NOT IN ( select plant FROM [MatWorkFlow].[dbo].[Tbl_Material]  where MaterialID = ''17000116-0025'' ) '
            //string filter = " LabOffice = '" + inputLabOffice.Text + "'";

            SearchLabOffice("");
        }

        protected void SearchLabOffice(string Filter)
        {

            DataTable dt = new DataTable();
            dt = Mstr_LabOffice_GetData(Filter, "", "");
            GridViewLabOffice.DataSource = dt;
            GridViewLabOffice.DataBind();
        }

        private DataTable Mstr_LabOffice_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_LabOffice_Select", param);

            return dt;
        }

        protected void selectLabOffice_Click(object sender, EventArgs e)
        {
            lblLabOffice.ForeColor = Color.Black;
        }

        protected void GridViewLabOffice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] LabOffice = e.CommandArgument.ToString().Split(',');

                inputLabOffice.Text = LabOffice[0];
                lblLabOffice.Text = LabOffice[1];

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalLabOffice", "$('#modalLabOffice').modal('hide');", true);

            }
        }
        protected void inputLabOffice_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //  string Filter = " Plant = '" + inputPlant.Text +"'";
            string Filter = " LabOffice = '" + inputLabOffice.Text.Trim() + "'";

            dt = Mstr_LabOffice_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblLabOffice.Text = dt.Rows[0]["Lab_Desc"].ToString();
                lblLabOffice.ForeColor = Color.Black;
            }
            else
            {
                lblLabOffice.Text = "Wrong Input!";
                lblLabOffice.ForeColor = Color.Red;
                inputLabOffice.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }
        #endregion Search LabOffice

        #region SOrganization
        protected void GridViewSalesOrg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] SOrg = e.CommandArgument.ToString().Split(',');

                inputSalesOrg.Text = SOrg[0];
                lblSalesOrg.Text = SOrg[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSalesOrg", "$('#modalSalesOrg').modal('hide');", true);

            }
        }

        protected void LBSearchSOrg_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSalesOrg", "$('#modalSalesOrg').modal();", true);

            SearchSOrg("");
        }

        protected void SearchSOrg(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_SOrg_GetData(Filter, "", "");
            GridViewSalesOrg.DataSource = dt;
            GridViewSalesOrg.DataBind();
        }
        protected void selectSalesOrg_Click(object sender, EventArgs e)
        {
            lblSalesOrg.ForeColor = Color.Black;

            lblPlant.Text = "Plant Desc";
            lblPlant.ForeColor = Color.Black;
            inputPlant.Text = "";
            lblStorLoc.Text = "Stor Loc Desc";
            lblStorLoc.ForeColor = Color.Black;
            inputStorLoc.Text = "";
        }

        private DataTable Mstr_SOrg_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_SOrg_Select", param);

            return dt;
        }

        protected void inputSalesOrg_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string filter = " SOrg = '" + inputSalesOrg.Text.Trim() + "'";

            dt = Mstr_SOrg_GetData(filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblSalesOrg.Text = dt.Rows[0]["SOrg_Desc"].ToString();
                lblSalesOrg.ForeColor = Color.Black;

                lblPlant.Text = "Plant Desc";
                lblPlant.ForeColor = Color.Black;
                inputPlant.Text = "";
                lblStorLoc.Text = "Sales Org. Desc";
                lblStorLoc.ForeColor = Color.Black;
                inputStorLoc.Text = "";
            }
            else
            {
                lblSalesOrg.Text = "Wrong Input!";
                lblSalesOrg.ForeColor = Color.Red;
                inputSalesOrg.Focus();
                inputSalesOrg.Text = "";

                lblPlant.Text = "Plant Desc";
                lblPlant.ForeColor = Color.Black;
                inputPlant.Text = "";
                lblStorLoc.Text = "Sales Org. Desc";
                lblStorLoc.ForeColor = Color.Black;
                inputStorLoc.Text = "";

                ShowMessageError("Wrong Input!");
            }

        }
        #endregion SOrganization

        #region Search Special Procurement
        protected void LBSearchSpcProc_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSpcProc", "$('#modalSpcProc').modal();", true);

            string filter = "";
            if (inputProcType.Text.ToUpper().Trim() == "X")
            {
                filter = " Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            }
            else if (inputProcType.Text.ToUpper().Trim() == "E")
            {
                filter = " [SpclProcurement] LIKE 50 AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            }
            else if (inputProcType.Text.ToUpper().Trim() == "F")
            {
                filter = " [SpclProcurement] NOT LIKE 50 AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            }
            SearchSpcProc(filter);
        }
        protected void selectSpcProc_Click(object sender, EventArgs e)
        {
            Label52.ForeColor = Color.Black;
            inputSpcProcRnd.Focus();
        }
        protected void SearchSpcProc(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_SpecialProcurement_GetData(Filter, "", "");
            GridViewSpcProc.DataSource = dt;
            GridViewSpcProc.DataBind();
        }
        private DataTable Mstr_SpecialProcurement_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_SpecialProcurement_Select]", param);

            return dt;
        }
        protected void GridViewSpcProc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] SpcProcRnd = e.CommandArgument.ToString().Split(',');

                inputSpcProcRnd.Text = SpcProcRnd[0];
                Label52.Text = SpcProcRnd[1];

                // inputSpcProc.Text = SpcProcRnd[0];
                //lblSpcProc.Text = SpcProcRnd[1];


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSpcProc", "$('#modalSpcProc').modal('hide');", true);

            }
        }
        protected void inputSpcProcRnd_TextChanged(object sender, EventArgs e)
        {
            //"SpclProcurement, SpclProcDesc"
            DataTable dt = new DataTable();
            string filter = "";
            if (inputProcType.Text.ToUpper().Trim() == "X")
            {
                filter = " SpclProcurement = '" + inputSpcProcRnd.Text.Trim() + "' AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            }
            else if (inputProcType.Text.ToUpper().Trim() == "E")
            {
                filter = " SpclProcurement = '" + inputSpcProcRnd.Text.Trim() + "' AND [SpclProcurement] LIKE 50 AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            }
            else if (inputProcType.Text.ToUpper().Trim() == "F")
            {
                filter = " SpclProcurement = '" + inputSpcProcRnd.Text.Trim() + "' AND [SpclProcurement] NOT LIKE 50 AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            }
            dt = Mstr_SpecialProcurement_GetData(filter, "", "");

            if (dt.Rows.Count > 0)
            {
                Label52.Text = dt.Rows[0]["SpclProcDesc"].ToString();
                Label52.ForeColor = Color.Black;
            }
            else
            {
                Label52.Text = "Wrong Input!";
                Label52.ForeColor = Color.Red;
                inputSpcProcRnd.Text = "";
                ShowMessageError("Wrong Input");
            }
        }
        #endregion Search Special Procurement

        #region Search CommImpCode
        protected void LBSearchCommImpCode_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommImp", "$('#modalCommImp').modal();", true);

            SearchCommImpCode("");
        }
        protected void SearchCommImpCode(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ForeignTrade_GetData(Filter, "", "");
            GridViewCommImp.DataSource = dt;
            GridViewCommImp.DataBind();
        }
        protected void selectCommImp_Click(object sender, EventArgs e)
        {
            lblCommImpCode.ForeColor = Color.Black;
        }
        private DataTable Mstr_ForeignTrade_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_ForeignTrade_Select]", param);

            return dt;
        }
        protected void GridViewCommImp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] CommImpCode = e.CommandArgument.ToString().Split(',');

                inputCommImpCode.Text = CommImpCode[0];
                lblCommImpCode.Text = CommImpCode[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommImp", "$('#modalCommImp').modal('hide');", true);

            }
        }
        protected void inputCommImpCode_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //  "ForeignTrade, ForeignDesc"
            string Filter = "ForeignTrade = '" + inputCommImpCode.Text.Trim() + "'";
            dt = Mstr_ForeignTrade_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblCommImpCode.Text = dt.Rows[0]["ForeignDesc"].ToString();
                lblCommImpCode.ForeColor = Color.Black;
            }
            else
            {
                lblCommImpCode.Text = "Wrong Input!";
                lblCommImpCode.ForeColor = Color.Red;
                inputCommImpCode.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }
        #endregion Search CommImpCode

        #region Search Loading Group
        protected void LBSearchLoadingGroup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalLoadingGroup", "$('#modalLoadingGroup').modal();", true);

            string filter = " LoadGrp LIKE '%" + inputPlant.Text.Trim() + "%'";
            SearchLoadingGroup(filter);
        }
        protected void selectLoadingGrp_Click(object sender, EventArgs e)
        {
            lblLoadingGrp.ForeColor = Color.Black;
        }
        protected void SearchLoadingGroup(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_LoadingGroup_GetData(Filter, "", "");
            GridViewLoadingGroup.DataSource = dt;
            GridViewLoadingGroup.DataBind();
        }
        private DataTable Mstr_LoadingGroup_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_LoadingGroup_Select]", param);

            return dt;
        }
        protected void GridViewLoadingGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] LoadingGroup = e.CommandArgument.ToString().Split(',');

                inputLoadingGrp.Text = LoadingGroup[0];
                lblLoadingGrp.Text = LoadingGroup[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalLoadingGroup", "$('#modalLoadingGroup').modal('hide');", true);

            }
        }
        protected void inputLoadingGrp_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string filter = "LoadGrp = '" + inputLoadingGrp.Text + "' AND LoadGrp LIKE '%" + inputPlant.Text.Trim() + "%'";
            dt = Mstr_LoadingGroup_GetData(filter, "", "");

            if (dt.Rows.Count == 1)
            {
                lblLoadingGrp.Text = dt.Rows[0]["LoadGrpDesc"].ToString();
                lblLoadingGrp.ForeColor = Color.Black;
            }
            else
            {
                lblLoadingGrp.Text = "Wrong Input!";
                lblLoadingGrp.ForeColor = Color.Red;
                inputLoadingGrp.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search Loading Group

        #region Search Purchasing Group



        protected void selectPurcGrp_Click(object sender, EventArgs e)
        {
            lblPurcGrp.ForeColor = Color.Black;
        }
        protected void LBSearchPurchasingGroup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPurchasingGroup", "$('#modalPurchasingGroup').modal();", true);

            SearchPurchasingGroup("");
        }

        protected void SearchPurchasingGroup(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_PurchasingGroup_GetData(Filter, "", "");
            GridViewPurchasingGroup.DataSource = dt;
            GridViewPurchasingGroup.DataBind();
        }

        private DataTable Mstr_PurchasingGroup_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_PurchasingGroup_Select]", param);

            return dt;
        }
        protected void GridViewPurchasingGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] PurchasingGroup = e.CommandArgument.ToString().Split(',');

                inputPurcGrp.Text = PurchasingGroup[0];
                lblPurcGrp.Text = PurchasingGroup[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPurchasingGroup", "$('#modalPurchasingGroup').modal('hide');", true);

            }
        }
        protected void inputPurcGrp_TextChanged(object sender, EventArgs e)
        {
            //"PurchGrp ,PurchGrpDesc"
            DataTable dt = new DataTable();
            string Filter = "PurchGrp  = '" + inputPurcGrp.Text + "'";
            dt = Mstr_PurchasingGroup_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {
                lblPurcGrp.Text = dt.Rows[0]["PurchGrpDesc"].ToString();
                lblPurcGrp.ForeColor = Color.Black;
            }
            else
            {
                lblPurcGrp.Text = "Wrong Input!";
                lblPurcGrp.ForeColor = Color.Red;
                inputPurcGrp.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }
        #endregion Search Purchasing Group

        #region Search MRP Group
        protected void LBSearchMRPGroup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPGroup", "$('#modalMRPGroup').modal();", true);
            string filter = "";
            filter = " Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            SearchMRPGroup(filter);
        }
        protected void SearchMRPGroup(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_MRPGroup_GetData(Filter, "", "");
            GridViewMRPGroup.DataSource = dt;
            GridViewMRPGroup.DataBind();
        }
        protected void selectMRPGr_Click(object sender, EventArgs e)
        {
            lblMRPGr.ForeColor = Color.Black;
            inputMRPGr.Focus();

            if (inputMRPGr.Text == "0004")
            {
                if (inputPlant.Text == "2200")
                {
                    if (lblDivision.Text == "15")
                    {
                        inputStrtgyGr.Text = "52";
                        bindLblStrategyGroup();
                    }
                }
            }
        }
        private DataTable Mstr_MRPGroup_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_MRPGroup_Select", param);

            return dt;
        }

        protected void GridViewMRPGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] MRPGroup = e.CommandArgument.ToString().Split(',');

                inputMRPGr.Text = MRPGroup[0];
                lblMRPGr.Text = MRPGroup[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPGroup", "$('#modalMRPGroup').modal('hide');", true);

            }
        }
        protected void inputMRPGr_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = "";
            Filter = " MRPGroup = '" + inputMRPGr.Text + "' AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            dt = Mstr_MRPGroup_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {
                lblMRPGr.Text = dt.Rows[0]["MRPGroupDesc"].ToString();
                lblMRPGr.ForeColor = Color.Black;

                if (inputMRPGr.Text == "0004")
                {
                    if (inputPlant.Text == "2200")
                    {
                        if (lblDivision.Text == "15")
                        {
                            inputStrtgyGr.Text = "52";
                            bindLblStrategyGroup();
                        }
                    }
                }
            }
            else
            {
                lblMRPGr.Text = "Wrong Input!";
                lblMRPGr.ForeColor = Color.Red;
                inputMRPGr.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search MRP Group

        #region Search MRP Type
        protected void selectMRPTyp_Click(object sender, EventArgs e)
        {
            lblMRPTyp.ForeColor = Color.Black;
        }
        protected void LBSearchMRPType_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPType", "$('#modalMRPType').modal();", true);

            SearchMRPType("");
        }
        protected void SearchMRPType(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_MRPType_GetData(Filter, "", "");
            GridViewMRPType.DataSource = dt;
            GridViewMRPType.DataBind();
        }
        private DataTable Mstr_MRPType_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_MRPType_Select", param);

            return dt;
        }
        protected void GridViewMRPType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] MRPType = e.CommandArgument.ToString().Split(',');

                inputMRPTyp.Text = MRPType[0];
                lblMRPTyp.Text = MRPType[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPType", "$('#modalMRPType').modal('hide');", true);

            }
        }
        protected void inputMRPTyp_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " MRPType = '" + inputMRPTyp.Text + "'";
            dt = Mstr_MRPType_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {
                lblMRPTyp.Text = dt.Rows[0]["MRPTypeDesc"].ToString();
                lblMRPTyp.ForeColor = Color.Black;
            }
            else
            {
                lblMRPTyp.Text = "Wrong Input!";
                lblMRPTyp.ForeColor = Color.Red;
                inputMRPTyp.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search MRP Type

        #region Search MRP Ctrl
        protected void selectMRPCtrl_Click(object sender, EventArgs e)
        {
            lblMRPCtrl.ForeColor = Color.Black;
        }
        protected void LBSearchMRPCtrl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPCtrl", "$('#modalMRPCtrl').modal();", true);

            string filter = "";
            filter = " Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            SearchMRPCtrl(filter);
        }
        protected void SearchMRPCtrl(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_MRPCtrl_GetData(Filter, "", "");
            GridViewMRPCtrl.DataSource = dt;
            GridViewMRPCtrl.DataBind();
        }
        private DataTable Mstr_MRPCtrl_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_MRPCtrl_Select", param);

            return dt;
        }
        protected void GridViewMRPCtrl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] MRPCtrl = e.CommandArgument.ToString().Split(',');

                inputMRPCtrl.Text = MRPCtrl[0];
                lblMRPCtrl.Text = MRPCtrl[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPCtrl", "$('#modalMRPCtrl').modal('hide');", true);

            }
        }
        protected void inputMRPCtrl_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = "";
            Filter = " MRPController = '" + inputMRPCtrl.Text + "' AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            dt = Mstr_MRPCtrl_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblMRPCtrl.Text = dt.Rows[0]["MRPControllerDesc"].ToString();
                lblMRPCtrl.ForeColor = Color.Black;
            }
            else
            {
                lblMRPCtrl.Text = "Wrong Input!";
                lblMRPCtrl.ForeColor = Color.Red;
                inputMRPCtrl.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search MRP Ctrl

        #region Search LOT Size
        protected void inputLOTSize_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " LotSize = '" + inputLOTSize.Text + "'";
            dt = Mstr_MRPLotSize_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblLOTSize.Text = dt.Rows[0]["LotSizeDesc"].ToString();

                if (inputLOTSize.Text.ToUpper().Trim() == "FX")
                {
                    RowFixLOTSize.Visible = true;
                    lblLOTSize.ForeColor = Color.Black;
                }
                else
                {
                    RowFixLOTSize.Visible = false;
                    inputFixLOTSize.Text = "";
                }
            }
            else
            {
                RowFixLOTSize.Visible = false;
                inputFixLOTSize.Text = "";
                lblLOTSize.Text = "Wrong Input!";
                lblLOTSize.ForeColor = Color.Red;
                inputLOTSize.Text = "";
                MsgBox("Wrong Input!", this.Page, this);
            }


        }
        protected void selectLOTSize_Click(object sender, EventArgs e)
        {
            lblLOTSize.ForeColor = Color.Black;
        }
        protected void LBSearchLOTSize_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPLotSize", "$('#modalMRPLotSize').modal();", true);

            SearchMRPLotSize("");
        }

        protected void SearchMRPLotSize(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_MRPLotSize_GetData(Filter, "", "");
            GridViewMRPLotSize.DataSource = dt;
            GridViewMRPLotSize.DataBind();
        }
        private DataTable Mstr_MRPLotSize_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_MRPLotSize_Select", param);

            return dt;
        }
        protected void GridViewMRPLotSize_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] MRPLotSize = e.CommandArgument.ToString().Split(',');

                inputLOTSize.Text = MRPLotSize[0];
                lblLOTSize.Text = MRPLotSize[1];
                if (inputLOTSize.Text == "FX") RowFixLOTSize.Visible = true;
                else RowFixLOTSize.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMRPLotSize", "$('#modalMRPLotSize').modal('hide');", true);

            }
        }
        #endregion Search LOT Size

        #region Search IndStdDesc
        protected void LBSearchIndStdDesc_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalIndStdDesc", "$('#modalIndStdDesc').modal();", true);

            SearchIndStdDesc("");

        }
        protected void SearchIndStdDesc(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_IndStdDesc_GetData(Filter, "", "");
            GridViewIndStdDesc.DataSource = dt;
            GridViewIndStdDesc.DataBind();
        }

        private DataTable Mstr_IndStdDesc_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_IndStdDesc_Select]", param);

            return dt;
        }

        protected void selectIndStdDesc_Click(object sender, EventArgs e)
        {
            lblIndStdDesc.ForeColor = Color.Black;
        }

        protected void GridViewIndStdDesc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] IndStdDesc = e.CommandArgument.ToString().Split(',');

                inputIndStdDesc.Text = IndStdDesc[0];
                lblIndStdDesc.Text = IndStdDesc[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalIndStdDesc", "$('#modalIndStdDesc').modal('hide');", true);

            }
        }
        protected void inputIndStdDesc_TextChanged(object sender, EventArgs e)
        {
            //      [IndStdCode]
            //,[IndStdDesc]
            DataTable dt = new DataTable();
            string Filter = " IndStdCode = '" + inputIndStdDesc.Text + "'";
            dt = Mstr_IndStdDesc_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblIndStdDesc.Text = dt.Rows[0]["IndStdCode"].ToString();
                lblIndStdDesc.ForeColor = Color.Black;
            }
            else
            {
                lblIndStdDesc.Text = "Wrong Input!";
                lblIndStdDesc.ForeColor = Color.Red;
                inputIndStdDesc.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }
        #endregion Search IndStdDesc

        #region Search Store Condition
        protected void LBSearchStoreCond_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalStoreCond", "$('#modalStoreCond').modal();", true);

            SearchStoreCond("");

        }
        protected void SearchStoreCond(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_StorCondition_GetData(Filter, "", "");
            GridViewStoreCond.DataSource = dt;
            GridViewStoreCond.DataBind();
        }
        private DataTable Mstr_StorCondition_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_StorCondition_Select]", param);

            return dt;
        }
        protected void GridViewStoreCond_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] StoreCond = e.CommandArgument.ToString().Split(',');

                inputStoreCond.Text = StoreCond[0];
                lblStoreCond.Text = StoreCond[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalStoreCond", "$('#modalStoreCond').modal('hide');", true);

            }
        }
        protected void selectStorCond_Click(object sender, EventArgs e)
        {
            lblStoreCond.ForeColor = Color.Black;
        }
        protected void inputStoreCond_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " StorConditions = '" + inputStoreCond.Text + "'";
            dt = Mstr_StorCondition_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {
                lblStoreCond.Text = dt.Rows[0]["StorConditionsDesc"].ToString();
                lblStoreCond.ForeColor = Color.Black;
            }
            else
            {
                lblStoreCond.Text = "Wrong Input!";
                lblStoreCond.ForeColor = Color.Red;
                inputStoreCond.Focus();
                inputStoreCond.Text = "";
                ShowMessageError("Wrong Input!");
            }

        }

        #endregion Search Store Condition

        #region search ProcTypePlanner
        protected void LBSearchProcTypePlanner_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProcTypePlanner", "$('#modalProcTypePlanner').modal();", true);
            string filter = "";
            filter = " Plant LIKE '" + inputPlant.Text.Trim() + "'";
            SearchProcTypePlanner(filter);
        }
        protected void SearchProcTypePlanner(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ProcType_GetData(Filter, "", "");
            GridViewProcTypePlanner.DataSource = dt;
            GridViewProcTypePlanner.DataBind();
        }
        private DataTable Mstr_ProcType_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_ProcType_Select", param);

            return dt;
        }
        protected void GridViewProcTypePlanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] ProcTypePlanner = e.CommandArgument.ToString().Split(',');

                // inputProcTypePlanner.Text = ProcTypePlanner[0];


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProcTypePlanner", "$('#modalProcTypePlanner').modal('hide');", true);

            }
        }
        #endregion search ProcTypePlanner

        #region Search Special Procurement For Planner

        protected void SearchSpcProcPlanner(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_SpecialProcurement_GetData(Filter, "", "");
            GridViewSpcProcPlanner.DataSource = dt;
            GridViewSpcProcPlanner.DataBind();
        }

        protected void LBSearchSpcProcPlanner_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSpcProcPlanner", "$('#modalSpcProcPlanner').modal();", true);

            SearchSpcProcPlanner("");
        }

        protected void GridViewSpcProcPlanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //masi belum ya
            if (e.CommandName == "Select")
            {
                string[] SpcProcTypePlanner = e.CommandArgument.ToString().Split(',');

                //inputSpcProc.Text = SpcProcTypePlanner[0];
                //lblSpcProc.Text = SpcProcTypePlanner[1];

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSpcProcPlanner", "$('#modalSpcProcPlanner').modal('hide');", true);

            }
        }
        #endregion Search Special Procurement For Planner

        #region Search Store Location Production
        protected void LBSearchProdStorLoc_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProdStorLoc", "$('#modalProdStorLoc').modal();", true);
            string Filter = " Plant ='" + inputPlant.Text.Trim() + "'";
            SearchProdStorLoc(Filter);
        }
        protected void selectProdStorLoc_Click(object sender, EventArgs e)
        {
            lblProdStorLoc.ForeColor = Color.Black;
        }
        protected void SearchProdStorLoc(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_Location_GetData(Filter, "", "");
            GridViewProdStorLoc.DataSource = dt;
            GridViewProdStorLoc.DataBind();
        }

        protected void GridViewProdStorLoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] SLoc = e.CommandArgument.ToString().Split(',');

                inputProdStorLoc.Text = SLoc[0];
                lblProdStorLoc.Text = SLoc[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProdStorLoc", "$('#modalProdStorLoc').modal('hide');", true);

            }
        }
        protected void inputProdStorLoc_TextChanged(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            string Filter = " SLoc ='" + inputProdStorLoc.Text.Trim() + "' AND Plant ='" + inputPlant.Text.Trim() + "'";
            dt = Mstr_Location_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {
                lblProdStorLoc.Text = dt.Rows[0]["SLoc_Desc"].ToString();
                lblProdStorLoc.ForeColor = Color.Black;
            }
            else
            {
                lblProdStorLoc.Text = "Wrong Input!";
                lblProdStorLoc.ForeColor = Color.Red;
                inputProdStorLoc.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search Store Location Production

        #region Search Sched Margin Key
        protected void selectSchedMargKey_Click(object sender, EventArgs e)
        {
            lblSchedMargKey.Text = "";
            lblSchedMargKey.ForeColor = Color.Black;
            inputSchedMargKey.Focus();
        }
        protected void LBSearchSchedMarginKey_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSchedMarginKey", "$('#modalSchedMarginKey').modal();", true);
            string filter = " Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            SearchSchedMarginKey(filter);
        }
        protected void SearchSchedMarginKey(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_SchedMarginKey_GetData(Filter, "", "");
            GridViewSchedMarginKey.DataSource = dt;
            GridViewSchedMarginKey.DataBind();
        }
        private DataTable Mstr_SchedMarginKey_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_SchedMarginKey_Select", param);

            return dt;
        }
        protected void GridViewSchedMarginKey_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] SchedMarginKey = e.CommandArgument.ToString().Split(',');
                //SchedMargin Key
                inputSchedMargKey.Text = SchedMarginKey[0];
                lblSchedMargKey.Text = SchedMarginKey[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSchedMarginKey", "$('#modalSchedMarginKey').modal('hide');", true);

            }

        }
        protected void inputSchedMargKey_TextChanged(object sender, EventArgs e)
        {
            string Filter = " SchedType = '" + inputSchedMargKey.Text + "' AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            DataTable dt = new DataTable();
            dt = Mstr_SchedMarginKey_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblSchedMargKey.Text = "";
                lblSchedMargKey.ForeColor = Color.Black;
            }
            else
            {
                lblSchedMargKey.Text = "Wrong Input!";
                lblSchedMargKey.ForeColor = Color.Red;
                inputSchedMargKey.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search Sched Margin key

        #region Search Strategy Group
        protected void LBSearchStrategyGroup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalStrategyGroup", "$('#modalStrategyGroup').modal();", true);

            SearchStrategyGroup("");
        }
        protected void SearchStrategyGroup(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_StrategyGroup_GetData(Filter, "", "");
            GridViewStrategyGroup.DataSource = dt;
            GridViewStrategyGroup.DataBind();
        }
        protected void selectStrategyGroup_Click(object sender, EventArgs e)
        {
            lblStrtgyGr.ForeColor = Color.Black;
        }
        private DataTable Mstr_StrategyGroup_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("sp_Mstr_StrategyGroup_Select", param);

            return dt;
        }


        protected void GridViewStrategyGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] StrategyGroup = e.CommandArgument.ToString().Split(',');

                inputStrtgyGr.Text = StrategyGroup[0];
                lblStrtgyGr.Text = StrategyGroup[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalStrategyGroup", "$('#modalStrategyGroup').modal('hide');", true);

            }
        }
        protected void inputStrtgyGr_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " PlanStrategyGroup = '" + inputStrtgyGr.Text + "'";
            dt = Mstr_StrategyGroup_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblStrtgyGr.Text = dt.Rows[0]["PlanStrategyGrpDesc"].ToString();
                lblStrtgyGr.ForeColor = Color.Black;
            }
            else
            {
                lblStrtgyGr.Text = "Wrong Input!";
                lblStrtgyGr.ForeColor = Color.Red;
                inputStrtgyGr.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search Strategy Group

        #region Search Prod Scheduler
        protected void selectProdSched_Click(object sender, EventArgs e)
        {
            lblProdSched.ForeColor = Color.Black;
            lblProdSchedProfile.ForeColor = Color.Black;
            lblProdSchedProfile.Text = absProdSchedProfile.Text;
            inputProdSchedProfile.Text = "";
        }
        protected void LBSearchProdScheduler_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProdScheduler", "$('#modalProdScheduler').modal();", true);

            string filter = " Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            SearchProdScheduler(filter);
        }
        protected void SearchProdScheduler(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ProdSched_GetData(Filter, "", "");
            GridViewProdScheduler.DataSource = dt;
            GridViewProdScheduler.DataBind();
        }
        private DataTable Mstr_ProdSched_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_ProdSched_Select]", param);

            return dt;
        }
        protected void GridViewProdScheduler_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] ProdScheduler = e.CommandArgument.ToString().Split(',');

                inputProdSched.Text = ProdScheduler[0];
                lblProdSched.Text = ProdScheduler[1];

                string filter = " Plant LIKE '%" + inputPlant.Text.Trim() + "%' AND ProdSchedProfile = '" + ProdScheduler[2] + "'";
                DataTable dt = new DataTable();
                dt = Mstr_ProdSchedProfile_GetData(filter, "", "");
                if (dt.Rows.Count > 0)
                {
                    inputProdSchedProfile.Text = dt.Rows[0]["ProdSchedProfile"].ToString();
                    lblProdSchedProfile.Text = dt.Rows[0]["ProdSchedProfileDesc"].ToString();
                }
                else
                {
                    inputProdSchedProfile.Text = "";
                    lblProdSchedProfile.Text = "Production Scheduler Profile Desc.";
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProdScheduler", "$('#modalProdScheduler').modal('hide');", true);

            }
        }

        protected void inputProdSched_TextChanged(object sender, EventArgs e)
        {
            //       [Plant],[ProdSched],[ProdSchedDesc],[ProdSchedProfile]

            string Filter = " ProdSched = '" + inputProdSched.Text.Trim() + "'  AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            DataTable dt = new DataTable();
            dt = Mstr_ProdSched_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                lblProdSched.Text = dt.Rows[0]["ProdSchedDesc"].ToString();

                string filter2 = " Plant LIKE '%" + inputPlant.Text.Trim() + "%' AND ProdSchedProfile = '" + dt.Rows[0]["ProdSchedProfile"].ToString() + "'";
                DataTable dt2 = new DataTable();
                dt2 = Mstr_ProdSchedProfile_GetData(filter2, "", "");
                if (dt2.Rows.Count > 0)
                {
                    inputProdSchedProfile.Text = dt2.Rows[0]["ProdSchedProfile"].ToString();
                    lblProdSchedProfile.Text = dt2.Rows[0]["ProdSchedProfileDesc"].ToString();
                    lblProdSched.ForeColor = Color.Black;
                    bindLblProdSched();
                    bindLblProdSchedProfile();
                }
                else
                {
                    inputProdSchedProfile.Text = "";
                    lblProdSchedProfile.Text = "Production Scheduler Profile Desc.";
                    lblProdSched.ForeColor = Color.Black;
                }
            }
            else
            {
                inputProdSchedProfile.Text = "";
                lblProdSchedProfile.Text = "Production Scheduler Profile Desc.";
                lblProdSched.Text = "Wrong Input!";
                lblProdSched.ForeColor = Color.Red;
                inputProdSched.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search Prod Scheduler

        #region Search CommImpCode 2
        protected void LBSearchCommImpCode2_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommImp2", "$('#modalCommImp2').modal();", true);

            SearchCommImpCode2("");
        }


        protected void SearchCommImpCode2(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ForeignTrade_GetData(Filter, "", "");
            GridViewCommImp2.DataSource = dt;
            GridViewCommImp2.DataBind();
        }

        protected void GridViewCommImp2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] CommImpCode = e.CommandArgument.ToString().Split(',');

                inputCommImpCodeProc.Text = CommImpCode[0];
                lblCommImpProc.Text = CommImpCode[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommImp2", "$('#modalCommImp2').modal('hide');", true);

            }
        }
        protected void inputCommImpCodeProc_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " ForeignTrade = '" + inputCommImpCodeProc.Text + "'";
            dt = Mstr_ForeignTrade_GetData(Filter, "", "");

            if (dt.Rows.Count > 0)
            {
                lblCommImpProc.Text = dt.Rows[0]["ForeignDesc"].ToString();
                lblCommImpCode.ForeColor = Color.Black;
            }
            else
            {
                lblCommImpCode.Text = "Wrong Input!";
                lblCommImpCode.ForeColor = Color.Red;
                inputCommImpCodeProc.Text = "";
                ShowMessageError("Wrong Input!");
            }
        }
        #endregion Search CommImpCode 2

        #region Search Prod Sched Profile
        protected void selectProdSchedProfile_Click(object sender, EventArgs e)
        {
            lblProdSchedProfile.ForeColor = Color.Black;
        }
        protected void LBSearchProdSchedProfile_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProdSchedulerProfile", "$('#modalProdSchedulerProfile').modal();", true);
            string filter = " Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            SearchProdSchedulerProfile(filter);
        }
        protected void SearchProdSchedulerProfile(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ProdSchedProfile_GetData(Filter, "", "");
            GridViewProdSchedProfile.DataSource = dt;
            GridViewProdSchedProfile.DataBind();
        }
        private DataTable Mstr_ProdSchedProfile_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_ProdSchedProfile_Select]", param);

            return dt;
        }
        protected void GridViewProdSchedProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] ProdSchedulerProfile = e.CommandArgument.ToString().Split(',');

                inputProdSchedProfile.Text = ProdSchedulerProfile[0];
                lblProdSchedProfile.Text = ProdSchedulerProfile[1];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProdSchedulerProfile", "$('#modalProdSchedulerProfile').modal('hide');", true);

            }
        }
        protected void inputProdSchedProfile_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string Filter = " ProdSchedProfile = '" + inputProdSchedProfile.Text + "' AND Plant LIKE '%" + inputPlant.Text.Trim() + "%'";
            dt = Mstr_ProdSchedProfile_GetData(Filter, "", "");
            if (dt.Rows.Count > 0)
            {
                inputProdSchedProfile.Text = dt.Rows[0]["ProdSchedProfile"].ToString();
                lblProdSchedProfile.Text = dt.Rows[0]["ProdSchedProfileDesc"].ToString();
            }
            else
            {
                inputProdSchedProfile.Text = "";
                lblProdSchedProfile.Text = "Production Scheduler Profile Desc.";
            }
        }
        #endregion Search Prod Sched Profile

        #region Search Profit Center
        protected void LBSearchProfitCent_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProfitCent", "$('#modalProfitCent').modal();", true);

            SearchProfitCent("");
        }
        protected void SearchProfitCent(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ProfitCenter_GetData(Filter, "", "");
            GridViewProfitCent.DataSource = dt;
            GridViewProfitCent.DataBind();
        }
        private DataTable Mstr_ProfitCenter_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_ProfitCenter_Select]", param);

            return dt;
        }

        protected void GridViewProfitCent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] ProfitCenter = e.CommandArgument.ToString().Split(',');

                // inputProfitCent.Text = ProfitCenter[0];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProfitCent", "$('#modalProfitCent').modal('hide');", true);

            }
        }
        #endregion Search Profit Center

        #region Search Val Class
        protected void LBSearchValClass_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalValClass", "$('#modalValClass').modal();", true);

            SearchValClass("");
        }
        protected void SearchValClass(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_ValuationClasses_GetData(Filter, "", "");
            GridViewValClass.DataSource = dt;
            GridViewValClass.DataBind();
        }
        private DataTable Mstr_ValuationClasses_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_ValuationClasses_Select]", param);

            return dt;
        }

        protected void GridViewValClass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] ValClass = e.CommandArgument.ToString().Split(',');

                // inputValClass.Text = ValClass[0];
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalValClass", "$('#modalValClass').modal('hide');", true);

            }
        }
        #endregion Search Val Class

        #region Tab Menu
        protected void MenuRnD_Click(object sender, EventArgs e)
        {
            this.MenuRnD.BackColor = Color.DeepSkyBlue;
            this.MenuProcurement.BackColor = Color.Empty;
            this.MenuPlanner.BackColor = Color.Empty;
            this.MenuQA.BackColor = Color.Empty;
            this.MenuQC.BackColor = Color.Empty;
            this.MenuQR.BackColor = Color.Empty;
            lblMenu.Text = "R&D";
            rndTBL.Visible = true;

            procTBL.Visible = false;
            plannerTbl.Visible = false;
            QCTbl.Visible = false;
            QATbl.Visible = false;
            QRTbl.Visible = false;

            // ficoTBL.Visible = false;
            if (inputTypeProc.Text == "RM")
            {
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;
                //foreignTradeDt.Visible = false;
                WorkingSchedule.Visible = true;
            }
            else
            {
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;
                bscDt1Dimension.Visible = true;
                //MRP1LOTSizeDt.Visible = true;
                //foreignTradeDt.Visible = true;
                plantShelfLifeDt.Visible = true;
            }


        }

        protected void MenuProcurement_Click(object sender, EventArgs e)
        {
            this.MenuRnD.BackColor = Color.Empty;
            this.MenuProcurement.BackColor = Color.DeepSkyBlue;
            this.MenuPlanner.BackColor = Color.Empty;
            this.MenuQA.BackColor = Color.Empty;
            this.MenuQC.BackColor = Color.Empty;
            this.MenuQR.BackColor = Color.Empty;

            lblMenu.Text = "Procurement";
            procTBL.Visible = true;

            rndTBL.Visible = false;
            plannerTbl.Visible = false;
            QCTbl.Visible = false;
            QATbl.Visible = false;
            QRTbl.Visible = false;

            //procTBL.Style.Add("display", "flex");
            //rndTBL.Style.Add("display", "none");
            //plannerTbl.Style.Add("display", "none");
            //QCTbl.Style.Add("display", "none");
            //QATbl.Style.Add("display", "none");
            //QRTbl.Style.Add("display", "none");
            if (inputTypeProc.Text == "RM")
            {
                bscDt1GnrlDtProc.Visible = true;
                BscDtDimension.Visible = true;
                purchValNOrder.Visible = true;
                MRPLotSize.Visible = true;
                //ForeignTradeData.Visible = true;
                //PlantShelfLifeDtProc.Visible = true;
                SalesData.Visible = true;

            }
            else
            {
                bscDt1GnrlDtProc.Visible = true;
                purchValNOrder.Visible = true;
                SalesData.Visible = true;

            }

        }

        protected void MenuPlanner_Click(object sender, EventArgs e)
        {
            this.MenuRnD.BackColor = Color.Empty;
            this.MenuProcurement.BackColor = Color.Empty;
            this.MenuPlanner.BackColor = Color.DeepSkyBlue;
            this.MenuQA.BackColor = Color.Empty;
            this.MenuQC.BackColor = Color.Empty;
            this.MenuQR.BackColor = Color.Empty;
            lblMenu.Text = "Planner";
            plannerTbl.Visible = true;

            procTBL.Visible = false;
            rndTBL.Visible = false;
            QCTbl.Visible = false;
            QATbl.Visible = false;
            QRTbl.Visible = false;

            bscDt1GnrlDtPlanner.Visible = true;
            bscDt2OtrDt.Visible = true;
            MRP1.Visible = true;
            MRP2.Visible = true;
            MRP3.Visible = true;
            //ficoTBL.Visible = false;


        }

        protected void MenuQC_Click(object sender, EventArgs e)
        {
            this.MenuRnD.BackColor = Color.Empty;
            this.MenuProcurement.BackColor = Color.Empty;
            this.MenuPlanner.BackColor = Color.Empty;
            this.MenuQA.BackColor = Color.Empty;
            this.MenuQC.BackColor = Color.DeepSkyBlue;
            this.MenuQR.BackColor = Color.Empty;
            lblMenu.Text = "QC";
            QCTbl.Visible = true;

            procTBL.Visible = false;
            rndTBL.Visible = false;
            plannerTbl.Visible = false;
            QATbl.Visible = false;
            QRTbl.Visible = false;
            //ficoTBL.Visible = false;


        }

        protected void MenuQA_Click(object sender, EventArgs e)
        {
            this.MenuRnD.BackColor = Color.Empty;
            this.MenuProcurement.BackColor = Color.Empty;
            this.MenuPlanner.BackColor = Color.Empty;
            this.MenuQA.BackColor = Color.DeepSkyBlue;
            this.MenuQC.BackColor = Color.Empty;
            this.MenuQR.BackColor = Color.Empty;
            lblMenu.Text = "QA";
            QATbl.Visible = true;

            QCTbl.Visible = false;
            procTBL.Visible = false;
            rndTBL.Visible = false;
            plannerTbl.Visible = false;
            QRTbl.Visible = false;
            //ficoTBL.Visible = false;


        }

        protected void MenuQR_Click(object sender, EventArgs e)
        {
            this.MenuRnD.BackColor = Color.Empty;
            this.MenuProcurement.BackColor = Color.Empty;
            this.MenuPlanner.BackColor = Color.Empty;
            this.MenuQA.BackColor = Color.Empty;
            this.MenuQC.BackColor = Color.Empty;
            this.MenuQR.BackColor = Color.DeepSkyBlue;

            lblMenu.Text = "QR";
            QRTbl.Visible = true;

            QATbl.Visible = false;
            QCTbl.Visible = false;
            procTBL.Visible = false;
            rndTBL.Visible = false;
            plannerTbl.Visible = false;
            //ficoTBL.Visible = false;


        }

        protected void MenuFICO_Click(object sender, EventArgs e)
        {

            lblMenu.Text = "FICO";
            //ficoTBL.Visible = true;

            QATbl.Visible = false;
            QRTbl.Visible = false;
            QCTbl.Visible = false;
            procTBL.Visible = false;
            rndTBL.Visible = false;
            plannerTbl.Visible = false;

        }
        #endregion Tab Menu 



        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable sukses;
                SqlParameter[] param = new SqlParameter[8];

                //master
                param[0] = new SqlParameter("@TransID", lblTransID.Text.Trim());
                param[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim());
                param[2] = new SqlParameter("@CreatedBy", lblUser.Text.Trim());
                param[3] = new SqlParameter("@ApprovalStatus", "Approve");
                param[4] = new SqlParameter("@RejectRevisionNotes", inputRejectReason.Text.Trim());
                param[5] = new SqlParameter("@Module_User", lblPosition.Text.Trim());
                param[6] = new SqlParameter("@Usnam", lblUser.Text.Trim());
                param[7] = new SqlParameter("@LogID", lblLogID.Text.Trim());

                //send param to SP sql
                //sukses = sqlC.ExecuteDataTable("ExtendPlant_Approval", param);

                //send param to SP sql
                sukses = sqlC.ExecuteDataTable("ExtendPlant_Approval", param);
                if (sukses.Rows[0]["Return"].ToString() == "1")
                {
                    // countsuccess++;
                    showMessage("Success Approve Extend Plant");
                    // Response.Redirect("~/Pages/extendPlantPage");
                }
                else
                { //countfailed++;
                    ShowMessageError("Error Approve Extend Plant");
                }


            }
            catch (Exception ezz)
            {
                //ShowMessageError(ezz.Message.ToString());
                ShowMessageError("Error Approve Extend Plant");
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/extendPlantPage");
        }




        protected void GridViewListView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridViewListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Checking the RowType of the Row  
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbSelect = (LinkButton)e.Row.FindControl("slcExtendMaterial");
                string xx = e.Row.Cells[19].Text.ToString();

                if (lblPosition.Text == "R&D MGR")
                {
                    if (e.Row.Cells[19].Text.ToString() == null || e.Row.Cells[19].Text.ToString() == "&nbsp;")
                    {


                        lbSelect.Visible = true;
                    }

                    else
                    {
                        if (e.Row.Cells[21].Text.ToString() == "CANCELED")
                        {
                            lbSelect.Visible = false;
                        }
                        lbSelect.Visible = false;

                    }
                }
                else if (lblPosition.Text == "R&D")
                {
                    if (e.Row.Cells[20].Text.ToString() == null || e.Row.Cells[20].Text.ToString() == "&nbsp;" || e.Row.Cells[20].Text.ToString().ToUpper() == "X")
                    {
                        lbSelect.Visible = true;
                    }
                    else
                    { lbSelect.Visible = false; }
                }

            }
        }


        protected void btntestajah_Click(object sender, EventArgs e)
        {
            MsgBox("Yey", this.Page, this);
            Response.Redirect("~/Pages/extendPlantPage");
        }


        protected void LBSearchPurchValKey_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPurcValKey", "$('#modalPurcValKey').modal();", true);

            SearchPurchValKey("");


        }
        protected void selectPurcValKey_Click(object sender, EventArgs e)
        {
            lblPurcValKey.Text = "";
            lblPurcValKey.ForeColor = Color.Black;
        }
        protected void SearchPurchValKey(string Filter)
        {
            DataTable dt = new DataTable();
            dt = Mstr_PurchasingValueKey_GetData(Filter, "", "");
            GridViewPurcValKey.DataSource = dt;
            GridViewPurcValKey.DataBind();
        }
        protected void inputPurcValKey_onBlur(object sender, EventArgs e)
        {
            if (inputPurcValKey.Text == "")
            {

            }
            else if (inputPurcValKey.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_PurchasingValueKey WHERE PurchValueKey = @InputPurcValKey";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputPurcValKey", this.inputPurcValKey.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPurcValKey.Text = "";
                    lblPurcValKey.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblPurcValKey.Text = "Wrong Input!";
                    lblPurcValKey.ForeColor = Color.Red;
                    inputPurcValKey.Text = "";
                    ShowMessageError("Wrong Input!");
                }
                conMatWorkFlow.Close();
            }
        }

        private DataTable Mstr_PurchasingValueKey_GetData(String Filter, string StartRow, string Row)
        {
            SqlParameter[] param = new SqlParameter[3];

            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@Filter", Filter);
            param[1] = new SqlParameter("@StartRow", StartRow);
            param[2] = new SqlParameter("@Row", Row);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[sp_Mstr_PurchasingValueKey_Select]", param);

            return dt;
        }

        protected void GridViewPurcValKey_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] PurchValKey = e.CommandArgument.ToString().Split(',');

                inputPurcValKey.Text = PurchValKey[0];

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPurcValKey", "$('#modalPurcValKey').modal('hide');", true);

            }
        }

        protected void chkbxQMProcActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxQMProcActive.Checked)
            {
                inputQMCtrlKey.Text = "Z990";
            }
            else
            {
                inputQMCtrlKey.Text = "";
            }
        }



        protected void chkbxCoProd_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkbxCoProd.Checked)
            //{

            //}
            //else
            //{
            //    inputQMCtrlKey.Text = "";
            //}
        }

        protected void btnTes_Click(object sender, EventArgs e)
        {
            showMessage("this is test button");
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

        private void gridSearch(string Filter)
        {
            if (Filter.Trim().ToUpper() == "ALL")
            {
                GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);

                SqlParameter[] param = new SqlParameter[2];

                //if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
                //{

                //}

                param[0] = new SqlParameter("@inputMatIDDESC", "%" + this.inputMatIDDESC.Text + "%");
                param[1] = new SqlParameter("@TypeUser", lblPosition.Text);

                //send param to SP sql


                ViewState["Row"] = 0;
                string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                string Row = ddlPageSize.SelectedValue;
                var dt = sqlC.ExecuteDataTable("srcListViewExtendMat", param);

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
            else if (Filter.Trim().ToUpper() == "RM")
            {
                GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);

                SqlParameter[] param = new SqlParameter[2];

                //if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
                //{

                //}

                param[0] = new SqlParameter("@Type", "RM");
                param[1] = new SqlParameter("@TypeUser", lblPosition.Text);

                //send param to SP sql


                ViewState["Row"] = 0;
                string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                string Row = ddlPageSize.SelectedValue;
                var dt = sqlC.ExecuteDataTable("srcListViewRMSFFGExtendMat", param);

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

                SqlParameter[] param = new SqlParameter[2];

                //if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
                //{

                //}

                param[0] = new SqlParameter("@Type", "SF");
                param[1] = new SqlParameter("@TypeUser", lblPosition.Text);

                //send param to SP sql


                ViewState["Row"] = 0;
                string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                string Row = ddlPageSize.SelectedValue;
                var dt = sqlC.ExecuteDataTable("srcListViewRMSFFGExtendMat", param);

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

                SqlParameter[] param = new SqlParameter[2];

                //if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
                //{

                //}

                param[0] = new SqlParameter("@Type", "FG");
                param[1] = new SqlParameter("@TypeUser", lblPosition.Text);

                //send param to SP sql


                ViewState["Row"] = 0;
                string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                string Row = ddlPageSize.SelectedValue;
                var dt = sqlC.ExecuteDataTable("srcListViewRMSFFGExtendMat", param);

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
            gridSearch(FilterSearch.Text);
        }

        protected void reptUntMeas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }
    }
}