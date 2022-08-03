using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentBD.Models
{
    public class TollModel
    {
        [Required(ErrorMessage ="Vehicle Number")]
        public string NumberPlate { get; set; }
        [Required(ErrorMessage = "Select Interchange")]
        public string InterChange { get; set; }
        [Required(ErrorMessage = "Date is Required")]
        public DateTime DateTime { get; set; }
    }

}