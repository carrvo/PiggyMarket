using System;
using System.Collections.Generic;
using Finance.Management.Service.Accounting.Interfaces;

namespace Finance.Management.Service.Operating.Interfaces
{
    using ITransaction = ITransaction<ISubCategory<ICategory>, IEnumerable<ITag>>;
    using ISubCategory = ISubCategory<ICategory>;

    /// <summary>
    /// Existence.
    /// </summary>
    public interface ITransaction<TSubCategory, TTags>
        where TSubCategory : ISubCategory<ICategory>
        where TTags : IEnumerable<ITag>
    {
        /// <summary>
        /// Identifies the <see cref="ITransaction" />
        /// so that it can be recognized.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// The <see cref="ISubCategory" /> that gives
        /// the <see cref="ITransaction" /> meaning.
        /// </summary>
        TSubCategory SubCategory { get; set; }

        /// <summary>
        /// The monetary value of the <see cref="ITransaction" />.
        /// </summary>
        Double Price { get; }

        /// <summary>
        /// Any additional structured (<see cref="Boolean" />)
        /// information or meta-data to be added to the
        /// <see cref="ITransaction" /> for the user's benefit.
        /// </summary>
        TTags Tags { get; set; }

        /// <summary>
        /// Splits the <see cref="ITransaction" /> into two whose
        /// <see cref="Price" />s sum up to the original <see cref="Price" />.
        ///
        /// Allows for the itemization of transactions.
        /// </summary>
        /// <param name="originalName">New name for the original <see cref="ITransaction"/>.</param>
        /// <param name="newName">New name for the new <see cref="ITransaction"/>.</param>
        /// <param name="newPrice"><see cref="Price"/> for the new <see cref="ITransaction"/>,
        /// whose value is subtracted from the original <see cref="ITransaction"/>.</param>
        void Split(String originalName, String newName, Double newPrice);
    }
}
