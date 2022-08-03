using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentBD.Models
{
    public class ViewTollModel
    {
        public Guid GuidId { get; set; }
        [Required]
        public string NumberPlate { get; set; }
        [Required]
        public string InterChange { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}