﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace testForm.Controllers
{
    public class surchargeMaterialController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public List<Models.InsertAndUpdate> surchargeMaterial(string FromDate, string ToDate)
        {
            object[] param = new[] { FromDate, ToDate };
            List<Models.InsertAndUpdate> Result = dc.ExecuteQuery<Models.InsertAndUpdate>("Exec Report_SurchargeMaterial @FromDate={0},@ToDate={1}", param).ToList<Models.InsertAndUpdate>();
            return Result;
        }
    }
}