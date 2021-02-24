using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Accounting.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Retrives the <see cref="ISubCategory{ICategory}"/>s.</para>
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
    [Cmdlet(VerbsCommon.Get, "SubCategory", ConfirmImpact = ConfirmImpact.None, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = true, SupportsShouldProcess = false)]
    [OutputType(typeof(ISubCategory<ICategory>))]
    public sealed class GetSubCategoryCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">Name of the <see cref="ISubCategory{ICategory}"/> to filter for.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public String Name { get; set; }

        /// <summary>
        /// <para type="description">Filters for all that belong to a <see cref="ICategory"/>.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public ICategory Category { get; set; }
    }
}
