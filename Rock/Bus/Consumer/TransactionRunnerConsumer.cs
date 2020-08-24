using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Rock.Bus.Message;
using Rock.Transactions;
using Rock.Web.Cache;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionRunnerConsumer : IConsumer<TransactionRunnerMessage>
    {
        /// <summary>
        /// Consumes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Consume( ConsumeContext<TransactionRunnerMessage> context )
        {
            return Task.Run( () =>
            {
                Debug.WriteLine( $"Consuming {context.Message.TransactionClassName}" );
            } );
        }
    }
}
