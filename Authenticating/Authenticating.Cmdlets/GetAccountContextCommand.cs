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
    [Cmdlet(VerbsCommon.Get, "AccountContext", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = true, SupportsShouldProcess = false)]
    public sealed class GetAccountContextCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        [Parameter(Mandatory = true, ParameterSetName = "Guid")]
        [Parameter(Mandatory = true, ParameterSetName = "Current")]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">Friendly Name for the <see cref="IAccountContext"/>.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public String Name { get; set; }

        /// <summary>
        /// <para type="description">Identifier for the <see cref="IAccountContext"/>.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Guid")]
        public Guid Guid { get; set; }

        /// <summary>
        /// <para type="description">The <see cref="Current"/> <see cref="IAccountContext"/>.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Current")]
        public SwitchParameter Current { get; set; }
    }
}
