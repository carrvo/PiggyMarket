using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Implementation
{
    class BankAccount : IBankAccount, ITrendable, IAccountable, IPayee
    {
        public string Name => throw new NotImplementedException();

        public string BankName => throw new NotImplementedException();

        public double CurrentFunds => throw new NotImplementedException();

        public ECurrency Currency => throw new NotImplementedException();
    }
}
