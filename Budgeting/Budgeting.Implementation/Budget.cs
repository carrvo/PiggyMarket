using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Budgeting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Budgeting.Implementation
{
    class Budget<C, SC> : IBudget<C, SC>
        where C : ICategory
        where SC : ISubCategory<C>
    {
        public C Category => throw new NotImplementedException();

        public SC? SubCategory => throw new NotImplementedException();

        public double Target { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ECurrency Currency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TimeSpan Period => throw new NotImplementedException();
    }
}
