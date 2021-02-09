using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;

namespace Finance.Management.Service.Reporting.Interfaces
{
    public interface ITracking<FilterCriteria, Ts>
        where FilterCriteria : IEnumerable<ITrendable>
        where Ts : IEnumerable<ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>>
    {
        String Name { get; }
        Date Start { get; }
        Date End { get; }
        Ts Transactions { get; }
        Double Actual { get; }
        Double? Target { get; }
        ECurrency Currency { get; set; }
        Double? Deviance { get; }
    }
}
