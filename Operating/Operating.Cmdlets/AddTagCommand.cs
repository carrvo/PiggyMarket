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

    [OutputType(typeof(ITransaction))]
    public sealed class AddTagCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public ITransaction Transaction { get; set; }

        [Parameter(Mandatory = true)]
        public IEnumerable<ITag> Tags { get; set; }
    }
}
