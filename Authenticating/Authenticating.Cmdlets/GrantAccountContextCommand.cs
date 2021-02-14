using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Authenticating.Cmdlets
{
    public sealed class GrantAccountContextCommand : Cmdlet
    {
        /// <summary>
        /// Kudoos to: https://stackoverflow.com/questions/201323/how-to-validate-an-email-address-using-a-regular-expression
        /// Reference: http://worsethanfailure.com/articles/Validating_Email_Addresses
        /// </summary>
        const String ValidEmailPattern = "^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z] (-?[a-zA-Z0-9])*(\\.[a-zA-Z] (-?[a-zA-Z0-9])*)+$";

        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = true)]
        [ValidatePattern(ValidEmailPattern)]
        public String Email { get; set; }
    }
}
