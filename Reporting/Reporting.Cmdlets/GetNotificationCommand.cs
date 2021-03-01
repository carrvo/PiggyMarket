using Finance.Management.Service.Accounting.Interfaces;
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
    /// <para type="synopsis">Searches for notifications in the system.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Notification", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = true, SupportsShouldProcess = false)]
    public sealed class GetNotificationCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">The reason for the notification.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public String Reason { get; set; }

        /// <summary>
        /// <para type="description">Filter for the <see cref="ICategory"/> being notified about.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public ICategory Category { get; set; }

        /// <summary>
        /// <para type="description">Filter for the <see cref="IBankAccount"/> being notificed about.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public IBankAccount Account { get; set; }
    }
}
