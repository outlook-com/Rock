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

using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;
using Rock.Bus.Message;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// Log messages for debug
    /// </summary>
    public class DebugLogConsumer : IRockConsumer<IRockMessage>
    {
        /// <summary>
        /// Consumes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Consume( ConsumeContext<IRockMessage> context )
        {
            return Task.Run(() => {
                var messageAsJson = context.Message.ToJson( Newtonsoft.Json.Formatting.Indented );
                var channel = context.ReceiveContext.InputAddress;
                var message = $"-- BEGIN BUS DEBUG --\n{channel}\n{messageAsJson}\n-- END BUS DEBUG --";
                Debug.WriteLine( message );
            } );
        }
    }
}
