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
    public class AccountController : BaseController
    {
        private readonly IRepository<Account> _repository;
        public AccountController()
        {
            _repository = kernel.Get<IRepository<Account>>();
        }
        public ActionResult Index()
        {
            IList<AccountViewModel> accounts = new List<AccountViewModel>();
            foreach (var account in _repository.GetAll())
            {
                AccountViewModel accountViewModel = new AccountViewModel()
                {
                    Email = account.Email,
                    Id = account.Id,
                    Password = account.Password,
                    Phone = account.Phone
                };
                accounts.Add(accountViewModel);
            }
            return View(accounts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel accountViewModel)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account()
                {
                    Id = accountViewModel.Id,
                    Email = accountViewModel.Email,
                    Password = accountViewModel.Password,
                    Phone = accountViewModel.Phone
                };
                _repository.Create(account);
                return RedirectToAction("Index");
            }

            return View(accountViewModel);
        }
    }
}
