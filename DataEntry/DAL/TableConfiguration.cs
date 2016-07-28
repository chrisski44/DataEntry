﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure.Interception;
using DataEntry.DAL;
using DataEntry.Logging;

namespace DataEntry.DAL
{
    public class TableConfiguration : DbConfiguration
    {
        public TableConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new TableInterceptorLogging());
            

        }

    }
}