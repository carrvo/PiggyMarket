﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Banking</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "BankAccount", ConfirmImpact = ConfirmImpact.Low, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class AddBankAccountCommand : Cmdlet
    {
    }
}
