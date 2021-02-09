using System;
using Finance.Management.Service.Reporting.Interfaces;

namespace Finance.Management.Service.Operating.Interfaces
{
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
