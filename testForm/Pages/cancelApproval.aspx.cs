using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testForm.Pages
{
    public partial class cancelApproval : System.Web.UI.Page
    {
        SQLConnect.SQLConnect sqlC = new SQLConnect.SQLConnect();
        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());
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
                if (lblPosition.Text != "FICO MGR" && lblPosition.Text != "Admin")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
                srcListViewBinding();
            }
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        //Binding List View Code Public
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
            srcListViewBinding();
        }
        protected DataTable GetData_srcListViewMatID(string inputMatIDDESC, string Module_User)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Module_User", Module_User);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatIDFICOCancelApproval", param);

            return dt;
        }
        protected DataTable GetData_srcListViewMatDESC(string inputMatIDDESC, string Module_User)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Module_User", Module_User);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatDESCFICOCancelApproval", param);

            return dt;
        }
        protected void srcListViewBinding()
        {
            //if (conMatWorkFlow.State == ConnectionState.Closed)
            //{
            //    conMatWorkFlow.Open();
            //}

            //if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
            //{
            //    var dt = new DataTable();
            //    dt = GetData_srcListViewMatID("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D");

            //    GridViewListView.DataSource = dt;
            //    GridViewListView.DataBind();
            //    conMatWorkFlow.Close();
            //}
            //else if (lsbxMatIDDESC.SelectedItem.Text == "Material Description")
            //{
            //    var dt = new DataTable();
            //    dt = GetData_srcListViewMatDESC("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D");

            //    GridViewListView.DataSource = dt;
            //    GridViewListView.DataBind();
            //    conMatWorkFlow.Close();
            //}
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
                string columnTransID = e.Row.Cells[indexTransID].Text;
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
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESCFico")).Visible = true;
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
        protected void modifyThisFico_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_CancelFinalApproval", conMatWorkFlow);
            cmd.Parameters.AddWithValue("TransID", grdrow.Cells[0].Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("MaterialID", grdrow.Cells[1].Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("UsrApp", this.lblUser.Text.Trim());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conMatWorkFlow.Close();

            srcListViewBinding();
            MsgBox("Youre cancel approval succeed.", this.Page, this);
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
                    var dt = GetData_srcListViewMatID("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D");

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
                    var dt = GetData_srcListViewMatDESC("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D");

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
            //else if (Filter.Trim().ToUpper() == "RM")
            //{
            //    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


            //    ViewState["Row"] = 0;
            //    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
            //    string Row = ddlPageSize.SelectedValue;
            //    var dt = GetData_srcListViewRM("R&D", "", lblPosition.Text.Trim());

            //    GridViewListView.DataSource = dt;
            //    GridViewListView.DataBind();


            //    if (dt.Rows.Count > 0)
            //    {
            //        var select = dt.Rows.Count;

            //        if (ViewState["Row"].ToString().Trim() == "0")
            //        {
            //            ViewState["grandtotal"] = select.ToString();
            //            lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
            //            // DivGrid.Visible = true;
            //            // lblNoData.Visible = false;
            //            ViewState["Row"] = 1;
            //            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
            //            lblTotalNumberOfPages.Text = pageCount.ToString();
            //            txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
            //            //  GridViewField.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        //  GridViewField.Visible = false;
            //        ViewState["grandtotal"] = 0;
            //        // DivGrid.Visible = false;
            //        //lblNoData.Visible = true;
            //    }
            //}
            //else if (Filter.Trim().ToUpper() == "SF")
            //{
            //    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


            //    ViewState["Row"] = 0;
            //    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
            //    string Row = ddlPageSize.SelectedValue;
            //    var dt = GetData_srcListViewSF("R&D", "", lblPosition.Text.Trim());

            //    GridViewListView.DataSource = dt;
            //    GridViewListView.DataBind();


            //    if (dt.Rows.Count > 0)
            //    {
            //        var select = dt.Rows.Count;

            //        if (ViewState["Row"].ToString().Trim() == "0")
            //        {
            //            ViewState["grandtotal"] = select.ToString();
            //            lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
            //            // DivGrid.Visible = true;
            //            // lblNoData.Visible = false;
            //            ViewState["Row"] = 1;
            //            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
            //            lblTotalNumberOfPages.Text = pageCount.ToString();
            //            txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
            //            //  GridViewField.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        //  GridViewField.Visible = false;
            //        ViewState["grandtotal"] = 0;
            //        // DivGrid.Visible = false;
            //        //lblNoData.Visible = true;
            //    }
            //}
            //else if (Filter.Trim().ToUpper() == "FG")
            //{
            //    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


            //    ViewState["Row"] = 0;
            //    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
            //    string Row = ddlPageSize.SelectedValue;
            //    var dt = GetData_srcListViewFG("R&D", "", lblPosition.Text.Trim());

            //    GridViewListView.DataSource = dt;
            //    GridViewListView.DataBind();


            //    if (dt.Rows.Count > 0)
            //    {
            //        var select = dt.Rows.Count;

            //        if (ViewState["Row"].ToString().Trim() == "0")
            //        {
            //            ViewState["grandtotal"] = select.ToString();
            //            lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
            //            // DivGrid.Visible = true;
            //            // lblNoData.Visible = false;
            //            ViewState["Row"] = 1;
            //            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
            //            lblTotalNumberOfPages.Text = pageCount.ToString();
            //            txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
            //            //  GridViewField.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        //  GridViewField.Visible = false;
            //        ViewState["grandtotal"] = 0;
            //        // DivGrid.Visible = false;
            //        //lblNoData.Visible = true;
            //    }
            //}
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