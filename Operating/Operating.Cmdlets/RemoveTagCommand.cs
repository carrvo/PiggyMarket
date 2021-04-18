using Finance.Management.Service.Accounting.Interfaces;
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

    /// <summary>
    /// <para type="synopsis">Removes additional meta-data meaning to an <see cref="ITransaction"/>.</para>
    /// <para type="description">
    /// These work like <see cref="Boolean" />s
    /// and either the meaning applies or not.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "Tag", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    [OutputType(typeof(Transaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>))]
    public sealed class RemoveTagCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">The <see cref="ITransaction"/> applied against.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public ITransaction Transaction { get; set; }

        /// <summary>
        /// <para type="description">Meta-data to apply.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IEnumerable<ITag> Tags { get; set; }
    }
}
