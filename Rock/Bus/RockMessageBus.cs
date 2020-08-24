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

using MassTransit;
using Rock.Bus.Consumer;
using Rock.Bus.Message;
using Rock.Bus.Transport;
using Rock.Transactions;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Rock.Bus
{
    /// <summary>
    /// Rock Bus Process Controls: Start the bus
    /// </summary>
    public static class RockMessageBus
    {
        private static readonly ConcurrentBag<IBusControl> _buses = new ConcurrentBag<IBusControl>();

        /// <summary>
        /// Starts this bus.
        /// </summary>
        public static async void Start()
        {
            var componentNames = new[]
            {
                //"Rock.Bus.Transport.Component.InMemory",
                "Rock.Bus.Transport.Component.RabbitMQ"
            };

            foreach ( var componentName in componentNames )
            {
                var component = BusTransportContainer.GetComponent( componentName );
                var bus = component.Create(
                    //() => (IRockConsumer<IRockMessage>) new TransactionRunnerConsumer(),
                    () => new DebugLogConsumer()
                );
                _buses.Add( bus );

                await bus.StartAsync();

                bus.ConnectConsumer<StreakTypeRebuildTransaction>();
                bus.ConnectConsumer<InteractionTransaction>();
            }
        }

        /// <summary>
        /// Publishes the specified rock message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static async Task Publish<T>( T message ) where T : class
        {
            Debug.WriteLine( $"Publishing {typeof( T )}" );

            foreach ( var bus in _buses )
            {
                await bus.Publish( message );
            }
        }
    }
}
