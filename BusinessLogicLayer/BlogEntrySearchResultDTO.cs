using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class BlogEntrySearchResultDTO
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}