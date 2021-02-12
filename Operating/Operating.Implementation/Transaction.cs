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
    class Transaction<TSubCategory, TBankAccount, TPymentMethod, TTags>
        : ITransaction<TSubCategory, TBankAccount, TPymentMethod, TTags>, ISubCategory<ICategory>, ITrendable
        where TSubCategory : ISubCategory<ICategory>
        where TBankAccount : IBankAccount
        where TPymentMethod : IPaymentMethod
        where TTags : IEnumerable<ITag>
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TSubCategory SubCategory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TBankAccount? BankAccount => throw new NotImplementedException();

        public TPymentMethod PaymentMethod => throw new NotImplementedException();

        public IPayee Payee => throw new NotImplementedException();

        public double Price => throw new NotImplementedException();

        public ECurrency Currency => throw new NotImplementedException();

        public DateTime Posted => throw new NotImplementedException();

        public DateTime Processed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Comment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TTags Tags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ReceiptID => throw new NotImplementedException();

        public bool Verified => throw new NotImplementedException();

        public void Split(string originalName, string newName, double newPrice)
        {
            throw new NotImplementedException();
        }
    }
}
