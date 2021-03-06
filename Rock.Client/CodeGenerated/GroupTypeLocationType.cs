//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;


namespace Rock.Client
{
    /// <summary>
    /// Base client model for GroupTypeLocationType that only includes the non-virtual fields. Use this for PUT/POSTs
    /// </summary>
    public partial class GroupTypeLocationTypeEntity
    {
        /// <summary />
        public int GroupTypeId { get; set; }

        /// <summary />
        public int LocationTypeValueId { get; set; }

        /// <summary>
        /// Copies the base properties from a source GroupTypeLocationType object
        /// </summary>
        /// <param name="source">The source.</param>
        public void CopyPropertiesFrom( GroupTypeLocationType source )
        {
            this.GroupTypeId = source.GroupTypeId;
            this.LocationTypeValueId = source.LocationTypeValueId;

        }
    }

    /// <summary>
    /// Client model for GroupTypeLocationType that includes all the fields that are available for GETs. Use this for GETs (use GroupTypeLocationTypeEntity for POST/PUTs)
    /// </summary>
    public partial class GroupTypeLocationType : GroupTypeLocationTypeEntity
    {
        /// <summary />
        public GroupType GroupType { get; set; }

        /// <summary />
        public DefinedValue LocationTypeValue { get; set; }

    }
}
