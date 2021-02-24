using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Accounting.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "SubCategory", ConfirmImpact = ConfirmImpact.High, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class RemoveSubCategoryCommand : Cmdlet
    {
    }
}
