using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Reporting.Interfaces
{
    /// <summary>
    /// Budget-specific Tracking.
    /// </summary>
    public interface IBudgetTracking
    {
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
        /// <summary>
        /// If derived from an <see cref="IBudget"/>, the predicted
        /// monetary value of the <see cref="ITransaction"/>s.
        /// Otherwise should be <see cref="Nullable"/>.
        /// </summary>
        Double? Target { get; }
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved

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
