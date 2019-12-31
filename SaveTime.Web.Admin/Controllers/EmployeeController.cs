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
using SaveTime.DataModel.Organization;
using SaveTime.Web.Admin.Models;
using SaveTime.Web.Admin.Repo;

namespace SaveTime.Web.Admin.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Branch> _branchRepository;
        public EmployeeController()
        {
            _accountRepository = kernel.Get<IRepository<Account>>();
            _branchRepository = kernel.Get<IRepository<Branch>>();
            _employeeRepository = kernel.Get<IRepository<Employee>>();
        }
        public ActionResult Index()
        {
            var employees = _employeeRepository.GetAll().ToList();
            IList <EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach (var employee in employees)
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                if (employee.AccountId!=null)
                {
                    employeeViewModel.AccountEmail = employee.Account.Email;
                    employeeViewModel.AccountPhone = employee.Account.Phone;
                }
                else
                {
                    employeeViewModel.AccountEmail =string.Empty;
                    employeeViewModel.AccountPhone = string.Empty;
                }
                if (employee.BranchId!=null)
                {
                    employeeViewModel.BranchAddress = employee.Branch.Address;
                    employeeViewModel.BranchPhone = employee.Branch.Phone;
                }
                else
                {
                    employeeViewModel.BranchAddress = string.Empty;
                    employeeViewModel.BranchPhone = string.Empty;
                }
                employeeViewModel. Id = employee.Id;
                employeeViewModel.Name = employee.Name;
                employeeViewModels.Add(employeeViewModel);
            }
            return View(employeeViewModels);
        }
        public ActionResult Create()
        {
            IList<string> _branchAddresses = new List<string>();
            IList<string> _branchPhones = new List<string>();
            IList<string> _accountEmails = new List<string>();
            IList<string> _accountPhones = new List<string>();
            var branches = _branchRepository.GetAll().ToList();
            var accounts = _accountRepository.GetAll().ToList();
            foreach (var branch in branches) 
            {
                _branchAddresses.Add(branch.Address);
                _branchPhones.Add(branch.Phone);
            }
            foreach (var account in accounts)
            {
                _accountEmails.Add(account.Email);
                _accountPhones.Add(account.Phone);
            }
            ViewBag.BranchAddresses = _branchAddresses;
            ViewBag.BranchPhones = _branchPhones;
            ViewBag.AccountEmails = _accountEmails;
            ViewBag.AccountPhones = _accountEmails;
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel  employeeViewModel)
        {
            var branches = _branchRepository.GetAll().ToList();
            var accounts = _accountRepository.GetAll().ToList();

            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                foreach (var a in accounts)
                    if (a.Email == employeeViewModel.AccountEmail || a.Phone==employeeViewModel.AccountPhone)
                    {
                        employee.AccountId = a.Id;
                        break;
                    }
                foreach (var b in branches)
                    if (b.Phone == employeeViewModel.BranchPhone || b.Address==employeeViewModel.BranchAddress)
                    {
                        employee.BranchId = b.Id;
                        break;
                    }
                employee.Name = employeeViewModel.Name;

                _employeeRepository.Create(employee);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            Employee employee = _employeeRepository.GetEntity(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEntity(id);
            if (employee == null)
                return HttpNotFound();

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel();
            foreach (var a in _accountRepository.GetAll())
            {
                employeeEditViewModel.AccountEmails.Add(a.Email);
                employeeEditViewModel.AccountPhones.Add(a.Phone);
            }
            foreach (var b in _branchRepository.GetAll())
            {
                employeeEditViewModel.BranchAddresses.Add(b.Address);
                employeeEditViewModel.BranchPhones.Add(b.Phone);
            }
            employeeEditViewModel.Name=employee.Name;
            employeeEditViewModel.Id = employee.Id;
            return View(employeeEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeEditViewModel employeeEditViewModel, 
            string accountEmail, string accountPhone, string branchAddress, 
            string branchPhone)
        {
            if (ModelState.IsValid)
            {
                var empoyees = _employeeRepository.GetAll();
                foreach (var e in empoyees)
                    if (e.Id == employeeEditViewModel.Id)
                    {
                        foreach (var account in _accountRepository.GetAll())
                            if (account.Email == accountEmail ||
                                account.Phone == accountPhone)
                            {
                                e.AccountId = account.Id;
                                break;
                            }
                        foreach (var branch in _branchRepository.GetAll())
                            if (branch.Phone==branchPhone || 
                                branch.Address==branchAddress)
                            {
                                e.BranchId = branch.Id;
                                break;
                            }
                        e.Name = employeeEditViewModel.Name;
                        _employeeRepository.Update(e);
                        break;
                    }
                return RedirectToAction("Index");
            }
            return View(employeeEditViewModel);
        }
        public ActionResult Delete(int id)
        {
            Employee employee = _employeeRepository.GetEntity(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _employeeRepository.GetEntity(id);
            _employeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
