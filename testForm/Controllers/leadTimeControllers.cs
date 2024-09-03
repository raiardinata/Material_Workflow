using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace testForm.Controllers
{
    public class leadTimeControllers : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public List<Models.InsertAndUpdate> leadTime(string MaterialID, string ModuleUser, string FromDate, string ToDate)
        {
            object[] param = new[] { MaterialID, ModuleUser, FromDate, ToDate };
            List<Models.InsertAndUpdate> Result = dc.ExecuteQuery<Models.InsertAndUpdate>("Exec Report_LeadTime @MaterialID={0},@Module_User={1},@FromDate={2},@ToDate={3}", param).ToList<Models.InsertAndUpdate>();
            return Result;
        }
    }
}