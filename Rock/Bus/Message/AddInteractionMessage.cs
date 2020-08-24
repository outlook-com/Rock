using Rock.Bus.Message;
using Rock.Transactions;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class AddInteractionMessage
    {
        /// <summary>
        /// Gets or sets the streak type identifier.
        /// </summary>
        public int SomeInt { get; set; }
    }
}
