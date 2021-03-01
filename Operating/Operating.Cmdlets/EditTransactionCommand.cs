using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Operating.Cmdlets
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>;
    using ISubCategory = ISubCategory<ICategory>;

    /// <summary>
    /// <para type="synopsis">Modifies the <see cref="ITransaction"/>.</para>
    /// </summary>
    [Cmdlet(VerbsData.Edit, "Transaction", ConfirmImpact = ConfirmImpact.Medium, DefaultParameterSetName = "SubCategory", RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    [OutputType(typeof(ITransaction))]
    public sealed class EditTransactionCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">The <see cref="ITransaction"/> to edit.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ITransaction Transaction { get; set; }

        /// <summary>
        /// <para type="description">
        /// Identifies the <see cref="ITransaction" />
        /// so that it can be recognized.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public String Name { get; set; }

        /// <summary>
        /// <para type="description">No meaning is associated with the <see cref="ITransaction" />.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        public SwitchParameter UnCategorized { get; set; }

        /// <summary>
        /// <para type="description">
        /// The <see cref="ISubCategory" /> that gives
        /// the <see cref="ITransaction" /> meaning.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        /// <summary>
        /// <para type="description">Meta-data.</para>
        /// <para type="description">
        /// Any additional unstructured information to be
        /// added to the <see cref="ITransaction" /> for the
        /// user's benefit.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public String Comment { get; set; }
    }
}
