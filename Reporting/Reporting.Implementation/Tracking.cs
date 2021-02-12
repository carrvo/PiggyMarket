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
    using ITransaction = ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>;

    class Tracking<TFilterCriteria, TTransactions> : ITracking<TFilterCriteria, TTransactions>
        where TFilterCriteria : IEnumerable<ITrendable>
        where TTransactions : IEnumerable<ITransaction>
    {
        public string Name => throw new NotImplementedException();

        public DateTime Start => throw new NotImplementedException();

        public DateTime End => throw new NotImplementedException();

        public TTransactions Transactions => throw new NotImplementedException();

        public double Actual => throw new NotImplementedException();

        public double? Target => throw new NotImplementedException();

        public ECurrency Currency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double? Deviance => throw new NotImplementedException();
    }
}
