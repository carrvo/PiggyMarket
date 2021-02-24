﻿using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "AccountContext", ConfirmImpact = ConfirmImpact.Medium, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class SetAccountContextCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        [Parameter(Mandatory = true, ParameterSetName = "Guid")]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public String Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Guid")]
        public Guid Guid { get; set; }
    }
}