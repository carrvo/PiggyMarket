using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Operating.Interfaces;

namespace Finance.Management.Service.Reporting.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;

    /// <summary>
    /// Defines a type that is used for statistics and other
    /// reporting metrics.
    /// </summary>
    public interface ITracking
    {
        /// <summary>
        /// The start of the period that the metrics correspond to.
        /// </summary>
        DateTime Start { get; }

        /// <summary>
        /// The end of the period that the metrics correspond to.
        /// </summary>
        DateTime End { get; }

        /// <summary>
        /// The total monetary value of the <see cref="ITransaction"/>s.
        /// </summary>
        Double Actual { get; }
    }
}
