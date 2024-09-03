using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testForm.Pages
{
    public partial class displayMaterialSAP : System.Web.UI.Page
    {
        string stringCoProd = "";
        string stringFxdPrice = "";
        string stringDontCost = "";
        string stringWithQtyStruct = "";
        string stringMatOrigin = "";
        SQLConnect.SQLConnect sqlC = new SQLConnect.SQLConnect();
        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            FICONavigationMenu.Items[0].Selected = true;
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
                if (lblPosition.Text != "Admin")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
                srcListViewBinding();
            }
            else if (IsPostBack)
            {
                //srcListViewBinding();
            }
            FICONavigationMenu.MenuItemDataBound += new MenuEventHandler(NavigationMenuReport_MenuItemDataBound);
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        protected DataTable GetData_srcListViewRMSFFG(string Module_User, string MatSample, string Division, string Type)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Module_User", Module_User);
            param[1] = new SqlParameter("@Division", Division);
            param[2] = new SqlParameter("@Type", Type);
            param[3] = new SqlParameter("@MatSample", MatSample);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewRMSFFGMatDisp", param);

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
                Value = lblMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            reptUntMeas.DataSource = ds;
            reptUntMeasProc.DataSource = ds;
            reptUntMeas.DataBind();
            reptUntMeasProc.DataBind();
            conMatWorkFlow.Close();
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
                Value = inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            rptClassType.DataSource = ds;
            rptClassType.DataBind();
            conMatWorkFlow.Close();
        }
        protected void QCDataBindRepeater()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * from Tbl_QCData WHERE TransID = @TransID and MaterialID = @MaterialID", conMatWorkFlow);
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
                Value = inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            rptInspectionType.DataSource = ds;
            rptInspectionType.DataBind();
            conMatWorkFlow.Close();
        }

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
            if (e.Item.Value == "0")
            {
                lblMenu.Text = "Finance";

                ficoTBL.Visible = true;

                rndTBL.Visible = false;
                procTBL.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;

                inputProfitCent.ReadOnly = true;
                tdImgBtnFico1.Visible = false;
                inputValClass.ReadOnly = true;
                tdImgBtnFico2.Visible = false;
                //ddPriceCtrl.Enabled = false;
                inputMovPrice.ReadOnly = true;
                inputStndrdPrice.ReadOnly = true;
                inputCostingLotSize.ReadOnly = true;
                inputValCat.ReadOnly = true;
            }
            else if (e.Item.Value == "1")
            {
                lblMenu.Text = "R&D";
                rndTBL.Visible = true;

                ficoTBL.Visible = false;
                procTBL.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;

                if (inputType.Text == "RM")
                {
                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                }
                else
                {
                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                    bscDt1Dimension.Visible = true;
                    MRP1LOTSizeDt.Visible = true;
                    foreignTradeDt.Visible = true;
                    plantShelfLifeDt.Visible = true;
                    trCoProd.Visible = true;
                }
            }
            else if (e.Item.Value == "2")
            {
                lblMenu.Text = "Procurement";
                procTBL.Visible = true;

                ficoTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;

                if (inputType.Text == "RM")
                {
                    rmContent.Visible = true;
                    bscDt1GnrlDtProc.Visible = true;
                    BscDtDimension.Visible = true;
                    purchValNOrder.Visible = true;
                    MRPLotSize.Visible = true;
                    ForeignTradeData.Visible = true;
                    PlantShelfLifeDtProc.Visible = true;
                    SalesData.Visible = true;
                    trPlantDeliveryTime.Visible = true;
                }
                else
                {
                    rmContent.Visible = true;
                    bscDt1GnrlDtProc.Visible = true;
                    purchValNOrder.Visible = true;
                }
            }
            else if (e.Item.Value == "3")
            {
                lblMenu.Text = "Planner";
                plannerTbl.Visible = true;

                procTBL.Visible = false;
                ficoTBL.Visible = false;
                rndTBL.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;

                bscDt1GnrlDtPlanner.Visible = true;
                MRP1.Visible = true;
                MRP2.Visible = true;
                MRP3.Visible = true;
            }
            else if (e.Item.Value == "4")
            {
                lblMenu.Text = "QC";
                QCTbl.Visible = true;

                procTBL.Visible = false;
                ficoTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
            }
            else if (e.Item.Value == "5")
            {
                lblMenu.Text = "QA";
                QATbl.Visible = true;

                QCTbl.Visible = false;
                procTBL.Visible = false;
                ficoTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
                QRTbl.Visible = false;
            }
            else if (e.Item.Value == "6")
            {
                lblMenu.Text = "QR";
                QRTbl.Visible = true;

                QATbl.Visible = false;
                QCTbl.Visible = false;
                procTBL.Visible = false;
                ficoTBL.Visible = false;
                rndTBL.Visible = false;
                plannerTbl.Visible = false;
            }
        }

        //Binding List View Code Public
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
            srcListViewBinding();
        }
        protected DataTable GetData_srcListViewMatID(string inputMatIDDESC, string Module_User, string Division, string MatSample)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Module_User", Module_User);
            param[2] = new SqlParameter("@Division", Division);
            param[3] = new SqlParameter("@MatSample", MatSample);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewCreateMatDisp", param);

            return dt;
        }
        protected DataTable GetData_srcListViewMatDESC(string inputMatIDDESC, string Module_User, string Division, string MatSample)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Module_User", Module_User);
            param[2] = new SqlParameter("@Division", Division);
            param[3] = new SqlParameter("@MatSample", MatSample);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewCreateMatDisp", param);

            return dt;
        }
        protected void srcListViewBinding()
        {
            FilterSearch.Text = "ALL";
            gridSearch(FilterSearch.Text);
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

                if (columnRDEnd == "&nbsp;" && columnStatus == "&nbsp;")
                {
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                }
                else if (columnRDEnd == "&nbsp;" && columnStatus == "REVISIONRND" && lblPosition.Text != "FICO MGR")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                }
                else if (columnRDEnd == "&nbsp;" && columnStatus == "REVISIONRNDAPPROVAL" && lblPosition.Text != "FICO MGR")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                }
                else if (columnRDEnd != "&nbsp;" && lblPosition.Text != "FICO MGR")
                {
                    // jika fico revisi dan dept const revisi
                    if (columnFicoEnd == "&nbsp;" && columnFicoStart != "&nbsp;" && (columnQREnd == "&nbsp;" || columnQAEnd == "&nbsp;" || columnQCEnd == "&nbsp;" || columnPlanEnd == "&nbsp;" || columnProcEnd == "&nbsp;" || columnRDEnd == "&nbsp;"))
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;

                        if (columnQAEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnPlanEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnQCEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnProcEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                    }
                    //jika rd dan dept const selesai
                    else if (columnFicoEnd == "&nbsp;" && columnFicoStart != "&nbsp;" && columnQREnd != "&nbsp;" && columnQAEnd != "&nbsp;" && columnQCEnd != "&nbsp;" && columnPlanEnd != "&nbsp;" && columnProcEnd != "&nbsp;" && columnRDEnd != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Open";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                        string lblNotes = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = true;
                    }
                    //jika menunggu approval fico
                    else if (columnFicoEnd != "&nbsp;" && columnFicoStart != "&nbsp;" && columnQREnd != "&nbsp;" && columnQAEnd != "&nbsp;" && columnQCEnd != "&nbsp;" && columnPlanEnd != "&nbsp;" && columnProcEnd != "&nbsp;" && columnRDEnd != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                        string lblNotesIn = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;

                        if (columnMaterialApproved == "x")
                        {
                            ((Label)e.Row.FindControl("lblStatus")).Text = "Closed";
                            ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Queue";
                            string lblNotesIOn = "";
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotesIOn;
                            ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                            if (columnGlobalStatus == "Completed")
                            {
                                ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Completed";
                            }
                        }
                    }
                    else if (columnFicoStart == "&nbsp;" && columnFicoEnd == "&nbsp;")
                    {
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;

                        if (columnQAEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }

                        if (columnPlanEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }

                        if (columnQCEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }

                        if (columnProcEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }

                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                    }
                }

                if (columnFicoStart != "&nbsp;" && columnFicoEnd != "&nbsp;" && columnRDEnd != "&nbsp;" && columnProcEnd != "&nbsp;" && columnPlanEnd != "&nbsp;" && columnQCEnd != "&nbsp;" && columnQAEnd != "&nbsp;" && columnQREnd != "&nbsp;" && lblPosition.Text == "FICO MGR" && columnMaterialApproved == "&nbsp;")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = true;
                }
                else if (columnFicoStart != "&nbsp;" && columnFicoEnd != "&nbsp;" && columnQRStart != "&nbsp;" && columnQREnd != "&nbsp;" && columnQAStart != "&nbsp;" && columnQAEnd != "&nbsp;" && columnQCStart != "&nbsp;" && columnQCEnd != "&nbsp;" && columnPlanStart != "&nbsp;" && columnPlanEnd != "&nbsp;" && columnProcStart != "&nbsp;" && columnProcEnd != "&nbsp;" && lblPosition.Text == "FICO MGR" && columnMaterialApproved == "x")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Closed";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Queue";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                    if (columnGlobalStatus == "Completed")
                    {
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Completed";
                    }
                }
                else if (columnFicoStart != "&nbsp;" && columnFicoEnd == "&nbsp;" && lblPosition.Text == "FICO MGR" && columnMaterialApproved == "&nbsp;")
                {
                    if (columnRDEnd != "&nbsp;" && columnProcEnd != "&nbsp;" && columnPlanEnd != "&nbsp;" && columnQCEnd != "&nbsp;" && columnQAEnd != "&nbsp;" && columnQREnd != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Open";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                        string lblNotes = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                    }
                    else if (columnRDEnd != "&nbsp;" && (columnProcEnd == "&nbsp;" || columnPlanEnd == "&nbsp;" || columnQCEnd == "&nbsp;" || columnQAEnd == "&nbsp;" || columnQREnd == "&nbsp;"))
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                        if (columnQAEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnPlanEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnQCEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnProcEnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        }
                    }

                    else
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                        string lblNotes = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                    }
                }
                else if (columnProcEnd == "&nbsp;" || columnPlanEnd == "&nbsp;" || columnQCEnd == "&nbsp;" || columnQAEnd == "&nbsp;" || columnQREnd == "&nbsp;" && columnFicoStart == "&nbsp;" && columnFicoEnd == "&nbsp;")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;

                    if (columnQAEnd != "&nbsp;")
                    {
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    }
                    if (columnPlanEnd != "&nbsp;")
                    {
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    }
                    if (columnQCEnd != "&nbsp;")
                    {
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    }
                    if (columnProcEnd != "&nbsp;")
                    {
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    }
                    if (columnQREnd != "&nbsp;")
                    {
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    }
                    if (columnRDEnd == "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                    }
                }
                if (columnGlobalStatus == "HOLD")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Req. Cancel";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "HOLD";
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                }
                else if (columnGlobalStatus == "CANCELED")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Canceled";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "CANCELED";
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                }
                else if (columnGlobalStatus == "Error")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = columnGlobalStatus;
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = columnNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                }
                if (lblPosition.Text == "Admin")
                {
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = false;
                }
            }
        }
        //Specific
        protected void modifyThisFico_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            spanTransID.Style.Add("display", "flex");
            Master.FindControl("NavigationMenu").Visible = false;
            Master.FindControl("btnLogOut").Visible = false;
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            LinkButton display = (LinkButton)grdrow.FindControl("display");
            string TransID = display.Text;
            //MainContent
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            if (lblPosition.Text == "Admin")
            {
                try
                {
                    SqlCommand cmdLOGTRANS = new SqlCommand("SELECT * FROM Tbl_LogTrans WHERE TransID = @TransID AND MaterialID=@MaterialID AND Usnam=@Usnam", conMatWorkFlow);
                    cmdLOGTRANS.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdLOGTRANS.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdLOGTRANS.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Usnam",
                        Value = this.lblUser.Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    while (drLOGTRANS.Read())
                    {
                        lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim();
                        lblTransID.Text = drLOGTRANS["TransID"].ToString().Trim();
                        lblMatID.Text = drLOGTRANS["MaterialID"].ToString().Trim();
                    }
                    conMatWorkFlow.Close();

                    conMatWorkFlow.Open();
                    SqlCommand cmdCheckNew = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmdCheckNew.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["NewFICO"].ToString().Trim() == "")
                        {
                            listViewFico.Visible = false;

                            rmContent.Visible = true;
                            ficoTBL.Visible = true;

                            btnSave.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputFicoPlant.Text = dr["Plant"].ToString().Trim();
                            stringCoProd = dr["COProd"].ToString().Trim();

                            if (stringCoProd == "X")
                            {
                                chkbxCoProdFICO.Checked = true;
                                chkbxFxdPrice.Checked = true;
                                stringFxdPrice = "X";
                                lblFxdP.Text = stringFxdPrice;
                            }
                            else
                            {
                                chkbxCoProdFICO.Checked = false;
                                chkbxFxdPrice.Checked = false;
                                chkbxFxdPrice.Enabled = false;
                                stringFxdPrice = "";
                                lblFxdP.Text = stringFxdPrice;
                            }
                        }
                        //Update Data
                        else
                        {
                            listViewFico.Visible = false;

                            rmContent.Visible = true;
                            ficoTBL.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpd.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputFicoPlant.Text = dr["Plant"].ToString().Trim();
                            inputProfitCent.Text = dr["ProfitCenter"].ToString().Trim();
                            inputValClass.Text = dr["ValuationClass"].ToString().Trim();
                            ddPriceCtrl.SelectedValue = dr["PriceControl"].ToString().Trim();
                            inputMovPrice.Text = dr["MovingPrice"].ToString().Trim();
                            inputStndrdPrice.Text = dr["StandardPrice"].ToString().Trim();
                            inputCostingLotSize.Text = dr["CostingLotSize"].ToString().Trim();
                            inputValCat.Text = dr["ValuationCtgry"].ToString().Trim();
                            stringFxdPrice = dr["FxdPrice"].ToString().Trim();
                            stringDontCost = dr["DoNotCost"].ToString().Trim();
                            stringWithQtyStruct = dr["WithQtyStructure"].ToString().Trim();
                            stringMatOrigin = dr["MaterialOrigin"].ToString().Trim();
                            stringCoProd = dr["COProd"].ToString().Trim();

                            if (stringCoProd == "X")
                            {
                                chkbxCoProdFICO.Checked = true;
                                chkbxFxdPrice.Checked = true;
                                stringFxdPrice = "X";
                                lblFxdP.Text = stringFxdPrice;
                            }
                            else
                            {
                                chkbxCoProdFICO.Checked = false;
                                chkbxFxdPrice.Checked = false;
                                chkbxFxdPrice.Enabled = false;
                                stringFxdPrice = "";
                                lblFxdP.Text = stringFxdPrice;
                            }
                            if (stringFxdPrice == "X")
                            {
                                chkbxFxdPrice.Checked = true;
                                lblFxdP.Text = stringFxdPrice;
                            }
                            else
                            {
                                chkbxFxdPrice.Checked = false;
                                lblFxdP.Text = stringFxdPrice;
                            }
                            if (stringDontCost == "X")
                            {
                                chkbxDontCost.Checked = true;
                                lblDont.Text = stringDontCost;
                                lblDontCost.Text = "Active";
                            }
                            else
                            {
                                chkbxDontCost.Checked = false;
                                lblDont.Text = stringDontCost;
                                lblDontCost.Text = "Not Active";
                            }
                            if (stringWithQtyStruct == "X")
                            {
                                chkbxWithQtyStruct.Checked = true;
                                lblWith.Text = stringWithQtyStruct;
                                lblWithQtyStruct.Text = "Active";
                            }
                            else
                            {
                                chkbxWithQtyStruct.Checked = false;
                                lblWith.Text = stringWithQtyStruct;
                                lblWithQtyStruct.Text = "Not Active";
                            }
                            if (stringMatOrigin == "X")
                            {
                                chkbxMatOrigin.Checked = true;
                                lblMat.Text = stringMatOrigin;
                                lblMatOrigin.Text = "Active";
                            }
                            else
                            {
                                chkbxMatOrigin.Checked = false;
                                lblMat.Text = stringMatOrigin;
                                lblMatOrigin.Text = "Not Active";
                            }
                        }
                    }
                    conMatWorkFlow.Close();
                    srcValClassModalBinding();
                    srcProfitCentModalBinding();
                    bindLblProfitCenter();
                    bindLblValuationClass();
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                    return;
                }
            }
            else if (lblPosition.Text == "FICO")
            {
                try
                {
                    SqlCommand cmdLOGTRANS = new SqlCommand("SELECT * FROM Tbl_LogTrans WHERE TransID = @TransID AND MaterialID=@MaterialID AND Usnam=@Usnam", conMatWorkFlow);
                    cmdLOGTRANS.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdLOGTRANS.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdLOGTRANS.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Usnam",
                        Value = this.lblUser.Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    while (drLOGTRANS.Read())
                    {
                        lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim();
                        lblTransID.Text = drLOGTRANS["TransID"].ToString().Trim();
                        lblMatID.Text = drLOGTRANS["MaterialID"].ToString().Trim();
                    }
                    conMatWorkFlow.Close();

                    conMatWorkFlow.Open();
                    SqlCommand cmdCheckNew = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmdCheckNew.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["NewFICO"].ToString().Trim() == "")
                        {
                            listViewFico.Visible = false;

                            rmContent.Visible = true;
                            ficoTBL.Visible = true;

                            btnSave.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputFicoPlant.Text = dr["Plant"].ToString().Trim();
                            stringCoProd = dr["COProd"].ToString().Trim();
                            string extendPlant = dr["ExtendPlant"].ToString().Trim();

                            if (extendPlant.ToUpper().Trim() == "X")
                            {
                                inputValClass.Text = dr["ValuationClass"].ToString().Trim();
                                ddPriceCtrl.SelectedValue = dr["PriceControl"].ToString().Trim();

                                ListItem li = new ListItem(ddPriceCtrl.SelectedItem.Text, ddPriceCtrl.SelectedValue, true);
                                ddPriceCtrl.Items.Clear();
                                ddPriceCtrl.Items.Add(li);
                                ddPriceCtrl.CssClass = "txtBoxRO";

                                tdValClass.Attributes.Add("colspan", "2");
                                inputValClass.ReadOnly = true;
                                inputValClass.CssClass = "txtBoxRO";
                                tdImgBtnFico2.Visible = false;
                            }

                            if (stringCoProd == "X")
                            {
                                chkbxCoProdFICO.Checked = true;
                                chkbxFxdPrice.Checked = true;
                                stringFxdPrice = "X";
                                lblFxdP.Text = stringFxdPrice;
                            }
                            else
                            {
                                chkbxCoProdFICO.Checked = false;
                                chkbxFxdPrice.Checked = false;
                                chkbxFxdPrice.Enabled = false;
                                stringFxdPrice = "";
                                lblFxdP.Text = stringFxdPrice;
                            }
                        }
                        //Update Data
                        else
                        {
                            listViewFico.Visible = false;

                            rmContent.Visible = true;
                            ficoTBL.Visible = true;

                            btnSave.Visible = false;
                            btnCancelSave.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpd.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputFicoPlant.Text = dr["Plant"].ToString().Trim();
                            inputProfitCent.Text = dr["ProfitCenter"].ToString().Trim();
                            inputValClass.Text = dr["ValuationClass"].ToString().Trim();
                            ddPriceCtrl.SelectedValue = dr["PriceControl"].ToString().Trim();
                            inputMovPrice.Text = dr["MovingPrice"].ToString().Trim();
                            inputStndrdPrice.Text = dr["StandardPrice"].ToString().Trim();
                            inputCostingLotSize.Text = dr["CostingLotSize"].ToString().Trim();
                            inputValCat.Text = dr["ValuationCtgry"].ToString().Trim();
                            stringFxdPrice = dr["FxdPrice"].ToString().Trim();
                            stringDontCost = dr["DoNotCost"].ToString().Trim();
                            stringWithQtyStruct = dr["WithQtyStructure"].ToString().Trim();
                            stringMatOrigin = dr["MaterialOrigin"].ToString().Trim();
                            stringCoProd = dr["COProd"].ToString().Trim();
                            string extendPlant = dr["ExtendPlant"].ToString().Trim();

                            if (extendPlant.ToUpper().Trim() == "X")
                            {
                                ListItem li = new ListItem(ddPriceCtrl.SelectedItem.Text, ddPriceCtrl.SelectedValue, true);
                                ddPriceCtrl.Items.Clear();
                                ddPriceCtrl.Items.Add(li);
                                ddPriceCtrl.CssClass = "txtBoxRO";

                                tdValClass.Attributes.Add("colspan", "2");
                                inputValClass.ReadOnly = true;
                                inputValClass.CssClass = "txtBoxRO";
                                tdImgBtnFico2.Visible = false;
                            }

                            if (stringCoProd == "X")
                            {
                                chkbxCoProdFICO.Checked = true;
                                chkbxFxdPrice.Checked = true;
                                stringFxdPrice = "X";
                                lblFxdP.Text = stringFxdPrice;
                            }
                            else
                            {
                                chkbxCoProdFICO.Checked = false;
                                chkbxFxdPrice.Checked = false;
                                chkbxFxdPrice.Enabled = false;
                                stringFxdPrice = "";
                                lblFxdP.Text = stringFxdPrice;
                            }
                            if (stringFxdPrice == "X")
                            {
                                chkbxFxdPrice.Checked = true;
                                lblFxdP.Text = stringFxdPrice;
                            }
                            else
                            {
                                chkbxFxdPrice.Checked = false;
                                lblFxdP.Text = stringFxdPrice;
                            }
                            if (stringDontCost == "X")
                            {
                                chkbxDontCost.Checked = true;
                                lblDont.Text = stringDontCost;
                                lblDontCost.Text = "Active";
                            }
                            else
                            {
                                chkbxDontCost.Checked = false;
                                lblDont.Text = stringDontCost;
                                lblDontCost.Text = "Not Active";
                            }
                            if (stringWithQtyStruct == "X")
                            {
                                chkbxWithQtyStruct.Checked = true;
                                lblWith.Text = stringWithQtyStruct;
                                lblWithQtyStruct.Text = "Active";
                            }
                            else
                            {
                                chkbxWithQtyStruct.Checked = false;
                                lblWith.Text = stringWithQtyStruct;
                                lblWithQtyStruct.Text = "Not Active";
                            }
                            if (stringMatOrigin == "X")
                            {
                                chkbxMatOrigin.Checked = true;
                                lblMat.Text = stringMatOrigin;
                                lblMatOrigin.Text = "Active";
                            }
                            else
                            {
                                chkbxMatOrigin.Checked = false;
                                lblMat.Text = stringMatOrigin;
                                lblMatOrigin.Text = "Not Active";
                            }
                        }
                    }
                    conMatWorkFlow.Close();
                    srcValClassModalBinding();
                    srcProfitCentModalBinding();
                    bindLblProfitCenter();
                    bindLblValuationClass();
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                    return;
                }
            }
            else if (lblPosition.Text == "FICO MGR")
            {
                listViewFico.Visible = false;
                btnSave.Visible = false;
                btnCancelSave.Visible = false;
                btnRevisionFico.Visible = true;
                btnRevisionRnD.Visible = true;
                btnRevisionProc.Visible = true;
                btnRevisionPlanner.Visible = true;
                btnRevisionQC.Visible = true;
                btnRevisionQA.Visible = true;
                btnRevisionQR.Visible = true;

                rmContent.Visible = true;
                divApprMn.Visible = true;
                btnApprove.Visible = true;
                btnCancelApprove.Visible = true;
                //FICO
                inputProfitCent.CssClass = "txtBoxRO";
                inputValClass.CssClass = "txtBoxRO";
                ddPriceCtrl.CssClass = "txtBoxRO";
                inputMovPrice.CssClass = "txtBoxRO";
                inputStndrdPrice.CssClass = "txtBoxRO";
                inputCostingLotSize.CssClass = "txtBoxRO";
                inputValCat.CssClass = "txtBoxRO";
                inputOldMtrlNum.Attributes.Add("placeholder", "");
                inputProfitCent.Attributes.Add("placeholder", "");
                inputValClass.Attributes.Add("placeholder", "");
                inputMovPrice.Attributes.Add("placeholder", "");
                inputStndrdPrice.Attributes.Add("placeholder", "");
                inputCostingLotSize.Attributes.Add("placeholder", "");
                inputValCat.Attributes.Add("placeholder", "");
                inputProfitCent.ReadOnly = true;
                inputValClass.ReadOnly = true;
                //inputProfitCent.ReadOnly = false;
                //inputValClass.ReadOnly = false;
                //ddPriceCtrl.Enabled = false;
                //ddPriceCtrl.Enabled = true;
                inputMovPrice.ReadOnly = true;
                //inputMovPrice.ReadOnly = false;
                inputStndrdPrice.ReadOnly = true;
                //inputStndrdPrice.ReadOnly = false;
                inputCostingLotSize.ReadOnly = true;
                inputValCat.ReadOnly = true;
                chkbxDontCost.Enabled = false;
                //chkbxDontCost.Enabled = true;
                chkbxMatOrigin.Enabled = false;
                //chkbxMatOrigin.Enabled = true;
                chkbxWithQtyStruct.Enabled = false;
                //chkbxWithQtyStruct.Enabled = true;
                tdImgBtnFico1.Visible = false;
                tdImgBtnFico2.Visible = false;
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
                inputStorLoc.CssClass = "txtBoxRO";
                inputSalesOrg.CssClass = "txtBoxRO";
                inputDistrChl.CssClass = "txtBoxRO";
                inputProcType.CssClass = "txtBoxRO";
                inputNetWeight.CssClass = "txtBoxRO";
                inputCommImpCode.CssClass = "txtBoxRO";
                inputMinRemShLf.CssClass = "txtBoxRO";
                inputTotalShelfLife.CssClass = "txtBoxRO";
                inputMinLotSize.CssClass = "txtBoxRO";
                inputRoundValue.CssClass = "txtBoxRO";
                inputMatID.ReadOnly = true;
                inputMatDesc.ReadOnly = true;
                inputUoM.ReadOnly = true;
                inputMatGr.ReadOnly = true;
                inputOldMatNum.ReadOnly = true;
                inputDivision.ReadOnly = true;
                inputPckgMat.ReadOnly = true;
                inputMatType.ReadOnly = true;
                inputPlant.ReadOnly = true;
                inputStorLoc.ReadOnly = true;
                inputSalesOrg.ReadOnly = true;
                inputDistrChl.ReadOnly = true;
                inputProcType.ReadOnly = true;
                inputNetWeight.ReadOnly = true;
                inputCommImpCode.ReadOnly = true;
                inputMinRemShLf.ReadOnly = true;
                inputTotalShelfLife.ReadOnly = true;
                inputMinLotSize.ReadOnly = true;
                inputRoundValue.ReadOnly = true;

                try
                {
                    SqlCommand cmdCheckNew = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmdCheckNew.ExecuteReader();
                    while (dr.Read())
                    {
                        string extendPlant = dr["ExtendPlant"].ToString().Trim();

                        if (extendPlant.ToUpper().Trim() == "X")
                        {
                            btnRevisionProc.Visible = false;
                            btnRevisionPlanner.Visible = false;
                            btnRevisionQC.Visible = false;
                            btnRevisionQA.Visible = false;
                            btnRevisionQR.Visible = false;
                        }

                        lblTransID.Text = dr["TransID"].ToString().Trim();
                        lblMatID.Text = dr["MaterialID"].ToString().Trim();
                        //FICO
                        inputType.Text = dr["Type"].ToString().Trim();
                        inputMatTyp.Text = dr["MatType"].ToString().Trim();
                        inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                        inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                        inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputFicoPlant.Text = dr["Plant"].ToString().Trim();
                        inputProfitCent.Text = dr["ProfitCenter"].ToString().Trim();
                        inputValClass.Text = dr["ValuationClass"].ToString().Trim();
                        ddPriceCtrl.SelectedValue = dr["PriceControl"].ToString().Trim();
                        inputMovPrice.Text = dr["MovingPrice"].ToString().Trim();
                        inputStndrdPrice.Text = dr["StandardPrice"].ToString().Trim();
                        inputCostingLotSize.Text = dr["CostingLotSize"].ToString().Trim();
                        inputValCat.Text = dr["ValuationCtgry"].ToString().Trim();
                        stringFxdPrice = dr["FxdPrice"].ToString().Trim();
                        stringDontCost = dr["DoNotCost"].ToString().Trim();
                        stringWithQtyStruct = dr["WithQtyStructure"].ToString().Trim();
                        stringMatOrigin = dr["MaterialOrigin"].ToString().Trim();
                        stringCoProd = dr["COProd"].ToString().Trim();

                        if (stringCoProd == "X")
                        {
                            chkbxCoProdFICO.Checked = true;
                            chkbxCoProdFICO.Enabled = false;
                            chkbxFxdPrice.Checked = true;
                            chkbxFxdPrice.Enabled = false;
                            stringFxdPrice = "X";
                            lblFxdP.Text = stringFxdPrice;
                        }
                        else
                        {
                            chkbxCoProd.Checked = false;
                            chkbxCoProd.Enabled = false;
                            chkbxFxdPrice.Checked = false;
                            chkbxFxdPrice.Enabled = false;
                            stringFxdPrice = "";
                            lblFxdP.Text = stringFxdPrice;
                        }
                        if (stringFxdPrice == "X")
                        {
                            chkbxFxdPrice.Checked = true;
                            lblFxdP.Text = stringFxdPrice;
                        }
                        else
                        {
                            chkbxFxdPrice.Checked = false;
                            lblFxdP.Text = stringFxdPrice;
                        }
                        if (stringDontCost == "X")
                        {
                            chkbxDontCost.Checked = true;
                            lblDontCost.Text = "Active";
                        }
                        else
                        {
                            chkbxDontCost.Checked = false;
                            lblDontCost.Text = "Not Active";
                        }
                        if (stringWithQtyStruct == "X")
                        {
                            chkbxWithQtyStruct.Checked = true;
                            lblWithQtyStruct.Text = "Active";
                        }
                        else
                        {
                            chkbxWithQtyStruct.Checked = false;
                            lblWithQtyStruct.Text = "Not Active";
                        }
                        if (stringMatOrigin == "X")
                        {
                            chkbxMatOrigin.Checked = true;
                            lblMatOrigin.Text = "Active";
                        }
                        else
                        {
                            chkbxMatOrigin.Checked = false;
                            lblMatOrigin.Text = "Not Active";
                        }

                        //RND
                        inputMatType.Text = dr["MatType"].ToString().Trim();
                        inputMatID.Text = dr["MaterialID"].ToString().Trim();
                        inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoM.Text = dr["UoM"].ToString().Trim();
                        inputMatGr.Text = dr["MatlGroup"].ToString().Trim();
                        inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputDivision.Text = dr["Division"].ToString().Trim();
                        inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                        inputNetWeightUnitRnd.Text = dr["NetUnit"].ToString().Trim().ToUpper();

                        inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();

                        inputSalesOrg.Text = dr["SOrg"].ToString().Trim();
                        inputPlant.Text = dr["Plant"].ToString().Trim();
                        inputStorLoc.Text = dr["Sloc"].ToString().Trim();
                        inputDistrChl.Text = dr["DistrChl"].ToString().Trim();

                        inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                        inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                        stringCoProd = dr["COProd"].ToString().Trim();
                        inputProcType.Text = dr["ProcType"].ToString().Trim();
                        inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                        inputSpcProcRnd.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();

                        inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                        inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();
                        ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();

                        inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();
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
                        //PROC
                        inputTypeProc.Text = dr["Type"].ToString().Trim();
                        inputMatTypeProc.Text = dr["MatType"].ToString().Trim();
                        inputMatIDProc.Text = dr["MaterialID"].ToString().Trim();
                        inputMatDescProc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMProc.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumProc.Text = dr["OldMatNumb"].ToString().Trim();
                        inputProcPlant.Text = dr["Plant"].ToString().Trim();
                        inputPckgMatProc.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputNetWeightProc.Text = dr["NetWeight"].ToString().Trim();
                        inputNetWeightUnitProc.Text = dr["NetUnit"].ToString().Trim();

                        inputCommImpCodeProc.Text = dr["ForeignTrade"].ToString().Trim();

                        inputMinRemShLfProc.Text = dr["MinShelfLife"].ToString().Trim();
                        ddListSLEDProc.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                        inputTotalShelfLifeProc.Text = dr["TotalShelfLife"].ToString().Trim();

                        inputMinLotSizeProc.Text = dr["MinLotSize"].ToString().Trim();
                        inputRoundValueProc.Text = dr["RoundingValue"].ToString().Trim();

                        inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                        inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                        inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                        inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();
                        inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();

                        inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();
                        if (ddListSLEDProc.Text == "D")
                        {
                            lblMinRemShelfLifeProc.Text = "DAY";
                        }
                        else
                        {
                            lblMinRemShelfLifeProc.Text = "MONTH";
                        }
                        //PLANNER
                        inputTypePlanner.Text = dr["Type"].ToString().Trim();
                        inputMatTypePlanner.Text = dr["MatType"].ToString().Trim();
                        inputMatIDPlanner.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescPlanner.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMPlanner.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbPlanner.Text = dr["OldMatNumb"].ToString().Trim();
                        inputPlannerPlant.Text = dr["Plant"].ToString().Trim();
                        inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                        inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                        inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                        inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                        inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                        inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                        inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                        inputProcTypePlanner.Text = dr["ProcType"].ToString().Trim();
                        inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                        inputProdStorLoc.Text = dr["SLoc"].ToString().Trim();
                        inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                        inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                        inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();

                        inputStrtgyGr.Text = dr["PlanStrategyGroup"].ToString().Trim();
                        inputTotalLeadTime.Text = dr["TotLeadTime"].ToString().Trim();

                        inputProdSched.Text = dr["ProdSched"].ToString().Trim();
                        inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString().Trim();
                        //QC
                        string stringInspectSet = "";
                        inputTypeQC.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQC.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQC.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQC.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQC.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQC.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQCPlant.Text = dr["Plant"].ToString().Trim();
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
                        //QA
                        inputTypeQA.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQA.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQA.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQA.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQA.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQA.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQAPlant.Text = dr["Plant"].ToString().Trim();
                        inputStoreCond.Text = dr["StorConditions"].ToString().Trim();
                        //QR
                        inputTypeQR.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQR.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQR.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQR.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQR.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQR.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQRPlant.Text = dr["Plant"].ToString().Trim();
                        inputQMCtrlKey.Text = dr["QMControlKey"].ToString().Trim();
                        lblChkbx.Text = dr["QMProcActive"].ToString().Trim();
                        if (lblChkbx.Text.ToUpper().Trim() == "X")
                        {
                            chkbxQMProcActive.Checked = true;
                            lblQMProcActive.Text = "Active";
                        }
                        else
                        {
                            chkbxQMProcActive.Checked = false;
                            lblQMProcActive.Text = "Non Active";
                        }

                        if (inputMatTyp.Text == "RMRD" || inputMatTyp.Text == "SFRD" || inputMatTyp.Text == "FMRD")
                        {
                            btnRevisionProc.Visible = false;
                            btnRevisionPlanner.Visible = false;
                            btnRevisionQC.Visible = false;
                            btnRevisionQA.Visible = false;
                            btnRevisionQR.Visible = false;
                        }
                    }
                    conMatWorkFlow.Close();
                    tmpBindRepeater();
                    ClassBindRepeater();
                    QCDataBindRepeater();
                    //FICO Lbl
                    bindLblProfitCenter();
                    bindLblValuationClass();
                    //R&D Lbl
                    bindLblBsUntMeas();
                    bindLblDivision();
                    bindLblMatGr();
                    bindLblMatType();
                    bindLblPackMat();
                    bindLblPlant();
                    bindLblSalesOrg();
                    bindLblStorLoc();
                    bindLblDistrChl();
                    bindLblProcType();
                    bindLblIndStdDesc();
                    bindLblSpecialProc();
                    //PROC Lbl
                    bindLblCommImp();
                    bindLblLoadingGroup();
                    bindLblPurcGrp();
                    //PLANNER
                    bindLblLabOffice();
                    bindLblMRPGrp();
                    bindLblMRPType();
                    bindLblMRPController();
                    bindLblLotSize();
                    bindLblProdSched();
                    bindLblProdSchedProfile();
                    //QA
                    bindLblStorCondition();

                    ListItem li = new ListItem(ddPriceCtrl.SelectedItem.Text, ddPriceCtrl.SelectedValue, true);
                    ddPriceCtrl.Items.Clear();
                    ddPriceCtrl.Items.Add(li);

                    ListItem la = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
                    ddListSLED.Items.Clear();
                    ddListSLED.Items.Add(la);

                    ListItem lc = new ListItem(ddListSLED.SelectedItem.Text, ddListSLEDProc.SelectedValue, true);
                    ddListSLEDProc.Items.Clear();
                    ddListSLEDProc.Items.Add(lc);
                }
                catch (Exception exMgr)
                {
                    MsgBox(exMgr.ToString().Trim(), this.Page, this);
                }
            }
            else
            {
                MsgBox(this.lblUser.Text.Trim() + " username ar not for FICO division", this.Page, this);
                Response.Redirect("~/Pages/displayCreateMaterial");
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

            if (lblPosition.Text == "FICO")
            {
                listViewFico.Visible = false;
                btnSave.Visible = false;
                btnCancelSave.Visible = false;
                btnClose.Visible = true;

                rmContent.Visible = true;
                //FICO
                inputProfitCent.CssClass = "txtBoxRO";
                inputValClass.CssClass = "txtBoxRO";
                ddPriceCtrl.CssClass = "txtBoxRO";
                inputMovPrice.CssClass = "txtBoxRO";
                inputStndrdPrice.CssClass = "txtBoxRO";
                inputCostingLotSize.CssClass = "txtBoxRO";
                inputValCat.CssClass = "txtBoxRO";
                inputOldMtrlNum.Attributes.Add("placeholder", "");
                inputProfitCent.Attributes.Add("placeholder", "");
                inputValClass.Attributes.Add("placeholder", "");
                inputMovPrice.Attributes.Add("placeholder", "");
                inputStndrdPrice.Attributes.Add("placeholder", "");
                inputCostingLotSize.Attributes.Add("placeholder", "");
                inputValCat.Attributes.Add("placeholder", "");
                inputProfitCent.ReadOnly = true;
                inputValClass.ReadOnly = true;
                //inputProfitCent.ReadOnly = false;
                //inputValClass.ReadOnly = false;
                //ddPriceCtrl.Enabled = false;
                //ddPriceCtrl.Enabled = true;
                inputMovPrice.ReadOnly = true;
                //inputMovPrice.ReadOnly = false;
                inputStndrdPrice.ReadOnly = true;
                //inputStndrdPrice.ReadOnly = false;
                inputCostingLotSize.ReadOnly = true;
                inputValCat.ReadOnly = true;
                chkbxDontCost.Enabled = false;
                //chkbxDontCost.Enabled = true;
                chkbxMatOrigin.Enabled = false;
                //chkbxMatOrigin.Enabled = true;
                chkbxWithQtyStruct.Enabled = false;
                //chkbxWithQtyStruct.Enabled = true;
                tdImgBtnFico1.Visible = false;
                tdImgBtnFico2.Visible = false;
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
                inputStorLoc.CssClass = "txtBoxRO";
                inputSalesOrg.CssClass = "txtBoxRO";
                inputDistrChl.CssClass = "txtBoxRO";
                inputProcType.CssClass = "txtBoxRO";
                inputNetWeight.CssClass = "txtBoxRO";
                inputCommImpCode.CssClass = "txtBoxRO";
                inputMinRemShLf.CssClass = "txtBoxRO";
                inputTotalShelfLife.CssClass = "txtBoxRO";
                inputMinLotSize.CssClass = "txtBoxRO";
                inputRoundValue.CssClass = "txtBoxRO";
                inputMatID.ReadOnly = true;
                inputMatDesc.ReadOnly = true;
                inputUoM.ReadOnly = true;
                inputMatGr.ReadOnly = true;
                inputOldMatNum.ReadOnly = true;
                inputDivision.ReadOnly = true;
                inputPckgMat.ReadOnly = true;
                inputMatType.ReadOnly = true;
                inputPlant.ReadOnly = true;
                inputStorLoc.ReadOnly = true;
                inputSalesOrg.ReadOnly = true;
                inputDistrChl.ReadOnly = true;
                inputProcType.ReadOnly = true;
                inputNetWeight.ReadOnly = true;
                inputCommImpCode.ReadOnly = true;
                inputMinRemShLf.ReadOnly = true;
                inputTotalShelfLife.ReadOnly = true;
                inputMinLotSize.ReadOnly = true;
                inputRoundValue.ReadOnly = true;

                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                try
                {
                    SqlCommand cmdCheckNew = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmdCheckNew.ExecuteReader();
                    while (dr.Read())
                    {
                        string extendPlant = dr["ExtendPlant"].ToString().Trim();

                        if (extendPlant.ToUpper().Trim() == "X")
                        {
                            btnRevisionProc.Visible = false;
                            btnRevisionPlanner.Visible = false;
                            btnRevisionQC.Visible = false;
                            btnRevisionQA.Visible = false;
                            btnRevisionQR.Visible = false;
                        }

                        lblTransID.Text = dr["TransID"].ToString().Trim();
                        lblMatID.Text = dr["MaterialID"].ToString().Trim();
                        //FICO
                        inputType.Text = dr["Type"].ToString().Trim();
                        inputMatTyp.Text = dr["MatType"].ToString().Trim();
                        inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                        inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                        inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputFicoPlant.Text = dr["Plant"].ToString().Trim();
                        inputProfitCent.Text = dr["ProfitCenter"].ToString().Trim();
                        inputValClass.Text = dr["ValuationClass"].ToString().Trim();
                        ddPriceCtrl.SelectedValue = dr["PriceControl"].ToString().Trim();
                        inputMovPrice.Text = dr["MovingPrice"].ToString().Trim();
                        inputStndrdPrice.Text = dr["StandardPrice"].ToString().Trim();
                        inputCostingLotSize.Text = dr["CostingLotSize"].ToString().Trim();
                        inputValCat.Text = dr["ValuationCtgry"].ToString().Trim();
                        stringFxdPrice = dr["FxdPrice"].ToString().Trim();
                        stringDontCost = dr["DoNotCost"].ToString().Trim();
                        stringWithQtyStruct = dr["WithQtyStructure"].ToString().Trim();
                        stringMatOrigin = dr["MaterialOrigin"].ToString().Trim();
                        stringCoProd = dr["COProd"].ToString().Trim();

                        if (stringCoProd == "X")
                        {
                            chkbxCoProdFICO.Checked = true;
                            chkbxCoProdFICO.Enabled = false;
                            chkbxFxdPrice.Checked = true;
                            chkbxFxdPrice.Enabled = false;
                            stringFxdPrice = "X";
                            lblFxdP.Text = stringFxdPrice;
                        }
                        else
                        {
                            chkbxCoProd.Checked = false;
                            chkbxCoProd.Enabled = false;
                            chkbxFxdPrice.Checked = false;
                            chkbxFxdPrice.Enabled = false;
                            stringFxdPrice = "";
                            lblFxdP.Text = stringFxdPrice;
                        }
                        if (stringFxdPrice == "X")
                        {
                            chkbxFxdPrice.Checked = true;
                            lblFxdP.Text = stringFxdPrice;
                        }
                        else
                        {
                            chkbxFxdPrice.Checked = false;
                            lblFxdP.Text = stringFxdPrice;
                        }
                        if (stringDontCost == "X")
                        {
                            chkbxDontCost.Checked = true;
                            lblDontCost.Text = "Active";
                        }
                        else
                        {
                            chkbxDontCost.Checked = false;
                            lblDontCost.Text = "Not Active";
                        }
                        if (stringWithQtyStruct == "X")
                        {
                            chkbxWithQtyStruct.Checked = true;
                            lblWithQtyStruct.Text = "Active";
                        }
                        else
                        {
                            chkbxWithQtyStruct.Checked = false;
                            lblWithQtyStruct.Text = "Not Active";
                        }
                        if (stringMatOrigin == "X")
                        {
                            chkbxMatOrigin.Checked = true;
                            lblMatOrigin.Text = "Active";
                        }
                        else
                        {
                            chkbxMatOrigin.Checked = false;
                            lblMatOrigin.Text = "Not Active";
                        }

                        //RND
                        inputMatType.Text = dr["MatType"].ToString().Trim();
                        inputMatID.Text = dr["MaterialID"].ToString().Trim();
                        inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoM.Text = dr["UoM"].ToString().Trim();
                        inputMatGr.Text = dr["MatlGroup"].ToString().Trim();
                        inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputDivision.Text = dr["Division"].ToString().Trim();
                        inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                        inputNetWeightUnitRnd.Text = dr["NetUnit"].ToString().Trim().ToUpper();

                        inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();

                        inputSalesOrg.Text = dr["SOrg"].ToString().Trim();
                        inputPlant.Text = dr["Plant"].ToString().Trim();
                        inputStorLoc.Text = dr["Sloc"].ToString().Trim();
                        inputDistrChl.Text = dr["DistrChl"].ToString().Trim();

                        inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                        inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                        stringCoProd = dr["COProd"].ToString().Trim();
                        inputProcType.Text = dr["ProcType"].ToString().Trim();
                        inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                        inputSpcProcRnd.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();

                        inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                        inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();
                        ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();

                        inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();
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
                        //PROC
                        inputTypeProc.Text = dr["Type"].ToString().Trim();
                        inputMatTypeProc.Text = dr["MatType"].ToString().Trim();
                        inputMatIDProc.Text = dr["MaterialID"].ToString().Trim();
                        inputMatDescProc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMProc.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumProc.Text = dr["OldMatNumb"].ToString().Trim();
                        inputProcPlant.Text = dr["Plant"].ToString().Trim();
                        inputPckgMatProc.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputNetWeightProc.Text = dr["NetWeight"].ToString().Trim();
                        inputNetWeightUnitProc.Text = dr["NetUnit"].ToString().Trim();

                        inputCommImpCodeProc.Text = dr["ForeignTrade"].ToString().Trim();

                        inputMinRemShLfProc.Text = dr["MinShelfLife"].ToString().Trim();
                        ddListSLEDProc.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                        inputTotalShelfLifeProc.Text = dr["TotalShelfLife"].ToString().Trim();

                        inputMinLotSizeProc.Text = dr["MinLotSize"].ToString().Trim();
                        inputRoundValueProc.Text = dr["RoundingValue"].ToString().Trim();

                        inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                        inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                        inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                        inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();
                        inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();

                        inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();
                        if (ddListSLEDProc.Text == "D")
                        {
                            lblMinRemShelfLifeProc.Text = "DAY";
                        }
                        else
                        {
                            lblMinRemShelfLifeProc.Text = "MONTH";
                        }
                        //PLANNER
                        inputTypePlanner.Text = dr["Type"].ToString().Trim();
                        inputMatTypePlanner.Text = dr["MatType"].ToString().Trim();
                        inputMatIDPlanner.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescPlanner.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMPlanner.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbPlanner.Text = dr["OldMatNumb"].ToString().Trim();
                        inputPlannerPlant.Text = dr["Plant"].ToString().Trim();
                        inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                        inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                        inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                        inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                        inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                        inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                        inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                        inputProcTypePlanner.Text = dr["ProcType"].ToString().Trim();
                        inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                        inputProdStorLoc.Text = dr["SLoc"].ToString().Trim();
                        inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                        inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                        inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();

                        inputStrtgyGr.Text = dr["PlanStrategyGroup"].ToString().Trim();
                        inputTotalLeadTime.Text = dr["TotLeadTime"].ToString().Trim();

                        inputProdSched.Text = dr["ProdSched"].ToString().Trim();
                        inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString().Trim();
                        //QC
                        string stringInspectSet = "";
                        inputTypeQC.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQC.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQC.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQC.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQC.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQC.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQCPlant.Text = dr["Plant"].ToString().Trim();
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
                        //QA
                        inputTypeQA.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQA.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQA.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQA.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQA.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQA.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQAPlant.Text = dr["Plant"].ToString().Trim();
                        inputStoreCond.Text = dr["StorConditions"].ToString().Trim();
                        //QR
                        inputTypeQR.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQR.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQR.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQR.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQR.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQR.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQRPlant.Text = dr["Plant"].ToString().Trim();
                        inputQMCtrlKey.Text = dr["QMControlKey"].ToString().Trim();
                        lblChkbx.Text = dr["QMProcActive"].ToString().Trim();
                        if (lblChkbx.Text.ToUpper().Trim() == "X")
                        {
                            chkbxQMProcActive.Checked = true;
                            lblQMProcActive.Text = "Active";
                        }
                        else
                        {
                            chkbxQMProcActive.Checked = false;
                            lblQMProcActive.Text = "Non Active";
                        }

                        if (inputMatTyp.Text == "RMRD" || inputMatTyp.Text == "SFRD" || inputMatTyp.Text == "FMRD")
                        {
                            btnRevisionProc.Visible = false;
                            btnRevisionPlanner.Visible = false;
                            btnRevisionQC.Visible = false;
                            btnRevisionQA.Visible = false;
                            btnRevisionQR.Visible = false;
                        }
                    }
                    conMatWorkFlow.Close();
                    tmpBindRepeater();
                    ClassBindRepeater();
                    QCDataBindRepeater();
                    //FICO Lbl
                    bindLblProfitCenter();
                    bindLblValuationClass();
                    //R&D Lbl
                    bindLblBsUntMeas();
                    bindLblDivision();
                    bindLblMatGr();
                    bindLblMatType();
                    bindLblPackMat();
                    bindLblPlant();
                    bindLblSalesOrg();
                    bindLblStorLoc();
                    bindLblDistrChl();
                    bindLblProcType();
                    bindLblIndStdDesc();
                    bindLblSpecialProc();
                    //PROC Lbl
                    bindLblCommImp();
                    bindLblLoadingGroup();
                    bindLblPurcGrp();
                    //PLANNER
                    bindLblLabOffice();
                    bindLblMRPGrp();
                    bindLblMRPType();
                    bindLblMRPController();
                    bindLblLotSize();
                    bindLblProdSched();
                    bindLblProdSchedProfile();
                    //QA
                    bindLblStorCondition();

                    ListItem li = new ListItem(ddPriceCtrl.SelectedItem.Text, ddPriceCtrl.SelectedValue, true);
                    ddPriceCtrl.Items.Clear();
                    ddPriceCtrl.Items.Add(li);

                    ListItem la = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
                    ddListSLED.Items.Clear();
                    ddListSLED.Items.Add(la);

                    ListItem lc = new ListItem(ddListSLED.SelectedItem.Text, ddListSLEDProc.SelectedValue, true);
                    ddListSLEDProc.Items.Clear();
                    ddListSLEDProc.Items.Add(lc);
                }
                catch (Exception exMgr)
                {
                    MsgBox(exMgr.ToString().Trim(), this.Page, this);
                }
            }
            else
            {
                listViewFico.Visible = false;
                btnSave.Visible = false;
                btnCancelSave.Visible = false;

                rmContent.Visible = true;
                divApprMn.Visible = true;
                btnClose.Visible = true;

                //FICO
                inputProfitCent.CssClass = "txtBoxRO";
                inputValClass.CssClass = "txtBoxRO";
                ddPriceCtrl.CssClass = "txtBoxRO";
                inputMovPrice.CssClass = "txtBoxRO";
                inputStndrdPrice.CssClass = "txtBoxRO";
                inputCostingLotSize.CssClass = "txtBoxRO";
                inputValCat.CssClass = "txtBoxRO";
                inputOldMtrlNum.Attributes.Add("placeholder", "");
                inputProfitCent.Attributes.Add("placeholder", "");
                inputValClass.Attributes.Add("placeholder", "");
                inputMovPrice.Attributes.Add("placeholder", "");
                inputStndrdPrice.Attributes.Add("placeholder", "");
                inputCostingLotSize.Attributes.Add("placeholder", "");
                inputValCat.Attributes.Add("placeholder", "");
                inputProfitCent.ReadOnly = true;
                inputValClass.ReadOnly = true;
                //inputProfitCent.ReadOnly = false;
                //inputValClass.ReadOnly = false;
                //ddPriceCtrl.Enabled = false;
                //ddPriceCtrl.Enabled = true;
                inputMovPrice.ReadOnly = true;
                //inputMovPrice.ReadOnly = false;
                inputStndrdPrice.ReadOnly = true;
                //inputStndrdPrice.ReadOnly = false;
                inputCostingLotSize.ReadOnly = true;
                inputValCat.ReadOnly = true;
                chkbxDontCost.Enabled = false;
                //chkbxDontCost.Enabled = true;
                chkbxMatOrigin.Enabled = false;
                //chkbxMatOrigin.Enabled = true;
                chkbxWithQtyStruct.Enabled = false;
                //chkbxWithQtyStruct.Enabled = true;
                tdImgBtnFico1.Visible = false;
                tdImgBtnFico2.Visible = false;
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
                inputStorLoc.CssClass = "txtBoxRO";
                inputSalesOrg.CssClass = "txtBoxRO";
                inputDistrChl.CssClass = "txtBoxRO";
                inputProcType.CssClass = "txtBoxRO";
                inputNetWeight.CssClass = "txtBoxRO";
                inputCommImpCode.CssClass = "txtBoxRO";
                inputMinRemShLf.CssClass = "txtBoxRO";
                inputTotalShelfLife.CssClass = "txtBoxRO";
                inputMinLotSize.CssClass = "txtBoxRO";
                inputRoundValue.CssClass = "txtBoxRO";
                inputMatID.ReadOnly = true;
                inputMatDesc.ReadOnly = true;
                inputUoM.ReadOnly = true;
                inputMatGr.ReadOnly = true;
                inputOldMatNum.ReadOnly = true;
                inputDivision.ReadOnly = true;
                inputPckgMat.ReadOnly = true;
                inputMatType.ReadOnly = true;
                inputPlant.ReadOnly = true;
                inputStorLoc.ReadOnly = true;
                inputSalesOrg.ReadOnly = true;
                inputDistrChl.ReadOnly = true;
                inputProcType.ReadOnly = true;
                inputNetWeight.ReadOnly = true;
                inputCommImpCode.ReadOnly = true;
                inputMinRemShLf.ReadOnly = true;
                inputTotalShelfLife.ReadOnly = true;
                inputMinLotSize.ReadOnly = true;
                inputRoundValue.ReadOnly = true;

                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                try
                {
                    SqlCommand cmdCheckNew = new SqlCommand("SELECT * FROM Tbl_Material WHERE TransID = @TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = grdrow.Cells[1].Text.Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmdCheckNew.ExecuteReader();
                    while (dr.Read())
                    {
                        string extendPlant = dr["ExtendPlant"].ToString().Trim();

                        if (extendPlant.ToUpper().Trim() == "X")
                        {
                            btnRevisionProc.Visible = false;
                            btnRevisionPlanner.Visible = false;
                            btnRevisionQC.Visible = false;
                            btnRevisionQA.Visible = false;
                            btnRevisionQR.Visible = false;
                        }

                        lblTransID.Text = dr["TransID"].ToString().Trim();
                        lblMatID.Text = dr["MaterialID"].ToString().Trim();
                        //FICO
                        inputType.Text = dr["Type"].ToString().Trim();
                        inputMatTyp.Text = dr["MatType"].ToString().Trim();
                        inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                        inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                        inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputDivisionFico.Text = dr["Division"].ToString().Trim();
                        inputFicoPlant.Text = dr["Plant"].ToString().Trim();
                        inputProfitCent.Text = dr["ProfitCenter"].ToString().Trim();
                        inputValClass.Text = dr["ValuationClass"].ToString().Trim();
                        ddPriceCtrl.SelectedValue = dr["PriceControl"].ToString().Trim();
                        inputMovPrice.Text = dr["MovingPrice"].ToString().Trim();
                        inputStndrdPrice.Text = dr["StandardPrice"].ToString().Trim();
                        inputCostingLotSize.Text = dr["CostingLotSize"].ToString().Trim();
                        inputValCat.Text = dr["ValuationCtgry"].ToString().Trim();
                        stringFxdPrice = dr["FxdPrice"].ToString().Trim();
                        stringDontCost = dr["DoNotCost"].ToString().Trim();
                        stringWithQtyStruct = dr["WithQtyStructure"].ToString().Trim();
                        stringMatOrigin = dr["MaterialOrigin"].ToString().Trim();
                        stringCoProd = dr["COProd"].ToString().Trim();

                        if (stringCoProd == "X")
                        {
                            chkbxCoProdFICO.Checked = true;
                            chkbxCoProdFICO.Enabled = false;
                            chkbxFxdPrice.Checked = true;
                            chkbxFxdPrice.Enabled = false;
                            stringFxdPrice = "X";
                            lblFxdP.Text = stringFxdPrice;
                        }
                        else
                        {
                            chkbxCoProd.Checked = false;
                            chkbxCoProd.Enabled = false;
                            chkbxFxdPrice.Checked = false;
                            chkbxFxdPrice.Enabled = false;
                            stringFxdPrice = "";
                            lblFxdP.Text = stringFxdPrice;
                        }
                        if (stringFxdPrice == "X")
                        {
                            chkbxFxdPrice.Checked = true;
                            lblFxdP.Text = stringFxdPrice;
                        }
                        else
                        {
                            chkbxFxdPrice.Checked = false;
                            lblFxdP.Text = stringFxdPrice;
                        }
                        if (stringDontCost == "X")
                        {
                            chkbxDontCost.Checked = true;
                            lblDontCost.Text = "Active";
                        }
                        else
                        {
                            chkbxDontCost.Checked = false;
                            lblDontCost.Text = "Not Active";
                        }
                        if (stringWithQtyStruct == "X")
                        {
                            chkbxWithQtyStruct.Checked = true;
                            lblWithQtyStruct.Text = "Active";
                        }
                        else
                        {
                            chkbxWithQtyStruct.Checked = false;
                            lblWithQtyStruct.Text = "Not Active";
                        }
                        if (stringMatOrigin == "X")
                        {
                            chkbxMatOrigin.Checked = true;
                            lblMatOrigin.Text = "Active";
                        }
                        else
                        {
                            chkbxMatOrigin.Checked = false;
                            lblMatOrigin.Text = "Not Active";
                        }

                        //RND
                        inputMatType.Text = dr["MatType"].ToString().Trim();
                        inputMatID.Text = dr["MaterialID"].ToString().Trim();
                        inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoM.Text = dr["UoM"].ToString().Trim();
                        inputMatGr.Text = dr["MatlGroup"].ToString().Trim();
                        inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputDivision.Text = dr["Division"].ToString().Trim();
                        inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                        inputNetWeightUnitRnd.Text = dr["NetUnit"].ToString().Trim().ToUpper();

                        inputIndStdDesc.Text = dr["IndStdCode"].ToString().Trim();

                        inputSalesOrg.Text = dr["SOrg"].ToString().Trim();
                        inputPlant.Text = dr["Plant"].ToString().Trim();
                        inputStorLoc.Text = dr["Sloc"].ToString().Trim();
                        inputDistrChl.Text = dr["DistrChl"].ToString().Trim();

                        inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                        inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                        stringCoProd = dr["COProd"].ToString().Trim();
                        inputProcType.Text = dr["ProcType"].ToString().Trim();
                        inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();
                        inputSpcProcRnd.Text = dr["SpclProcurement"].ToString().Trim().ToUpper();

                        inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                        inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();
                        ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();

                        inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();
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
                        //PROC
                        inputTypeProc.Text = dr["Type"].ToString().Trim();
                        inputMatTypeProc.Text = dr["MatType"].ToString().Trim();
                        inputMatIDProc.Text = dr["MaterialID"].ToString().Trim();
                        inputMatDescProc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMProc.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumProc.Text = dr["OldMatNumb"].ToString().Trim();
                        inputProcPlant.Text = dr["Plant"].ToString().Trim();
                        inputPckgMatProc.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputNetWeightProc.Text = dr["NetWeight"].ToString().Trim();
                        inputNetWeightUnitProc.Text = dr["NetUnit"].ToString().Trim();

                        inputCommImpCodeProc.Text = dr["ForeignTrade"].ToString().Trim();

                        inputMinRemShLfProc.Text = dr["MinShelfLife"].ToString().Trim();
                        ddListSLEDProc.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                        inputTotalShelfLifeProc.Text = dr["TotalShelfLife"].ToString().Trim();

                        inputMinLotSizeProc.Text = dr["MinLotSize"].ToString().Trim();
                        inputRoundValueProc.Text = dr["RoundingValue"].ToString().Trim();

                        inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                        inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                        inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                        inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();
                        inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();

                        inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();
                        if (ddListSLEDProc.Text == "D")
                        {
                            lblMinRemShelfLifeProc.Text = "DAY";
                        }
                        else
                        {
                            lblMinRemShelfLifeProc.Text = "MONTH";
                        }
                        //PLANNER
                        inputTypePlanner.Text = dr["Type"].ToString().Trim();
                        inputMatTypePlanner.Text = dr["MatType"].ToString().Trim();
                        inputMatIDPlanner.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescPlanner.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMPlanner.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbPlanner.Text = dr["OldMatNumb"].ToString().Trim();
                        inputPlannerPlant.Text = dr["Plant"].ToString().Trim();
                        inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                        inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                        inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                        inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                        inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                        inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                        inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                        inputProcTypePlanner.Text = dr["ProcType"].ToString().Trim();
                        inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                        inputProdStorLoc.Text = dr["SLoc"].ToString().Trim();
                        inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                        inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                        inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();

                        inputStrtgyGr.Text = dr["PlanStrategyGroup"].ToString().Trim();
                        inputTotalLeadTime.Text = dr["TotLeadTime"].ToString().Trim();

                        inputProdSched.Text = dr["ProdSched"].ToString().Trim();
                        inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString().Trim();
                        //QC
                        string stringInspectSet = "";
                        inputTypeQC.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQC.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQC.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQC.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQC.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQC.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQCPlant.Text = dr["Plant"].ToString().Trim();
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
                        //QA
                        inputTypeQA.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQA.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQA.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQA.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQA.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQA.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQAPlant.Text = dr["Plant"].ToString().Trim();
                        inputStoreCond.Text = dr["StorConditions"].ToString().Trim();
                        //QR
                        inputTypeQR.Text = dr["Type"].ToString().Trim();
                        inputMatTypeQR.Text = dr["MatType"].ToString().Trim();
                        inputMatIDQR.Text = dr["MaterialID"].ToString().Trim();
                        inputMaterialDescQR.Text = dr["MaterialDesc"].ToString().Trim();
                        inputUoMQR.Text = dr["UoM"].ToString().Trim();
                        inputOldMatNumbQR.Text = dr["OldMatNumb"].ToString().Trim();
                        inputQRPlant.Text = dr["Plant"].ToString().Trim();
                        inputQMCtrlKey.Text = dr["QMControlKey"].ToString().Trim();
                        lblChkbx.Text = dr["QMProcActive"].ToString().Trim();
                        if (lblChkbx.Text.ToUpper().Trim() == "X")
                        {
                            chkbxQMProcActive.Checked = true;
                            lblQMProcActive.Text = "Active";
                        }
                        else
                        {
                            chkbxQMProcActive.Checked = false;
                            lblQMProcActive.Text = "Non Active";
                        }

                        if (inputMatTyp.Text == "RMRD" || inputMatTyp.Text == "SFRD" || inputMatTyp.Text == "FMRD")
                        {
                            btnRevisionProc.Visible = false;
                            btnRevisionPlanner.Visible = false;
                            btnRevisionQC.Visible = false;
                            btnRevisionQA.Visible = false;
                            btnRevisionQR.Visible = false;
                        }
                    }
                    conMatWorkFlow.Close();
                    tmpBindRepeater();
                    ClassBindRepeater();
                    QCDataBindRepeater();
                    //FICO Lbl
                    bindLblProfitCenter();
                    bindLblValuationClass();
                    //R&D Lbl
                    bindLblBsUntMeas();
                    bindLblDivision();
                    bindLblMatGr();
                    bindLblMatType();
                    bindLblPackMat();
                    bindLblPlant();
                    bindLblSalesOrg();
                    bindLblStorLoc();
                    bindLblDistrChl();
                    bindLblProcType();
                    bindLblIndStdDesc();
                    bindLblSpecialProc();
                    //PROC Lbl
                    bindLblCommImp();
                    bindLblLoadingGroup();
                    bindLblPurcGrp();
                    //PLANNER
                    bindLblLabOffice();
                    bindLblMRPGrp();
                    bindLblMRPType();
                    bindLblMRPController();
                    bindLblLotSize();
                    bindLblProdSched();
                    bindLblProdSchedProfile();
                    //QA
                    bindLblStorCondition();

                    ListItem li = new ListItem(ddPriceCtrl.SelectedItem.Text, ddPriceCtrl.SelectedValue, true);
                    ddPriceCtrl.Items.Clear();
                    ddPriceCtrl.Items.Add(li);

                    ListItem la = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
                    ddListSLED.Items.Clear();
                    ddListSLED.Items.Add(la);

                    ListItem lc = new ListItem(ddListSLED.SelectedItem.Text, ddListSLEDProc.SelectedValue, true);
                    ddListSLEDProc.Items.Clear();
                    ddListSLEDProc.Items.Add(lc);
                }
                catch (Exception exMgr)
                {
                    MsgBox(exMgr.ToString().Trim(), this.Page, this);
                }
            }
        }

        //LightBox Data Code
        protected void srcProfitCentModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcProfitCenter", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "Plant",
                Value = this.inputFicoPlant.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 4
            });
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewProfitCent.DataSource = ds.Tables[0];
            GridViewProfitCent.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcValClassModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcValClass", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewValClass.DataSource = ds.Tables[0];
            GridViewValClass.DataBind();
            conMatWorkFlow.Close();
        }

        //Modal
        protected void selectProfitCent_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputProfitCent.Text = grdrow.Cells[0].Text;
            lblProfitCent.Text = grdrow.Cells[1].Text;
            lblProfitCent.ForeColor = Color.Black;
            inputProfitCent.Focus();
        }
        protected void selectValClass_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputValClass.Text = grdrow.Cells[0].Text;
            lblValClass.Text = grdrow.Cells[2].Text;
            lblValClass.ForeColor = Color.Black;
            inputValClass.Focus();
        }

        //Save Code
        protected void Update_Click(object sender, EventArgs e)
        {
            if (ddPriceCtrl.SelectedValue == "")
            {
                MsgBox("Price Control cannot be empty!", this.Page, this);
                return;
            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            try
            {
                SqlCommand cmdUpdLogTrans = new SqlCommand("UPDATE Tbl_LogTrans SET TypeStatus=@TypeStatus WHERE LogID=@LogID AND TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmdUpdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TypeStatus",
                    Value = "End",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdUpdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdUpdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdUpdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdUpdLogTrans.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET ProfitCenter=@ProfitCenter, ValuationClass=@ValuationClass, PriceControl=@PriceControl, MovingPrice=@MovingPrice, StandardPrice=@StandardPrice, DoNotCost=@DoNotCost, WithQtyStructure=@WithQtyStructure, MaterialOrigin=@MaterialOrigin, CostingLotSize=@CostingLotSize, ValuationCtgry=@ValuationCtgry, NewFICO=@NewFICO, FxdPrice=@FxdPrice WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "FxdPrice",
                    Value = this.lblFxdP.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ValuationCtgry",
                    Value = this.inputValCat.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CostingLotSize",
                    Value = this.inputCostingLotSize.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "DoNotCost",
                    Value = this.lblDont.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "WithQtyStructure",
                    Value = this.lblWith.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialOrigin",
                    Value = this.lblMat.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "StandardPrice",
                    Value = this.inputMovPrice.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MovingPrice",
                    Value = this.inputMovPrice.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PriceControl",
                    Value = this.ddPriceCtrl.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "NewFICO",
                    Value = "x",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ValuationClass",
                    Value = this.inputValClass.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProfitCenter",
                    Value = inputProfitCent.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET FicoEnd = @FicoEnd WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = lblTransID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = lblMatID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "FicoEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdTracking.ExecuteNonQuery();
                conMatWorkFlow.Close();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Redirect("~/Pages/displayCreateMaterial");
        }
        protected void CancelUpd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/displayCreateMaterial");
        }
        protected void Approve_Click(object sender, EventArgs e)
        {
            if (lblRevision.Text == "")
            {
                try
                {
                    if (conMatWorkFlow.State == ConnectionState.Closed)
                    {
                        conMatWorkFlow.Open();
                    }

                    SqlCommand cmd = new SqlCommand("ficoApprove", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = this.inputMtrlID.Text,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = this.lblTransID.Text,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "FICOApproveBy",
                        Value = lblUser.Text,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "FICOApproveTime",
                        Value = DateTime.Now,
                        SqlDbType = SqlDbType.DateTime
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "LogID",
                        Value = this.lblLogID.Text,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();

                    conMatWorkFlow.Open();
                    SqlCommand cmdApprovedMaterial = new SqlCommand("UPDATE Tbl_Tracking SET MaterialApproved=@MaterialApproved WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                    cmdApprovedMaterial.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = this.inputMtrlID.Text,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdApprovedMaterial.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = this.lblTransID.Text,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdApprovedMaterial.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialApproved",
                        Value = "x",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 1
                    });
                    cmdApprovedMaterial.ExecuteNonQuery();
                    conMatWorkFlow.Close();

                    Response.Redirect("~/Pages/displayCreateMaterial");
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                }
            }
            else
            {
                MsgBox("There is a revision inprogress.", this.Page, this);
            }
        }
        protected void CancelApprove_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/displayCreateMaterial");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            if (ddPriceCtrl.SelectedValue == "")
            {
                MsgBox("Price Control cannot be empty!", this.Page, this);
                return;
            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            try
            {
                SqlCommand cmdLogTrans = new SqlCommand("saveFICO", conMatWorkFlow);
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.Session["Devisi"].ToString().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TypeStatus",
                    Value = "End",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.Session["Usnam"].ToString().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });

                //Parameter Tracking
                if (chkbxFxdPrice.Checked == true)
                {
                    stringFxdPrice = "X";
                    lblFxdP.Text = stringFxdPrice;
                }
                else
                {
                    stringFxdPrice = "";
                    lblFxdP.Text = stringFxdPrice;
                }
                if (chkbxDontCost.Checked == true)
                {
                    stringDontCost = "X";
                }
                else
                {
                    stringDontCost = "";
                }
                if (chkbxWithQtyStruct.Checked == true)
                {
                    stringWithQtyStruct = "X";
                }
                else
                {
                    stringWithQtyStruct = "";
                }
                if (chkbxMatOrigin.Checked == true)
                {
                    stringMatOrigin = "X";
                }
                else
                {
                    stringMatOrigin = "";
                }

                //Parameter Material
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ValuationCtgry",
                    Value = this.inputValCat.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CostingLotSize",
                    Value = this.inputCostingLotSize.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "FxdPrice",
                    Value = this.lblFxdP.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "DoNotCost",
                    Value = this.stringDontCost.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "WithQtyStructure",
                    Value = this.stringWithQtyStruct.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialOrigin",
                    Value = this.stringMatOrigin.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "StandardPrice",
                    Value = this.inputMovPrice.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MovingPrice",
                    Value = this.inputMovPrice.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PriceControl",
                    Value = this.ddPriceCtrl.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ValuationClass",
                    Value = this.inputValClass.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProfitCenter",
                    Value = inputProfitCent.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmdLogTrans.CommandType = CommandType.StoredProcedure;
                cmdLogTrans.ExecuteNonQuery();
                conMatWorkFlow.Close();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Redirect("~/Pages/displayCreateMaterial");
        }
        protected void Reject_Click(object sender, EventArgs e)
        {
            ficoTBL.Visible = false;
            rndTBL.Visible = false;
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("ficoReject", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "FicoEnd",
                Value = DBNull.Value,
                SqlDbType = SqlDbType.DateTime
            });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            Response.Redirect("~/Pages/displayCreateMaterial");
        }
        protected void CancelSave_Click(object sender, EventArgs e)
        {
            //if (conMatWorkFlow.State == ConnectionState.Closed)
            //{
            //    conMatWorkFlow.Open();
            //}
            //SqlCommand cmdTRCLogTrans = new SqlCommand("DELETE FROM Tbl_LogTrans WHERE LogID=@LogID", conMatWorkFlow);
            //cmdTRCLogTrans.Parameters.Add(new SqlParameter
            //{
            //    ParameterName = "LogID",
            //    Value = this.lblLogID.Text,
            //    SqlDbType = SqlDbType.NVarChar,
            //    Size = 10
            //});
            //cmdTRCLogTrans.ExecuteNonQuery();
            //conMatWorkFlow.Close();
            Response.Redirect("~/Pages/displayCreateMaterial");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/displayCreateMaterial");
        }
        protected void RevisionFico_Click(object sender, EventArgs e)
        {
            btnRevisionFico.Visible = false;
            lblRevision.Text = "X";
            MsgBox("Your revision has been sent.", this.Page, this);

            try
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                SqlCommand cmd = new SqlCommand("revision", conMatWorkFlow);
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
                    Value = lblMatID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "revision",
                    Value = "FICO",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RejectRevisionNotes",
                    Value = this.inputRejectReasonFICO.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 200
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                autoGenLogID();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            FICONavigationMenu.Items[0].Selected = true;
        }
        protected void RevisionRnD_Click(object sender, EventArgs e)
        {
            btnRevisionRnD.Visible = false;
            lblRevision.Text = "X";
            MsgBox("Your revision has been sent.", this.Page, this);

            try
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                SqlCommand cmd = new SqlCommand("revision", conMatWorkFlow);
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
                    Value = lblMatID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "revision",
                    Value = "RND",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RejectRevisionNotes",
                    Value = this.inputRejectReasonRnd.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 200
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                autoGenLogID();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            FICONavigationMenu.Items[1].Selected = true;
        }
        protected void RevisionProc_Click(object sender, EventArgs e)
        {
            btnRevisionProc.Visible = false;
            lblRevision.Text = "X";
            MsgBox("Your revision has been sent.", this.Page, this);

            try
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                SqlCommand cmd = new SqlCommand("revision", conMatWorkFlow);
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
                    Value = lblMatID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "revision",
                    Value = "PROC",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RejectRevisionNotes",
                    Value = this.inputRejectReasonProc.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 200
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                autoGenLogID();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            FICONavigationMenu.Items[2].Selected = true;
        }
        protected void RevisionPlanner_Click(object sender, EventArgs e)
        {
            btnRevisionPlanner.Visible = false;
            lblRevision.Text = "X";
            MsgBox("Your revision has been sent.", this.Page, this);

            try
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                SqlCommand cmd = new SqlCommand("revision", conMatWorkFlow);
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
                    Value = lblMatID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "revision",
                    Value = "PLANNER",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RejectRevisionNotes",
                    Value = this.inputRejectReasonPlanner.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 200
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                autoGenLogID();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            FICONavigationMenu.Items[3].Selected = true;
        }
        protected void RevisionQC_Click(object sender, EventArgs e)
        {
            btnRevisionQC.Visible = false;
            lblRevision.Text = "X";
            MsgBox("Your revision has been sent.", this.Page, this);

            try
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                SqlCommand cmd = new SqlCommand("revision", conMatWorkFlow);
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
                    Value = lblMatID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "revision",
                    Value = "QC",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RejectRevisionNotes",
                    Value = this.inputRejectReasonQC.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 200
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                autoGenLogID();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            FICONavigationMenu.Items[4].Selected = true;
        }
        protected void RevisionQA_Click(object sender, EventArgs e)
        {
            btnRevisionQA.Visible = false;
            lblRevision.Text = "X";
            MsgBox("Your revision has been sent.", this.Page, this);

            try
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                SqlCommand cmd = new SqlCommand("revision", conMatWorkFlow);
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
                    Value = lblMatID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "revision",
                    Value = "QA",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RejectRevisionNotes",
                    Value = this.inputRejectReasonQA.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 200
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                autoGenLogID();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            FICONavigationMenu.Items[5].Selected = true;
        }
        protected void RevisionQR_Click(object sender, EventArgs e)
        {
            btnRevisionQR.Visible = false;
            lblRevision.Text = "X";
            MsgBox("Your revision has been sent.", this.Page, this);

            try
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                SqlCommand cmd = new SqlCommand("revision", conMatWorkFlow);
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
                    Value = lblMatID.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "revision",
                    Value = "QR",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.lblPosition.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.lblUser.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RejectRevisionNotes",
                    Value = this.inputRejectReasonQR.Text.Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 200
                });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                autoGenLogID();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            FICONavigationMenu.Items[6].Selected = true;
        }

        //inputProfitCent_onBlur
        protected void inputProfitCent_onBlur(object sender, EventArgs e)
        {
            if (inputProfitCent.Text == "")
            {
                inputProfitCent.Text = "";
                lblProfitCent.Text = absProfitCent.Text;
                lblProfitCent.ForeColor = Color.Black;
                inputProfitCent.Focus();
            }
            else if (inputProfitCent.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("onblurProfitCenter", conMatWorkFlow);
                cmd.Parameters.Add("@ProfitCenter", SqlDbType.NVarChar).Value = this.inputProfitCent.Text;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblProfitCent.Text = dr["ProfitCenterDesc"].ToString();
                    lblProfitCent.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblProfitCent.Text = "Wrong Input!";
                    lblProfitCent.ForeColor = Color.Red;
                    inputProfitCent.Focus();
                    inputProfitCent.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputValClass_onBlur
        protected void inputValClass_onBlur(object sender, EventArgs e)
        {
            if (inputValClass.Text == "")
            {
                inputValClass.Text = "";
                lblValClass.Text = absProfitCent.Text;
                lblValClass.ForeColor = Color.Black;
                inputValClass.Focus();
            }
            else if (inputValClass.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("onblurValClass", conMatWorkFlow);
                cmd.Parameters.AddWithValue("ValuationClass", this.inputValClass.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblValClass.Text = dr["ValuationClassDesc"].ToString();
                    lblValClass.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblValClass.Text = "Wrong Input!";
                    lblValClass.ForeColor = Color.Red;
                    inputValClass.Focus();
                    inputValClass.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
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
        //checkbox FxdPrice Type
        protected void chkbxFxdPrice_CheckedChanged(object senders, EventArgs e)
        {
            if (chkbxFxdPrice.Checked == true)
            {
                stringFxdPrice = "X";
                lblFxdP.Text = stringFxdPrice.ToString();
            }
            else
            {
                stringFxdPrice = "";
                lblFxdP.Text = stringFxdPrice.ToString();
            }
        }
        //checkbox DontCost Type
        protected void chkbxDontCost_CheckedChanged(object senders, EventArgs e)
        {
            if (chkbxDontCost.Checked == true)
            {
                stringDontCost = "X";
                lblDont.Text = stringDontCost.ToString();
                lblDontCost.Text = "Active";
            }
            else
            {
                stringDontCost = "";
                lblDont.Text = stringDontCost.ToString();
                lblDontCost.Text = "Non Active";
            }
        }
        //checkbox With Qty Structure Type
        protected void chkbxWithQtyStruct_CheckedChanged(object senders, EventArgs e)
        {
            if (chkbxWithQtyStruct.Checked == true)
            {
                stringWithQtyStruct = "X";
                lblWith.Text = stringWithQtyStruct.ToString();
                lblWithQtyStruct.Text = "Active";
            }
            else
            {
                stringWithQtyStruct = "";
                lblWith.Text = stringWithQtyStruct.ToString();
                lblWithQtyStruct.Text = "Non Active";
            }
        }
        //checkbox Material Origin Type
        protected void chkbxMatOrigin_CheckedChanged(object senders, EventArgs e)
        {
            if (chkbxMatOrigin.Checked == true)
            {
                stringMatOrigin = "X";
                lblMat.Text = stringMatOrigin.ToString();
                lblMatOrigin.Text = "Active";
            }
            else
            {
                stringMatOrigin = "";
                lblMat.Text = stringMatOrigin.ToString();
                lblMatOrigin.Text = "Non Active";
            }
        }
        //Price Control Selected Change
        protected void ddPriceCtrl_SelectedChange(object senders, EventArgs e)
        {
            if (ddPriceCtrl.SelectedValue == "0")
            {
                MsgBox("Price Control cannot be empty!", this.Page, this);
                ddPriceCtrl.Focus();
            }
        }

        //rnd Manager
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
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "ProdSched",
                Value = this.inputProdSched.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 5
            });
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
                Value = this.inputMtrlID.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblProdSched.Text = dr["ProdSchedDesc"].ToString();
                lblProdSched.ForeColor = Color.Black;
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
        protected void bindLblMRPGrp()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblMRPGroup", conMatWorkFlow);
            cmd.Parameters.AddWithValue("MRPGroup", this.inputMatGr.Text);
            cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            cmd.Parameters.AddWithValue("MaterialID", this.inputMtrlID.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMRPGr.Text = dr["MRPGroupDesc"].ToString();
                lblMRPGr.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblPurcGrp()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblPurcGrp", conMatWorkFlow);
            cmd.Parameters.AddWithValue("PurchGrp", this.inputPurcGrp.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblPurcGrp.Text = dr["PurchGrpDesc"].ToString();
                lblPurcGrp.ForeColor = Color.Black;
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
        protected void bindLblValuationClass()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblValuationClass", conMatWorkFlow);
            cmd.Parameters.AddWithValue("ValuationClass", this.inputValClass.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblValClass.Text = dr["ValuationClassDesc"].ToString();
                lblValClass.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }
        protected void bindLblProfitCenter()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblProfitCenter", conMatWorkFlow);
            cmd.Parameters.AddWithValue("ProfitCenter", this.inputProfitCent.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblProfitCent.Text = dr["ProfitCenterDesc"].ToString();
                lblProfitCent.ForeColor = Color.Black;
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
            cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputBsUntMeas.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblBsUntMeas.Text = dr["UoM_Desc"].ToString();
                lblBsUntMeas.ForeColor = Color.Black;
                lblUoMRnD.Text = dr["UoM_Desc"].ToString();
                lblUoMRnD.ForeColor = Color.Black;
                lblNetWeightUnitRnd.Text = dr["UoM_Desc"].ToString();
                lblNetWeightUnitRnd.ForeColor = Color.Black;
                lblUoMProc.Text = dr["UoM_Desc"].ToString();
                lblUoMProc.ForeColor = Color.Black;
                lblNetWeightUnitProc.Text = dr["UoM_Desc"].ToString();
                lblNetWeightUnitProc.ForeColor = Color.Black;
                lblUoMPlanner.Text = dr["UoM_Desc"].ToString();
                lblUoMPlanner.ForeColor = Color.Black;
                lblUoMQC.Text = dr["UoM_Desc"].ToString();
                lblUoMQC.ForeColor = Color.Black;
                lblUoMQA.Text = dr["UoM_Desc"].ToString();
                lblUoMQA.ForeColor = Color.Black;
                lblUoMQR.Text = dr["UoM_Desc"].ToString();
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
                lblPckgMatProc.Text = dr["MatlGrpPack_Desc"].ToString();
                lblPckgMatProc.ForeColor = Color.Black;
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
            cmd.Parameters.AddWithValue("Type", this.inputType.Text);
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
            }
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            SqlCommand cmdP = new SqlCommand("bindLblStorLoc", conMatWorkFlow);
            cmdP.Parameters.AddWithValue("inputStorLoc", this.inputProdStorLoc.Text);
            cmdP.Parameters.AddWithValue("inputPlant", this.inputPlannerPlant.Text);
            cmdP.CommandType = CommandType.StoredProcedure;
            SqlDataReader drP = cmdP.ExecuteReader();
            while (drP.Read())
            {
                lblProdStorLoc.Text = drP["SLoc_Desc"].ToString();
                lblProdStorLoc.ForeColor = Color.Black;
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
                lblProcTypePlanner.Text = dr["ProcTypeDesc"].ToString();
                lblProcTypePlanner.ForeColor = Color.Black;
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
            cmd.Parameters.AddWithValue("SpclProcurement", this.inputSpcProcRnd.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblSpcProcRnd.Text = dr["SpclProcDesc"].ToString();
                lblSpcProcRnd.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            SqlCommand cmdP = new SqlCommand("bindLblSpecialProc", conMatWorkFlow);
            cmdP.Parameters.AddWithValue("SpclProcurement", this.inputSpcProc.Text);
            cmdP.CommandType = CommandType.StoredProcedure;
            SqlDataReader drP = cmdP.ExecuteReader();
            while (drP.Read())
            {
                lblSpcProc.Text = drP["SpclProcDesc"].ToString();
                lblSpcProc.ForeColor = Color.Black;
            }
            conMatWorkFlow.Close();
        }

        private void gridSearch(string Filter)
        {
            if (Filter.Trim().ToUpper() == "ALL")
            {
                if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
                {
                    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                    ViewState["Row"] = 0;
                    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                    string Row = ddlPageSize.SelectedValue;
                    var dt = GetData_srcListViewMatID("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D", lblPosition.Text.Trim(), "S");

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
                    var dt = GetData_srcListViewMatDESC("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D", lblPosition.Text.Trim(), "S");

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
                var dt = GetData_srcListViewRMSFFG("R&D", "S", lblPosition.Text.Trim(), "RM");

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
                var dt = GetData_srcListViewRMSFFG("R&D", "S", lblPosition.Text.Trim(), "SF");

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
                var dt = GetData_srcListViewRMSFFG("R&D", "S", lblPosition.Text.Trim(), "FG");

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
    }
}