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
                employeeViewModel.AccountEmail = employee.Account.Email;
                employeeViewModel.AccountPhone = employee.Account.Phone;
                employeeViewModel. BranchAddress = employee.Branch.Address;
                employeeViewModel.BranchPhone = employee.Branch.Phone;
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

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(employee).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee);
        //}
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    db.Employees.Remove(employee);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
