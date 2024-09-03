using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using testForm;

namespace HR.Controllers
{
    public class LeaveDetailControllers
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public List<Models.InsertAndUpdate> LeaveDetail(string NIK, string FromDate, string Todate)
        {
            object[] param = new[] { NIK, FromDate, Todate };
            List<Models.InsertAndUpdate> Result = dc.ExecuteQuery<Models.InsertAndUpdate>("Exec LeaveDetail_Report @NIK={0},@FromDate={1},@ToDate={2}", param).ToList<Models.InsertAndUpdate>();
            return Result;
        }
    }
}