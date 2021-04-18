using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Finance.Management.Service.Budgeting.Interfaces;
using Finance.Management.Service.Operating.Implementation;

namespace Finance.Management.Service.Operating.Cmdlets
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;
    using ISubCategory = ISubCategory<ICategory>;
    using IBudget = IBudget<ICategory>;

    /// <summary>
    /// <para type="synopsis">Moves monetary funds.</para>
    /// <para type="description">
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
    /// </para>
    /// <para type="description">
    /// With the ability to itemize <see cref="ITransaction" />s,
    /// common recurring items can be treated in the same fashion
    /// as a <see cref="ISubCategory" /> with the roll-up to
    /// <see cref="ICategory" /> (for the purposes of <see cref="IBudget"/>ing).
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "Transaction", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    [OutputType(typeof(Transaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>))]
    public sealed class NewTransactionCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">
        /// Identifies the <see cref="ITransaction" />
        /// so that it can be recognized.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public String Name { get; set; }

        /// <summary>
        /// <para type="description">The other side of the monetary funds movement.</para>
        /// <para type="description">
        /// If the <see cref="Price" /> is positive (+), this
        /// is where the funds are coming *from*. If the
        /// <see cref="Price" /> is negative (-), this is where
        /// the funcs are going *to*.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public IPayee Payee { get; set; }

        /// <summary>
        /// <para type="description">No meaning is associated with the <see cref="ITransaction" />.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        public SwitchParameter UnCategorized { get; set; }

        /// <summary>
        /// <para type="description">
        /// The <see cref="ISubCategory" /> that gives
        /// the <see cref="ITransaction" /> meaning.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        /// <summary>
        /// <para type="description"><see cref="EPaymentMethod.Cash"/> purchase.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        public SwitchParameter Cash { get; set; }

        /// <summary>
        /// <para type="description"><see cref="EPaymentMethod.Cheque"/> purchase.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        public SwitchParameter Cheque { get; set; }

        /// <summary>
        /// <para type="description"><see cref="EPaymentMethod.Credit"/> purchase.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        public SwitchParameter Credit { get; set; }

        /// <summary>
        /// <para type="description"><see cref="EPaymentMethod.Debit"/> purchase.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public SwitchParameter Debit { get; set; }

        /// <summary>
        /// <para type="description">The monetary value of the <see cref="ITransaction" />.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public Double Price { get; set; }

        /// <summary>
        /// <para type="description">The monetary system the <see cref="ITransaction" /> is taking part in.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public ECurrency Currency { get; set; }

        /// <summary>
        /// <para type="description">No receipt was kept.</para>
        /// <para type="description">
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
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        public SwitchParameter NoReceipt { get; set; }

        /// <summary>
        /// <para type="description">
        /// An identifier for the physical receipt that a
        /// <see cref="ITransaction" /> corresponds to.
        /// </para>
        /// <para type="description">
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
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public String ReceiptID { get; set; }

        /// <summary>
        /// <para type="description">Meta-data.</para>
        /// <para type="description">
        /// Any additional structured (<see cref="Boolean" />)
        /// information or meta-data to be added to the
        /// <see cref="ITransaction" /> for the user's benefit.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-Receipt")]
        public IEnumerable<ITag> Tags { get; set; }

        /// <summary>
        /// <para type="description">Meta-data.</para>
        /// <para type="description">
        /// Any additional unstructured information to be
        /// added to the <see cref="ITransaction" /> for the
        /// user's benefit.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-Receipt")]
        public String Comment { get; set; }

        /// <summary>
        /// <para type="description">Date that the <see cref="ITransaction" /> took part on.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-Receipt")]
        public DateTime Posted { get; set; }
    }
}
