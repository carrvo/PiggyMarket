using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;

namespace Finance.Management.Service.Operating.Interfaces
{
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
    /// <see cref="ICategory" /> (for the purposes of <see cref="IBudgeting" />).
    /// </summary>
    public interface ITransaction<TSubCategory, TBanckAccount, TPaymentMethod, TTags>
        where TSubCategory : ISubCategory<ICategory>
        where TBanckAccount : IBankAccount
        where TPaymentMethod : IPaymentMethod
        where TTags : IEnumerable<ITag>
    {
        /// <summary>
        /// Identifies the <see cref="ITransaction" />
        /// so that it can be recognized.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// The <see cref="ISubCategpry" /> that gives
        /// the <see cref="ITransaction" /> meaning.
        ///
        /// When the <see cref="ISubCategory" /> is changed,
        /// the corresponding <see cref="ICategory.CurrentFunds" />
        /// need to be adjusted.
        /// </summary>
        TSubCategory SubCategory { get; set; }

        /// <summary>
        /// The physical <see cref="IBankAccount" /> that
        /// the <see cref="ITransaction" /> belongs to.
        /// </summary>
        TBanckAccount? BankAccount { get; }

        /// <summary>
        /// </summary>
        TPaymentMethod PaymentMethod { get; }

        /// <summary>
        /// The other side of the monetary funds movement.
        ///
        /// If the <see cref="Price" /> is positive (+), this
        /// is where the funds are coming *from*. If the
        /// <see cref="Price" /> is negative (-), this is where
        /// the funcs are going *to*.
        /// </summary>
        IPayee Payee { get; }

        /// <summary>
        /// The monetary value of the <see cref="ITransaction" />.
        /// </summary>
        Double Price { get; }

        /// <summary>
        /// Splits the <see cref="ITransaction" /> into two whose
        /// <see cref="Price" />s sum up to the original <see cref="Price" />.
        ///
        /// Allows for the itemization of transactions.
        /// </summary>
        /// <param name="originalName">New name for the original <see cref="ITransaction">.<param/>
        /// <param name="newName">New name for the new <see cref="ITransaction">.<param/>
        /// <param name="newPrice"><see cref="Price"> for the new <see cref="ITransaction">,
        /// whose value is subtracted from the original <see cref="ITransaction">.<param/>
        void Split(String originalName, String newName, Double newPrice);

        /// <summary>
        /// The monetary system the <see cref="ITransaction" />
        /// is taking part in.
        /// </summary>
        ECurrency Currency { get; }

        /// <summary>
        /// Date that the <see cref="ITransaction" />
        /// took part on.
        /// </summary>
        DateTime Posted { get; }

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
        DateTime Processed { get; set; }

        /// <summary>
        /// Any additional unstructured information to be
        /// added to the <see cref="ITransaction" /> for the
        /// user's benefit.
        /// </summary>
        String Comment { get; set; }

        /// <summary>
        /// Any additional structured (<see cref="Boolean" />)
        /// information or meta-data to be added to the
        /// <see cref="ITransaction" /> for the user's benefit.
        /// </summary>
        TTags Tags { get; set; }

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
        /// <see cref "ITransaction" /> that has a
        /// <see cref="Price" /> that counteracts this
        /// <see cref="ITransaction" />, or at least
        /// the portion that is being refunded/reversed.
        /// </summary>
        String ReceiptID { get; }

        /// <summary>
        /// Determines whether the <see cref="ITransaction">
        /// has been confirmed by two sources of authority
        /// (purchase and <see cref="IBankAccount">).
        /// </summary>
        Boolean Verified { get; }
    }
}
