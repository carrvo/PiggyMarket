using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Implementation
{
    class PaymentMethod : IPaymentMethod, ITrendable
    {
        public String Name
        {
            get
            {
                return Method.ToString();
            }
        }

        public EPaymentMethod Method => throw new NotImplementedException();
    }
}
