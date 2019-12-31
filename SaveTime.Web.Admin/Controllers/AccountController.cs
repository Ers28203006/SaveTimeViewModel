using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
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

        public ActionResult Details(int id)
        {
            var account = _repository.GetEntity(id);
            if (account == null)
                return HttpNotFound();
            AccountEditViewModel accountEditViewModel = new AccountEditViewModel() 
            { 
                Email=account.Email,
                Id=account.Id,
                Password=account.Password,
                Phone=account.Phone
            };
            return View(accountEditViewModel);
        }
        public ActionResult Edit(int id)
        {
            Account account = _repository.GetEntity(id);
            if (account == null)
                return HttpNotFound();
            AccountEditViewModel accountEditViewModel = new AccountEditViewModel()
            {
                Email = account.Email,
                Id = account.Id,
                Password = account.Password,
                Phone = account.Phone
            };
            return View(accountEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountEditViewModel accountEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account()
                {
                    Email = accountEditViewModel.Email,
                    Id = accountEditViewModel.Id,
                    Phone = accountEditViewModel.Phone,
                    Password = accountEditViewModel.Password
                };
                _repository.Update(account);
                return RedirectToAction("Index");
            }
            return View(accountEditViewModel);
        }

        public ActionResult Delete(int id)
        {
            Account account = _repository.GetEntity(id);
            if (account == null)
                return HttpNotFound();
            AccountEditViewModel accountEditViewModel = new AccountEditViewModel()
            {
                Email = account.Email,
                Id = account.Id,
                Password = account.Password,
                Phone = account.Phone
            };
            return View(accountEditViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = _repository.GetEntity(id);
            _repository.Delete(account);
            return RedirectToAction("Index");
        }
    }
}
