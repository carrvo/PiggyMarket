using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;

namespace Finance.Management.Service.Budgeting.Interfaces
{
    /// <summary>
    /// Defines a type that is used for monetary
    /// planning, tracking, and management.
    ///
    /// This is the linchpin of the system. This is
    /// where user's are to define the monetary funds
    /// that they allocate, including their income.
    /// This is also where new <see cref="ITracking">
    /// are defined on a <see cref="IBudget.Period">ic
    /// basis. This type defines the planning stage to
    /// inform the <see cref="ICategory"> management and
    /// <see cref="ITracking">; all of which form the
    /// complete <see cref="IBudget">ing system.
    ///
    /// The complete <see cref="IBudget">ing system is
    /// intended to function as follows:
    /// 1) A user allocates monetary funds to a
    /// <see cref="ICategory"> using a <see cref="IBudget">.
    /// 2) The user then spends the allocated monetary funds
    /// from their <see cref="ICategory">, treating it as a
    /// virtual <see cref="IAccountable">, and also selecting
    /// a physical <see cref="IAccountable"> for which to
    /// spend from--together forming the <see cref="ITransaction">.
    /// 3) Finally the user <see cref="ITracking">Tracks</see> the
    /// <see cref="ITransaction" />s at the end of the
    /// <see cref="IBudget.Period" /> to see whether they were
    /// over or under <see cref="IBudget" />, and either update
    /// as necessary or change their spending behaviour accordingly.
    ///
    /// <see cref="IBudget" />ing is not meant to prescribe any
    /// lifestyle, but to determine if such a lifestyle is
    /// affordable for the user.
    /// </summary>
    public interface IBudget<TCategory, TSubCategory>
        where TCategory : ICategory
        where TSubCategory : ISubCategory<TCategory>
    {
        /// <summary>
        /// Identifies the <see cref="IBudget" /> and is
        /// what the <see cref="IBudget" /> applies against.
        ///
        /// The <see cref="Period">ic monetary value is
        /// allocated to this <see cref="IAccountable" />
        /// as funds.
        /// </summary>
        TCategory Category { get; }

        /// <summary>
        /// Offered for high-fidelity <see cref="IBudget" />ing.
        ///
        /// Ultimately this is used to contribute to the
        /// <see cref="Category" /> and *must* be mutally exclusive
        /// as to which derives the <see cref="Category" />:
        /// a <see cref="ICategory" /> directly or
        /// a <see cref="ISubCategory" /> indirectly.
        /// </summary>
        TSubCategory? SubCategory { get; }

        /// <summary>
        /// The monetary value that is being allocated
        /// for a <see cref="Period" />.
        /// </summary>
        Double Target { get; set; }

        /// <summary>
        /// The monetary system the value is being applied through.
        /// </summary>
        ECurrency Currency { get; set; }

        /// <summary>
        /// The <see cref="TimeSpan" /> for which allocated monetary
        /// funds are intended to last and for which new monetary
        /// funds are expected to be acquired and allocated.
        /// </summary>
        TimeSpan Period { get; }
    }
}
