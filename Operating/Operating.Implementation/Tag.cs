using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Operating.Implementation
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;

    /// <summary>
    /// Defines a type to provide additional meta-data
    /// meaning about a <see cref="ITransaction" />.
    ///
    /// These work like <see cref="Boolean" />s
    /// and either the meaning applies or not.
    /// </summary>
    public class Tag : ITag, ITrendable
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        /// <summary>
        /// Identifies the meta-data meaning.
        /// </summary>
        public String Name => throw new NotImplementedException();
    }
}
