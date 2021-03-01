using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Authenticating.Interfaces
{
    /// <summary>
    /// A logical grouping of the system's functionality to provide access control.
    /// </summary>
    public interface IAccountContext
    {
        /// <summary>
        /// Friendly Name for the <see cref="IAccountContext"/>.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// Identifier for the <see cref="IAccountContext"/>.
        /// </summary>
        Guid Guid { get; set; }
    }
}
