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
    public partial class qcPage : System.Web.UI.Page
    {
        string stringInspectSet = "0";
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
                    if (lblPosition.Text != "QC")
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
                    //srcClassTypModalBinding();
                    //srcClassModalBinding();
                }
                else
                {
                    //srcListViewBinding();
                    //srcClassTypModalBinding();
                    //srcClassModalBinding();
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
        protected DataTable GetLastLineClassType(string TransID, string MaterialID)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TransID", TransID);
            param[1] = new SqlParameter("@MaterialID", MaterialID);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("[Tbl_QCClass_GetLine]", param);

            return dt;
        }
        protected void autoGenLineClassType()
        {
            string num1 = "";
            DataTable dt = new DataTable();
            dt = GetLastLineClassType(lblTransID.Text.Trim(), inputMtrlID.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                num1 = dt.Rows[0]["Line"].ToString();
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLineClassType.Text = num1;
            }
            else
            {
                num1 = lblLineClassType.Text;
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLineClassType.Text = num1;
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
        protected void autoGenLineInspectType()
        {
            string num1 = "";
            DataTable dt = new DataTable();
            dt = GetLastLineInspectType(lblTransID.Text.Trim(), inputMtrlID.Text.Trim());
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

            if (lblPosition.Text == "QC")
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
                        if (dr["NewQC"].ToString().Trim() == "")
                        {
                            listViewQC.Visible = false;

                            rmContent.Visible = true;

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
                            listViewQC.Visible = false;

                            rmContent.Visible = true;

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
                        if (dr["NewQC"].ToString().Trim() == "")
                        {
                            listViewQC.Visible = false;

                            rmContent.Visible = true;

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
                            listViewQC.Visible = false;

                            rmContent.Visible = true;

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
                MsgBox(this.lblUser.Text + " username are not for QC division", this.Page, this);
                Response.Redirect("~/Pages/qcPage");
            }            
            conMatWorkFlow.Close();
            ClassBindRepeater();
            QCDataBindRepeater();
            //srcClassTypModalBinding();
            srcClassModalBinding();
            srcInspectionTypeModalBinding();
            bindLblBsUntMeas();

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            // doing checking
            DataTable dtCheck = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
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

            tblInspectType.Visible = false;
            tblClass.Visible = false;
            rptClassType.Visible = false;
            rptInspectionType.Visible = false;

            rptClassTypeDisplay.Visible = true;
            rptInspectionTypeDisplay.Visible = true;

            inputType.ReadOnly = true;
            inputMatTyp.ReadOnly = true;
            inputMtrlID.ReadOnly = true;
            inputMtrlDesc.ReadOnly = true;
            inputBsUntMeas.ReadOnly = true;
            inputOldMtrlNum.ReadOnly = true;
            inputPlant.ReadOnly = true;
            inputInspectIntrv.ReadOnly = true;

            inputType.CssClass = "txtBoxRO";
            inputMatTyp.CssClass = "txtBoxRO";
            inputMtrlID.CssClass = "txtBoxRO";
            inputMtrlDesc.CssClass = "txtBoxRO";
            inputBsUntMeas.CssClass = "txtBoxRO";
            inputOldMtrlNum.CssClass = "txtBoxRO";
            inputPlant.CssClass = "txtBoxRO";
            inputInspectIntrv.CssClass = "txtBoxRO";

            inputType.Attributes.Add("placeholder", "");
            inputMatTyp.Attributes.Add("placeholder", "");
            inputMtrlID.Attributes.Add("placeholder", "");
            inputMtrlDesc.Attributes.Add("placeholder", "");
            inputBsUntMeas.Attributes.Add("placeholder", "");
            inputOldMtrlNum.Attributes.Add("placeholder", "");
            inputPlant.Attributes.Add("placeholder", "");
            inputInspectIntrv.Attributes.Add("placeholder", "");

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
                listViewQC.Visible = false;

                rmContent.Visible = true;

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnClose.Visible = true;

                chkbxInspectSet.Enabled = false;
                inputType.Text = dr["Type"].ToString().Trim();
                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                lblTransID.Text = dr["TransID"].ToString().Trim();
                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                inputPlant.Text = dr["Plant"].ToString().Trim();
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
            }
            conMatWorkFlow.Close();
            ClassBindRepeater();
            QCDataBindRepeater();
            bindLblBsUntMeas();
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
                    if (columnQCEnd == "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Open for QC";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotesQR = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQR;
                        }
                        if (columnQAEnd != "&nbsp;")
                        {
                            string lblNotesQA = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQA;
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
                    else if (columnQCEnd != "&nbsp;" && columnQCStart != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Closed for QC";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotesQR = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQR;
                        }
                        if (columnQAEnd != "&nbsp;")
                        {
                            string lblNotesQA = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQA;
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

        //CLASSNTYP Code
        protected void btnAddClassnTyp_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            // doing checking
            DataTable dtCheckA = new DataTable();
            SqlParameter[] paramA = new SqlParameter[2];
            paramA[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
            paramA[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

            //send paramA to SP sql
            dtCheckA = sqlC.ExecuteDataTable("CheckClassType", paramA);

            if (dtCheckA.Rows.Count > 0)
            {
                // munculkan pesan bahwa sudah ada
                MsgBox("There is already 023 Class Type in the table!", this.Page, this);
                return;
            }
            conMatWorkFlow.Close();

            if (inputClass.Text.Trim() == "" || inputClassTyp.Text.Trim() == "")
            {
                MsgBox("Class Type or Class is empty, cannot add empty data.", this.Page, this);
                return;
            }
            else
            {
                conMatWorkFlow.Open();
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
                param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                param[2] = new SqlParameter("@Class", this.inputClass.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("CheckClass", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your Class " + this.inputClass.Text + " is already Exist or did not match Classification master data!", this.Page, this);
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_QCCLASS(TransID, MaterialID, Line, ClassType, ClassNo, CreateBy, CreateTime, New) VALUES(@TransID, @MaterialID, @Line, @ClassType, @ClassNo, @CreateBy, @CreateTime, @New)", conMatWorkFlow);
                    cmd.Parameters.AddWithValue("@TransID", lblTransID.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@MaterialID", inputMtrlID.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@Line", lblLineClassType.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@ClassType", inputClassTyp.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@ClassNo", inputClass.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@CreateBy", lblUser.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@CreateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@New", "");

                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                    Clear_Controls();
                    ClassBindRepeater();
                }
            }
            autoGenLineClassType();
        }
        private void Clear_Controls()
        {
            inputClass.Text = string.Empty;
            inputClassTyp.Focus();
        }
        protected void ClassBindRepeater()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * from Tbl_QCClass WHERE TransID = @TransID and MaterialID = @MaterialID and ClassType <>'001' and ClassType<>'300'", conMatWorkFlow);
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

            conMatWorkFlow.Open();
            SqlCommand cmdMgr = new SqlCommand("Select * from Tbl_QCClass WHERE TransID = @TransID and MaterialID = @MaterialID", conMatWorkFlow);
            cmdMgr.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdMgr.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet dsMgr = new DataSet();
            SqlDataAdapter adpMgr = new SqlDataAdapter(cmdMgr);
            adpMgr.Fill(dsMgr);
            rptClassTypeDisplay.DataSource = dsMgr;
            rptClassTypeDisplay.DataBind();
            conMatWorkFlow.Close();
        }
        protected void reptClassType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblClassTypTbl")).Visible = false;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblClassTbl")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtClassTyp")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtClass")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkEdit")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkDelete")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkUpdate")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkCancel")).Visible = true;
            }
            if (e.CommandName == "update")
            {
                string Ct = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtClassTyp")).Text.ToUpper().Trim();
                string Cs = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtClass")).Text.ToUpper().Trim();
                string ID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIDClassnTypTbl")).Text.ToUpper().Trim();
                SqlDataAdapter adp = new SqlDataAdapter("Update Tbl_QCClass set ClassType= @ClassType, ClassNo=@ClassNo where IDClassType = @IDClassType", conMatWorkFlow);
                adp.SelectCommand.Parameters.AddWithValue("@ClassTyp", Ct);
                adp.SelectCommand.Parameters.AddWithValue("@ClassNo", Cs);
                adp.SelectCommand.Parameters.AddWithValue("@IDClassType", ID);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ClassBindRepeater();
            }
            if (e.CommandName == "delete")
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                string ID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIDClassnTypTbl")).Text;
                SqlCommand cmd = new SqlCommand("delete from Tbl_QCClass where IDClassType = @IDClassType", conMatWorkFlow);
                cmd.Parameters.AddWithValue("@IDClassType", ID);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                ClassBindRepeater();
            }
            if (e.CommandName == "cancel")
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblClassTypTbl")).Visible = true;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblClassTbl")).Visible = true;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtClassTyp")).Visible = false;
                ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtClass")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkEdit")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkDelete")).Visible = true;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkUpdate")).Visible = false;
                ((System.Web.UI.WebControls.Button)e.Item.FindControl("lnkCancel")).Visible = false;
            }
        }

        //InspectionType Code
        protected void btnAddInspectionType_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (txtInspectionType.Text == "")
            {
                MsgBox("Inspection Type is empty, cannot add empty data.", this.Page, this);
                return;
            }
            else
            {
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
                param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                param[2] = new SqlParameter("@InspectionType", this.txtInspectionType.Text.Trim().ToUpper()); 

                 //send param to SP sql
                 dtCheck = sqlC.ExecuteDataTable("CheckInspectionType", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your Inspection Type " + this.txtInspectionType.Text + " is already Exist or did not match Inspection Type master data!", this.Page, this);
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_QCData(TransID, MaterialID, Line, InspType, CreateBy, CreateTime, New) VALUES(@TransID, @MaterialID, @Line, @InspType, @CreateBy, @CreateTime, @New)", conMatWorkFlow);
                    cmd.Parameters.AddWithValue("@TransID", lblTransID.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@MaterialID", inputMtrlID.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@Line", lblLineInspectionType.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@InspType", txtInspectionType.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@CreateBy", lblUser.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@CreateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@New", "");

                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                    Clear_ControlsQCData();
                    QCDataBindRepeater();
                    chkbxInspectSet.Checked = true;
                    lblInspectSet.Text = "Active";
                }
            }
            srcInspectionTypeModalBinding();
            autoGenLineInspectType();
        }
        private void Clear_ControlsQCData()
        {
            txtInspectionType.Text = string.Empty;
            txtInspectionType.Focus();
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
                Value = inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            rptInspectionType.DataSource = ds;
            rptInspectionType.DataBind();
            rptInspectionTypeDisplay.DataSource = ds;
            rptInspectionTypeDisplay.DataBind();
            conMatWorkFlow.Close();
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
                param[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
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

        //LightBox Data Code
        protected void srcClassTypModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_ClassType"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewClassTyp.DataSource = ds.Tables[0];
            GridViewClassTyp.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcClassModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("CheckClassNotIn", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "ClassType",
                Value = this.inputClassTyp.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 3
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

            GridViewClass.DataSource = ds.Tables[0];
            GridViewClass.DataBind();
            conMatWorkFlow.Close();
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
            dataAdapter.SelectCommand.Parameters.AddWithValue("@MaterialID", this.inputMtrlID.Text.ToUpper().Trim());

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewInspectionType.DataSource = ds.Tables[0];
            GridViewInspectionType.DataBind();
            conMatWorkFlow.Close();
        }

        //Modal
        protected void selectClassTyp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string PlantID = grdrow.Cells[0].Text;
            string PlantName = grdrow.Cells[1].Text;
            inputClassTyp.Text = grdrow.Cells[0].Text;
            inputClassTyp.Focus();
            srcClassModalBinding();
        }
        protected void selectClass_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputClass.Text = grdrow.Cells[0].Text;
            inputClass.Focus();
        }
        protected void selectInspectionType_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            txtInspectionType.Text = grdrow.Cells[0].Text;
            txtInspectionType.Focus();
        }

        //Save Code
        protected void Update_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (chkbxInspectSet.Checked == true)
            {
                stringInspectSet = "X";
            }
            else if (chkbxInspectSet.Checked == false)
            {
                stringInspectSet = "";
            }
            try
            {
                SqlCommand cmdQCClass = new SqlCommand("UPDATE Tbl_QCClass SET New=@New WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmdQCClass.Parameters.Add(new SqlParameter
                {
                    ParameterName = "New",
                    Value = "x",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdQCClass.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdQCClass.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdQCClass.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdQCData = new SqlCommand("UPDATE Tbl_QCData SET New=@New WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmdQCData.Parameters.Add(new SqlParameter
                {
                    ParameterName = "New",
                    Value = "x",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdQCData.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdQCData.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdQCData.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
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
                SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET InspectionSetup=@InspectionSetup, InspectionInterval=@InspectionInterval, NewQC=@NewQC WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "NewQC",
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
                    ParameterName = "InspectionInterval",
                    Value = this.inputInspectIntrv.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "InspectionSetup",
                    Value = stringInspectSet.ToString().Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET QCEnd = @QCEnd WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
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
                    ParameterName = "QCEnd",
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
            Response.Redirect("~/Pages/qcPage");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (chkbxInspectSet.Checked == true)
            {
                stringInspectSet = "X";
            }
            else if (chkbxInspectSet.Checked == false)
            {
                stringInspectSet = "";
            }

            try
            {
                SqlCommand cmdSave = new SqlCommand("saveQC", conMatWorkFlow);
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "New",
                    Value = "x",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LogID",
                    Value = this.lblLogID.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Module_User",
                    Value = this.Session["Devisi"].ToString().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TypeStatus",
                    Value = "End",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Usnam",
                    Value = this.Session["Usnam"].ToString().Trim().ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "InspectionInterval",
                    Value = this.inputInspectIntrv.Text.ToUpper(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdSave.Parameters.Add(new SqlParameter
                {
                    ParameterName = "InspectionSetup",
                    Value = stringInspectSet.ToString().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
                });
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.ExecuteNonQuery();
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
            Response.Redirect("~/Pages/qcPage");
        }
        protected void Reject_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/qcPage");
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            conMatWorkFlow.Open();
            SqlCommand cmdQCClass = new SqlCommand("DELETE FROM Tbl_QCClass WHERE New=@New AND TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
            cmdQCClass.Parameters.Add(new SqlParameter
            {
                ParameterName = "New",
                Value = "",
                SqlDbType = SqlDbType.NVarChar,
                Size = 1
            });
            cmdQCClass.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdQCClass.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdQCClass.ExecuteNonQuery();
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            SqlCommand cmdQCData = new SqlCommand("DELETE FROM Tbl_QCData WHERE New=@New AND TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
            cmdQCData.Parameters.Add(new SqlParameter
            {
                ParameterName = "New",
                Value = "",
                SqlDbType = SqlDbType.NVarChar,
                Size = 1
            });
            cmdQCData.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdQCData.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            conMatWorkFlow.Close();

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
            Response.Redirect("~/Pages/qcPage");
        }
        protected void CancelUpdate_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmdQCClass = new SqlCommand("DELETE FROM Tbl_QCClass WHERE New=@New AND TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
            cmdQCClass.Parameters.Add(new SqlParameter
            {
                ParameterName = "New",
                Value = "",
                SqlDbType = SqlDbType.NVarChar,
                Size = 1
            });
            cmdQCClass.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdQCClass.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdQCClass.ExecuteNonQuery();
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            SqlCommand cmdQCData = new SqlCommand("DELETE FROM Tbl_QCData WHERE New=@New AND TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
            cmdQCData.Parameters.Add(new SqlParameter
            {
                ParameterName = "New",
                Value = "",
                SqlDbType = SqlDbType.NVarChar,
                Size = 1
            });
            cmdQCData.Parameters.Add(new SqlParameter
            {
                ParameterName = "TransID",
                Value = this.lblTransID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdQCData.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMtrlID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdQCData.ExecuteNonQuery();
            conMatWorkFlow.Close();
            Response.Redirect("~/Pages/qcPage");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/qcPage");
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

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }
        protected void GridViewClass_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewClass.PageIndex = e.NewPageIndex;
            srcClassModalBinding();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "openModal();", true);

        }
        protected void GridViewClass_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = GridViewClass.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridViewClass.DataSource = dataView;
                GridViewClass.DataBind();
            }
        }

        protected void inputClassTyp_TextChanged(object sender, EventArgs e)
        {
            if (inputClassTyp.Text == "")
            {

            }
            else if (inputClassTyp.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_ClassType WHERE ClassType = @inputClassTyp";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputClassTyp", this.inputClassTyp.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                }
                if (dr.HasRows == false)
                {
                    inputClassTyp.Text = "";
                    MsgBox("Wrong Input at Class Type!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            srcClassModalBinding();
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
                    MsgBox("Wrong Input at Inspection Type!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }

        protected void inputClass_TextChanged(object sender, EventArgs e)
        {
            if (inputClass.Text == "")
            {

            }
            else if (inputClass.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT* FROM Mstr_Classification WHERE ClassType = @ClassType AND Class = @inputClass";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputClass", this.inputClass.Text);
                cmd.Parameters.AddWithValue("ClassType", this.inputClassTyp.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                }
                if (dr.HasRows == false)
                {
                    inputClass.Text = "";
                    MsgBox("Wrong Input at Class!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
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

        protected void btnSync_Click(object sender, EventArgs e)
        {
            if(conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand("sp_Sync_Master_Classification", conMatWorkFlow);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();
                MsgBox("Please wait around maximum 10 minutes for Classification master data to be full synchronize.", this.Page, this);
            }
            catch(Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
        }
        protected void callingClassificationData(object sender, EventArgs e)
        {
            srcClassModalBinding();
        }

        protected void LBSearchMatID_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalClass", "$('#modalClass').modal();", true);
            srcClassModalBinding();
        }
    }
}