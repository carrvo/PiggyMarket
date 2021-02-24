using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Cmdlets
{
    [Cmdlet(VerbsCommon.Add, "Transfer", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class AddTransferCommand : Cmdlet
    {
    }
}
