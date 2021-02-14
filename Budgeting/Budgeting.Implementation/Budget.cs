using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Budgeting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Budgeting.Implementation
{
    class Budget<TCategory, TSubCategory> : IBudget<TCategory, TSubCategory>
        where TCategory : ICategory
        where TSubCategory : ISubCategory<TCategory>
    {
        protected IAccessToken AccessToken { get; }

        public TCategory Category => throw new NotImplementedException();

        public TSubCategory? SubCategory => throw new NotImplementedException();

        public double Target { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ECurrency Currency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TimeSpan Period => throw new NotImplementedException();
    }
}
