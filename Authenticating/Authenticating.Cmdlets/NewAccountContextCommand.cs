using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Account Context</para>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AccountContext", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    [OutputType(typeof(IAccountContext))]
    public sealed class NewAccountContextCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">Friendly Name for the <see cref="IAccountContext"/>.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public String Name { get; set; }
    }
}
