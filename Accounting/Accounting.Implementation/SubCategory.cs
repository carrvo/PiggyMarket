using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Accounting.Implementation
{
    using ISubCategory = ISubCategory<ICategory>;

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
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
    public class SubCategory : ISubCategory<Category>, ITrendable
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        /// <summary>
        /// Identifies the <see cref="ISubCategory" /> and its meaning.
        /// </summary>
        public String Name => throw new NotImplementedException();

        /// <summary>
        /// Information about the <see cref="ISubCategory"/>.
        /// </summary>
        public String Description => throw new NotImplementedException();

        /// <summary>
        /// The <see cref="ICategory" /> that it corresponds to.
        /// </summary>
        public Category Category => throw new NotImplementedException();
    }
}
