using SaveTime.DataModel.Dictionary;
using SaveTime.DataModel.Marker;
using System.Collections.Generic;

namespace SaveTime.DataModel.Organization
{
    public class Employee : IEntity, IAccountOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual IList<Service> Services { get; set; }
        public Employee()
        {
            Services = new List<Service>();
        }
    }
}
