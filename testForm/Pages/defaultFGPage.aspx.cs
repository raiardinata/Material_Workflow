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
    public partial class defaultFGPage : System.Web.UI.Page
    {
        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((String)Session["Usnam"]))
            {
                Response.Redirect("~/Pages/loginPage");
            }
            else
            {
                lblUser.Text = Session["Usnam"].ToString().Trim();
                lblPosition.Text = Session["Devisi"].ToString().Trim();
            }
            if(!IsPostBack)
            {
                defaultValueBinding();
            }
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        protected void defaultValueBinding()
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("defaultValue", conMatWorkFlow);
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = this.lblType.Text;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.HasRows == true)
                    {
                        inputPriceUnit.Text = dr["PriceUnit"].ToString().Trim();
                        inputGenItemCatGroup.Text = dr["ItemCatGrp"].ToString().Trim();
                        inputXPlantMatStatus.Text = dr["XPlantMatStatus"].ToString().Trim();
                        inputStatus.Text = dr["AssignmentStatus"].ToString().Trim();
                        inputPlantSPStatus.Text = dr["PlantSPMatStatus"].ToString().Trim();
                        inputBatchEntry.Text = dr["BatchEntry"].ToString().Trim();
                        inputPeriodIndicator.Text = dr["PeriodInd"].ToString().Trim();
                        inputBatchManagement.Text = dr["BatchManagement"].ToString().Trim();
                        inputPeriodIndForSELD.Text = dr["PeriodIndForSELD"].ToString().Trim();
                        inputTransGrp.Text = dr["TransGrp"].ToString().Trim();
                        inputXDistrChainStatus.Text = dr["XDistChainStatus"].ToString().Trim();
                        inputCashDiscount.Text = dr["CashDisc"].ToString().Trim();
                        inputTaxClassification.Text = dr["TaxClasification"].ToString().Trim();
                        inputMatlStatisticGroup.Text = dr["MatStatisticGrp"].ToString().Trim();
                    }
                    else
                    {

                    }
                }
                conMatWorkFlow.Close();
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString().Trim(), this.Page, this);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (conMatWorkFlow.State == ConnectionState.Closed)
            {
                conMatWorkFlow.Open();
            }
                SqlCommand cmd = new SqlCommand("defaultValueBtnSave", conMatWorkFlow);
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = "FG";
                cmd.Parameters.Add("@PriceUnit", SqlDbType.NVarChar).Value = this.inputPriceUnit.Text.Trim().ToUpper();
                cmd.Parameters.Add("@ItemCatGrp", SqlDbType.NVarChar).Value = this.inputGenItemCatGroup.Text.Trim().ToUpper();
                cmd.Parameters.Add("@XPlantMatStatus", SqlDbType.NVarChar).Value = this.inputXPlantMatStatus.Text.Trim().ToUpper();
                cmd.Parameters.Add("@AssignmentStatus", SqlDbType.NVarChar).Value = this.inputStatus.Text.Trim().ToUpper();
                cmd.Parameters.Add("@PlantSPMatStatus", SqlDbType.NVarChar).Value = this.inputPlantSPStatus.Text.Trim().ToUpper();
                cmd.Parameters.Add("@BatchEntry", SqlDbType.NVarChar).Value = this.inputBatchEntry.Text.Trim().ToUpper();
                cmd.Parameters.Add("@PeriodInd", SqlDbType.NVarChar).Value = this.inputPeriodIndicator.Text.Trim().ToUpper();
                cmd.Parameters.Add("@BatchManagement", SqlDbType.NVarChar).Value = this.inputBatchManagement.Text.Trim().ToUpper();
                cmd.Parameters.Add("@PeriodIndForSELD", SqlDbType.NVarChar).Value = this.inputPeriodIndForSELD.Text.Trim().ToUpper();
                cmd.Parameters.Add("@TransGrp", SqlDbType.NVarChar).Value = this.inputTransGrp.Text.Trim().ToUpper();
                cmd.Parameters.Add("@XDistChainStatus", SqlDbType.NVarChar).Value = this.inputXDistrChainStatus.Text.Trim().ToUpper();
                cmd.Parameters.Add("@CashDisc", SqlDbType.NVarChar).Value = this.inputCashDiscount.Text.Trim().ToUpper();
                cmd.Parameters.Add("@TaxClasification", SqlDbType.NVarChar).Value = this.inputTaxClassification.Text.Trim().ToUpper();
                cmd.Parameters.Add("@MatStatisticGrp", SqlDbType.NVarChar).Value = this.inputMatlStatisticGroup.Text.Trim().ToUpper();
            cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conMatWorkFlow.Close();
                Response.Redirect("~/Pages/homePage");
        }
        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/homePage");
        }
    }
}