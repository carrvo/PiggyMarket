using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using System;
using System.Collections.Generic;

namespace Finance.Management.Service.Reporting.Interfaces
{
    using ITracking = ITracking<IEnumerable<ITrendable>, IEnumerable<ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>>>;

    /// <summary>
    /// Defines a type to be used for <see cref="ITracking" /> purposes.
    /// This includes: grouping, filtering, and sorting data.
    /// </summary>
    public interface ITrendable
    {
        /// <summary>
        /// Identifies the criteria used for <see cref="ITracking" />.
        /// </summary>
        String Name { get; }
    }
}
