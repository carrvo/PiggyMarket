using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Implementation
{
    /// <summary>
    /// Defines a type to express Payment method for a transaction.
    /// 
    /// Useful for metrics.
    /// </summary>
    public class PaymentMethod : ITrendable
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        /// <summary>
        /// User friendly representation of the <see cref="Method"/>.
        /// </summary>
        public String Name => Method.ToString();

        /// <summary>
        /// Underlying representation.
        /// </summary>
        public EPaymentMethod Method => throw new NotImplementedException();
    }
}
