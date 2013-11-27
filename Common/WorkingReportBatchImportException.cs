using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.Common
{
    public class WorkingReportBatchImportException:Exception
    {
        public WorkingReportBatchImportException(string message) : base(message)
        {
        }
    }
}