using SaveTime.DataModel.Marker;

namespace SaveTime.DataModel.Organization
{
    public class Employer : IEntity, IAccountOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Account Account { get; set; }
    }
}
