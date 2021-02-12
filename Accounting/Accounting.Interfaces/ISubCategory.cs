using System;
using System.Collections.Generic;

namespace Finance.Management.Service.Accounting.Interfaces
{
    /// <summary>
    /// Defines a type to extend the concept of <see cref="ICategory" />.
    ///
    /// Whereby a <see cref="ICategory" /> must have a limited
    /// number of instances, a <see cref="ISubCategory" /> extends
    /// past that limitation. This allows for a more fine-grained
    /// organization to provide a higher fidelity view, while
    /// working in conjuction with <see cref="ICategory" /> whose
    /// purpose is to aggregate and summarize.
    ///
    /// This leads to applying <see cref="ICategory" /> indirectly
    /// to <see cref="ITransaction" />s. They will have a
    /// <see cref="ISubCategory" /> applied to them; that then
    /// corresponds to a <see cref="ICategory" />, for
    /// aggregation purposes.
    ///
    /// The limitation is that a <see cref="ISubCategory" /> will
    /// *not* be able to directly determine how much of the
    /// <see cref="IBudget" /> is left at any given time. This
    /// can still, however, be indirectly accomplished through
    /// the use of a <see cref="ITracking" />.
    /// </summary>
    public interface ISubCategory<TCategory>
        where TCategory : ICategory
    {
        /// <summary>
        /// Identifies the <see cref="ISubCategpry" /> and its meaning.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Information about the <see cref="ISubCategory">.
        /// </summary>
        String Description { get; }

        /// <summary>
        /// The <see cref="ICategory" /> that it corresponds to.
        /// </summary>
        TCategory Category { get; }
    }
}
