using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Interfaces
{
    /// <summary>
    /// Defines a type to express Payment method for a transaction.
    /// 
    /// Useful for metrics.
    /// </summary>
    public interface IPaymentMethod
    {
        /// <summary>
        /// Underlying representation.
        /// </summary>
        EPaymentMethod Method { get; }
    }
}
