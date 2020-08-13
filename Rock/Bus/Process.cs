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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Mono.CSharp;
using Rock.Bus.Message;

namespace Rock.Bus
{
    /// <summary>
    /// Rock Bus Process Controls: Start the bus
    /// </summary>
    public static class Process
    {
        /// <summary>
        /// Starts this bus.
        /// </summary>
        public static async void Start()
        {
            var bus = MassTransit.Bus.Factory.CreateUsingInMemory( ConfigureInMemory );
            await bus.StartAsync();

            await bus.Publish( new RockMessage { Source = "Hi" } );
        }

        /// <summary>
        /// Configures the in memory.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        private static void ConfigureInMemory( IInMemoryBusFactoryConfigurator configurator )
        {
            configurator.ReceiveEndpoint( "test_queue", ep =>
            {
                ep.Handler<RockMessage>( context =>
                {
                    var message = $"Bus Received: {context.Message.Source} {context.Message.Time} {context.Message.Id}";
                    Debug.WriteLine( message );
                    return Console.Out.WriteLineAsync( message );
                } );
            } );
        }
    }
}
