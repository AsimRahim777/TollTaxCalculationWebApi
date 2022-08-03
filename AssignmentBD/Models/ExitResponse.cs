using AssignmentBD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AssignmentBD.Common.CommonUtitlity;

namespace AssignmentBD.Models
{
    public class ExitResponse : CommonProps
    {
        public ViewExitResponse ResponseData { get; set; }
    }
    public class ViewExitResponse
    {
        public double BaseRate { get; set; }
        public double DistanceCost { get; set; }
        public double Discount { get; set; }
        public double SubTotal { get; set; }
        public double TotalCharges { get; internal set; }
    }
}