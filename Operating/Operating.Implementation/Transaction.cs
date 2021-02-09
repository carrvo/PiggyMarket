using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Banking.Interfaces;
using Finance.Management.Service.Operating.Interfaces;
using Finance.Management.Service.Reporting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Operating.Implementation
{
    class Transaction<SC, BA, PM, Ts> : ITransaction<SC, BA, PM, Ts>, ISubCategory<ICategory>, ITrendable
        where SC : ISubCategory<ICategory>
        where BA : IBankAccount
        where PM : IPaymentMethod
        where Ts : IEnumerable<ITag>
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SC SubCategory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BA? BankAccount => throw new NotImplementedException();

        public PM PaymentMethod => throw new NotImplementedException();

        public IPayee Payee => throw new NotImplementedException();

        public double Price => throw new NotImplementedException();

        public ECurrency Currency => throw new NotImplementedException();

        public Date Posted => throw new NotImplementedException();

        public Date Processed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Comment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Ts Tags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ReceiptID => throw new NotImplementedException();

        public bool Verified => throw new NotImplementedException();

        public void Split(string originalName, string newName, double newPrice)
        {
            throw new NotImplementedException();
        }
    }
}
