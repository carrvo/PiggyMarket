using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Cmdlets
{
    [Cmdlet(VerbsData.Update, "BankAccount", ConfirmImpact = ConfirmImpact.Medium, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class UpdateBankAccountCommand : Cmdlet
    {
    }
}
