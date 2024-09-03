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
    public partial class procPage : System.Web.UI.Page
    {
        SQLConnect.SQLConnect sqlC = new SQLConnect.SQLConnect();
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
                if (lblPosition.Text != "Proc")
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
            param[1] = new SqlParameter("@MatSample", MatSample);
            param[2] = new SqlParameter("@Division", Division);
            param[3] = new SqlParameter("@Type", Type);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewRMSFFGDeptConst", param);

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
        }

        //Binding List View Code
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
            lblSort.Text = "";
            lblSortDirection.Text = "";
            lblSortExpression.Text = "";
            srcListViewBinding();
        }
        protected DataTable GetData_srcListViewMatID(string inputMatIDDESC, string InitiateBy, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@InitiateBy", InitiateBy);
            param[2] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatIDDeptConst", param);

            return dt;
        }
        protected DataTable GetData_srcListViewMatDESC(string inputMatIDDESC, string InitiateBy, string Division)
        {
            DataTable dt;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@inputMatIDDESC", inputMatIDDESC);
            param[1] = new SqlParameter("@InitiateBy", InitiateBy);
            param[2] = new SqlParameter("@Division", Division);

            //send param to SP sql
            dt = sqlC.ExecuteDataTable("srcListViewMatDESCDeptConst", param);

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
                    //SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    //while (drLOGTRANS.Read())
                    //{
                    //    lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim();
                    //}
                    //conMatWorkFlow.Close();

                    //conMatWorkFlow.Open();
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
                        if (dr["NewProc"].ToString().Trim() == "")
                        {
                            // save Button
                            if (dr["Type"].ToString().Trim() == "RM")
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                BscDtDimension.Visible = true;
                                purchValNOrder.Visible = true;
                                MRPLotSize.Visible = true;
                                ForeignTradeData.Visible = true;
                                PlantShelfLifeDt.Visible = true;
                                SalesData.Visible = true;
                                trPlantDeliveryTime.Visible = true;

                                btnSave.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                                inputNetWeight.Text = dr["NetWeight"].ToString().Trim();
                                inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim();

                                inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();

                                inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                                ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                                inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();

                                inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                                inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                                inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();

                                inputLoadingGrp.Text = dr["Plant"].ToString().Trim();
                                if (ddListSLED.Text == "D")
                                {
                                    lblMinRemShelfLife.Text = "DAY";
                                }
                                else
                                {
                                    lblMinRemShelfLife.Text = "MONTH";
                                }
                            }
                            //Create New SF/FG
                            else
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                purchValNOrder.Visible = true;

                                btnSave.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                            }
                        }
                        else
                        {
                            // update Button
                            if (dr["Type"].ToString().Trim() == "RM")
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                BscDtDimension.Visible = true;
                                purchValNOrder.Visible = true;
                                MRPLotSize.Visible = true;
                                ForeignTradeData.Visible = true;
                                PlantShelfLifeDt.Visible = true;
                                SalesData.Visible = true;
                                trPlantDeliveryTime.Visible = true;

                                btnSave.Visible = false;
                                btnUpdate.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                                inputNetWeight.Text = dr["NetWeight"].ToString().Trim();
                                inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim();

                                inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();

                                inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                                ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                                inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();

                                inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                                inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                                inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();

                                inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();
                                if (ddListSLED.Text == "D")
                                {
                                    lblMinRemShelfLife.Text = "DAY";
                                }
                                else
                                {
                                    lblMinRemShelfLife.Text = "MONTH";
                                }
                            }
                            //UPDATE SF/FG
                            else
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                purchValNOrder.Visible = true;

                                btnSave.Visible = false;
                                btnUpdate.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString(), this.Page, this);
                }
            }
            else if (lblPosition.Text == "Proc")
            {
                try
                {
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
                    //SqlDataReader drLOGTRANS = cmdLOGTRANS.ExecuteReader();
                    //while (drLOGTRANS.Read())
                    //{
                    //    lblLogID.Text = drLOGTRANS["LogID"].ToString().Trim();
                    //}
                    //conMatWorkFlow.Close();
                    
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
                        if (dr["NewProc"].ToString().Trim() == "")
                        {
                            // save Button
                            if (dr["Type"].ToString().Trim() == "RM")
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                BscDtDimension.Visible = true;
                                purchValNOrder.Visible = true;
                                MRPLotSize.Visible = true;
                                ForeignTradeData.Visible = true;
                                PlantShelfLifeDt.Visible = true;
                                SalesData.Visible = true;
                                trPlantDeliveryTime.Visible = true;

                                lblGR.Text = "GR Proc. Time*";
                                inputGRProcTimeMRP1.Attributes.Add("required", "true");

                                btnSave.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();
                                inputNetWeight.Text = dr["NetWeight"].ToString().Trim();
                                inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim();

                                inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();

                                inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                                ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                                inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();

                                inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                                inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                                inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();

                                inputLoadingGrp.Text = dr["Plant"].ToString().Trim();
                                if (ddListSLED.Text == "D")
                                {
                                    lblMinRemShelfLife.Text = "DAY";
                                }
                                else
                                {
                                    lblMinRemShelfLife.Text = "MONTH";
                                }
                            }
                            //Create New SF/FG
                            else
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                purchValNOrder.Visible = true;

                                btnSave.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();

                                inputLoadingGrp.Text = dr["Plant"].ToString().Trim();
                            }
                        }
                        else
                        {
                            // update Button
                            if (dr["Type"].ToString().Trim() == "RM")
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                BscDtDimension.Visible = true;
                                purchValNOrder.Visible = true;
                                MRPLotSize.Visible = true;
                                ForeignTradeData.Visible = true;
                                PlantShelfLifeDt.Visible = true;
                                SalesData.Visible = true;
                                trPlantDeliveryTime.Visible = true;

                                lblGR.Text = "GR Proc. Time*";
                                inputGRProcTimeMRP1.Attributes.Add("required", "true");

                                btnSave.Visible = false;
                                btnUpdate.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                                inputNetWeight.Text = dr["NetWeight"].ToString().Trim();
                                inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim();

                                inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();

                                inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                                ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                                inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();

                                inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                                inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                                inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();

                                inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();
                                if (ddListSLED.Text == "D")
                                {
                                    lblMinRemShelfLife.Text = "DAY";
                                }
                                else
                                {
                                    lblMinRemShelfLife.Text = "MONTH";
                                }
                            }
                            //UPDATE SF/FG
                            else
                            {
                                listViewProc.Visible = false;

                                rmContent.Visible = true;
                                bscDt1GnrlDt.Visible = true;
                                purchValNOrder.Visible = true;

                                btnSave.Visible = false;
                                btnUpdate.Visible = true;

                                inputType.Text = dr["Type"].ToString().Trim();
                                inputMatTyp.Text = dr["MatType"].ToString().Trim();
                                lblTransID.Text = dr["TransID"].ToString().Trim();
                                inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                                inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                                inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                                inputBun.Text = dr["UoM"].ToString().Trim();
                                inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                                inputPlant.Text = dr["Plant"].ToString().Trim();
                                inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                                inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                                inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                                inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                                if (inputGRProcTimeMRP1.Text == "")
                                {
                                    inputGRProcTimeMRP1.Text = "1";
                                }
                                inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();

                                inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();
                            }
                        }
                    }
                    conMatWorkFlow.Close();

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
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString(), this.Page, this);
                }
            }
            else
            {
                MsgBox(this.lblUser.Text + " username are not for Procurement division", this.Page, this);
                Response.Redirect("~/Pages/procPage");
            }
            tmpBindRepeater();
            srcPurcGrpModalBinding();
            srcLoadingGrpModalBinding();
            srcPurcValKeyModalBinding();
            srcWeightUntModalBinding();
            srcVolUntModalBinding();
            srcCommImpModalBinding();
            srcAunModalBinding();
            srcPckgMatModalBinding();
            srcNetWeightUntModalBinding();
            bindLblBsUntMeas();
            bindLblPackMat();
            bindLblCommImp();
            bindLblPurcGrp();
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

            otrInputTbl.Visible = false;
            reptUntMeas.Visible = false;
            imgBtnPckgMat.Visible = false;
            imgBtnPurcGroup.Visible = false;
            imgBtnPurcValKey.Visible = false;
            //Td1.Visible = false;
            imgBtnCommImp.Visible = false;
            imgBtnLoadingGrp.Visible = false;
            reptUntMeasMgr.Visible = true;

            inputType.ReadOnly = true;
            inputMatTyp.ReadOnly = true;
            inputMtrlID.ReadOnly = true;
            inputMtrlDesc.ReadOnly = true;
            inputBsUntMeas.ReadOnly = true;
            inputBun.ReadOnly = true;
            inputOldMtrlNum.ReadOnly = true;
            inputPlant.ReadOnly = true;
            inputPckgMat.ReadOnly = true;

            inputNetWeight.ReadOnly = true;
            inputNetWeightUnit.ReadOnly = true;

            inputCommImpCode.ReadOnly = true;

            inputMinRemShLf.ReadOnly = true;
            inputTotalShelfLife.ReadOnly = true;

            inputMinLotSize.ReadOnly = true;
            inputRoundValue.ReadOnly = true;

            inputPurcGrp.ReadOnly = true;
            inputPurcValKey.ReadOnly = true;
            inputGRProcTimeMRP1.ReadOnly = true;
            inputPlantDeliveryTime.ReadOnly = true;
            inputMfrPrtNum.ReadOnly = true;

            inputLoadingGrp.ReadOnly = true;

            inputType.CssClass = "txtBoxRO";
            inputMatTyp.CssClass = "txtBoxRO";
            inputMtrlID.CssClass = "txtBoxRO";
            inputMtrlDesc.CssClass = "txtBoxRO";
            inputBsUntMeas.CssClass = "txtBoxRO";
            inputBun.CssClass = "txtBoxRO";
            inputOldMtrlNum.CssClass = "txtBoxRO";
            inputPlant.CssClass = "txtBoxRO";
            inputPckgMat.CssClass = "txtBoxRO";

            inputNetWeight.CssClass = "txtBoxRO";
            inputNetWeightUnit.CssClass = "txtBoxRO";

            inputCommImpCode.CssClass = "txtBoxRO";

            inputMinRemShLf.CssClass = "txtBoxRO";
            inputTotalShelfLife.CssClass = "txtBoxRO";

            inputMinLotSize.CssClass = "txtBoxRO";
            inputRoundValue.CssClass = "txtBoxRO";

            inputPurcGrp.CssClass = "txtBoxRO";
            inputPurcValKey.CssClass = "txtBoxRO";
            inputGRProcTimeMRP1.CssClass = "txtBoxRO";
            inputPlantDeliveryTime.CssClass = "txtBoxRO";
            inputMfrPrtNum.CssClass = "txtBoxRO";

            inputLoadingGrp.CssClass = "txtBoxRO";

            inputType.Attributes.Add("placeholder", "");
            inputMatTyp.Attributes.Add("placeholder", "");
            inputMtrlID.Attributes.Add("placeholder", "");
            inputMtrlDesc.Attributes.Add("placeholder", "");
            inputBsUntMeas.Attributes.Add("placeholder", "");
            inputBun.Attributes.Add("placeholder", "");
            inputOldMtrlNum.Attributes.Add("placeholder", "");
            inputPlant.Attributes.Add("placeholder", "");
            inputPckgMat.Attributes.Add("placeholder", "");

            inputNetWeight.Attributes.Add("placeholder", "");
            inputNetWeightUnit.Attributes.Add("placeholder", "");

            inputCommImpCode.Attributes.Add("placeholder", "");

            inputMinRemShLf.Attributes.Add("placeholder", "");
            inputTotalShelfLife.Attributes.Add("placeholder", "");

            inputMinLotSize.Attributes.Add("placeholder", "");
            inputRoundValue.Attributes.Add("placeholder", "");

            inputPurcGrp.Attributes.Add("placeholder", "");
            inputPurcValKey.Attributes.Add("placeholder", "");
            inputGRProcTimeMRP1.Attributes.Add("placeholder", "");
            inputPlantDeliveryTime.Attributes.Add("placeholder", "");
            inputMfrPrtNum.Attributes.Add("placeholder", "");

            inputLoadingGrp.Attributes.Add("placeholder", "");

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
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
                    Value = grdrow.Cells[1].Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 18
                });
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // update Button
                    if (dr["Type"].ToString().Trim() == "RM")
                    {
                        listViewProc.Visible = false;

                        rmContent.Visible = true;
                        bscDt1GnrlDt.Visible = true;
                        BscDtDimension.Visible = true;
                        purchValNOrder.Visible = true;
                        MRPLotSize.Visible = true;
                        ForeignTradeData.Visible = true;
                        PlantShelfLifeDt.Visible = true;
                        SalesData.Visible = true;
                        trPlantDeliveryTime.Visible = true;

                        btnClose.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;

                        inputType.Text = dr["Type"].ToString().Trim();
                        inputMatTyp.Text = dr["MatType"].ToString().Trim();
                        lblTransID.Text = dr["TransID"].ToString().Trim();
                        inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                        inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                        inputBun.Text = dr["UoM"].ToString().Trim();
                        inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputPlant.Text = dr["Plant"].ToString().Trim();
                        inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputNetWeight.Text = dr["NetWeight"].ToString().Trim();
                        inputNetWeightUnit.Text = dr["NetUnit"].ToString().Trim();

                        inputCommImpCode.Text = dr["ForeignTrade"].ToString().Trim();

                        inputMinRemShLf.Text = dr["MinShelfLife"].ToString().Trim();
                        ddListSLED.SelectedValue = dr["PeriodIndForSELD"].ToString().Trim().ToUpper();
                        inputTotalShelfLife.Text = dr["TotalShelfLife"].ToString().Trim();

                        inputMinLotSize.Text = dr["MinLotSize"].ToString().Trim();
                        inputRoundValue.Text = dr["RoundingValue"].ToString().Trim();

                        inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                        inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                        inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                        if (inputGRProcTimeMRP1.Text == "")
                        {
                            inputGRProcTimeMRP1.Text = "1";
                        }
                        inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();

                        inputLoadingGrp.Text = dr["LoadGrp"].ToString().Trim();

                        inputPlantDeliveryTime.Text = dr["PlantDeliveryTime"].ToString().Trim();

                        if (ddListSLED.Text == "D")
                        {
                            lblMinRemShelfLife.Text = "DAY";
                        }
                        else
                        {
                            lblMinRemShelfLife.Text = "MONTH";
                        }
                    }
                    //UPDATE SF/FG
                    else
                    {
                        listViewProc.Visible = false;

                        rmContent.Visible = true;
                        bscDt1GnrlDt.Visible = true;
                        purchValNOrder.Visible = true;

                        btnClose.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;

                        inputType.Text = dr["Type"].ToString().Trim();
                        inputMatTyp.Text = dr["MatType"].ToString().Trim();
                        lblTransID.Text = dr["TransID"].ToString().Trim();
                        inputMtrlID.Text = dr["MaterialID"].ToString().Trim();
                        inputMtrlDesc.Text = dr["MaterialDesc"].ToString().Trim();
                        inputBsUntMeas.Text = dr["UoM"].ToString().Trim();
                        inputBun.Text = dr["UoM"].ToString().Trim();
                        inputOldMtrlNum.Text = dr["OldMatNumb"].ToString().Trim();
                        inputPlant.Text = dr["Plant"].ToString().Trim();
                        inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim();

                        inputPurcGrp.Text = dr["PurchGrp"].ToString().Trim();
                        inputPurcValKey.Text = dr["PurchValueKey"].ToString().Trim();
                        inputGRProcTimeMRP1.Text = dr["GRProcessingTime"].ToString().Trim();
                        if (inputGRProcTimeMRP1.Text == "")
                        {
                            inputGRProcTimeMRP1.Text = "1";
                        }
                        inputMfrPrtNum.Text = dr["MfrPartNumb"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
            conMatWorkFlow.Close();
            tmpBindRepeater();
            srcPurcGrpModalBinding();
            srcLoadingGrpModalBinding();
            srcPurcValKeyModalBinding();
            srcWeightUntModalBinding();
            srcVolUntModalBinding();
            srcCommImpModalBinding();
            srcAunModalBinding();
            srcPckgMatModalBinding();
            srcNetWeightUntModalBinding();
            bindLblBsUntMeas();
            bindLblPackMat();
            bindLblCommImp();
            bindLblPurcGrp();

            ListItem li = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
            ddListSLED.Items.Clear();
            ddListSLED.Items.Add(li);
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
                    if (columnProcEnd == "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Open for Procurement";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";

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
                    else if (columnProcEnd != "&nbsp;" && columnProcStart != "&nbsp;")
                    {
                        ((Label)e.Row.FindControl("lblStatus")).Text = "Closed for Procurement";
                        ((Label)e.Row.FindControl("lblGlobalStatus")).Text = "Dept Const";
                        string lblNotes = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Proc/", "");
                        ((Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                        ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;

                        if (columnPlanEnd != "&nbsp;")
                        {
                            string lblNotesIn = ((Label)e.Row.FindControl("lblNotes")).Text.Trim().Replace("Plan/", "");
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
        protected void srcLoadingGrpModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcLoadingGroup", conMatWorkFlow);
            cmd.Parameters.Add("@TransID", SqlDbType.NVarChar).Value = this.lblTransID.Text;
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewLoadingGrp.DataSource = ds.Tables[0];
            GridViewLoadingGrp.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcPurcGrpModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_PurchasingGroup"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewPurcGrp.DataSource = ds.Tables[0];
            GridViewPurcGrp.DataBind();
            conMatWorkFlow.Close();
        }
        protected void srcPurcValKeyModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "SELECT * FROM Mstr_PurchasingValueKey"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewPurcValKey.DataSource = ds.Tables[0];
            GridViewPurcValKey.DataBind();
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
        protected void srcAunModalBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            var queryString = "Select IT.*  from Mstr_UoM IT WHERE IT.UoM NOT IN (SELECT QD.Aun FROM Tbl_DetailUomMat QD WHERE QD.TransID = @TransID AND QD.MaterialID = @MaterialID)"; //return data from UserApp table
            var dataAdapter = new SqlDataAdapter(queryString, conMatWorkFlow);
            dataAdapter.SelectCommand.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            dataAdapter.SelectCommand.Parameters.AddWithValue("MaterialID", this.inputMtrlID.Text);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewAun.DataSource = ds.Tables[0];
            GridViewAun.DataBind();
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

        //Modal
        protected void selectPckgMat_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputPckgMat.Text = grdrow.Cells[0].Text;
            lblPckgMat.Text = grdrow.Cells[1].Text;
            lblPckgMat.ForeColor = Color.Black;
            inputPckgMat.Focus();
        }
        protected void selectLoadingGrp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputLoadingGrp.Text = grdrow.Cells[0].Text;
            lblLoadingGrp.Text = grdrow.Cells[1].Text;
            inputLoadingGrp.Focus();
        }
        protected void selectPurcGrp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputPurcGrp.Text = grdrow.Cells[0].Text;
            lblPurcGrp.Text = grdrow.Cells[1].Text;
            inputPurcGrp.Focus();
        }
        protected void selectPurcValKey_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputPurcValKey.Text = grdrow.Cells[0].Text;
            lblPurcValKey.Text = "";
            inputPurcValKey.Focus();
        }
        protected void selectWeightUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputWeightUnt.Text = grdrow.Cells[0].Text;
            lblWeightUnt.Text = grdrow.Cells[1].Text;
            lblWeightUnit.ForeColor = Color.Black;
            inputWeightUnt.Focus();
        }
        protected void selectNetWeightUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputNetWeightUnit.Text = grdrow.Cells[0].Text;
            inputWeightUnt.Text = grdrow.Cells[0].Text;
            lblNetWeightUnit.Text = grdrow.Cells[1].Text;
            //lblWeightUnit.Text = grdrow.Cells[1].Text;
            lblNetWeightUnit.ForeColor = Color.Black;
            inputNetWeightUnit.Focus();
        }
        protected void selectVolUnt_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputVolUnt.Text = grdrow.Cells[0].Text;
            lblVolUnt.Text = grdrow.Cells[1].Text;
            inputVolUnt.Focus();
        }
        protected void selectCommImp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputCommImpCode.Text = grdrow.Cells[0].Text;
            lblCommImpCode.Text = grdrow.Cells[1].Text;
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
                Value = inputMtrlID.Text,
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
            dt = GetLastLineDetailUom(lblTransID.Text.Trim(), inputMtrlID.Text.Trim().ToUpper());
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

            if (inputMtrlID.Text == "")
            {
                MsgBox("Please insert your Material ID first.", this.Page, this);
                inputMtrlID.Focus();
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
                param[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("selectAllMaterial_ByMaterialID", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your MaterialID " + this.inputMtrlID.Text + " is already Exist!", this.Page, this);
                }
                else
                {
                    if (inputGrossWeight.Text != "0")
                    {
                        // doing checking
                        DataTable dtCheckB = new DataTable();
                        SqlParameter[] paramB = new SqlParameter[3];
                        paramB[0] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                        paramB[1] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
                        paramB[2] = new SqlParameter("@Aun", this.inputAun.Text.Trim().ToUpper());

                        //send param to SP sql
                        dtCheckB = sqlC.ExecuteDataTable("CheckXExist", paramB);

                        if (dtCheckB.Rows.Count > 0)
                        {
                            // munculkan pesan bahwa sudah ada
                            MsgBox("Your Aun " + this.inputAun.Text + " is already Exist or did not match Inspection Type master data!", this.Page, this);
                        }
                        else
                        {
                            //doing insert
                            SqlParameter[] paramm = new SqlParameter[14];
                            paramm[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
                            paramm[1] = new SqlParameter("@MaterialID", inputMtrlID.Text.Trim().ToUpper());
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

                            inputMtrlID.ReadOnly = true;
                            inputMtrlID.CssClass = "TxtBoxRO";
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
                    paramB[1] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
                    paramB[2] = new SqlParameter("@Aun", this.inputAun.Text.Trim().ToUpper());

                    //send param to SP sql
                    dtCheckB = sqlC.ExecuteDataTable("CheckXExist", paramB);

                    if (dtCheckB.Rows.Count > 0)
                    {
                        // munculkan pesan bahwa sudah ada
                        MsgBox("Your Aun " + this.inputAun.Text + " is already Exist or did not match Inspection Type master data!", this.Page, this);
                        return;
                    }
                    else
                    {
                        //doing insert
                        SqlParameter[] paramm = new SqlParameter[14];
                        paramm[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
                        paramm[1] = new SqlParameter("@MaterialID", inputMtrlID.Text.Trim().ToUpper());
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
                        inputMtrlID.ReadOnly = true;
                        inputMtrlID.CssClass = "TxtBoxRO";
                    }
                }
                else
                {
                    MsgBox("Gross Weight value cannot 0.", this.Page, this);
                    return;
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
            inputGrossWeight.ReadOnly = true;
            inputWeightUnt.ReadOnly = true;
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
                Value = inputMtrlID.Text,
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
                string x = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtX")).Text.Trim();
                string An = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtAun")).Text.Trim();
                string y = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtY")).Text.Trim();
                string Bn = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtBun")).Text.Trim();

                string gw = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtGrossWeight")).Text.Trim();
                string wu = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtWeightUnit")).Text.Trim();
                string v = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolume")).Text.Trim();
                string vu = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtVolUnit")).Text.Trim();

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
            // doing checking
            DataTable dtCheck = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
            param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

            //send param to SP sql
            dtCheck = sqlC.ExecuteDataTable("CheckDetailUom", param);

            if (dtCheck.Rows.Count == 0)
            {
                // munculkan pesan bahwa sudah ada
                MsgBox("Your detail Uom table cannot empty!", this.Page, this);
                return;
            }
            conMatWorkFlow.Close();

            conMatWorkFlow.Open();
            // doing checking
            DataTable dtCheckB = new DataTable();
            SqlParameter[] paramB = new SqlParameter[2];
            paramB[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
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

            conMatWorkFlow.Open();
            // doing checking
            DataTable dtCheckC = new DataTable();
            SqlParameter[] paramC = new SqlParameter[3];
            paramC[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
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

            if (inputType.Text.Trim() != "RM")
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

            try
            {
                conMatWorkFlow.Open();
                if (inputType.Text.Trim() == "RM")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET MatlGrpPack = @MatlGrpPack, PurchGrp = @PurchGrp, NetWeight = @NetWeight, PurchValueKey = @PurchValueKey, GRProcessingTime = @GRProcessingTime, MfrPartNumb = @MfrPartNumb, ForeignTrade = @ForeignTrade, MinShelfLife = @MinShelfLife, TotalShelfLife = @TotalShelfLife, MinLotSize = @MinLotSize, RoundingValue = @RoundingValue, LoadGrp = @LoadGrp, NewProc = @NewProc, PlantDeliveryTime=@PlantDeliveryTime WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PlantDeliveryTime",
                        Value = this.inputPlantDeliveryTime.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
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
                        ParameterName = "PurchGrp",
                        Value = this.inputPurcGrp.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "NewProc",
                        Value = "x",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 1
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
                        ParameterName = "NetWeight",
                        Value = this.inputNetWeight.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PurchValueKey",
                        Value = this.inputPurcValKey.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 20
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "GRProcessingTime",
                        Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MfrPartNumb",
                        Value = this.inputMfrPrtNum.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 15
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "ForeignTrade",
                        Value = this.inputCommImpCode.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 25
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinShelfLife",
                        Value = this.inputMinRemShLf.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TotalShelfLife",
                        Value = this.inputTotalShelfLife.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinLotSize",
                        Value = this.inputMinLotSize.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "RoundingValue",
                        Value = this.inputRoundValue.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 19
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "LoadGrp",
                        Value = this.inputLoadingGrp.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET MatlGrpPack = @MatlGrpPack , LoadGrp = @LoadGrp, PurchGrp = @PurchGrp, PurchValueKey = @PurchValueKey, GRProcessingTime = @GRProcessingTime, MfrPartNumb = @MfrPartNumb, NewProc = @NewProc WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MatlGrpPack",
                        Value = this.inputPckgMat.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 4
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "LoadGrp",
                        Value = this.inputLoadingGrp.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PurchGrp",
                        Value = this.inputPurcGrp.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "NewProc",
                        Value = "x",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 1
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
                        ParameterName = "GrossWeight",
                        Value = this.inputGrossWeight.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "NetWeight",
                        Value = this.inputNetWeight.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "WeightUnit",
                        Value = this.inputWeightUnt.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Volume",
                        Value = this.inputVolume.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "VolUnit",
                        Value = this.inputVolUnt.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PurchValueKey",
                        Value = this.inputPurcValKey.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 20
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "GRProcessingTime",
                        Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MfrPartNumb",
                        Value = this.inputMfrPrtNum.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 15
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "ForeignTrade",
                        Value = this.inputCommImpCode.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 15
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinShelfLife",
                        Value = this.inputMinRemShLf.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TotalShelfLife",
                        Value = this.inputTotalShelfLife.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinLotSize",
                        Value = this.inputMinLotSize.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "RoundingValue",
                        Value = this.inputRoundValue.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 19
                    });
                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                }

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
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET ProcEnd = @ProcEnd WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
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
                    ParameterName = "ProcEnd",
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
            Response.Redirect("~/Pages/procPage");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            autoGenLogID();
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

            if (inputNetWeight.Text == "0")
            {
                MsgBox("Net Weight cannot be 0!", this.Page, this);
                return;
            }
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            if (inputType.Text == "RM")
            {
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
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
            paramB[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
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

            conMatWorkFlow.Open();
            // doing checking
            DataTable dtCheckC = new DataTable();
            SqlParameter[] paramC = new SqlParameter[3];
            paramC[0] = new SqlParameter("@MaterialID", this.inputMtrlID.Text.Trim().ToUpper());
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

            if (inputType.Text.Trim() != "RM")
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
            try
            {
                conMatWorkFlow.Open();
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
                SqlCommand cmdTracking = new SqlCommand("UPDATE Tbl_Tracking SET ProcEnd = @ProcEnd WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
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
                    ParameterName = "ProcEnd",
                    Value = DateTime.Now,
                    SqlDbType = SqlDbType.DateTime
                });
                cmdTracking.ExecuteNonQuery();
                conMatWorkFlow.Close();

                if (inputType.Text.Trim() == "RM")
                {
                    conMatWorkFlow.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET MatlGrpPack = @MatlGrpPack, NetWeight = @NetWeight, PurchGrp = @PurchGrp, PurchValueKey = @PurchValueKey, GRProcessingTime = @GRProcessingTime, MfrPartNumb = @MfrPartNumb, ForeignTrade = @ForeignTrade, MinShelfLife = @MinShelfLife, TotalShelfLife = @TotalShelfLife, MinLotSize = @MinLotSize, RoundingValue = @RoundingValue, LoadGrp = @LoadGrp, NewProc = @NewProc, PlantDeliveryTime=@PlantDeliveryTime WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PlantDeliveryTime",
                        Value = this.inputPlantDeliveryTime.Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
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
                        ParameterName = "PurchGrp",
                        Value = this.inputPurcGrp.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "NewProc",
                        Value = "x",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 1
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
                        ParameterName = "NetWeight",
                        Value = this.inputNetWeight.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PurchValueKey",
                        Value = this.inputPurcValKey.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 20
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "GRProcessingTime",
                        Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MfrPartNumb",
                        Value = this.inputMfrPrtNum.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 15
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "ForeignTrade",
                        Value = this.inputCommImpCode.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 15
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinShelfLife",
                        Value = this.inputMinRemShLf.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TotalShelfLife",
                        Value = this.inputTotalShelfLife.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinLotSize",
                        Value = this.inputMinLotSize.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "RoundingValue",
                        Value = this.inputRoundValue.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 19
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "LoadGrp",
                        Value = this.inputPlant.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                }
                else
                {
                    conMatWorkFlow.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Tbl_Material SET MatlGrpPack = @MatlGrpPack, PurchGrp = @PurchGrp, LoadGrp=@LoadGrp, PurchValueKey = @PurchValueKey, GRProcessingTime = @GRProcessingTime, MfrPartNumb = @MfrPartNumb, NewProc = @NewProc WHERE TransID = @TransID AND MaterialID = @MaterialID", conMatWorkFlow);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MatlGrpPack",
                        Value = this.inputPckgMat.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 4
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PurchGrp",
                        Value = this.inputPurcGrp.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "LoadGrp",
                        Value = this.inputPlant.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "NewProc",
                        Value = "x",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 1
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
                        ParameterName = "GrossWeight",
                        Value = this.inputGrossWeight.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "NetWeight",
                        Value = this.inputNetWeight.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "WeightUnit",
                        Value = this.inputWeightUnt.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Volume",
                        Value = this.inputVolume.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "VolUnit",
                        Value = this.inputVolUnt.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "PurchValueKey",
                        Value = this.inputPurcValKey.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 20
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "GRProcessingTime",
                        Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MfrPartNumb",
                        Value = this.inputMfrPrtNum.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 15
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "ForeignTrade",
                        Value = this.inputCommImpCode.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 15
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinShelfLife",
                        Value = this.inputMinRemShLf.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TotalShelfLife",
                        Value = this.inputTotalShelfLife.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MinLotSize",
                        Value = this.inputMinLotSize.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "RoundingValue",
                        Value = this.inputRoundValue.Text.ToUpper().Trim(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 19
                    });
                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();
                }

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
            }
            Response.Redirect("~/Pages/procPage");
        }
        protected void Reject_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/procPage");
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
            Response.Redirect("~/Pages/procPage");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/procPage");
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
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputLoadingGrp_onBlur
        protected void inputLoadingGrp_onBlur(object sender, EventArgs e)
        {
            if (inputLoadingGrp.Text == "")
            {
                lblLoadingGrp.Text = absLoadingGrp.Text;
                lblLoadingGrp.ForeColor = Color.Black;
            }
            else if (inputLoadingGrp.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_LoadingGroup WHERE LoadGrp = @inputLoadingGrp";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputLoadingGrp", this.inputLoadingGrp.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblLoadingGrp.Text = dr["LoadGrpDesc"].ToString();
                    lblLoadingGrp.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblLoadingGrp.Text = "Wrong Input!";
                    lblLoadingGrp.ForeColor = Color.Red;
                    inputLoadingGrp.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputPurcValKey_onBlur
        protected void inputPurcGrp_onBlur(object sender, EventArgs e)
        {
            if (inputPurcGrp.Text == "")
            {

            }
            else if (inputPurcGrp.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_PurchasingGroup WHERE PurchGrp = @PurchGrp";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("PurchGrp", this.inputPurcGrp.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPurcGrp.Text = dr["PurchGrpDesc"].ToString();
                    lblPurcGrp.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblPurcGrp.Text = "Wrong Input!";
                    lblPurcGrp.ForeColor = Color.Red;
                    inputPurcGrp.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputPurcValKey_onBlur
        protected void inputPurcValKey_onBlur(object sender, EventArgs e)
        {
            if (inputPurcValKey.Text == "")
            {
                inputPurcValKey.Text = "";
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
                    MsgBox("Wrong Input!", this.Page, this);
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
                    MsgBox("Wrong Input!", this.Page, this);
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
                    inputWeightUnt.Text = dr["UoM"].ToString();
                    //lblWeightUnit.Text = dr["UoM_Desc"].ToString();
                    lblNetWeightUnit.Text = dr["UoM_Desc"].ToString();
                    lblNetWeightUnit.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblNetWeightUnit.Text = "Wrong Input!";
                    lblNetWeightUnit.ForeColor = Color.Red;
                    inputNetWeightUnit.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
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
                    MsgBox("Wrong Input!", this.Page, this);
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
                    MsgBox("Wrong Input!", this.Page, this);
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
                    MsgBox("Wrong Input!", this.Page, this);
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
                    txtAun.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
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
                    txtBun.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
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
        //inputNetWeight_onBlur
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
                inputNetWeightUnit.Text = dr["UoM"].ToString();
                inputWeightUnt.Text = dr["UoM"].ToString();
                lblBsUntMeas.Text = dr["UoM_Desc"].ToString();
                lblBMeas.Text = dr["UoM_Desc"].ToString();
                lblNetWeightUnit.Text = dr["UoM_Desc"].ToString();
                //lblWeightUnit.Text = dr["UoM_Desc"].ToString();
                lblBsUntMeas.ForeColor = Color.Black;
                lblBMeas.ForeColor = Color.Black;
                lblNetWeightUnit.ForeColor = Color.Black;
                lblWeightUnit.ForeColor = Color.Black;
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
                var dt = GetData_srcListViewRMSFFG("R&D", "", lblPosition.Text.Trim(), "RM");

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
                var dt = GetData_srcListViewRMSFFG("R&D", "", lblPosition.Text.Trim(), "SF");

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
                var dt = GetData_srcListViewRMSFFG("R&D", "", lblPosition.Text.Trim(), "FG");

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
            DataTable dt = GetData_srcListViewMatID("%" + inputMatIDDESC.Text.ToUpper().Trim() + "%", Session["UsnamPlant"].ToString(), lblPosition.Text.Trim());

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