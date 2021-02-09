using System;

namespace Finance.Management.Service.Reporting
{
    /// <summary>
    /// Defines a type to be used for <see cref="ITracking" /> purposes.
    /// This includes: grouping, filtering, and sorting data.
    /// </summary>
    public interface ITrendable
    {
        /// <summary>
        /// Identifies the criteria used for <see cref="ITracking" />.
        /// </summary>
        String Name { get; }
    }
}
