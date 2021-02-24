using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Budgeting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Budgeting.Cmdlets
{
    /// <summary>
    /// <para type="description">Used for monetary planning, tracking, and management.</para>
    /// <para type="description">
    /// This is the linchpin of the system. This is
    /// where user's are to define the monetary funds
    /// that they allocate, including their income.
    /// This is also where new <see cref="ITracking">
    /// are defined on a <see cref="IBudget.Period">ic
    /// basis. This type defines the planning stage to
    /// inform the <see cref="ICategory"> management and
    /// <see cref="ITracking">; all of which form the
    /// complete <see cref="IBudget">ing system.
    /// </para>
    /// <para type="description">
    /// The complete <see cref="IBudget">ing system is
    /// intended to function as follows:
    /// 1) A user allocates monetary funds to a
    /// <see cref="ICategory"> using a <see cref="IBudget">.
    /// 2) The user then spends the allocated monetary funds
    /// from their <see cref="ICategory">, treating it as a
    /// virtual <see cref="IAccountable">, and also selecting
    /// a physical <see cref="IAccountable"> for which to
    /// spend from--together forming the <see cref="ITransaction">.
    /// 3) Finally the user <see cref="ITracking">Tracks</see> the
    /// <see cref="ITransaction" />s at the end of the
    /// <see cref="IBudget.Period" /> to see whether they were
    /// over or under <see cref="IBudget" />, and either update
    /// as necessary or change their spending behaviour accordingly.
    /// </para>
    /// <para type="description">
    /// <see cref="IBudget" />ing is not meant to prescribe any
    /// lifestyle, but to determine if such a lifestyle is
    /// affordable for the user.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Budget", ConfirmImpact = ConfirmImpact.None, DefaultParameterSetName = "Category", RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = true, SupportsShouldProcess = false)]
    [OutputType(typeof(IBudget<ICategory, ISubCategory<ICategory>>))]
    public sealed class GetBudgetCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Name")]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        public ICategory Category { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Name")]
        [Alias("ItemName")]
        public String TransactionName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Name")]
        public TimeSpan Period { get; set; }
    }
}
