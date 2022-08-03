using AssignmentBD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using static AssignmentBD.Common.CommonUtitlity;

namespace AssignmentBD.Models
{
    public class EntryResponse: CommonProps
    {
        public ViewTollModel ResponseData { get; set; }
       
    }
}