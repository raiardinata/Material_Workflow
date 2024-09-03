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
    public partial class resetPassword : System.Web.UI.Page
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

                    inputUserID.Text = Session["Usnam"].ToString().Trim();
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
                cmd.Parameters.AddWithValue("Usnam", inputUserID.Text.Trim());
                cmd.Parameters.AddWithValue("Pswrd", inputConfirmNewPassword.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();
                MsgBox("Successfully change password!", this.Page, this);
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Redirect("~/Pages/changePassword");
        }
        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/cancelPage");
        }
    }
}