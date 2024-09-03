using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testForm.Pages
{
    public partial class qaPage : System.Web.UI.Page
    {
        SQLConnect.SQLConnect sqlC = new SQLConnect.SQLConnect();
        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            //Bind Other Data Repeater
            if (!IsPostBack)
            {
                if (lblPosition.Text == "Admin")
                {
                    srcListViewBindingAdmin();
                }
                else
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
                    if (lblPosition.Text != "QA")
                    {
                        Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                        return;
                    }
                    srcListViewBinding();
                }
            }
            else if(IsPostBack)
            {
                if (lblPosition.Text == "Admin")
                {
                    //srcListViewBindingAdmin();
                }
                else
                {
                    //srcListViewBinding();
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
        protected DataTable GetData_srcListViewRMSFFG(string Plant, string Division, string Type)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Plant", Plant);
            param[1] = new SqlParameter("@Division", Division);
            param[2] = new SqlParameter("@Type", Type);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewRMSFFGByPlant", param);

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

        //Binding List View Code
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
            srcListViewBinding();
        }
        protected DataTable GetData_srcListViewMatID(string inputMatIDDESC, string Plant, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Plant", Plant);
            param[2] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatIDByPlant", param);

            return dt;
        }
        protected DataTable GetData_srcListViewMatDESC(string inputMatIDDESC, string Plant, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@Plant", Plant);
            param[2] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatDESCByPlant", param);

            return dt;
        }
        protected void srcListViewBinding()
        {
            FilterSearch.Text = "ALL";
            gridSearch(FilterSearch.Text);
        }
        protected DataTable GetData_srcListViewMatIDAdmin(string inputMatIDDESC, string InitiateBy)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@InitiateBy", InitiateBy);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatIDDeptConst", param);

            return dt;
        }
        protected DataTable GetData_srcListViewMatDESCAdmin(string inputMatIDDESC, string InitiateBy)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@InitiateBy", InitiateBy);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatDESCDeptConst", param);

            return dt;
        }
        protected void srcListViewBindingAdmin()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            if (lsbxMatIDDESC.SelectedItem.Text == "Material ID")
            {
                var dt = new DataTable();
                dt = GetData_srcListViewMatIDAdmin("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D");

                GridViewListView.DataSource = dt;
                GridViewListView.DataBind();
                conMatWorkFlow.Close();
            }
            else if (lsbxMatIDDESC.SelectedItem.Text == "Material Description")
            {
                var dt = new DataTable();
                dt = GetData_srcListViewMatDESCAdmin("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", "R&D");

                GridViewListView.DataSource = dt;
                GridViewListView.DataBind();
                conMatWorkFlow.Close();
            }
        }
        protected void modifyThis_Click(object sender, EventArgs e)
        {
            srcStoreCondModalBinding();
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

            if (lblPosition.Text == "QA")
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
                        if (dr["NewQA"].ToString().Trim() == "")
                        {
                            listViewQA.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            qmQualityControllDt.Visible = true;

                            btnSave.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                        }
                        //Update Data
                        else
                        {
                            listViewQA.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            qmQualityControllDt.Visible = true;

                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpdate.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                            inputStoreCond.Text = dr["StorConditions"].ToString().Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                    return;
                }
            }
            else if(lblPosition.Text == "Admin")
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
                        if (dr["NewQA"].ToString().Trim() == "")
                        {
                            listViewQA.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            qmQualityControllDt.Visible = true;

                            btnSave.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                        }
                        //Update Data
                        else
                        {
                            listViewQA.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            qmQualityControllDt.Visible = true;

                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpdate.Visible = true;

                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                            inputStoreCond.Text = dr["StorConditions"].ToString().Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                    return;
                }
            }
            else
            {
                MsgBox(this.lblUser.Text + " username are not for QA division", this.Page, this);
                Response.Redirect("~/Pages/qaPage");
            }            
            conMatWorkFlow.Close();

            bindLblBsUntMeas();
            bindLblStorCondition();
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

            imgBtnStorCond.Visible = false;

            inputType.ReadOnly = true;
            inputMatTyp.ReadOnly = true;
            inputMtrlID.ReadOnly = true;
            inputMtrlDesc.ReadOnly = true;
            inputBsUntMeas.ReadOnly = true;
            inputOldMtrlNum.ReadOnly = true;
            inputPlant.ReadOnly = true;
            inputStoreCond.ReadOnly = true;

            inputType.CssClass = "txtBoxRO";
            inputMatTyp.CssClass = "txtBoxRO";
            inputMtrlID.CssClass = "txtBoxRO";
            inputMtrlDesc.CssClass = "txtBoxRO";
            inputBsUntMeas.CssClass = "txtBoxRO";
            inputOldMtrlNum.CssClass = "txtBoxRO";
            inputPlant.CssClass = "txtBoxRO";
            inputStoreCond.CssClass = "txtBoxRO";

            inputType.Attributes.Add("placeholder", "");
            inputMatTyp.Attributes.Add("placeholder", "");
            inputMtrlID.Attributes.Add("placeholder", "");
            inputMtrlDesc.Attributes.Add("placeholder", "");
            inputBsUntMeas.Attributes.Add("placeholder", "");
            inputOldMtrlNum.Attributes.Add("placeholder", "");
            inputPlant.Attributes.Add("placeholder", "");
            inputStoreCond.Attributes.Add("placeholder", "");

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            
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
                listViewQA.Visible = false;

                rmContent.Visible = true;
                bscDt1GnrlDt.Visible = true;
                qmQualityControllDt.Visible = true;

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnClose.Visible = true;

                inputType.Text = dr["Type"].ToString().Trim();
                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                lblTransID.Text = dr["TransID"].ToString().Trim();
                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                inputPlant.Text = dr["Plant"].ToString().Trim();
                inputStoreCond.Text = dr["StorConditions"].ToString().Trim();
            }
            conMatWorkFlow.Close();
            bindLblBsUntMeas();
            bindLblStorCondition();
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
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                else if (columnRDEnd == "&nbsp;" && columnStatus == "REVISIONRND")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                else if (columnRDEnd == "&nbsp;" && columnStatus == "REVISIONRNDAPPROVAL")
                {
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                else if (columnRDEnd != "&nbsp;")
                {
                    if (columnQAEnd == "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Open for QA";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = true;

                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotesQR = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQR;
                        }
                        if (columnQCEnd != "&nbsp;")
                        {
                            string lblNotesQC = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQC;
                        }
                        if (columnPlanEnd != "&nbsp;")
                        {
                            string lblNotesPlan = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesPlan;
                        }
                        if (columnProcEnd != "&nbsp;")
                        {
                            string lblNotesProc = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesProc;
                        }
                    }
                    else if (columnQAEnd != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Closed for QA";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;

                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotesQR = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQR;
                        }
                        if (columnQCEnd != "&nbsp;")
                        {
                            string lblNotesQC = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQC;
                        }
                        if (columnPlanEnd != "&nbsp;")
                        {
                            string lblNotesPlan = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesPlan;
                        }
                        if (columnProcEnd != "&nbsp;")
                        {
                            string lblNotesProc = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesProc;
                        }
                    }                    

                    if (columnFicoStart != "&nbsp;" && columnFicoEnd == "&nbsp;" && columnQREnd != "&nbsp;" && columnQAEnd != "&nbsp;" && columnQCEnd != "&nbsp;" && columnPlanEnd != "&nbsp;" && columnProcEnd != "&nbsp;" && columnRDEnd != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Open";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                        string lblNotesIn = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                    }
                    else if (columnFicoStart != "&nbsp;" && columnFicoEnd != "&nbsp;" && columnQREnd != "&nbsp;" && columnQAEnd != "&nbsp;" && columnQCEnd != "&nbsp;" && columnPlanEnd != "&nbsp;" && columnProcEnd != "&nbsp;" && columnRDEnd != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "FICO";
                        string lblNotesIn = "";
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotesIn;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        if (columnMaterialApproved == "x")
                        {
                            ((Label)e.Row.FindControl("lblStatus")).Text = "Closed";
                            ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Queue";
                            string lblNotesIOn = "";
                            ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotesIOn;
                            ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                            if (columnGlobalStatus == "Completed")
                            {
                                ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Completed";
                            }
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
                    ((Label)e.Row.FindControl("lblStatus")).Text = "Failed";
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
        protected void srcStoreCondModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_StorCondition"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewStoreCond.DataSource = ds.Tables[0];
            GridViewStoreCond.DataBind();
            conMatWorkFlow.Close();
        }
        //Modal
        protected void selectStoreCond_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string PlantID = grdrow.Cells[0].Text;
            string PlantName = grdrow.Cells[1].Text;
            inputStoreCond.Text = grdrow.Cells[0].Text;
            lblStoreCond.Text = grdrow.Cells[1].Text;
            lblStoreCond.ForeColor = Color.Black;
            inputStoreCond.Focus();
        }

        //Save Code
        protected void Update_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            if (inputOldMtrlNum.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "confirm", "confirmFunction();", true);
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
                SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET OldMatNumb=@OldMatNumb, StorConditions=@StorConditions WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "OldMatNumb",
                    Value = this.inputOldMtrlNum.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
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
                    ParameterName = "StorConditions",
                    Value = this.inputStoreCond.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ModifiedBy",
                    Value = this.lblUser.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ModifiedTime",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET QAEnd = @QAEnd WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
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
                    Value = inputMtrlID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "QAEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdTracking.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdCheckFico = new SqlCommand("checkFicoStart", conMatWorkFlow);
                cmdCheckFico.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdCheckFico.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdCheckFico.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Revision",
                    Value = "X",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdCheckFico.CommandType = CommandType.StoredProcedure;
                cmdCheckFico.ExecuteNonQuery();
                conMatWorkFlow.Close();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Redirect("~/Pages/qaPage");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            if (inputStoreCond.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_StorCondition WHERE StorConditions = @inputStoreCond";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputStoreCond", this.inputStoreCond.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblStoreCond.Text = dr["StorConditionsDesc"].ToString();
                    lblStoreCond.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblStoreCond.Text = "Wrong Input!";
                    lblStoreCond.ForeColor = Color.Red;
                    inputStoreCond.Focus();
                    inputStoreCond.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                    return;
                }
                conMatWorkFlow.Close();
            }

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "No")
            {
                return;
            }
            else
            {

            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            try
            {
                SqlCommand cmdLogTrans = new SqlCommand("INSERT INTO Tbl_LogTrans (TransID, MaterialID, Module_User, TypeStatus, Usnam, time, LogID) VALUES (@TransID, @MaterialID, @Module_User, @TypeStatus, @Usnam, @time, @LogID)", conMatWorkFlow);
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
                    Value = this.Session["Devisi"].ToString().Trim().ToUpper(),
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
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "time",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdLogTrans.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET QAEnd = @QAEnd WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "QAEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdTracking.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET OldMatNumb=@OldMatNumb, StorConditions=@StorConditions, NewQA=@NewQA WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "OldMatNumb",
                    Value = this.inputOldMtrlNum.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "NewQA",
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
                    ParameterName = "StorConditions",
                    Value = this.inputStoreCond.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdCheckFico = new SqlCommand("checkFicoStart", conMatWorkFlow);
                cmdCheckFico.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdCheckFico.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdCheckFico.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Revision",
                    Value = "",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdCheckFico.CommandType = CommandType.StoredProcedure;
                cmdCheckFico.ExecuteNonQuery();
                conMatWorkFlow.Close();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Redirect("~/Pages/qaPage");
        }
        protected void Reject_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/qaPage");
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            /*conMatWorkFlow.Open();
            SqlCommand cmdTRCLogTrans = new SqlCommand("DELETE FROM Tbl_LogTrans WHERE LogID=@LogID", conMatWorkFlow);
            cmdTRCLogTrans.Parameters.Add(new SqlParameter
            {
                ParameterName = "LogID",
                Value = this.lblLogID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdTRCLogTrans.ExecuteNonQuery();
            conMatWorkFlow.Close();*/
            Response.Redirect("~/Pages/qaPage");
        }
        protected void CancelUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/qaPage");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/qaPage");
        }

        //inputStoreCond_onBlur
        protected void inputStoreCond_onBlur(object sender, EventArgs e)
        {
            if (inputStoreCond.Text == "")
            {
                lblStoreCond.Text = absStoreCond.Text;
                lblStoreCond.ForeColor = Color.Black;
                inputStoreCond.Focus();
            }
            else if (inputStoreCond.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_StorCondition WHERE StorConditions = @inputStoreCond";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputStoreCond", this.inputStoreCond.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblStoreCond.Text = dr["StorConditionsDesc"].ToString();
                    lblStoreCond.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblStoreCond.Text = "Wrong Input!";
                    lblStoreCond.ForeColor = Color.Red;
                    inputStoreCond.Focus();
                    inputStoreCond.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
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
                    var dt = GetData_srcListViewMatID("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", Session["UsnamPlant"].ToString(), lblPosition.Text.Trim());

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
                    var dt = GetData_srcListViewMatDESC("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", Session["UsnamPlant"].ToString(), lblPosition.Text.Trim());

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
                var dt = GetData_srcListViewRMSFFG(Session["UsnamPlant"].ToString(), lblPosition.Text.Trim(), "RM");

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
                var dt = GetData_srcListViewRMSFFG(Session["UsnamPlant"].ToString(), lblPosition.Text.Trim(), "SF");

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
                var dt = GetData_srcListViewRMSFFG(Session["UsnamPlant"].ToString(), lblPosition.Text.Trim(), "FG");

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