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
    public sealed class GetNotificationCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = false)]
        public String Reason { get; set; }

        [Parameter(Mandatory = false)]
        public ICategory Category { get; set; }

        [Parameter(Mandatory = false)]
        public IBankAccount Account { get; set; }
    }
}
