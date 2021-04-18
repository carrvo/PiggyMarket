using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;

namespace Finance.Management.Service.Budgeting.Interfaces
{
    using IBudget = IBudget<ICategory>;

    /// <summary>
    /// This is where user's are to define the monetary funds
    /// that they allocate, including their income.
    /// </summary>
    public interface IBudget<TCategory>
        where TCategory : ICategory
    {
        /// <summary>
        /// Identifies the <see cref="IBudget" /> and is
        /// what the <see cref="IBudget" /> applies against.
        ///
        /// The <see cref="Period"/>ic monetary value is
        /// allocated to this <see cref="IAccountable" />
        /// as funds.
        /// </summary>
        TCategory Category { get; }

        /// <summary>
        /// The monetary value that is being allocated
        /// for a <see cref="Period" />.
        /// </summary>
        Double Target { get; set; }

        /// <summary>
        /// The <see cref="TimeSpan" /> for which allocated monetary
        /// funds are intended to last and for which new monetary
        /// funds are expected to be acquired and allocated.
        /// </summary>
        TimeSpan Period { get; }
    }
}
