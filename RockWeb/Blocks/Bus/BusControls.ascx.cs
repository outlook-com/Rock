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
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Rock.Bus;
using Rock.Bus.Consumer;
using Rock.Bus.Message;
using Rock.Model;
using Rock.Transactions;
using Rock.Web.Cache;
using Rock.Web.UI;

namespace RockWeb.Blocks.Bus
{
    [DisplayName( "Bus Controls" )]
    [Category( "Bus" )]
    [Description( "Control the bus." )]

    public partial class BusControls : RockBlock
    {
        /// <summary>
        /// Handles the Click event of the btnPublishStreakRebuild control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnPublishStreakRebuild_Click( object sender, EventArgs e )
        {
            var streakType = StreakTypeCache.All().FirstOrDefault();

            /*
            RockMessageBus.Publish( new StreakRebuildMessage
            {

                StreakTypeId = streakType == null ? 0 : streakType.Id
            } );
            */

            for ( var i = 0; i < 10; i++ )
            {
                RockMessageBus.Publish( new StreakRebuildMessage
                {
                    StreakTypeId = 1
                } );
            }
        }

        /// <summary>
        /// Handles the Click event of the btnPublishAddInteraction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnPublishAddInteraction_Click( object sender, EventArgs e )
        {
            for ( var i = 0; i < 10; i++ )
            {
                RockMessageBus.Publish( new AddInteractionMessage() );
            }
        }
    }
}