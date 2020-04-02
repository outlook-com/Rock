using System.Linq;
using System.Net;
using Rock.Web.Cache;

namespace Rock.Obsidian.Controls
{
    public class DefinedTypePicker
    {
        /// <summary>
        /// Gets all defined types.
        /// </summary>
        /// <returns></returns>
        [ControlAction( "GetAllDefinedTypes" )]
        public ControlActionResult GetAllDefinedTypes()
        {
            var definedTypes = DefinedTypeCache.All().Where( dt => dt.IsActive ).Select( dt => new
            {
                dt.Name,
                dt.Guid
            } );

            return new ControlActionResult( HttpStatusCode.Created, definedTypes );
        }
    }
}
