using Ninject.Modules;
using SaveTime.Web.Admin.Repo;
using SaveTime.Web.Admin.Repo.Impl;

namespace SaveTime.Web.Admin.IocModules
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
        }
    }
}