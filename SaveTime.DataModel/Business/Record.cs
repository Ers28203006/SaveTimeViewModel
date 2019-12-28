using SaveTime.DataModel.Marker;
using SaveTime.DataModel.Organization;
using System;

namespace SaveTime.DataModel.Business
{
    public class Record : IEntity
    {
        public int Id { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Client Client { get; set; }
        public DateTime Time { get; set; }
    }
}
