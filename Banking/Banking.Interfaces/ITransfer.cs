using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;

    /// <summary>
    /// Transfer to and from another party by some means.
    /// </summary>
    public interface ITransfer<TBankAccount, TPaymentMethod>
        where TBankAccount : IBankAccount
        where TPaymentMethod : IPaymentMethod
    {
        /// <summary>
        /// The physical <see cref="IBankAccount" /> that
        /// the <see cref="ITransaction" /> belongs to.
        /// 
        /// This will be <see cref="Nullable"/> for
        /// <see cref="EPaymentMethod.Cash"/> purchases.
        /// </summary>
        TBankAccount BankAccount { get; }

        /// <summary>
        /// <see cref="IPaymentMethod"/> used.
        /// </summary>
        TPaymentMethod PaymentMethod { get; }

        /// <summary>
        /// The other side of the monetary funds movement.
        ///
        /// If the <see cref="ITransaction.Price" /> is positive (+), this
        /// is where the funds are coming *from*. If the
        /// <see cref="ITransaction.Price" /> is negative (-), this is where
        /// the funcs are going *to*.
        /// </summary>
        IPayee Payee { get; }

        /// <summary>
        /// Determines whether the <see cref="ITransaction"/>
        /// has been confirmed by two sources of authority
        /// (purchase and <see cref="IBankAccount"/>).
        /// </summary>
        Boolean Verified { get; }
    }
}
