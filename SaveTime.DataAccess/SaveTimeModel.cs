namespace SaveTime.DataAccess
{
    using SaveTime.DataModel.Business;
    using SaveTime.DataModel.Dictionary;
    using SaveTime.DataModel.Organization;
    using System.Data.Entity;

    public class SaveTimeModel : DbContext
    {
        public SaveTimeModel()
            : base("name=SaveTimeModel") {}
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
    }
}