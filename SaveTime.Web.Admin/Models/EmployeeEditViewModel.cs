using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveTime.Web.Admin.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<string> BranchAddresses { get; set; }=new List<string>();
        public IList<string> AccountPhones { get; set; }  =new List<string>();
        public IList<string> AccountEmails { get; set; }  =new List<string>();
        public IList<string> BranchPhones { get; set; }   =new List<string>();
    }
}