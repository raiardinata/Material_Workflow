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
    public partial class sampleMaterial : System.Web.UI.Page
    {
        string stringCoProd = "";
        string stringFxdPrice = "";
        string stringDontCost = "";
        string stringWithQtyStruct = "";
        string stringMatOrigin = "";
        string stringInspectSet = "0";
        string chkbxValue;
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
                if (lblPosition.Text != "R&D" && lblPosition.Text != "R&D MGR" && lblPosition.Text != "PD" && lblPosition.Text != "PD MGR")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
                srcListViewBinding();
                if (Session["Devisi"].ToString().Trim() == "R&D MGR")
                {
                    tdCreateMaterial.Visible = false;
                    createMat.Visible = false;
                    //rmsffgAspMenu.Visible = false;
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
                int intIndexMenu = Convert.ToInt32(idxMenu.Text);
                //srcListViewBinding();
                FICONavigationMenu.Items[intIndexMenu].Selected = true;

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

        //CLASSNTYP Code
        protected void btnAddClassnTyp_Click(object sender, EventArgs e)
        {
            autoGenLineClassType();

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            // doing checking
            DataTable dtCheckA = new DataTable();
            SqlParameter[] paramA = new SqlParameter[2];
            paramA[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
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
            }
            else
            {
                conMatWorkFlow.Open();
                // doing checking
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                param[2] = new SqlParameter("@Class", this.inputClass.Text.Trim().ToUpper());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("CheckClass", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your Class " + this.inputClass.Text + " is already Exist or did not match Classification master data!", this.Page, this);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_QCCLASS(TransID, MaterialID, Line, ClassType, ClassNo, CreateBy, CreateTime, New) VALUES(@TransID, @MaterialID, @Line, @ClassType, @ClassNo, @CreateBy, @CreateTime, @New)", conMatWorkFlow);
                    cmd.Parameters.AddWithValue("@TransID", lblTransID.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@MaterialID", inputMatID.Text.Trim().ToUpper());
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
                Value = inputMatID.Text,
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
                Value = inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet dsMgr = new DataSet();
            SqlDataAdapter adpMgr = new SqlDataAdapter(cmdMgr);
            adpMgr.Fill(dsMgr);
            rptClassTypeMgr.DataSource = dsMgr;
            rptClassTypeMgr.DataBind();
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
            dt = GetLastLineClassType(lblTransID.Text.Trim(), inputMatID.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                num1 = dt.Rows[0]["Line"].ToString();
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLineClassType.Text = num1;
            }
            else
            {
                num1 = lblLineInspectionType.Text;
                num1 = string.Format("LN{0}", (Convert.ToUInt32(num1.Substring(3)) + 1).ToString("D3"));
                lblLineClassType.Text = num1;
            }
        }
        //InspectionType Code
        protected void btnAddInspectionType_Click(object sender, EventArgs e)
        {
            autoGenLineInspectType();

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            if (txtInspectionType.Text == "")
            {
                MsgBox("Inspection Type is empty, cannot add empty data.", this.Page, this);
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
                    MsgBox("Your Inspection Type " + this.txtInspectionType.Text + " is already Exist or did not match Inspection Type master data!", this.Page, this);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_QCData(TransID, MaterialID, Line, InspType, CreateBy, CreateTime, New) VALUES(@TransID, @MaterialID, @Line, @InspType, @CreateBy, @CreateTime, @New)", conMatWorkFlow);
                    cmd.Parameters.AddWithValue("@TransID", lblTransID.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@MaterialID", inputMatID.Text.Trim().ToUpper());
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
                Value = inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            rptInspectionType.DataSource = ds;
            rptInspectionType.DataBind();
            rptInspectionTypeMgr.DataSource = ds;
            rptInspectionTypeMgr.DataBind();
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

        //Create Sample Material
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
                FilterSearch.Text = "RM";
                gridSearch(FilterSearch.Text);
            }
            else if (e.Item.Value == "1")
            {
                FilterSearch.Text = "SF";
                gridSearch(FilterSearch.Text);
            }
            else if (e.Item.Value == "2")
            {
                FilterSearch.Text = "FG";
                gridSearch(FilterSearch.Text);
            }
            rmsffgMenuLabel.Text = rmsffgAspMenu.SelectedItem.Text.Trim().ToUpper();
        }
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
                rndTBL.Visible = true;

                procTBL.Visible = false;
                plannerTbl.Visible = false;
                QCTbl.Visible = false;
                QATbl.Visible = false;
                QRTbl.Visible = false;
                idxMenu.Text = "0";
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
                idxMenu.Text = "1";
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
                MRP1.Visible = true;
                MRP2.Visible = true;
                MRP3.Visible = true;
                idxMenu.Text = "2";
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
                idxMenu.Text = "3";
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
                idxMenu.Text = "4";
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
                idxMenu.Text = "5";
            }
        }

        //Binding List View Code Public
        protected void createNewMat_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            autoGenTransID();
            spanTransID.Style.Add("display", "flex");
            Master.FindControl("NavigationMenu").Visible = false;
            Master.FindControl("btnLogOut").Visible = false;


            listViewFico.Visible = false;

            rmContent.Visible = true;
            divApprMn.Visible = true;

            //R&D
            if (rmsffgMenuLabel.Text == "RM")
            {
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;

                lblGR.Text = "GR Proc. Time*";
                inputGRProcTimeMRP1.Attributes.Add("required", "true");
            }
            else if (rmsffgMenuLabel.Text == "SF")
            {
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;
                bscDt1Dimension.Visible = true;
                MRP1LOTSizeDt.Visible = true;
                foreignTradeDt.Visible = true;
                plantShelfLifeDt.Visible = true;
                trCoProd.Visible = true;
                inputMatType.CssClass = "txtBox";
                inputMatType.ReadOnly = false;
                imgBtnMatType.Visible = true;
            }
            else
            {
                bscDt1GnrlDt.Visible = true;
                orgLv.Visible = true;
                MRP2Proc.Visible = true;
                bscDt1Dimension.Visible = true;
                MRP1LOTSizeDt.Visible = true;
                foreignTradeDt.Visible = true;
                plantShelfLifeDt.Visible = true;
                trCoProd.Visible = true;
            }
            srcSpcProcModalBinding();
            srcVolUntModalBinding();
            srcMatTypModalBinding("X");
            srcIndStdDescModalBinding();
            srcPckgMatModalBinding();
            srcMatGrModalBinding();
            srcSalesOrgModalBinding();
            srcStorLocModalBinding();
            srcDistrChlModalBinding();
            srcAunModalBinding();
            srcBsUntMeasModalBinding();
            srcDivisionModalBinding();
            srcProcTypeModalBinding();
            srcWeightUntModalBinding();
            srcNetWeightUntModalBinding();
            srcCommImpModalBinding();
            srcLabOfficeModalBinding();
            srcMRPTypModalBinding();
            srcLOTSizeModalBinding();
            srcStrategyGroupModalBinding();
            srcInspectionTypeModalBinding();
            srcClassModalBinding();
            //srcClassTypModalBinding();
            srcStoreCondModalBinding();

            tmpBindRepeater();
            ClassBindRepeater();
            QCDataBindRepeater();

            //Proc
            srcLoadingGrpModalBindingProc();
            srcPurcGrpModalBinding();
            srcPurcValKeyModalBinding();

            if (rmsffgMenuLabel.Text == "RM")
            {
                inputMatType.Text = "RMRD";
            }
            else if (rmsffgMenuLabel.Text == "SF")
            {
                inputMatType.Text = "SFRD";
            }
            else if (rmsffgMenuLabel.Text == "FG")
            {
                inputMatType.Text = "FMRD";
            }
            bindLblMatType();
        }
        protected void srcListview_Click(object sender, ImageClickEventArgs e)
        {
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
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
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
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
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
                    lblPosition.Text == "R&D MGR")
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
                    lblPosition.Text == "R&D MGR")
                {
                    if (columnStatus == "REVISIONRNDAPPROVAL")
                    {
                        ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
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
                else if (columnRDStart != "&nbsp" && columnStatus == "REVISIONRND" && lblPosition.Text == "R&D MGR")
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblGlobalStatus")).Text = "R&D";
                    string lblNotes = "";
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblNotes")).Text = lblNotes;
                    ((LinkButton)e.Row.FindControl("slcThsMatIDDESC")).Visible = false;
                }
                //cek jika revisi rnd sudah di save siap untuk di approve
                else if (columnRDStart != "&nbsp" && columnStatus == "REVISIONRNDAPPROVAL" && lblPosition.Text == "R&D MGR")
                {
                    ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblStatus")).Text = "Waiting for Approval";
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
                    lblPosition.Text == "R&D MGR")
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
        //Specific
        protected void modifyThisSample_Click(object sender, EventArgs e)
        {
            autoGenLogID();
            spanTransID.Style.Add("display", "flex");
            Master.FindControl("NavigationMenu").Visible = false;
            Master.FindControl("btnLogOut").Visible = false;
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            LinkButton display = (LinkButton)grdrow.FindControl("display");
            string TransID = display.Text;

            listViewFico.Visible = false;

            rmContent.Visible = true;
            divApprMn.Visible = true;

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmdA = new SqlCommand("SELECT GrossWeight FROM Tbl_DetailUomMat WHERE TransID = @TransID AND MaterialID = @MaterialID AND Line = 'LN001'", conMatWorkFlow);
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
            SqlDataReader drA = cmdA.ExecuteReader();
            while (drA.Read())
            {
                inputGrossWeightRnd.Text = drA["GrossWeight"].ToString().Trim();
                inputGrossWeightRnd.CssClass = "txtBoxRO";
            }
            if (drA.HasRows == false)
            {
                inputGrossWeightRnd.Text = "0";
            }
            conMatWorkFlow.Close();

            if (lblPosition.Text.ToUpper().Trim() == "R&D")
            {
                lblUpdate.Text = "X";
                inputMatID.ReadOnly = true;
                inputMatType.ReadOnly = true;
                inputMatType.CssClass = "txtBoxRO";
                inputMatID.CssClass = "txtBoxRO";

                btnSave.Visible = false;
                btnUpdate.Visible = true;

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
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                        lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                        inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                        inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                        inputBsUntMeasRnd.Text = dr["UoM"].ToString().Trim().ToUpper();
                        inputWeightUntRnd.Text = dr["UoM"].ToString().Trim().ToUpper();
                        inputBunRnd.Text = dr["UoM"].ToString().Trim().ToUpper();
                        inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                        inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                        inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                        inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                        inputMatType.Text = dr["MatType"].ToString().Trim().ToUpper();
                        inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                        inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                        inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                        inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                        inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                        stringCoProd = dr["COProd"].ToString().Trim();
                        inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                        inputNetWeightUnitRnd.Text = dr["NetUnit"].ToString().Trim().ToUpper();
                        inputCommImpCodeRnd.Text = dr["ForeignTrade"].ToString().Trim().ToUpper();
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
                        
                        if (inputProcType.Text.ToUpper().Trim() == "E")
                        {
                            chkbxCoProd.Enabled = true;
                        }
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
                            lblQMProcActive.Text = "Active";
                        }
                    }
                    conMatWorkFlow.Close();

                    if (rmsffgMenuLabel.Text.Trim().ToUpper() == "RM")
                    {
                        lblGR.Text = "GR Proc. Time*";
                        inputGRProcTimeMRP1.Attributes.Add("required", "true");
                    }

                    srcSpcProcModalBinding();
                    srcVolUntModalBinding();
                    srcMatTypModalBinding("X");
                    srcIndStdDescModalBinding();
                    srcPckgMatModalBinding();
                    srcMatGrModalBinding();
                    srcSalesOrgModalBinding();
                    srcStorLocModalBinding();
                    srcDistrChlModalBinding();
                    srcAunModalBinding();
                    srcBsUntMeasModalBinding();
                    srcDivisionModalBinding();
                    srcProcTypeModalBinding();
                    srcWeightUntModalBinding();
                    srcNetWeightUntModalBinding();
                    srcCommImpModalBinding();
                    srcLabOfficeModalBinding();
                    srcMRPGrModalBinding();
                    srcMRPTypModalBinding();
                    srcMRPCtrlModalBinding();
                    srcLOTSizeModalBinding();
                    srcProdStorLocModalBinding();
                    srcSchedMargKeyModalBinding();
                    srcProdSchedModalBinding();
                    srcProdSchedProfileModalBinding();
                    srcStrategyGroupModalBinding();
                    srcInspectionTypeModalBinding();
                    srcClassModalBinding();
                    //srcClassTypModalBinding();
                    srcStoreCondModalBinding();

                    //Proc
                    srcLoadingGrpModalBindingProc();
                    srcPurcGrpModalBinding();
                    srcPurcValKeyModalBinding();

                    tmpBindRepeater();
                    ClassBindRepeater();
                    QCDataBindRepeater();

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
                    bindLblStrategyGroup();
                    //QA
                    bindLblStorCondition();
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                }
            }
            else if (lblPosition.Text.ToUpper().Trim() == "R&D MGR")
            {
                inputMatID.ReadOnly = true;
                inputMatDesc.ReadOnly = true;
                inputBsUntMeasRnd.ReadOnly = true;
                inputMatGr.ReadOnly = true;
                inputOldMatNum.ReadOnly = true;
                inputDivision.ReadOnly = true;
                inputPckgMat.ReadOnly = true;
                inputCommImpCodeRnd.ReadOnly = true;
                inputMinRemShLf.ReadOnly = true;
                inputIndStdDesc.ReadOnly = true;
                chkbxInspectSet.Enabled = false;
                chkbxQMProcActive.Enabled = false;
                chkbxCoProd.Enabled = false;
                inputTotalShelfLife.ReadOnly = true;
                inputGrossWeightRnd.ReadOnly = true;
                inputNetWeight.ReadOnly = true;
                inputNetWeightUnitRnd.ReadOnly = true;
                inputWeightUntRnd.ReadOnly = true;
                inputVolumeRnd.ReadOnly = true;
                inputVolUntRnd.ReadOnly = true;
                inputMatType.ReadOnly = true;
                inputPlant.ReadOnly = true;
                inputStorLoc.ReadOnly = true;
                inputSalesOrg.ReadOnly = true;
                inputDistrChl.ReadOnly = true;
                inputMatDesc.ReadOnly = true;
                inputMinLotSize.ReadOnly = true;
                inputRoundValue.ReadOnly = true;
                inputProcType.ReadOnly = true;
                inputSpcProcRnd.ReadOnly = true;
                inputPurcGrp.ReadOnly = true;
                inputPurcValKey.ReadOnly = true;
                inputGRProcTimeMRP1.ReadOnly = true;
                inputPlantDeliveryTime.ReadOnly = true;
                inputMfrPrtNum.ReadOnly = true;
                inputLoadingGrp.ReadOnly = true;
                inputTotalLeadTime.ReadOnly = true;

                inputLabOffice.ReadOnly = true;
                inputMRPGr.ReadOnly = true;
                inputMRPTyp.ReadOnly = true;
                inputMRPCtrl.ReadOnly = true;
                inputLOTSize.ReadOnly = true;
                inputFixLotSize.ReadOnly = true;
                trFixLotSize.Visible = true;
                inputMaxStockLv.ReadOnly = true;
                inputProdStorLoc.ReadOnly = true;
                inputSchedMargKey.ReadOnly = true;
                inputSftyStck.ReadOnly = true;
                inputMinSftyStck.ReadOnly = true;
                inputStrtgyGr.ReadOnly = true;
                inputProdSched.ReadOnly = true;
                inputProdSchedProfile.ReadOnly = true;
                inputInspectIntrv.ReadOnly = true;
                inputStoreCond.ReadOnly = true;
                inputQMCtrlKey.ReadOnly = true;

                inputTotalLeadTime.CssClass = "txtBoxRO";
                inputIndStdDesc.CssClass = "txtBoxRO";
                inputSpcProcRnd.CssClass = "txtBoxRO";
                inputMatID.CssClass = "txtBoxRO";
                inputMatDesc.CssClass = "txtBoxRO";
                inputBsUntMeasRnd.CssClass = "txtBoxRO";
                inputMatGr.CssClass = "txtBoxRO";
                inputOldMatNum.CssClass = "txtBoxRO";
                inputDivision.CssClass = "txtBoxRO";
                inputPckgMat.CssClass = "txtBoxRO";
                inputCommImpCodeRnd.CssClass = "txtBoxRO";
                inputMinRemShLf.CssClass = "txtBoxRO";
                ddListSLED.CssClass = "txtBoxRO";
                inputTotalShelfLife.CssClass = "txtBoxRO";
                inputGrossWeightRnd.CssClass = "txtBoxRO";
                inputNetWeight.CssClass = "txtBoxRO";
                inputNetWeightUnitRnd.CssClass = "txtBoxRO";
                inputWeightUntRnd.CssClass = "txtBoxRO";
                inputVolumeRnd.CssClass = "txtBoxRO";
                inputVolUntRnd.CssClass = "txtBoxRO";
                inputMatType.CssClass = "txtBoxRO";
                inputPlant.CssClass = "txtBoxRO";
                inputStorLoc.CssClass = "txtBoxRO";
                inputSalesOrg.CssClass = "txtBoxRO";
                inputDistrChl.CssClass = "txtBoxRO";
                inputMatDesc.CssClass = "txtBoxRO";
                inputMinLotSize.CssClass = "txtBoxRO";
                inputRoundValue.CssClass = "txtBoxRO";
                inputProcType.CssClass = "txtBoxRO";
                inputPurcGrp.CssClass = "txtBoxRO";
                inputPurcValKey.CssClass = "txtBoxRO";
                inputGRProcTimeMRP1.CssClass = "txtBoxRO";
                inputPlantDeliveryTime.CssClass = "txtBoxRO";
                inputMfrPrtNum.CssClass = "txtBoxRO";
                inputLoadingGrp.CssClass = "txtBoxRO";
                inputLabOffice.CssClass = "txtBoxRO";
                inputMRPGr.CssClass = "txtBoxRO";
                inputMRPTyp.CssClass = "txtBoxRO";
                inputMRPCtrl.CssClass = "txtBoxRO";
                inputLOTSize.CssClass = "txtBoxRO";
                inputFixLotSize.CssClass = "txtBoxRO";
                inputMaxStockLv.CssClass = "txtBoxRO";
                inputProdStorLoc.CssClass = "txtBoxRO";
                inputSchedMargKey.CssClass = "txtBoxRO";
                inputSftyStck.CssClass = "txtBoxRO";
                inputMinSftyStck.CssClass = "txtBoxRO";
                inputStrtgyGr.CssClass = "txtBoxRO";
                inputProdSched.CssClass = "txtBoxRO";
                inputProdSchedProfile.CssClass = "txtBoxRO";
                inputInspectIntrv.CssClass = "txtBoxRO";
                inputStoreCond.CssClass = "txtBoxRO";
                inputQMCtrlKey.CssClass = "txtBoxRO";

                inputTotalLeadTime.Attributes.Add("placeholder", "");
                inputSpcProcRnd.Attributes.Add("placeholder", "");
                inputMatID.Attributes.Add("placeholder", "");
                inputMatDesc.Attributes.Add("placeholder", "");
                inputBsUntMeasRnd.Attributes.Add("placeholder", "");
                inputMatGr.Attributes.Add("placeholder", "");
                inputOldMatNum.Attributes.Add("placeholder", "");
                inputDivision.Attributes.Add("placeholder", "");
                inputPckgMat.Attributes.Add("placeholder", "");
                inputCommImpCodeRnd.Attributes.Add("placeholder", "");
                inputMinRemShLf.Attributes.Add("placeholder", "");
                ddListSLED.Attributes.Add("placeholder", "");
                inputTotalShelfLife.Attributes.Add("placeholder", "");
                inputGrossWeightRnd.Attributes.Add("placeholder", "");
                inputNetWeight.Attributes.Add("placeholder", "");
                inputNetWeightUnitRnd.Attributes.Add("placeholder", "");
                inputWeightUntRnd.Attributes.Add("placeholder", "");
                inputVolumeRnd.Attributes.Add("placeholder", "");
                inputVolUntRnd.Attributes.Add("placeholder", "");
                inputMatType.Attributes.Add("placeholder", "");
                inputPlant.Attributes.Add("placeholder", "");
                inputStorLoc.Attributes.Add("placeholder", "");
                inputSalesOrg.Attributes.Add("placeholder", "");
                inputDistrChl.Attributes.Add("placeholder", "");
                inputMatDesc.Attributes.Add("placeholder", "");
                inputMinLotSize.Attributes.Add("placeholder", "");
                inputRoundValue.Attributes.Add("placeholder", "");
                inputProcType.Attributes.Add("placeholder", "");
                inputIndStdDesc.Attributes.Add("placeholder", "");
                inputPurcGrp.Attributes.Add("placeholder", "");
                inputPurcValKey.Attributes.Add("placeholder", "");
                inputGRProcTimeMRP1.Attributes.Add("placeholder", "");
                inputPlantDeliveryTime.Attributes.Add("placeholder", "");
                inputMfrPrtNum.Attributes.Add("placeholder", "");
                inputLoadingGrp.Attributes.Add("placeholder", "");
                inputLabOffice.Attributes.Add("placeholder", "");
                inputMRPGr.Attributes.Add("placeholder", "");
                inputMRPTyp.Attributes.Add("placeholder", "");
                inputMRPCtrl.Attributes.Add("placeholder", "");
                inputLOTSize.Attributes.Add("placeholder", "");
                inputFixLotSize.Attributes.Add("placeholder", "");
                inputMaxStockLv.Attributes.Add("placeholder", "");
                inputProdStorLoc.Attributes.Add("placeholder", "");
                inputSchedMargKey.Attributes.Add("placeholder", "");
                inputSftyStck.Attributes.Add("placeholder", "");
                inputMinSftyStck.Attributes.Add("placeholder", "");
                inputStrtgyGr.Attributes.Add("placeholder", "");
                inputProdSched.Attributes.Add("placeholder", "");
                inputProdSchedProfile.Attributes.Add("placeholder", "");
                inputInspectIntrv.Attributes.Add("placeholder", "");
                inputStoreCond.Attributes.Add("placeholder", "");
                inputQMCtrlKey.Attributes.Add("placeholder", "");

                imgBtnIndStdDesc.Visible = false;
                imgBtnAunRnd.Visible = false;
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
                imgBtnVolUntRnd.Visible = false;
                imgBtnWeightUntRnd.Visible = false;
                //imgBtnNetWeightUntRnd.Visible = false;
                imgBtnSpcProc.Visible = false;
                imgBtnPurchGrp.Visible = false;
                imgBtnPurchValKey.Visible = false;
                imgBtnLoadGrp.Visible = false;
                imgBtnLabOffice.Visible = false;
                imgBtnMRPGr.Visible = false;
                imgBtnMRPType.Visible = false;
                imgBtnMRPCont.Visible = false;
                imgBtnLotSize.Visible = false;
                imgBtnProdStorLoc.Visible = false;
                imgBtnSchedMargKey.Visible = false;
                imgBtnStrategyGroup.Visible = false;
                imgBtnProdSched.Visible = false;
                imgBtnProdSchedProfile.Visible = false;
                imgBtnStorCond.Visible = false;

                otrInputTbl.Visible = false;
                InspectInputTbl.Visible = false;
                ClassInputTbl.Visible = false;
                reptUntMeas.Visible = false;
                rptInspectionType.Visible = false;
                rptClassType.Visible = false;

                reptUntMeasMgr.Visible = true;
                rptClassTypeMgr.Visible = true;
                rptInspectionTypeMgr.Visible = true;

                btnSave.Visible = false;
                btnCancelSave.Visible = false;

                btnApprove.Visible = true;
                btnReject.Visible = true;
                btnCancelApprove.Visible = true;

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
                        Value = grdrow.Cells[1].Text.Trim().ToUpper(),
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 18
                    });
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                        lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                        inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                        inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                        inputBsUntMeasRnd.Text = dr["UoM"].ToString().Trim().ToUpper();
                        inputBunRnd.Text = dr["UoM"].ToString().Trim().ToUpper();
                        inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                        inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                        inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                        inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                        inputMatType.Text = dr["MatType"].ToString().Trim().ToUpper();
                        inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                        inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                        inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                        inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                        inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                        stringCoProd = dr["COProd"].ToString().Trim();
                        inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                        inputNetWeightUnitRnd.Text = dr["NetUnit"].ToString().Trim().ToUpper();
                        inputCommImpCodeRnd.Text = dr["ForeignTrade"].ToString().Trim().ToUpper();
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

                    tmpBindRepeater();
                    ClassBindRepeater();
                    QCDataBindRepeater();

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

                    ListItem li = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
                    ddListSLED.Items.Clear();
                    ddListSLED.Items.Add(li);
                }
                catch (Exception ex)
                {
                    MsgBox(ex.ToString().Trim(), this.Page, this);
                }
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

            inputPurcGrp.ReadOnly = true;
            inputPurcValKey.ReadOnly = true;
            inputGRProcTimeMRP1.ReadOnly = true;
            inputPlantDeliveryTime.ReadOnly = true;
            inputMfrPrtNum.ReadOnly = true;
            inputLoadingGrp.ReadOnly = true;
            inputLabOffice.ReadOnly = true;
            inputMRPGr.ReadOnly = true;
            inputMRPTyp.ReadOnly = true;
            inputMRPCtrl.ReadOnly = true;
            inputLOTSize.ReadOnly = true;
            inputFixLotSize.Visible = true;
            inputFixLotSize.ReadOnly = true;
            inputMaxStockLv.ReadOnly = true;
            inputProdStorLoc.ReadOnly = true;
            inputSchedMargKey.ReadOnly = true;
            inputSftyStck.ReadOnly = true;
            inputMinSftyStck.ReadOnly = true;
            inputStrtgyGr.ReadOnly = true;
            inputTotalLeadTime.ReadOnly = true;
            inputProdSched.ReadOnly = true;
            inputProdSchedProfile.ReadOnly = true;
            inputInspectIntrv.ReadOnly = true;
            inputStoreCond.ReadOnly = true;
            inputQMCtrlKey.ReadOnly = true;

            chkbxInspectSet.Enabled = false;
            chkbxQMProcActive.Enabled = false;
            InspectInputTbl.Visible = false;
            ClassInputTbl.Visible = false;

            inputMatID.ReadOnly = true;
            inputMatDesc.ReadOnly = true;
            inputBsUntMeasRnd.ReadOnly = true;
            inputMatGr.ReadOnly = true;
            inputOldMatNum.ReadOnly = true;
            inputDivision.ReadOnly = true;
            inputPckgMat.ReadOnly = true;
            inputCommImpCodeRnd.ReadOnly = true;
            inputMinRemShLf.ReadOnly = true;
            inputIndStdDesc.ReadOnly = true;
            chkbxCoProd.Enabled = false;
            inputTotalShelfLife.ReadOnly = true;
            inputGrossWeightRnd.ReadOnly = true;
            inputNetWeight.ReadOnly = true;
            inputNetWeightUnitRnd.ReadOnly = true;
            inputWeightUntRnd.ReadOnly = true;
            inputVolumeRnd.ReadOnly = true;
            inputVolUntRnd.ReadOnly = true;
            inputMatType.ReadOnly = true;
            inputPlant.ReadOnly = true;
            inputStorLoc.ReadOnly = true;
            inputSalesOrg.ReadOnly = true;
            inputDistrChl.ReadOnly = true;
            inputMatDesc.ReadOnly = true;
            inputMinLotSize.ReadOnly = true;
            inputRoundValue.ReadOnly = true;
            inputProcType.ReadOnly = true;
            inputSpcProcRnd.ReadOnly = true;

            inputPurcGrp.CssClass = "txtBoxRO";
            inputPurcValKey.CssClass = "txtBoxRO";
            inputGRProcTimeMRP1.CssClass = "txtBoxRO";
            inputPlantDeliveryTime.CssClass = "txtBoxRO";
            inputMfrPrtNum.CssClass = "txtBoxRO";
            inputLoadingGrp.CssClass = "txtBoxRO";
            inputLabOffice.CssClass = "txtBoxRO";
            inputMRPGr.CssClass = "txtBoxRO";
            inputMRPTyp.CssClass = "txtBoxRO";
            inputMRPCtrl.CssClass = "txtBoxRO";
            inputLOTSize.CssClass = "txtBoxRO";
            inputFixLotSize.CssClass = "txtBoxRO";
            inputMaxStockLv.CssClass = "txtBoxRO";
            inputProdStorLoc.CssClass = "txtBoxRO";
            inputSchedMargKey.CssClass = "txtBoxRO";
            inputSftyStck.CssClass = "txtBoxRO";
            inputMinSftyStck.CssClass = "txtBoxRO";
            inputStrtgyGr.CssClass = "txtBoxRO";
            inputTotalLeadTime.CssClass = "txtBoxRO";
            inputProdSched.CssClass = "txtBoxRO";
            inputProdSchedProfile.CssClass = "txtBoxRO";
            inputInspectIntrv.CssClass = "txtBoxRO";
            inputStoreCond.CssClass = "txtBoxRO";
            inputQMCtrlKey.CssClass = "txtBoxRO";

            inputIndStdDesc.CssClass = "txtBoxRO";
            inputSpcProcRnd.CssClass = "txtBoxRO";
            inputMatID.CssClass = "txtBoxRO";
            inputMatDesc.CssClass = "txtBoxRO";
            inputBsUntMeasRnd.CssClass = "txtBoxRO";
            inputMatGr.CssClass = "txtBoxRO";
            inputOldMatNum.CssClass = "txtBoxRO";
            inputDivision.CssClass = "txtBoxRO";
            inputPckgMat.CssClass = "txtBoxRO";
            inputCommImpCodeRnd.CssClass = "txtBoxRO";
            inputMinRemShLf.CssClass = "txtBoxRO";
            ddListSLED.CssClass = "txtBoxRO";
            inputTotalShelfLife.CssClass = "txtBoxRO";
            inputGrossWeightRnd.CssClass = "txtBoxRO";
            inputNetWeight.CssClass = "txtBoxRO";
            inputNetWeightUnitRnd.CssClass = "txtBoxRO";
            inputWeightUntRnd.CssClass = "txtBoxRO";
            inputVolumeRnd.CssClass = "txtBoxRO";
            inputVolUntRnd.CssClass = "txtBoxRO";
            inputMatType.CssClass = "txtBoxRO";
            inputPlant.CssClass = "txtBoxRO";
            inputStorLoc.CssClass = "txtBoxRO";
            inputSalesOrg.CssClass = "txtBoxRO";
            inputDistrChl.CssClass = "txtBoxRO";
            inputMatDesc.CssClass = "txtBoxRO";
            inputMinLotSize.CssClass = "txtBoxRO";
            inputRoundValue.CssClass = "txtBoxRO";
            inputProcType.CssClass = "txtBoxRO";

            inputPurcGrp.Attributes.Add("placeholder", "");
            inputPurcValKey.Attributes.Add("placeholder", "");
            inputGRProcTimeMRP1.Attributes.Add("placeholder", "");
            inputPlantDeliveryTime.Attributes.Add("placeholder", "");
            inputMfrPrtNum.Attributes.Add("placeholder", "");
            inputLoadingGrp.Attributes.Add("placeholder", "");
            inputLabOffice.Attributes.Add("placeholder", "");
            inputMRPGr.Attributes.Add("placeholder", "");
            inputMRPTyp.Attributes.Add("placeholder", "");
            inputMRPCtrl.Attributes.Add("placeholder", "");
            inputLOTSize.Attributes.Add("placeholder", "");
            inputFixLotSize.Attributes.Add("placeholder", "");
            inputMaxStockLv.Attributes.Add("placeholder", "");
            inputProdStorLoc.Attributes.Add("placeholder", "");
            inputSchedMargKey.Attributes.Add("placeholder", "");
            inputSftyStck.Attributes.Add("placeholder", "");
            inputMinSftyStck.Attributes.Add("placeholder", "");
            inputStrtgyGr.Attributes.Add("placeholder", "");
            inputTotalLeadTime.Attributes.Add("placeholder", "");
            inputProdSched.Attributes.Add("placeholder", "");
            inputProdSchedProfile.Attributes.Add("placeholder", "");
            inputInspectIntrv.Attributes.Add("placeholder", "");
            inputStoreCond.Attributes.Add("placeholder", "");
            inputQMCtrlKey.Attributes.Add("placeholder", "");

            inputSpcProcRnd.Attributes.Add("placeholder", "");
            inputMatID.Attributes.Add("placeholder", "");
            inputMatDesc.Attributes.Add("placeholder", "");
            inputBsUntMeasRnd.Attributes.Add("placeholder", "");
            inputMatGr.Attributes.Add("placeholder", "");
            inputOldMatNum.Attributes.Add("placeholder", "");
            inputDivision.Attributes.Add("placeholder", "");
            inputPckgMat.Attributes.Add("placeholder", "");
            inputCommImpCodeRnd.Attributes.Add("placeholder", "");
            inputMinRemShLf.Attributes.Add("placeholder", "");
            ddListSLED.Attributes.Add("placeholder", "");
            inputTotalShelfLife.Attributes.Add("placeholder", "");
            inputGrossWeightRnd.Attributes.Add("placeholder", "");
            inputNetWeight.Attributes.Add("placeholder", "");
            inputNetWeightUnitRnd.Attributes.Add("placeholder", "");
            inputWeightUntRnd.Attributes.Add("placeholder", "");
            inputVolumeRnd.Attributes.Add("placeholder", "");
            inputVolUntRnd.Attributes.Add("placeholder", "");
            inputMatType.Attributes.Add("placeholder", "");
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
            imgBtnAunRnd.Visible = false;
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
            imgBtnVolUntRnd.Visible = false;
            imgBtnWeightUntRnd.Visible = false;
            //imgBtnNetWeightUntRnd.Visible = false;
            imgBtnSpcProc.Visible = false;
            otrInputTbl.Visible = false;
            reptUntMeas.Visible = false;
            imgBtnPurchGrp.Visible = false;
            imgBtnPurchValKey.Visible = false;
            imgBtnLoadGrp.Visible = false;
            imgBtnLabOffice.Visible = false;
            imgBtnMRPGr.Visible = false;
            imgBtnMRPType.Visible = false;
            imgBtnMRPCont.Visible = false;
            imgBtnProdStorLoc.Visible = false;
            imgBtnSchedMargKey.Visible = false;
            imgBtnStrategyGroup.Visible = false;
            imgBtnProdSched.Visible = false;
            imgBtnStorCond.Visible = false;
            imgBtnLotSize.Visible = false;

            divApprMn.Visible = true;
            reptUntMeasMgr.Visible = true;

            btnSave.Visible = false;
            btnCancelSave.Visible = false;
            btnClose.Visible = true;

            rptClassType.Visible = false;
            rptInspectionType.Visible = false;
            rptClassTypeMgr.Visible = true;
            rptInspectionTypeMgr.Visible = true;

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
                    listViewFico.Visible = false;

                    rmContent.Visible = true;
                    bscDt1GnrlDt.Visible = true;
                    orgLv.Visible = true;
                    MRP2Proc.Visible = true;
                    bscDt1Dimension.Visible = true;
                    MRP1LOTSizeDt.Visible = true;
                    foreignTradeDt.Visible = true;
                    plantShelfLifeDt.Visible = true;
                    trCoProd.Visible = true;

                    rmsffgMenuLabel.Text = dr["Type"].ToString().Trim().ToUpper();
                    lblTransID.Text = dr["TransID"].ToString().Trim().ToUpper();
                    inputMatID.Text = dr["MaterialID"].ToString().Trim().ToUpper();
                    inputMatDesc.Text = dr["MaterialDesc"].ToString().Trim().ToUpper();
                    inputBsUntMeasRnd.Text = dr["UoM"].ToString().Trim().ToUpper();
                    inputBunRnd.Text = dr["UoM"].ToString().Trim().ToUpper();
                    inputMatGr.Text = dr["MatlGroup"].ToString().Trim().ToUpper();
                    inputOldMatNum.Text = dr["OldMatNumb"].ToString().Trim().ToUpper();
                    inputDivision.Text = dr["Division"].ToString().Trim().ToUpper();
                    inputPckgMat.Text = dr["MatlGrpPack"].ToString().Trim().ToUpper();
                    inputMatType.Text = dr["MatType"].ToString().Trim().ToUpper();
                    inputPlant.Text = dr["Plant"].ToString().Trim().ToUpper();
                    inputStorLoc.Text = dr["Sloc"].ToString().Trim().ToUpper();
                    inputSalesOrg.Text = dr["SOrg"].ToString().Trim().ToUpper();
                    inputDistrChl.Text = dr["DistrChl"].ToString().Trim().ToUpper();
                    inputProcType.Text = dr["ProcType"].ToString().Trim().ToUpper();
                    stringCoProd = dr["COProd"].ToString().Trim();
                    inputNetWeight.Text = dr["NetWeight"].ToString().Trim().ToUpper();
                    inputNetWeightUnitRnd.Text = dr["NetUnit"].ToString().Trim().ToUpper();
                    inputCommImpCodeRnd.Text = dr["ForeignTrade"].ToString().Trim().ToUpper();
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
                    inputProdStorLoc.Text = dr["SLoc"].ToString().Trim();
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
                SqlCommand cmdUOM = new SqlCommand("SELECT * FROM Tbl_DetailUomMat WHERE TransID = @TransID", conMatWorkFlow);
                cmdUOM.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TransID",
                    Value = TransID,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10
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
            catch (Exception exMgr)
            {
                MsgBox(exMgr.ToString().Trim().ToUpper(), this.Page, this);
            }
            tmpBindRepeater();
            QCDataBindRepeater();
            ClassBindRepeater();

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
            bindLblStrategyGroup();
            //QA
            bindLblStorCondition();

            ListItem li = new ListItem(ddListSLED.SelectedItem.Text, ddListSLED.SelectedValue, true);
            ddListSLED.Items.Clear();
            ddListSLED.Items.Add(li);
        }


        //Other Data Code
        //auto generate Line
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
        protected void btnAddUntMeasRnd_Click(object sender, EventArgs e)
        {
            if (inputBunRnd.Text == "")
            {
                MsgBox("Please insert your Base UoM first.", this.Page, this);
                inputBsUntMeasRnd.Focus();
                return;
            }

            if (inputMatID.Text == "")
            {
                MsgBox("Please insert your Material ID first.", this.Page, this);
                inputMatID.Focus();
                return;
            }

            if (inputGrossWeightRnd.Text == "0" || inputXRnd.Text == "0" || inputYRnd.Text == "0" || inputGrossWeightRnd.Text == "" || inputXRnd.Text == "" || inputYRnd.Text == "" || inputAunRnd.Text == "")
            {
                MsgBox("Gross Weight, X, Aun or Y cannot be empty!", this.Page, this);
                return;
            }

            decimal GrossWeight;
            decimal GrossWeightTotal;
            int Y;
            decimal.TryParse(inputGrossWeightRnd.Text, out GrossWeight);
            Int32.TryParse(inputYRnd.Text, out Y);

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
                    if (inputGrossWeightRnd.Text != "0")
                    {
                        // doing checking
                        DataTable dtCheckB = new DataTable();
                        SqlParameter[] paramB = new SqlParameter[3];
                        paramB[0] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                        paramB[1] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                        paramB[2] = new SqlParameter("@Aun", this.inputAunRnd.Text.Trim().ToUpper());

                        //send param to SP sql
                        dtCheckB = sqlC.ExecuteDataTable("CheckXExist", paramB);

                        if (dtCheckB.Rows.Count > 0)
                        {
                            // munculkan pesan bahwa sudah ada
                            MsgBox("Your Aun " + this.inputAunRnd.Text + " is already Exist or did not match Inspection Type master data!", this.Page, this);
                            return;
                        }
                        else
                        {
                            //doing insert
                            SqlParameter[] paramm = new SqlParameter[14];
                            paramm[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
                            paramm[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim().ToUpper());
                            paramm[2] = new SqlParameter("@Line", lblLine.Text.Trim().ToUpper());
                            paramm[3] = new SqlParameter("@X", inputXRnd.Text.Trim().ToUpper());
                            paramm[4] = new SqlParameter("@Aun", inputAunRnd.Text.Trim().ToUpper());
                            paramm[5] = new SqlParameter("@Y", inputYRnd.Text.Trim());
                            paramm[6] = new SqlParameter("@Bun", inputBunRnd.Text.Trim().ToUpper());
                            paramm[7] = new SqlParameter("@GrossWeight", GrossWeightTotal);
                            paramm[8] = new SqlParameter("@WeightUnit", inputWeightUntRnd.Text.Trim().ToUpper());
                            paramm[9] = new SqlParameter("@Volume", inputVolumeRnd.Text.Trim().ToUpper());
                            paramm[10] = new SqlParameter("@VolUnit", inputVolUntRnd.Text.Trim().ToUpper());
                            paramm[11] = new SqlParameter("@CreateBy", lblUser.Text.Trim());
                            paramm[12] = new SqlParameter("@CreateTime", DateTime.Now);
                            paramm[13] = new SqlParameter("@New", "");
                            //send param to SP sql
                            sqlC.ExecuteNonQuery("DetailUomMat_InsertData", paramm);

                            inputMatID.ReadOnly = true;
                            inputMatID.CssClass = "TxtBoxRO";
                        }
                    }
                    else
                    {
                        MsgBox("Gross Weight value cannot 0.", this.Page, this);
                        return;
                    }
                }
                dtCheck.Clear();
                dtCheck = null;
            }
            else
            {
                if (inputGrossWeightRnd.Text != "0")
                {
                    // doing checking
                    DataTable dtCheckB = new DataTable();
                    SqlParameter[] paramB = new SqlParameter[3];
                    paramB[0] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
                    paramB[1] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
                    paramB[2] = new SqlParameter("@Aun", this.inputAunRnd.Text.Trim().ToUpper());

                    //send param to SP sql
                    dtCheckB = sqlC.ExecuteDataTable("CheckXExist", paramB);

                    if (dtCheckB.Rows.Count > 0)
                    {
                        // munculkan pesan bahwa sudah ada
                        MsgBox("Your Aun " + this.inputAunRnd.Text + " is already Exist or did not match Inspection Type master data!", this.Page, this);
                        return;
                    }
                    else
                    {
                        //doing insert
                        SqlParameter[] paramm = new SqlParameter[14];
                        paramm[0] = new SqlParameter("@TransID", lblTransID.Text.Trim().ToUpper());
                        paramm[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim().ToUpper());
                        paramm[2] = new SqlParameter("@Line", lblLine.Text.Trim().ToUpper());
                        paramm[3] = new SqlParameter("@X", inputXRnd.Text.Trim().ToUpper());
                        paramm[4] = new SqlParameter("@Aun", inputAunRnd.Text.Trim().ToUpper());
                        paramm[5] = new SqlParameter("@Y", inputYRnd.Text.Trim());
                        paramm[6] = new SqlParameter("@Bun", inputBunRnd.Text.Trim().ToUpper());
                        paramm[7] = new SqlParameter("@GrossWeight", GrossWeightTotal);
                        paramm[8] = new SqlParameter("@WeightUnit", inputWeightUntRnd.Text.Trim().ToUpper());
                        paramm[9] = new SqlParameter("@Volume", inputVolumeRnd.Text.Trim().ToUpper());
                        paramm[10] = new SqlParameter("@VolUnit", inputVolUntRnd.Text.Trim().ToUpper());
                        paramm[11] = new SqlParameter("@CreateBy", lblUser.Text.Trim());
                        paramm[12] = new SqlParameter("@CreateTime", DateTime.Now);
                        paramm[13] = new SqlParameter("@New", "");
                        //send param to SP sql
                        sqlC.ExecuteNonQuery("DetailUomMat_InsertData", paramm);
                        inputMatID.ReadOnly = true;
                        inputMatID.CssClass = "TxtBoxRO";
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
        private void Clear_ControlsRnd()
        {
            inputGrossWeightRnd.CssClass = "txtBoxRO";
            inputWeightUntRnd.CssClass = "txtBoxRO";
            imgBtnWeightUntRnd.Visible = false;
            tdVolume.ColSpan = 1;
            tdVolume.Style.Add("padding-left", "0px");

            inputXRnd.Text = string.Empty;
            inputAunRnd.Text = string.Empty;
            lblAMeasRnd.Text = "Aun Desc.";
            lblVolUntRnd.Text = "Volume Unit Desc.";
            inputYRnd.Text = string.Empty;

            inputVolumeRnd.Text = "0";
            inputVolUntRnd.Text = string.Empty;
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

        //LightBox Data Code
        //RnD Lightbox
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

                GridViewSpcProcRnd.DataSource = ds.Tables[0];
                GridViewSpcProcRnd.DataBind();
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

                GridViewSpcProcRnd.DataSource = ds.Tables[0];
                GridViewSpcProcRnd.DataBind();
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

                GridViewSpcProcRnd.DataSource = ds.Tables[0];
                GridViewSpcProcRnd.DataBind();
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

            GridViewVolUntRnd.DataSource = ds.Tables[0];
            GridViewVolUntRnd.DataBind();
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
            SqlCommand cmd = new SqlCommand("srcMatTypeSampleSF", conMatWorkFlow);
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
                var queryString = "SELECT * FROM Mstr_MaterialGroup WHERE MatlGroup_Desc2 NOT LIKE 'SEMI%' AND MatlGroup_Desc2 NOT LIKE 'FINISHED%'"; //return data from UserApp table
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
                var queryString = "SELECT * FROM Mstr_MaterialGroup WHERE MatlGroup_Desc2 NOT LIKE 'RAW%' AND MatlGroup_Desc2 NOT LIKE 'FINISHED%'"; //return data from UserApp table
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
                var queryString = "SELECT * FROM Mstr_MaterialGroup WHERE MatlGroup_Desc2 NOT LIKE 'SEMI%' AND MatlGroup_Desc2 NOT LIKE 'RAW%'"; //return data from UserApp table
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

            GridViewWeightUntRnd.DataSource = ds.Tables[0];
            GridViewWeightUntRnd.DataBind();
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

            GridViewNetWeightUntRnd.DataSource = ds.Tables[0];
            GridViewNetWeightUntRnd.DataBind();
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

            GridViewCommImpRnd.DataSource = ds.Tables[0];
            GridViewCommImpRnd.DataBind();
            conMatWorkFlow.Close();
        }

        //Proc Lightbox
        protected void srcLoadingGrpModalBindingProc()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("srcLoadingGroupSample", conMatWorkFlow);
            cmd.Parameters.Add("@TransID", SqlDbType.NVarChar).Value = this.lblTransID.Text.ToUpper().Trim();
            cmd.Parameters.Add("@Plant", SqlDbType.NVarChar).Value = this.inputPlant.Text.ToUpper().Trim();
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

        //Planner Lightbox
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
            SqlCommand cmd = new SqlCommand("srcMRPGroupSample", conMatWorkFlow);
            cmd.Parameters.Add("@Plant", SqlDbType.NVarChar).Value = this.inputPlant.Text;
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
            SqlCommand cmd = new SqlCommand("srcMRPType", conMatWorkFlow);
            cmd.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

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
            SqlCommand cmd = new SqlCommand("srcMRPControllersSample", conMatWorkFlow);
            cmd.Parameters.Add("@Plant", SqlDbType.NVarChar).Value = this.inputPlant.Text;
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
                Value = this.inputPlant.Text,
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
                Value = this.inputPlant.Text,
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
            SqlCommand cmd = new SqlCommand("srcProdSchedSample", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "Plant",
                Value = this.inputPlant.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 4
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
            SqlCommand cmd = new SqlCommand("srcProdSchedProfileSample", conMatWorkFlow);
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "Plant",
                Value = this.inputPlant.Text.ToUpper().Trim(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 4
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

        //QC Lightbox
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
                Value = this.inputMatID.Text,
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
            dataAdapter.SelectCommand.Parameters.AddWithValue("@MaterialID", this.inputMatID.Text.ToUpper().Trim());

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridViewInspectionType.DataSource = ds.Tables[0];
            GridViewInspectionType.DataBind();
            conMatWorkFlow.Close();
        }

        //QA Lightbox
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

        //RnD Modal
        protected void selectIndStdDesc_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputIndStdDesc.Text = grdrow.Cells[0].Text;
            lblIndStdDesc.Text = grdrow.Cells[1].Text;
            lblIndStdDesc.ForeColor = Color.Black;
            inputIndStdDesc.Focus();
        }
        protected void selectCommImpRnd_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputCommImpCodeRnd.Text = grdrow.Cells[0].Text;
            lblCommImpCodeRnd.Text = grdrow.Cells[1].Text;
            lblCommImpCodeRnd.ForeColor = Color.Black;
        }
        protected void selectVolUntRnd_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputVolUntRnd.Text = grdrow.Cells[0].Text;
            lblVolUntRnd.Text = grdrow.Cells[1].Text;
            lblVolUntRnd.ForeColor = Color.Black;
            inputVolUntRnd.Focus();
        }
        protected void selectWeightUntRnd_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputWeightUntRnd.Text = grdrow.Cells[0].Text;

            lblWeightUntRnd.Text = grdrow.Cells[1].Text;
            lblWeightUntRnd.ForeColor = Color.Black;
            inputWeightUntRnd.Focus();
        }
        protected void selectNetWeightUntRnd_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputNetWeightUnitRnd.Text = grdrow.Cells[0].Text;
            lblNetWeightUnitRnd.Text = grdrow.Cells[2].Text;
            lblNetWeightUnitRnd.ForeColor = Color.Black;
            inputNetWeightUnitRnd.Focus();
        }
        protected void selectMatTyp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputMatType.Text = grdrow.Cells[0].Text;
            lblMatType.Text = grdrow.Cells[1].Text;
            lblMatType.ForeColor = Color.Black;
            inputMatType.Focus();
            if (inputMatType.Text.ToUpper().Trim() == "SFAT")
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
            inputLoadingGrp.Text = inputPlant.Text;
            inputPlant.Focus();
            lblStorLoc.Text = absStorLoc.Text;
            lblStorLoc.ForeColor = Color.Black;
            inputStorLoc.Text = "";

            srcStorLocModalBinding();
            srcLoadingGrpModalBindingProc();
            srcMRPGrModalBinding();
            srcMRPCtrlModalBinding();
            srcProdStorLocModalBinding();
            srcSchedMargKeyModalBinding();
            srcProdSchedModalBinding();
            srcProdSchedProfileModalBinding();

            if (inputPlant.Text == "5200")
            {
                inputStrtgyGr.Text = "52";
            }
            else if (inputPlant.Text == "2200")
            {
                if (inputDivision.Text == "15")
                {
                    if (inputMRPGr.Text == "0004")
                    {
                        inputStrtgyGr.Text = "52";
                        bindLblStrategyGroup();
                    }
                }
            }
            else
            {
                inputStrtgyGr.Text = "40";
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
        protected void selectAunRnd_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputAunRnd.Text = grdrow.Cells[0].Text;
            inputAunRnd.Focus();
        }
        protected void selectBsUntMeas_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputBsUntMeasRnd.Text = grdrow.Cells[0].Text;
            inputWeightUntRnd.Text = grdrow.Cells[0].Text;
            inputBunRnd.Text = grdrow.Cells[0].Text;
            inputNetWeightUnitRnd.Text = grdrow.Cells[0].Text;
            inputWeightUntRnd.Text = grdrow.Cells[0].Text;
            lblBsUntMeasRnd.Text = grdrow.Cells[1].Text;
            lblBsUntMeasRnd.ForeColor = Color.Black;
            lblNetWeightUnitRnd.ForeColor = Color.Black;
        }
        protected void selectDivision_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputDivision.Text = grdrow.Cells[0].Text;
            lblDivision.Text = grdrow.Cells[1].Text;
            lblDivision.ForeColor = Color.Black;
            inputDivision.Focus();

            if (inputDivision.Text == "15")
            {
                if (inputPlant.Text == "2200")
                {
                    if (inputMRPGr.Text == "0004")
                    {
                        inputStrtgyGr.Text = "52";
                        bindLblStrategyGroup();
                    }
                }
            }
            else
            {
                inputStrtgyGr.Text = "40";
            }
        }
        protected void selectProcType_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputProcType.Text = grdrow.Cells[0].Text.Trim
                ();
            lblProcType.Text = grdrow.Cells[1].Text.Trim();
            lblProcType.ForeColor = Color.Black;
            inputProcType.Focus();
            srcSpcProcModalBinding();
            if (inputProcType.Text.ToUpper().Trim() == "&nbsp;")
            {
                inputProcType.Text = "";
            }
            if (inputProcType.Text == "E")
            {
                chkbxCoProd.Enabled = true;
            }
            else
            {
                chkbxCoProd.Enabled = false;
                chkbxCoProd.Checked = false;
            }
        }
        protected void selectSpcProcRnd_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputSpcProcRnd.Text = grdrow.Cells[0].Text;
            lblSpcProcRnd.Text = grdrow.Cells[1].Text;
            lblSpcProcRnd.ForeColor = Color.Black;
            inputSpcProcRnd.Focus();
        }

        //Proc Modal
        protected void selectLoadingGrp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputLoadingGrp.Text = grdrow.Cells[0].Text;
            lblLoadingGrp.Text = grdrow.Cells[1].Text;
            lblLoadingGrp.ForeColor = Color.Black;
            inputLoadingGrp.Focus();
        }
        protected void selectPurcGrp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputPurcGrp.Text = grdrow.Cells[0].Text;
            lblPurcGrp.Text = grdrow.Cells[1].Text;
            lblPurcGrp.ForeColor = Color.Black;
            inputPurcGrp.Focus();
        }
        protected void selectPurcValKey_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputPurcValKey.Text = grdrow.Cells[0].Text;
            inputPurcValKey.Focus();
        }

        //Planner Modal
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

            if (inputMRPGr.Text == "0004")
            {
                if (inputPlant.Text == "2200")
                {
                    if (inputDivision.Text == "15")
                    {
                        inputStrtgyGr.Text = "52";
                        bindLblStrategyGroup();
                    }
                }
            }
            else
            {
                inputStrtgyGr.Text = "40";
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
            if (inputMRPTyp.Text == dt.Rows[0]["HIGH"].ToString())
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
        }
        protected void selectProdSchedProfile_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            inputProdSchedProfile.Text = grdrow.Cells[1].Text;
            lblProdSchedProfile.Text = grdrow.Cells[2].Text;
            lblProdSchedProfile.ForeColor = Color.Black;
        }

        //QC Modal
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

        //QA Modal
        //inputStoreCond_onBlur
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
            if (inputNetWeight.Text == "0")
            {
                MsgBox("Net Weight cannot be 0!", this.Page, this);
                return;
            }

            if (inputMatType.Text == "" || inputMatID.Text == "" || inputMatDesc.Text == "" || inputBsUntMeasRnd.Text == "" || inputMatGr.Text == "" || inputNetWeight.Text == "" || inputSalesOrg.Text == "" || inputPlant.Text == "" || inputStorLoc.Text == "" || inputDistrChl.Text == "" || inputProcType.Text == "" || inputMRPGr.Text == "" || inputMRPTyp.Text == "" || inputMRPCtrl.Text == "" || inputSchedMargKey.Text == "")
            {
                MsgBox("One of the required field still empty.", this.Page, this);
                return;
            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
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

            conMatWorkFlow.Open();
            // doing checking
            DataTable dtCheckC = new DataTable();
            SqlParameter[] paramC = new SqlParameter[3];
            paramC[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
            paramC[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
            paramC[2] = new SqlParameter("@Uom", this.inputBsUntMeasRnd.Text.Trim().ToUpper());

            //send param to SP sql
            dtCheckC = sqlC.ExecuteDataTable("CheckBun", paramC);

            if (dtCheckC.Rows.Count == 0)
            {
                // munculkan pesan bahwa sudah ada
                MsgBox("Your Bun value not exactly the same as Base Unit Measurement value. Please update the detail Uom table first.", this.Page, this);
                return;
            }
            conMatWorkFlow.Close();

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            if (chkbxCoProd.Checked == true)
            {
                stringCoProd = "X";
            }
            else if (chkbxCoProd.Checked == false)
            {
                stringCoProd = "";
            }
            if (chkbxInspectSet.Checked == true)
            {
                stringInspectSet = "X";
            }
            else if (chkbxInspectSet.Checked == false)
            {
                stringInspectSet = "";
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
                SqlCommand cmd = new SqlCommand("saveSampleMaterial", conMatWorkFlow);
                cmd.Parameters.Add("@Update", SqlDbType.NVarChar).Value = "X";
                cmd.Parameters.Add("@TransID", SqlDbType.NVarChar).Value = this.lblTransID.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar).Value = this.inputMatID.Text.ToUpper().Trim();
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = this.rmsffgMenuLabel.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MaterialDesc", SqlDbType.NVarChar).Value = this.inputMatDesc.Text.ToUpper().Trim();

                cmd.Parameters.Add("@UoM", SqlDbType.NVarChar).Value = this.inputBsUntMeasRnd.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MatlGroup", SqlDbType.NVarChar).Value = this.inputMatGr.Text.ToUpper().Trim();
                cmd.Parameters.Add("@OldMatNumb", SqlDbType.NVarChar).Value = this.inputOldMatNum.Text.ToUpper().Trim();
                cmd.Parameters.Add("@Division", SqlDbType.NVarChar).Value = this.inputDivision.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MatlGrpPack", SqlDbType.NVarChar).Value = this.inputPckgMat.Text.ToUpper().Trim();

                cmd.Parameters.Add("@MatType", SqlDbType.NVarChar).Value = this.inputMatType.Text.ToUpper().Trim();
                cmd.Parameters.Add("@Plant", SqlDbType.NVarChar).Value = this.inputPlant.Text.ToUpper().Trim();
                cmd.Parameters.Add("@Sloc", SqlDbType.NVarChar).Value = this.inputStorLoc.Text.ToUpper().Trim();
                cmd.Parameters.Add("@SOrg", SqlDbType.NVarChar).Value = this.inputSalesOrg.Text.ToUpper().Trim();
                cmd.Parameters.Add("@DistrChl", SqlDbType.NVarChar).Value = this.inputDistrChl.Text.ToUpper().Trim();

                cmd.Parameters.Add("@ProcType", SqlDbType.NVarChar).Value = this.inputProcType.Text.ToUpper().Trim();
                cmd.Parameters.Add("@CreateBy", SqlDbType.NVarChar).Value = this.lblUser.Text.ToUpper().Trim();
                cmd.Parameters.Add("@NetWeight", SqlDbType.Decimal).Value = this.inputNetWeight.Text.ToUpper().Trim();
                cmd.Parameters.Add("@NetUnit", SqlDbType.NVarChar).Value = this.inputNetWeightUnitRnd.Text.ToUpper().Trim();
                cmd.Parameters.Add("@ForeignTrade", SqlDbType.NVarChar).Value = this.inputCommImpCodeRnd.Text.ToUpper().Trim();

                cmd.Parameters.Add("@MinShelfLife", SqlDbType.NVarChar).Value = this.inputMinRemShLf.Text.ToUpper().Trim();
                cmd.Parameters.Add("@TotalShelfLife", SqlDbType.NVarChar).Value = this.inputTotalShelfLife.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MinLotSize", SqlDbType.NVarChar).Value = this.inputMinLotSize.Text.ToUpper().Trim();
                cmd.Parameters.Add("@RoundingValue", SqlDbType.NVarChar).Value = this.inputRoundValue.Text.ToUpper().Trim();
                cmd.Parameters.Add("@COProd", SqlDbType.NVarChar).Value = this.stringCoProd.ToString().ToUpper().Trim();

                cmd.Parameters.Add("@SpclProcurement", SqlDbType.NVarChar).Value = this.inputSpcProcRnd.Text.ToUpper().Trim();
                cmd.Parameters.Add("@PeriodIndForSELD", SqlDbType.NVarChar).Value = this.ddListSLED.SelectedValue.ToUpper().Trim();
                cmd.Parameters.Add("@IndStdCode", SqlDbType.NVarChar).Value = this.inputIndStdDesc.Text.ToUpper().Trim();
                cmd.Parameters.Add("@PurchGrp", SqlDbType.NVarChar).Value = this.inputPurcGrp.Text.ToUpper().Trim();
                cmd.Parameters.Add("@PurchValueKey", SqlDbType.NVarChar).Value = this.inputPurcValKey.Text.ToUpper().Trim();

                cmd.Parameters.Add("@GRProcessingTime", SqlDbType.NVarChar).Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim();
                cmd.Parameters.Add("@PlantDeliveryTime", SqlDbType.NVarChar).Value = this.inputPlantDeliveryTime.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MfrPartNumb", SqlDbType.NVarChar).Value = this.inputMfrPrtNum.Text.ToUpper().Trim();
                cmd.Parameters.Add("@LoadGrp", SqlDbType.NVarChar).Value = this.inputLoadingGrp.Text.ToUpper().Trim();
                cmd.Parameters.Add("@LabOffice", SqlDbType.NVarChar).Value = this.inputLabOffice.Text.ToUpper().Trim();

                cmd.Parameters.Add("@MRPGroup", SqlDbType.NVarChar).Value = this.inputMRPGr.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MRPType", SqlDbType.NVarChar).Value = this.inputMRPTyp.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MRPController", SqlDbType.NVarChar).Value = this.inputMRPCtrl.Text.ToUpper().Trim();
                cmd.Parameters.Add("@LotSize", SqlDbType.NVarChar).Value = this.inputLOTSize.Text.ToUpper().Trim();
                cmd.Parameters.Add("@FixLotSize", SqlDbType.NVarChar).Value = this.inputFixLotSize.Text.ToUpper().Trim();

                cmd.Parameters.Add("@MaxStockLvl", SqlDbType.NVarChar).Value = this.inputMaxStockLv.Text.ToUpper().Trim();
                cmd.Parameters.Add("@ProdSLoc", SqlDbType.NVarChar).Value = this.inputProdStorLoc.Text.ToUpper().Trim();
                cmd.Parameters.Add("@SchedType", SqlDbType.NVarChar).Value = this.inputSchedMargKey.Text.ToUpper().Trim();
                cmd.Parameters.Add("@SafetyStock", SqlDbType.NVarChar).Value = this.inputSftyStck.Text.ToUpper().Trim();
                cmd.Parameters.Add("@MinSafetyStock", SqlDbType.NVarChar).Value = this.inputMinSftyStck.Text.ToUpper().Trim();

                cmd.Parameters.Add("@PlanStrategyGroup", SqlDbType.NVarChar).Value = this.inputStrtgyGr.Text.ToUpper().Trim();
                cmd.Parameters.Add("@ProdSched", SqlDbType.NVarChar).Value = this.inputProdSched.Text.ToUpper().Trim();
                cmd.Parameters.Add("@ProdSchedProfile", SqlDbType.NVarChar).Value = this.inputProdSchedProfile.Text.ToUpper().Trim();
                cmd.Parameters.Add("@InspectionSetup", SqlDbType.NVarChar).Value = this.stringInspectSet.ToString().ToUpper().Trim();
                cmd.Parameters.Add("@InspectionInterval", SqlDbType.NVarChar).Value = this.inputInspectIntrv.Text.ToUpper().Trim();

                cmd.Parameters.Add("@StorConditions", SqlDbType.NVarChar).Value = this.inputStoreCond.Text.ToUpper().Trim();
                cmd.Parameters.Add("@QMProcActive", SqlDbType.NVarChar).Value = this.lblChkbx.Text.ToUpper().Trim();
                cmd.Parameters.Add("@QMControlKey", SqlDbType.NVarChar).Value = this.inputQMCtrlKey.Text.ToUpper().Trim();
                cmd.Parameters.Add("@Module_User", SqlDbType.NVarChar).Value = this.lblPosition.Text.ToUpper().Trim();
                cmd.Parameters.Add("@Usnam", SqlDbType.NVarChar).Value = this.lblUser.Text.Trim();

                cmd.Parameters.Add("@LogID", SqlDbType.NVarChar).Value = this.lblLogID.Text.ToUpper().Trim();
                cmd.Parameters.Add("@TotLeadTime", SqlDbType.NVarChar).Value = this.inputTotalLeadTime.Text.ToUpper().Trim();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();

                conMatWorkFlow.Open();
                if (chkbxCoProd.Checked == true)
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
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
            Response.Write("<script language='javascript'>window.alert('Sample Material has been saved.');window.location='sampleMaterial';</script>");
        }
        protected void CancelUpd_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmdDetailNew = new SqlCommand("DELETE FROM Tbl_DetailUomMat WHERE TransID=TransID AND MaterialID=@MaterialID AND New=@New", conMatWorkFlow);
            cmdDetailNew.Parameters.AddWithValue("New", "");
            cmdDetailNew.Parameters.Add(new SqlParameter
            {
                ParameterName = "TrasnID",
                Value = this.lblTransID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdDetailNew.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMatID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdDetailNew.ExecuteNonQuery();
            conMatWorkFlow.Close();

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
                Value = this.inputMatID.Text,
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
                Value = this.inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdQCData.ExecuteNonQuery();
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

            Response.Redirect("~/Pages/sampleMaterial");
        }
        protected void Approve_Click(object sender, EventArgs e)
        {
            try
            {
                int sukses;
                SqlParameter[] param = new SqlParameter[6];

                //master
                param[0] = new SqlParameter("@TransID", lblTransID.Text.Trim());
                param[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim());
                param[2] = new SqlParameter("@CreatedBy", lblUser.Text.Trim());
                param[3] = new SqlParameter("@ApprovalStatus", "Approve");
                param[4] = new SqlParameter("@Module_User", lblPosition.Text.Trim());
                param[5] = new SqlParameter("@RejectRevisionNotes", inputRejectReason.Text.Trim());

                //send param to SP sql
                //sukses = sqlC.ExecuteDataTable("ExtendPlant_Approval", param);

                //send param to SP sql
                sukses = sqlC.ExecuteNonQuery("SampleMaterial_Approval", param);
                if (sukses != 1)
                {
                    // countsuccess++;
                    MsgBox("Success Approve Sample Material", this.Page, this);
                    // Response.Redirect("~/Pages/extendPlantPage");
                }
                else
                { //countfailed++;
                    MsgBox("Error Approve Sample Material", this.Page, this);
                }


            }
            catch (Exception ezz)
            {
                //ShowMessageError(ezz.Message.ToString());
                MsgBox("Error Approve Sample Material", this.Page, this);
            }
            Response.Redirect("~/Pages/sampleMaterial");
        }
        protected void CancelApprove_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/sampleMaterial");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            if (inputNetWeight.Text == "0")
            {
                MsgBox("Net Weight cannot be 0!", this.Page, this);
                return;
            }

            autoGenTransID();
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
            if (inputMatType.Text == "" || inputMatID.Text == "" || inputMatDesc.Text == "" || inputBsUntMeasRnd.Text == "" || inputMatGr.Text == "" || inputNetWeight.Text == "" || inputSalesOrg.Text == "" || inputPlant.Text == "" || inputStorLoc.Text == "" || inputDistrChl.Text == "" || inputProcType.Text == "" || inputMRPGr.Text == "" || inputMRPTyp.Text == "" || inputMRPCtrl.Text == "" || inputSchedMargKey.Text == "")
            {
                MsgBox("One of the required field still empty.", this.Page, this);
                return;
            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            // doing checking
            DataTable dtCheck = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
            param[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());

            //send param to SP sql
            dtCheck = sqlC.ExecuteDataTable("CheckDetailUom", param);

            if (dtCheck.Rows.Count == 0)
            {
                // munculkan pesan bahwa belum ada
                MsgBox("Your detail Uom table cannot be empty!", this.Page, this);
                return;
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

            conMatWorkFlow.Open();
            // doing checking
            DataTable dtCheckC = new DataTable();
            SqlParameter[] paramC = new SqlParameter[3];
            paramC[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());
            paramC[1] = new SqlParameter("@TransID", this.lblTransID.Text.Trim().ToUpper());
            paramC[2] = new SqlParameter("@Uom", this.inputBsUntMeasRnd.Text.Trim().ToUpper());

            //send param to SP sql
            dtCheckC = sqlC.ExecuteDataTable("CheckBun", paramC);

            if (dtCheckC.Rows.Count == 0)
            {
                // munculkan pesan bahwa sudah ada
                MsgBox("Your Bun value not exactly the same as Base Unit Measurement value. Please update the detail Uom table first.", this.Page, this);
                return;
            }
            conMatWorkFlow.Close();

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            if (chkbxCoProd.Checked == true)
            {
                stringCoProd = "X";
            }
            else if (chkbxCoProd.Checked == false)
            {
                stringCoProd = "";
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
                //RnD Transaction Input
                // doing checking
                DataTable dtCheckA = new DataTable();
                SqlParameter[] paramA = new SqlParameter[1];
                paramA[0] = new SqlParameter("@MaterialID", this.inputMatID.Text.Trim().ToUpper());

                //send paramA to SP sql
                dtCheckA = sqlC.ExecuteDataTable("selectAllMaterial_ByMaterialID", paramA);

                if (dtCheckA.Rows.Count > 0)
                {
                    // munculkan pesan bahwa sudah ada
                    MsgBox("Your MaterialID " + this.inputMatID.Text + " is already Exist!", this.Page, this);
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("saveSampleMaterial", conMatWorkFlow);
                    cmd.Parameters.Add("@Update", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@TransID", SqlDbType.NVarChar).Value = this.lblTransID.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar).Value = this.inputMatID.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = this.rmsffgMenuLabel.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MaterialDesc", SqlDbType.NVarChar).Value = this.inputMatDesc.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@UoM", SqlDbType.NVarChar).Value = this.inputBsUntMeasRnd.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MatlGroup", SqlDbType.NVarChar).Value = this.inputMatGr.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@OldMatNumb", SqlDbType.NVarChar).Value = this.inputOldMatNum.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@Division", SqlDbType.NVarChar).Value = this.inputDivision.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MatlGrpPack", SqlDbType.NVarChar).Value = this.inputPckgMat.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@MatType", SqlDbType.NVarChar).Value = this.inputMatType.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@Plant", SqlDbType.NVarChar).Value = this.inputPlant.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@Sloc", SqlDbType.NVarChar).Value = this.inputStorLoc.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@SOrg", SqlDbType.NVarChar).Value = this.inputSalesOrg.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@DistrChl", SqlDbType.NVarChar).Value = this.inputDistrChl.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@ProcType", SqlDbType.NVarChar).Value = this.inputProcType.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@CreateBy", SqlDbType.NVarChar).Value = this.lblUser.Text.ToUpper().Trim();
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "NetWeight",
                        Value = this.inputNetWeight.Text,
                        SqlDbType = SqlDbType.Decimal,
                        Precision = 18,
                        Scale = 3
                    });
                    cmd.Parameters.Add("@NetUnit", SqlDbType.NVarChar).Value = this.inputNetWeightUnitRnd.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@ForeignTrade", SqlDbType.NVarChar).Value = this.inputCommImpCodeRnd.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@MinShelfLife", SqlDbType.NVarChar).Value = this.inputMinRemShLf.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@TotalShelfLife", SqlDbType.NVarChar).Value = this.inputTotalShelfLife.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MinLotSize", SqlDbType.NVarChar).Value = this.inputMinLotSize.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@RoundingValue", SqlDbType.NVarChar).Value = this.inputRoundValue.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@COProd", SqlDbType.NVarChar).Value = this.stringCoProd.ToString().ToUpper().Trim();

                    cmd.Parameters.Add("@SpclProcurement", SqlDbType.NVarChar).Value = this.inputSpcProcRnd.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@PeriodIndForSELD", SqlDbType.NVarChar).Value = this.ddListSLED.SelectedValue.ToUpper().Trim();
                    cmd.Parameters.Add("@IndStdCode", SqlDbType.NVarChar).Value = this.inputIndStdDesc.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@PurchGrp", SqlDbType.NVarChar).Value = this.inputPurcGrp.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@PurchValueKey", SqlDbType.NVarChar).Value = this.inputPurcValKey.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@GRProcessingTime", SqlDbType.NVarChar).Value = this.inputGRProcTimeMRP1.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@PlantDeliveryTime", SqlDbType.NVarChar).Value = this.inputPlantDeliveryTime.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MfrPartNumb", SqlDbType.NVarChar).Value = this.inputMfrPrtNum.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@LoadGrp", SqlDbType.NVarChar).Value = this.inputLoadingGrp.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@LabOffice", SqlDbType.NVarChar).Value = this.inputLabOffice.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@MRPGroup", SqlDbType.NVarChar).Value = this.inputMRPGr.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MRPType", SqlDbType.NVarChar).Value = this.inputMRPTyp.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MRPController", SqlDbType.NVarChar).Value = this.inputMRPCtrl.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@LotSize", SqlDbType.NVarChar).Value = this.inputLOTSize.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@FixLotSize", SqlDbType.NVarChar).Value = this.inputFixLotSize.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@MaxStockLvl", SqlDbType.NVarChar).Value = this.inputMaxStockLv.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@ProdSLoc", SqlDbType.NVarChar).Value = this.inputProdStorLoc.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@SchedType", SqlDbType.NVarChar).Value = this.inputSchedMargKey.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@SafetyStock", SqlDbType.NVarChar).Value = this.inputSftyStck.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@MinSafetyStock", SqlDbType.NVarChar).Value = this.inputMinSftyStck.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@PlanStrategyGroup", SqlDbType.NVarChar).Value = this.inputStrtgyGr.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@ProdSched", SqlDbType.NVarChar).Value = this.inputProdSched.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@ProdSchedProfile", SqlDbType.NVarChar).Value = this.inputProdSchedProfile.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@InspectionSetup", SqlDbType.NVarChar).Value = this.stringInspectSet.ToString().ToUpper().Trim();
                    cmd.Parameters.Add("@InspectionInterval", SqlDbType.NVarChar).Value = this.inputInspectIntrv.Text.ToUpper().Trim();

                    cmd.Parameters.Add("@StorConditions", SqlDbType.NVarChar).Value = this.inputStoreCond.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@QMProcActive", SqlDbType.NVarChar).Value = this.lblChkbx.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@QMControlKey", SqlDbType.NVarChar).Value = this.inputQMCtrlKey.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@Module_User", SqlDbType.NVarChar).Value = this.lblPosition.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@Usnam", SqlDbType.NVarChar).Value = this.lblUser.Text.Trim();

                    cmd.Parameters.Add("@LogID", SqlDbType.NVarChar).Value = this.lblLogID.Text.ToUpper().Trim();
                    cmd.Parameters.Add("@TotLeadTime", SqlDbType.NVarChar).Value = this.inputTotalLeadTime.Text.ToUpper().Trim();
                    // cmd.Parameters.Add("@SampleMat", SqlDbType.NVarChar).Value = "X";

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conMatWorkFlow.Close();

                    Response.Write("<script language='javascript'>window.alert('Sample Material has been saved.');window.location='sampleMaterial';</script>");
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }

        }
        protected void Reject_Click(object sender, EventArgs e)
        {
            try
            {
                int sukses;
                SqlParameter[] param = new SqlParameter[6];

                //master
                param[0] = new SqlParameter("@TransID", lblTransID.Text.Trim());
                param[1] = new SqlParameter("@MaterialID", inputMatID.Text.Trim());
                param[2] = new SqlParameter("@CreatedBy", lblUser.Text.Trim());
                param[3] = new SqlParameter("@ApprovalStatus", "Reject");
                param[4] = new SqlParameter("@Module_User", lblPosition.Text.Trim());
                param[5] = new SqlParameter("@RejectRevisionNotes", inputRejectReason.Text.Trim().ToUpper());

                //send param to SP sql
                //sukses = sqlC.ExecuteDataTable("ExtendPlant_Approval", param);

                //send param to SP sql
                sukses = sqlC.ExecuteNonQuery("SampleMaterial_Approval", param);
                if (sukses != 1)
                {
                    // countsuccess++;
                    MsgBox("Success Reject Sample Material", this.Page, this);
                    // Response.Redirect("~/Pages/extendPlantPage");
                }
                else
                { //countfailed++;
                    MsgBox("Error Approve Sample Material", this.Page, this);
                }


            }
            catch (Exception ezz)
            {
                //ShowMessageError(ezz.Message.ToString());
                MsgBox("Error Approve Sample Material", this.Page, this);
            }
            Response.Redirect("~/Pages/sampleMaterial");
        }
        protected void CancelSave_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmdDetailNew = new SqlCommand("DELETE FROM Tbl_DetailUomMat WHERE TransID=TransID AND MaterialID=@MaterialID AND New=@New", conMatWorkFlow);
            cmdDetailNew.Parameters.AddWithValue("New", "");
            cmdDetailNew.Parameters.Add(new SqlParameter
            {
                ParameterName = "TrasnID",
                Value = this.lblTransID.Text.Trim().ToUpper(),
                SqlDbType = SqlDbType.NVarChar,
                Size = 10
            });
            cmdDetailNew.Parameters.Add(new SqlParameter
            {
                ParameterName = "MaterialID",
                Value = this.inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdDetailNew.ExecuteNonQuery();
            conMatWorkFlow.Close();

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
                Value = this.inputMatID.Text,
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
                Value = this.inputMatID.Text,
                SqlDbType = SqlDbType.NVarChar,
                Size = 18
            });
            cmdQCData.ExecuteNonQuery();
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

            Response.Redirect("~/Pages/sampleMaterial");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/sampleMaterial");
        }

        //Rnd Onblur and Checkbox
        //inputAun_onBlur
        protected void inputAunRnd_onBlur(object sender, EventArgs e)
        {
            if (inputAunRnd.Text == "")
            {
                lblAMeasRnd.Text = absAMeasRnd.Text;
                lblAMeasRnd.ForeColor = Color.Black;
                inputAunRnd.Focus();
            }
            else if (inputAunRnd.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputAun";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputAun", this.inputAunRnd.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblAMeasRnd.Text = dr["UoM_Desc"].ToString();
                    lblAMeasRnd.ForeColor = Color.Black;
                    inputYRnd.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblAMeasRnd.Text = "Wrong Input!";
                    lblAMeasRnd.ForeColor = Color.Red;
                    inputAunRnd.Focus();
                    inputAunRnd.Text = "";
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
        //inputWeightUnit_onBlur
        protected void inputWeightUntRnd_onBlur(object sender, EventArgs e)
        {
            if (inputWeightUntRnd.Text == "")
            {
                lblWeightUntRnd.Text = absWeightUntRnd.Text;
                lblWeightUntRnd.ForeColor = Color.Black;
            }
            else if (inputWeightUntRnd.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputWeightUnt";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputWeightUnt", this.inputWeightUntRnd.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblWeightUntRnd.Text = dr["UoM_Desc"].ToString();
                    lblWeightUntRnd.ForeColor = Color.Black;
                    inputVolumeRnd.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblWeightUntRnd.Text = "Wrong Input!";
                    lblWeightUntRnd.ForeColor = Color.Red;
                    inputWeightUntRnd.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
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
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputSpcProc_onBlur
        protected void inputSpcProcRnd_onBlur(object sender, EventArgs e)
        {
            if (inputSpcProcRnd.Text == "")
            {
                lblSpcProcRnd.Text = absSpcProcRnd.Text;
                lblSpcProcRnd.ForeColor = Color.Black;
            }
            else if (inputSpcProcRnd.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                if (inputProcType.Text == "X" || inputProcType.Text == "x")
                {
                    var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE SpclProcurement = @inputSpcProc";
                    SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                    cmd.Parameters.AddWithValue("inputSpcProc", this.inputSpcProcRnd.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lblSpcProcRnd.Text = dr["SpclProcDesc"].ToString();
                        lblSpcProcRnd.ForeColor = Color.Black;
                    }
                    if (dr.HasRows == false)
                    {
                        lblSpcProcRnd.Text = "Wrong Input!";
                        lblSpcProcRnd.ForeColor = Color.Red;
                        inputSpcProcRnd.Text = "";
                        MsgBox("Wrong Input", this.Page, this);
                    }
                }
                else if (inputProcType.Text == "E" || inputProcType.Text == "e")
                {
                    var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE SpclProcurement = @inputSpcProc AND SpclProcurement = '50'";
                    SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                    cmd.Parameters.AddWithValue("inputSpcProc", this.inputSpcProcRnd.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lblSpcProcRnd.Text = dr["SpclProcDesc"].ToString();
                        lblSpcProcRnd.ForeColor = Color.Black;
                    }
                    if (dr.HasRows == false)
                    {
                        lblSpcProcRnd.Text = "Wrong Input!";
                        lblSpcProcRnd.ForeColor = Color.Red;
                        inputSpcProcRnd.Text = "";
                        MsgBox("Wrong Input", this.Page, this);
                    }
                }
                else if (inputProcType.Text == "F" || inputProcType.Text == "f")
                {
                    var queryString = "SELECT * FROM Mstr_SpecialProcurement WHERE SpclProcurement = @inputSpcProc AND SpclProcurement NOT LIKE'50'";
                    SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                    cmd.Parameters.AddWithValue("inputSpcProc", this.inputSpcProcRnd.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lblSpcProcRnd.Text = dr["SpclProcDesc"].ToString();
                        lblSpcProcRnd.ForeColor = Color.Black;
                    }
                    if (dr.HasRows == false)
                    {
                        lblSpcProcRnd.Text = "Wrong Input!";
                        lblSpcProcRnd.ForeColor = Color.Red;
                        inputSpcProcRnd.Text = "";
                        MsgBox("Wrong Input", this.Page, this);
                    }
                }
                conMatWorkFlow.Close();
            }
        }
        //inputCommImpCode_onBlur
        protected void inputCommImpCodeRnd_onBlur(object sender, EventArgs e)
        {
            if (inputCommImpCodeRnd.Text == "")
            {
                lblCommImpCodeRnd.Text = absCommImpCodeNoRnd.Text;
                lblCommImpCodeRnd.ForeColor = Color.Black;
            }
            else if (inputCommImpCodeRnd.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblCommImp", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputCommImpCode", this.inputCommImpCodeRnd.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblCommImpCodeRnd.Text = dr["ForeignDesc"].ToString();
                    lblCommImpCodeRnd.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblCommImpCodeRnd.Text = "Wrong Input!";
                    lblCommImpCodeRnd.ForeColor = Color.Red;
                    inputCommImpCodeRnd.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputVolUnt_onBlur
        protected void inputVolUntRnd_onBlur(object sender, EventArgs e)
        {
            if (inputVolUntRnd.Text == "")
            {
                lblVolUntRnd.Text = absVolUntRnd.Text;
                lblVolUntRnd.ForeColor = Color.Black;
            }
            else if (inputVolUntRnd.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputVolUnt";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputVolUnt", this.inputVolUntRnd.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblVolUntRnd.Text = dr["UoM_Desc"].ToString();
                    lblVolUntRnd.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblVolUntRnd.Text = "Wrong Input!";
                    lblVolUntRnd.ForeColor = Color.Red;
                    inputVolUntRnd.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputMatTyp_onBlur
        protected void inputMatTyp_onBlur(object sender, EventArgs e)
        {
            if (inputMatType.Text == "")
            {
                lblMatType.Text = absMatType.Text;
                lblMatType.ForeColor = Color.Black;
                inputMatType.Focus();
            }
            else if (inputMatType.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblMatTypeSampleSF", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputMatTyp", this.inputMatType.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblMatType.Text = dr["MatTypeDesc"].ToString();
                    lblMatType.ForeColor = Color.Black;
                    inputMatID.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblMatType.Text = "Wrong Input!";
                    lblMatType.ForeColor = Color.Red;
                    inputMatType.Focus();
                    inputMatType.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
            if (inputMatType.Text.ToUpper().Trim() == "SFAT")
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
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
            }
        }
        //inputBsUntMeas_onBlur
        protected void inputBsUntMeas_onBlur(object sender, EventArgs e)
        {
            if (inputBsUntMeasRnd.Text == "")
            {
                inputBunRnd.Text = "";
                lblBsUntMeasRnd.Text = absBsUntMeasRnd.Text;
                lblBMeasRnd.Text = absBsUntMeasRnd.Text;
                lblBsUntMeasRnd.ForeColor = Color.Black;
                lblBMeasRnd.ForeColor = Color.Black;
                inputBsUntMeasRnd.Focus();
            }
            else if (inputBsUntMeasRnd.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblBsUntMeas", conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputBsUntMeasRnd.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    inputBunRnd.Text = dr["UoM"].ToString();
                    inputNetWeightUnitRnd.Text = dr["UoM"].ToString();
                    inputWeightUntRnd.Text = dr["UoM"].ToString();
                    lblNetWeightUnitRnd.Text = dr["UoM_Desc"].ToString();
                    lblBsUntMeasRnd.Text = dr["UoM_Desc"].ToString();
                    lblBMeasRnd.Text = dr["UoM_Desc"].ToString();
                    lblNetWeightUnitRnd.ForeColor = Color.Black;
                    lblBsUntMeasRnd.ForeColor = Color.Black;
                    lblBMeasRnd.ForeColor = Color.Black;
                    inputMatGr.Focus();
                }
                if (dr.HasRows == false)
                {
                    lblBsUntMeasRnd.Text = "Wrong Input!";
                    lblBsUntMeasRnd.ForeColor = Color.Red;
                    inputBsUntMeasRnd.Focus();
                    inputBsUntMeasRnd.Text = "";
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

                if (inputPlant.Text == "5200")
                {
                    inputStrtgyGr.Text = "52";
                }
                else
                {
                    inputStrtgyGr.Text = "40";
                }
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

                    if (inputDivision.Text == "15")
                    {
                        if (inputPlant.Text == "2200")
                        {
                            if (inputMRPGr.Text == "0004")
                            {
                                inputStrtgyGr.Text = "52";
                            }
                        }
                    }
                    else
                    {
                        inputStrtgyGr.Text = "40";
                    }
                }
                if (dr.HasRows == false)
                {
                    lblDivision.Text = "Wrong Input!";
                    lblDivision.ForeColor = Color.Red;
                    inputDivision.Focus();
                    inputDivision.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);

                    if (inputPlant.Text == "5200")
                    {
                        inputStrtgyGr.Text = "52";
                    }
                    else
                    {
                        inputStrtgyGr.Text = "40";
                    }
                }
                conMatWorkFlow.Close();
            }
            bindLblStrategyGroup();
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
                inputStrtgyGr.Text = "40";
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
                    inputLoadingGrp.Text = inputPlant.Text;

                    if (inputPlant.Text == "5200")
                    {
                        inputStrtgyGr.Text = "52";
                    }
                    else if (inputPlant.Text == "2200")
                    {
                        if (inputDivision.Text == "15")
                        {
                            if (inputMRPGr.Text == "0004")
                            {
                                inputStrtgyGr.Text = "52";
                            }
                        }
                    }
                    else
                    {
                        inputStrtgyGr.Text = "40";
                    }
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
                    MsgBox("Wrong Input!", this.Page, this);

                    inputStrtgyGr.Text = "40";
                }
                conMatWorkFlow.Close();
            }
            srcStorLocModalBinding();
            srcLoadingGrpModalBindingProc();
            srcMRPGrModalBinding();
            srcMRPCtrlModalBinding();
            srcProdStorLocModalBinding();
            srcSchedMargKeyModalBinding();
            srcProdSchedModalBinding();
            srcProdSchedProfileModalBinding();

            bindLblStrategyGroup();
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

                    lblPlant.Text = absPlant.Text;
                    lblPlant.ForeColor = Color.Black;
                    inputPlant.Text = "";
                    lblStorLoc.Text = absStorLoc.Text;
                    lblStorLoc.ForeColor = Color.Black;
                    inputStorLoc.Text = "";

                    inputDistrChl.Focus();
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

                    MsgBox("Wrong Input!", this.Page, this);
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
                    inputDistrChl.Focus();
                    inputDistrChl.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
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
                    inputSpcProcRnd.Text = "";
                    lblSpcProcRnd.Text = absSpcProcRnd.Text;
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
                    inputSpcProcRnd.Text = "";
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
        //inputNetWeightUnt_onBlur
        protected void inputNetWeightUnt_onBlur(object sender, EventArgs e)
        {
            if (inputNetWeightUnitRnd.Text == "")
            {
                lblNetWeightUnitRnd.Text = absNetWeightUnitRnd.Text;
                lblNetWeightUnitRnd.ForeColor = Color.Black;
            }
            else if (inputNetWeightUnitRnd.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                var queryString = "SELECT * FROM Mstr_UoM WHERE UoM = @inputNetWeightUnt";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputNetWeightUnt", this.inputNetWeightUnitRnd.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblNetWeightUnitRnd.Text = dr["UoM_Desc"].ToString();
                    lblNetWeightUnitRnd.ForeColor = Color.Black;
                }
                if (dr.HasRows == false)
                {
                    lblNetWeightUnitRnd.Text = "Wrong Input!";
                    lblNetWeightUnitRnd.ForeColor = Color.Red;
                    inputNetWeightUnitRnd.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);
                }
                conMatWorkFlow.Close();
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
        //inputGrossWeight_onBlur
        protected void inputGrossWeight_TextChanged(object sender, EventArgs e)
        {
            decimal GrossWeight;
            decimal NetWeight;
            decimal.TryParse(inputGrossWeightRnd.Text, out GrossWeight);
            decimal.TryParse(inputNetWeight.Text, out NetWeight);
            decimal plus;
            decimal.TryParse("0.001", out plus);

            if (GrossWeight <= NetWeight)
            {
                MsgBox("Your gross weight cannot be less or equal with net weight!", this.Page, this);
                inputGrossWeightRnd.Text = (NetWeight + plus).ToString();
                return;
            }
        }
        //inputNetWeight_onBlur
        protected void inputNetWeight_TextChanged(object sender, EventArgs e)
        {
            decimal GrossWeight;
            decimal NetWeight;
            decimal.TryParse(inputGrossWeightRnd.Text, out GrossWeight);
            decimal.TryParse(inputNetWeight.Text, out NetWeight);
            decimal plus;
            decimal.TryParse("0.001", out plus);

            tmpBindRepeater();
            inputGrossWeightRnd.Text = (NetWeight + plus).ToString();
        }

        //Proc Onblur and Checkbox
        //inputPurcValKey_onBlur
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

        //Planner onblur and Checkbox
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

                if (inputPlant.Text == "5200")
                {
                    inputStrtgyGr.Text = "52";
                }
                else
                {
                    inputStrtgyGr.Text = "40";
                }
            }
            else if (inputMRPGr.Text.Length >= 1)
            {
                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }

                SqlCommand cmd = new SqlCommand("bindLblMRPGroupSample", conMatWorkFlow);
                cmd.Parameters.AddWithValue("MRPGroup", this.inputMRPGr.Text);
                cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
                cmd.Parameters.AddWithValue("MaterialID", this.inputMatID.Text);
                cmd.Parameters.AddWithValue("Plant", this.inputPlant.Text);
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
                            if (inputDivision.Text == "15")
                            {
                                inputStrtgyGr.Text = "52";
                            }
                        }
                    }
                    else
                    {
                        inputStrtgyGr.Text = "40";
                    }
                }
                if (dr.HasRows == false)
                {
                    lblMRPGr.Text = "Wrong Input!";
                    lblMRPGr.ForeColor = Color.Red;
                    inputMRPGr.Text = "";
                    MsgBox("Wrong Input!", this.Page, this);

                    if (inputPlant.Text == "5200")
                    {
                        inputStrtgyGr.Text = "52";
                    }
                    else
                    {
                        inputStrtgyGr.Text = "40";
                    }
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

                var queryString = "SELECT * FROM Mstr_MRPControllers WHERE MRPController = @inputMRPCtrl";
                SqlCommand cmd = new SqlCommand(queryString, conMatWorkFlow);
                cmd.Parameters.AddWithValue("inputMRPCtrl", this.inputMRPCtrl.Text);
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
                    Value = this.inputPlant.Text,
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
                    Value = this.inputMatID.Text.ToUpper().Trim(),
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

                SqlCommand cmd = new SqlCommand("srcProdSchedProfileSample", conMatWorkFlow);
                cmd.Parameters.Add("@Plant", SqlDbType.NVarChar).Value = this.inputPlant.Text;
                cmd.CommandType = CommandType.StoredProcedure;
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

        //QC onblur and checkbox
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

        //QA onblur and checkbox
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

        //QR onblur and checkbox
        //checkbox Preferedins Type
        protected void chkbxQMProcActive_CheckedChanged(object senders, EventArgs e)
        {
            if (chkbxQMProcActive.Checked == true)
            {
                chkbxValue = "X";
                lblChkbx.Text = chkbxValue.ToString().Trim();
                lblQMProcActive.Text = "Active";
                inputQMCtrlKey.Text = "Z990";
            }
            else
            {
                chkbxValue = "";
                lblChkbx.Text = chkbxValue.ToString().Trim();
                lblQMProcActive.Text = "Non Active";
                inputQMCtrlKey.Text = "";
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

            SqlCommand cmd = new SqlCommand("bindLblMRPGroupSample", conMatWorkFlow);
            cmd.Parameters.AddWithValue("MRPGroup", this.inputMatGr.Text);
            cmd.Parameters.AddWithValue("TransID", this.lblTransID.Text);
            cmd.Parameters.AddWithValue("MaterialID", this.inputMatID.Text);
            cmd.Parameters.AddWithValue("Plant", this.inputPlant.Text);
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
        protected void bindLblBsUntMeas()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }

            SqlCommand cmd = new SqlCommand("bindLblBsUntMeas", conMatWorkFlow);
            cmd.Parameters.AddWithValue("inputBsUntMeas", this.inputBsUntMeasRnd.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblBsUntMeasRnd.Text = dr["UoM_Desc"].ToString();
                lblBsUntMeasRnd.ForeColor = Color.Black;
                lblNetWeightUnitRnd.Text = dr["UoM_Desc"].ToString();
                lblNetWeightUnitRnd.ForeColor = Color.Black;
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
                //lblPckgMatProc.Text = dr["MatlGrpPack_Desc"].ToString();
                //lblPckgMatProc.ForeColor = Color.Black;
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
            cmd.Parameters.AddWithValue("Type", this.rmsffgMenuLabel.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMatType.Text = dr["MatTypeDesc"].ToString();
                lblMatType.ForeColor = Color.Black;
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
            cmdP.Parameters.AddWithValue("inputPlant", this.inputPlant.Text);
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
            cmd.Parameters.AddWithValue("inputCommImpCode", this.inputCommImpCodeRnd.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblCommImpCodeRnd.Text = dr["ForeignDesc"].ToString();
                lblCommImpCodeRnd.ForeColor = Color.Black;
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
                    var dt = GetData_srcListViewMatID("%" + inputMatIDDESC.Text + "%", "R&D", "X", lblPosition.Text.Trim());

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
                    var dt = GetData_srcListViewMatDESC("%" + inputMatIDDESC.Text + "%", "R&D", "X", lblPosition.Text.Trim());

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
            gridSearch(FilterSearch.Text);
        }
    }
}