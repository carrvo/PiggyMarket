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
    [OutputType(typeof(ICategory))]
    public sealed class GetCategoryCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = false)]
        public String Name { get; set; }
    }
}
