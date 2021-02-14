using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Banking.Interfaces
{
    public interface IPaymentMethod
    {
        String Name { get; }

        EPaymentMethod Method { get; }
    }
}
