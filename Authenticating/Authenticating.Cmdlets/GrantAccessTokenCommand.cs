using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Finance.Management.Service.Authenticating.Interfaces;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    [Cmdlet(VerbsSecurity.Grant, "AccessToken", ConfirmImpact = ConfirmImpact.High, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = false)]
    [OutputType(typeof(IAccessToken))]
    public sealed class GrantAccessTokenCommand : Cmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "All")]
        public SwitchParameter All { get; set; }
    }
}
