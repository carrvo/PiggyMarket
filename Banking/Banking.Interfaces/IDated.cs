using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;

    /// <summary>
    /// Important timestamps for <see cref="ITransaction"/>s.
    /// </summary>
    public interface IDated
    {
        /// <summary>
        /// Date that the <see cref="ITransaction" />
        /// took part on.
        /// </summary>
        DateTime Posted { get; }

        /// <summary>
        /// Date that the <see cref="IBankAccount" />
        /// processed the <see cref="ITransaction" />.
        ///
        /// This is different from the <see cref="Posted" />
        /// date because a <see cref="IBankAccount.BankName" />
        /// may have a delay in confirming the
        /// <see cref="ITransaction" />. This delay may
        /// be the result of a <see cref="ITransaction" />
        /// occuring after business hours or other internal
        /// reasons, like for security.
        /// </summary>
        DateTime Processed { get; set; }
    }
}
