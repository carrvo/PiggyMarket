using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Account Context</para>
    /// </summary>
    [Cmdlet(VerbsSecurity.Grant, "AccountContext", ConfirmImpact = ConfirmImpact.High, RemotingCapability = RemotingCapability.PowerShell, SupportsPaging = false, SupportsShouldProcess = true)]
    public sealed class GrantAccountContextCommand : Cmdlet
    {
        /// <summary>
        /// Kudoos to: https://stackoverflow.com/questions/201323/how-to-validate-an-email-address-using-a-regular-expression
        /// Reference: http://worsethanfailure.com/articles/Validating_Email_Addresses
        /// </summary>
        const String ValidEmailPattern = "^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z] (-?[a-zA-Z0-9])*(\\.[a-zA-Z] (-?[a-zA-Z0-9])*)+$";

        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description"><see cref="Email"/> of a new user to add to the current <see cref="IAccountContext"/>.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidatePattern(ValidEmailPattern)]
        public String Email { get; set; }
    }
}
