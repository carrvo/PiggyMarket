using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Utilities.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Migrates the application to a new version.</para>
    /// </summary>
    [Cmdlet("Migrate", "SchemaVersion", ConfirmImpact = ConfirmImpact.High, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class MigrateSchemaVersionCommand : Cmdlet
    {
    }
}
