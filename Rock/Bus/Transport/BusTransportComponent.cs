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
using System.Collections.ObjectModel;
using MassTransit;
using Rock.Bus.Consumer;
using Rock.Bus.Message;

namespace Rock.Bus.Transport
{
    /// <summary>
    /// Base class for bus transport components
    /// </summary>
    public abstract class BusTransportComponent : Rock.Extension.Component
    {
        /// <summary>
        /// Create the transport
        /// </summary>
        /// <param name="consumerFactories">The consumer factories.</param>
        /// <returns></returns>
        public abstract IBusControl Create( params Func<IRockConsumer<IRockMessage>>[] consumerFactories );

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public override bool IsActive
        {
            get => true;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public override int Order
        {
            get => 0;
        }
    }
}
