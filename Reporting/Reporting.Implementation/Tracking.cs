using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Reporting.Implementation
{
    class Tracking<FilterCriteria, Ts> : ITracking<FilterCriteria, Ts>
        where FilterCriteria : IEnumerable<ITrendable>
        where Ts : IEnumerable<ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>>
    {
        public string Name => throw new NotImplementedException();

        public Date Start => throw new NotImplementedException();

        public Date End => throw new NotImplementedException();

        public Ts Transactions => throw new NotImplementedException();

        public double Actual => throw new NotImplementedException();

        public double? Target => throw new NotImplementedException();

        public ECurrency Currency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double? Deviance => throw new NotImplementedException();
    }
}
