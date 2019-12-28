using Ninject;
using SaveTime.Web.Admin.IocModules;
using System.Web.Mvc;

namespace SaveTime.Web.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected IKernel kernel;
        public BaseController()
        {
            kernel = new StandardKernel(new RepositoryModule());
        }
    }
}