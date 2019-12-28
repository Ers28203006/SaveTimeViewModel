using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SaveTime.DataAccess;
using SaveTime.DataModel.Dictionary;
using SaveTime.Web.Admin.Models;
using SaveTime.Web.Admin.Repo;

namespace SaveTime.Web.Admin.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IRepository<Service> _repository;
        public ServiceController()
        {
            _repository = kernel.Get<IRepository<Service>>();
        }
        public ActionResult Index()
        {
            IList<ServiceViewModel> serviceViewModels = new List<ServiceViewModel>();
            foreach (var service in _repository.GetAll())
            {
                ServiceViewModel serviceViewModel = new ServiceViewModel()
                {
                    Id = service.Id,
                    Title = service.Title
                };
                serviceViewModels.Add(serviceViewModel);
            }
            return View(serviceViewModels);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceViewModel serviceViewModel)
        {
            if (ModelState.IsValid)
            {
                Service service = new Service()
                {
                    Title = serviceViewModel.Title
                };
                _repository.Create(service);
                return RedirectToAction("Index");
            }
            return View(serviceViewModel);
        }
    }
}
