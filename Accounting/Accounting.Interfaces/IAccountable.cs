using System;
using System.Collections.Generic;
using Finance.Management.Service.Reporting.Interfaces;

namespace Finance.Management.Service.Accounting.Interfaces
{
    /// <summary>
    /// Defines a type to treatable like an account
    /// whereby it stores monetary value.
    /// </summary>
    public interface IAccountable
    {
        /// <summary>
        /// The monetary value being stored.
        /// </summary>
        Double CurrentFunds { get; }

        /// <summary>
        /// The monetary system the value is being stored in.
        /// </summary>
        ECurrency Currency { get; }
    }
}
