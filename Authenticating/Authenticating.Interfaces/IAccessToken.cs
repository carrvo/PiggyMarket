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
    /// <summary>
    /// Security token to determine access control permissions.
    /// </summary>
    public interface IAccessToken
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        IAccountContext Context { get; }

        IIdentity Identity { get; }

        IPrincipal Principal { get; }

        TokenImpersonationLevel ImpersonationLevel { get; }

        Claim Claim { get; }

        IReadOnlySet<String> Roles { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
