using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;

namespace Finance.Management.Service.Banking.Interfaces
{
    /// <summary>
    /// Defines a type to represent a physical bank account.
    /// </summary>
    public interface IBankAccount
    {
        /// <summary>
        /// Identifies the physical bank account (<see cref="IBankAccount" />).
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The name of the financial institution that the bank account belongs to.
        /// </summary>
        String BankName { get; }
    }
}
