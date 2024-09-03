using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR.Pages.ContentPages
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Print"].ToString() != "")
            {
                string FilePath = Server.MapPath("../PDF/" + Session["Print"].ToString() + ".pdf");

                WebClient User = new WebClient();

                Byte[] FileBuffer = User.DownloadData(FilePath);

                if (FileBuffer != null)
                {

                    Response.ContentType = "application/pdf";

                    Response.AddHeader("content-length", FileBuffer.Length.ToString());

                    Response.BinaryWrite(FileBuffer);


                }


                if ((System.IO.File.Exists(Server.MapPath("../PDF/" + Session["Print"].ToString() + ".pdf"))))
                {
                    System.IO.File.Delete(Server.MapPath("../PDF/" + Session["Print"].ToString() + ".pdf"));
                }
                Session["Print"] = "";
            }
        }
    }
}
