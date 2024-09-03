using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
//using System.Windows.Forms;
//using System.Net;
//using System.Net.CredentialCache.DefaultCredentials;

using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using static testForm.Global;

namespace testForm.Pages
{
    public partial class reportPage : System.Web.UI.Page
    {
        Controllers.leadTimeControllers LT = new Controllers.leadTimeControllers();
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
                if (lblPosition.Text != "R&D" && lblPosition.Text != "R&D MGR" && lblPosition.Text != "PD" && lblPosition.Text != "PD MGR" && lblPosition.Text != "Admin" && lblPosition.Text != "QA" && lblPosition.Text != "FICO" && lblPosition.Text != "FICO MGR")
                {
                    Response.Write("<script language='javascript'>window.alert('You do not have authorization for this page!');window.location='homePage';</script>");
                    return;
                }
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

        protected void GenerateReport(string MaterialID, string ModuleUser, string FromDate, string ToDate)
        {
            /*if (MaterialID.Trim() != "" && ToDate.Trim() != "")
            {*/
            //try
            //{
                var leadTimeReport = LT.leadTime(MaterialID, ModuleUser, FromDate, ToDate);

                if (leadTimeReport.Count() > 0)
                {
                    lblError.Visible = false;
                    lblError.Text = string.Empty;

                    rViewer.Visible = true;
                    rViewer.ProcessingMode = ProcessingMode.Remote;

                    ServerReport serverReport = rViewer.ServerReport;


                    serverReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerURL"].ToString());
                    serverReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"].ToString() + "Rpt_LeadTime";

                    ReportParameter[] paramreport = new ReportParameter[4];
                    paramreport[0] = new ReportParameter("MaterialID", MaterialID);
                    paramreport[1] = new ReportParameter("Module_User", ModuleUser);
                    paramreport[2] = new ReportParameter("FromDate", FromDate);
                    paramreport[3] = new ReportParameter("ToDate", ToDate);

                    rViewer.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                    rViewer.ShowParameterPrompts = false;
                    rViewer.Width = Unit.Percentage(100);
                    rViewer.Height = Unit.Percentage(95);
                    rViewer.ServerReport.SetParameters(paramreport);
                    rViewer.ServerReport.Refresh();
                }
                else
                {
                    rViewer.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "There is no data for this MaterialID";
                }
            //}
            //catch(Exception ex)
            //{
            //    MsgBox(ex.ToString(), this.Page, this);
            //}
        }
        /*else
        {
            rViewer.Visible = false;
            lblError.Visible = true;
            lblError.Text = "From Date And To Date Cant Empty";
        }*/
        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GenerateReport(inputMatID.Text, Session["Devisi"].ToString(), txtDateStart.Text, txtDateEnd.Text);
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            Session["Print"] = HttpContext.Current.Session["ActiveUser"].ToString() + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("Hmmss");
            byte[] bytes = rViewer.ServerReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);
            using (FileStream fs = new FileStream(Server.MapPath("../../PDF/" + Session["Print"].ToString() + ".pdf"), FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "Open", "window.open('Print.aspx','_newtab');", true);
        }
    }    
}