﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testForm.Pages
{
    public partial class homePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((String)Session["Usnam"]))
            {
                Response.Redirect("~/Pages/loginPage");
            }
            else
            {
                lblUser.Text = Session["Usnam"].ToString().Trim();
            }
        }
    }
}