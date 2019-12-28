using SaveTime.DataModel.Marker;
using System;
using System.Collections.Generic;

namespace SaveTime.DataModel.Organization
{
    public class Branch : IEntity
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public int StepWork { get; set; }
        public virtual Company Company { get; set; }
        public virtual IList<Employee> Employees { get; set; }
        public Branch()
        {
            Employees = new List<Employee>();
        }
    }
}
