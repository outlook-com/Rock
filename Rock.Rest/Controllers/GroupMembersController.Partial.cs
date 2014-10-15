﻿// <copyright>
// Copyright 2013 by the Spark Development Network
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
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
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Rock.Model;
using Rock.Rest.Filters;
using Rock.Web.Cache;

namespace Rock.Rest.Controllers
{
    public partial class GroupMembersController 
    {

        /// <summary>
        /// Overrides base Get controller method to include deceased GroupMembers
        /// </summary>
        /// <returns>A queryable collection of GroupMembers, including deceased, that match the provided query.</returns>
        [Authenticate, Secured]
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All)]
        public override IQueryable<GroupMember> Get()
        {
            var queryString = Request.RequestUri.Query;
            var includeDeceased = System.Web.HttpUtility.ParseQueryString( queryString ).Get( "IncludeDeceased" );

            if ( includeDeceased.AsBoolean( false ) )
            {
                var rockContext = new Rock.Data.RockContext();
                return new GroupMemberService( rockContext ).Queryable( true );
            }
            else
            {
                return base.Get();
            }
        }


    }
}
