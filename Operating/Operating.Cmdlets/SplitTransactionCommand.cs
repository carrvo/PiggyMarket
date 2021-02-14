using Finance.Management.Service.Accounting.Interfaces;
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

    [OutputType(typeof(Nullable))]
    public sealed class SplitTransactionCommand : Cmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ITransaction Transaction { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public String Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        public SwitchParameter UnCategorized { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = true, ParameterSetName = "SubCategory")]
        public Double Price { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public IEnumerable<ITag> Tags { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "UnCategorized")]
        [Parameter(Mandatory = false, ParameterSetName = "SubCategory")]
        public String Comment { get; set; }
    }
}
