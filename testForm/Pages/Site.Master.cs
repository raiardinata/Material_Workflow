using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testForm
{
    public partial class SiteMaster : MasterPage
    {
        SqlConnection conMatWorkFlow = new SqlConnection(ConfigurationManager.ConnectionStrings["MATWORKFLOWCONNECTIONSTRING"].ConnectionString.ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usnam"].ToString().Trim() != null)
                {
                    if (Session["Devisi"].ToString().Trim() == "R&D" || Session["Devisi"].ToString().Trim() == "R&D MGR" || Session["Devisi"].ToString().Trim() == "PD" || Session["Devisi"].ToString().Trim() == "PD MGR" || Session["Devisi"].ToString().Trim() == "BD MGR")
                    {
                        SiteMapDataSource1.SiteMapProvider = "rndProvider";
                    }
                    else if (Session["Devisi"].ToString().Trim() == "Proc")
                    {
                        SiteMapDataSource1.SiteMapProvider = "procProvider";
                    }
                    else if (Session["Devisi"].ToString().Trim() == "Planner")
                    {
                        SiteMapDataSource1.SiteMapProvider = "planProvider";
                    }
                    else if (Session["Devisi"].ToString().Trim() == "QC")
                    {
                        SiteMapDataSource1.SiteMapProvider = "qcProvider";
                    }
                    else if (Session["Devisi"].ToString().Trim() == "QA")
                    {
                        SiteMapDataSource1.SiteMapProvider = "qaProvider";
                    }
                    else if (Session["Devisi"].ToString().Trim() == "QR")
                    {
                        SiteMapDataSource1.SiteMapProvider = "qrProvider";
                    }
                    else if (Session["Devisi"].ToString().Trim() == "FICO" || Session["Devisi"].ToString().Trim() == "FICO MGR")
                    {
                        if (Session["Devisi"].ToString().Trim() == "FICO MGR")
                        {
                            SiteMapDataSource1.SiteMapProvider = "ficoMgrProvider";
                        }
                        else
                        {
                            SiteMapDataSource1.SiteMapProvider = "ficoProvider";
                        }
                    }
                    else if (Session["Devisi"].ToString().Trim() == "Admin")
                    {
                        SiteMapDataSource1.SiteMapProvider = "adminProvider";
                    }
                }
            }
            catch
            {
                Response.Redirect("~/Pages/loginPage");
            }
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        protected void NavigationMenu_OnMenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                MenuItem item = e.Item;
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
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Pages/loginPage");
        }
    }
}