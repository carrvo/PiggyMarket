using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Finance.Management.Service.Authenticating.Interfaces;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    [OutputType(typeof(IAccessToken))]
    public sealed class GrantAccessTokenCommand : Cmdlet
    {
    }
}
