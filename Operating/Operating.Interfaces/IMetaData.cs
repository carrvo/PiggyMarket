using Finance.Management.Service.Accounting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Operating.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;

    /// <summary>
    /// Additional information added to the <see cref="ITransaction"/> for the user's benefit.
    /// </summary>
    public interface IMetaData<TTags>
        where TTags : IEnumerable<ITag>
    {
        /// <summary>
        /// Any additional unstructured information to be
        /// added to the <see cref="ITransaction" /> for the
        /// user's benefit.
        /// </summary>
        String Comment { get; set; }

        /// <summary>
        /// Any additional structured (<see cref="Boolean" />)
        /// information or meta-data to be added to the
        /// <see cref="ITransaction" /> for the user's benefit.
        /// </summary>
        TTags Tags { get; set; }

        /// <summary>
        /// An identifier for the physical receipt that a
        /// <see cref="ITransaction" /> corresponds to.
        ///
        /// Note that a receipt is a legally binding
        /// contract and is used as evidence that a
        /// <see cref="ITransaction" /> has occured.
        /// Receipts are, as a bonus to constituting
        /// evidence, often used for returns or
        /// <see cref="ITransaction" /> reversals.
        /// Either way, it will show up as a new
        /// <see cref="ITransaction" /> that has a
        /// <see cref="ITransaction.Price" /> that counteracts this
        /// <see cref="ITransaction" />, or at least
        /// the portion that is being refunded/reversed.
        /// </summary>
        String ReceiptID { get; }
    }
}
