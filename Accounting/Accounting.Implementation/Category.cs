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
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
    /// <summary>
    /// Defines a type to categorize <see cref="ITransaction" />.
    /// This is used for budgeting purposes to organize
    /// and describe <see cref="ITransaction" />s.
    ///
    /// Categories are meant to only apply once to a
    /// <see cref="ITransaction" /> and to give them
    /// additional meaning behind their creation.
    /// Categories also serve to aggregate
    /// <see cref="ITransaction" />s that hold the same meaning.
    ///
    /// It is important that these are limited in the
    /// number of instances so that they can properly
    /// aggregate and summarize <see cref="ITransaction" />s,
    /// providing a course-grained view.
    /// </summary>
    public class Category : ICategory, ITrendable, IAccountable
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        /// <summary>
        /// Identifies the <see cref="ICategory" /> and its meaning.
        /// </summary>
        public String Name => throw new NotImplementedException();

        /// <summary>
        /// Information about the <see cref="ICategory"/>.
        /// </summary>
        public String Description => throw new NotImplementedException();

        /// <summary>
        /// Tracks the monetary value allocated to this
        /// <see cref="ICategory" />.
        /// </summary>
        public Double CurrentFunds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// The monetary system the value is being stored in.
        /// </summary>
        public ECurrency Currency => throw new NotImplementedException();
    }
}
