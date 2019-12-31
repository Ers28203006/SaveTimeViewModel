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
        private readonly IRepository<Employee> _employeeRepository;
        private IList<BranchViewModel> _branches = new List<BranchViewModel>();

        public BranchController()
        {
            _repository = kernel.Get<IRepository<Branch>>();
            _employeeRepository = kernel.Get<IRepository<Employee>>();
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

        public ActionResult Details(int id)
        {
            var branch = _repository.GetEntity(id);
            if (branch == null)
                return HttpNotFound();
            return View(branch);
        }

        public ActionResult Edit(int id)
        {
            var branch = _repository.GetEntity(id);
            if (branch == null)
                return HttpNotFound();
            BranchEditViewModel branchEditViewModel = new BranchEditViewModel()
            {
                Address = branch.Address,
                Email = branch.Email,
                EndWork = branch.EndWork,
                Id = branch.Id,
                Phone = branch.Phone,
                StartWork = branch.StartWork,
                StepWork = branch.StepWork
            };
            return View(branchEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BranchEditViewModel branchEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Branch branch = new Branch()
                {
                    Address = branchEditViewModel.Address,
                    Email = branchEditViewModel.Email,
                    EndWork = branchEditViewModel.EndWork,
                    StepWork = branchEditViewModel.StepWork,
                    Phone = branchEditViewModel.Phone,
                    StartWork = branchEditViewModel.StartWork,
                    Id = branchEditViewModel.Id
                };
                _repository.Update(branch);
                return RedirectToAction("Index");
            }
            return View(branchEditViewModel);
        }

        public ActionResult Delete(int id)
        {
            var branch = _repository.GetEntity(id);
            if (branch == null)
                return HttpNotFound();
            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var branch = _repository.GetEntity(id);
            _repository.Delete(branch);
            return RedirectToAction("Index");
        }
    }
}
