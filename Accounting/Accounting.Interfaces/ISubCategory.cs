using System;
using System.Collections.Generic;

namespace Finance.Management.Service.Accounting.Interfaces
{
    /// <summary>
    /// The association with a <see cref="ICategory"/>.
    /// </summary>
    public interface ISubCategory<TCategory>
        where TCategory : ICategory
    {
        /// <summary>
        /// The <see cref="ICategory" /> that it corresponds to.
        /// </summary>
        TCategory Category { get; }
    }
}
