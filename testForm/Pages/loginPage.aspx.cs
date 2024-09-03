using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testForm.Pages
{
    public partial class loginPage : System.Web.UI.Page
    {
        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            userid.Attributes.Add("onFocus", "onFocus(userid)");
            psw.Attributes.Add("onFocus", "onFocus(psw)");
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        protected void loginValidation_Click(object sender, EventArgs e)
        {
            userid.Text.ToLower();
            if (ValidateUser())
            {
                FormsAuthentication.RedirectFromLoginPage(userid.Text, false);
                FormsAuthentication.RedirectFromLoginPage(psw.Text, false);
                if(conMatWorkFlow.State == ConnectionState.Closed)
                {
                    conMatWorkFlow.Open();
                }
                //Maintain User Login Value
                SqlCommand cmdDTL = new SqlCommand("Select * from Mstr_User WHERE Usnam = @UserID", conMatWorkFlow);
                cmdDTL.Parameters.Add(new SqlParameter
                {
                    ParameterName = "UserID",
                    Value = this.userid.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50
                });
                using (SqlDataReader rdr = cmdDTL.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Session["Usnam"] = rdr["Usnam"].ToString().Trim();
                        Session["Devisi"] = rdr["Devisi"].ToString().Trim();
                        Session["UsnamPlant"] = rdr["Plant"].ToString().Trim();
                    }
                }
                conMatWorkFlow.Close();
                Response.Redirect("~/Pages/homePage");
            }
            else
            {
                MsgBox("Wrong Username or Password!", this.Page, this);
                userid.Focus();
            }
        }

        private Boolean ValidateUser()
        {
            userid.Text.ToLower();
            Boolean result = false;
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM Mstr_User", conMatWorkFlow);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["Usnam"].ToString().Trim() == userid.Text.ToLower() && dr["Pswrd"].ToString().Trim() == psw.Text)
                {
                    result = true;
                    break;
                }
            }
            conMatWorkFlow.Close();
            return result;
        }
    }
}