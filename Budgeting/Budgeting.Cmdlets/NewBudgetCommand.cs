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
    public sealed class NewBudgetCommand : Cmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        public ICategory Category { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        [Alias("ItemName")]
        public String TransactionName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public Double Target { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public ECurrency Currency { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public TimeSpan Period { get; set; }
    }
}
