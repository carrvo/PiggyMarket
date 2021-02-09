using System;
using Finance.Management.Service.Accounting.Interfaces;

namespace Finance.Management.Service.Operating.Interfaces
{
    /// <summary>
    /// Defines a type to provide additional meta-data
    /// meaning about a <see cref="ITransaction" />.
    ///
    /// These work like <see cref="Boolean" />s
    /// and either the meaning applies or not.
    /// </summary>
    public interface ITag
    {
        /// <summary>
        /// Identifies the meta-data meaning.
        /// </summary>
        String Name { get; }
    }
}
