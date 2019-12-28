using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using SaveTime.DataModel.Organization;
using SaveTime.Web.Admin.Models;
using SaveTime.Web.Admin.Repo;

namespace SaveTime.Web.Admin.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IRepository<Branch> _repository;
        private IList<BranchViewModel> _branches = new List<BranchViewModel>();
        public BranchController()
        {
            _repository = kernel.Get<IRepository<Branch>>();
        }
        public ActionResult Index()
        {
            foreach (var branch in _repository.GetAll())
            {
                BranchViewModel branchViewModel = new BranchViewModel()
                {
                    Address = branch.Address,
                    Email = branch.Email,
                    Id = branch.Id,
                    Phone = branch.Phone,
                    EndWork = branch.EndWork,
                    StartWork = branch.StartWork,
                    StepWork = branch.StepWork
                };
                _branches.Add(branchViewModel);
            }
            return View(_branches);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BranchViewModel branchViewModel, DateTime startWork, DateTime endWork)
        {
            if (ModelState.IsValid)
            {
                Branch branch = new Branch()
                {
                    Address = branchViewModel.Address,
                    Email = branchViewModel.Email,
                    StartWork = startWork,
                    StepWork = branchViewModel.StepWork,
                    EndWork=endWork,
                    Phone=branchViewModel.Phone,
                };
                _repository.Create(branch);
                return RedirectToAction("Index");
            }
            return View(branchViewModel);
        }
    }
}
