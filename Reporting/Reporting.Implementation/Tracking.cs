using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Reporting.Implementation
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;

    /// <summary>
    /// Defines a type that is used for statistics and other
    /// reporting metrics.
    /// </summary>
    /// <typeparam name="TFilterCriteria"></typeparam>
    /// <typeparam name="TTransactions"></typeparam>
    public class Tracking<TFilterCriteria, TTransactions> : ITracking, IBudgetTracking
        where TFilterCriteria : IEnumerable<ITrendable>
        where TTransactions : IEnumerable<ITransaction>
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        /// <summary>
        /// Identifier.
        /// </summary>
        public String Name => throw new NotImplementedException();

        /// <summary>
        /// The start of the period that the metrics correspond to.
        /// </summary>
        public DateTime Start => throw new NotImplementedException();

        /// <summary>
        /// The end of the period that the metrics correspond to.
        /// </summary>
        public DateTime End => throw new NotImplementedException();

        /// <summary>
        /// The <see cref="ITransaction"/> that the metrics are calculated against.
        /// </summary>
        public TTransactions Transactions => throw new NotImplementedException();

        /// <summary>
        /// The total monetary value of the <see cref="ITransaction"/>s.
        /// </summary>
        public Double Actual => throw new NotImplementedException();

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
        /// <summary>
        /// If derived from an <see cref="IBudget"/>, the predicted
        /// monetary value of the <see cref="ITransaction"/>s.
        /// Otherwise should be <see cref="Nullable"/>.
        /// </summary>
        public Double? Target => throw new NotImplementedException();
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved

        /// <summary>
        /// The monetary system the value is being applied through.
        /// </summary>
        public ECurrency Currency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
        /// <summary>
        /// If derived from an <see cref="IBudget"/>, the difference
        /// between <see cref="Actual"/> and <see cref="Target"/>.
        /// Otherwise should be <see cref="Nullable"/>.
        /// </summary>
        public Double? Deviance => throw new NotImplementedException();
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
    }
}
