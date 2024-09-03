using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace testForm.Controllers
{
    public class mrpTypeControllers : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public List<Models.InsertAndUpdate> MRPTypeValidation()
        {
            object[] param = new[] {""};
            List<Models.InsertAndUpdate> Result = dc.ExecuteQuery<Models.InsertAndUpdate>("Exec bindCommonTableMRPType", param).ToList<Models.InsertAndUpdate>();
            return Result;
        }
    }
}