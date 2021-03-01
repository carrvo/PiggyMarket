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

namespace Finance.Management.Service.Accounting.Cmdlets
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>;
    using ISubCategory = ISubCategory<ICategory>;

    /// <summary>
    /// <para type="synopsis">Creates a new <see cref="ISubCategory"/>.</para>
    /// <para type="description">
    /// This allows for a more fine-grained
    /// organization to provide a higher fidelity view, while
    /// working in conjuction with <see cref="ICategory" /> whose
    /// purpose is to aggregate and summarize.
    /// </para>
    /// <para type="description">
    /// This leads to applying <see cref="ICategory" /> indirectly
    /// to <see cref="ITransaction" />s. They will have a
    /// <see cref="ISubCategory" /> applied to them; that then
    /// corresponds to a <see cref="ICategory" />, for
    /// aggregation purposes.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "SubCategory", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    [OutputType(typeof(ISubCategory<ICategory>))]
    public sealed class NewSubCategoryCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">The <see cref="ICategory"/> this belongs to.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public ICategory Category { get; set; }

        /// <summary>
        /// <para type="description">The <see cref="ISubCategory{ICategory}.Name"/>.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public String Name { get; set; }

        /// <summary>
        /// <para type="description">The <see cref="ISubCategory{ICategory}.Description"/>.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public String Description { get; set; }
    }
}
