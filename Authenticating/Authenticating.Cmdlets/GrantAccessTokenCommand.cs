using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Finance.Management.Service.Authenticating.Interfaces;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Authenticate and receive an Authorization token.</para>
    /// </summary>
    [Cmdlet(VerbsSecurity.Grant, "AccessToken", ConfirmImpact = ConfirmImpact.High, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = false)]
    [OutputType(typeof(IAccessToken))]
    public sealed class GrantAccessTokenCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Request for ALL permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "All")]
        public SwitchParameter All { get; set; }
    }
}
