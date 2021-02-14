using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    public sealed class GetAccountContextCommand : Cmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        [Parameter(Mandatory = true, ParameterSetName = "Guid")]
        [Parameter(Mandatory = true, ParameterSetName = "Current")]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public String Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Guid")]
        public Guid Guid { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Current")]
        public SwitchParameter Current { get; set; }
    }
}
