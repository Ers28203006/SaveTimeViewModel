using SaveTime.DataAccess;
using SaveTime.DataModel.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveTime.Web.Admin.Repo.Impl
{
    public class BranchRepository : IBranchRepository
    {
        private readonly SaveTimeModel model;

        public BranchRepository()
        {
            this.model = new SaveTimeModel();
        }
        public void Create(Branch branch)
        {
            model.Branches.Add(branch);
            model.SaveChanges();
        }

    }
}