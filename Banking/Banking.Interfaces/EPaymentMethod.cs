using System;

namespace Finance.Management.Service.Banking.Interfaces
{
    /// <summary>
    /// Payment method for a transaction.
    /// 
    /// Useful for metrics.
    /// </summary>
    public enum EPaymentMethod
    {
        /// <summary>
        /// Use of a physical monetary system.
        /// </summary>
        Cash,

        /// <summary>
        /// An order from one party (the payer) to another (the bank) to release funds to a third party (the payee).
        /// Citation: https://ell.stackexchange.com/a/102434
        /// </summary>
        Cheque,

        /// <summary>
        /// An agreement with a third party to pay on your behalf and you will pay the third party within a given time period.
        /// More: https://money.stackexchange.com/a/6075
        /// </summary>
        Credit,

        /// <summary>
        /// Monetary funds that a financial institution holds on your behalf.
        /// </summary>
        Debit
    }
}
