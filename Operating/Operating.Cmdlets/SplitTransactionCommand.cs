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

    /// <summary>
    /// <para type="synopsis">Splits a <see cref="ITransaction"/> into two <see cref="ITransaction"/>s.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Split, "Transaction", ConfirmImpact = ConfirmImpact.Medium, DefaultParameterSetName = "SubCategory", RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    [OutputType(typeof(Nullable))]
    public sealed class SplitTransactionCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">The <see cref="ITransaction"/> to split.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ITransaction Transaction { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public String Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        public SwitchParameter UnCategorized { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public Double Price { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public IEnumerable<ITag> Tags { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public String Comment { get; set; }
    }
}
