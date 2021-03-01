using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;

namespace Finance.Management.Service.Operating.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>;

    /// <summary>
    /// Defines a type to represent a party that
    /// monetary funds are transfered to (through
    /// <see cref="ITransaction" />s).
    /// </summary>
    public interface IPayee
    {
        /// <summary>
        /// Identifies the party the monetary
        /// funds transfer to.
        /// </summary>
        String Name { get; }
    }
}
