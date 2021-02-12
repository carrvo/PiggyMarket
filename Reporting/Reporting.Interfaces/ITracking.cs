using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;

namespace Finance.Management.Service.Reporting.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>;
    
    public interface ITracking<TFilterCriteria, TTransactions>
        where TFilterCriteria : IEnumerable<ITrendable>
        where TTransactions : IEnumerable<ITransaction>
    {
        String Name { get; }
        Date Start { get; }
        Date End { get; }
        TTransactions Transactions { get; }
        Double Actual { get; }
        Double? Target { get; }
        ECurrency Currency { get; set; }
        Double? Deviance { get; }
    }
}
