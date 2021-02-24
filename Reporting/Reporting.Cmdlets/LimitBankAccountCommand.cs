using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Reporting.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Sets a limit on a <see cref="IBankAccount"/>.</para>
    /// <para type="description">Sets a limit for notification purposes ONLY.</para>
    /// <para type="description">
    /// This should be useful for anyone who wants to
    /// transfer execess funds to savings or investments.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsData.Limit, "BankAccount", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = false)]
    public sealed class LimitBankAccountCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">The <see cref="IBankAccount"/> to set a notification limit for.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public IBankAccount Account { get; set; }

        /// <summary>
        /// <para type="description">The notification limit to set.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public Double Above { get; set; }
    }
}
