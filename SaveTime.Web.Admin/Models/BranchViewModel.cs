using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveTime.Web.Admin.Models
{
    public class BranchViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public int StepWork { get; set; }
    }
}