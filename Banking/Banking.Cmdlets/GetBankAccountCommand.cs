using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "BankAccount", ConfirmImpact = ConfirmImpact.None, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = true, SupportsShouldProcess = false)]
    public sealed class GetBankAccountCommand : Cmdlet
    {
    }
}
