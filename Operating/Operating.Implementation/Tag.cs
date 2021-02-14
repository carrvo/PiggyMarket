using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Operating.Implementation
{
    class Tag : ITag, ITrendable
    {
        protected IAccessToken AccessToken { get; }

        public string Name => throw new NotImplementedException();
    }
}
