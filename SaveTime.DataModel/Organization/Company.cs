using SaveTime.DataModel.Marker;
using System.Collections.Generic;

namespace SaveTime.DataModel.Organization
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public virtual IList<Branch> Branches { get; set; }
        public Company()
        {
            Branches = new List<Branch>();
        }
    }
}
