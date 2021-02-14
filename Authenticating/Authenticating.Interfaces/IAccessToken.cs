using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Permissions;
using System.Security.Authentication;
using System.Security.Claims;

namespace Finance.Management.Service.Authenticating.Interfaces
{
    public interface IAccessToken
    {
        IAccountContext Context { get; }

        IIdentity Identity { get; }

        IPrincipal Principal { get; }

        TokenImpersonationLevel ImpersonationLevel { get; }

        Claim Claim { get; }

        IReadOnlySet<String> Roles { get; }
    }
}
