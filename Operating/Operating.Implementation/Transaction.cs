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

namespace Finance.Management.Service.Operating.Implementation
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;
    using ISubCategory = ISubCategory<ICategory>;

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
    /// <summary>
    /// Defines a type that represents monetary funds moving or flowing.
    ///
    /// This is the heart of the active part of the system
    /// because it is what causes changes in monetary value
    /// within any <see cref="IAccountable" />. All
    /// <see cref="IAccountable" />s will see a dedicated
    /// <see cref="ITransaction" /> for any monetary value
    /// entering or exiting, represented by positive (+)
    /// or negative (-) <see cref="Price" />; meaning that
    /// if there is a transfer between <see cref="IAccountable" />s
    /// of the same type, then there will be two (2)
    /// <see cref="ITransaction" />s representing the transfer,
    /// one for each <see cref="IAccountable" /> from their
    /// perspective. The net sum, in such a case, will always
    /// be zero (0) because one <see cref="IAccountable" /> will
    /// see the monetary value exiting, thus be negative (+),
    /// and the other <see cref="IAccountable" /> will see the
    /// monetary value entering, thus be positive (+) with
    /// the same value.
    ///
    /// With the ability to itemize <see cref="ITransaction" />s,
    /// common recurring items can be treated in the same fashion
    /// as a <see cref="ISubCategory" /> with the roll-up to
    /// <see cref="ICategory" /> (for the purposes of <see cref="IBudget" />ing).
    /// </summary>
    public class Transaction<TSubCategory, TBankAccount, TPaymentMethod, TTags>
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
        : ITransaction<TSubCategory, TTags>, ITransfer<TBankAccount, TPaymentMethod>, IDated, IMetaData<TTags>, ISubCategory<ICategory>, ITrendable
        where TSubCategory : ISubCategory<ICategory>
        where TBankAccount : IBankAccount
        where TPaymentMethod : IPaymentMethod
        where TTags : IEnumerable<ITag>
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        /// <summary>
        /// Identifies the <see cref="ITransaction" />
        /// so that it can be recognized.
        /// </summary>
        public String Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public String Description => Name;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// The <see cref="ISubCategory" /> that gives
        /// the <see cref="ITransaction" /> meaning.
        ///
        /// When the <see cref="ISubCategory" /> is changed,
        /// the corresponding <see cref="IAccountable.CurrentFunds" />
        /// need to be adjusted.
        /// </summary>
        public TSubCategory SubCategory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ICategory Category => SubCategory.Category;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// The physical <see cref="IBankAccount" /> that
        /// the <see cref="ITransaction" /> belongs to.
        /// 
        /// This will be <see cref="Nullable"/> for
        /// <see cref="EPaymentMethod.Cash"/> purchases.
        /// </summary>
        public TBankAccount BankAccount => throw new NotImplementedException();

        /// <summary>
        /// <see cref="IPaymentMethod"/> used.
        /// </summary>
        public TPaymentMethod PaymentMethod => throw new NotImplementedException();

        /// <summary>
        /// The other side of the monetary funds movement.
        ///
        /// If the <see cref="Price" /> is positive (+), this
        /// is where the funds are coming *from*. If the
        /// <see cref="Price" /> is negative (-), this is where
        /// the funcs are going *to*.
        /// </summary>
        public IPayee Payee => throw new NotImplementedException();

        /// <summary>
        /// The monetary value of the <see cref="ITransaction" />.
        /// </summary>
        public Double Price => throw new NotImplementedException();

        /// <summary>
        /// The monetary system the <see cref="ITransaction" />
        /// is taking part in.
        /// </summary>
        public ECurrency Currency => throw new NotImplementedException();

        /// <summary>
        /// Date that the <see cref="ITransaction" />
        /// took part on.
        /// </summary>
        public DateTime Posted => throw new NotImplementedException();

        /// <summary>
        /// Date that the <see cref="IBankAccount" />
        /// processed the <see cref="ITransaction" />.
        ///
        /// This is different from the <see cref="Posted" />
        /// date because a <see cref="IBankAccount.BankName" />
        /// may have a delay in confirming the
        /// <see cref="ITransaction" />. This delay may
        /// be the result of a <see cref="ITransaction" />
        /// occuring after business hours or other internal
        /// reasons, like for security.
        /// </summary>
        public DateTime Processed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Any additional unstructured information to be
        /// added to the <see cref="ITransaction" /> for the
        /// user's benefit.
        /// </summary>
        public String Comment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Any additional structured (<see cref="Boolean" />)
        /// information or meta-data to be added to the
        /// <see cref="ITransaction" /> for the user's benefit.
        /// </summary>
        public TTags Tags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// An identifier for the physical receipt that a
        /// <see cref="ITransaction" /> corresponds to.
        ///
        /// Note that a receipt is a legally binding
        /// contract and is used as evidence that a
        /// <see cref="ITransaction" /> has occured.
        /// Receipts are, as a bonus to constituting
        /// evidence, often used for returns or
        /// <see cref="ITransaction" /> reversals.
        /// Either way, it will show up as a new
        /// <see cref="ITransaction" /> that has a
        /// <see cref="Price" /> that counteracts this
        /// <see cref="ITransaction" />, or at least
        /// the portion that is being refunded/reversed.
        /// </summary>
        public String ReceiptID => throw new NotImplementedException();

        /// <summary>
        /// Determines whether the <see cref="ITransaction"/>
        /// has been confirmed by two sources of authority
        /// (purchase and <see cref="IBankAccount"/>).
        /// </summary>
        public Boolean Verified => throw new NotImplementedException();

        /// <summary>
        /// Splits the <see cref="ITransaction" /> into two whose
        /// <see cref="Price" />s sum up to the original <see cref="Price" />.
        ///
        /// Allows for the itemization of transactions.
        /// </summary>
        /// <param name="originalName">New name for the original <see cref="ITransaction"/>.</param>
        /// <param name="newName">New name for the new <see cref="ITransaction"/>.</param>
        /// <param name="newPrice"><see cref="Price"/> for the new <see cref="ITransaction"/>,
        /// whose value is subtracted from the original <see cref="ITransaction"/>.</param>
        public void Split(String originalName, String newName, Double newPrice) => throw new NotImplementedException();
    }
}
