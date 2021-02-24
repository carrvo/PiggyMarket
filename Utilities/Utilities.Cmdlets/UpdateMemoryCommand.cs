using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Utilities.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates the in-memory object from the authority source.</para>
    /// <para type="description">This is ONLY a syncing method for refreshing the in-memory object with a new copy.</para>
    /// </summary>
    [Cmdlet(VerbsData.Update, "Memory", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = false)]
    public sealed class UpdateMemoryCommand : Cmdlet
    {
    }
}
