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
    public partial class masterUser : System.Web.UI.Page
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

                if (lblPosition.Text != "Admin")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
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

                    inputPassword.Text = dtCheck.Rows[0]["Pswrd"].ToString().Trim();
                    inputName.Text = dtCheck.Rows[0]["Name"].ToString().Trim();
                    inputDivision.Text = dtCheck.Rows[0]["Devisi"].ToString().Trim();
                    inputSOrg.Text = dtCheck.Rows[0]["SOrg"].ToString().Trim();

                    if (inputSOrg.Text == "8200")
                    {
                        chkbx2100.Visible = true;
                        chkbx2200.Visible = true;
                        chkbx2300.Visible = true;
                        chkbx2400.Visible = true;
                    }
                    else if (inputSOrg.Text == "8300")
                    {
                        chkbx3100.Visible = true;
                        chkbx3200.Visible = true;
                        chkbx3300.Visible = true;
                        chkbx3400.Visible = true;
                    }
                    else if (inputSOrg.Text == "8500")
                    {
                        chkbx5100.Visible = true;
                        chkbx5200.Visible = true;
                    }

                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        if (dr["Plant"].ToString().Trim() == "1100")
                        {
                            chkbx1100.Checked = true;
                            chkbx1100.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "2100")
                        {
                            chkbx2100.Checked = true;
                            chkbx2100.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "2200")
                        {
                            chkbx2200.Checked = true;
                            chkbx2200.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "2300")
                        {
                            chkbx2300.Checked = true;
                            chkbx2300.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "2400")
                        {
                            chkbx2400.Checked = true;
                            chkbx2400.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "3100")
                        {
                            chkbx3100.Checked = true;
                            chkbx3100.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "3200")
                        {
                            chkbx3200.Checked = true;
                            chkbx3200.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "3300")
                        {
                            chkbx3300.Checked = true;
                            chkbx3300.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "3400")
                        {
                            chkbx3400.Checked = true;
                            chkbx3400.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "5100")
                        {
                            chkbx5100.Checked = true;
                            chkbx5100.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "5200")
                        {
                            chkbx5200.Checked = true;
                            chkbx5200.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "6100")
                        {
                            chkbx6100.Checked = true;
                            chkbx6100.Enabled = false;
                        }
                        else if (dr["Plant"].ToString().Trim() == "6200")
                        {
                            chkbx6200.Checked = true;
                            chkbx6200.Enabled = false;
                        }
                    }
                }

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
                inputUserID.SelectedValue = "admin";

            }
            else if (IsPostBack)
            {

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
            string P1100 = "";
            string P2100 = "";
            string P2200 = "";
            string P2300 = "";
            string P2400 = "";
            string P3100 = "";
            string P3200 = "";
            string P3300 = "";
            string P3400 = "";
            string P5100 = "";
            string P5200 = "";
            string P6100 = "";
            string P6200 = "";
            if (chkbx1100.Checked == true)
            {
                P1100 = "1";
            }
            if (chkbx2100.Checked == true)
            {
                P2100 = "1";
            }
            if (chkbx2200.Checked == true)
            {
                P2200 = "1";
            }
            if (chkbx2300.Checked == true)
            {
                P2300 = "1";
            }
            if (chkbx2400.Checked == true)
            {
                P2400 = "1";
            }
            if (chkbx3100.Checked == true)
            {
                P3100 = "1";
            }
            if (chkbx3200.Checked == true)
            {
                P3200 = "1";
            }
            if (chkbx3300.Checked == true)
            {
                P3300 = "1";
            }
            if (chkbx3400.Checked == true)
            {
                P3400 = "1";
            }
            if (chkbx5100.Checked == true)
            {
                P5100 = "1";
            }
            if (chkbx5200.Checked == true)
            {
                P5200 = "1";
            }
            if (chkbx6100.Checked == true)
            {
                P6100 = "1";
            }
            if (chkbx6200.Checked == true)
            {
                P6200 = "1";
            }

            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("saveMasterUser", conMatWorkFlow);
                cmd.Parameters.AddWithValue("Usnam", inputUserID.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("Email", inputEmail.Text.ToUpper().Trim());
                cmd.Parameters.AddWithValue("P1100", P1100);
                cmd.Parameters.AddWithValue("P2100", P2100);
                cmd.Parameters.AddWithValue("P2200", P2200);
                cmd.Parameters.AddWithValue("P2300", P2300);
                cmd.Parameters.AddWithValue("P2400", P2400);
                cmd.Parameters.AddWithValue("P3100", P3100);
                cmd.Parameters.AddWithValue("P3200", P3200);
                cmd.Parameters.AddWithValue("P3300", P3300);
                cmd.Parameters.AddWithValue("P3400", P3400);
                cmd.Parameters.AddWithValue("P5100", P5100);
                cmd.Parameters.AddWithValue("P5200", P5200);
                cmd.Parameters.AddWithValue("P6100", P6100);
                cmd.Parameters.AddWithValue("P6200", P6200);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
                return;
            }
            Response.Write("<script language='javascript'>window.alert('Successfully change master user!');window.location='homePage';</script>");
        }
        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/homePage");
        }

        protected void chkbxReset_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void inputUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bunch of checkboxes
            chkbx1100.Checked = false;
            chkbx2100.Checked = false;
            chkbx2200.Checked = false;
            chkbx2300.Checked = false;
            chkbx2400.Checked = false;
            chkbx3100.Checked = false;
            chkbx3200.Checked = false;
            chkbx3300.Checked = false;
            chkbx3400.Checked = false;
            chkbx5100.Checked = false;
            chkbx5200.Checked = false;
            chkbx6100.Checked = false;
            chkbx6200.Checked = false;

            chkbx1100.Visible = false;
            chkbx2100.Visible = false;
            chkbx2200.Visible = false;
            chkbx2300.Visible = false;
            chkbx2400.Visible = false;
            chkbx3100.Visible = false;
            chkbx3200.Visible = false;
            chkbx3300.Visible = false;
            chkbx3400.Visible = false;
            chkbx5100.Visible = false;
            chkbx5200.Visible = false;
            chkbx6100.Visible = false;
            chkbx6200.Visible = false;

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
                inputPassword.Text = dtCheck.Rows[0]["Pswrd"].ToString().Trim();
                inputName.Text = dtCheck.Rows[0]["Name"].ToString().Trim();
                inputDivision.Text = dtCheck.Rows[0]["Devisi"].ToString().Trim();
                inputEmail.Text = dtCheck.Rows[0]["Email"].ToString().Trim();

                var distinctValues = dtCheck.AsEnumerable().Select(row => new
                {
                    sOrg = row.Field<string>("SOrg")
                }).Distinct();

                inputSOrg.Text = " ";


                //inputSOrg.Text = distinctValues.ElementAt(0).sOrg.ToString();
                foreach (var item in distinctValues)
                {
                    if (item.sOrg == null) { }
                    else
                    {
                        inputSOrg.Text += item.sOrg.ToString();

                        inputSOrg.Text += " ";
                    }

                }


                foreach (DataRow dr in dtCheck.Rows)
                {

                    //S Org
                    if (dr["SOrg"].ToString().Trim() == "8200")
                    {
                        chkbx2100.Visible = true;
                        chkbx2200.Visible = true;
                        chkbx2300.Visible = true;
                        chkbx2400.Visible = true;
                    }
                    else if (dr["SOrg"].ToString().Trim() == "8300")
                    {
                        chkbx3100.Visible = true;
                        chkbx3200.Visible = true;
                        chkbx3300.Visible = true;
                        chkbx3400.Visible = true;
                    }
                    else if (dr["SOrg"].ToString().Trim() == "8500")
                    {
                        chkbx5100.Visible = true;
                        chkbx5200.Visible = true;
                    }

                    //Plant
                    if (dr["Plant"].ToString().Trim() == "1100")
                    {
                        chkbx1100.Checked = true;
                        chkbx1100.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "2100")
                    {
                        chkbx2100.Checked = true;
                        chkbx2100.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "2200")
                    {
                        chkbx2200.Checked = true;
                        chkbx2200.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "2300")
                    {
                        chkbx2300.Checked = true;
                        chkbx2300.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "2400")
                    {
                        chkbx2400.Checked = true;
                        chkbx2400.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "3100")
                    {
                        chkbx3100.Checked = true;
                        chkbx3100.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "3200")
                    {
                        chkbx3200.Checked = true;
                        chkbx3200.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "3300")
                    {
                        chkbx3300.Checked = true;
                        chkbx3300.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "3400")
                    {
                        chkbx3400.Checked = true;
                        chkbx3400.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "5100")
                    {
                        chkbx5100.Checked = true;
                        chkbx5100.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "5200")
                    {
                        chkbx5200.Checked = true;
                        chkbx5200.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "6100")
                    {
                        chkbx6100.Checked = true;
                        chkbx6100.Enabled = false;
                    }
                    else if (dr["Plant"].ToString().Trim() == "6200")
                    {
                        chkbx6200.Checked = true;
                        chkbx6200.Enabled = false;
                    }
                }
            }
        }
    }
}