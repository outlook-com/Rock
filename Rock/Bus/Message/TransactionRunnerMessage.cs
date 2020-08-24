namespace Rock.Bus.Message
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionRunnerMessage
    {
        /// <summary>
        /// Gets or sets the name of the transaction class.
        /// </summary>
        public string TransactionClassName { get; set; }
    }
}
