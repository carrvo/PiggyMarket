using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Accounting.Cmdlets
{
    [OutputType(typeof(ISubCategory<ICategory>))]
    public sealed class NewSubCategoryCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = true)]
        public ICategory Category { get; set; }

        [Parameter(Mandatory = true)]
        public String Name { get; set; }

        [Parameter(Mandatory = false)]
        public String Description { get; set; }
    }
}
