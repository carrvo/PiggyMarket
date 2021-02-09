using System;
using System.Collections.Generic;
using Finance.Management.Service.Banking;
using Finance.Management.Service.Operating;

namespace Finance.Management.Service.Reporting
{
    interface ITracking<FilterCriteria, Ts> where FilterCriteria : IEnumerable<ITrendable>, Ts : IEnumerable<ITransactions>
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
