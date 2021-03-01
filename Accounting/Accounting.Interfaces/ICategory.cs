using System;
using System.Collections.Generic;

namespace Finance.Management.Service.Accounting.Interfaces
{

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
    /// <summary>
    /// Defines a type to categorize <see cref="ITransaction" />.
    /// This is used for budgeting purposes to organize
    /// and describe <see cref="ITransaction" />s.
    ///
    /// Categories are meant to only apply once to a
    /// <see cref="ITransaction" /> and to give them
    /// additional meaning behind their creation.
    /// Categories also serve to aggregate
    /// <see cref="ITransaction" />s that hold the same meaning.
    ///
    /// It is important that these are limited in the
    /// number of instances so that they can properly
    /// aggregate and summarize <see cref="ITransaction" />s,
    /// providing a course-grained view.
    /// </summary>
    public interface ICategory
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
    {
        /// <summary>
        /// Identifies the <see cref="ICategory" /> and its meaning.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Information about the <see cref="ICategory"/>.
        /// </summary>
        String Description { get; }

        /// <summary>
        /// Tracks the monetary value allocated to this
        /// <see cref="ICategory" />.
        /// </summary>
        Double CurrentFunds { get; set; }

        /// <summary>
        /// The monetary system the value is being stored in.
        /// </summary>
        ECurrency Currency { get; }
    }
}
