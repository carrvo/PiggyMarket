using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Implementation;
using Finance.Management.Service.Operating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Operating.Cmdlets
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;
    using ISubCategory = ISubCategory<ICategory>;

    /// <summary>
    /// <para type="synopsis">Searches for the <see cref="ITransaction"/>s.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Transaction", ConfirmImpact = ConfirmImpact.None, DefaultParameterSetName = "All", RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = true, SupportsShouldProcess = false)]
    [OutputType(typeof(Transaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>))]
    public sealed class GetTransactionCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        [Parameter(Mandatory = true, ParameterSetName = "All")]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">Find a <see cref="ITransaction"/> by its identifier.</para>
        /// <para type="description">
        /// If multiple <see cref="ITransaction"/>s are desired, forgo this
        /// parameter and use the <code>Where-Object</code> cmdlet instead.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public String Name { get; set; }

        /// <summary>
        /// <para type="description">Filter for those <see cref="ITransaction"/> with no meaning associated.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        public SwitchParameter UnCategorized { get; set; }

        /// <summary>
        /// <para type="description">
        /// Filter for the <see cref="ISubCategory" /> that gives
        /// the <see cref="ITransaction" /> meaning.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        /// <summary>
        /// <para type="description">
        /// Filter for the <see cref="ICategory"/> this
        /// <see cref="ITransaction"/> is associated to.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        public ICategory Category { get; set; }

        /// <summary>
        /// <para type="description">Filter for the other side of the monetary funds movement.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public IPayee Payee { get; set; }

        /// <summary>
        /// <para type="description">Filter for <see cref="ITransaction.Price"/> equal or greater than this.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        [Alias("Min")]
        public Double Minimum { get; set; }

        /// <summary>
        /// <para type="description">Filter for <see cref="ITransaction.Price"/> equal or less than this.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        [Alias("Max")]
        public Double Maximum { get; set; }

        /// <summary>
        /// <para type="description">Filter for on or after the date that the <see cref="ITransaction" /> took part on.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public DateTime PostedOnOrAfter { get; set; }

        /// <summary>
        /// <para type="description">Filter for on or before the date that the <see cref="ITransaction" /> took part on.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public DateTime PostedOnOrBefore { get; set; }

        /// <summary>
        /// <para type="description">Meta-data to filter for.</para>
        /// <para type="description">
        /// Any additional structured (<see cref="Boolean" />)
        /// information or meta-data to be added to the
        /// <see cref="ITransaction" /> for the user's benefit.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public IEnumerable<ITag> Tags { get; set; }

        /// <summary>
        /// <para type="description">Meta-data to filter out.</para>
        /// <para type="description">
        /// Any additional structured (<see cref="Boolean" />)
        /// information or meta-data to be added to the
        /// <see cref="ITransaction" /> for the user's benefit.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public IEnumerable<ITag> NoTags { get; set; }
    }
}
