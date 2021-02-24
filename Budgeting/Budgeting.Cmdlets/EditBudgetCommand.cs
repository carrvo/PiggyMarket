using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Budgeting.Cmdlets
{
    [Cmdlet(VerbsData.Edit, "Budget", ConfirmImpact = ConfirmImpact.Medium, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class EditBudgetCommand : Cmdlet
    {
    }
}
