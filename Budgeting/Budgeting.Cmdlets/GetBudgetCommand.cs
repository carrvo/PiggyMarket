using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Budgeting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Budgeting.Cmdlets
{
    [OutputType(typeof(IBudget<ICategory, ISubCategory<ICategory>>))]
    public sealed class GetBudgetCommand : Cmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Name")]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        public ICategory Category { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Name")]
        [Alias("ItemName")]
        public String TransactionName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Name")]
        public TimeSpan Period { get; set; }
    }
}
