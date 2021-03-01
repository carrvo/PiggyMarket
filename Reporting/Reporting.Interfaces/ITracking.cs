using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;

namespace Finance.Management.Service.Reporting.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>;
    
    /// <summary>
    /// Defines a type that is used for statistics and other
    /// reporting metrics.
    /// </summary>
    /// <typeparam name="TFilterCriteria"></typeparam>
    /// <typeparam name="TTransactions"></typeparam>
    public interface ITracking<TFilterCriteria, TTransactions>
        where TFilterCriteria : IEnumerable<ITrendable>
        where TTransactions : IEnumerable<ITransaction>
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The start of the period that the metrics correspond to.
        /// </summary>
        DateTime Start { get; }

        /// <summary>
        /// The end of the period that the metrics correspond to.
        /// </summary>
        DateTime End { get; }

        /// <summary>
        /// The <see cref="ITransaction"/> that the metrics are calculated against.
        /// </summary>
        TTransactions Transactions { get; }

        /// <summary>
        /// The total monetary value of the <see cref="ITransaction"/>s.
        /// </summary>
        Double Actual { get; }


#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
        /// <summary>
        /// If derived from an <see cref="IBudget"/>, the predicted
        /// monetary value of the <see cref="ITransaction"/>s.
        /// Otherwise should be <see cref="Nullable"/>.
        /// </summary>
        Double? Target { get; }
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved

        /// <summary>
        /// The monetary system the value is being applied through.
        /// </summary>
        ECurrency Currency { get; set; }


#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
        /// <summary>
        /// If derived from an <see cref="IBudget"/>, the difference
        /// between <see cref="Actual"/> and <see cref="Target"/>.
        /// Otherwise should be <see cref="Nullable"/>.
        /// </summary>
        Double? Deviance { get; }
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
    }
}
