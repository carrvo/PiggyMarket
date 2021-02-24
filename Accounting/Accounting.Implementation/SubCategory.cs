using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Accounting.Implementation
{
    class SubCategory : ISubCategory<Category>, ITrendable
    {
        /// <summary>
        /// Security token to determine access control permissions.
        /// </summary>
        protected IAccessToken AccessToken { get; }

        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public Category Category => throw new NotImplementedException();
    }
}
