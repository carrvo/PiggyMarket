using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting;
using Finance.Management.Service.Reporting;
using Finance.Management.Service.Operating;

namespace Finance.Management.Service.Banking
{
    /// <summary>
    /// Defines a type to represent a physical bank account.
    /// </summary>
    public interface IBankAccount : ITrendable, IAccountable, IPayee
    {
        /// <summary>
        /// Identifies the physical bank account (<see cref="IBankAccount" />).
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The name of the financial institution that the bank account belongs to.
        /// </summary>
        String BankName { get; }

        /// <summary>
        /// The number used to identify the financial institution.
        /// </summary>
        // Int32 TransitNumber { get; }

        /// <summary>
        /// The number used to identify the account at the bank.
        /// </summary>
        // Int64 AccountNumber { get; }
    }
}
