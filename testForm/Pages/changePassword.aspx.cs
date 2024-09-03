using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testForm.Pages
{
    public partial class changePassword : System.Web.UI.Page
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

                    ListItem li = new ListItem(Session["Usnam"].ToString().Trim(), Session["Usnam"].ToString().Trim(), true);
                    inputUserID.Items.Clear();
                    inputUserID.Items.Add(li);
                }

                // doing binding
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Usnam", this.lblUser.Text.Trim());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("checkPassword", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // binding password to oldPassword textbox
                    inputOldPassword.Text = dtCheck.Rows[0]["Pswrd"].ToString();
                }
                if(lblPosition.Text == "Admin")
                {
                    chkbxReset.Visible = true;
                }
            }
            else if (IsPostBack)
            {
                //srcListViewBinding();
                // doing binding
                DataTable dtCheck = new DataTable();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Usnam", this.inputUserID.SelectedValue.Trim());

                //send param to SP sql
                dtCheck = sqlC.ExecuteDataTable("checkPassword", param);

                if (dtCheck.Rows.Count > 0)
                {
                    // binding password to oldPassword textbox
                    inputOldPassword.Text = dtCheck.Rows[0]["Pswrd"].ToString();
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


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (inputConfirmNewPassword.Text != inputNewPassword.Text)
            {
                inputConfirmNewPassword.Text = "";
                MsgBox("Your confirmation password did not match with the new password.", this.Page, this);
                return;
            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("saveChangePassword", conMatWorkFlow);
                cmd.Parameters.AddWithValue("Usnam", inputUserID.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("Pswrd", inputConfirmNewPassword.Text.Trim());
                cmd.Parameters.AddWithValue("Reset", "");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();
            }
            catch(Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Write("<script language='javascript'>window.alert('Successfully change password!');window.location='homePage';</script>");
        }
        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/homePage");
        }

        protected void chkbxReset_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxReset.Checked == true)
            {
                lblReset.Text = "X";
                inputNewPassword.Text = "initial";
                inputConfirmNewPassword.Text = "initial";
                inputNewPassword.ReadOnly = true;
                inputConfirmNewPassword.ReadOnly = true;
                inputNewPassword.CssClass = "txtBoxRO";
                inputConfirmNewPassword.CssClass = "txtBoxRO";
                inputUserID.Items.Clear();

                if (conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT Usnam FROM Mstr_User", conMatWorkFlow);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    ListItem li = new ListItem(dr["Usnam"].ToString().Trim(), dr["Usnam"].ToString().Trim(), true);
                    inputUserID.Items.Add(li);
                }
                conMatWorkFlow.Close();
            }
            else
            {
                lblReset.Text = "";
                inputNewPassword.ReadOnly = false;
                inputConfirmNewPassword.ReadOnly = false;
                inputNewPassword.CssClass = "txtBox";
                inputConfirmNewPassword.CssClass = "txtBox";

                ListItem li = new ListItem(Session["Usnam"].ToString().Trim(), Session["Usnam"].ToString().Trim(), true);
                inputUserID.Items.Clear();
                inputUserID.Items.Add(li);
            }
        }

        protected void inputUserID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}