using Finance.Management.Service.Accounting.Interfaces;
using Finance.Management.Service.Authenticating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Management.Service.Accounting.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Retrives the <see cref="ICategory"/>s.</para>
    /// <para type="description">
    /// These are used for budgeting purposes to organize
    /// and describe <see cref="ITransaction" />s.
    /// </para>
    /// <para type="description">
    /// Categories are meant to only apply once to a
    /// <see cref="ITransaction" /> and to give them
    /// additional meaning behind their creation.
    /// Categories also serve to aggregate
    /// <see cref="ITransaction" />s that hold the same meaning.
    /// </para>
    /// </summary>
    [OutputType(typeof(ICategory))]
    public sealed class GetCategoryCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Security token to determine access control permissions.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public IAccessToken AccessToken { get; set; }

        /// <summary>
        /// <para type="description">Name of the <see cref="ICategory"/> to filter for.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public String Name { get; set; }
    }
}
