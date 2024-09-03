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
    public partial class CommonTable : System.Web.UI.Page
    {
        SQLConnect.SQLConnect sqlC = new SQLConnect.SQLConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty((String)Session["Usnam"]))
                {
                    Response.Redirect("~/Pages/loginPage");
                }
                else
                {
                    lblUser.Text = Session["Usnam"].ToString().Trim();
                    lblPosition.Text = Session["Devisi"].ToString().Trim();
                }
               // if (lblPosition.Text != "R&D" && lblPosition.Text != "R&D MGR" && lblPosition.Text != "PD" && lblPosition.Text != "PD MGR")
                 if(lblPosition.Text != "Admin")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
                // CommonTable_GetDataAll();

                GridViewListViewSearch(this.inputProgramFunctionID.Text);
            }
        }

        protected void GridViewListViewViewListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Checking the RowType of the Row  
           
        }

        private void GridViewListViewSearch(string Filter)
        {
            GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);
            
            ViewState["Row"] = 0;
            string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
            string Row = ddlPageSize.SelectedValue;

            DataTable dt = CommonTable_GetDataAlls(Filter , StartRow , Row);

            GridViewListView.DataSource = dt;
            GridViewListView.DataBind();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (ViewState["Row"].ToString().Trim() == "0")
                    {
                        ViewState["grandtotal"] = dr["TotalRow"].ToString();
                        lblTotalRecords.Text = String.Format("Total Records : {0}", ViewState["grandtotal"]);
                      
                        ViewState["Row"] = 1;
                        int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ViewState["grandtotal"]) / GridViewListView.PageSize));
                        lblTotalNumberOfPages.Text = pageCount.ToString();
                        txtGoToPage.Text = (GridViewListView.PageIndex + 1).ToString();
                        //  GridViewListViewViewField.Visible = true;
                    }
                }

            }
            else
            {
                //  GridViewListViewViewField.Visible = false;
                ViewState["grandtotal"] = 0;
               
            }
        }

        private DataTable CommonTable_GetDataAlls(string Filter , string StartRow, string Row)
        {

            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            
            param[0] = new SqlParameter("@inputProgramFunctionID", "%" + Filter + "%");
            param[1] = new SqlParameter("@TypeUser", lblPosition.Text);
            param[2] = new SqlParameter("@StartRow", StartRow);
            param[3] = new SqlParameter("@Row", Row);
            //send param to SP sql
            dt = sqlC.ExecuteDataTable("CommonTable_GetDataAll", param);

            return dt;
        }

        private void CommonTable_GetDataAll()
        {
            
            SqlParameter[] param = new SqlParameter[2];
            DataTable dt = new DataTable();
            param[0] = new SqlParameter("@inputProgramFunctionID", "%" + this.inputProgramFunctionID.Text + "%");
            param[1] = new SqlParameter("@TypeUser", lblPosition.Text);
           
            //send param to SP sql
            dt = sqlC.ExecuteDataTable("CommonTable_GetDataAll", param);

            GridViewListView.DataSource = dt;
            GridViewListView.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommonTable", "$('#modalCommonTable').modal();", true);

            //SearchPurchasingGroup("");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void GridViewListView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edittttt")
            {
                string[] CommonTable = e.CommandArgument.ToString().Split(',');

                legend1.InnerText = "Form Edit";
                txtProgramID.Text = CommonTable[0];
                txtFunctionID.Text = CommonTable[1];
                txtSeqNo.Text = CommonTable[2];
                txtProgramID.ReadOnly = true;
                txtProgramID.Enabled = false;

                txtFunctionID.ReadOnly = true;
                txtFunctionID.Enabled = false;
                txtSeqNo.ReadOnly = true;
                txtSeqNo.Enabled = false;
                txtOption.Text = CommonTable[3];
                txtLow.Text = CommonTable[4];
                txtHigh.Text = CommonTable[5];

                btnSave.Text = "Update";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommonTable", "$('#modalCommonTable').modal();", true);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalSpcProc", "$('#modalSpcProc').modal('hide');", true);

            }
            //else if (e.CommandName == "Deleteeee")
            //{
            //    //Process Delete

            //}
        }

        

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            legend1.InnerText = "Form Insert";
            txtProgramID.Text = "";
            txtProgramID.Enabled = true;
            txtProgramID.ReadOnly = false;

            txtFunctionID.Text = "";
            txtFunctionID.Enabled = true;
            txtFunctionID.ReadOnly = false;

            txtSeqNo.Text = "";
            txtSeqNo.Enabled = true;
            txtSeqNo.ReadOnly = false;

            txtOption.Text = "";
            txtLow.Text = "";
            txtHigh.Text = "";

            btnSave.Text = "Insert";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommonTable", "$('#modalCommonTable').modal();", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Insert")
            {
                // , HttpContext.Current.Session["ActiveUser"].ToString().ToLower() == "administrator" ? ddlCompany.SelectedValue : lblCompanyID.Text, txtPlantID.Text, txtWarehouseID.Text, HttpContext.Current.Session["ActiveUser"].ToString()
                //Process Insert
                try
                {


                    if (CommonTableInsert(txtProgramID.Text, txtFunctionID.Text, txtSeqNo.Text, txtOption.Text, txtLow.Text, txtHigh.Text) > 0)
                    {
                        showMessage("Insert Success");

                        //CommonTable_GetDataAlls("","0","20");
                        GridViewListViewSearch(this.inputProgramFunctionID.Text);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommonTable", "$('#modalCommonTable').modal('hide');", true);


                    }
                }
                catch (Exception eex)
                {
                    showMessage("Wrong Input Value");
                }
            }
            else
            {
                if (CommonTableUpdate(txtProgramID.Text, txtFunctionID.Text, txtSeqNo.Text, txtOption.Text, txtLow.Text, txtHigh.Text) > 0)
                {
                    showMessage("Update Success");

                    //CommonTable_GetDataAlls("","0","20");
                    GridViewListViewSearch(this.inputProgramFunctionID.Text);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCommonTable", "$('#modalCommonTable').modal('hide');", true);


                }
            }
        }

        protected void showMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('" + message + "');",
                    true);
        }

        private int CommonTableInsert(string ProgramID, string FunctionID, string SeqNo, string Option, string Low, string High)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ProgramID", ProgramID);
            param[1] = new SqlParameter("@FunctionID", FunctionID);
            param[2] = new SqlParameter("@SeqNo", SeqNo);
            param[3] = new SqlParameter("@Option", Option);
            param[4] = new SqlParameter("@LOW", Low);
            param[5] = new SqlParameter("@HIGH", High);
           
            return sqlC.ExecuteNonQuery("CommonTable_Insert", param);
        }

        private int CommonTableUpdate(string ProgramID, string FunctionID, string SeqNo, string Option, string Low, string High)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ProgramID", ProgramID);
            param[1] = new SqlParameter("@FunctionID", FunctionID);
            param[2] = new SqlParameter("@SeqNo", SeqNo);
            param[3] = new SqlParameter("@Option", Option);
            param[4] = new SqlParameter("@LOW", Low);
            param[5] = new SqlParameter("@HIGH", High);

            return sqlC.ExecuteNonQuery("CommonTable_Update", param);
        }

        protected void searchBtn_Click(object sender, ImageClickEventArgs e)
        {
           //DataTable dt = CommonTable_GetDataAlls(inputProgramFunctionID.Text, "0", ddlPageSize.SelectedValue);

           // GridViewListView.DataSource = dt;
           // GridViewListView.DataBind();

            GridViewListViewSearch(inputProgramFunctionID.Text);
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);
            GridViewListView.PageIndex = 0;
            GridViewListViewSearch(inputProgramFunctionID.Text);
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

       

        protected void GridViewListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewListView.PageIndex = e.NewPageIndex;
            GridViewListViewSearch(inputProgramFunctionID.Text);
        }
    }
}