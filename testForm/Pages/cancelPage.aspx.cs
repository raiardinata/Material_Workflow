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
    public partial class cancelPage : System.Web.UI.Page
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
                if (lblPosition.Text != "R&D" && lblPosition.Text != "R&D MGR" && lblPosition.Text != "PD" && lblPosition.Text != "PD MGR" && lblPosition.Text != "Admin")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
                srcListViewBinding();
            }
            else if(IsPostBack)
            {
                //srcListViewBinding();
            }
        }

        //Binding List View Code
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
            srcListViewBinding();
        }
        protected DataTable GetData_srcListViewCancelMgrMatID(string inputMatIDDESC)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewCancelMgrMatID", param);

            return dt;
        }
        protected DataTable GetData_srcListViewCancelMgrMatDESC(string inputMatIDDESC)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewCancelMgrMatDESC", param);

            return dt;
        }
        protected DataTable GetData_srcListViewCancelMatID(string inputMatIDDESC)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewCancelMatID", param);

            return dt;
        }
        protected DataTable GetData_srcListViewCancelMatDESC(string inputMatIDDESC)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewCancelMatDESC", param);

            return dt;
        }
        protected void srcListViewBinding()
        {
            gridSearch(inputMatIDDESC.Text);
        }
        protected void cancelThis_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string stringGlobalStatus = grdrow.Cells[8].Text.Trim();
            if (grdrow.Cells[6].Text == "&nbsp;" && lblPosition.Text != "R&D MGR")
            {
                cancelForm.Visible = true;
                listViewCancel.Visible = false;

                inputTransID.Text = grdrow.Cells[0].Text;
                inputMatID.Text = grdrow.Cells[1].Text;
                inputMaterialDesc.Text = grdrow.Cells[2].Text;
                inputPlant.Text = grdrow.Cells[3].Text;
                inputType.Text = grdrow.Cells[4].Text;
                inputMaterialType.Text = grdrow.Cells[5].Text;
            }
            else if( stringGlobalStatus.ToString().Trim() == "HOLD" && lblPosition.Text == "R&D MGR")
            {
                cancelForm.Visible = true;
                listViewCancel.Visible = false;
                btnSave.Visible = false;
                btnCancelSave.Visible = false;
                btnApprove.Visible = true;
                btnCancelApprove.Visible = true;

                inputTransID.Text = grdrow.Cells[0].Text;
                inputMatID.Text = grdrow.Cells[1].Text;
                inputMaterialDesc.Text = grdrow.Cells[2].Text;
                inputPlant.Text = grdrow.Cells[3].Text;
                inputType.Text = grdrow.Cells[4].Text;
                inputMaterialType.Text = grdrow.Cells[5].Text;
                inputRemark.Text = grdrow.Cells[7].Text;

                inputRemark.CssClass = "txtBoxRO";
                inputRemark.ReadOnly = true;
            }
        }
        protected void checkStatsNGlobStats_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indexGlobalStatus = GetColumnIndexByName(e.Row, "GlobalStatus");
                string columnGlobalStatus = e.Row.Cells[indexGlobalStatus].Text;
                if(columnGlobalStatus == "HOLD" && lblPosition.Text != "R&D MGR")
                {
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
            }
        }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("cancelMaterial", conMatWorkFlow);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "RemarkCancel",
                Value = this.inputRemark.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 200
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "CancelBy",
                Value = this.Session["Usnam"].ToString().ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.inputTransID.Text.ToUpper().Trim(),
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
            cmd.ExecuteNonQuery();
            conMatWorkFlow.Close();
            Response.Redirect("~/Pages/cancelPage");
        }
        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/cancelPage");
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("cancelApproveMaterial", conMatWorkFlow);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.inputTransID.Text.ToUpper().Trim(),
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
            cmd.ExecuteNonQuery();
            conMatWorkFlow.Close();
            Response.Redirect("~/Pages/cancelPage");
        }
        protected void btnCancelApprove_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("cancelRejectMaterial", conMatWorkFlow);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.inputTransID.Text.ToUpper().Trim(),
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
            cmd.ExecuteNonQuery();
            conMatWorkFlow.Close();
            Response.Redirect("~/Pages/cancelPage");
        }

        private void gridSearch(string Filter)
        {

            if (lblPosition.Text == "R&D MGR")
            {
                if (lstbxMatIDDESC.SelectedItem.Text == "Material ID")
                {
                    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                    ViewState["Row"] = 0;
                    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                    string Row = ddlPageSize.SelectedValue;

                    var dt = GetData_srcListViewCancelMgrMatID("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%");
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

                    var dt = GetData_srcListViewCancelMgrMatDESC("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%");
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
            else
            {
                if (lstbxMatIDDESC.SelectedItem.Text == "Material ID")
                {
                    GridViewListView.PageSize = int.Parse(ddlPageSize.SelectedValue);


                    ViewState["Row"] = 0;
                    string StartRow = (GridViewListView.PageIndex * GridViewListView.PageSize).ToString();
                    string Row = ddlPageSize.SelectedValue;

                    var dt = GetData_srcListViewCancelMatID("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%");
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

                    var dt = GetData_srcListViewCancelMatDESC("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%");
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
            gridSearch(inputMatIDDESC.Text);
        }

        protected void GridViewListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewListView.PageIndex = e.NewPageIndex;
            gridSearch(inputMatIDDESC.Text);
        }
    }
}