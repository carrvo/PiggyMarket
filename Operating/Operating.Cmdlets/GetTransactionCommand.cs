using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Operating.Cmdlets
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IBankAccount, IPaymentMethod, IEnumerable<ITag>>;

    [OutputType(typeof(ITransaction))]
    public sealed class GetTransactionCommand : Cmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        [Parameter(Mandatory = true, ParameterSetName = "All")]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public String Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        public SwitchParameter UnCategorized { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Category")]
        public ICategory Category { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public IPayee Payee { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        [Alias("Min")]
        public Double Minimum { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        [Alias("Max")]
        public Double Maximum { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public DateTime PostedOnOrAfter { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public DateTime PostedOnOrBefore { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        [Parameter(Mandatory = false, ParameterSetName = "Category")]
        [Parameter(Mandatory = false, ParameterSetName = "All")]
        public IEnumerable<ITag> Tags { get; set; }
    }
}
