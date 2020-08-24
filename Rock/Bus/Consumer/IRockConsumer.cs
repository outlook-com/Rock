using MassTransit;
using Rock.Bus.Message;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRockConsumer<T> : IConsumer<T>
        where T : class, IRockMessage
    {
    }
}
