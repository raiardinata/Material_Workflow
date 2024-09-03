using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using testForm;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.IO;
using System.Data;
using System.Web.Optimization;

namespace testForm
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        [Serializable]
        public sealed class MyReportServerCredentials :
        IReportServerCredentials
        {
            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    // Use the default Windows user.  Credentials will be
                    // provided by the NetworkCredentials property.
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    // Read the user information from the Web.config file.  
                    // By reading the information on demand instead of 
                    // storing it, the credentials will not be stored in 
                    // session, reducing the vulnerable surface area to the
                    // Web.config file, which can be secured with an ACL.

                    // User name
                    //string userName = "admin";
                    string userName = ConfigurationManager.AppSettings["MyReportViewerUser"];

                    //if (string.IsNullOrEmpty(userName))
                    //  throw new Exception(
                    //    "Missing user name from web.config file");

                    // Password
                    //string password = "1Tind3550";
                    string password = ConfigurationManager.AppSettings["MyReportViewerPassword"];

                    //if (string.IsNullOrEmpty(password))
                    //  throw new Exception(
                    //    "Missing password from web.config file");

                    // Domain
                    //string domain = "hrid";
                    string domain = ConfigurationManager.AppSettings["MyReportViewerDomain"];

                    //if (string.IsNullOrEmpty(domain))
                    //  throw new Exception(
                    //    "Missing domain from web.config file");

                    return new NetworkCredential(userName, password, domain);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie,
                        out string userName, out string password,
                        out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;

                // Not using form credentials
                return false;
            }
        }
    }
}