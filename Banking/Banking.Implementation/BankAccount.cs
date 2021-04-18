using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Implementation
{
    /// <summary>
    /// Defines a type to represent a physical bank account.
    /// </summary>
    public class BankAccount : IBankAccount, ITrendable, IAccountable, IPayee
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        /// <summary>
        /// Identifies the physical bank account (<see cref="IBankAccount" />).
        /// </summary>
        public String Name => throw new NotImplementedException();

        /// <summary>
        /// The name of the financial institution that the bank account belongs to.
        /// </summary>
        public String BankName => throw new NotImplementedException();

        /// <summary>
        /// The number used to identify the financial institution.
        /// </summary>
        private Int32 TransitNumber => throw new NotImplementedException();

        /// <summary>
        /// The number used to identify the account at the bank.
        /// </summary>
        private Int64 AccountNumber => throw new NotImplementedException();

        /// <summary>
        /// The monetary value being stored.
        /// </summary>
        public Double CurrentFunds => throw new NotImplementedException();

        /// <summary>
        /// The monetary system the value is being stored in.
        /// </summary>
        public ECurrency Currency => throw new NotImplementedException();
    }
}
