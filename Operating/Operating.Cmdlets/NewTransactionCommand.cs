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
    public sealed class NewTransactionCommand : Cmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public IAccessToken AccessToken { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public String Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public IPayee Payee { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        public SwitchParameter UnCategorized { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public ISubCategory<ICategory> SubCategory { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        public SwitchParameter Cash { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        public SwitchParameter Cheque { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        public SwitchParameter Credit { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public SwitchParameter Debit { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public Double Price { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public ECurrency Currency { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        public SwitchParameter NoReceipt { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = true, ParameterSetName = "Debit-SubCategory-Receipt")]
        public String ReceiptID { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-Receipt")]
        public IEnumerable<ITag> Tags { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-Receipt")]
        public String Comment { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-NoReceipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cash-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Cheque-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Credit-SubCategory-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-UnCategorized-Receipt")]
        [Parameter(Mandatory = false, ParameterSetName = "Debit-SubCategory-Receipt")]
        public DateTime Posted { get; set; }
    }
}
