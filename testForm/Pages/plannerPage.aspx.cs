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
    public partial class plannerPage : System.Web.UI.Page
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
                    if (lblPosition.Text != "Planner")
                    {
                        Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                        return;
                    }
                    srcListViewBinding();
                }

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
            else if (IsPostBack)
            {
                if (lblPosition.Text == "Admin")
                {
                    //srcListViewBindingAdmin();
                }
                else
                {
                    //srcListViewBinding();
                }

                //srcLabOfficeModalBinding();
                //srcMRPGrModalBinding();
                //srcMRPTypModalBinding();
                //srcMRPCtrlModalBinding();
                //srcLOTSizeModalBinding();
                //srcSpcProcModalBinding();
                //srcProdStorLocModalBinding();
                //srcSchedMargKeyModalBinding();

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
                    SqlCommand cmdLOGTRANS = new SqlCommand("logIDBinding", conMatWorkFlow);
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
                    cmdLOGTRANS.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    while (drLOGTRANS.Read())
                    {
                        lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim();
                    }
                    conMatWorkFlow.Close();

                    conMatWorkFlow.Open();
                    SqlCommand cmdCheckNew = new SqlCommand("selectAllMaterial", conMatWorkFlow);
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCheckNew.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmdCheckNew.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["NewPlanner"].ToString().Trim() == "")
                        {
                            listViewPlan.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            MRP1.Visible = true;
                            MRP2.Visible = true;
                            MRP3.Visible = true;

                            btnSave.Visible = true;

                            lblPlant.Text = dr["Plant"].ToString().Trim();
                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                            inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                            inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                            inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                            inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                            inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                            inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                            inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                            inputProcType.Text = dr["ProcType"].ToString().Trim();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                            inputProdStorLoc.Text = dr["ProdSLoc"].ToString().Trim();
                            inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                            inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                            inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();
                        }
                        //Update Data
                        else
                        {
                            listViewPlan.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            MRP1.Visible = true;
                            MRP2.Visible = true;
                            MRP3.Visible = true;

                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpdate.Visible = true;

                            lblPlant.Text = dr["Plant"].ToString().Trim();
                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                            inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                            inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                            inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                            inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                            inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                            inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                            inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                            inputProcType.Text = dr["ProcType"].ToString().Trim();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                            inputProdStorLoc.Text = dr["ProdSLoc"].ToString().Trim();
                            inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                            inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                            inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();

                            inputStrtgyGr.Text = dr["PlanStrategyGroup"].ToString().Trim();

                            inputProdSched.Text = dr["ProdSched"].ToString().Trim();
                            inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString().Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                    return;
                }
            }
            else if (lblPosition.Text == "Planner")
            {
                try
                {
                    //SqlCommand cmdLOGTRANS = new SqlCommand("logIDBinding", conMatWorkFlow);
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
                    //    Value = grdrow.Cells[1].Text.Trim(),
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
                    //cmdLOGTRANS.CommandType = CommandType.StoredProcedure;
                    //SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    //while (drLOGTRANS.Read())
                    //{
                    //    lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim();
                    //}
                    //conMatWorkFlow.Close();

                    //conMatWorkFlow.Open();
                    SqlCommand cmdCheckNew = new SqlCommand("selectAllMaterial", conMatWorkFlow);
                    cmdCheckNew.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = TransID,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdCheckNew.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmdCheckNew.ExecuteReader();
                    while (dr.Read())
                    {
                        lblDivision.Text = dr["Division"].ToString().Trim();
                        if (dr["NewPlanner"].ToString().Trim() == "")
                        {
                            listViewPlan.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            MRP1.Visible = true;
                            MRP2.Visible = true;
                            MRP3.Visible = true;

                            btnSave.Visible = true;

                            lblPlant.Text = dr["Plant"].ToString().Trim();
                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                            inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                            inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                            inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                            inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                            inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                            inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                            inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                            inputProcType.Text = dr["ProcType"].ToString().Trim();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                            inputProdStorLoc.Text = dr["ProdSLoc"].ToString().Trim();
                            inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                            inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                            inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();

                            inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                            if (inputGRProcTimeMRP1.Text == "")
                            {
                                inputGRProcTimeMRP1.Text = "1";
                            }
                            inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();
                        }
                        //Update Data
                        else
                        {
                            listViewPlan.Visible = false;

                            rmContent.Visible = true;
                            bscDt1GnrlDt.Visible = true;
                            MRP1.Visible = true;
                            MRP2.Visible = true;
                            MRP3.Visible = true;

                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnUpdate.Visible = true;
                            btnCancelUpdate.Visible = true;

                            lblPlant.Text = dr["Plant"].ToString().Trim();
                            inputType.Text = dr["Type"].ToString().Trim();
                            inputMatTyp.Text = dr["MatType"].ToString().Trim();
                            lblTransID.Text = dr["TransID"].ToString().Trim();
                            inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                            inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                            inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                            inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                            inputPlant.Text = dr["Plant"].ToString().Trim();
                            inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                            inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                            inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                            inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                            inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                            inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                            inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                            inputProcType.Text = dr["ProcType"].ToString().Trim();
                            inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                            inputProdStorLoc.Text = dr["ProdSLoc"].ToString().Trim();
                            inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                            inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                            inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();

                            inputStrtgyGr.Text = dr["PlanStrategyGroup"].ToString().Trim();

                            inputProdSched.Text = dr["ProdSched"].ToString().Trim();
                            inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString().Trim();

                            inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                            if (inputGRProcTimeMRP1.Text == "")
                            {
                                inputGRProcTimeMRP1.Text = "1";
                            }
                            inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();
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
                MsgBox(this.lblUser.Text.Trim() + " username are not for Planner division", this.Page, this);
                Response.Redirect("~/Pages/plannerPage");
            }
            conMatWorkFlow.Close();
            srcLabOfficeModalBinding();
            srcMRPGrModalBinding();
            srcMRPTypModalBinding();
            srcMRPCtrlModalBinding();
            srcLOTSizeModalBinding();
            srcSpcProcModalBinding();
            srcProdStorLocModalBinding();
            srcSchedMargKeyModalBinding();
            srcStrategyGroupModalBinding();
            srcProdSchedModalBinding();

            bindLblBsUntMeas();
            bindLblLabOffice();
            bindLblMRPGrp();
            bindLblMRPType();
            bindLblMRPController();
            bindLblLotSize();
            bindLblProdSched();
            bindLblProdSchedProfile();
            bindLblStrategyGroup();

            if (inputPlant.Text == "5200")
            {
                inputStrtgyGr.Text = "52";
            }

            if (inputProcType.Text == "X" || inputProcType.Text == "x")
            {
                lblProcType.Text = "Both Procurement";
            }
            else if (inputProcType.Text == "F" || inputProcType.Text == "f")
            {
                lblProcType.Text = "External Procurement";
            }
            else if (inputProcType.Text == "E" || inputProcType.Text == "e")
            {
                lblProcType.Text = "In-house Production";
            }
            else if (inputProcType.Text == "")
            {
                lblProcType.Text = "No Procurement";
            }

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

            if(inputType.Text.Trim().ToUpper() != "RM")
            {
                PurchasingValue.Visible = true;
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

            imgBtnLabOffice.Visible = false;
            imgBtnLOTSize.Visible = false;
            imgBtnMRPController.Visible = false;
            imgBtnMRPGr.Visible = false;
            imgBtnMRPType.Visible = false;
            imgBtnProcSchedProfile.Visible = false;
            imgBtnProdSched.Visible = false;
            imgBtnProdStorLoc.Visible = false;
            imgBtnSchedMargKey.Visible = false;
            imgBtnSpcProc.Visible = false;
            imgBtnStrategyGroup.Visible = false;

            inputType.ReadOnly = true;
            inputMatTyp.ReadOnly = true;
            inputMtrlID.ReadOnly = true;
            inputMtrlDesc.ReadOnly = true;
            inputBsUntMeas.ReadOnly = true;
            inputOldMtrlNum.ReadOnly = true;
            inputPlant.ReadOnly = true;
            inputLabOffice.ReadOnly = true;

            inputMRPGr.ReadOnly = true;
            inputMRPTyp.ReadOnly = true;
            inputMRPCtrl.ReadOnly = true;
            inputLOTSize.ReadOnly = true;
            inputFixLotSize.ReadOnly = true;
            inputMaxStockLv.ReadOnly = true;

            inputProcType.ReadOnly = true;
            inputSpcProc.ReadOnly = true;
            inputProdStorLoc.ReadOnly = true;
            inputSchedMargKey.ReadOnly = true;
            inputSftyStck.ReadOnly = true;
            inputMinSftyStck.ReadOnly = true;

            inputStrtgyGr.ReadOnly = true;
            inputTotalLeadTime.ReadOnly = true;

            inputProdSched.ReadOnly = true;
            inputProdSchedProfile.ReadOnly = true;

            inputGRProcTimeMRP1.ReadOnly = true;
            inputPlantDeliveryTime.ReadOnly = true;

            inputType.CssClass = "txtBoxRO";
            inputMatTyp.CssClass = "txtBoxRO";
            inputMtrlID.CssClass = "txtBoxRO";
            inputMtrlDesc.CssClass = "txtBoxRO";
            inputBsUntMeas.CssClass = "txtBoxRO";
            inputOldMtrlNum.CssClass = "txtBoxRO";
            inputPlant.CssClass = "txtBoxRO";
            inputLabOffice.CssClass = "txtBoxRO";

            inputMRPGr.CssClass = "txtBoxRO";
            inputMRPTyp.CssClass = "txtBoxRO";
            inputMRPCtrl.CssClass = "txtBoxRO";
            inputLOTSize.CssClass = "txtBoxRO";
            inputFixLotSize.CssClass = "txtBoxRO";
            inputMaxStockLv.CssClass = "txtBoxRO";

            inputProcType.CssClass = "txtBoxRO";
            inputSpcProc.CssClass = "txtBoxRO";
            inputProdStorLoc.CssClass = "txtBoxRO";
            inputSchedMargKey.CssClass = "txtBoxRO";
            inputSftyStck.CssClass = "txtBoxRO";
            inputMinSftyStck.CssClass = "txtBoxRO";

            inputStrtgyGr.CssClass = "txtBoxRO";
            inputTotalLeadTime.CssClass = "txtBoxRO";

            inputProdSched.CssClass = "txtBoxRO";
            inputProdSchedProfile.CssClass = "txtBoxRO";

            inputGRProcTimeMRP1.CssClass = "txtBoxRO";
            inputPlantDeliveryTime.CssClass = "txtBoxRO";

            inputType.Attributes.Add("placeholder", "");
            inputMatTyp.Attributes.Add("placeholder", "");
            inputMtrlID.Attributes.Add("placeholder", "");
            inputMtrlDesc.Attributes.Add("placeholder", "");
            inputBsUntMeas.Attributes.Add("placeholder", "");
            inputOldMtrlNum.Attributes.Add("placeholder", "");
            inputPlant.Attributes.Add("placeholder", "");
            inputLabOffice.Attributes.Add("placeholder", "");

            inputMRPGr.Attributes.Add("placeholder", "");
            inputMRPTyp.Attributes.Add("placeholder", "");
            inputMRPCtrl.Attributes.Add("placeholder", "");
            inputLOTSize.Attributes.Add("placeholder", "");
            inputFixLotSize.Attributes.Add("placeholder", "");
            inputMaxStockLv.Attributes.Add("placeholder", "");

            inputProcType.Attributes.Add("placeholder", "");
            inputSpcProc.Attributes.Add("placeholder", "");
            inputProdStorLoc.Attributes.Add("placeholder", "");
            inputSchedMargKey.Attributes.Add("placeholder", "");
            inputSftyStck.Attributes.Add("placeholder", "");
            inputMinSftyStck.Attributes.Add("placeholder", "");

            inputStrtgyGr.Attributes.Add("placeholder", "");
            inputTotalLeadTime.Attributes.Add("placeholder", "");

            inputProdSched.Attributes.Add("placeholder", "");
            inputProdSchedProfile.Attributes.Add("placeholder", "");

            inputGRProcTimeMRP1.Attributes.Add("placeholder", "");
            inputPlantDeliveryTime.Attributes.Add("placeholder", "");

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
                listViewPlan.Visible = false;

                rmContent.Visible = true;
                bscDt1GnrlDt.Visible = true;
                MRP1.Visible = true;
                MRP2.Visible = true;
                MRP3.Visible = true;

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnClose.Visible = true;

                lblPlant.Text = dr["Plant"].ToString().Trim();
                inputType.Text = dr["Type"].ToString().Trim();
                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                lblTransID.Text = dr["TransID"].ToString().Trim();
                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                inputPlant.Text = dr["Plant"].ToString().Trim();
                inputLabOffice.Text = dr["LabOffice"].ToString().Trim();

                inputMRPGr.Text = dr["MRPGroup"].ToString().Trim();
                inputMRPTyp.Text = dr["MRPType"].ToString().Trim();
                inputMRPCtrl.Text = dr["MRPController"].ToString().Trim();
                inputLOTSize.Text = dr["LotSize"].ToString().Trim();
                inputFixLotSize.Text = dr["FixLotSize"].ToString().Trim();
                inputMaxStockLv.Text = dr["MaxStockLvl"].ToString().Trim();

                inputProcType.Text = dr["ProcType"].ToString().Trim();
                inputSpcProc.Text = dr["SpclProcurement"].ToString().Trim();
                inputProdStorLoc.Text = dr["ProdSLoc"].ToString().Trim();
                inputSchedMargKey.Text = dr["SchedType"].ToString().Trim();
                inputSftyStck.Text = dr["SafetyStock"].ToString().Trim();
                inputMinSftyStck.Text = dr["MinSafetyStock"].ToString().Trim();

                inputStrtgyGr.Text = dr["PlanStrategyGroup"].ToString().Trim();

                inputProdSched.Text = dr["ProdSched"].ToString().Trim();
                inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString().Trim();

                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                if (inputGRProcTimeMRP1.Text == "")
                {
                    inputGRProcTimeMRP1.Text = "1";
                }
                inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();
            }
            conMatWorkFlow.Close();            

            if (inputProcType.Text == "X" || inputProcType.Text == "x")
            {
                lblProcType.Text = "Both Procurement";
            }
            else if (inputProcType.Text == "F" || inputProcType.Text == "f")
            {
                lblProcType.Text = "External Procurement";
            }
            else if (inputProcType.Text == "E" || inputProcType.Text == "e")
            {
                lblProcType.Text = "In-house Production";
            }
            else if (inputProcType.Text == "")
            {
                lblProcType.Text = "No Procurement";
            }
            if (inputType.Text.Trim().ToUpper() != "RM")
            {
                PurchasingValue.Visible = true;
            }

            bindLblBsUntMeas();
            bindLblLabOffice();
            bindLblMRPGrp();
            bindLblMRPType();
            bindLblMRPController();
            bindLblLotSize();
            bindLblProdSched();
            bindLblProdSchedProfile();
            bindLblStrategyGroup();
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
                    if (columnPlanEnd == "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Open for Planner";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        if (columnQAEnd != "&nbsp;")
                        {
                            string lblNotesQA = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQA;
                        }
                        if (columnQCEnd != "&nbsp;")
                        {
                            string lblNotesQC = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQC;
                        }
                        if (columnProcEnd != "&nbsp;")
                        {
                            string lblNotesPROC = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesPROC;
                        }
                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotesQR = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQR;
                        }                    }
                    else if (columnPlanEnd != "&nbsp;" && columnPlanStart != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Closed for Planner";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                        if (columnQAEnd != "&nbsp;")
                        {
                            string lblNotesQA = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QA/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQA;
                        }
                        if (columnQCEnd != "&nbsp;")
                        {
                            string lblNotesQC = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QC/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQC;
                        }
                        if (columnProcEnd != "&nbsp;")
                        {
                            string lblNotesPROC = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesPROC;
                        }
                        if (columnQREnd != "&nbsp;")
                        {
                            string lblNotesQR = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("QR", "");
                            ((Label)e.Row.FindControl("lblNotes")).Text = lblNotesQR;
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
        protected void srcLabOfficeModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_LabOffice"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewLabOffice.DataSource = ds.Tables[0];
            GridViewLabOffice.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcMRPGrModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcMRPGroup", conMatWorkFlow);
            cmd.Parameters.Add("@TransID", SqlDbType.NVarChar).Value = this.lblTransID.Text;
            cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar).Value = this.inputMtrlID.Text;
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewMRPGr.DataSource = ds.Tables[0];
            GridViewMRPGr.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcMRPTypModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_MRPType"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewMRPTyp.DataSource = ds.Tables[0];
            GridViewMRPTyp.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcMRPCtrlModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcMRPControllers", conMatWorkFlow);
            cmd.Parameters.Add("@TransID", SqlDbType.NVarChar).Value = this.lblTransID.Text;
            cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar).Value = this.inputMtrlID.Text;
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewMRPCtrl.DataSource = ds.Tables[0];
            GridViewMRPCtrl.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcLOTSizeModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_MRPLotSize"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewLOTSize.DataSource = ds.Tables[0];
            GridViewLOTSize.DataBind();
            conMatWorkFlow.Close();
        }
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
            if (inputProcType.Text == "F" || inputProcType.Text == "f")
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
        protected void srcProdStorLocModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_Location WHERE Plant=@Plant"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
            dataAdapter.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "Plant",
                Value = this.lblPlant.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 5
            });

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewProdStorLoc.DataSource = ds.Tables[0];
            GridViewProdStorLoc.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcSchedMargKeyModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_SchedMarginKey WHERE Plant=@Plant"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
            dataAdapter.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "Plant",
                Value = this.lblPlant.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 5
            });

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewSchedMargKey.DataSource = ds.Tables[0];
            GridViewSchedMargKey.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcStrategyGroupModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_StrategyGroup"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewStrategyGroup.DataSource = ds.Tables[0];
            GridViewStrategyGroup.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcProdSchedModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcProdSched", conMatWorkFlow);
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
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewProdSched.DataSource = ds.Tables[0];
            GridViewProdSched.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcProdSchedProfileModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcProdSchedProfile", conMatWorkFlow);
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
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewProdSchedProfile.DataSource = ds.Tables[0];
            GridViewProdSchedProfile.DataBind();
            conMatWorkFlow.Close();
        }

        //Modal
        protected void selectLabOffice_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputLabOffice.Text = grdrow.Cells[0].Text;
            lblLabOffice.Text = grdrow.Cells[1].Text;
            lblLabOffice.ForeColor = Color.Black;
            inputLabOffice.Focus();
        }
        protected void selectMRPGr_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputMRPGr.Text = grdrow.Cells[0].Text;
            lblMRPGr.Text = grdrow.Cells[2].Text;
            lblMRPGr.ForeColor = Color.Black;
            inputMRPGr.Focus();

            if(inputMRPGr.Text == "0004")
            {
                if(inputPlant.Text == "2200")
                {
                    if(lblDivision.Text == "15")
                    {
                        inputStrtgyGr.Text = "52";
                        bindLblStrategyGroup();
                    }
                }
            }
        }
        protected void selectMRPTyp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputMRPTyp.Text = grdrow.Cells[0].Text.Trim();
            lblMRPTyp.Text = grdrow.Cells[1].Text.Trim();
            lblMRPTyp.ForeColor = Color.Black;
            inputMRPTyp.Focus();

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
        protected void selectMRPCtrl_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputMRPCtrl.Text = grdrow.Cells[0].Text;
            lblMRPCtrl.Text = grdrow.Cells[2].Text;
            lblMRPCtrl.ForeColor = Color.Black;
            inputMRPCtrl.Focus();
        }
        protected void selectLOTSize_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputLOTSize.Text = grdrow.Cells[0].Text;
            lblLOTSize.Text = grdrow.Cells[1].Text;
            lblLOTSize.ForeColor = Color.Black;
            inputLOTSize.Focus();

            if (inputLOTSize.Text.Trim().ToUpper() == "FX")
            {
                trFixLotSize.Visible = true;
            }
            else
            {
                trFixLotSize.Visible = false;
                inputFixLotSize.Text = "";
            }
        }
        protected void selectSpcProc_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputSpcProc.Text = grdrow.Cells[0].Text;
            lblSpcProc.Text = grdrow.Cells[1].Text;
            lblSpcProc.ForeColor = Color.Black;
            inputSpcProc.Focus();
        }
        protected void selectProdStorLoc_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputProdStorLoc.Text = grdrow.Cells[1].Text;
            lblProdStorLoc.Text = grdrow.Cells[2].Text;
            lblProdStorLoc.ForeColor = Color.Black;
            inputProdStorLoc.Focus();
        }
        protected void selectSchedMargKey_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputSchedMargKey.Text = grdrow.Cells[0].Text;
            lblSchedMargKey.Text = "";
            lblSchedMargKey.ForeColor = Color.Black;
            inputSchedMargKey.Focus();
        }
        protected void selectStrategyGroup_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputStrtgyGr.Text = grdrow.Cells[0].Text;
            lblStrategyGroup.Text = grdrow.Cells[1].Text;
            lblStrategyGroup.ForeColor = Color.Black;
            inputStrtgyGr.Focus();
        }
        protected void selectProdSched_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputProdSched.Text = grdrow.Cells[1].Text;
            lblProdSched.Text = grdrow.Cells[2].Text;
            inputProdSchedProfile.Text = grdrow.Cells[3].Text;
            lblProdSchedProfile.Text = grdrow.Cells[4].Text;
            lblProdSched.ForeColor = Color.Black;
            srcProdSchedProfileModalBinding();
        }
        protected void selectProdSchedProfile_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputProdSchedProfile.Text = grdrow.Cells[1].Text;
            lblProdSchedProfile.Text = grdrow.Cells[2].Text;
            lblProdSchedProfile.ForeColor = Color.Black;
        }

        //Save Code
        protected void Update_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            if (inputSpcProc.Text == "")
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
                SqlCommand cmdUpdTracking = new SqlCommand("UPDATE Tbl_Tracking SET PlanEnd=@PlanEnd WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
                cmdUpdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PlanEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdUpdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = this.lblTransID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmdUpdTracking.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaterialID",
                    Value = this.inputMtrlID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                cmdUpdTracking.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET TotLeadTime=@TotLeadTime, ProdSched=@ProdSched, ProdSchedProfile=@ProdSchedProfile, LabOffice=@LabOffice, MRPGroup = @MRPGroup, MRPType = @MRPType, MRPController = @MRPController, LotSize = @LotSize, MaxStockLvl = @MaxStockLvl, ProdSLoc = @ProdSLoc, SpclProcurement = @SpclProcurement, SchedType = @SchedType, SafetyStock = @SafetyStock, MinSafetyStock = @MinSafetyStock, PlanStrategyGroup = @PlanStrategyGroup, NewPlanner=@NewPlanner, FixLotSize=@FixLotSize WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TotLeadTime",
                    Value = this.inputTotalLeadTime.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "FixLotSize",
                    Value = this.inputFixLotSize.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProdSched",
                    Value = this.inputProdSched.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProdSchedProfile",
                    Value = this.inputProdSchedProfile.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 6
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "NewPlanner",
                    Value = "x",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
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
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LabOffice",
                    Value = this.inputLabOffice.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MRPGroup",
                    Value = this.inputMRPGr.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MRPType",
                    Value = this.inputMRPTyp.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MRPController",
                    Value = this.inputMRPCtrl.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LotSize",
                    Value = this.inputLOTSize.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaxStockLvl",
                    Value = this.inputMaxStockLv.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProdSLoc",
                    Value = this.inputProdStorLoc.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
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
                    ParameterName = "SchedType",
                    Value = this.inputSchedMargKey.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SafetyStock",
                    Value = this.inputSftyStck.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MinSafetyStock",
                    Value = this.inputMinSftyStck.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PlanStrategyGroup",
                    Value = this.inputStrtgyGr.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CreateBy",
                    Value = this.lblUser.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CreateTime",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime,
                });
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET PlanEnd = @PlanEnd WHERE TransID=@TransID AND MaterialID=@MaterialID", conMatWorkFlow);
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
                    ParameterName = "PlanEnd",
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

                if (inputType.Text.Trim().ToUpper() != "RM")
                {
                    conMatWorkFlow.Open();
                    SqlCommand cmdGR = new SqlCommand("UPDATE Tbl_Material SET GRProcessingTime = @GRProcessingTime, PlantDeliveryTime = @PlantDeliveryTime WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = this.lblTransID.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = this.inputMtrlID.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "GRProcessingTime",
                        Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PlantDeliveryTime",
                        Value = this.inputPlantDeliveryTime.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdGR.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Redirect("~/Pages/plannerPage");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "No")
            {
                return;
            }
            else
            {
                
            }

            if (inputMRPGr.Text == "" || inputMRPTyp.Text == "" || inputMRPCtrl.Text == "" || inputSchedMargKey.Text == "")
            {
                MsgBox("One of the required data is empty!", this.Page, this);
                return;
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
                cmdLogTrans.Parameters.Add(new SqlParameter
                {
                    ParameterName = "time",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdLogTrans.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET PlanEnd = @PlanEnd WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
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
                    ParameterName = "PlanEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdTracking.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET TotLeadTime=@TotLeadTime, ProdSched=@ProdSched, ProdSchedProfile=@ProdSchedProfile, LabOffice=@LabOffice, MRPGroup = @MRPGroup, MRPType = @MRPType, MRPController = @MRPController, LotSize = @LotSize, MaxStockLvl = @MaxStockLvl, ProdSLoc = @ProdSLoc, SpclProcurement = @SpclProcurement, SchedType = @SchedType, SafetyStock = @SafetyStock, MinSafetyStock = @MinSafetyStock, PlanStrategyGroup = @PlanStrategyGroup, NewPlanner=@NewPlanner, FixLotSize=@FixLotSize WHERE TransID = @TransID", conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TotLeadTime",
                    Value = this.inputTotalLeadTime.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "FixLotSize",
                    Value = this.inputFixLotSize.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProdSched",
                    Value = this.inputProdSched.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProdSchedProfile",
                    Value = this.inputProdSchedProfile.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 6
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "NewPlanner",
                    Value = "x",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 1
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
                    ParameterName = "LabOffice",
                    Value = this.inputLabOffice.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MRPGroup",
                    Value = this.inputMRPGr.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MRPType",
                    Value = this.inputMRPTyp.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MRPController",
                    Value = this.inputMRPCtrl.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "LotSize",
                    Value = this.inputLOTSize.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaxStockLvl",
                    Value = this.inputMaxStockLv.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ProdSLoc",
                    Value = this.inputProdStorLoc.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
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
                    ParameterName = "SchedType",
                    Value = this.inputSchedMargKey.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SafetyStock",
                    Value = this.inputSftyStck.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MinSafetyStock",
                    Value = this.inputMinSftyStck.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PlanStrategyGroup",
                    Value = this.inputStrtgyGr.Text.ToUpper().Trim(),
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

                if (inputType.Text.Trim().ToUpper() != "RM")
                {
                    conMatWorkFlow.Open();
                    SqlCommand cmdGR = new SqlCommand("UPDATE Tbl_Material SET GRProcessingTime = @GRProcessingTime, PlantDeliveryTime = @PlantDeliveryTime WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TransID",
                        Value = this.lblTransID.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MaterialID",
                        Value = this.inputMtrlID.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "GRProcessingTime",
                        Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdGR.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PlantDeliveryTime",
                        Value = this.inputPlantDeliveryTime.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmdGR.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                }
            }
            catch(Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Redirect("~/Pages/plannerPage");
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            //conMatWorkFlow.Open();
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
            Response.Redirect("~/Pages/plannerPage");
        }
        protected void CancelUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/plannerPage");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/plannerPage");
        }

        //inputLabOffice_onBlur
        protected void inputLabOffice_onBlur(object sender, EventArgs e)
        {
            if (inputLabOffice.Text == "")
            {
                lblLabOffice.Text = absLabOffice.Text;
                lblLabOffice.ForeColor = Color.Black;
            }
            else if (inputLabOffice.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_LabOffice WHERE LabOffice = @inputLabOffice";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputLabOffice", this.inputLabOffice.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblLabOffice.Text = dr["Lab_Desc"].ToString();
                    lblLabOffice.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblLabOffice.Text = "Wrong Input!";
                    lblLabOffice.ForeColor = Color.Red;
                    inputLabOffice.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputMRPGr_onBlur
        protected void inputMRPGr_onBlur(object sender, EventArgs e)
        {
            if (inputMRPGr.Text == "")
            {
                lblMRPGr.Text = absMRPGr.Text;
                lblMRPGr.ForeColor = Color.Black;
            }
            else if (inputMRPGr.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblMRPGroup", conMatWorkFlow);
                cmd.Parameters.AddWithValue("MRPGroup", this.inputMRPGr.Text);
                cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
                cmd.Parameters.AddWithValue("MaterialID", this.inputMtrlID.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblMRPGr.Text = dr["MRPGroupDesc"].ToString();
                    lblMRPGr.ForeColor = Color.Black;

                    if (inputMRPGr.Text == "0004")
                    {
                        if (inputPlant.Text == "2200")
                        {
                            if (lblDivision.Text == "15")
                            {
                                inputStrtgyGr.Text = "52";
                            }
                        }
                    }
                }
                if (dr.HasRows == false)
                {
                    lblMRPGr.Text = "Wrong Input!";
                    lblMRPGr.ForeColor = Color.Red;
                    inputMRPGr.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            bindLblStrategyGroup();
        }
        //inputMRPTyp_onBlur
        protected void inputMRPTyp_onBlur(object sender, EventArgs e)
        {
            if (inputMRPTyp.Text == "")
            {
                lblMRPTyp.Text = absMRPTyp.Text;
                lblMRPTyp.ForeColor = Color.Black;
            }
            else if (inputMRPTyp.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                
                var queryString = "SELECT * FROM Mstr_MRPType WHERE MRPType = @inputMRPTyp";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputMRPTyp", this.inputMRPTyp.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblMRPTyp.Text = dr["MRPTypeDesc"].ToString();
                    lblMRPTyp.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblMRPTyp.Text = "Wrong Input!";
                    lblMRPTyp.ForeColor = Color.Red;
                    inputMRPTyp.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                //panggil sp yg load data comon table yg nyimpem type2 MRPType
                DataTable dt = new DataTable();

                SqlCommand cmdT = new SqlCommand("bindCommonTableMRPType", conMatWorkFlow);
                cmdT.CommandType = CommandType.StoredProcedure;
                var dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmdT;

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
        //inputMRPCtrl_onBlur
        protected void inputMRPCtrl_onBlur(object sender, EventArgs e)
        {
            if (inputMRPCtrl.Text == "")
            {
                lblMRPCtrl.Text = absMRPCtrl.Text;
                lblMRPCtrl.ForeColor = Color.Black;
            }
            else if (inputMRPCtrl.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_MRPControllers WHERE MRPController = @MRPController AND Plant = @Plant";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("MRPController", this.inputMRPCtrl.Text);
                cmd.Parameters.AddWithValue("Plant", this.inputPlant.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblMRPCtrl.Text = dr["MRPControllerDesc"].ToString();
                    lblMRPCtrl.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblMRPCtrl.Text = "Wrong Input!";
                    lblMRPCtrl.ForeColor = Color.Red;
                    inputMRPCtrl.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputLOTSize_onBlur
        protected void inputLOTSize_onBlur(object sender, EventArgs e)
        {
            if (inputLOTSize.Text == "")
            {
                lblLOTSize.Text = absLOTSize.Text;
                lblLOTSize.ForeColor = Color.Black;
            }
            else if (inputLOTSize.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_MRPLotSize WHERE LotSize = @inputLOTSize";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputLOTSize", this.inputLOTSize.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblLOTSize.Text = dr["LotSizeDesc"].ToString();
                    lblLOTSize.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblLOTSize.Text = "Wrong Input!";
                    lblLOTSize.ForeColor = Color.Red;
                    inputLOTSize.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }

            if (inputLOTSize.Text.Trim().ToUpper() == "FX")
            {
                trFixLotSize.Visible = true;
            }
            else
            {
                trFixLotSize.Visible = false;
                inputFixLotSize.Text = "";
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
        //inputProdStorLoc_onBlur
        protected void inputProdStorLoc_onBlur(object sender, EventArgs e)
        {
            if (inputProdStorLoc.Text == "")
            {
                lblProdStorLoc.Text = absProdStorLoc.Text;
                lblProdStorLoc.ForeColor = Color.Black;
            }
            else if (inputProdStorLoc.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_Location WHERE SLoc = @inputProdStorLoc";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputProdStorLoc", this.inputProdStorLoc.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblProdStorLoc.Text = dr["SLoc_Desc"].ToString();
                    lblProdStorLoc.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblProdStorLoc.Text = "Wrong Input!";
                    lblProdStorLoc.ForeColor = Color.Red;
                    inputProdStorLoc.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputSchedMargKey_onBlur
        protected void inputSchedMargKey_onBlur(object sender, EventArgs e)
        {
            if (inputSchedMargKey.Text == "")
            {
                lblSchedMargKey.Text = absSchedMargKey.Text;
                lblSchedMargKey.ForeColor = Color.Black;
            }
            else if (inputSchedMargKey.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_SchedMarginKey WHERE SchedType = @inputSchedMargKey AND Plant = @Plant";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputSchedMargKey", this.inputSchedMargKey.Text);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Plant",
                    Value = this.lblPlant.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 5
                });
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblSchedMargKey.Text = "";
                    lblSchedMargKey.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblSchedMargKey.Text = "Wrong Input!";
                    lblSchedMargKey.ForeColor = Color.Red;
                    inputSchedMargKey.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputSchedMargKey_onBlur
        protected void inputStrategyGroup_onBlur(object sender, EventArgs e)
        {
            if (inputStrtgyGr.Text == "")
            {
                lblStrategyGroup.Text = absStrategyGroup.Text;
                lblStrategyGroup.ForeColor = Color.Black;
            }
            else if (inputStrtgyGr.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_StrategyGroup WHERE PlanStrategyGroup=@PlanStrategyGroup";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PlanStrategyGroup",
                    Value = this.inputStrtgyGr.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4
                });
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblStrategyGroup.Text = dr["PlanStrategyGrpDesc"].ToString();
                    lblStrategyGroup.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblStrategyGroup.Text = "Wrong Input!";
                    lblStrategyGroup.ForeColor = Color.Red;
                    inputStrtgyGr.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        protected void inputProdSched_TextChanged(object sender, EventArgs e)
        {
            if (inputProdSched.Text == "")
            {
                inputProdSchedProfile.Text = "";
                lblProdSchedProfile.Text = absProdSchedProfile.Text;
                lblProdSched.Text = absProdSched.Text;
                lblProdSched.ForeColor = Color.Black;
            }
            else if (inputProdSched.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                
                SqlCommand cmd = new SqlCommand("bindLblProdSched", conMatWorkFlow);
                cmd.CommandType = CommandType.StoredProcedure;
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
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblProdSched.Text = dr["ProdSchedDesc"].ToString();
                    lblProdSched.ForeColor = Color.Black;
                    inputProdSchedProfile.Text = dr["ProdSchedProfile"].ToString();
                    lblProdSchedProfile.Text = dr["ProdSchedProfileDesc"].ToString();
                    lblProdSchedProfile.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblProdSched.Text = "Wrong Input!";
                    lblProdSched.ForeColor = Color.Red;
                    inputProdSched.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            srcProdSchedProfileModalBinding();
        }
        protected void inputProdSchedProfile_TextChanged(object sender, EventArgs e)
        {
            if (inputProdSchedProfile.Text == "")
            {
                lblProdSchedProfile.Text = absProdSchedProfile.Text;
                lblProdSchedProfile.ForeColor = Color.Black;
            }
            else if (inputProdSchedProfile.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("srcProdSched", conMatWorkFlow);
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
                    Value = this.inputMtrlID.Text.ToUpper().Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblProdSchedProfile.Text = dr["ProdSchedProfileDesc"].ToString();
                    lblProdSchedProfile.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblProdSchedProfile.Text = "Wrong Input!";
                    lblProdSchedProfile.ForeColor = Color.Red;
                    inputProdSchedProfile.Text = "";
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
            if (inputLOTSize.Text.Trim().ToUpper() == "FX")
            {
                trFixLotSize.Visible = true;
            }
            else
            {
                trFixLotSize.Visible = false;
                inputFixLotSize.Text = "";
            }
        }
        protected void bindLblMRPController()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblMRPController", conMatWorkFlow);
            cmd.Parameters.AddWithValue("MRPController", this.inputMRPCtrl.Text);
            cmd.Parameters.AddWithValue("Plant", this.inputPlant.Text);
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
            cmd.Parameters.AddWithValue("MRPGroup", this.inputMRPGr.Text);
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
                lblStrategyGroup.Text = dr["PlanStrategyGrpDesc"].ToString();
                lblStrategyGroup.ForeColor = Color.Black;
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